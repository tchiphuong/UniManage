namespace UniManage.Shared.Application.Modules.SyUser.Queries
{
    #region Query

    /// <summary>
    /// Query to get paginated user list with filters
    /// </summary>
    public sealed class GetUserListQuery : BaseListQuery, IRequest<PagedResponse<GetUserListQuery.Response>>, ILoggableCommand
    {
        /// <summary>
        /// User status for filtering (Active/Inactive)
        /// </summary>
        public string? Status { get; set; }

        /// <summary>
        /// Response DTO for each user row
        /// </summary>
        public sealed record Response
        {
            public long Id { get; set; }
            public Guid Uuid { get; set; }
            public string Username { get; set; } = default!;
            public string EmployeeCode { get; set; } = default!;
            public string RoleCode { get; set; } = default!;
            public string Status { get; set; } = default!;
            public DateTime CreatedAt { get; set; }
        }
    }



    #region Handler

    /// <summary>
    /// Handler for getting user list
    /// </summary>
    public sealed class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, PagedResponse<GetUserListQuery.Response>>
    {
        /// <summary>
        /// Handle getting paginated data from Database
        /// </summary>
        public async Task<PagedResponse<GetUserListQuery.Response>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            using (var dbContext = new DbContext())
            {
                try
                {
                    // Initialize query from SyUsers table
                    var query = dbContext.Set<SyUsers>().AsQueryable();

                    // STEP 1: Apply business filters
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

                    // STEP 2: Get total record count before pagination for UI calculation
                    var totalItems = await query.CountAsync(cancellationToken);

                    // STEP 3: Apply sorting and pagination directly from Query parameters
                    // Use request.Offset and request.PageSize which are pre-calculated by BaseListQuery
                    var items = await query
                        .OrderByDescending(u => u.CreatedAt)
                        .Skip(request.Offset)
                        .Take(request.PageSize)
                        .Select(u => new GetUserListQuery.Response
                        {
                            Id = u.Id,
                            Uuid = u.Uuid,
                            Username = u.Username,
                            EmployeeCode = u.EmployeeCode ?? string.Empty,
                            RoleCode = u.RoleCode ?? string.Empty,
                            Status = u.Status,
                            CreatedAt = u.CreatedAt
                        })
                        .ToListAsync(cancellationToken);

                    // STEP 4: Package the return result according to PagedResult standard
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

                    var response = ResponseHelper.PagedSuccess(result, string.Format(CoreResource.common_listSuccess, CoreResource.entity_user));

                    return response;
                }
                catch (Exception)
                {
                    return ResponseHelper.PagedError<GetUserListQuery.Response>(CoreResource.common_error);
                }
            }
        }
    }

    #endregion
}








