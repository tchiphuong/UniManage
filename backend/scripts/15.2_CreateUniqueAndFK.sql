-- =====================================================
-- FIX: Tạo UNIQUE constraint cho hr_positions.Code và FK
-- Strategy: Id là PK, Code là UNIQUE, FK reference Code
-- =====================================================

USE UniManage;
GO

PRINT '==== FIX UNIQUE CONSTRAINT & FK ===='
GO

-- Tạo UNIQUE constraint cho hr_positions.Code
IF OBJECT_ID('dbo.hr_positions', 'U') IS NOT NULL
BEGIN
    IF NOT EXISTS (
        SELECT 1 
        FROM sys.key_constraints kc
        INNER JOIN sys.index_columns ic ON kc.parent_object_id = ic.object_id AND kc.unique_index_id = ic.index_id
        INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
        WHERE kc.parent_object_id = OBJECT_ID('dbo.hr_positions')
        AND kc.type = 'UQ'
        AND c.name = 'Code'
    )
    BEGIN
        PRINT '↻ Tạo UNIQUE constraint cho hr_positions.Code'
        
        ALTER TABLE [dbo].[hr_positions]
        ADD CONSTRAINT [UQ_hr_positions_Code] UNIQUE ([Code]);
        
        PRINT '✓ Đã tạo UQ_hr_positions_Code'
    END
    ELSE
    BEGIN
        PRINT '✓ UNIQUE constraint đã tồn tại trên hr_positions.Code'
    END
END
GO

-- Tạo FK từ hr_employees.PositionCode → hr_positions.Code
IF OBJECT_ID('dbo.hr_employees', 'U') IS NOT NULL
AND EXISTS (
    SELECT 1 FROM sys.columns 
    WHERE object_id = OBJECT_ID('dbo.hr_employees') 
    AND name = 'PositionCode'
)
BEGIN
    -- Drop FK cũ nếu tồn tại
    IF EXISTS (
        SELECT 1 FROM sys.foreign_keys 
        WHERE name = 'FK_hr_employees_hr_positions'
    )
    BEGIN
        PRINT '↻ Drop FK cũ'
        ALTER TABLE [dbo].[hr_employees] DROP CONSTRAINT [FK_hr_employees_hr_positions];
    END
    
    PRINT '↻ Tạo FK từ hr_employees.PositionCode → hr_positions.Code'
    
    ALTER TABLE [dbo].[hr_employees] 
    ADD CONSTRAINT [FK_hr_employees_hr_positions] 
    FOREIGN KEY ([PositionCode]) REFERENCES [dbo].[hr_positions]([Code]);
    
    PRINT '✓ Đã tạo FK_hr_employees_hr_positions'
END
GO

-- Verify
PRINT ''
PRINT '==== VERIFY CONSTRAINTS ===='
PRINT ''
PRINT 'Primary Keys:'
SELECT 
    OBJECT_NAME(kc.parent_object_id) AS Table_Name,
    kc.name AS Constraint_Name,
    c.name AS Column_Name
FROM sys.key_constraints kc
INNER JOIN sys.index_columns ic ON kc.parent_object_id = ic.object_id AND kc.unique_index_id = ic.index_id
INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
WHERE kc.type = 'PK'
AND OBJECT_NAME(kc.parent_object_id) IN ('hr_positions', 'hr_departments')
ORDER BY Table_Name;

PRINT ''
PRINT 'Unique Constraints:'
SELECT 
    OBJECT_NAME(kc.parent_object_id) AS Table_Name,
    kc.name AS Constraint_Name,
    c.name AS Column_Name
FROM sys.key_constraints kc
INNER JOIN sys.index_columns ic ON kc.parent_object_id = ic.object_id AND kc.unique_index_id = ic.index_id
INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
WHERE kc.type = 'UQ'
AND OBJECT_NAME(kc.parent_object_id) IN ('hr_positions', 'hr_departments')
ORDER BY Table_Name;

PRINT ''
PRINT 'Foreign Keys:'
SELECT 
    OBJECT_NAME(fk.parent_object_id) AS FK_Table,
    fk.name AS FK_Name,
    OBJECT_NAME(fk.referenced_object_id) AS Referenced_Table,
    COL_NAME(fkc.parent_object_id, fkc.parent_column_id) AS FK_Column,
    COL_NAME(fkc.referenced_object_id, fkc.referenced_column_id) AS Referenced_Column
FROM sys.foreign_keys fk
INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
WHERE OBJECT_NAME(fk.referenced_object_id) IN ('hr_departments', 'hr_positions')
ORDER BY FK_Table, FK_Name;

PRINT ''
PRINT '✓ HOÀN TẤT'
PRINT ''
PRINT 'SUMMARY:'
PRINT '  - hr_positions: Id (PK) + Code (UNIQUE)'
PRINT '  - hr_departments: Code (PK - theo bilingual pattern)'
PRINT '  - hr_employees.PositionCode → hr_positions.Code (FK)'
PRINT '  - hr_employees.DepartmentCode → hr_departments.Code (FK)'
GO
