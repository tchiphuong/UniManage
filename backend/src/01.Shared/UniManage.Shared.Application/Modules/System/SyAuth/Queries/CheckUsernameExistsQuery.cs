using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyAuth.Queries
{
    #region Query

    /// <summary>
    /// Truy v?n ki?m tra s? t?n t?i c?a t�n dang nh?p (Username) trong h? th?ng
    /// </summary>
    public sealed class CheckUsernameExistsQuery : BaseQuery, IRequest<ApiResponse<CheckUsernameExistsQuery.Result>>
    {
        /// <summary>
        /// T�n dang nh?p c?n ki?m tra
        /// </summary>
        public string Username { get; set; } = string.Empty;

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
    /// B? ki?m tra d? li?u cho truy v?n ki?m tra t?n t?i username
    /// </summary>
    public sealed class CheckUsernameExistsQueryValidator : AbstractValidator<CheckUsernameExistsQuery>
    {
        public CheckUsernameExistsQueryValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_username))
                .DependentRules(() =>
                {
                    RuleFor(x => x.Username)
                        .MaximumLength(50)
                        .WithMessage(string.Format(CoreResource.validation_maxLength, CoreResource.lbl_username, 50));
                });
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// B? x? l� truy v?n ki?m tra t?n t?i username
    /// </summary>
    public sealed class CheckUsernameExistsQueryHandler : IRequestHandler<CheckUsernameExistsQuery, ApiResponse<CheckUsernameExistsQuery.Result>>
    {
        public async Task<ApiResponse<CheckUsernameExistsQuery.Result>> Handle(CheckUsernameExistsQuery request, CancellationToken cancellationToken)
        {
            // Kh?i t?o log nghi?p v?
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.Username), request.Username)
                }
            };

            try
            {
                using (var dbContext = new DbContext())
                {
                    // S? d?ng EF Core LINQ d? ki?m tra t?n t?i m?t c�ch nhanh ch�ng
                    var exists = await dbContext.Set<SyUsers>()
                        .AnyAsync(u => u.Username == request.Username, cancellationToken);

                    var result = new CheckUsernameExistsQuery.Result
                    {
                        Exists = exists
                    };

                    var response = ResponseHelper.Success(result, string.Format(CoreResource.common_getSuccess, CoreResource.entity_user));
                    
                    log.Result = result;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = "Check username existence success";

                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                
                return ResponseHelper.Error<CheckUsernameExistsQuery.Result>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}




