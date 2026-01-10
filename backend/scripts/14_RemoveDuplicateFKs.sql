-- ================================================
-- Script: 14_RemoveDuplicateFKs.sql
-- Description: Remove duplicate foreign key constraints
-- Date: 2026-01-07
-- ================================================

USE [UniManage]
GO

PRINT '================================================'
PRINT 'Removing duplicate foreign keys...'
PRINT '================================================'
GO

-- Drop duplicate FK constraints (keeping the first one created)

ALTER TABLE [dbo].[hr_attendance] DROP CONSTRAINT [FK_hr_attendance_employees]
PRINT '✓ Dropped hr_attendance.FK_hr_attendance_employees'
GO

ALTER TABLE [dbo].[hr_employee_shifts] DROP CONSTRAINT [FK_hr_employee_shifts_employees]
PRINT '✓ Dropped hr_employee_shifts.FK_hr_employee_shifts_employees'
GO

ALTER TABLE [dbo].[hr_employees] DROP CONSTRAINT [FK_hr_employees_departments]
PRINT '✓ Dropped hr_employees.FK_hr_employees_departments'
GO

ALTER TABLE [dbo].[hr_employees] DROP CONSTRAINT [FK_hr_employees_positions]
PRINT '✓ Dropped hr_employees.FK_hr_employees_positions'
GO

ALTER TABLE [dbo].[hr_salaries] DROP CONSTRAINT [FK_hr_salaries_employees]
PRINT '✓ Dropped hr_salaries.FK_hr_salaries_employees'
GO

ALTER TABLE [dbo].[it_item_tag_map] DROP CONSTRAINT [FK_it_item_tag_map_tags]
PRINT '✓ Dropped it_item_tag_map.FK_it_item_tag_map_tags'
GO

ALTER TABLE [dbo].[it_items] DROP CONSTRAINT [FK_it_items_brand]
PRINT '✓ Dropped it_items.FK_it_items_brand'
GO

ALTER TABLE [dbo].[it_items] DROP CONSTRAINT [FK_it_items_category]
PRINT '✓ Dropped it_items.FK_it_items_category'
GO

ALTER TABLE [dbo].[sy_function_mapping] DROP CONSTRAINT [FK_sy_function_mapping_actions]
PRINT '✓ Dropped sy_function_mapping.FK_sy_function_mapping_actions'
GO

ALTER TABLE [dbo].[sy_function_mapping] DROP CONSTRAINT [FK_sy_function_mapping_functions]
PRINT '✓ Dropped sy_function_mapping.FK_sy_function_mapping_functions'
GO

ALTER TABLE [dbo].[sy_role_permissions] DROP CONSTRAINT [FK_sy_role_permissions_roles]
PRINT '✓ Dropped sy_role_permissions.FK_sy_role_permissions_roles'
GO

ALTER TABLE [dbo].[sy_role_permissions] DROP CONSTRAINT [FK_sy_role_permissions_functions]
PRINT '✓ Dropped sy_role_permissions.FK_sy_role_permissions_functions'
GO

ALTER TABLE [dbo].[sy_role_permissions] DROP CONSTRAINT [FK_sy_role_permissions_actions]
PRINT '✓ Dropped sy_role_permissions.FK_sy_role_permissions_actions'
GO

ALTER TABLE [dbo].[sy_users] DROP CONSTRAINT [FK_sy_users_roles]
PRINT '✓ Dropped sy_users.FK_sy_users_roles'
GO

ALTER TABLE [dbo].[wf_request] DROP CONSTRAINT [FK_wf_request_approval_route]
PRINT '✓ Dropped wf_request.FK_wf_request_approval_route'
GO

PRINT '================================================'
PRINT 'Duplicate foreign keys removed successfully!'
PRINT '================================================'
GO
