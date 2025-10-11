-- ============================================
-- Seed IdentityServer Configuration Data
-- ============================================

-- 1. Identity Resources (OpenID Connect standard scopes)
INSERT INTO [dbo].[is_identity_resources] ([Name], [DisplayName], [UserClaims], [Enabled], [Required])
VALUES 
    ('openid', 'OpenID', '["sub"]', 1, 1),
    ('profile', 'User Profile', '["name","family_name","given_name","middle_name","nickname","preferred_username","profile","picture","website","gender","birthdate","zoneinfo","locale","updated_at"]', 1, 0),
    ('email', 'Email', '["email","email_verified"]', 1, 0);

-- 2. API Scopes
INSERT INTO [dbo].[is_api_scopes] ([Name], [DisplayName], [Description], [Enabled])
VALUES 
    ('unimanage.api', 'UniManage API', 'Full access to UniManage API', 1),
    ('unimanage.api.read', 'UniManage API Read', 'Read-only access to UniManage API', 1),
    ('unimanage.api.write', 'UniManage API Write', 'Write access to UniManage API', 1);

-- 3. API Resources
INSERT INTO [dbo].[is_api_resources] ([Name], [DisplayName], [Scopes], [UserClaims], [Enabled])
VALUES 
    ('unimanage-api', 'UniManage API', '["unimanage.api","unimanage.api.read","unimanage.api.write"]', '["name","email","role"]', 1);

-- 4. Clients
-- Client 1: Web Application (Authorization Code + PKCE)
INSERT INTO [dbo].[is_clients] 
(
    [ClientId], 
    [ClientName], 
    [ClientSecrets],
    [AllowedGrantTypes], 
    [AllowedScopes], 
    [RedirectUris], 
    [PostLogoutRedirectUris],
    [AllowOfflineAccess],
    [AccessTokenLifetime],
    [RequirePkce],
    [Enabled]
)
VALUES 
(
    'unimanage-web',
    'UniManage Web Application',
    '[{"Value":"K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=","Type":"SharedSecret","Description":"Default secret (hashed: secret)"}]',
    '["authorization_code","refresh_token"]',
    '["openid","profile","email","unimanage.api","offline_access"]',
    '["http://localhost:4200/callback","http://localhost:4200/silent-refresh.html"]',
    '["http://localhost:4200"]',
    1, -- AllowOfflineAccess
    3600, -- 1 hour
    1, -- RequirePkce
    1 -- Enabled
);

-- Client 2: Mobile App (Authorization Code + PKCE)
INSERT INTO [dbo].[is_clients] 
(
    [ClientId], 
    [ClientName], 
    [ClientSecrets],
    [AllowedGrantTypes], 
    [AllowedScopes], 
    [RedirectUris], 
    [PostLogoutRedirectUris],
    [AllowOfflineAccess],
    [AccessTokenLifetime],
    [RequirePkce],
    [Enabled]
)
VALUES 
(
    'unimanage-mobile',
    'UniManage Mobile App',
    NULL, -- Public client, no secret
    '["authorization_code","refresh_token"]',
    '["openid","profile","email","unimanage.api","offline_access"]',
    '["unimanage://callback"]',
    '["unimanage://logout"]',
    1,
    3600,
    1,
    1
);

-- Client 3: Postman/Testing (Resource Owner Password - CHỈ CHO DEV/TEST)
INSERT INTO [dbo].[is_clients] 
(
    [ClientId], 
    [ClientName], 
    [ClientSecrets],
    [AllowedGrantTypes], 
    [AllowedScopes], 
    [AllowOfflineAccess],
    [AccessTokenLifetime],
    [RequirePkce],
    [Enabled]
)
VALUES 
(
    'postman-test',
    'Postman Testing Client',
    '[{"Value":"K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=","Type":"SharedSecret"}]',
    '["password","refresh_token"]', -- Resource Owner Password (ROPC)
    '["openid","profile","email","unimanage.api","offline_access"]',
    1,
    3600,
    0, -- ROPC không dùng PKCE
    1
);

-- Client 4: Machine-to-Machine (Client Credentials)
INSERT INTO [dbo].[is_clients] 
(
    [ClientId], 
    [ClientName], 
    [ClientSecrets],
    [AllowedGrantTypes], 
    [AllowedScopes], 
    [AllowOfflineAccess],
    [AccessTokenLifetime],
    [RequirePkce],
    [Enabled]
)
VALUES 
(
    'background-service',
    'Background Service',
    '[{"Value":"K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=","Type":"SharedSecret"}]',
    '["client_credentials"]',
    '["unimanage.api"]',
    0, -- No refresh token for client credentials
    3600,
    0,
    1
);

GO

PRINT 'IdentityServer seed data inserted successfully';
PRINT '';
PRINT 'Default Clients:';
PRINT '1. unimanage-web: Authorization Code + PKCE (Web app)';
PRINT '2. unimanage-mobile: Authorization Code + PKCE (Mobile)';
PRINT '3. postman-test: Resource Owner Password (Testing only - secret: secret)';
PRINT '4. background-service: Client Credentials (M2M)';
