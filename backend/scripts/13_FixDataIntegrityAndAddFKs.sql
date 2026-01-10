-- ================================================
-- Script: 13_FixDataIntegrityAndAddFKs.sql
-- Description: Fix data integrity issues and add remaining FKs
-- Date: 2026-01-07
-- ================================================

USE [UniManage]
GO

PRINT '================================================'
PRINT 'Fixing data integrity and adding FKs...'
PRINT '================================================'
GO

-- =============================================
-- 1. Fix sy_users.EmployeeCode data integrity
-- =============================================

-- Create missing EMP001 in hr_employees if not exists
IF NOT EXISTS (SELECT 1 FROM hr_employees WHERE EmployeeCode = 'EMP001')
BEGIN
    INSERT INTO hr_employees (
        EmployeeCode, 
        FirstName, 
        LastName, 
        Status,
        CreatedBy, 
        CreatedAt
    )
    VALUES (
        'EMP001',
        'System',
        'Admin',
        'active',
        'system',
        GETDATE()
    )
    PRINT '✓ Created EMP001 in hr_employees'
END
ELSE
    PRINT '- EMP001 already exists'
GO

-- Now add FK: sy_users.EmployeeCode -> hr_employees.EmployeeCode
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_sy_users_employees')
BEGIN
    ALTER TABLE [dbo].[sy_users]
    ADD CONSTRAINT [FK_sy_users_employees]
    FOREIGN KEY ([EmployeeCode]) REFERENCES [dbo].[hr_employees]([EmployeeCode])
    PRINT '✓ sy_users.EmployeeCode -> hr_employees.EmployeeCode'
END
ELSE
    PRINT '- sy_users.EmployeeCode FK exists'
GO

-- =============================================
-- 2. Fix wf_* data type mismatch (NVARCHAR -> VARCHAR)
-- =============================================

-- wf_request.ApplicantEmployeeCode
PRINT 'Converting wf_request.ApplicantEmployeeCode to VARCHAR(50)...'
ALTER TABLE [dbo].[wf_request]
ALTER COLUMN [ApplicantEmployeeCode] VARCHAR(50) NULL
PRINT '✓ wf_request.ApplicantEmployeeCode converted'
GO

-- wf_request_approval.ApproverEmployeeCode
PRINT 'Converting wf_request_approval.ApproverEmployeeCode to VARCHAR(50)...'
ALTER TABLE [dbo].[wf_request_approval]
ALTER COLUMN [ApproverEmployeeCode] VARCHAR(50) NULL
PRINT '✓ wf_request_approval.ApproverEmployeeCode converted'
GO

-- wf_approval_route_approver.ApproverEmployeeCode
PRINT 'Converting wf_approval_route_approver.ApproverEmployeeCode to VARCHAR(50)...'
ALTER TABLE [dbo].[wf_approval_route_approver]
ALTER COLUMN [ApproverEmployeeCode] VARCHAR(50) NULL
PRINT '✓ wf_approval_route_approver.ApproverEmployeeCode converted'
GO

-- =============================================
-- 3. Add wf_* FKs to hr_employees
-- =============================================

-- wf_request.ApplicantEmployeeCode -> hr_employees.EmployeeCode
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_wf_request_applicant')
BEGIN
    ALTER TABLE [dbo].[wf_request]
    ADD CONSTRAINT [FK_wf_request_applicant]
    FOREIGN KEY ([ApplicantEmployeeCode]) REFERENCES [dbo].[hr_employees]([EmployeeCode])
    PRINT '✓ wf_request.ApplicantEmployeeCode -> hr_employees.EmployeeCode'
END
ELSE
    PRINT '- wf_request.ApplicantEmployeeCode FK exists'
GO

-- wf_request_approval.ApproverEmployeeCode -> hr_employees.EmployeeCode
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_wf_request_approval_approver')
BEGIN
    ALTER TABLE [dbo].[wf_request_approval]
    ADD CONSTRAINT [FK_wf_request_approval_approver]
    FOREIGN KEY ([ApproverEmployeeCode]) REFERENCES [dbo].[hr_employees]([EmployeeCode])
    PRINT '✓ wf_request_approval.ApproverEmployeeCode -> hr_employees.EmployeeCode'
END
ELSE
    PRINT '- wf_request_approval.ApproverEmployeeCode FK exists'
GO

-- wf_approval_route_approver.ApproverEmployeeCode -> hr_employees.EmployeeCode
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_wf_approval_route_approver_employee')
BEGIN
    ALTER TABLE [dbo].[wf_approval_route_approver]
    ADD CONSTRAINT [FK_wf_approval_route_approver_employee]
    FOREIGN KEY ([ApproverEmployeeCode]) REFERENCES [dbo].[hr_employees]([EmployeeCode])
    PRINT '✓ wf_approval_route_approver.ApproverEmployeeCode -> hr_employees.EmployeeCode'
END
ELSE
    PRINT '- wf_approval_route_approver.ApproverEmployeeCode FK exists'
GO

PRINT '================================================'
PRINT 'Data integrity fixes and FK additions completed!'
PRINT '================================================'
GO
