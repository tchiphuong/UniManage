using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.System.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.System.Application.Queries.Auth
{
    #region Query

    /// <summary>
    /// Truy vấn kiểm tra sự tồn tại của địa chỉ Email trong hệ thống
    /// </summary>
    public sealed class CheckEmailExistsQuery : BaseQuery, IRequest<ApiResponse<CheckEmailExistsQuery.Result>>
    {
        /// <summary>
        /// Địa chỉ Email cần kiểm tra
        /// </summary>
        public string Email { get; set; } = string.Empty;

        public class Result
        {
            /// <summary>
            /// Kết quả kiểm tra (True: đã tồn tại, False: chưa tồn tại)
            /// </summary>
            public bool Exists { get; set; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// Bộ kiểm tra dữ liệu cho truy vấn kiểm tra tồn tại Email
    /// </summary>
    public sealed class CheckEmailExistsQueryValidator : AbstractValidator<CheckEmailExistsQuery>
    {
        public CheckEmailExistsQueryValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_email))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Email)
                        .MaximumLength(100)
                        .WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_email, 100))
                        .Must(ValidationHelper.IsValidEmail)
                        .WithMessage(CoreResource.validation_invalidEmail);
                });
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Bộ xử lý truy vấn kiểm tra tồn tại Email
    /// </summary>
    public sealed class CheckEmailExistsQueryHandler : IRequestHandler<CheckEmailExistsQuery, ApiResponse<CheckEmailExistsQuery.Result>>
    {
        public async Task<ApiResponse<CheckEmailExistsQuery.Result>> Handle(CheckEmailExistsQuery request, CancellationToken cancellationToken)
        {
            // Khởi tạo log nghiệp vụ
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.Email), request.Email)
                }
            };

            try
            {
                using (var dbContext = new DbContext())
                {
                    // Sử dụng EF Core LINQ để kiểm tra tồn tại một cách nhanh chóng
                    var exists = await dbContext.Set<SyUsers>()
                        .AnyAsync(u => u.Email == request.Email, cancellationToken);

                    var result = new CheckEmailExistsQuery.Result
                    {
                        Exists = exists
                    };

                    var response = ResponseHelper.Success(result, string.Format(CoreResource.common_getSuccess, CoreResource.entity_user));
                    
                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = "Check email existence success";

                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                
                return ResponseHelper.Error<CheckEmailExistsQuery.Result>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}




