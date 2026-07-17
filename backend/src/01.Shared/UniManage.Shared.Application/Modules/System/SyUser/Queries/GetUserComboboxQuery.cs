using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Domain.Models;
using UniManage.Shared.Application.Interfaces;

namespace UniManage.Shared.Application.Modules.SyUser.Queries
{
    #region Query

    /// <summary>
    /// Query to get a shortened user list for Combobox/Dropdown components
    /// </summary>
    public sealed class GetUserComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItem<long>>>>, ICacheable, ILoggableCommand
    {
        /// <summary>
        /// Search keyword by Username
        /// </summary>
        public string? Keyword { get; init; }

        public string CacheKey => CacheHelper.BuildKey(string.Format(CacheKeyConstant.System.ComboboxUsers, Keyword ?? "all"));
        public int? CacheTTLMinutes => CacheTimeConstant.Normal;
    }

    #endregion

    #region Handler

    /// <summary>
    /// Query handler to get user list for Combobox
    /// </summary>
    public sealed class GetUserComboboxQueryHandler : IRequestHandler<GetUserComboboxQuery, ApiResponse<List<ComboboxItem<long>>>>
    {
        /// <summary>
        /// Handle getting data from Database
        /// </summary>
        public async Task<ApiResponse<List<ComboboxItem<long>>>> Handle(GetUserComboboxQuery request, CancellationToken cancellationToken)
        {
            using (var dbContext = new DbContext())
            {
                try
                {
                    // Only get active users
                    var query = dbContext.Set<SyUsers>()
                        .Include(u => u.HrEmployees) // Eager load employee information to get FullName
                        .Where(u => u.Status == CoreCommon.Value.Commonstatus.Active);

                    // Filter by keyword if any
                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        var kw = request.Keyword.Trim();
                        query = query.Where(u => u.Username.Contains(kw));
                    }

                    // Convert data to standard ComboboxItem format
                    var users = await query
                        .OrderBy(u => u.Username)
                        .Take(100) // Limit to 100 records to ensure UI performance
                        .Select(u => new ComboboxItem<long>
                        {
                            Code = u.Id,
                            // Display as "Username - FullName" if employee info exists, otherwise just Username
                            Name = u.HrEmployees != null ? $"{u.Username} - {u.HrEmployees.FullName}" : u.Username,
                            ExtData = u.Username
                        })
                        .ToListAsync(cancellationToken);

                    var response = ResponseHelper.Success(users);

                    return response;
                }
                catch (Exception)
                {
                    throw;
                }
            }
        }
    }

    #endregion
}
