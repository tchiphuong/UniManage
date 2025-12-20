-- =====================================================
-- MIGRATION: Add Uuid column to all tables with Identity Id
-- =====================================================

DECLARE @TableName NVARCHAR(256);
DECLARE @Sql NVARCHAR(MAX);

DECLARE TableCursor CURSOR FOR
SELECT t.name
FROM sys.tables t
JOIN sys.identity_columns ic ON t.object_id = ic.object_id
WHERE ic.name = 'Id';

OPEN TableCursor;
FETCH NEXT FROM TableCursor INTO @TableName;

WHILE @@FETCH_STATUS = 0
BEGIN
    -- Check if Uuid column exists
    IF NOT EXISTS (
        SELECT 1 
        FROM sys.columns 
        WHERE object_id = OBJECT_ID(@TableName) AND name = 'Uuid'
    )
    BEGIN
        PRINT 'Adding Uuid to table: ' + @TableName;
        
        -- Note: SQL Server ALTER TABLE adds columns to the end. 
        -- Reordering columns (Id -> Uuid) requires table recreation which is risky for migration scripts.
        -- In C# Models/API, the order is handled to be Id -> Uuid.

        -- Add Uuid column with default value
        SET @Sql = 'ALTER TABLE ' + QUOTENAME(@TableName) + ' ADD Uuid UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID();';
        EXEC sp_executesql @Sql;
        
        -- Add Unique Index
        SET @Sql = 'CREATE UNIQUE INDEX IX_' + @TableName + '_Uuid ON ' + QUOTENAME(@TableName) + '(Uuid);';
        EXEC sp_executesql @Sql;
    END

    FETCH NEXT FROM TableCursor INTO @TableName;
END

CLOSE TableCursor;
DEALLOCATE TableCursor;
GO
