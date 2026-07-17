using MediatR;
using Microsoft.EntityFrameworkCore;
using UniManage.Shared.Domain;
using UniManage.Shared.Application.Interfaces;

namespace UniManage.Shared.Application.Modules.SyUser.Queries
{
    #region Query

    /// <summary>
    /// Truy v?n l?y danh s�ch ngu?i d�ng r�t g?n cho c�c th�nh ph?n Combobox/Dropdown
    /// </summary>
    public sealed class GetUserComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItem<long>>>>, ICacheable
    {
        /// <summary>
        /// T? kh�a t�m ki?m theo Username
        /// </summary>
        public string? Keyword { get; init; }

        public string CacheKey => CacheHelper.BuildKey(string.Format(CacheKeyConstant.System.ComboboxUsers, Keyword ?? "all"));
        public int? CacheTTLMinutes => CacheTimeConstant.Normal;
    }

    #endregion

    #region Handler

    /// <summary>
    /// B? x? l� truy v?n l?y danh s�ch ngu?i d�ng cho Combobox
    /// </summary>
    public sealed class GetUserComboboxQueryHandler : IRequestHandler<GetUserComboboxQuery, ApiResponse<List<ComboboxItem<long>>>>
    {
        /// <summary>
        /// H�m x? l� l?y d? li?u t? Database
        /// </summary>
        public async Task<ApiResponse<List<ComboboxItem<long>>>> Handle(GetUserComboboxQuery request, CancellationToken cancellationToken)
        {
            // Kh?i t?o log API v?i th�ng tin header v� tham s? keyword
            var log = new ApiLogModel(request.HeaderInfo)
            {
                Parameter = new List<LogParamModel>
                {
                    new(nameof(request.Keyword), request.Keyword)
                }
            };

            try
            {
                using var dbContext = new DbContext();

                // Ch? l?y nh?ng ngu?i d�ng dang ho?t d?ng (Active)
                var query = dbContext.Set<SyUsers>()
                    .Include(u => u.HrEmployees) // Eager load th�ng tin nh�n vi�n d? l?y FullName
                    .Where(u => u.Status == CoreCommon.Value.Commonstatus.Active);

                // L?c theo t? kh�a n?u c�
                if (!string.IsNullOrEmpty(request.Keyword))
                {
                    var kw = request.Keyword.Trim();
                    query = query.Where(u => u.Username.Contains(kw));
                }

                // Chuy?n d?i d? li?u sang d?nh d?ng ComboboxItem chu?n
                var users = await query
                    .OrderBy(u => u.Username)
                    .Take(100) // Gi?i h?n 100 b?n ghi d? d?m b?o hi?u nang UI
                    .Select(u => new ComboboxItem<long>
                    {
                        Code = u.Id,
                        // Hi?n th? d?ng "Username - FullName" n?u c� th�ng tin nh�n vi�n, ngu?c l?i ch? hi?n Username
                        Name = u.HrEmployees != null ? $"{u.Username} - {u.HrEmployees.FullName}" : u.Username,
                        ExtData = u.Username
                    })
                    .ToListAsync(cancellationToken);

                var response = ResponseHelper.Success(users);
                
                log.Result = new { Count = users.Count };
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;

                return response;
            }
            catch (Exception ex)
            {
                // Ghi nh?n ngo?i l? v�o log
                log.IsException = true;
                log.Message = ex.Message;
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                throw;
            }
            finally
            {
                // Ghi log t?p trung
                UniLogManager.WriteApiLog(log);
            }
        }
    }

    #endregion
}







