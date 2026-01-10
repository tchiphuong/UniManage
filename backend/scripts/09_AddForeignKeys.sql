-- ================================================
-- Script: 09_AddForeignKeys.sql
-- Description: Add foreign key constraints for sy_* tables
-- Date: 2026-01-07
-- ================================================

USE [UniManage]
GO

PRINT '================================================'
PRINT 'Adding foreign key constraints...'
PRINT '================================================'
GO

-- =============================================
-- 1. sy_users.RoleCode -> sy_roles.Code
-- =============================================
IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys 
    WHERE name = 'FK_sy_users_roles'
)
BEGIN
    ALTER TABLE [dbo].[sy_users]
    ADD CONSTRAINT [FK_sy_users_roles]
    FOREIGN KEY ([RoleCode]) REFERENCES [dbo].[sy_roles]([Code])
    
    PRINT '✓ Added FK: sy_users.RoleCode -> sy_roles.Code'
END
ELSE
    PRINT '- sy_users.RoleCode FK already exists'
GO

-- =============================================
-- 2. sy_role_permissions.RoleCode -> sy_roles.Code
-- =============================================
IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys 
    WHERE name = 'FK_sy_role_permissions_roles'
)
BEGIN
    ALTER TABLE [dbo].[sy_role_permissions]
    ADD CONSTRAINT [FK_sy_role_permissions_roles]
    FOREIGN KEY ([RoleCode]) REFERENCES [dbo].[sy_roles]([Code])
    
    PRINT '✓ Added FK: sy_role_permissions.RoleCode -> sy_roles.Code'
END
ELSE
    PRINT '- sy_role_permissions.RoleCode FK already exists'
GO

-- =============================================
-- 3. sy_role_permissions.FunctionCode -> sy_functions.Code
-- =============================================
IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys 
    WHERE name = 'FK_sy_role_permissions_functions'
)
BEGIN
    ALTER TABLE [dbo].[sy_role_permissions]
    ADD CONSTRAINT [FK_sy_role_permissions_functions]
    FOREIGN KEY ([FunctionCode]) REFERENCES [dbo].[sy_functions]([Code])
    
    PRINT '✓ Added FK: sy_role_permissions.FunctionCode -> sy_functions.Code'
END
ELSE
    PRINT '- sy_role_permissions.FunctionCode FK already exists'
GO

-- =============================================
-- 4. sy_role_permissions.ActionCode -> sy_actions.Code
-- =============================================
IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys 
    WHERE name = 'FK_sy_role_permissions_actions'
)
BEGIN
    ALTER TABLE [dbo].[sy_role_permissions]
    ADD CONSTRAINT [FK_sy_role_permissions_actions]
    FOREIGN KEY ([ActionCode]) REFERENCES [dbo].[sy_actions]([Code])
    
    PRINT '✓ Added FK: sy_role_permissions.ActionCode -> sy_actions.Code'
END
ELSE
    PRINT '- sy_role_permissions.ActionCode FK already exists'
GO

-- =============================================
-- 5. sy_refresh_tokens.UserName -> sy_users.Username
-- =============================================
IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys 
    WHERE name = 'FK_sy_refresh_tokens_users'
)
BEGIN
    ALTER TABLE [dbo].[sy_refresh_tokens]
    ADD CONSTRAINT [FK_sy_refresh_tokens_users]
    FOREIGN KEY ([UserName]) REFERENCES [dbo].[sy_users]([Username])
    ON DELETE NO ACTION
    
    PRINT '✓ Added FK: sy_refresh_tokens.UserName -> sy_users.Username'
END
ELSE
    PRINT '- sy_refresh_tokens.UserName FK already exists'
GO

-- =============================================
-- 6. sy_function_mapping.FunctionCode -> sy_functions.Code
-- =============================================
IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys 
    WHERE name = 'FK_sy_function_mapping_functions'
)
BEGIN
    ALTER TABLE [dbo].[sy_function_mapping]
    ADD CONSTRAINT [FK_sy_function_mapping_functions]
    FOREIGN KEY ([FunctionCode]) REFERENCES [dbo].[sy_functions]([Code])
    
    PRINT '✓ Added FK: sy_function_mapping.FunctionCode -> sy_functions.Code'
END
ELSE
    PRINT '- sy_function_mapping.FunctionCode FK already exists'
GO

-- =============================================
-- 7. sy_function_mapping.ActionCode -> sy_actions.Code
-- =============================================
IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys 
    WHERE name = 'FK_sy_function_mapping_actions'
)
BEGIN
    ALTER TABLE [dbo].[sy_function_mapping]
    ADD CONSTRAINT [FK_sy_function_mapping_actions]
    FOREIGN KEY ([ActionCode]) REFERENCES [dbo].[sy_actions]([Code])
    
    PRINT '✓ Added FK: sy_function_mapping.ActionCode -> sy_actions.Code'
END
ELSE
    PRINT '- sy_function_mapping.ActionCode FK already exists'
GO

-- =============================================
-- 8. sy_menus.ParentCode -> sy_menus.Code (Self-referencing)
-- =============================================
IF NOT EXISTS (
    SELECT 1 FROM sys.foreign_keys 
    WHERE name = 'FK_sy_menus_parent'
)
BEGIN
    ALTER TABLE [dbo].[sy_menus]
    ADD CONSTRAINT [FK_sy_menus_parent]
    FOREIGN KEY ([ParentCode]) REFERENCES [dbo].[sy_menus]([Code])
    
    PRINT '✓ Added FK: sy_menus.ParentCode -> sy_menus.Code (self-reference)'
END
ELSE
    PRINT '- sy_menus.ParentCode FK already exists'
GO

PRINT '================================================'
PRINT 'Foreign key constraints completed!'
PRINT '================================================'
GO
