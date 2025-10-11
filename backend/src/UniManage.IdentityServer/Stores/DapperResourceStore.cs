using Dapper;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Stores;
using Newtonsoft.Json;
using UniManage.Core.Database;

namespace UniManage.IdentityServer.Stores
{
    /// <summary>
    /// Dapper implementation of IResourceStore using DbContext from Core
    /// </summary>
    public class DapperResourceStore : IResourceStore
    {
        public async Task<Resources> GetAllResourcesAsync()
        {
            using var dbContext = new DbContext();

            var identityResources = await GetIdentityResourcesAsync(dbContext);
            var apiScopes = await GetApiScopesAsync(dbContext);
            var apiResources = await GetApiResourcesAsync(dbContext);

            return new Resources(identityResources, apiResources, apiScopes);
        }

        public async Task<IEnumerable<IdentityResource>> FindIdentityResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            using var dbContext = new DbContext();

            var sql = @"
                SELECT [Name], [DisplayName], [Description], [UserClaims], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument]
                FROM [dbo].[is_identity_resources]
                WHERE [Name] IN @ScopeNames
                    AND [Enabled] = 1";

            var dtos = await dbContext.connection.QueryAsync<IdentityResourceDto>(sql, new { ScopeNames = scopeNames.ToList() });

            return dtos.Select(MapToIdentityResource).ToList();
        }

        public async Task<IEnumerable<ApiScope>> FindApiScopesByNameAsync(IEnumerable<string> scopeNames)
        {
            using var dbContext = new DbContext();

            var sql = @"
                SELECT [Name], [DisplayName], [Description], [UserClaims], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument]
                FROM [dbo].[is_api_scopes]
                WHERE [Name] IN @ScopeNames
                    AND [Enabled] = 1";

            var dtos = await dbContext.connection.QueryAsync<ApiScopeDto>(sql, new { ScopeNames = scopeNames.ToList() });

            return dtos.Select(MapToApiScope).ToList();
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByScopeNameAsync(IEnumerable<string> scopeNames)
        {
            using var dbContext = new DbContext();

            var sql = @"
                SELECT [Name], [DisplayName], [Description], [Scopes], [UserClaims], [Enabled]
                FROM [dbo].[is_api_resources]
                WHERE [Enabled] = 1";

            var dtos = await dbContext.connection.QueryAsync<ApiResourceDto>(sql);

            // Filter by scopes
            var result = new List<ApiResource>();
            foreach (var dto in dtos)
            {
                var resource = MapToApiResource(dto);
                if (resource.Scopes.Any(s => scopeNames.Contains(s)))
                {
                    result.Add(resource);
                }
            }

            return result;
        }

        public async Task<IEnumerable<ApiResource>> FindApiResourcesByNameAsync(IEnumerable<string> apiResourceNames)
        {
            using var dbContext = new DbContext();

            var sql = @"
                SELECT [Name], [DisplayName], [Description], [Scopes], [UserClaims], [Enabled]
                FROM [dbo].[is_api_resources]
                WHERE [Name] IN @ApiResourceNames
                    AND [Enabled] = 1";

            var dtos = await dbContext.connection.QueryAsync<ApiResourceDto>(sql, new { ApiResourceNames = apiResourceNames.ToList() });

            return dtos.Select(MapToApiResource).ToList();
        }

        private async Task<IEnumerable<IdentityResource>> GetIdentityResourcesAsync(DbContext dbContext)
        {
            var sql = @"
                SELECT [Name], [DisplayName], [Description], [UserClaims], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument]
                FROM [dbo].[is_identity_resources]
                WHERE [Enabled] = 1";

            var dtos = await dbContext.connection.QueryAsync<IdentityResourceDto>(sql);
            return dtos.Select(MapToIdentityResource).ToList();
        }

        private async Task<IEnumerable<ApiScope>> GetApiScopesAsync(DbContext dbContext)
        {
            var sql = @"
                SELECT [Name], [DisplayName], [Description], [UserClaims], [Enabled], [Required], [Emphasize], [ShowInDiscoveryDocument]
                FROM [dbo].[is_api_scopes]
                WHERE [Enabled] = 1";

            var dtos = await dbContext.connection.QueryAsync<ApiScopeDto>(sql);
            return dtos.Select(MapToApiScope).ToList();
        }

        private async Task<IEnumerable<ApiResource>> GetApiResourcesAsync(DbContext dbContext)
        {
            var sql = @"
                SELECT [Name], [DisplayName], [Description], [Scopes], [UserClaims], [Enabled]
                FROM [dbo].[is_api_resources]
                WHERE [Enabled] = 1";

            var dtos = await dbContext.connection.QueryAsync<ApiResourceDto>(sql);
            return dtos.Select(MapToApiResource).ToList();
        }

        private IdentityResource MapToIdentityResource(IdentityResourceDto dto)
        {
            var resource = new IdentityResource
            {
                Name = dto.Name,
                DisplayName = dto.DisplayName,
                Description = dto.Description,
                Enabled = dto.Enabled,
                Required = dto.Required,
                Emphasize = dto.Emphasize,
                ShowInDiscoveryDocument = dto.ShowInDiscoveryDocument
            };

            if (!string.IsNullOrEmpty(dto.UserClaims))
            {
                resource.UserClaims = JsonConvert.DeserializeObject<List<string>>(dto.UserClaims) ?? new List<string>();
            }

            return resource;
        }

        private ApiScope MapToApiScope(ApiScopeDto dto)
        {
            var scope = new ApiScope
            {
                Name = dto.Name,
                DisplayName = dto.DisplayName,
                Description = dto.Description,
                Enabled = dto.Enabled,
                Required = dto.Required,
                Emphasize = dto.Emphasize,
                ShowInDiscoveryDocument = dto.ShowInDiscoveryDocument
            };

            if (!string.IsNullOrEmpty(dto.UserClaims))
            {
                scope.UserClaims = JsonConvert.DeserializeObject<List<string>>(dto.UserClaims) ?? new List<string>();
            }

            return scope;
        }

        private ApiResource MapToApiResource(ApiResourceDto dto)
        {
            var resource = new ApiResource
            {
                Name = dto.Name,
                DisplayName = dto.DisplayName,
                Description = dto.Description,
                Enabled = dto.Enabled
            };

            if (!string.IsNullOrEmpty(dto.Scopes))
            {
                resource.Scopes = JsonConvert.DeserializeObject<List<string>>(dto.Scopes) ?? new List<string>();
            }

            if (!string.IsNullOrEmpty(dto.UserClaims))
            {
                resource.UserClaims = JsonConvert.DeserializeObject<List<string>>(dto.UserClaims) ?? new List<string>();
            }

            return resource;
        }

        // DTOs
        private class IdentityResourceDto
        {
            public string Name { get; set; } = default!;
            public string? DisplayName { get; set; }
            public string? Description { get; set; }
            public string? UserClaims { get; set; }
            public bool Enabled { get; set; }
            public bool Required { get; set; }
            public bool Emphasize { get; set; }
            public bool ShowInDiscoveryDocument { get; set; }
        }

        private class ApiScopeDto
        {
            public string Name { get; set; } = default!;
            public string? DisplayName { get; set; }
            public string? Description { get; set; }
            public string? UserClaims { get; set; }
            public bool Enabled { get; set; }
            public bool Required { get; set; }
            public bool Emphasize { get; set; }
            public bool ShowInDiscoveryDocument { get; set; }
        }

        private class ApiResourceDto
        {
            public string Name { get; set; } = default!;
            public string? DisplayName { get; set; }
            public string? Description { get; set; }
            public string? Scopes { get; set; }
            public string? UserClaims { get; set; }
            public bool Enabled { get; set; }
        }
    }
}
