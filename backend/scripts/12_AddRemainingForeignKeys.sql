-- ================================================
-- Script: 12_AddRemainingForeignKeys.sql
-- Description: Add remaining foreign keys after full database review
-- Date: 2026-01-07
-- ================================================

USE [UniManage]
GO

PRINT '================================================'
PRINT 'Adding remaining foreign keys...'
PRINT '================================================'
GO

-- =============================================
-- Administrative Tables
-- =============================================

-- ad_provinces.AdministrativeRegionId -> ad_administrative_regions.Id
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_ad_provinces_region')
BEGIN
    ALTER TABLE [dbo].[ad_provinces]
    ADD CONSTRAINT [FK_ad_provinces_region]
    FOREIGN KEY ([AdministrativeRegionId]) REFERENCES [dbo].[ad_administrative_regions]([Id])
    PRINT '✓ ad_provinces.AdministrativeRegionId -> ad_administrative_regions.Id'
END
ELSE PRINT '- ad_provinces.AdministrativeRegionId FK exists'
GO

-- ad_provinces.AdministrativeUnitId -> ad_administrative_units.Id
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_ad_provinces_unit')
BEGIN
    ALTER TABLE [dbo].[ad_provinces]
    ADD CONSTRAINT [FK_ad_provinces_unit]
    FOREIGN KEY ([AdministrativeUnitId]) REFERENCES [dbo].[ad_administrative_units]([Id])
    PRINT '✓ ad_provinces.AdministrativeUnitId -> ad_administrative_units.Id'
END
ELSE PRINT '- ad_provinces.AdministrativeUnitId FK exists'
GO

-- ad_wards.AdministrativeUnitId -> ad_administrative_units.Id
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_ad_wards_unit')
BEGIN
    ALTER TABLE [dbo].[ad_wards]
    ADD CONSTRAINT [FK_ad_wards_unit]
    FOREIGN KEY ([AdministrativeUnitId]) REFERENCES [dbo].[ad_administrative_units]([Id])
    PRINT '✓ ad_wards.AdministrativeUnitId -> ad_administrative_units.Id'
END
ELSE PRINT '- ad_wards.AdministrativeUnitId FK exists'
GO

-- =============================================
-- System Tables
-- =============================================

-- sy_users.EmployeeCode -> hr_employees.EmployeeCode
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_sy_users_employees')
BEGIN
    ALTER TABLE [dbo].[sy_users]
    ADD CONSTRAINT [FK_sy_users_employees]
    FOREIGN KEY ([EmployeeCode]) REFERENCES [dbo].[hr_employees]([EmployeeCode])
    PRINT '✓ sy_users.EmployeeCode -> hr_employees.EmployeeCode'
END
ELSE PRINT '- sy_users.EmployeeCode FK exists'
GO

-- sy_functions.GroupCode -> sy_functions.Code (self-reference for grouping)
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_sy_functions_parent')
BEGIN
    ALTER TABLE [dbo].[sy_functions]
    ADD CONSTRAINT [FK_sy_functions_parent]
    FOREIGN KEY ([GroupCode]) REFERENCES [dbo].[sy_functions]([Code])
    PRINT '✓ sy_functions.GroupCode -> sy_functions.Code (self-reference)'
END
ELSE PRINT '- sy_functions.GroupCode FK exists'
GO

-- =============================================
-- IdentityServer Tables (Skip - external identifiers)
-- =============================================
PRINT 'Skip IdentityServer tables - ClientId, SessionId, SubjectId are external identifiers'
GO

-- =============================================
-- Note: Following FKs skipped due to schema design
-- =============================================
-- sy_users.Username, hr_employees.EmployeeCode: These are unique identifiers, not FKs
-- hr_positions.PositionCode: Unique identifier, not FK
-- sa_customers.CustomerCode, sa_orders.OrderCode, sa_payments.PaymentCode: Unique identifiers
-- sy_languages.LanguageCode: Unique identifier (ISO codes like 'vi', 'en')
-- wf_*_approver.ApproverEmployeeCode: NVARCHAR vs VARCHAR data type mismatch with hr_employees.EmployeeCode
GO

PRINT '================================================'
PRINT 'Remaining foreign keys completed!'
PRINT '================================================'
PRINT 'Skipped: IdentityServer external identifiers'
PRINT 'Skipped: Data type mismatches (NVARCHAR vs VARCHAR)'
PRINT 'Skipped: Unique identifier columns (not FK candidates)'
PRINT '================================================'
GO
