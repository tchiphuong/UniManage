using FluentValidation;
using MediatR;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.User;

#region Query

/// <summary>
/// Query to get user by id
/// </summary>
public sealed class GetUserByIdQuery : BaseQuery, IRequest<ApiResponse<GetUserByIdQuery.Response>>
{
    public long Id { get; init; }

    public sealed record Response
    {
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmployeeCode { get; set; }
        public byte Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

#endregion

#region Validator

/// <summary>
/// Validator for GetUserByIdQuery
/// </summary>
public sealed class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
{
    public GetUserByIdQueryValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0");
    }
}

#endregion

#region Handler

/// <summary>
/// Handler for GetUserByIdQuery
/// </summary>
public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<GetUserByIdQuery.Response>>
{
    public async Task<ApiResponse<GetUserByIdQuery.Response>> Handle(GetUserByIdQuery request, CancellationToken ct)
    {
        ApiResponse<GetUserByIdQuery.Response> response;

        // Initialize log data
        CoreLogModel logData = new CoreLogModel(request.HeaderInfo);
        logData.Parameter = new List<CoreParamModel>
        {
            new CoreParamModel(nameof(request.Id), request.Id.ToString())
        };

        using (DbContext dbContext = new DbContext())
        {
            try
            {
                var query = $@"
                    SELECT 
                        Username        AS {nameof(GetUserByIdQuery.Response.Username)},
                        DisplayName     AS {nameof(GetUserByIdQuery.Response.DisplayName)},
                        Email           AS {nameof(GetUserByIdQuery.Response.Email)},
                        PhoneNumber     AS {nameof(GetUserByIdQuery.Response.PhoneNumber)},
                        EmployeeCode    AS {nameof(GetUserByIdQuery.Response.EmployeeCode)},
                        Status          AS {nameof(GetUserByIdQuery.Response.Status)},
                        CreatedAt       AS {nameof(GetUserByIdQuery.Response.CreatedAt)},
                        UpdatedAt       AS {nameof(GetUserByIdQuery.Response.UpdatedAt)}
                    FROM sy_users
                    WHERE Id = @Id";

                var user = await dbContext.QueryFirstOrDefaultAsync<GetUserByIdQuery.Response>(
                    query,
                    new { request.Id },
                    ct);

                if (user == null)
                {
                    response = ResponseHelper.NotFound<GetUserByIdQuery.Response>("User not found");
                    logData.ReturnCode = response.ReturnCode;
                }
                else
                {
                    response = ResponseHelper.Success(user, CoreResource.User_msg_GetSuccess);
                    logData.Result = new { user.Username };
                    logData.ReturnCode = response.ReturnCode;
                }
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving user by id {request.Id}: {ex.Message}", ex);
                response = ResponseHelper.Error<GetUserByIdQuery.Response>(CoreResource.Common_msg_ExceptionOccurred);

                logData.Message = ex.ToString();
                logData.IsException = 1;
                logData.ReturnCode = response.ReturnCode;
            }
        }

        UniLogManager.WriteApiLog(logData);

        return response;
    }
}

#endregion