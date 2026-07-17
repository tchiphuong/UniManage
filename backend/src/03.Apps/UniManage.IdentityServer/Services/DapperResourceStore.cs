using UniManage.Shared.Infrastructure.Database;
using Dapper;
using UniManage.IdentityServer.Interfaces;
using UniManage.IdentityServer.Models;
using UniManage.Shared.Domain.Interfaces;
using UniManage.Shared.Infrastructure.Logging;

namespace UniManage.IdentityServer.Services
{
    /// <summary>
    /// Store quản lý các tài nguyên (Identity, API Resources, Scopes) sử dụng Dapper.
    /// </summary>
    public class DapperResourceStore : IResourceStore
    {
        /// <summary>
        /// Tìm kiếm các tài nguyên API dựa trên tên resource.
        /// </summary>
        /// <param name="apiResourceNames">Danh sách tên các API resource cần tìm.</param>
        /// <returns>Danh sách các ApiResourceModel tìm thấy.</returns>
        public async Task<IEnumerable<ApiResourceModel>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            try
            {
                var allApiResources = await GetApiResourcesFromDbAsync();
                return allApiResources.Where(a => apiResourceNames.Contains(a.Name));
            }
            catch (Exception ex)
            {
                UniLogger.Error("Error fetching API resources by name", ex);
                return Enumerable.Empty<ApiResourceModel>();
            }
        }

        /// <summary>
        /// Tìm kiếm các tài nguyên API dựa trên tên scope.
        /// </summary>
        /// <param name="scopeNames">Danh sách tên các scope cần tìm.</param>
        /// <returns>Danh sách các ApiResourceModel tìm thấy.</returns>
        public async Task<IEnumerable<ApiResourceModel>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            try
            {
                var allApiResources = await GetApiResourcesFromDbAsync();
                return allApiResources.Where(a => a.Scopes.Intersect(scopeNames).Any());
            }
            catch (Exception ex)
            {
                UniLogger.Error("Error fetching API resources by scope name", ex);
                return Enumerable.Empty<ApiResourceModel>();
            }
        }

        /// <summary>
        /// Tìm kiếm API Scope dựa trên tên.
        /// </summary>
        /// <param name="scopeNames">Danh sách tên các scope cần tìm.</param>
        /// <returns>Danh sách các ApiScopeModel tìm thấy.</returns>
        public async Task<IEnumerable<ApiScopeModel>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            try
            {
                var allScopes = await GetApiScopesFromDbAsync();
                return allScopes.Where(s => scopeNames.Contains(s.Name));
            }
            catch (Exception ex)
            {
                UniLogger.Error("Error fetching API scopes by name", ex);
                return Enumerable.Empty<ApiScopeModel>();
            }
        }

        /// <summary>
        /// Tìm kiếm các tài nguyên Identity dựa trên tên scope.
        /// </summary>
        /// <param name="scopeNames">Danh sách tên các scope cần tìm.</param>
        /// <returns>Danh sách các IdentityResourceModel tìm thấy.</returns>
        public async Task<IEnumerable<IdentityResourceModel>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            try
            {
                var allIdentityResources = await GetIdentityResourcesFromDbAsync();
                return allIdentityResources.Where(i => scopeNames.Contains(i.Name));
            }
            catch (Exception ex)
            {
                UniLogger.Error("Error fetching identity resources by scope name", ex);
                return Enumerable.Empty<IdentityResourceModel>();
            }
        }

        /// <summary>
        /// Lấy danh sách Identity Resources từ cơ sở dữ liệu.
        /// </summary>
        /// <returns>Danh sách IdentityResourceModel.</returns>
        private async Task<List<IdentityResourceModel>> GetIdentityResourcesFromDbAsync()
        {
            using var dbContext = new DbContext();
            var sql = @"
                SELECT 
                    [Name], 
                    [DisplayName] 
                FROM [dbo].[is_identity_resources]";
            var dtoList = await dbContext.QueryAsync<IdentityResourceDbModel>(sql);
            
            return dtoList.Select(dto => new IdentityResourceModel
            {
                Name = dto.Name,
                DisplayName = dto.DisplayName
            }).ToList();
        }

        /// <summary>
        /// Lấy danh sách API Resources từ cơ sở dữ liệu.
        /// </summary>
        /// <returns>Danh sách ApiResourceModel.</returns>
        private async Task<List<ApiResourceModel>> GetApiResourcesFromDbAsync()
        {
            using var dbContext = new DbContext();
            var sql = @"
                SELECT 
                    [Name], 
                    [DisplayName] 
                FROM [dbo].[is_api_resources]";
            var dtoList = await dbContext.QueryAsync<ApiResourceDbModel>(sql);
            
            var scopeSql = "SELECT [Name] FROM [dbo].[is_api_scopes]";
            var allScopes = (await dbContext.QueryAsync<string>(scopeSql)).ToList();

            return dtoList.Select(dto => new ApiResourceModel
            {
                Name = dto.Name,
                DisplayName = dto.DisplayName,
                Scopes = allScopes
            }).ToList();
        }

        /// <summary>
        /// Lấy danh sách API Scopes từ cơ sở dữ liệu.
        /// </summary>
        /// <returns>Danh sách ApiScopeModel.</returns>
        private async Task<List<ApiScopeModel>> GetApiScopesFromDbAsync()
        {
            using var dbContext = new DbContext();
            var sql = @"
                SELECT 
                    [Name], 
                    [DisplayName] 
                FROM [dbo].[is_api_scopes]";
            var dtoList = await dbContext.QueryAsync<ApiScopeDbModel>(sql);
            
            return dtoList.Select(dto => new ApiScopeModel
            {
                Name = dto.Name,
                DisplayName = dto.DisplayName
            }).ToList();
        }

        private class IdentityResourceDbModel
        {
            public string Name { get; set; } = default!;
            public string? DisplayName { get; set; }
        }

        private class ApiResourceDbModel
        {
            public string Name { get; set; } = default!;
            public string? DisplayName { get; set; }
        }

        private class ApiScopeDbModel
        {
            public string Name { get; set; } = default!;
            public string? DisplayName { get; set; }
        }
    }
}


