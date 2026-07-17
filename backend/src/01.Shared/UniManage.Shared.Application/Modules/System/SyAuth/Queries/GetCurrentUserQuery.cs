using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SyAuth.Queries
{
    #region Query

    /// <summary>
    /// Truy v?n l?y th�ng tin chi ti?t c?a ngu?i d�ng hi?n t?i dang dang nh?p
    /// </summary>
    public sealed class GetCurrentUserQuery : BaseQuery, IRequest<ApiResponse<GetCurrentUserQuery.Result>>
    {
        /// <summary>
        /// K?t qu? tr? v? ch?a th�ng tin co b?n c?a ngu?i d�ng
        /// </summary>
        public class Result
        {
            public long Id { get; set; }
            public string UserCode { get; set; } = string.Empty;
            public string DisplayName { get; set; } = string.Empty;
            public string? Email { get; set; }
            public string? RoleCode { get; set; }
            public string? EmployeeCode { get; set; }
            public DateTime CreatedAt { get; set; }
        }
    }

    #endregion

    #region Validator

    /// <summary>
    /// B? ki?m tra d? li?u cho truy v?n l?y th�ng tin ngu?i d�ng hi?n t?i
    /// </summary>
    public sealed class GetCurrentUserQueryValidator : AbstractValidator<GetCurrentUserQuery>
    {
        public GetCurrentUserQueryValidator()
        {
            RuleFor(x => x.HeaderInfo.Username)
                .NotEmpty().WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_username));
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// B? x? l� truy v?n l?y th�ng tin ngu?i d�ng hi?n t?i
    /// </summary>
    public sealed class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, ApiResponse<GetCurrentUserQuery.Result>>
    {
        public async Task<ApiResponse<GetCurrentUserQuery.Result>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var username = request.HeaderInfo.Username ?? string.Empty;
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.HeaderInfo.Username), username)
                }
            };

            try
            {
                using (var dbContext = new DbContext())
                {
                    // S? d?ng EF Core v?i Entity SyUsers d? l?y th�ng tin
                    var user = await dbContext.Set<SyUsers>()
                        .Where(u => u.Username == username)
                        .Select(u => new GetCurrentUserQuery.Result
                        {
                            Id = u.Id,
                            UserCode = u.Username,
                            EmployeeCode = u.EmployeeCode,
                            DisplayName = u.EmployeeCode ?? u.Username, // Uu ti�n hi?n th? m� nh�n vi�n
                            RoleCode = u.RoleCode,
                            Email = u.Email,
                            CreatedAt = u.CreatedAt
                        })
                        .FirstOrDefaultAsync(cancellationToken);

                    if (user == null)
                    {
                        var errorResponse = ResponseHelper.NotFound<GetCurrentUserQuery.Result>(string.Format(CoreResource.common_notFound, CoreResource.entity_user));
                        log.Message = "Current user not found in database";
                        log.ReturnCode = errorResponse.ReturnCode;
                        return errorResponse;
                    }

                    var response = ResponseHelper.Success(user, string.Format(CoreResource.common_getSuccess, CoreResource.entity_user));
                    
                    log.Result = user;
                    log.ReturnCode = response.ReturnCode;
                    log.Message = "Get current user profile success";

                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                
                return ResponseHelper.Error<GetCurrentUserQuery.Result>(CoreResource.common_error);
            }
            finally
            {
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}




