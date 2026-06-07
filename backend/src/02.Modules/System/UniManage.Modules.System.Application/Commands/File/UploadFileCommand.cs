using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using UniManage.Modules.System.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Domain;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.System.Application.Commands.File
{
    #region Command

    /// <summary>
    /// Lệnh tải lên file đa năng và bảo mật
    /// Unified and secure command for multi-purpose file uploads
    /// </summary>
    public sealed class UploadFileCommand : BaseCommand, IRequest<ApiResponse<string>>
    {
        /// <summary>
        /// Nội dung file từ client gửi lên
        /// The file content from client
        /// </summary>
        public IFormFile File { get; init; } = default!;
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho lệnh tải lên file
    /// </summary>
    public sealed class UploadFileValidator : AbstractValidator<UploadFileCommand>
    {
        public UploadFileValidator()
        {
            // Kiểm tra tập tin không được để trống
            RuleFor(x => x.File)
                .NotNull().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_file))
                .DependentRules(() =>
                {
                    RuleFor(x => x.File)
                        // Kiểm tra định dạng tập tin có nằm trong danh sách cho phép hay không
                        .Must(f => IsAllowed(f.FileName)).WithMessage(CoreResource.validation_invalidFormat)
                        // Kiểm tra dung lượng tập tin tối đa cho phép
                        .Must((cmd, f) => f.Length <= FileHelper.GetMaxFileSize(f.FileName))
                        .WithMessage((cmd, f) => string.Format(CoreResource.validation_maxLength, CoreResource.lbl_file, FileHelper.FormatFileSize(FileHelper.GetMaxFileSize(f.FileName))))
                        // Kiểm tra chữ ký tập tin (Magic Numbers) để ngăn chặn mã độc giả mạo đuôi mở rộng
                        .Must(f => FileHelper.IsValidFileSignature(f.OpenReadStream(), f.FileName)).WithMessage(CoreResource.validation_invalidFormat);
                });
        }

        /// <summary>
        /// Kiểm tra nhanh đuôi file có nằm trong danh sách cho phép hay không
        /// </summary>
        private static bool IsAllowed(string fileName) => 
            FileHelper.IsValidImageFile(fileName) || FileHelper.IsValidDocumentFile(fileName) || FileHelper.IsValidVideoFile(fileName) || FileHelper.IsValidAudioFile(fileName);
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý lệnh tải lên file
    /// </summary>
    public sealed class UploadFileHandler(IWebHostEnvironment env) : IRequestHandler<UploadFileCommand, ApiResponse<string>>
    {
        /// <summary>
        /// Hàm xử lý chính: Lưu file vật lý và ghi nhận vào Database
        /// </summary>
        public async Task<ApiResponse<string>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            // Khởi tạo log nghiệp vụ
            var log = new ApiLogModel(request.HeaderInfo) 
            { 
                Parameter = new List<LogParamModel> 
                { 
                    new(nameof(request.File.FileName), request.File.FileName), 
                    new(nameof(request.File.Length), FileHelper.FormatFileSize(request.File.Length)) 
                } 
            };

            try
            {
                // BƯỚC 1: Xác định đường dẫn lưu trữ (Luôn lưu vào thư mục TEMP trước)
                var timePath = DateTimeHelper.Now.ToString(FileHelper.FolderDateFormat);
                var relativeDirPath = Path.Combine(FileHelper.UploadPath, FileHelper.TempPath, timePath);
                var absoluteDirPath = Path.Combine(env.WebRootPath, relativeDirPath);
                
                // Tạo thư mục nếu chưa tồn tại
                FileHelper.EnsureDirectoryExists(absoluteDirPath);

                // BƯỚC 2: Tạo mã định danh duy nhất (FileCode) và tên file an toàn
                var extension = Path.GetExtension(request.File.FileName).ToLowerInvariant();
                var fileCode = Guid.NewGuid().ToString("N");
                var safeName = FileHelper.GenerateSafeFileName(request.File.FileName);
                var uniqueName = $"{fileCode}_{safeName}";
                var filePath = Path.Combine(absoluteDirPath, uniqueName);

                // BƯỚC 3: Ghi dữ liệu file xuống ổ cứng
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream, cancellationToken);
                }

                // BƯỚC 4: Ghi nhận thông tin file vào Database bằng EF Core
                var relativeUrl = "/" + Path.Combine(relativeDirPath, uniqueName).Replace("\\", "/");
                var fileCategory = GetFileCategory(request.File.FileName);

                using (var dbContext = new DbContext(openTransaction: true))
                {
                    var fileEntity = new SyFiles
                    {
                        FileCode = fileCode,
                        FileName = request.File.FileName,
                        FilePath = relativeUrl,
                        FileType = fileCategory,
                        FileExtension = extension,
                        FileSize = request.File.Length,
                        IsUsed = false, // Mặc định là file tạm
                        CreatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser,
                        CreatedAt = DateTimeHelper.Now
                    };

                    dbContext.Set<SyFiles>().Add(fileEntity);
                    await dbContext.SaveChangesAsync(cancellationToken);
                    await dbContext.CommitAsync(cancellationToken);
                }

                // BƯỚC 5: Trả về kết quả thành công kèm FileCode để Frontend sử dụng
                var response = ResponseHelper.Success(fileCode, CoreResource.common_uploadSuccess);
                
                log.Result = fileCode;
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                return response;
            }
            catch (Exception ex)
            {
                // Ghi nhận lỗi nếu có ngoại lệ xảy ra
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<string>(CoreResource.common_error);
            }
            finally
            {
                // Ghi log API tập trung
                UniLogManager.WriteApiLog(log);
            }
        }

        /// <summary>
        /// Phân loại file dựa trên đuôi mở rộng
        /// </summary>
        private static string GetFileCategory(string fileName) => fileName switch
        {
            _ when FileHelper.IsValidImageFile(fileName) => CoreConstant.FileCategory.Image,
            _ when FileHelper.IsValidDocumentFile(fileName) => CoreConstant.FileCategory.Document,
            _ when FileHelper.IsValidVideoFile(fileName) => CoreConstant.FileCategory.Video,
            _ when FileHelper.IsValidAudioFile(fileName) => CoreConstant.FileCategory.Audio,
            _ => CoreConstant.FileCategory.Others
        };
    }

    #endregion
}





