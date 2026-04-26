using FluentValidation;
using MediatR;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;

namespace UniManage.Application.Commands.System.User
{
    #region Command
    /// <summary>
    /// Example Command to create a user / Lệnh ví dụ để tạo người dùng
    /// </summary>
    public sealed class CreateUserExampleCommand : BaseCommand, IRequest<ApiResponse<long>>
    {
        /// <summary>
        /// Unique username of the user / Tên đăng nhập duy nhất của người dùng
        /// </summary>
        public string Username { get; init; } = default!;

        /// <summary>
        /// Raw password to be hashed / Mật khẩu thô sẽ được băm
        /// </summary>
        public string Password { get; init; } = default!;
    }
    #endregion

    #region Validator
    /// <summary>
    /// Validator for CreateUserExampleCommand / Bộ kiểm tra cho CreateUserExampleCommand
    /// </summary>
    public sealed class CreateUserExampleValidator : AbstractValidator<CreateUserExampleCommand>
    {
        public CreateUserExampleValidator()
        {
            // Get dynamic MaxLength from Entity / Lấy độ dài tối đa động từ thực thể
            int usernameMax = ValidationHelper.GetMaxLength<sy_users>(x => x.Username);
            int passwordMax = ValidationHelper.GetMaxLength<sy_users>(x => x.Password);

            // CascadeMode.Stop: Stop validation on first failure / Dừng kiểm tra ngay khi gặp lỗi đầu tiên
            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.user_username))
                .Length(3, usernameMax).WithMessage(string.Format(CoreResource.validation_range, CoreResource.user_username, 3, usernameMax))
                .MustAsync(async (username, cancel) => !await IsUsernameExistsAsync(username))
                .WithMessage(x => string.Format(CoreResource.validation_alreadyExists, CoreResource.user_username));

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.auth_password))
                .MinimumLength(8).WithMessage(string.Format(CoreResource.validation_minLength, CoreResource.auth_password, 8))
                .MaximumLength(passwordMax).WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.auth_password, passwordMax));
        }

        /// <summary>
        /// Check if username exists / Kiểm tra tên đăng nhập đã tồn tại chưa
        /// </summary>
        private static async Task<bool> IsUsernameExistsAsync(string username)
        {
            using (DbContext dbContext = new DbContext())
            {
                // Use EF Core for simple check / Dùng EF Core để kiểm tra đơn giản
                return await dbContext.Set<sy_users>().AnyAsync(x => x.Username == username);
            }
        }
    }
    #endregion

    #region Handler
    /// <summary>
    /// Handler for CreateUserExampleCommand / Bộ xử lý cho CreateUserExampleCommand
    /// </summary>
    public sealed class CreateUserExampleHandler : IRequestHandler<CreateUserExampleCommand, ApiResponse<long>>
    {
        public async Task<ApiResponse<long>> Handle(CreateUserExampleCommand request, CancellationToken ct)
        {
            // 1. Initialize standard Log (Explicit class initialization) 
            // 1. Khởi tạo Log chuẩn (Khởi tạo với tên Class tường minh)
            CoreLogModel log = new CoreLogModel(request.HeaderInfo)
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.Username), request.Username)
                }
            };

            // 2. Use DbContext with Transaction enabled / 2. Sử dụng DbContext có kèm Transaction
            using (DbContext dbContext = new DbContext(openTransaction: true))
            {
                try
                {
                    // Hash password using Utility / Logic băm mật khẩu từ Utility
                    string hash = PasswordHelper.HashPassword(request.Password);

                    sy_users user = new sy_users
                    {
                        Username = request.Username,
                        Password = hash,
                        CreatedBy = request.HeaderInfo?.Username ?? "SYSTEM",
                        CreatedAt = DateTimeHelper.Now
                    };

                    // 3. EF Core for CRUD operations / 3. EF Core cho các tác vụ CRUD
                    dbContext.Set<sy_users>().Add(user);
                    await dbContext.SaveChangesAsync(ct);
                    
                    // Commit Transaction / Xác nhận hoàn tất giao dịch
                    await dbContext.CommitAsync(ct);

                    // 4. Standard Response using CoreResource (user_... convention)
                    // 4. Response chuẩn dùng CoreResource (theo quy tắc user_...)
                    ApiResponse<long> response = ResponseHelper.Success(user.Id, CoreResource.user_msg_createSuccess);
                    
                    log.Result = user.Id;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;

                    return response;
                }
                catch (Exception ex)
                {
                    // Rollback on failure / Hoàn tác khi có lỗi
                    await dbContext.RollbackAsync(ct);

                    log.IsException = true; // bool type / Kiểu bool
                    log.Message = ex.Message;
                    
                    return ResponseHelper.Error<long>(CoreResource.common_error);
                }
                finally
                {
                    // Always write API Log in finally block / Luôn ghi log API trong khối finally
                    UniLogManager.WriteApiLog(log);
                }
            }
        }
    }
    #endregion
}
