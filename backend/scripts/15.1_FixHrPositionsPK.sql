-- =====================================================
-- FIX: Tạo lại Primary Key cho hr_positions.Code
-- =====================================================

USE UniManage;
GO

PRINT '==== FIX PRIMARY KEY CHO hr_positions ===='
GO

-- Kiểm tra và tạo PK cho hr_positions.Code
IF OBJECT_ID('dbo.hr_positions', 'U') IS NOT NULL
BEGIN
    -- Kiểm tra xem đã có PK chưa
    IF NOT EXISTS (
        SELECT 1 
        FROM sys.key_constraints 
        WHERE parent_object_id = OBJECT_ID('dbo.hr_positions')
        AND type = 'PK'
    )
    BEGIN
        PRINT '↻ Tạo Primary Key cho hr_positions.Code'
        
        ALTER TABLE [dbo].[hr_positions] 
        ADD CONSTRAINT [PK_hr_positions] PRIMARY KEY ([Code]);
        
        PRINT '✓ Đã tạo PK_hr_positions'
    END
    ELSE
    BEGIN
        PRINT '✓ Primary Key đã tồn tại trên hr_positions'
    END
    
    -- Tạo lại FK từ hr_employees nếu chưa có
    IF OBJECT_ID('dbo.hr_employees', 'U') IS NOT NULL
    AND EXISTS (
        SELECT 1 FROM sys.columns 
        WHERE object_id = OBJECT_ID('dbo.hr_employees') 
        AND name = 'PositionCode'
    )
    BEGIN
        IF NOT EXISTS (
            SELECT 1 FROM sys.foreign_keys 
            WHERE name = 'FK_hr_employees_hr_positions'
        )
        BEGIN
            PRINT '↻ Tạo FK từ hr_employees.PositionCode → hr_positions.Code'
            
            ALTER TABLE [dbo].[hr_employees] 
            ADD CONSTRAINT [FK_hr_employees_hr_positions] 
            FOREIGN KEY ([PositionCode]) REFERENCES [dbo].[hr_positions]([Code]);
            
            PRINT '✓ Đã tạo FK_hr_employees_hr_positions'
        END
        ELSE
        BEGIN
            PRINT '✓ FK_hr_employees_hr_positions đã tồn tại'
        END
    END
END
GO

-- Verify
PRINT ''
PRINT '==== VERIFY ===='
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
GO
