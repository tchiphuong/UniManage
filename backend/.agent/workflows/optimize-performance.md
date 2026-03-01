# Optimize Performance

Identify and fix performance issues in the UniManage system.

## Steps

### 1. Identify Performance Issue

**User reports:**

-   What is slow? (Page load, API response, query execution)
-   When did it start?
-   How often does it happen?
-   Which users are affected?
-   Current metrics (response time, load time)

**Performance targets:**

-   API response: < 500ms (p95)
-   Page load: < 2s (FCP)
-   Database query: < 100ms
-   Time to Interactive: < 3s

### 2. Measure Current Performance

**Backend Performance Metrics:**

````bash
# Check API response times in logs
grep -r "elapsed.*ms" backend/logs/$(date +%Y-%m-%d)/

# SQL Server query performance
```sql
-- Find slow queries
SELECT TOP 20
    qs.execution_count,
    qs.total_elapsed_time / 1000000.0 AS total_elapsed_time_sec,
    qs.total_worker_time / 1000000.0 AS total_cpu_time_sec,
    qs.total_logical_reads,
    qs.total_physical_reads,
    SUBSTRING(qt.text, (qs.statement_start_offset/2)+1,
        ((CASE qs.statement_end_offset
            WHEN -1 THEN DATALENGTH(qt.text)
            ELSE qs.statement_end_offset
        END - qs.statement_start_offset)/2)+1) AS statement_text
FROM sys.dm_exec_query_stats qs
CROSS APPLY sys.dm_exec_sql_text(qs.sql_handle) qt
ORDER BY qs.total_elapsed_time DESC;

-- Check missing indexes
SELECT
    OBJECT_NAME(d.object_id) AS TableName,
    d.equality_columns,
    d.inequality_columns,
    d.included_columns,
    s.avg_user_impact,
    s.user_seeks
FROM sys.dm_db_missing_index_details d
INNER JOIN sys.dm_db_missing_index_group_stats s
    ON d.index_handle = s.group_handle
ORDER BY s.avg_user_impact * s.user_seeks DESC;
````

**Frontend Performance Metrics:**

```bash
# Use Lighthouse
npx lighthouse http://localhost:3000 --view

# Check bundle size
npm run build
# Look at .next/build-manifest.json

# React DevTools Profiler
# Open browser DevTools → Profiler tab
# Record interaction and analyze
```

### 3. Backend Performance Optimization

**Database Query Optimization:**

**Problem: N+1 Query**

```csharp
// ❌ BAD - N+1 queries
var users = await dbContext.QueryAsync<User>("SELECT * FROM sy_users");
foreach (var user in users)
{
    var department = await dbContext.QuerySingleAsync<Department>(
        "SELECT * FROM hr_departments WHERE Id = @DeptId",
        new { DeptId = user.DepartmentId });
    // Process...
}

// ✅ GOOD - Single query with JOIN
var users = await dbContext.QueryAsync<UserWithDepartment>(@"
    SELECT
        u.Id, u.Username, u.DisplayName,
        d.DepartmentCode, d.DepartmentName
    FROM sy_users u
    LEFT JOIN hr_departments d ON u.DepartmentId = d.Id
");
```

**Problem: SELECT \***

```csharp
// ❌ BAD - Fetching unnecessary columns
var users = await dbContext.QueryAsync<User>("SELECT * FROM sy_users");

// ✅ GOOD - Only fetch needed columns
var users = await dbContext.QueryAsync<UserListItem>(@"
    SELECT Id, Username, DisplayName, Email, Status
    FROM sy_users
");
```

**Problem: Missing indexes**

```sql
-- Add index for frequently queried columns
CREATE INDEX IX_Users_Email ON sy_users(Email);
CREATE INDEX IX_Users_Status_CreatedAt ON sy_users(Status, CreatedAt);

-- Composite index for filtered queries
CREATE INDEX IX_Employees_DepartmentId_Status
ON hr_employees(DepartmentId, Status)
INCLUDE (EmployeeCode, DisplayName);
```

**Problem: Inefficient pagination**

```csharp
// ❌ BAD - Loading all rows then skipping
var allUsers = await dbContext.QueryAsync<User>("SELECT * FROM sy_users");
var page = allUsers.Skip((pageIndex - 1) * pageSize).Take(pageSize);

// ✅ GOOD - Database-level pagination
var users = await dbContext.QueryAsync<User>(@"
    SELECT Id, Username, DisplayName
    FROM sy_users
    ORDER BY CreatedAt DESC
    OFFSET @Skip ROWS FETCH NEXT @Take ROWS ONLY
", new { Skip = (pageIndex - 1) * pageSize, Take = pageSize });
```

**Problem: Unnecessary database calls**

```csharp
// ❌ BAD - Multiple calls in loop
foreach (var userId in userIds)
{
    var exists = await dbContext.ExecuteScalarAsync<bool>(
        "SELECT CASE WHEN EXISTS(SELECT 1 FROM sy_users WHERE Id = @Id) THEN 1 ELSE 0 END",
        new { Id = userId });
}

// ✅ GOOD - Single batch query
var existingIds = await dbContext.QueryAsync<int>(
    "SELECT Id FROM sy_users WHERE Id IN @Ids",
    new { Ids = userIds });
```

**Caching Strategy:**

```csharp
// Add caching behavior for frequently accessed data
public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : class
{
    private readonly IMemoryCache _cache;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        // Only cache queries
        if (request is not BaseQuery)
            return await next();

        var cacheKey = $"{typeof(TRequest).Name}:{JsonConvert.SerializeObject(request)}";

        if (_cache.TryGetValue(cacheKey, out TResponse cachedResponse))
            return cachedResponse;

        var response = await next();

        _cache.Set(cacheKey, response, TimeSpan.FromMinutes(5));

        return response;
    }
}
```

**Response Compression:**

```csharp
// Startup.cs or Program.cs
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
    options.Providers.Add<BrotliCompressionProvider>();
});

builder.Services.Configure<GzipCompressionProviderOptions>(options =>
{
    options.Level = CompressionLevel.Fastest;
});
```

### 4. Frontend Performance Optimization

**Code Splitting:**

```tsx
// ❌ BAD - Import everything upfront
import { UserTable } from "@/components/features/users/UserTable";
import { DepartmentTable } from "@/components/features/departments/DepartmentTable";

// ✅ GOOD - Lazy load components
import dynamic from "next/dynamic";

const UserTable = dynamic(() => import("@/components/features/users/UserTable"), {
    loading: () => <Spinner />,
    ssr: false, // Disable SSR if not needed
});

const DepartmentTable = dynamic(() => import("@/components/features/departments/DepartmentTable"));
```

**Image Optimization:**

```tsx
// ❌ BAD - Regular img tag
<img src="/logo.png" width={200} height={50} />;

// ✅ GOOD - Next.js Image component
import Image from "next/image";

<Image
    src="/logo.png"
    alt="Logo"
    width={200}
    height={50}
    priority // For above-the-fold images
/>;
```

**Bundle Size Optimization:**

```bash
# Analyze bundle
npm run build
npx @next/bundle-analyzer

# Remove unused dependencies
npm uninstall unused-package

# Use tree-shaking friendly imports
# ❌ BAD
import _ from 'lodash';

# ✅ GOOD
import debounce from 'lodash/debounce';
```

**React Component Optimization:**

```tsx
// ❌ BAD - Re-renders on every parent render
export function UserCard({ user }) {
  return <div>{user.name}</div>;
}

// ✅ GOOD - Memoized component
import { memo } from 'react';

export const UserCard = memo(function UserCard({ user }) {
  return <div>{user.name}</div>;
});

// ❌ BAD - Expensive calculation on every render
export function UserList({ users }) {
  const sortedUsers = users.sort((a, b) => a.name.localeCompare(b.name));
  return <div>{sortedUsers.map(...)}</div>;
}

// ✅ GOOD - Memoized calculation
import { useMemo } from 'react';

export function UserList({ users }) {
  const sortedUsers = useMemo(
    () => users.sort((a, b) => a.name.localeCompare(b.name)),
    [users]
  );
  return <div>{sortedUsers.map(...)}</div>;
}

// ❌ BAD - New function on every render
export function UserList() {
  return <UserCard onDelete={(id) => deleteUser(id)} />;
}

// ✅ GOOD - Memoized callback
import { useCallback } from 'react';

export function UserList() {
  const handleDelete = useCallback((id) => {
    deleteUser(id);
  }, []);

  return <UserCard onDelete={handleDelete} />;
}
```

**TanStack Query Optimization:**

```tsx
// ❌ BAD - Fetching too frequently
useQuery({
    queryKey: ["users"],
    queryFn: UserService.getList,
    refetchInterval: 1000, // Every second!
});

// ✅ GOOD - Appropriate stale time
useQuery({
    queryKey: ["users"],
    queryFn: UserService.getList,
    staleTime: 5 * 60 * 1000, // 5 minutes
    cacheTime: 10 * 60 * 1000, // 10 minutes
    refetchOnWindowFocus: false, // Disable if not needed
});

// ❌ BAD - Not using placeholderData
const { data } = useQuery({
    queryKey: ["users", page],
    queryFn: () => UserService.getList({ pageIndex: page }),
});

// ✅ GOOD - Show previous data while fetching
const { data } = useQuery({
    queryKey: ["users", page],
    queryFn: () => UserService.getList({ pageIndex: page }),
    placeholderData: (previousData) => previousData,
});
```

**Virtual Scrolling for Large Lists:**

```tsx
// ❌ BAD - Rendering 10,000 items
<div>
    {users.map((user) => (
        <UserCard key={user.id} user={user} />
    ))}
</div>;

// ✅ GOOD - Virtual scrolling
import { useVirtualizer } from "@tanstack/react-virtual";

export function UserList({ users }) {
    const parentRef = useRef<HTMLDivElement>(null);

    const virtualizer = useVirtualizer({
        count: users.length,
        getScrollElement: () => parentRef.current,
        estimateSize: () => 50, // Estimated row height
    });

    return (
        <div ref={parentRef} style={{ height: "600px", overflow: "auto" }}>
            <div style={{ height: `${virtualizer.getTotalSize()}px`, position: "relative" }}>
                {virtualizer.getVirtualItems().map((virtualItem) => (
                    <div
                        key={virtualItem.key}
                        style={{
                            position: "absolute",
                            top: 0,
                            left: 0,
                            width: "100%",
                            height: `${virtualItem.size}px`,
                            transform: `translateY(${virtualItem.start}px)`,
                        }}
                    >
                        <UserCard user={users[virtualItem.index]} />
                    </div>
                ))}
            </div>
        </div>
    );
}
```

### 5. Database Performance Tuning

**Update Statistics:**

```sql
-- Update statistics for better query plans
UPDATE STATISTICS sy_users WITH FULLSCAN;

-- Rebuild indexes
ALTER INDEX ALL ON sy_users REBUILD;
```

**Identify and Fix Blocking:**

```sql
-- Find blocking queries
SELECT
    blocking.session_id AS blocking_session_id,
    blocked.session_id AS blocked_session_id,
    blocking_text.text AS blocking_query,
    blocked_text.text AS blocked_query
FROM sys.dm_exec_requests blocked
INNER JOIN sys.dm_exec_requests blocking
    ON blocked.blocking_session_id = blocking.session_id
CROSS APPLY sys.dm_exec_sql_text(blocking.sql_handle) blocking_text
CROSS APPLY sys.dm_exec_sql_text(blocked.sql_handle) blocked_text;
```

**Optimize Table Structure:**

```sql
-- Add appropriate indexes
CREATE NONCLUSTERED INDEX IX_Users_Email
ON sy_users(Email)
INCLUDE (Username, DisplayName);

-- Partition large tables (if needed)
CREATE PARTITION FUNCTION pfYearMonth (DATETIME2)
AS RANGE RIGHT FOR VALUES
    ('2024-01-01', '2024-02-01', '2024-03-01', ...);
```

### 6. Monitor Performance Improvements

**Before/After Comparison:**

```bash
# Backend - Compare API response times
# Before optimization
Average: 1200ms, P95: 2500ms, P99: 5000ms

# After optimization
Average: 300ms, P95: 500ms, P99: 800ms

# Database - Compare query execution times
# Before: 450ms
# After: 50ms (9x improvement)

# Frontend - Compare Lighthouse scores
# Before: Performance 45, FCP 3.2s, LCP 4.5s
# After: Performance 92, FCP 1.1s, LCP 1.8s
```

### 7. Implement Performance Monitoring

**Application Insights / Logging:**

```csharp
// Log slow queries
public class PerformanceLoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger _logger;

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken ct)
    {
        var sw = Stopwatch.StartNew();
        var response = await next();
        sw.Stop();

        if (sw.ElapsedMilliseconds > 500) // Slow query threshold
        {
            _logger.Warning($"Slow query detected: {typeof(TRequest).Name} took {sw.ElapsedMilliseconds}ms");
        }

        return response;
    }
}
```

**Frontend Performance Monitoring:**

```tsx
// components/PerformanceMonitor.tsx
"use client";

import { useEffect } from "react";

export function PerformanceMonitor() {
    useEffect(() => {
        // Report Web Vitals
        if (typeof window !== "undefined" && "PerformanceObserver" in window) {
            const observer = new PerformanceObserver((list) => {
                for (const entry of list.getEntries()) {
                    console.log(`${entry.name}: ${entry.duration}ms`);
                    // Send to analytics
                }
            });

            observer.observe({ entryTypes: ["navigation", "paint", "largest-contentful-paint"] });
        }
    }, []);

    return null;
}
```

### 8. Document Performance Improvements

**Create performance report:**

```markdown
## Performance Optimization Report

### Issue

User list page was loading slowly (3-5 seconds)

### Root Cause

1. Missing index on sy_users(Status)
2. N+1 queries loading departments
3. Frontend rendering 1000+ items without virtualization

### Optimizations Applied

1. Added composite index: IX_Users_Status_CreatedAt
2. Changed to single JOIN query
3. Implemented virtual scrolling with @tanstack/react-virtual

### Results

-   API response time: 1200ms → 250ms (80% improvement)
-   Database query: 450ms → 35ms (92% improvement)
-   Page load time: 4.2s → 1.3s (69% improvement)
-   Lighthouse Performance Score: 45 → 88

### Next Steps

-   Monitor performance over next week
-   Consider implementing Redis cache for frequently accessed data
```

### 9. Verify Checklist

✅ Performance issue identified and measured
✅ Root cause analyzed
✅ Backend queries optimized
✅ Database indexes added
✅ Frontend components optimized
✅ Bundle size reduced
✅ Caching strategy implemented
✅ Performance metrics improved
✅ Monitoring in place
✅ Documentation updated

## Performance Best Practices

### Backend

-   Use database-level pagination
-   Add indexes for frequently queried columns
-   Avoid N+1 queries (use JOINs)
-   Cache frequently accessed data
-   Use async/await properly
-   Enable response compression

### Frontend

-   Lazy load components
-   Use React.memo, useMemo, useCallback
-   Implement virtual scrolling for large lists
-   Optimize images with next/image
-   Code split large bundles
-   Set appropriate staleTime for queries

### Database

-   Regular index maintenance
-   Update statistics regularly
-   Monitor slow queries
-   Optimize table structure
-   Use appropriate data types

## Summary

This workflow provides systematic performance optimization with:

-   Performance measurement and monitoring
-   Backend query optimization
-   Frontend bundle and component optimization
-   Database tuning and indexing
-   Before/after metrics comparison
-   Documentation of improvements
