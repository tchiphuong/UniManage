-- ================================================
-- Script: 08_UpdateAuditColumnsToVarchar.sql
-- Description: Update CreatedBy/UpdatedBy to VARCHAR(50) in all tables
-- Reason: These columns store username which is VARCHAR(50)
-- Date: 2026-01-07
-- ================================================

USE [UniManage]
GO

PRINT '================================================'
PRINT 'Updating audit columns to VARCHAR(50)...'
PRINT '================================================'
GO

-- Get all tables with CreatedBy or UpdatedBy columns
DECLARE @sql NVARCHAR(MAX) = ''
DECLARE @tableName NVARCHAR(128)
DECLARE @columnName NVARCHAR(128)
DECLARE @dataType NVARCHAR(128)

DECLARE columnCursor CURSOR FOR
SELECT DISTINCT 
    t.TABLE_NAME,
    c.COLUMN_NAME,
    c.DATA_TYPE
FROM INFORMATION_SCHEMA.TABLES t
INNER JOIN INFORMATION_SCHEMA.COLUMNS c ON t.TABLE_NAME = c.TABLE_NAME
WHERE t.TABLE_SCHEMA = 'dbo'
    AND t.TABLE_TYPE = 'BASE TABLE'
    AND c.COLUMN_NAME IN ('CreatedBy', 'UpdatedBy')
    AND c.DATA_TYPE = 'nvarchar'
ORDER BY t.TABLE_NAME, c.COLUMN_NAME

OPEN columnCursor
FETCH NEXT FROM columnCursor INTO @tableName, @columnName, @dataType

WHILE @@FETCH_STATUS = 0
BEGIN
    BEGIN TRY
        SET @sql = 'ALTER TABLE [dbo].[' + @tableName + '] ALTER COLUMN [' + @columnName + '] VARCHAR(50) NULL'
        EXEC sp_executesql @sql
        PRINT '✓ Updated ' + @tableName + '.' + @columnName + ' to VARCHAR(50)'
    END TRY
    BEGIN CATCH
        PRINT '✗ Failed to update ' + @tableName + '.' + @columnName + ': ' + ERROR_MESSAGE()
    END CATCH
    
    FETCH NEXT FROM columnCursor INTO @tableName, @columnName, @dataType
END

CLOSE columnCursor
DEALLOCATE columnCursor

PRINT '================================================'
PRINT 'Audit columns update completed!'
PRINT '================================================'
GO
