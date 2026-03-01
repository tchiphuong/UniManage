# Create Database Migration

Create a new database migration script for the UniManage SQL Server database.

## Steps

### 1. Clarify Migration Purpose

Ask the user:

-   What changes are needed? (Create table, Alter table, Add column, etc.)
-   Which table(s) are affected?
-   What is the migration for? (New feature, Bug fix, Refactoring)
-   Does it need to be reversible?
-   Will it affect existing data?

### 2. Determine Migration Type

Classify the migration:

-   **Schema Change** - CREATE/ALTER/DROP table/column
-   **Data Migration** - INSERT/UPDATE data
-   **Index Management** - CREATE/DROP indexes
-   **Constraint Management** - ADD/DROP foreign keys, unique constraints

### 3. Create Migration File

Location: `backend/database/migrations/`

File naming: `{SequenceNumber}_{Description}.sql`

Examples:

-   `01_CreateUsersTable.sql`
-   `02_AddEmailColumnToUsers.sql`
-   `03_CreateDepartmentsTable.sql`
-   `04_AddForeignKeyUserToDepartment.sql`

### 4. Write Migration Script

Use this template:

```sql
-- =============================================
-- Migration: {Description}
-- Date: {YYYY-MM-DD}
-- Author: {Name}
-- Description: {Detailed description of what this migration does}
-- =============================================

BEGIN TRANSACTION;

BEGIN TRY
    -- =============================================
    -- Your migration code here
    -- =============================================

    -- Example: Create table
    IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'table_name')
    BEGIN
        CREATE TABLE table_name (
            Id BIGINT IDENTITY(1,1) PRIMARY KEY,
            Column1 NVARCHAR(100) NOT NULL,

            -- Audit columns (REQUIRED)
            CreatedBy NVARCHAR(50) NOT NULL,
            CreatedAt DATETIME2(3) NOT NULL DEFAULT GETDATE(),
            UpdatedBy NVARCHAR(50) NULL,
            UpdatedAt DATETIME2(3) NULL,
            DataRowVersion ROWVERSION NOT NULL
        );

        PRINT 'Table table_name created successfully';
    END
    ELSE
    BEGIN
        PRINT 'Table table_name already exists, skipping...';
    END

    -- =============================================

    COMMIT TRANSACTION;
    PRINT '✓ Migration completed successfully';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();

    PRINT '✗ Migration failed: ' + @ErrorMessage;
    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;
GO
```

### 5. Implement Specific Migration Types

**CREATE TABLE:**

```sql
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'sy_users')
BEGIN
    CREATE TABLE sy_users (
        Id BIGINT IDENTITY(1,1) PRIMARY KEY,
        UserCode NVARCHAR(50) NOT NULL,
        DisplayName NVARCHAR(200) NOT NULL,
        Email NVARCHAR(255) NOT NULL,
        PasswordHash NVARCHAR(255) NOT NULL,
        PhoneNumber NVARCHAR(20) NULL,
        Status TINYINT NOT NULL DEFAULT 1,

        -- Audit columns
        CreatedBy NVARCHAR(50) NOT NULL,
        CreatedAt DATETIME2(3) NOT NULL DEFAULT GETDATE(),
        UpdatedBy NVARCHAR(50) NULL,
        UpdatedAt DATETIME2(3) NULL,
        DataRowVersion ROWVERSION NOT NULL,

        -- Constraints
        CONSTRAINT UQ_Users_UserCode UNIQUE (UserCode),
        CONSTRAINT UQ_Users_Email UNIQUE (Email)
    );

    PRINT 'Table sy_users created successfully';
END
```

**ADD COLUMN:**

```sql
IF NOT EXISTS (
    SELECT * FROM sys.columns
    WHERE object_id = OBJECT_ID('sy_users')
    AND name = 'NewColumn'
)
BEGIN
    ALTER TABLE sy_users
    ADD NewColumn NVARCHAR(100) NULL;

    PRINT 'Column NewColumn added to sy_users';
END
ELSE
BEGIN
    PRINT 'Column NewColumn already exists in sy_users';
END
```

**ALTER COLUMN:**

```sql
IF EXISTS (SELECT * FROM sys.columns WHERE object_id = OBJECT_ID('sy_users') AND name = 'Email')
BEGIN
    ALTER TABLE sy_users
    ALTER COLUMN Email NVARCHAR(300) NOT NULL;

    PRINT 'Column Email altered in sy_users';
END
```

**ADD FOREIGN KEY:**

```sql
IF NOT EXISTS (
    SELECT * FROM sys.foreign_keys
    WHERE name = 'FK_Employees_Departments'
)
BEGIN
    ALTER TABLE hr_employees
    ADD CONSTRAINT FK_Employees_Departments
    FOREIGN KEY (DepartmentId) REFERENCES hr_departments(Id)
    ON DELETE NO ACTION;  -- or CASCADE, SET NULL

    PRINT 'Foreign key FK_Employees_Departments added';
END
```

**CREATE INDEX:**

```sql
IF NOT EXISTS (
    SELECT * FROM sys.indexes
    WHERE name = 'IX_Users_Email'
    AND object_id = OBJECT_ID('sy_users')
)
BEGIN
    CREATE INDEX IX_Users_Email ON sy_users(Email);

    PRINT 'Index IX_Users_Email created';
END
```

**INSERT SEED DATA:**

```sql
-- Insert only if data doesn't exist
IF NOT EXISTS (SELECT * FROM sy_users WHERE UserCode = 'admin')
BEGIN
    INSERT INTO sy_users (UserCode, DisplayName, Email, PasswordHash, Status, CreatedBy, CreatedAt)
    VALUES ('admin', 'System Administrator', 'admin@unimanage.com', '{hash}', 1, 'SYSTEM', GETDATE());

    PRINT 'Admin user inserted';
END
ELSE
BEGIN
    PRINT 'Admin user already exists';
END
```

**MERGE for Idempotent Seeds:**

```sql
MERGE INTO lk_countries AS target
USING (VALUES
    ('VN', N'Việt Nam'),
    ('US', N'United States'),
    ('JP', N'Japan')
) AS source (CountryCode, CountryName)
ON target.CountryCode = source.CountryCode
WHEN NOT MATCHED THEN
    INSERT (CountryCode, CountryName, CreatedBy, CreatedAt)
    VALUES (source.CountryCode, source.CountryName, 'SYSTEM', GETDATE())
WHEN MATCHED THEN
    UPDATE SET CountryName = source.CountryName;

PRINT '3 countries merged';
```

**DROP COLUMN (with safety check):**

```sql
IF EXISTS (
    SELECT * FROM sys.columns
    WHERE object_id = OBJECT_ID('sy_users')
    AND name = 'OldColumn'
)
BEGIN
    -- Drop dependent objects first (indexes, constraints)
    IF EXISTS (SELECT * FROM sys.indexes WHERE name = 'IX_Users_OldColumn')
    BEGIN
        DROP INDEX IX_Users_OldColumn ON sy_users;
        PRINT 'Index dropped';
    END

    ALTER TABLE sy_users
    DROP COLUMN OldColumn;

    PRINT 'Column OldColumn dropped from sy_users';
END
```

### 6. Add Safety Checks

Include these checks in complex migrations:

```sql
-- Check if table has data before destructive operations
DECLARE @RowCount INT;
SELECT @RowCount = COUNT(*) FROM sy_users;

IF @RowCount > 0
BEGIN
    PRINT 'Warning: Table sy_users contains ' + CAST(@RowCount AS NVARCHAR(10)) + ' rows';
    -- Add additional safety logic or prompt
END

-- Backup data before modification
IF OBJECT_ID('tempdb..#UsersBackup') IS NOT NULL
    DROP TABLE #UsersBackup;

SELECT * INTO #UsersBackup FROM sy_users;
PRINT 'Backup created in #UsersBackup';

-- After modification, verify
IF (SELECT COUNT(*) FROM sy_users) = (SELECT COUNT(*) FROM #UsersBackup)
BEGIN
    PRINT 'Row count matches, migration successful';
END
ELSE
BEGIN
    RAISERROR('Row count mismatch detected!', 16, 1);
END
```

### 7. Test Migration

**Test in Development Database:**

1. **Open SQL Server Management Studio (SSMS)** or **Azure Data Studio**
2. **Connect to development database**
3. **Open migration file**
4. **Execute script**
5. **Verify results:**

```sql
-- Verify table created
SELECT * FROM sys.tables WHERE name = 'table_name';

-- Verify columns
SELECT name, TYPE_NAME(system_type_id), max_length, is_nullable
FROM sys.columns
WHERE object_id = OBJECT_ID('table_name');

-- Verify constraints
SELECT name, type_desc
FROM sys.objects
WHERE parent_object_id = OBJECT_ID('table_name')
AND type IN ('PK', 'F', 'UQ', 'C');

-- Verify indexes
SELECT name, type_desc, is_unique
FROM sys.indexes
WHERE object_id = OBJECT_ID('table_name');

-- Verify data
SELECT TOP 10 * FROM table_name;
```

### 8. Create Rollback Script (Optional)

For reversible migrations, create a rollback script:

File: `{SequenceNumber}_{Description}_Rollback.sql`

```sql
-- =============================================
-- Rollback: {Description}
-- Date: {YYYY-MM-DD}
-- =============================================

BEGIN TRANSACTION;

BEGIN TRY
    -- Reverse the changes

    -- If created table, drop it
    IF EXISTS (SELECT * FROM sys.tables WHERE name = 'table_name')
    BEGIN
        DROP TABLE table_name;
        PRINT 'Table table_name dropped';
    END

    -- If added column, drop it
    IF EXISTS (
        SELECT * FROM sys.columns
        WHERE object_id = OBJECT_ID('table_name')
        AND name = 'NewColumn'
    )
    BEGIN
        ALTER TABLE table_name DROP COLUMN NewColumn;
        PRINT 'Column NewColumn dropped';
    END

    COMMIT TRANSACTION;
    PRINT '✓ Rollback completed successfully';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    RAISERROR(@ErrorMessage, 16, 1);
END CATCH;
GO
```

### 9. Document Migration

Add entry to `backend/database/README.md`:

```markdown
## Migration History

| #   | File                    | Description            | Date       | Status     |
| --- | ----------------------- | ---------------------- | ---------- | ---------- |
| 01  | 01_CreateUsersTable.sql | Creates sy_users table | 2026-01-17 | ✅ Applied |
| 02  | 02_AddEmailColumn.sql   | Adds Email column      | 2026-01-17 | ✅ Applied |
```

### 10. Run Migration in Production

**Pre-deployment checklist:**

✅ Migration tested in development
✅ Rollback script prepared (if applicable)
✅ Database backup created
✅ Migration reviewed by team
✅ Downtime window scheduled (if needed)

**Execute migration:**

```bash
# Connect to production database
sqlcmd -S server -d database -U user -P password -i migration_file.sql

# Or via SSMS/Azure Data Studio
```

### 11. Verify Checklist

✅ Migration wrapped in transaction
✅ TRY/CATCH block for error handling
✅ IF EXISTS/NOT EXISTS checks for idempotency
✅ Audit columns included (CreatedBy, CreatedAt, etc.)
✅ Proper naming conventions (PascalCase, prefixes)
✅ Constraints properly named
✅ Indexes created for foreign keys
✅ NVARCHAR used for text (Vietnamese support)
✅ DATETIME2(3) used for timestamps
✅ PRINT statements for progress tracking
✅ Migration tested in development
✅ Documentation updated

## Summary

This workflow creates a complete database migration with:

-   Proper file naming and location
-   Transaction-wrapped execution
-   Error handling with TRY/CATCH
-   Idempotent checks (IF EXISTS/NOT EXISTS)
-   Audit columns for all tables
-   Safety checks for destructive operations
-   Rollback script (optional)
-   Testing verification queries
-   Documentation
