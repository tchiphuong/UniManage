using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyAuth.Queries
{
    #region Query

    /// <summary>
    /// Truy v?n ki?m tra s? t?n t?i c?a d?a ch? Email trong h? th?ng
    /// </summary>
    public sealed class CheckEmailExistsQuery : BaseQuery, IRequest<ApiResponse<CheckEmailExistsQuery.Result>>
    {
        /// <summary>
        /// �?a ch? Email c?n ki?m tra
        /// </summary>
        public string Email { get; set; } = string.Empty;

        public class Result
        {
            /// <summary>
            /// K?t qu? ki?m tra (True: d� t?n t?i, False: chua t?n t?i)
            /// </summary>
            public bool Exists { get; set; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// B? ki?m tra d? li?u cho truy v?n ki?m tra t?n t?i Email
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
    /// B? x? l� truy v?n ki?m tra t?n t?i Email
    /// </summary>
    public sealed class CheckEmailExistsQueryHandler : IRequestHandler<CheckEmailExistsQuery, ApiResponse<CheckEmailExistsQuery.Result>>
    {
        public async Task<ApiResponse<CheckEmailExistsQuery.Result>> Handle(CheckEmailExistsQuery request, CancellationToken cancellationToken)
        {
            // Kh?i t?o log nghi?p v?
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
                    // S? d?ng EF Core LINQ d? ki?m tra t?n t?i m?t c�ch nhanh ch�ng
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




