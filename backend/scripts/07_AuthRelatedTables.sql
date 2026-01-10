-- ================================================
-- Script: 07_AuthRelatedTables.sql
-- Description: Create authentication and RBAC tables
-- Date: 2026-01-07
-- ================================================

USE [UniManage]
GO

-- =============================================
-- 1. SKIP sy_roles (already exists with Id + Code)
-- =============================================
PRINT 'Skip sy_roles - table already exists'
GO

-- =============================================
-- 2. SKIP sy_permissions (đã có sy_role_permissions với FunctionCode + ActionCode)
-- =============================================
PRINT 'Skip sy_permissions - using existing sy_role_permissions table'
GO

-- =============================================
-- 3. User Roles Table (Username + RoleCode)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sy_user_roles]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[sy_user_roles](
        [Username] [VARCHAR](50) NOT NULL,
        [RoleCode] [VARCHAR](50) NOT NULL,
        [CreatedAt] [DATETIME2](3) NOT NULL DEFAULT SYSUTCDATETIME(),
        [CreatedBy] [NVARCHAR](50) NULL,
        CONSTRAINT [PK_sy_user_roles] PRIMARY KEY CLUSTERED ([Username] ASC, [RoleCode] ASC),
        CONSTRAINT [FK_sy_user_roles_users] FOREIGN KEY ([Username])
            REFERENCES [dbo].[sy_users] ([Username])
            ON DELETE CASCADE,
        CONSTRAINT [FK_sy_user_roles_roles] FOREIGN KEY ([RoleCode])
            REFERENCES [dbo].[sy_roles] ([Code])
            ON DELETE CASCADE
    )
    
    PRINT 'Table sy_user_roles created successfully'
END
ELSE
BEGIN
    PRINT 'Table sy_user_roles already exists'
END
GO

-- =============================================
-- 4. SKIP sy_role_permissions (already exists with RoleCode + FunctionCode + ActionCode)
-- =============================================
PRINT 'Skip sy_role_permissions - table already exists'
GO

-- =============================================
-- 5. Password Reset Tokens Table (Username FK)
-- =============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[sy_password_reset_tokens]') AND type in (N'U'))
BEGIN
    CREATE TABLE [dbo].[sy_password_reset_tokens](
        [Id] [BIGINT] IDENTITY(1,1) NOT NULL,
        [Username] [VARCHAR](50) NOT NULL,
        [Token] [NVARCHAR](100) NOT NULL,
        [ExpiresAt] [DATETIME2](3) NOT NULL,
        [UsedAt] [DATETIME2](3) NULL,
        [CreatedAt] [DATETIME2](3) NOT NULL DEFAULT SYSUTCDATETIME(),
        CONSTRAINT [PK_sy_password_reset_tokens] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_sy_password_reset_tokens_users] FOREIGN KEY ([Username])
            REFERENCES [dbo].[sy_users] ([Username])
            ON DELETE CASCADE
    )
    
    -- Index for token lookup
    CREATE UNIQUE NONCLUSTERED INDEX [IX_sy_password_reset_tokens_Token] 
        ON [dbo].[sy_password_reset_tokens]([Token] ASC)
    
    -- Index for user lookup
    CREATE NONCLUSTERED INDEX [IX_sy_password_reset_tokens_Username] 
        ON [dbo].[sy_password_reset_tokens]([Username] ASC)
    
    PRINT 'Table sy_password_reset_tokens created successfully'
END
ELSE
BEGIN
    PRINT 'Table sy_password_reset_tokens already exists'
END
GO

-- =============================================
-- 6. Seed Sample Data (SKIP - tables already populated)
-- =============================================
PRINT 'Skip seed data - sy_roles and sy_role_permissions already exist'
GO

PRINT '================================================'
PRINT 'Authentication tables setup completed!'
PRINT 'Created: sy_user_roles, sy_password_reset_tokens'
PRINT '================================================'
GO
