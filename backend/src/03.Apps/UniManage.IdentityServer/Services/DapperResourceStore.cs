using Dapper;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;

namespace UniManage.IdentityServer.Services
{
    /// <summary>
    /// Store quản lý các tài nguyên (Identity, API Resources, Scopes) trong IdentityServer sử dụng Dapper.
    /// </summary>
    public class DapperResourceStore : IResourceStore
    {
        /// <summary>
        /// Lấy tất cả các tài nguyên có trong hệ thống.
        /// </summary>
        /// <returns>Đối tượng Resources chứa tất cả Identity và API resources.</returns>
        public async Task<Resources> GetAllResourcesAsync()
        {
            try
            {
                var identityResources = await GetIdentityResourcesFromDbAsync();
                var apiResources = await GetApiResourcesFromDbAsync();
                var apiScopes = await GetApiScopesFromDbAsync();

                return new Resources(identityResources, apiResources, apiScopes);
            }
            catch (Exception ex)
            {
                UniLogger.Error("Error fetching all resources from Dapper store", ex);
                return new Resources();
            }
        }

        /// <summary>
        /// Tìm kiếm các tài nguyên API dựa trên tên resource.
        /// </summary>
        /// <param name="apiResourceNames">Danh sách tên các API resource cần tìm.</param>
        /// <returns>Danh sách các ApiResource tìm thấy.</returns>
        public async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            try
            {
                var allApiResources = await GetApiResourcesFromDbAsync();
                return allApiResources.Where(a => apiResourceNames.Contains(a.Name));
            }
            catch (Exception ex)
            {
                UniLogger.Error("Error fetching API resources by name", ex);
                return Enumerable.Empty<ApiResource>();
            }
        }

        /// <summary>
        /// Tìm kiếm các tài nguyên API dựa trên tên scope.
        /// </summary>
        /// <param name="scopeNames">Danh sách tên các scope cần tìm.</param>
        /// <returns>Danh sách các ApiResource tìm thấy.</returns>
        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            try
            {
                var allApiResources = await GetApiResourcesFromDbAsync();
                return allApiResources.Where(a => a.Scopes.Intersect(scopeNames).Any());
            }
            catch (Exception ex)
            {
                UniLogger.Error("Error fetching API resources by scope name", ex);
                return Enumerable.Empty<ApiResource>();
            }
        }

        /// <summary>
        /// Tìm kiếm API Scope dựa trên tên.
        /// </summary>
        /// <param name="scopeNames">Danh sách tên các scope cần tìm.</param>
        /// <returns>Danh sách các ApiScope tìm thấy.</returns>
        public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            try
            {
                var allScopes = await GetApiScopesFromDbAsync();
                return allScopes.Where(s => scopeNames.Contains(s.Name));
            }
            catch (Exception ex)
            {
                UniLogger.Error("Error fetching API scopes by name", ex);
                return Enumerable.Empty<ApiScope>();
            }
        }

        /// <summary>
        /// Tìm kiếm các tài nguyên Identity dựa trên tên scope.
        /// </summary>
        /// <param name="scopeNames">Danh sách tên các scope cần tìm.</param>
        /// <returns>Danh sách các IdentityResource tìm thấy.</returns>
        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            try
            {
                var allIdentityResources = await GetIdentityResourcesFromDbAsync();
                return allIdentityResources.Where(i => scopeNames.Contains(i.Name));
            }
            catch (Exception ex)
            {
                UniLogger.Error("Error fetching identity resources by scope name", ex);
                return Enumerable.Empty<IdentityResource>();
            }
        }

        /// <summary>
        /// Truy vấn danh sách Identity Resource từ database.
        /// </summary>
        private async Task<List<IdentityResource>> GetIdentityResourcesFromDbAsync()
        {
            using var dbContext = new DbContext();
            var sql = "SELECT [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument] FROM [dbo].[is_identity_resources]";
            var dtoList = await dbContext.QueryAsync<IdentityResourceDto>(sql);
            
            return dtoList.Select(dto => new IdentityResource
            {
                Name = dto.Name,
                DisplayName = dto.DisplayName,
                Description = dto.Description,
                Required = dto.Required,
                Emphasize = dto.Emphasize,
                ShowInDiscoveryDocument = dto.ShowInDiscoveryDocument
            }).ToList();
        }

        /// <summary>
        /// Truy vấn danh sách API Resource từ database.
        /// </summary>
        private async Task<List<ApiResource>> GetApiResourcesFromDbAsync()
        {
            using var dbContext = new DbContext();
            var sql = "SELECT [Name], [DisplayName], [Description], [ShowInDiscoveryDocument], [AllowedAccessTokenSigningAlgorithms] FROM [dbo].[is_api_resources]";
            var dtoList = await dbContext.QueryAsync<ApiResourceDto>(sql);
            
            var scopeSql = "SELECT [Name] FROM [dbo].[is_api_scopes]";
            var allScopes = (await dbContext.QueryAsync<string>(scopeSql)).ToList();

            return dtoList.Select(dto => new ApiResource(dto.Name, dto.DisplayName)
            {
                Description = dto.Description,
                ShowInDiscoveryDocument = dto.ShowInDiscoveryDocument,
                AllowedAccessTokenSigningAlgorithms = dto.AllowedAccessTokenSigningAlgorithms?.Split(',').ToList() ?? new List<string>(),
                Scopes = allScopes
            }).ToList();
        }

        /// <summary>
        /// Truy vấn danh sách API Scope từ database.
        /// </summary>
        private async Task<List<ApiScope>> GetApiScopesFromDbAsync()
        {
            using var dbContext = new DbContext();
            var sql = "SELECT [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument] FROM [dbo].[is_api_scopes]";
            var dtoList = await dbContext.QueryAsync<ApiScopeDto>(sql);
            
            return dtoList.Select(dto => new ApiScope(dto.Name, dto.DisplayName)
            {
                Description = dto.Description,
                Required = dto.Required,
                Emphasize = dto.Emphasize,
                ShowInDiscoveryDocument = dto.ShowInDiscoveryDocument
            }).ToList();
        }

        /// <summary>
        /// DTO cho IdentityResource.
        /// </summary>
        private class IdentityResourceDto
        {
            public string Name { get; set; } = default!;
            public string? DisplayName { get; set; }
            public string? Description { get; set; }
            public bool Required { get; set; }
            public bool Emphasize { get; set; }
            public bool ShowInDiscoveryDocument { get; set; }
        }

        /// <summary>
        /// DTO cho ApiResource.
        /// </summary>
        private class ApiResourceDto
        {
            public string Name { get; set; } = default!;
            public string? DisplayName { get; set; }
            public string? Description { get; set; }
            public bool ShowInDiscoveryDocument { get; set; }
            public string? AllowedAccessTokenSigningAlgorithms { get; set; }
        }

        /// <summary>
        /// DTO cho ApiScope.
        /// </summary>
        private class ApiScopeDto
        {
            public string Name { get; set; } = default!;
            public string? DisplayName { get; set; }
            public string? Description { get; set; }
            public bool Required { get; set; }
            public bool Emphasize { get; set; }
            public bool ShowInDiscoveryDocument { get; set; }
        }
    }
}


