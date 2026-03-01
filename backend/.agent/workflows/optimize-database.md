# Database Optimization

Optimize database schema, queries, and performance for SQL Server.

## Steps

### 1. Identify Optimization Target

**Ask user:**

-   What needs optimization? (Slow query, table structure, indexes, overall performance)
-   Which tables/queries are affected?
-   Current performance metrics (query time, table size)
-   Expected load (reads/writes per second)

**Performance baselines:**

-   Simple query: < 50ms
-   Complex query with joins: < 200ms
-   Pagination query: < 100ms
-   Write operations: < 50ms

### 2. Analyze Current State

**Table size analysis:**

```sql
-- Check table sizes
SELECT
    t.name AS TableName,
    p.rows AS RowCount,
    SUM(a.total_pages) * 8 AS TotalSpaceKB,
    SUM(a.used_pages) * 8 AS UsedSpaceKB,
    (SUM(a.total_pages) - SUM(a.used_pages)) * 8 AS UnusedSpaceKB
FROM sys.tables t
INNER JOIN sys.indexes i ON t.object_id = i.object_id
INNER JOIN sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
INNER JOIN sys.allocation_units a ON p.partition_id = a.container_id
WHERE t.name NOT LIKE 'dt%'
    AND t.is_ms_shipped = 0
    AND i.object_id > 255
GROUP BY t.name, p.rows
ORDER BY TotalSpaceKB DESC;

-- Check index fragmentation
SELECT
    OBJECT_NAME(ips.object_id) AS TableName,
    i.name AS IndexName,
    ips.avg_fragmentation_in_percent,
    ips.page_count
FROM sys.dm_db_index_physical_stats(
    DB_ID(), NULL, NULL, NULL, 'DETAILED') ips
INNER JOIN sys.indexes i
    ON ips.object_id = i.object_id
    AND ips.index_id = i.index_id
WHERE ips.avg_fragmentation_in_percent > 10
    AND ips.page_count > 1000
ORDER BY ips.avg_fragmentation_in_percent DESC;
```

**Query performance analysis:**

```sql
-- Top 20 slowest queries
SELECT TOP 20
    qs.execution_count,
    SUBSTRING(qt.text, (qs.statement_start_offset/2)+1,
        ((CASE qs.statement_end_offset
            WHEN -1 THEN DATALENGTH(qt.text)
            ELSE qs.statement_end_offset
        END - qs.statement_start_offset)/2)+1) AS query_text,
    qs.total_elapsed_time / 1000000.0 AS total_elapsed_time_sec,
    qs.total_elapsed_time / qs.execution_count / 1000000.0 AS avg_elapsed_time_sec,
    qs.total_worker_time / 1000000.0 AS total_cpu_time_sec,
    qs.total_logical_reads,
    qs.total_physical_reads,
    qs.creation_time,
    qs.last_execution_time
FROM sys.dm_exec_query_stats qs
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) qt
ORDER BY qs.total_elapsed_time DESC;

-- Queries with high CPU usage
SELECT TOP 20
    qs.execution_count,
    SUBSTRING(qt.text, (qs.statement_start_offset/2)+1,
        ((CASE qs.statement_end_offset
            WHEN -1 THEN DATALENGTH(qt.text)
            ELSE qs.statement_end_offset
        END - qs.statement_start_offset)/2)+1) AS query_text,
    qs.total_worker_time / 1000000.0 AS total_cpu_sec,
    qs.total_worker_time / qs.execution_count / 1000000.0 AS avg_cpu_sec
FROM sys.dm_exec_query_stats qs
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) qt
ORDER BY qs.total_worker_time DESC;

-- Check for missing indexes
SELECT
    OBJECT_NAME(d.object_id) AS TableName,
    d.equality_columns,
    d.inequality_columns,
    d.included_columns,
    gs.avg_user_impact AS AvgImprovementPct,
    gs.user_seeks,
    gs.user_scans,
    gs.avg_total_user_cost * gs.avg_user_impact * (gs.user_seeks + gs.user_scans) AS ImpactScore
FROM sys.dm_db_missing_index_details d
INNER JOIN sys.dm_db_missing_index_groups g ON d.index_handle = g.index_handle
INNER JOIN sys.dm_db_missing_index_group_stats gs ON g.index_group_handle = gs.group_handle
WHERE d.database_id = DB_ID()
ORDER BY ImpactScore DESC;
```

### 3. Index Optimization

**Add missing indexes:**

```sql
-- Scenario 1: Filter by status (WHERE clause)
-- Query: SELECT * FROM sy_users WHERE Status = 1
CREATE NONCLUSTERED INDEX IX_Users_Status
ON sy_users(Status)
INCLUDE (UserCode, DisplayName, Email); -- Frequently accessed columns

-- Scenario 2: Filter + sort (WHERE + ORDER BY)
-- Query: SELECT * FROM sy_users WHERE Status = 1 ORDER BY CreatedAt DESC
CREATE NONCLUSTERED INDEX IX_Users_Status_CreatedAt
ON sy_users(Status, CreatedAt DESC);

-- Scenario 3: Join optimization
-- Query: JOIN hr_departments d ON u.DepartmentId = d.Id
CREATE NONCLUSTERED INDEX IX_Users_DepartmentId
ON sy_users(DepartmentId);

-- Scenario 4: Search optimization
-- Query: WHERE UserCode LIKE 'ABC%'
CREATE NONCLUSTERED INDEX IX_Users_UserCode
ON sy_users(UserCode);

-- Scenario 5: Composite filter
-- Query: WHERE DepartmentId = X AND Status = Y
CREATE NONCLUSTERED INDEX IX_Users_DepartmentId_Status
ON sy_users(DepartmentId, Status)
INCLUDE (UserCode, DisplayName);

-- Scenario 6: Covering index (query doesn't need to look up base table)
CREATE NONCLUSTERED INDEX IX_Users_Status_Covering
ON sy_users(Status)
INCLUDE (Id, UserCode, DisplayName, Email, CreatedAt);
```

**Index best practices:**

```sql
-- ✅ GOOD: Selective index (high cardinality)
CREATE INDEX IX_Users_Email ON sy_users(Email); -- Email is unique

-- ❌ BAD: Non-selective index (low cardinality)
CREATE INDEX IX_Users_IsActive ON sy_users(IsActive); -- Only 2 values (0/1)

-- ✅ GOOD: Most selective column first in composite index
CREATE INDEX IX_Employees_EmployeeCode_Status
ON hr_employees(EmployeeCode, Status); -- EmployeeCode more selective

-- ❌ BAD: Less selective column first
CREATE INDEX IX_Employees_Status_EmployeeCode
ON hr_employees(Status, EmployeeCode); -- Status has few distinct values

-- ✅ GOOD: Use INCLUDE for non-key columns
CREATE INDEX IX_Users_Status
ON sy_users(Status)
INCLUDE (DisplayName, Email); -- Don't need to sort by these

-- ✅ GOOD: Filtered index for subset of data
CREATE INDEX IX_Users_Active
ON sy_users(CreatedAt DESC)
WHERE Status = 1; -- Only active users

-- ✅ GOOD: Use columnstore for analytics/reporting tables
CREATE COLUMNSTORE INDEX CCI_AuditLogs
ON audit_logs;
```

**Remove redundant indexes:**

```sql
-- Find duplicate/redundant indexes
WITH IndexColumns AS (
    SELECT
        OBJECT_NAME(ic.object_id) AS TableName,
        i.name AS IndexName,
        ic.index_id,
        ic.key_ordinal,
        COL_NAME(ic.object_id, ic.column_id) AS ColumnName
    FROM sys.index_columns ic
    INNER JOIN sys.indexes i ON ic.object_id = i.object_id AND ic.index_id = i.index_id
    WHERE ic.key_ordinal > 0
)
SELECT
    i1.TableName,
    i1.IndexName AS Index1,
    i2.IndexName AS Index2
FROM IndexColumns i1
INNER JOIN IndexColumns i2
    ON i1.TableName = i2.TableName
    AND i1.ColumnName = i2.ColumnName
    AND i1.key_ordinal = i2.key_ordinal
    AND i1.IndexName < i2.IndexName
GROUP BY i1.TableName, i1.IndexName, i2.IndexName
HAVING COUNT(*) >= 1;

-- Drop redundant index
DROP INDEX IX_Users_Email_Duplicate ON sy_users;
```

### 4. Query Optimization

**Optimize SELECT statements:**

```sql
-- ❌ BAD: SELECT *
SELECT * FROM sy_users WHERE Status = 1;

-- ✅ GOOD: Specific columns
SELECT Id, UserCode, DisplayName, Email
FROM sy_users
WHERE Status = 1;

-- ❌ BAD: Function on indexed column (prevents index usage)
SELECT * FROM sy_users
WHERE UPPER(UserCode) = 'ADMIN';

-- ✅ GOOD: Use case-insensitive collation or index
SELECT * FROM sy_users
WHERE UserCode = 'admin' COLLATE SQL_Latin1_General_CP1_CI_AS;

-- ❌ BAD: OR prevents index usage
SELECT * FROM sy_users
WHERE UserCode = 'admin' OR Email = 'admin@example.com';

-- ✅ GOOD: Use UNION or IN
SELECT * FROM sy_users WHERE UserCode = 'admin'
UNION
SELECT * FROM sy_users WHERE Email = 'admin@example.com';

-- ❌ BAD: NOT IN with subquery (poor performance with NULLs)
SELECT * FROM sy_users
WHERE Id NOT IN (SELECT UserId FROM user_sessions);

-- ✅ GOOD: LEFT JOIN with NULL check or NOT EXISTS
SELECT u.* FROM sy_users u
LEFT JOIN user_sessions s ON u.Id = s.UserId
WHERE s.UserId IS NULL;

-- OR
SELECT u.* FROM sy_users u
WHERE NOT EXISTS (
    SELECT 1 FROM user_sessions s WHERE s.UserId = u.Id
);
```

**Optimize JOINs:**

```sql
-- ❌ BAD: Cross join (Cartesian product)
SELECT *
FROM sy_users u, hr_departments d
WHERE u.DepartmentId = d.Id;

-- ✅ GOOD: Explicit JOIN
SELECT
    u.Id, u.UserCode, u.DisplayName,
    d.DepartmentCode, d.DepartmentName
FROM sy_users u
INNER JOIN hr_departments d ON u.DepartmentId = d.Id;

-- ✅ GOOD: Join on indexed columns
-- Ensure both u.DepartmentId and d.Id are indexed

-- ❌ BAD: Join with function
SELECT *
FROM sy_users u
INNER JOIN hr_departments d ON CAST(u.DepartmentId AS VARCHAR) = d.Code;

-- ✅ GOOD: Match data types, no functions
SELECT *
FROM sy_users u
INNER JOIN hr_departments d ON u.DepartmentId = d.Id;
```

**Optimize pagination:**

```sql
-- ❌ BAD: OFFSET/FETCH without proper index
SELECT Id, UserCode, DisplayName
FROM sy_users
ORDER BY CreatedAt DESC
OFFSET 1000 ROWS FETCH NEXT 20 ROWS ONLY;
-- Slow for high OFFSET values

-- ✅ GOOD: Use indexed column for ordering
CREATE INDEX IX_Users_CreatedAt ON sy_users(CreatedAt DESC);

-- ✅ BETTER: Seek-based pagination (keyset pagination)
-- First page
SELECT TOP 20 Id, UserCode, DisplayName, CreatedAt
FROM sy_users
ORDER BY CreatedAt DESC, Id DESC;

-- Next page (using last CreatedAt and Id from previous page)
SELECT TOP 20 Id, UserCode, DisplayName, CreatedAt
FROM sy_users
WHERE CreatedAt < @LastCreatedAt
   OR (CreatedAt = @LastCreatedAt AND Id < @LastId)
ORDER BY CreatedAt DESC, Id DESC;
```

### 5. Table Structure Optimization

**Normalize data:**

```sql
-- ❌ BAD: Repeated data
CREATE TABLE Orders (
    OrderId INT PRIMARY KEY,
    CustomerName NVARCHAR(100),
    CustomerEmail NVARCHAR(100),
    CustomerPhone NVARCHAR(20)
    -- Customer info repeated in every order
);

-- ✅ GOOD: Normalized
CREATE TABLE Customers (
    CustomerId INT PRIMARY KEY,
    Name NVARCHAR(100),
    Email NVARCHAR(100),
    Phone NVARCHAR(20)
);

CREATE TABLE Orders (
    OrderId INT PRIMARY KEY,
    CustomerId INT NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);
```

**Choose appropriate data types:**

```sql
-- ❌ BAD: Oversized data types
CREATE TABLE Products (
    ProductId INT,
    ProductCode NVARCHAR(MAX), -- Too large!
    Quantity DECIMAL(38, 10),  -- Too precise!
    IsActive BIT
);

-- ✅ GOOD: Right-sized data types
CREATE TABLE Products (
    ProductId INT,
    ProductCode NVARCHAR(50), -- Appropriate size
    Quantity INT,             -- Integer is enough
    IsActive BIT
);

-- ❌ BAD: NVARCHAR for ASCII-only data
CREATE TABLE Logs (
    LogId INT,
    Severity VARCHAR(10),     -- ASCII is enough
    Message NVARCHAR(MAX)     -- But use NVARCHAR for Vietnamese
);

-- ✅ GOOD: Use VARCHAR for ASCII, NVARCHAR for Unicode
CREATE TABLE Logs (
    LogId BIGINT,             -- BIGINT for high-volume tables
    Severity VARCHAR(10),
    Message NVARCHAR(MAX)
);
```

**Partitioning for large tables:**

```sql
-- Scenario: audit_logs table with 100M+ rows
-- Partition by month for better management and performance

-- Step 1: Create partition function
CREATE PARTITION FUNCTION pfAuditLogsByMonth (DATETIME2)
AS RANGE RIGHT FOR VALUES
    ('2024-01-01', '2024-02-01', '2024-03-01', '2024-04-01',
     '2024-05-01', '2024-06-01', '2024-07-01', '2024-08-01',
     '2024-09-01', '2024-10-01', '2024-11-01', '2024-12-01');

-- Step 2: Create partition scheme
CREATE PARTITION SCHEME psAuditLogsByMonth
AS PARTITION pfAuditLogsByMonth
ALL TO ([PRIMARY]); -- Or to specific filegroups

-- Step 3: Create partitioned table
CREATE TABLE audit_logs (
    LogId BIGINT IDENTITY(1,1) NOT NULL,
    CreatedAt DATETIME2(3) NOT NULL,
    Username NVARCHAR(100),
    Action NVARCHAR(100),
    Details NVARCHAR(MAX),
    CONSTRAINT PK_AuditLogs PRIMARY KEY (LogId, CreatedAt)
) ON psAuditLogsByMonth(CreatedAt);

-- Query specific partition (fast!)
SELECT * FROM audit_logs
WHERE CreatedAt >= '2024-06-01' AND CreatedAt < '2024-07-01';

-- Archive old partitions
ALTER TABLE audit_logs SWITCH PARTITION 1 TO audit_logs_archive PARTITION 1;
```

### 6. Stored Procedure Optimization

**Use stored procedures for complex queries:**

```sql
-- Benefits: Compiled execution plan, reduced network traffic, better security
CREATE OR ALTER PROCEDURE sp_GetUserList
    @Keyword NVARCHAR(100) = NULL,
    @Status TINYINT = NULL,
    @PageIndex INT = 1,
    @PageSize INT = 20,
    @TotalItems INT OUTPUT
AS
BEGIN
    SET NOCOUNT ON;

    -- Get total count
    SELECT @TotalItems = COUNT(*)
    FROM sy_users
    WHERE (@Keyword IS NULL
        OR UserCode LIKE @Keyword + '%'
        OR DisplayName LIKE '%' + @Keyword + '%')
      AND (@Status IS NULL OR Status = @Status);

    -- Get paged results
    SELECT
        Id, UserCode, DisplayName, Email, Status, CreatedAt
    FROM sy_users
    WHERE (@Keyword IS NULL
        OR UserCode LIKE @Keyword + '%'
        OR DisplayName LIKE '%' + @Keyword + '%')
      AND (@Status IS NULL OR Status = @Status)
    ORDER BY CreatedAt DESC
    OFFSET (@PageIndex - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;
END;

-- Usage from Dapper
var parameters = new DynamicParameters();
parameters.Add("@Keyword", keyword);
parameters.Add("@Status", status);
parameters.Add("@PageIndex", pageIndex);
parameters.Add("@PageSize", pageSize);
parameters.Add("@TotalItems", dbType: DbType.Int32, direction: ParameterDirection.Output);

var users = await dbContext.QueryAsync<UserListItem>(
    "sp_GetUserList",
    parameters,
    commandType: CommandType.StoredProcedure);

var totalItems = parameters.Get<int>("@TotalItems");
```

### 7. Maintenance Tasks

**Update statistics:**

```sql
-- Update statistics for better query plans
UPDATE STATISTICS sy_users WITH FULLSCAN;

-- Update all table statistics
EXEC sp_updatestats;

-- Auto-update statistics (recommended)
ALTER DATABASE UniManage SET AUTO_UPDATE_STATISTICS ON;
ALTER DATABASE UniManage SET AUTO_UPDATE_STATISTICS_ASYNC ON;
```

**Rebuild/reorganize indexes:**

```sql
-- Check fragmentation
SELECT
    OBJECT_NAME(ips.object_id) AS TableName,
    i.name AS IndexName,
    ips.avg_fragmentation_in_percent
FROM sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, 'LIMITED') ips
INNER JOIN sys.indexes i ON ips.object_id = i.object_id AND ips.index_id = i.index_id
WHERE ips.avg_fragmentation_in_percent > 10
ORDER BY ips.avg_fragmentation_in_percent DESC;

-- Rebuild if fragmentation > 30%
ALTER INDEX IX_Users_Status ON sy_users REBUILD;

-- Reorganize if fragmentation 10-30%
ALTER INDEX IX_Users_Email ON sy_users REORGANIZE;

-- Rebuild all indexes on a table
ALTER INDEX ALL ON sy_users REBUILD;
```

**Create maintenance plan:**

```sql
-- SQL Server Agent job to run weekly
-- Step 1: Update statistics
EXEC sp_updatestats;

-- Step 2: Rebuild highly fragmented indexes
DECLARE @TableName NVARCHAR(128), @IndexName NVARCHAR(128), @SQL NVARCHAR(MAX);
DECLARE index_cursor CURSOR FOR
    SELECT OBJECT_NAME(ips.object_id), i.name
    FROM sys.dm_db_index_physical_stats(DB_ID(), NULL, NULL, NULL, 'LIMITED') ips
    INNER JOIN sys.indexes i ON ips.object_id = i.object_id AND ips.index_id = i.index_id
    WHERE ips.avg_fragmentation_in_percent > 30;

OPEN index_cursor;
FETCH NEXT FROM index_cursor INTO @TableName, @IndexName;

WHILE @@FETCH_STATUS = 0
BEGIN
    SET @SQL = 'ALTER INDEX ' + @IndexName + ' ON ' + @TableName + ' REBUILD';
    EXEC sp_executesql @SQL;
    FETCH NEXT FROM index_cursor INTO @TableName, @IndexName;
END;

CLOSE index_cursor;
DEALLOCATE index_cursor;

-- Step 3: Update statistics
UPDATE STATISTICS sy_users WITH FULLSCAN;
```

### 8. Monitoring and Alerts

**Create performance baseline:**

```sql
-- Record baseline metrics
CREATE TABLE performance_baseline (
    BaselineDate DATETIME2 DEFAULT GETDATE(),
    TableName NVARCHAR(128),
    RowCount BIGINT,
    TotalSpaceKB BIGINT,
    AvgQueryTimeMs DECIMAL(10,2)
);

INSERT INTO performance_baseline (TableName, RowCount, TotalSpaceKB)
SELECT
    t.name,
    p.rows,
    SUM(a.total_pages) * 8
FROM sys.tables t
INNER JOIN sys.indexes i ON t.object_id = i.object_id
INNER JOIN sys.partitions p ON i.object_id = p.object_id AND i.index_id = p.index_id
INNER JOIN sys.allocation_units a ON p.partition_id = a.container_id
GROUP BY t.name, p.rows;
```

**Set up alerts:**

```sql
-- Alert for slow queries (using SQL Server Agent)
-- Alert for blocking (wait time > 30s)
-- Alert for high CPU usage (> 80%)
-- Alert for low disk space (< 10%)
```

### 9. Document Optimizations

```markdown
## Database Optimization Report - [Date]

### Changes Made

1. **Added indexes:**

    - `IX_Users_Status_CreatedAt` on `sy_users(Status, CreatedAt DESC)`
    - `IX_Employees_DepartmentId` on `hr_employees(DepartmentId)`

2. **Optimized queries:**

    - `GetUserListQuery`: Changed from SELECT \* to specific columns
    - Added pagination index

3. **Table structure:**
    - Changed `UserCode` from NVARCHAR(MAX) to NVARCHAR(50)
    - Normalized `Orders` table

### Performance Improvements

-   User list query: 850ms → 45ms (95% improvement)
-   Employee search: 320ms → 28ms (91% improvement)
-   Order creation: 120ms → 35ms (71% improvement)

### Maintenance Plan

-   Weekly: Rebuild fragmented indexes (> 30%)
-   Weekly: Update statistics
-   Monthly: Archive old audit logs

### Monitoring

-   Set up alerts for slow queries (> 1s)
-   Track index fragmentation weekly
-   Monitor table growth
```

### 10. Verify Checklist

✅ Slow queries identified and optimized
✅ Missing indexes added
✅ Redundant indexes removed
✅ Table structure optimized
✅ Appropriate data types used
✅ Statistics updated
✅ Indexes defragmented
✅ Maintenance plan created
✅ Performance monitoring in place
✅ Documentation updated

## Database Optimization Best Practices

### Indexing

-   Index foreign keys
-   Index columns in WHERE, JOIN, ORDER BY clauses
-   Use covering indexes for frequently queried columns
-   Don't over-index (slows writes)
-   Most selective column first in composite indexes

### Query Writing

-   Avoid SELECT \*
-   Use specific column names
-   Avoid functions on indexed columns
-   Use proper JOIN syntax
-   Parameterize queries (prevent SQL injection + better plans)

### Table Design

-   Normalize to reduce redundancy
-   Use appropriate data types (not too large)
-   Use INT for IDs (BIGINT for very large tables)
-   Use NVARCHAR for Unicode, VARCHAR for ASCII
-   Consider partitioning for very large tables (> 100M rows)

### Maintenance

-   Update statistics regularly (weekly)
-   Rebuild fragmented indexes (> 30%)
-   Archive old data
-   Monitor query performance
-   Regular backups

## Summary

This workflow provides comprehensive database optimization with:

-   Performance analysis and metrics
-   Index optimization strategies
-   Query optimization techniques
-   Table structure improvements
-   Maintenance procedures
-   Monitoring and alerting setup
-   Documentation of changes
