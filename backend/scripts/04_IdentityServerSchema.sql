-- ============================================
-- IdentityServer Tables
-- Lưu trữ Clients, Resources, Grants cho Duende IdentityServer
-- ============================================

-- 1. Clients (ứng dụng client được phép truy cập)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'is_clients')
BEGIN
    CREATE TABLE [dbo].[is_clients] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [ClientId] NVARCHAR(200) NOT NULL UNIQUE,
        [ClientName] NVARCHAR(200) NULL,
        [ClientSecrets] NVARCHAR(MAX) NULL, -- JSON array of secrets
        [AllowedGrantTypes] NVARCHAR(MAX) NOT NULL, -- JSON array: ["authorization_code", "client_credentials"]
        [AllowedScopes] NVARCHAR(MAX) NULL, -- JSON array: ["api1", "openid", "profile"]
        [RedirectUris] NVARCHAR(MAX) NULL, -- JSON array
        [PostLogoutRedirectUris] NVARCHAR(MAX) NULL, -- JSON array
        [AllowOfflineAccess] BIT NOT NULL DEFAULT 0,
        [AccessTokenLifetime] INT NOT NULL DEFAULT 3600,
        [RefreshTokenUsage] INT NOT NULL DEFAULT 1, -- 0=ReUse, 1=OneTimeOnly
        [RefreshTokenExpiration] INT NOT NULL DEFAULT 1, -- 0=Sliding, 1=Absolute
        [SlidingRefreshTokenLifetime] INT NOT NULL DEFAULT 2592000, -- 30 days
        [AbsoluteRefreshTokenLifetime] INT NOT NULL DEFAULT 2592000,
        [Enabled] BIT NOT NULL DEFAULT 1,
        [RequirePkce] BIT NOT NULL DEFAULT 1,
        [AllowPlainTextPkce] BIT NOT NULL DEFAULT 0,
        [CreatedAt] DATETIME2(3) NOT NULL DEFAULT SYSUTCDATETIME(),
        [UpdatedAt] DATETIME2(3) NULL,
        [DataRowVersion] ROWVERSION
    );

    CREATE INDEX IX_is_clients_ClientId ON [dbo].[is_clients]([ClientId]);
END

-- 2. Identity Resources (OpenID Connect resources)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'is_identity_resources')
BEGIN
    CREATE TABLE [dbo].[is_identity_resources] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [Name] NVARCHAR(200) NOT NULL UNIQUE,
        [DisplayName] NVARCHAR(200) NULL,
        [Description] NVARCHAR(1000) NULL,
        [UserClaims] NVARCHAR(MAX) NULL, -- JSON array: ["sub", "name", "email"]
        [Enabled] BIT NOT NULL DEFAULT 1,
        [Required] BIT NOT NULL DEFAULT 0,
        [Emphasize] BIT NOT NULL DEFAULT 0,
        [ShowInDiscoveryDocument] BIT NOT NULL DEFAULT 1,
        [CreatedAt] DATETIME2(3) NOT NULL DEFAULT SYSUTCDATETIME(),
        [DataRowVersion] ROWVERSION
    );
END

-- 3. API Scopes
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'is_api_scopes')
BEGIN
    CREATE TABLE [dbo].[is_api_scopes] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [Name] NVARCHAR(200) NOT NULL UNIQUE,
        [DisplayName] NVARCHAR(200) NULL,
        [Description] NVARCHAR(1000) NULL,
        [UserClaims] NVARCHAR(MAX) NULL, -- JSON array
        [Enabled] BIT NOT NULL DEFAULT 1,
        [Required] BIT NOT NULL DEFAULT 0,
        [Emphasize] BIT NOT NULL DEFAULT 0,
        [ShowInDiscoveryDocument] BIT NOT NULL DEFAULT 1,
        [CreatedAt] DATETIME2(3) NOT NULL DEFAULT SYSUTCDATETIME(),
        [DataRowVersion] ROWVERSION
    );
END

-- 4. API Resources
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'is_api_resources')
BEGIN
    CREATE TABLE [dbo].[is_api_resources] (
        [Id] INT IDENTITY(1,1) PRIMARY KEY,
        [Name] NVARCHAR(200) NOT NULL UNIQUE,
        [DisplayName] NVARCHAR(200) NULL,
        [Description] NVARCHAR(1000) NULL,
        [Scopes] NVARCHAR(MAX) NULL, -- JSON array: ["api1", "api2"]
        [UserClaims] NVARCHAR(MAX) NULL, -- JSON array
        [Enabled] BIT NOT NULL DEFAULT 1,
        [CreatedAt] DATETIME2(3) NOT NULL DEFAULT SYSUTCDATETIME(),
        [DataRowVersion] ROWVERSION
    );
END

-- 5. Persisted Grants (tokens, refresh tokens, authorization codes)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'is_persisted_grants')
BEGIN
    CREATE TABLE [dbo].[is_persisted_grants] (
        [Key] NVARCHAR(200) PRIMARY KEY,
        [Type] NVARCHAR(50) NOT NULL,
        [SubjectId] NVARCHAR(200) NULL,
        [SessionId] NVARCHAR(100) NULL,
        [ClientId] NVARCHAR(200) NOT NULL,
        [Description] NVARCHAR(200) NULL,
        [CreationTime] DATETIME2(3) NOT NULL,
        [Expiration] DATETIME2(3) NULL,
        [ConsumedTime] DATETIME2(3) NULL,
        [Data] NVARCHAR(MAX) NOT NULL
    );

    CREATE INDEX IX_is_persisted_grants_SubjectId ON [dbo].[is_persisted_grants]([SubjectId]);
    CREATE INDEX IX_is_persisted_grants_ClientId ON [dbo].[is_persisted_grants]([ClientId]);
    CREATE INDEX IX_is_persisted_grants_Expiration ON [dbo].[is_persisted_grants]([Expiration]);
    CREATE INDEX IX_is_persisted_grants_Type ON [dbo].[is_persisted_grants]([Type]);
END

-- 6. Device Flow Codes (cho device authentication)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'is_device_codes')
BEGIN
    CREATE TABLE [dbo].[is_device_codes] (
        [UserCode] NVARCHAR(200) PRIMARY KEY,
        [DeviceCode] NVARCHAR(200) NOT NULL UNIQUE,
        [SubjectId] NVARCHAR(200) NULL,
        [SessionId] NVARCHAR(100) NULL,
        [ClientId] NVARCHAR(200) NOT NULL,
        [Description] NVARCHAR(200) NULL,
        [CreationTime] DATETIME2(3) NOT NULL,
        [Expiration] DATETIME2(3) NOT NULL,
        [Data] NVARCHAR(MAX) NOT NULL
    );

    CREATE INDEX IX_is_device_codes_DeviceCode ON [dbo].[is_device_codes]([DeviceCode]);
    CREATE INDEX IX_is_device_codes_Expiration ON [dbo].[is_device_codes]([Expiration]);
END

-- 7. Server Side Sessions (optional, for session management)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'is_server_side_sessions')
BEGIN
    CREATE TABLE [dbo].[is_server_side_sessions] (
        [Key] NVARCHAR(200) PRIMARY KEY,
        [Scheme] NVARCHAR(100) NOT NULL,
        [SubjectId] NVARCHAR(200) NOT NULL,
        [SessionId] NVARCHAR(100) NULL,
        [DisplayName] NVARCHAR(200) NULL,
        [Created] DATETIME2(3) NOT NULL,
        [Renewed] DATETIME2(3) NOT NULL,
        [Expires] DATETIME2(3) NULL,
        [Data] NVARCHAR(MAX) NOT NULL
    );

    CREATE INDEX IX_is_server_side_sessions_SubjectId ON [dbo].[is_server_side_sessions]([SubjectId]);
    CREATE INDEX IX_is_server_side_sessions_SessionId ON [dbo].[is_server_side_sessions]([SessionId]);
    CREATE INDEX IX_is_server_side_sessions_Expires ON [dbo].[is_server_side_sessions]([Expires]);
END

GO

PRINT 'IdentityServer schema created successfully';
