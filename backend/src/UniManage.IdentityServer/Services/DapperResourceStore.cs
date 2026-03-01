using Dapper;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using UniManage.Core.Database;
using UniManage.Core.Logging;

namespace UniManage.IdentityServer.Services
{
    public class DapperResourceStore : IResourceStore
    {
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

        private async Task<List<IdentityResource>> GetIdentityResourcesFromDbAsync()
        {
            using var dbContext = new DbContext();
            var sql = "SELECT [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument] FROM [dbo].[sy_is_identity_resources]";
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

        private async Task<List<ApiResource>> GetApiResourcesFromDbAsync()
        {
            using var dbContext = new DbContext();
            var sql = "SELECT [Name], [DisplayName], [Description], [ShowInDiscoveryDocument], [AllowedAccessTokenSigningAlgorithms] FROM [dbo].[sy_is_api_resources]";
            var dtoList = await dbContext.QueryAsync<ApiResourceDto>(sql);
            
            // Lấy scopes (giả định map 1-1 cho đơn giản giống Config.cs hiện tại)
            var scopeSql = "SELECT [Name] FROM [dbo].[sy_is_api_scopes]";
            var allScopes = (await dbContext.QueryAsync<string>(scopeSql)).ToList();

            return dtoList.Select(dto => new ApiResource(dto.Name, dto.DisplayName)
            {
                Description = dto.Description,
                ShowInDiscoveryDocument = dto.ShowInDiscoveryDocument,
                AllowedAccessTokenSigningAlgorithms = dto.AllowedAccessTokenSigningAlgorithms?.Split(',').ToList() ?? new List<string>(),
                Scopes = allScopes // Add all scopes locally for this API demo
            }).ToList();
        }

        private async Task<List<ApiScope>> GetApiScopesFromDbAsync()
        {
            using var dbContext = new DbContext();
            var sql = "SELECT [Name], [DisplayName], [Description], [Required], [Emphasize], [ShowInDiscoveryDocument] FROM [dbo].[sy_is_api_scopes]";
            var dtoList = await dbContext.QueryAsync<ApiScopeDto>(sql);
            
            return dtoList.Select(dto => new ApiScope(dto.Name, dto.DisplayName)
            {
                Description = dto.Description,
                Required = dto.Required,
                Emphasize = dto.Emphasize,
                ShowInDiscoveryDocument = dto.ShowInDiscoveryDocument
            }).ToList();
        }

        private class IdentityResourceDto
        {
            public string Name { get; set; } = default!;
            public string? DisplayName { get; set; }
            public string? Description { get; set; }
            public bool Required { get; set; }
            public bool Emphasize { get; set; }
            public bool ShowInDiscoveryDocument { get; set; }
        }

        private class ApiResourceDto
        {
            public string Name { get; set; } = default!;
            public string? DisplayName { get; set; }
            public string? Description { get; set; }
            public bool ShowInDiscoveryDocument { get; set; }
            public string? AllowedAccessTokenSigningAlgorithms { get; set; }
        }

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
