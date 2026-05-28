using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Constant;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;
using FluentValidation;

namespace UniManage.Application.Queries.System.Auth
{
    #region Query

    public sealed class GetProfileQuery : BaseQuery, IRequest<ApiResponse<GetProfileQuery.Response>>
    {
        public sealed record Response
        {
            public Guid Uuid { get; set; }
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
        public async Task<ApiResponse<GetProfileQuery.Response>> Handle(GetProfileQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo);
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
                    var profile = await dbContext.Set<sy_users>()
                        .Where(u => u.Username == username)
                        .Select(u => new GetProfileQuery.Response
                        {
                            Uuid = u.Uuid,
                            Username = u.Username ?? string.Empty,
                            Email = u.Email,
                            Avatar = u.Avatar,
                            Preferences = u.Preferences,
                            EmployeeCode = u.EmployeeCode,
                            FullName = u.hr_employees != null ? u.hr_employees.FullName : null,
                            PhoneNumber = u.hr_employees != null ? u.hr_employees.PhoneNumber : null,
                            Address = u.hr_employees != null ? u.hr_employees.Address : null
                        })
                        .FirstOrDefaultAsync(ct);

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
                    log.ReturnCode = UniManage.Core.Constant.CoreApiReturnCode.ExceptionOccurred;
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
