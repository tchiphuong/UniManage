# Read API Logs

Guide agent through reading and analyzing UniManage API logs.

## When to Use This Skill

- User reports API error or unexpected behavior
- Need to troubleshoot API performance issues
- Investigating failed requests or exceptions
- Analyzing API usage patterns
- Debugging authentication/authorization issues

## Log Structure

**Location:** `backend/logs/YYYY-MM-DD/{api-name}.log`

**File naming convention:**
- Normal logs: `{ControllerName}-{ActionName}.log` (e.g., `Users-Create.log`)
- Error logs: `error-{ControllerName}-{ActionName}.log`
- Examples:
  - `backend/logs/2026-01-17/Users-Create.log`
  - `backend/logs/2026-01-17/error-Users-Create.log`
  - `backend/logs/2026-01-17/Departments-GetList.log`
  - `backend/logs/2026-01-17/System.log` (System-level logs)

**Log format (JSON per line):**

```json
{
  "timestamp": "2026-01-17 14:23:45.123",
  "level": "INFO",
  "username": "admin",
  "ipAddress": "192.168.1.100",
  "traceId": "abc123-def456-ghi789",
  "method": "POST",
  "path": "/api/v1/users",
  "statusCode": 200,
  "elapsedMs": 145,
  "parameters": "{\"username\":\"testuser\",\"email\":\"test@example.com\",\"password\":\"***MASKED***\"}",
  "result": "{\"returnCode\":0,\"message\":\"User created successfully\",\"data\":{\"id\":123}}",
  "returnCode": 0,
  "message": "User created successfully",
  "isException": 0,
  "errorMessage": null
}
```

**Log levels:**
- `INFO`: Normal successful operations
- `WARNING`: Non-critical issues (slow query, validation warning)
- `ERROR`: Errors but handled (validation failed, business rule violation)
- `FATAL`: Critical unhandled exceptions

## How to Read Logs

### Step 1: Identify the log file

Ask user:
- What date did the issue occur? (default: today)
- Which API endpoint? (e.g., Users/Create, Departments/GetList)
- Specific time range?

**Find log file:**
```powershell
# List today's logs
Get-ChildItem backend/logs/$(Get-Date -Format "yyyy-MM-dd")

# Find specific API logs
Get-ChildItem backend/logs/$(Get-Date -Format "yyyy-MM-dd") -Filter "*Users*"
```

### Step 2: Read log file

```powershell
# Read entire log file
Get-Content backend/logs/2026-01-17/Users-Create.log

# Read last 50 lines (recent entries)
Get-Content backend/logs/2026-01-17/Users-Create.log -Tail 50

# Read error logs
Get-Content backend/logs/2026-01-17/error-Users-Create.log
```

### Step 3: Parse JSON logs

```powershell
# Parse JSON and display nicely
Get-Content backend/logs/2026-01-17/Users-Create.log | ForEach-Object {
    $_ | ConvertFrom-Json
} | Format-Table timestamp, username, statusCode, elapsedMs, message -AutoSize

# Filter by user
Get-Content backend/logs/2026-01-17/Users-Create.log | ForEach-Object {
    $log = $_ | ConvertFrom-Json
    if ($log.username -eq "admin") { $log }
}

# Filter by status code (errors)
Get-Content backend/logs/2026-01-17/Users-Create.log | ForEach-Object {
    $log = $_ | ConvertFrom-Json
    if ($log.statusCode -ge 400) { $log }
}

# Filter by time range
Get-Content backend/logs/2026-01-17/Users-Create.log | ForEach-Object {
    $log = $_ | ConvertFrom-Json
    $time = [DateTime]::Parse($log.timestamp)
    if ($time -ge "14:00" -and $time -le "15:00") { $log }
}
```

### Step 4: Analyze patterns

**Common analysis queries:**

```powershell
# 1. Find slow requests (> 500ms)
Get-Content backend/logs/2026-01-17/Users-GetList.log | ForEach-Object {
    $log = $_ | ConvertFrom-Json
    if ($log.elapsedMs -gt 500) {
        [PSCustomObject]@{
            Time = $log.timestamp
            User = $log.username
            Duration = "$($log.elapsedMs)ms"
            Parameters = $log.parameters
        }
    }
} | Format-Table -AutoSize

# 2. Find all errors
Get-Content backend/logs/2026-01-17/Users-Create.log | ForEach-Object {
    $log = $_ | ConvertFrom-Json
    if ($log.isException -eq 1 -or $log.statusCode -ge 400) {
        [PSCustomObject]@{
            Time = $log.timestamp
            User = $log.username
            Status = $log.statusCode
            Error = $log.errorMessage ?? $log.message
        }
    }
} | Format-Table -AutoSize

# 3. Count requests by user
Get-Content backend/logs/2026-01-17/Users-GetList.log | ForEach-Object {
    ($_ | ConvertFrom-Json).username
} | Group-Object | Select-Object Count, Name | Sort-Object Count -Descending

# 4. Average response time
$logs = Get-Content backend/logs/2026-01-17/Users-GetList.log | ForEach-Object {
    $_ | ConvertFrom-Json
}
$avgMs = ($logs | Measure-Object -Property elapsedMs -Average).Average
Write-Host "Average response time: $([math]::Round($avgMs, 2))ms"

# 5. Find validation errors
Get-Content backend/logs/2026-01-17/Users-Create.log | ForEach-Object {
    $log = $_ | ConvertFrom-Json
    if ($log.returnCode -eq 1) { # ValidationError
        [PSCustomObject]@{
            Time = $log.timestamp
            User = $log.username
            Parameters = $log.parameters
            Errors = $log.result
        }
    }
} | Format-List
```

## Common Issues and Solutions

### Issue 1: API returns 500 error

**Steps:**
1. Check error log file:
   ```powershell
   Get-Content backend/logs/2026-01-17/error-Users-Create.log -Tail 10
   ```

2. Look for `errorMessage` field:
   ```json
   {
     "isException": 1,
     "errorMessage": "Cannot insert duplicate key in object 'dbo.sy_users'",
     "statusCode": 500
   }
   ```

3. Identify root cause:
   - SQL error → Check database constraints
   - Null reference → Check parameter validation
   - Timeout → Check query performance

### Issue 2: Validation errors

**Steps:**
1. Check normal log file (not error):
   ```powershell
   Get-Content backend/logs/2026-01-17/Users-Create.log | ForEach-Object {
       $log = $_ | ConvertFrom-Json
       if ($log.returnCode -eq 1) { $log.result }
   }
   ```

2. Look at result field:
   ```json
   {
     "returnCode": 1,
     "errors": [
       "Username is required",
       "Email format is invalid"
     ],
     "result": null
   }
   ```

3. Check FluentValidation rules in `*Validator.cs`

### Issue 3: Slow performance

**Steps:**
1. Find slow requests:
   ```powershell
   Get-Content backend/logs/2026-01-17/Users-GetList.log | ForEach-Object {
       $log = $_ | ConvertFrom-Json
       if ($log.elapsedMs -gt 500) { $log }
   } | Sort-Object elapsedMs -Descending | Select-Object -First 10
   ```

2. Check parameters of slow requests:
   - Large `pageSize`?
   - Complex search `keyword`?
   - Missing database index?

3. Follow `/optimize-performance` workflow

### Issue 4: Authentication failures

**Steps:**
1. Check System logs:
   ```powershell
   Get-Content backend/logs/2026-01-17/System.log | ForEach-Object {
       $log = $_ | ConvertFrom-Json
       if ($log.message -like "*authentication*" -or $log.statusCode -eq 401) {
           $log
       }
   }
   ```

2. Look for:
   - Token expired
   - Invalid credentials
   - Missing Authorization header

### Issue 5: Find user activity

**Steps:**
```powershell
# Get all logs for specific user across all APIs
$date = "2026-01-17"
$username = "admin"

Get-ChildItem "backend/logs/$date" -Filter "*.log" -Exclude "error-*" | ForEach-Object {
    $apiName = $_.BaseName
    Get-Content $_.FullName | ForEach-Object {
        $log = $_ | ConvertFrom-Json
        if ($log.username -eq $username) {
            [PSCustomObject]@{
                API = $apiName
                Time = $log.timestamp
                Method = $log.method
                Path = $log.path
                Status = $log.statusCode
                Duration = "$($log.elapsedMs)ms"
            }
        }
    }
} | Sort-Object Time | Format-Table -AutoSize
```

## Log Analysis Workflow

### For Error Investigation

1. **Read error log first:**
   ```powershell
   Get-Content backend/logs/$(Get-Date -Format "yyyy-MM-dd")/error-{API}.log -Tail 20
   ```

2. **Get traceId from error log**

3. **Find related requests in normal log:**
   ```powershell
   $traceId = "abc123-def456"
   Get-Content backend/logs/$(Get-Date -Format "yyyy-MM-dd")/{API}.log | ForEach-Object {
       $log = $_ | ConvertFrom-Json
       if ($log.traceId -eq $traceId) { $log }
   }
   ```

4. **Analyze full request flow**

### For Performance Analysis

1. **Calculate statistics:**
   ```powershell
   $logs = Get-Content backend/logs/2026-01-17/Users-GetList.log | ForEach-Object {
       $_ | ConvertFrom-Json
   }
   
   $stats = $logs | Measure-Object -Property elapsedMs -Average -Minimum -Maximum
   Write-Host "Count: $($logs.Count)"
   Write-Host "Average: $([math]::Round($stats.Average, 2))ms"
   Write-Host "Min: $($stats.Minimum)ms"
   Write-Host "Max: $($stats.Maximum)ms"
   Write-Host "P95: $(($logs | Sort-Object elapsedMs)[0.95 * $logs.Count].elapsedMs)ms"
   ```

2. **Identify outliers (requests > p95)**

3. **Check parameters of slow requests**

4. **Follow optimization workflow**

## Quick Commands Reference

```powershell
# List today's log files
Get-ChildItem backend/logs/$(Get-Date -Format "yyyy-MM-dd")

# Read last 20 entries
Get-Content backend/logs/2026-01-17/{API}.log -Tail 20

# Count total requests
(Get-Content backend/logs/2026-01-17/{API}.log).Count

# Find errors (status >= 400)
Get-Content backend/logs/2026-01-17/{API}.log | ForEach-Object {
    $log = $_ | ConvertFrom-Json
    if ($log.statusCode -ge 400) { $log }
}

# Find exceptions
Get-Content backend/logs/2026-01-17/{API}.log | ForEach-Object {
    $log = $_ | ConvertFrom-Json
    if ($log.isException -eq 1) { $log }
}

# Search by keyword in any field
Get-Content backend/logs/2026-01-17/{API}.log | Where-Object { $_ -like "*keyword*" }

# Get logs between times
Get-Content backend/logs/2026-01-17/{API}.log | ForEach-Object {
    $log = $_ | ConvertFrom-Json
    $time = [DateTime]::Parse($log.timestamp)
    if ($time -ge "14:00" -and $time -le "15:00") { $log }
}
```

## ReturnCode Reference

From `CoreApiReturnCode`:
- `0`: Success
- `1`: ValidationError
- `2`: NotFound
- `3`: Forbidden
- `4`: ExceptionOccurred
- `5`: BadRequest
- `6`: Conflict
- `7`: Unauthorized

## Tips

1. **Always check error logs first** for exceptions
2. **Use traceId** to correlate related requests
3. **Filter by username** to track user-specific issues
4. **Look at elapsedMs** for performance issues
5. **Check parameters** to understand what user was trying to do
6. **Compare with successful requests** to see the difference
7. **Use time ranges** to focus on when issue occurred
8. **Group by returnCode** to see error distribution

## Integration with Workflows

- `/fix-bug` - Use logs to reproduce and diagnose bugs
- `/optimize-performance` - Use logs to identify slow queries
- `/security-audit` - Use logs to check for suspicious activity

## Summary

This skill provides comprehensive log analysis capabilities:
- Understand log structure and location
- Parse JSON log entries
- Filter and search logs efficiently
- Common analysis patterns (errors, performance, users)
- PowerShell commands for log investigation
- Integration with other workflows
