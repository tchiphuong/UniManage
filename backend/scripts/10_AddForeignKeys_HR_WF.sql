-- ================================================
-- Script: 10_AddForeignKeys_HR_WF.sql
-- Description: Add foreign keys for HR and Workflow tables
-- Date: 2026-01-07
-- ================================================

USE [UniManage]
GO

PRINT '================================================'
PRINT 'Adding HR and Workflow foreign keys...'
PRINT '================================================'
GO

-- =============================================
-- HR Tables
-- =============================================

-- hr_attendance.EmployeeCode -> hr_employees.EmployeeCode
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_hr_attendance_employees')
BEGIN
    ALTER TABLE [dbo].[hr_attendance]
    ADD CONSTRAINT [FK_hr_attendance_employees]
    FOREIGN KEY ([EmployeeCode]) REFERENCES [dbo].[hr_employees]([EmployeeCode])
    PRINT '✓ hr_attendance.EmployeeCode -> hr_employees.EmployeeCode'
END
ELSE PRINT '- hr_attendance.EmployeeCode FK exists'
GO

-- hr_salaries.EmployeeCode -> hr_employees.EmployeeCode
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_hr_salaries_employees')
BEGIN
    ALTER TABLE [dbo].[hr_salaries]
    ADD CONSTRAINT [FK_hr_salaries_employees]
    FOREIGN KEY ([EmployeeCode]) REFERENCES [dbo].[hr_employees]([EmployeeCode])
    PRINT '✓ hr_salaries.EmployeeCode -> hr_employees.EmployeeCode'
END
ELSE PRINT '- hr_salaries.EmployeeCode FK exists'
GO

-- hr_employee_shifts.EmployeeCode -> hr_employees.EmployeeCode
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_hr_employee_shifts_employees')
BEGIN
    ALTER TABLE [dbo].[hr_employee_shifts]
    ADD CONSTRAINT [FK_hr_employee_shifts_employees]
    FOREIGN KEY ([EmployeeCode]) REFERENCES [dbo].[hr_employees]([EmployeeCode])
    PRINT '✓ hr_employee_shifts.EmployeeCode -> hr_employees.EmployeeCode'
END
ELSE PRINT '- hr_employee_shifts.EmployeeCode FK exists'
GO

-- hr_employees.DepartmentCode -> hr_departments.Code
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_hr_employees_departments')
BEGIN
    ALTER TABLE [dbo].[hr_employees]
    ADD CONSTRAINT [FK_hr_employees_departments]
    FOREIGN KEY ([DepartmentCode]) REFERENCES [dbo].[hr_departments]([Code])
    PRINT '✓ hr_employees.DepartmentCode -> hr_departments.Code'
END
ELSE PRINT '- hr_employees.DepartmentCode FK exists'
GO

-- hr_employees.PositionCode -> hr_positions.PositionCode
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_hr_employees_positions')
BEGIN
    ALTER TABLE [dbo].[hr_employees]
    ADD CONSTRAINT [FK_hr_employees_positions]
    FOREIGN KEY ([PositionCode]) REFERENCES [dbo].[hr_positions]([PositionCode])
    PRINT '✓ hr_employees.PositionCode -> hr_positions.PositionCode'
END
ELSE PRINT '- hr_employees.PositionCode FK exists'
GO

-- =============================================
-- Workflow Tables (Skip FK to hr_employees due to data type mismatch)
-- Note: wf_* uses NVARCHAR, hr_employees.EmployeeCode is VARCHAR
-- =============================================

-- wf_request.ApprovalRouteId -> wf_approval_route.Id
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_wf_request_approval_route')
BEGIN
    ALTER TABLE [dbo].[wf_request]
    ADD CONSTRAINT [FK_wf_request_approval_route]
    FOREIGN KEY ([ApprovalRouteId]) REFERENCES [dbo].[wf_approval_route]([Id])
    PRINT '✓ wf_request.ApprovalRouteId -> wf_approval_route.Id'
END
ELSE PRINT '- wf_request.ApprovalRouteId FK exists'
GO

-- wf_request_approval.RequestId -> wf_request.Id
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_wf_request_approval_request')
BEGIN
    ALTER TABLE [dbo].[wf_request_approval]
    ADD CONSTRAINT [FK_wf_request_approval_request]
    FOREIGN KEY ([RequestId]) REFERENCES [dbo].[wf_request]([Id])
    ON DELETE CASCADE
    PRINT '✓ wf_request_approval.RequestId -> wf_request.Id'
END
ELSE PRINT '- wf_request_approval.RequestId FK exists'
GO

-- wf_approval_route_level.ApprovalRouteId -> wf_approval_route.Id
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_wf_approval_route_level_route')
BEGIN
    ALTER TABLE [dbo].[wf_approval_route_level]
    ADD CONSTRAINT [FK_wf_approval_route_level_route]
    FOREIGN KEY ([ApprovalRouteId]) REFERENCES [dbo].[wf_approval_route]([Id])
    ON DELETE CASCADE
    PRINT '✓ wf_approval_route_level.ApprovalRouteId -> wf_approval_route.Id'
END
ELSE PRINT '- wf_approval_route_level.ApprovalRouteId FK exists'
GO

-- wf_approval_route_approver.ApprovalRouteLevelId -> wf_approval_route_level.Id
IF NOT EXISTS (SELECT 1 FROM sys.foreign_keys WHERE name = 'FK_wf_approval_route_approver_level')
BEGIN
    ALTER TABLE [dbo].[wf_approval_route_approver]
    ADD CONSTRAINT [FK_wf_approval_route_approver_level]
    FOREIGN KEY ([ApprovalRouteLevelId]) REFERENCES [dbo].[wf_approval_route_level]([Id])
    ON DELETE CASCADE
    PRINT '✓ wf_approval_route_approver.ApprovalRouteLevelId -> wf_approval_route_level.Id'
END
ELSE PRINT '- wf_approval_route_approver.ApprovalRouteLevelId FK exists'
GO

PRINT '================================================'
PRINT 'HR and Workflow foreign keys completed!'
PRINT 'Note: Skipped wf_* -> hr_employees FKs due to data type mismatch'
PRINT '(wf_* uses NVARCHAR, hr_employees.EmployeeCode is VARCHAR)'
PRINT '================================================'
GO
