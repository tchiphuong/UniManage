-- ================================================
-- Script: Insert sy_menus seed data
-- Cách dùng: Chạy từng section trong SSMS
-- ================================================

USE [UniManage]
GO

-- =============================================
-- 0. KIỂM TRA CẤU TRÚC HIỆN TẠI
-- =============================================
PRINT '=== sy_functions hiện có ==='
SELECT Code, ResourceKey, GroupCode FROM sy_functions WHERE IsActive = 1 ORDER BY GroupCode, Code
GO

PRINT '=== sy_role_permissions hiện có ==='
SELECT DISTINCT RoleCode, FunctionCode FROM sy_role_permissions ORDER BY RoleCode, FunctionCode
GO

PRINT '=== sy_user_roles hiện có ==='
SELECT * FROM sy_user_roles
GO

-- =============================================
-- 1. INSERT ROOT GROUPS (ParentCode = NULL)
-- Đây là section headers trong sidebar
-- =============================================
SET IDENTITY_INSERT [dbo].[sy_menus] ON

-- Xóa data cũ nếu có
DELETE FROM [dbo].[sy_menus] WHERE [ParentCode] IS NOT NULL
DELETE FROM [dbo].[sy_menus] WHERE [ParentCode] IS NULL

INSERT INTO [dbo].[sy_menus] ([Id], [Code], [FunctionCode], [ParentCode], [ResourceKey], [Url], [Icon], [Sort], [CreatedBy], [CreatedAt])
VALUES
    (1,  'GRP_MAIN',      NULL, NULL, 'menu.group.main',      NULL, NULL, 10, 'SYSTEM', SYSUTCDATETIME()),
    (2,  'GRP_HR',        NULL, NULL, 'menu.group.hr',        NULL, NULL, 20, 'SYSTEM', SYSUTCDATETIME()),
    (3,  'GRP_SALES',     NULL, NULL, 'menu.group.sales',     NULL, NULL, 30, 'SYSTEM', SYSUTCDATETIME()),
    (4,  'GRP_INVENTORY', NULL, NULL, 'menu.group.inventory', NULL, NULL, 40, 'SYSTEM', SYSUTCDATETIME()),
    (5,  'GRP_SYSTEM',    NULL, NULL, 'menu.group.system',    NULL, NULL, 90, 'SYSTEM', SYSUTCDATETIME())
GO

-- =============================================
-- 2. INSERT MENU ITEMS (với FunctionCode từ sy_functions)
-- ⚠️ Nếu FunctionCode không tồn tại trong sy_functions,
--    menu item đó sẽ KHÔNG hiển thị (do INNER JOIN trong API query)
--    Hãy update FunctionCode cho đúng sau khi chạy Section 0
-- =============================================

-- MAIN group
INSERT INTO [dbo].[sy_menus] ([Id], [Code], [FunctionCode], [ParentCode], [ResourceKey], [Url], [Icon], [Sort], [CreatedBy], [CreatedAt])
VALUES
    (101, 'MNU_DASHBOARD', 'DASHBOARD', 'GRP_MAIN', 'menu.dashboard', '/dashboard', 'fa-tachometer-alt', 10, 'SYSTEM', SYSUTCDATETIME())
GO

-- HR group  
INSERT INTO [dbo].[sy_menus] ([Id], [Code], [FunctionCode], [ParentCode], [ResourceKey], [Url], [Icon], [Sort], [CreatedBy], [CreatedAt])
VALUES
    (201, 'MNU_EMPLOYEES',   'HR_EMPLOYEE',   'GRP_HR', 'menu.employees',   '/hr/employees',   'fa-users',      10, 'SYSTEM', SYSUTCDATETIME()),
    (202, 'MNU_DEPARTMENTS', 'HR_DEPARTMENT', 'GRP_HR', 'menu.departments', '/hr/departments', 'fa-user-group', 20, 'SYSTEM', SYSUTCDATETIME()),
    (203, 'MNU_POSITIONS',   'HR_POSITION',   'GRP_HR', 'menu.positions',   '/hr/positions',   'fa-briefcase',  30, 'SYSTEM', SYSUTCDATETIME()),
    (204, 'MNU_ATTENDANCE',  'HR_ATTENDANCE', 'GRP_HR', 'menu.attendance',  '/hr/attendance',  'fa-clock',      40, 'SYSTEM', SYSUTCDATETIME()),
    (205, 'MNU_SALARIES',    'HR_SALARY',     'GRP_HR', 'menu.salaries',    '/hr/salaries',    'fa-banknotes',  50, 'SYSTEM', SYSUTCDATETIME())
GO

-- SALES group
INSERT INTO [dbo].[sy_menus] ([Id], [Code], [FunctionCode], [ParentCode], [ResourceKey], [Url], [Icon], [Sort], [CreatedBy], [CreatedAt])
VALUES
    (301, 'MNU_CUSTOMERS', 'SA_CUSTOMER', 'GRP_SALES', 'menu.customers', '/sales/customers', 'fa-heart',        10, 'SYSTEM', SYSUTCDATETIME()),
    (302, 'MNU_ORDERS',    'SA_ORDER',    'GRP_SALES', 'menu.orders',    '/sales/orders',    'fa-shopping-cart', 20, 'SYSTEM', SYSUTCDATETIME())
GO

-- INVENTORY group
INSERT INTO [dbo].[sy_menus] ([Id], [Code], [FunctionCode], [ParentCode], [ResourceKey], [Url], [Icon], [Sort], [CreatedBy], [CreatedAt])
VALUES
    (401, 'MNU_ITEMS', 'IT_ITEM', 'GRP_INVENTORY', 'menu.items', '/inventory/items', 'fa-box', 10, 'SYSTEM', SYSUTCDATETIME())
GO

-- SYSTEM group
INSERT INTO [dbo].[sy_menus] ([Id], [Code], [FunctionCode], [ParentCode], [ResourceKey], [Url], [Icon], [Sort], [CreatedBy], [CreatedAt])
VALUES
    (901, 'MNU_USERS',    'SY_USER',   'GRP_SYSTEM', 'menu.users',    '/system/users',    'fa-user-circle',  10, 'SYSTEM', SYSUTCDATETIME()),
    (902, 'MNU_ROLES',    'SY_ROLE',   'GRP_SYSTEM', 'menu.roles',    '/system/roles',    'fa-check-square', 20, 'SYSTEM', SYSUTCDATETIME()),
    (903, 'MNU_SETTINGS', 'SY_CONFIG', 'GRP_SYSTEM', 'menu.settings', '/system/settings', 'fa-cogs',         30, 'SYSTEM', SYSUTCDATETIME())
GO

SET IDENTITY_INSERT [dbo].[sy_menus] OFF
GO

-- =============================================
-- 3. KIỂM TRA KẾT QUẢ
-- =============================================
PRINT ''
PRINT '=== KẾT QUẢ INSERT sy_menus ==='

SELECT 
    m.Id, m.Code, m.FunctionCode, m.ParentCode, m.ResourceKey, m.Url, m.Icon, m.Sort,
    CASE WHEN m.ParentCode IS NULL THEN N'📁 GROUP' ELSE N'  📄 ITEM' END AS [Type],
    CASE 
        WHEN m.FunctionCode IS NULL THEN '-'
        WHEN EXISTS(SELECT 1 FROM sy_functions f WHERE f.Code = m.FunctionCode) THEN N'✓ OK'
        ELSE N'❌ NOT FOUND in sy_functions!' 
    END AS [FunctionCodeStatus]
FROM sy_menus m
ORDER BY 
    ISNULL(m.ParentCode, m.Code),
    m.Sort, m.Id

PRINT ''
PRINT N'⚠️ Nếu có ❌ NOT FOUND → chạy Section 0 để xem sy_functions.Code hiện có'
PRINT N'   Rồi UPDATE sy_menus SET FunctionCode = <đúng> WHERE Code = <menu>'
GO
