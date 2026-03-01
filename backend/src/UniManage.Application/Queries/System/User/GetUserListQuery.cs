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

public sealed class GetUserListQuery : BaseListQuery, IRequest<ApiResponse<PagedResult<GetUserListQuery.Response>>>
{
    public string? Status { get; set; }

    public sealed record Response
    {
        public long Id { get; set; }
        public string Username { get; set; } = default!;
        public string EmployeeCode { get; set; } = default!;
        public string RoleCode { get; set; } = default!;
        public string Status { get; set; } = default!;
        public DateTime CreatedAt { get; set; }
    }
}

#endregion

#region Validator

public sealed class GetUserListQueryValidator : AbstractValidator<GetUserListQuery>
{
    public GetUserListQueryValidator()
    {
        RuleFor(x => x.PageIndex)
            .GreaterThan(0).WithMessage("Page index must be greater than 0");

        RuleFor(x => x.PageSize)
            .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100");
    }
}

#endregion

#region Handler

public sealed class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, ApiResponse<PagedResult<GetUserListQuery.Response>>>
{
    public async Task<ApiResponse<PagedResult<GetUserListQuery.Response>>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
    {
        // Initialize log data
        var logData = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel(nameof(request.Keyword), request.Keyword),
                new CoreParamModel(nameof(request.Status), request.Status),
                new CoreParamModel(nameof(request.PageIndex), request.PageIndex),
                new CoreParamModel(nameof(request.PageSize), request.PageSize)
            }
        };

        using (var dbContext = new DbContext())
        {
            try
            {
                // Build query with EF Core LINQ
                var query = dbContext.Set<sy_users>().AsQueryable();

                // Apply filters
                if (!string.IsNullOrEmpty(request.Status))
                {
                    query = query.Where(u => u.Status == request.Status);
                }

                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    var keyword = request.Keyword.Trim().ToLower();
                    query = query.Where(u => 
                        u.Username.ToLower().Contains(keyword) ||
                        u.Email.ToLower().Contains(keyword) ||
                        (u.EmployeeCode != null && u.EmployeeCode.ToLower().Contains(keyword)));
                }

                // Get total count before pagination
                var totalItems = await query.CountAsync(cancellationToken);

                // Apply sorting (default: CreatedAt DESC)
                query = query.OrderByDescending(u => u.CreatedAt);

                // Apply pagination
                var items = await query
                    .Skip(request.Offset)
                    .Take(request.PageSize)
                    .Select(u => new GetUserListQuery.Response
                    {
                        Id = u.Id,
                        Username = u.Username,
                        EmployeeCode = u.EmployeeCode ?? string.Empty,
                        RoleCode = u.RoleCode ?? string.Empty,
                        Status = u.Status,
                        CreatedAt = u.CreatedAt
                    })
                    .ToListAsync(cancellationToken);

                // Build paged result
                var result = new PagedResult<GetUserListQuery.Response>
                {
                    Items = items,
                    Paging = new PagingInfo
                    {
                        PageIndex = request.PageIndex,
                        PageSize = request.PageSize,
                        TotalItems = totalItems
                    }
                };

                var response = ResponseHelper.Success(result, string.Format(CoreResource.crud_listSuccess, CoreResource.entity_user));

                logData.Result = result;
                logData.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(logData);

                return response;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving users list: {ex.Message}", ex);
                
                var response = ResponseHelper.Error<PagedResult<GetUserListQuery.Response>>(CoreResource.common_exceptionOccurred);
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