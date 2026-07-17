using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyFile.Commands
{
    #region Command

    /// <summary>
    /// L?nh t?i l�n file da nang v� b?o m?t
    /// Unified and secure command for multi-purpose file uploads
    /// </summary>
    public sealed class UploadFileCommand : BaseCommand, IRequest<ApiResponse<string>>
    {
        /// <summary>
        /// N?i dung file t? client g?i l�n
        /// The file content from client
        /// </summary>
        public IFormFile File { get; init; } = default!;
    }

    #endregion

    #region Validator

    /// <summary>
    /// B? ki?m tra d? li?u cho l?nh t?i l�n file
    /// </summary>
    public sealed class UploadFileValidator : AbstractValidator<UploadFileCommand>
    {
        public UploadFileValidator()
        {
            // Ki?m tra t?p tin kh�ng du?c d? tr?ng
            RuleFor(x => x.File)
                .NotNull().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_file))
                .DependentRules(() =>
                {
                    RuleFor(x => x.File)
                        // Ki?m tra d?nh d?ng t?p tin c� n?m trong danh s�ch cho ph�p hay kh�ng
                        .Must(f => IsAllowed(f.FileName)).WithMessage(CoreResource.validation_invalidFormat)
                        // Ki?m tra dung lu?ng t?p tin t?i da cho ph�p
                        .Must((cmd, f) => f.Length <= FileHelper.GetMaxFileSize(f.FileName))
                        .WithMessage((cmd, f) => string.Format(CoreResource.validation_maxLength, CoreResource.lbl_file, FileHelper.FormatFileSize(FileHelper.GetMaxFileSize(f.FileName))))
                        // Ki?m tra ch? k� t?p tin (Magic Numbers) d? ngan ch?n m� d?c gi? m?o du�i m? r?ng
                        .Must(f => FileHelper.IsValidFileSignature(f.OpenReadStream(), f.FileName)).WithMessage(CoreResource.validation_invalidFormat);
                });
        }

        /// <summary>
        /// Ki?m tra nhanh du�i file c� n?m trong danh s�ch cho ph�p hay kh�ng
        /// </summary>
        private static bool IsAllowed(string fileName) => 
            FileHelper.IsValidImageFile(fileName) || FileHelper.IsValidDocumentFile(fileName) || FileHelper.IsValidVideoFile(fileName) || FileHelper.IsValidAudioFile(fileName);
    }

    #endregion

    #region Handler

    /// <summary>
    /// B? x? l� l?nh t?i l�n file
    /// </summary>
    public sealed class UploadFileHandler(IWebHostEnvironment env) : IRequestHandler<UploadFileCommand, ApiResponse<string>>
    {
        /// <summary>
        /// H�m x? l� ch�nh: Luu file v?t l� v� ghi nh?n v�o Database
        /// </summary>
        public async Task<ApiResponse<string>> Handle(UploadFileCommand request, CancellationToken cancellationToken)
        {
            // Kh?i t?o log nghi?p v?
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
                // BU?C 1: X�c d?nh du?ng d?n luu tr? (Lu�n luu v�o thu m?c TEMP tru?c)
                var timePath = DateTimeHelper.Now.ToString(FileHelper.FolderDateFormat);
                var relativeDirPath = Path.Combine(FileHelper.UploadPath, FileHelper.TempPath, timePath);
                var absoluteDirPath = Path.Combine(env.WebRootPath, relativeDirPath);
                
                // T?o thu m?c n?u chua t?n t?i
                FileHelper.EnsureDirectoryExists(absoluteDirPath);

                // BU?C 2: T?o m� d?nh danh duy nh?t (FileCode) v� t�n file an to�n
                var extension = Path.GetExtension(request.File.FileName).ToLowerInvariant();
                var fileCode = Guid.NewGuid().ToString("N");
                var safeName = FileHelper.GenerateSafeFileName(request.File.FileName);
                var uniqueName = $"{fileCode}_{safeName}";
                var filePath = Path.Combine(absoluteDirPath, uniqueName);

                // BU?C 3: Ghi d? li?u file xu?ng ? c?ng
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.File.CopyToAsync(stream, cancellationToken);
                }

                // BU?C 4: Ghi nh?n th�ng tin file v�o Database b?ng EF Core
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
                        IsUsed = false, // M?c d?nh l� file t?m
                        CreatedBy = request.HeaderInfo?.Username ?? CoreConstant.SystemUser,
                        CreatedAt = DateTimeHelper.Now
                    };

                    dbContext.Set<SyFiles>().Add(fileEntity);
                    await dbContext.SaveChangesAsync(cancellationToken);
                    await dbContext.CommitAsync(cancellationToken);
                }

                // BU?C 5: Tr? v? k?t qu? th�nh c�ng k�m FileCode d? Frontend s? d?ng
                var response = ResponseHelper.Success(fileCode, CoreResource.common_uploadSuccess);
                
                log.Result = fileCode;
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                return response;
            }
            catch (Exception ex)
            {
                // Ghi nh?n l?i n?u c� ngo?i l? x?y ra
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                return ResponseHelper.Error<string>(CoreResource.common_error);
            }
            finally
            {
                // Ghi log API t?p trung
                UniLogManager.WriteApiLog(log);
            }
        }

        /// <summary>
        /// Ph�n lo?i file d?a tr�n du�i m? r?ng
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





