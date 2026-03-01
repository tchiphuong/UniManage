-- IdentityServer Dapper Stores Schema
-- Prefix: sy_is_ (System Identity Server)

-- 1. Identity Resources
CREATE TABLE [dbo].[sy_is_identity_resources] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(200) NOT NULL,
    [DisplayName] NVARCHAR(200) NULL,
    [Description] NVARCHAR(1000) NULL,
    [Required] BIT NOT NULL DEFAULT 0,
    [Emphasize] BIT NOT NULL DEFAULT 0,
    [ShowInDiscoveryDocument] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [PK_sy_is_identity_resources] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_sy_is_identity_resources_Name] UNIQUE ([Name])
);

-- 2. API Resources
CREATE TABLE [dbo].[sy_is_api_resources] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(200) NOT NULL,
    [DisplayName] NVARCHAR(200) NULL,
    [Description] NVARCHAR(1000) NULL,
    [ShowInDiscoveryDocument] BIT NOT NULL DEFAULT 1,
    [AllowedAccessTokenSigningAlgorithms] NVARCHAR(100) NULL,
    CONSTRAINT [PK_sy_is_api_resources] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_sy_is_api_resources_Name] UNIQUE ([Name])
);

-- 3. API Scopes
CREATE TABLE [dbo].[sy_is_api_scopes] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [Name] NVARCHAR(200) NOT NULL,
    [DisplayName] NVARCHAR(200) NULL,
    [Description] NVARCHAR(1000) NULL,
    [Required] BIT NOT NULL DEFAULT 0,
    [Emphasize] BIT NOT NULL DEFAULT 0,
    [ShowInDiscoveryDocument] BIT NOT NULL DEFAULT 1,
    CONSTRAINT [PK_sy_is_api_scopes] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_sy_is_api_scopes_Name] UNIQUE ([Name])
);

-- 4. Clients
CREATE TABLE [dbo].[sy_is_clients] (
    [Id] INT IDENTITY(1,1) NOT NULL,
    [ClientId] NVARCHAR(200) NOT NULL,
    [ClientName] NVARCHAR(200) NULL,
    [ClientSecret] NVARCHAR(2000) NULL, -- Khác với chuẩn bình thường, UniManage lưu 1 mảng/list qua JSON hoặc 1 hash ở đây. Để đơn giản ta dùng 1 secret hash.
    [AllowedGrantTypes] NVARCHAR(2000) NULL, -- Dấu phẩy phân cách, ví dụ: "password,client_credentials"
    [AllowedScopes] NVARCHAR(2000) NULL, -- Dấu phẩy phân cách, ví dụ: "openid,profile,unimanage.api"
    [AccessTokenLifetime] INT NOT NULL DEFAULT 3600,
    [AllowOfflineAccess] BIT NOT NULL DEFAULT 0,
    [IdentityTokenLifetime] INT NOT NULL DEFAULT 300,
    [AuthorizationCodeLifetime] INT NOT NULL DEFAULT 300,
    [AbsoluteRefreshTokenLifetime] INT NOT NULL DEFAULT 2592000,
    [SlidingRefreshTokenLifetime] INT NOT NULL DEFAULT 1296000,
    [RefreshTokenUsage] INT NOT NULL DEFAULT 1, -- 1 = OneTimeOnly, 0 = ReUse
    [RefreshTokenExpiration] INT NOT NULL DEFAULT 1, -- 1 = Absolute, 0 = Sliding
    [RequireClientSecret] BIT NOT NULL DEFAULT 1,
    [Description] NVARCHAR(1000) NULL,
    CONSTRAINT [PK_sy_is_clients] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UQ_sy_is_clients_ClientId] UNIQUE ([ClientId])
);

-- 5. Persisted Grants (Cho Refresh Token, Authorization Code, Reference Token)
CREATE TABLE [dbo].[sy_is_persisted_grants] (
    [Key] NVARCHAR(200) NOT NULL,
    [Type] NVARCHAR(50) NOT NULL,
    [SubjectId] NVARCHAR(200) NULL,
    [SessionId] NVARCHAR(100) NULL,
    [ClientId] NVARCHAR(200) NOT NULL,
    [Description] NVARCHAR(200) NULL,
    [CreationTime] DATETIME2 NOT NULL,
    [Expiration] DATETIME2 NULL,
    [ConsumedTime] DATETIME2 NULL,
    [Data] NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [PK_sy_is_persisted_grants] PRIMARY KEY CLUSTERED ([Key] ASC)
);

CREATE INDEX [IX_sy_is_persisted_grants_SubjectId_ClientId_Type] ON [dbo].[sy_is_persisted_grants] ([SubjectId], [ClientId], [Type]);
CREATE INDEX [IX_sy_is_persisted_grants_Expiration] ON [dbo].[sy_is_persisted_grants] ([Expiration]);

-- 6. Device Flows
CREATE TABLE [dbo].[sy_is_device_flows] (
    [UserCode] NVARCHAR(200) NOT NULL,
    [DeviceCode] NVARCHAR(200) NOT NULL,
    [SubjectId] NVARCHAR(200) NULL,
    [SessionId] NVARCHAR(100) NULL,
    [ClientId] NVARCHAR(200) NOT NULL,
    [Description] NVARCHAR(200) NULL,
    [CreationTime] DATETIME2 NOT NULL,
    [Expiration] DATETIME2 NOT NULL,
    [Data] NVARCHAR(MAX) NOT NULL,
    CONSTRAINT [PK_sy_is_device_flows] PRIMARY KEY CLUSTERED ([UserCode] ASC),
    CONSTRAINT [UQ_sy_is_device_flows_DeviceCode] UNIQUE ([DeviceCode])
);

CREATE INDEX [IX_sy_is_device_flows_DeviceCode] ON [dbo].[sy_is_device_flows] ([DeviceCode]);
CREATE INDEX [IX_sy_is_device_flows_Expiration] ON [dbo].[sy_is_device_flows] ([Expiration]);


-- B. THÊM DATA MẶC ĐỊNH TỪ Config.cs

INSERT INTO [dbo].[sy_is_identity_resources] ([Name], [DisplayName], [Required])
VALUES
('openid', 'Your user identifier', 1),
('profile', 'User profile', 0);

INSERT INTO [dbo].[sy_is_api_scopes] ([Name], [DisplayName])
VALUES
('unimanage.api', 'UniManage API');

INSERT INTO [dbo].[sy_is_api_resources] ([Name], [DisplayName])
VALUES
('UniManage.Api', 'UniManage API');

-- Lưu ý: ClientSecret ở đây được lưu dạng hash Sha256 giống trong Config.cs ("secret".Sha256())
-- K2jcGQ1H+Zp8hO+B1Yj7e2kK1h0o= (hoặc bạn có thể tạo mã băm base64 chuẩn của chữ "secret")
-- "secret".Sha256() produces "K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols="
INSERT INTO [dbo].[sy_is_clients] (
    [ClientId], 
    [ClientName], 
    [ClientSecret], 
    [AllowedGrantTypes], 
    [AllowedScopes], 
    [AccessTokenLifetime], 
    [AllowOfflineAccess]
)
VALUES
(
    'unimanage-client', 
    'UniManage Client', 
    'K7gNU3sdo+OL0wNhqoVWhr3g6s1xYv72ol/pe/Unols=', 
    'password', 
    'openid,profile,unimanage.api', 
    3600, 
    1
);
