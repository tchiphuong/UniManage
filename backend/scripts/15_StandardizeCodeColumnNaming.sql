-- =====================================================
-- MIGRATION: Chuẩn hóa tên cột Code trong database
-- Mục tiêu: Thống nhất naming convention
-- - Bảng dùng "Code" cho Primary Key
-- - Foreign Key dùng tên đầy đủ như "DepartmentCode", "PositionCode"
-- =====================================================

USE UniManage;
GO

PRINT '==== BẮNG ĐẦU CHUẨN HÓA CỘT CODE ===='
GO

-- =====================================================
-- 1. Kiểm tra và chuẩn hóa hr_departments
-- =====================================================
PRINT ''
PRINT '1. Kiểm tra hr_departments...'
GO

-- Kiểm tra xem bảng có tồn tại không
IF OBJECT_ID('dbo.hr_departments', 'U') IS NOT NULL
BEGIN
    -- Kiểm tra xem đã có cột Code chưa
    IF NOT EXISTS (
        SELECT 1 FROM sys.columns 
        WHERE object_id = OBJECT_ID('dbo.hr_departments') 
        AND name = 'Code'
    )
    BEGIN
        -- Nếu có cột DepartmentCode, đổi tên
        IF EXISTS (
            SELECT 1 FROM sys.columns 
            WHERE object_id = OBJECT_ID('dbo.hr_departments') 
            AND name = 'DepartmentCode'
        )
        BEGIN
            PRINT '   ↻ Đổi tên DepartmentCode → Code trong hr_departments'
            EXEC sp_rename 'hr_departments.DepartmentCode', 'Code', 'COLUMN';
            PRINT '   ✓ Đã đổi tên cột trong hr_departments'
        END
        ELSE
        BEGIN
            PRINT '   ⚠ Cột Code không tồn tại trong hr_departments - cần kiểm tra structure'
        END
    END
    ELSE
    BEGIN
        PRINT '   ✓ hr_departments.Code đã tồn tại'
    END
END
ELSE
BEGIN
    PRINT '   ⚠ Bảng hr_departments chưa tồn tại'
END
GO

-- =====================================================
-- 2. Chuẩn hóa hr_positions
-- =====================================================
PRINT ''
PRINT '2. Chuẩn hóa hr_positions...'
GO

IF OBJECT_ID('dbo.hr_positions', 'U') IS NOT NULL
BEGIN
    -- Kiểm tra xem có cột PositionCode không
    IF EXISTS (
        SELECT 1 FROM sys.columns 
        WHERE object_id = OBJECT_ID('dbo.hr_positions') 
        AND name = 'PositionCode'
    )
    BEGIN
        PRINT '   ↻ Bắt đầu đổi tên PositionCode → Code trong hr_positions'
        
        -- Bước 2.1: Tìm và drop tất cả foreign key constraints tham chiếu đến PositionCode
        DECLARE @fk_name NVARCHAR(255)
        DECLARE @fk_table NVARCHAR(255)
        DECLARE @fk_sql NVARCHAR(MAX)
        
        DECLARE fk_cursor CURSOR FOR
        SELECT 
            fk.name AS FK_Name,
            OBJECT_NAME(fk.parent_object_id) AS FK_Table
        FROM sys.foreign_keys fk
        INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
        INNER JOIN sys.columns c ON fkc.parent_object_id = c.object_id AND fkc.parent_column_id = c.column_id
        WHERE fkc.referenced_object_id = OBJECT_ID('dbo.hr_positions')
        AND c.name = 'PositionCode'
        
        OPEN fk_cursor
        FETCH NEXT FROM fk_cursor INTO @fk_name, @fk_table
        
        WHILE @@FETCH_STATUS = 0
        BEGIN
            SET @fk_sql = 'ALTER TABLE [dbo].[' + @fk_table + '] DROP CONSTRAINT [' + @fk_name + ']'
            PRINT '   ↻ Dropping FK: ' + @fk_name + ' từ bảng ' + @fk_table
            EXEC sp_executesql @fk_sql
            PRINT '   ✓ Đã drop FK: ' + @fk_name
            
            FETCH NEXT FROM fk_cursor INTO @fk_name, @fk_table
        END
        
        CLOSE fk_cursor
        DEALLOCATE fk_cursor
        
        -- Bước 2.2: Drop primary key constraint nếu PositionCode là PK
        DECLARE @pk_name NVARCHAR(255)
        SELECT @pk_name = kc.name
        FROM sys.key_constraints kc
        INNER JOIN sys.index_columns ic ON kc.parent_object_id = ic.object_id AND kc.unique_index_id = ic.index_id
        INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
        WHERE kc.parent_object_id = OBJECT_ID('dbo.hr_positions')
        AND kc.type = 'PK'
        AND c.name = 'PositionCode'
        
        IF @pk_name IS NOT NULL
        BEGIN
            PRINT '   ↻ Dropping PK: ' + @pk_name
            EXEC('ALTER TABLE [dbo].[hr_positions] DROP CONSTRAINT [' + @pk_name + ']')
            PRINT '   ✓ Đã drop PK: ' + @pk_name
        END
        
        -- Bước 2.3: Drop unique constraints nếu có
        DECLARE @uq_name NVARCHAR(255)
        DECLARE uq_cursor CURSOR FOR
        SELECT kc.name
        FROM sys.key_constraints kc
        INNER JOIN sys.index_columns ic ON kc.parent_object_id = ic.object_id AND kc.unique_index_id = ic.index_id
        INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
        WHERE kc.parent_object_id = OBJECT_ID('dbo.hr_positions')
        AND kc.type = 'UQ'
        AND c.name = 'PositionCode'
        
        OPEN uq_cursor
        FETCH NEXT FROM uq_cursor INTO @uq_name
        
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT '   ↻ Dropping UNIQUE: ' + @uq_name
            EXEC('ALTER TABLE [dbo].[hr_positions] DROP CONSTRAINT [' + @uq_name + ']')
            PRINT '   ✓ Đã drop UNIQUE: ' + @uq_name
            
            FETCH NEXT FROM uq_cursor INTO @uq_name
        END
        
        CLOSE uq_cursor
        DEALLOCATE uq_cursor
        
        -- Bước 2.4: Drop indexes liên quan
        DECLARE @idx_name NVARCHAR(255)
        DECLARE idx_cursor CURSOR FOR
        SELECT i.name
        FROM sys.indexes i
        INNER JOIN sys.index_columns ic ON i.object_id = ic.object_id AND i.index_id = ic.index_id
        INNER JOIN sys.columns c ON ic.object_id = c.object_id AND ic.column_id = c.column_id
        WHERE i.object_id = OBJECT_ID('dbo.hr_positions')
        AND c.name = 'PositionCode'
        AND i.is_primary_key = 0
        AND i.is_unique_constraint = 0
        
        OPEN idx_cursor
        FETCH NEXT FROM idx_cursor INTO @idx_name
        
        WHILE @@FETCH_STATUS = 0
        BEGIN
            PRINT '   ↻ Dropping INDEX: ' + @idx_name
            EXEC('DROP INDEX [' + @idx_name + '] ON [dbo].[hr_positions]')
            PRINT '   ✓ Đã drop INDEX: ' + @idx_name
            
            FETCH NEXT FROM idx_cursor INTO @idx_name
        END
        
        CLOSE idx_cursor
        DEALLOCATE idx_cursor
        
        -- Bước 2.5: Rename column
        PRINT '   ↻ Đổi tên cột PositionCode → Code'
        EXEC sp_rename 'hr_positions.PositionCode', 'Code', 'COLUMN';
        PRINT '   ✓ Đã đổi tên cột thành Code'
        
        -- Bước 2.6: Tạo lại Primary Key constraint
        IF @pk_name IS NOT NULL
        BEGIN
            PRINT '   ↻ Tạo lại PK với tên cột mới'
            ALTER TABLE [dbo].[hr_positions] ADD CONSTRAINT [PK_hr_positions] PRIMARY KEY ([Code]);
            PRINT '   ✓ Đã tạo PK_hr_positions trên cột Code'
        END
        
        -- Bước 2.7: Tạo lại Foreign Key từ hr_employees (nếu bảng tồn tại)
        IF OBJECT_ID('dbo.hr_employees', 'U') IS NOT NULL
        AND EXISTS (
            SELECT 1 FROM sys.columns 
            WHERE object_id = OBJECT_ID('dbo.hr_employees') 
            AND name = 'PositionCode'
        )
        BEGIN
            PRINT '   ↻ Tạo lại FK từ hr_employees.PositionCode → hr_positions.Code'
            IF NOT EXISTS (
                SELECT 1 FROM sys.foreign_keys 
                WHERE name = 'FK_hr_employees_hr_positions'
            )
            BEGIN
                ALTER TABLE [dbo].[hr_employees] 
                ADD CONSTRAINT [FK_hr_employees_hr_positions] 
                FOREIGN KEY ([PositionCode]) REFERENCES [dbo].[hr_positions]([Code]);
                PRINT '   ✓ Đã tạo FK_hr_employees_hr_positions'
            END
        END
        
        PRINT '   ✓ Hoàn tất chuẩn hóa hr_positions'
    END
    ELSE IF EXISTS (
        SELECT 1 FROM sys.columns 
        WHERE object_id = OBJECT_ID('dbo.hr_positions') 
        AND name = 'Code'
    )
    BEGIN
        PRINT '   ✓ hr_positions.Code đã tồn tại'
    END
    ELSE
    BEGIN
        PRINT '   ⚠ Không tìm thấy cột Code hoặc PositionCode trong hr_positions'
    END
END
ELSE
BEGIN
    PRINT '   ⚠ Bảng hr_positions chưa tồn tại'
END
GO

-- =====================================================
-- 3. Kiểm tra kết quả
-- =====================================================
PRINT ''
PRINT '3. Kiểm tra kết quả...'
GO

-- Kiểm tra hr_departments
IF OBJECT_ID('dbo.hr_departments', 'U') IS NOT NULL
BEGIN
    IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.hr_departments') AND name = 'Code')
        PRINT '   ✓ hr_departments.Code: OK'
    ELSE
        PRINT '   ✗ hr_departments.Code: MISSING'
END

-- Kiểm tra hr_positions
IF OBJECT_ID('dbo.hr_positions', 'U') IS NOT NULL
BEGIN
    IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.hr_positions') AND name = 'Code')
        PRINT '   ✓ hr_positions.Code: OK'
    ELSE
        PRINT '   ✗ hr_positions.Code: MISSING'
        
    IF EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID('dbo.hr_positions') AND name = 'PositionCode')
        PRINT '   ⚠ hr_positions.PositionCode vẫn còn tồn tại (cần review)'
END

-- Liệt kê Foreign Keys liên quan
PRINT ''
PRINT '4. Foreign Keys hiện tại:'
SELECT 
    OBJECT_NAME(fk.parent_object_id) AS FK_Table,
    fk.name AS FK_Name,
    OBJECT_NAME(fk.referenced_object_id) AS Referenced_Table,
    COL_NAME(fkc.parent_object_id, fkc.parent_column_id) AS FK_Column,
    COL_NAME(fkc.referenced_object_id, fkc.referenced_column_id) AS Referenced_Column
FROM sys.foreign_keys fk
INNER JOIN sys.foreign_key_columns fkc ON fk.object_id = fkc.constraint_object_id
WHERE OBJECT_NAME(fk.referenced_object_id) IN ('hr_departments', 'hr_positions')
   OR OBJECT_NAME(fk.parent_object_id) IN ('hr_departments', 'hr_positions')
ORDER BY FK_Table, FK_Name;

PRINT ''
PRINT '==== HOÀN TẤT CHUẨN HÓA ===='
PRINT ''
PRINT 'NAMING CONVENTION SAU KHI CHUẨN HÓA:'
PRINT '  ✓ hr_departments.Code (PK)'
PRINT '  ✓ hr_positions.Code (PK)'
PRINT '  ✓ hr_employees.DepartmentCode → hr_departments.Code (FK)'
PRINT '  ✓ hr_employees.PositionCode → hr_positions.Code (FK)'
PRINT ''
GO
