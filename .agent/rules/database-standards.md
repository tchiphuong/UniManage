# Database Standards

**Activation**: Model Decision - Apply when working with database schemas, migrations, or SQL queries

This rule applies to all database-related work in the UniManage project.

## Database Platform

-   **SQL Server** only
-   Use **Dapper** for data access (NO Entity Framework Core)
-   Store migrations in `backend/database/migrations/`

## Naming Conventions

### Tables

-   PascalCase: `Users`, `Departments`, `EmployeePositions`
-   Plural nouns for entity tables
-   Prefix for system tables: `sy_*` (e.g., `sy_users`, `sy_menus`)
-   Prefix for lookup tables: `lk_*` (e.g., `lk_countries`, `lk_provinces`)

### Columns

-   PascalCase: `UserId`, `DisplayName`, `CreatedAt`
-   Primary key always: `Id`
-   Foreign keys: `{TableName}Id` (e.g., `DepartmentId`, `UserId`)

### Column Suffixes

Use meaningful suffixes:

-   `*Amount` - Money values
-   `*Number` - Numeric identifiers
-   `*Name` - Text names
-   `*Code` - Alphanumeric codes/identifiers
-   `*Qty` - Quantities
-   `*Date` - Date-only values
-   `*At` - DateTime values
-   `*Rate` - Percentages

### Indexes

-   Format: `IX_{TableName}_{ColumnName}`
-   Example: `IX_Users_Email`, `IX_Employees_DepartmentId`

### Constraints

-   Primary Key: `PK_{TableName}`
-   Foreign Key: `FK_{TableName}_{ReferencedTable}`
-   Unique: `UQ_{TableName}_{ColumnName}`
-   Check: `CK_{TableName}_{ColumnName}`

## Table Structure Standards

### Primary Key

```sql
Id BIGINT IDENTITY(1,1) PRIMARY KEY
-- or
Id INT IDENTITY(1,1) PRIMARY KEY
```

### Data Types

Use these consistent types:

-   **Text**: `NVARCHAR(length)` - For Vietnamese support
-   **DateTime**: `DATETIME2(3)` - Millisecond precision
-   **Money**: `DECIMAL(18,2)` - Financial values
-   **Boolean**: `BIT` - True/False flags
-   **Large Text**: `NVARCHAR(MAX)` - Descriptions, notes

### Audit Columns (REQUIRED for ALL tables)

```sql
CreatedBy NVARCHAR(50) NOT NULL,
CreatedAt DATETIME2(3) NOT NULL DEFAULT GETDATE(),
UpdatedBy NVARCHAR(50) NULL,
UpdatedAt DATETIME2(3) NULL,
DataRowVersion ROWVERSION NOT NULL
```

**Notes:**

-   `CreatedBy` and `CreatedAt` are NOT NULL (must be set on insert)
-   `UpdatedBy` and `UpdatedAt` are NULL (set only on update)
-   `DataRowVersion` is ROWVERSION for optimistic concurrency

### Example Table

```sql
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

-- Indexes
CREATE INDEX IX_Users_Status ON sy_users(Status);
CREATE INDEX IX_Users_Email ON sy_users(Email);
```

## Migration Standards

### File Naming

Format: `{SequenceNumber}_{Description}.sql`

Examples:

-   `01_CreateUsersTable.sql`
-   `02_AddForeignKeys.sql`
-   `03_InsertInitialData.sql`

### Migration Template

```sql
-- =============================================
-- Migration: {Description}
-- Date: {YYYY-MM-DD}
-- Author: {Name}
-- =============================================

BEGIN TRANSACTION;

BEGIN TRY
    -- Your migration code here

    COMMIT TRANSACTION;
    PRINT 'Migration completed successfully';
END TRY
BEGIN CATCH
    ROLLBACK TRANSACTION;
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    DECLARE @ErrorSeverity INT = ERROR_SEVERITY();
    DECLARE @ErrorState INT = ERROR_STATE();

    RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
END CATCH;
GO
```

## Query Standards

### NEVER use `SELECT *`

```sql
-- ❌ BAD
SELECT * FROM sy_users;

-- ✅ GOOD
SELECT Id, UserCode, DisplayName, Email, Status
FROM sy_users;
```

### Always Use Parameterized Queries

Dapper automatically handles this:

```csharp
// ✅ GOOD - Parameterized
var users = await dbContext.QueryAsync<User>(
    "SELECT Id, UserCode FROM sy_users WHERE Status = @Status",
    new { Status = 1 }
);

// ❌ BAD - String concatenation (SQL injection risk)
var users = await dbContext.QueryAsync<User>(
    $"SELECT Id, UserCode FROM sy_users WHERE Status = {status}"
);
```

### Pagination Pattern

```sql
SELECT Id, UserCode, DisplayName, Email
FROM sy_users
WHERE Status = @Status
ORDER BY CreatedAt DESC, Id DESC
OFFSET (@PageIndex - 1) * @PageSize ROWS
FETCH NEXT @PageSize ROWS ONLY;

-- Total count
SELECT COUNT(*)
FROM sy_users
WHERE Status = @Status;
```

### Use DatabaseHelper for Pagination

```csharp
var baseQuery = "SELECT Id, UserCode, DisplayName FROM sy_users";
var whereClause = "Status = @Status";
var orderBy = "CreatedAt DESC, Id DESC";
var parameters = new { Status = 1 };

var result = await DatabaseHelper.QueryPagingAsync<UserDto>(
    dbContext,
    baseQuery,
    whereClause,
    orderBy,
    parameters,
    pageIndex: 1,
    pageSize: 20
);
```

## Foreign Key Standards

### Naming Convention

```sql
ALTER TABLE child_table
ADD CONSTRAINT FK_ChildTable_ParentTable
FOREIGN KEY (ParentId) REFERENCES parent_table(Id);
```

### Cascading Rules

Choose appropriate cascade behavior:

-   `ON DELETE CASCADE` - Delete child rows when parent is deleted
-   `ON DELETE SET NULL` - Set foreign key to NULL when parent is deleted
-   `ON DELETE NO ACTION` - Prevent deletion if child rows exist (default)

```sql
-- Example: Delete user positions when user is deleted
ALTER TABLE hr_employee_positions
ADD CONSTRAINT FK_EmployeePositions_Users
FOREIGN KEY (UserId) REFERENCES sy_users(Id)
ON DELETE CASCADE;

-- Example: Keep records but set user to NULL when user is deleted
ALTER TABLE audit_logs
ADD CONSTRAINT FK_AuditLogs_Users
FOREIGN KEY (UserId) REFERENCES sy_users(Id)
ON DELETE SET NULL;
```

## Index Standards

### When to Create Indexes

Create indexes for:

-   ✅ Foreign keys
-   ✅ Columns used in WHERE clauses frequently
-   ✅ Columns used in ORDER BY
-   ✅ Columns used in JOINs
-   ✅ Unique constraints (automatic)

Don't over-index:

-   ❌ Small tables (< 1000 rows)
-   ❌ Columns that change frequently
-   ❌ Columns with low cardinality (few distinct values)

### Index Types

```sql
-- Non-clustered index (most common)
CREATE INDEX IX_Users_Email ON sy_users(Email);

-- Unique index
CREATE UNIQUE INDEX UQ_Users_UserCode ON sy_users(UserCode);

-- Composite index (multiple columns)
CREATE INDEX IX_Users_Status_CreatedAt ON sy_users(Status, CreatedAt);

-- Covering index (includes additional columns)
CREATE INDEX IX_Users_Status_Covering
ON sy_users(Status)
INCLUDE (UserCode, DisplayName, Email);
```

## Transaction Standards

### Use Transactions for Data Modifications

```csharp
using (var dbContext = new DbContext(openTransaction: true))
{
    try
    {
        // Multiple operations
        await dbContext.ExecuteAsync("INSERT INTO ...", param1);
        await dbContext.ExecuteAsync("UPDATE ...", param2);
        await dbContext.ExecuteAsync("DELETE FROM ...", param3);

        await dbContext.CommitAsync();
    }
    catch
    {
        await dbContext.RollbackAsync();
        throw;
    }
}
```

### Read Operations Don't Need Transactions

```csharp
using (var dbContext = new DbContext())
{
    var users = await dbContext.QueryAsync<User>("SELECT ...");
    return users;
}
```

## Optimistic Concurrency

Use `DataRowVersion` (ROWVERSION) for concurrency control:

```sql
UPDATE sy_users
SET DisplayName = @DisplayName,
    UpdatedBy = @UpdatedBy,
    UpdatedAt = GETDATE()
WHERE Id = @Id
  AND DataRowVersion = @RowVersion;  -- Concurrency check

IF @@ROWCOUNT = 0
    THROW 50001, 'Concurrency conflict: Record was modified by another user', 1;
```

In C#:

```csharp
var rowsAffected = await dbContext.ExecuteAsync(
    @"UPDATE sy_users
      SET DisplayName = @DisplayName, UpdatedBy = @UpdatedBy, UpdatedAt = GETDATE()
      WHERE Id = @Id AND DataRowVersion = @RowVersion",
    new { Id = id, DisplayName = name, UpdatedBy = user, RowVersion = rowVersion }
);

if (rowsAffected == 0)
    throw new DbConcurrencyException("Record was modified by another user");
```

## Data Seeding Standards

### Initial Data Format

```sql
-- Insert system users
INSERT INTO sy_users (UserCode, DisplayName, Email, PasswordHash, Status, CreatedBy, CreatedAt)
VALUES
    ('admin', 'System Administrator', 'admin@unimanage.com', '{hash}', 1, 'SYSTEM', GETDATE()),
    ('demo', 'Demo User', 'demo@unimanage.com', '{hash}', 1, 'SYSTEM', GETDATE());

-- Insert lookup data
INSERT INTO lk_countries (CountryCode, CountryName, CreatedBy, CreatedAt)
VALUES
    ('VN', N'Việt Nam', 'SYSTEM', GETDATE()),
    ('US', N'United States', 'SYSTEM', GETDATE());
```

### Use MERGE for Idempotent Seeds

```sql
MERGE INTO lk_countries AS target
USING (VALUES
    ('VN', N'Việt Nam'),
    ('US', N'United States')
) AS source (CountryCode, CountryName)
ON target.CountryCode = source.CountryCode
WHEN NOT MATCHED THEN
    INSERT (CountryCode, CountryName, CreatedBy, CreatedAt)
    VALUES (source.CountryCode, source.CountryName, 'SYSTEM', GETDATE());
```

## Checklist for Database Work

Before creating/modifying database objects:

✅ Table names are PascalCase and plural
✅ Columns are PascalCase with meaningful suffixes
✅ Primary key is `Id` (INT or BIGINT IDENTITY)
✅ Audit columns included (CreatedBy, CreatedAt, UpdatedBy, UpdatedAt, DataRowVersion)
✅ Foreign keys have proper naming and cascade rules
✅ Indexes created for frequently queried columns
✅ NVARCHAR used for text (Vietnamese support)
✅ DATETIME2(3) used for timestamps
✅ Constraints properly named
✅ Migration wrapped in transaction with TRY/CATCH
✅ No `SELECT *` in queries
✅ Parameterized queries used (via Dapper)
✅ Concurrency control with DataRowVersion
