using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Modules.System.Domain;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Modules.System.Application.Queries.Auth
{
    #region Query

    public sealed class GetProfileQuery : BaseQuery, IRequest<ApiResponse<GetProfileQuery.Response>>
    {
        public sealed record Response
        {
            public long Id { get; set; }
            public string Username { get; set; } = string.Empty;
            public string? Email { get; set; }
            public string? Avatar { get; set; }
            public string? Preferences { get; set; }
            public string? FullName { get; set; }
            public string? PhoneNumber { get; set; }
            public string? Address { get; set; }
            public string? EmployeeCode { get; set; }
        }
    }

    #endregion

    #region Validator

    public sealed class GetProfileQueryValidator : AbstractValidator<GetProfileQuery>
    {
        public GetProfileQueryValidator()
        {
        }
    }

    #endregion

    #region Handler

    public sealed class GetProfileQueryHandler : IRequestHandler<GetProfileQuery, ApiResponse<GetProfileQuery.Response>>
    {
        public async Task<ApiResponse<GetProfileQuery.Response>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
        {
            var log = new ApiLogModel(request.HeaderInfo);
            var username = request.HeaderInfo?.Username;

            if (string.IsNullOrEmpty(username))
            {
                var unauthorized = ResponseHelper.Error<GetProfileQuery.Response>(CoreResource.common_unauthorized);
                log.ReturnCode = unauthorized.ReturnCode;
                UniLogManager.WriteApiLog(log);
                return unauthorized;
            }

            using (var dbContext = new DbContext())
            {
                try
                {
                    var profile = await dbContext.Set<SyUsers>()
                        .Where(u => u.Username == username)
                        .Select(u => new GetProfileQuery.Response
                        {
                            Id = u.Id,
                            Username = u.Username ?? string.Empty,
                            Email = u.Email,
                            Avatar = u.Avatar,
                            Preferences = u.Preferences,
                            EmployeeCode = u.EmployeeCode,
                            FullName = u.HrEmployees != null ? u.HrEmployees.FullName : null,
                            PhoneNumber = u.HrEmployees != null ? u.HrEmployees.PhoneNumber : null,
                            Address = u.HrEmployees != null ? u.HrEmployees.Address : null
                        })
                        .FirstOrDefaultAsync(cancellationToken);

                    if (profile == null)
                    {
                        var notFound = ResponseHelper.NotFound<GetProfileQuery.Response>(CoreResource.entity_user);
                        log.ReturnCode = notFound.ReturnCode;
                        UniLogManager.WriteApiLog(log);
                        return notFound;
                    }

                    var response = ResponseHelper.Success(profile, "Success");
                    log.ReturnCode = response.ReturnCode;
                    return response;
                }
                catch (Exception ex)
                {
                    log.IsException = true;
                    log.Message = ex.Message;
                    log.ReturnCode = UniManage.Shared.Infrastructure.Constant.CoreApiReturnCode.ExceptionOccurred;
                    return ResponseHelper.Error<GetProfileQuery.Response>(CoreResource.common_error);
                }
                finally
                {
                    UniLogManager.WriteApiLog(log);
                }
            }
        }
    }

    #endregion
}











