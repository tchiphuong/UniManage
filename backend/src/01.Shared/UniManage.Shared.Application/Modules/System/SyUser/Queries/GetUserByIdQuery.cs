using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Domain.Models;
using UniManage.Shared.Infrastructure.Constant;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;
using UniManage.Shared.Resource;


namespace UniManage.Shared.Application.Modules.SyUser.Queries
{
    #region Query

    /// <summary>
    /// Query to get user by id
    /// </summary>
    public sealed class GetUserByIdQuery : BaseQuery, IRequest<ApiResponse<GetUserByIdQuery.Response>>, ILoggableCommand
    {
        public Guid Uuid { get; init; }

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



    #region Handler

    /// <summary>
    /// Handler for GetUserByIdQuery
    /// </summary>
    public sealed class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<GetUserByIdQuery.Response>>
    {
        public async Task<ApiResponse<GetUserByIdQuery.Response>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            using (var dbContext = new DbContext())
            {
                try
                {
                    // Use EF Core LINQ to find user by Uuid
                    var user = await dbContext.Set<SyUsers>()
                        .Where(u => u.Uuid == request.Uuid)
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
                        .FirstOrDefaultAsync(cancellationToken);

                    if (user == null)
                    {
                        var notFoundResponse = ResponseHelper.NotFound<GetUserByIdQuery.Response>("User not found");
                        return notFoundResponse;
                    }

                    var response = ResponseHelper.Success(user, string.Format(CoreResource.common_getSuccess, CoreResource.entity_user));
                    return response;
                }
                catch (Exception)
                {
                    return ResponseHelper.Error<GetUserByIdQuery.Response>(CoreResource.common_error);
                }
            }
        }
    }

    #endregion
}
