using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Model.Entities;
using UniManage.Resource;
using DbContext = UniManage.Core.Database.DbContext;

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
        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? EmployeeCode { get; set; }
        public string? RoleCode { get; set; }
        public string Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public byte[] RowVersion { get; set; } = default!;
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
        // Initialize log data
        var logData = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Id), request.Id.ToString())
            }
        };

        using (var dbContext = new DbContext())
        {
            try
            {
                // Use EF Core LINQ to find user
                var user = await dbContext.Set<sy_users>()
                    .Where(u => u.Id == request.Id)
                    .Select(u => new GetUserByIdQuery.Response
                    {
                        Username = u.Username,
                        Email = u.Email,
                        EmployeeCode = u.EmployeeCode,
                        RoleCode = u.RoleCode,
                        Status = u.Status,
                        CreatedAt = u.CreatedAt,
                        UpdatedAt = u.UpdatedAt,
                        RowVersion = u.RowVersion
                    })
                    .FirstOrDefaultAsync(ct);

                if (user == null)
                {
                    var notFoundResponse = ResponseHelper.NotFound<GetUserByIdQuery.Response>("User not found");
                    logData.ReturnCode = notFoundResponse.ReturnCode;
                    UniLogManager.WriteApiLog(logData);
                    return notFoundResponse;
                }

                var response = ResponseHelper.Success(user, string.Format(CoreResource.crud_getSuccess, CoreResource.entity_user));
                logData.Result = new { user.Username };
                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);

                return response;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving user by id {request.Id}: {ex.Message}", ex);
                
                var response = ResponseHelper.Error<GetUserByIdQuery.Response>(CoreResource.common_exceptionOccurred);
                logData.Message = ex.Message;
                logData.IsException = 1;
                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);

                return response;
            }
        }
    }
}

#endregion