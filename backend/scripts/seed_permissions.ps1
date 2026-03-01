Add-Type -AssemblyName System.Data

$connStr = "Data Source=TCPHUONG\SQLEXPRESS;Initial Catalog=UniManage;User ID=uni_manager;Password=uni_manager@2024;TrustServerCertificate=True;Connect Timeout=10"
$conn = New-Object System.Data.SqlClient.SqlConnection($connStr)

try {
    $conn.Open()
    $cmd = $conn.CreateCommand()

    # 1. Seed sy_actions (if empty)
    $cmd.CommandText = "SELECT COUNT(*) FROM sy_actions"
    $actCount = $cmd.ExecuteScalar()
    Write-Host "sy_actions: $actCount"

    if ($actCount -eq 0) {
        Write-Host "Seeding sy_actions..." -ForegroundColor Yellow
        $cmd.CommandText = @"
INSERT INTO sy_actions (Code, ResourceKey, IsActive, Sort, CreatedBy, CreatedAt)
VALUES
    ('VIEW',   'action.view',   1, 10, 'SYSTEM', SYSUTCDATETIME()),
    ('CREATE', 'action.create', 1, 20, 'SYSTEM', SYSUTCDATETIME()),
    ('UPDATE', 'action.update', 1, 30, 'SYSTEM', SYSUTCDATETIME()),
    ('DELETE', 'action.delete', 1, 40, 'SYSTEM', SYSUTCDATETIME()),
    ('EXPORT', 'action.export', 1, 50, 'SYSTEM', SYSUTCDATETIME()),
    ('IMPORT', 'action.import', 1, 60, 'SYSTEM', SYSUTCDATETIME())
"@
        $cmd.ExecuteNonQuery() | Out-Null
        Write-Host "  Inserted 6 actions" -ForegroundColor Green
    }

    # 2. Insert role_permissions (ADMIN -> all functions -> VIEW)
    Write-Host "`nGranting ADMIN all functions..." -ForegroundColor Yellow
    $functions = @('DASHBOARD','HR_EMPLOYEE','HR_DEPARTMENT','HR_POSITION','HR_ATTENDANCE','HR_SALARY','SA_CUSTOMER','SA_ORDER','IT_ITEM','SY_USER','SY_ROLE','SY_CONFIG')
    
    foreach ($func in $functions) {
        $cmd.CommandText = "SELECT COUNT(*) FROM sy_role_permissions WHERE RoleCode = 'ADMIN' AND FunctionCode = '$func' AND ActionCode = 'VIEW'"
        if ($cmd.ExecuteScalar() -eq 0) {
            $cmd.CommandText = "INSERT INTO sy_role_permissions (RoleCode, FunctionCode, ActionCode, CreatedBy, CreatedAt) VALUES ('ADMIN', '$func', 'VIEW', 'SYSTEM', SYSUTCDATETIME())"
            try {
                $cmd.ExecuteNonQuery() | Out-Null
                Write-Host "  ADMIN -> $func OK" -ForegroundColor Green
            }
            catch {
                Write-Host "  ADMIN -> $func FAILED: $($_.Exception.Message)" -ForegroundColor Red
            }
        }
    }

    # 3. Verify - test menu query
    Write-Host "`n=== Menu query for tcphuong ===" -ForegroundColor Cyan
    $cmd.CommandText = @"
SELECT DISTINCT m.Code, m.FunctionCode, m.ParentCode, m.ResourceKey, m.Icon
FROM sy_menus m
INNER JOIN sy_functions f ON m.FunctionCode = f.Code
INNER JOIN sy_role_permissions rp ON f.Code = rp.FunctionCode
INNER JOIN sy_user_roles ur ON rp.RoleCode = ur.RoleCode
WHERE ur.Username = 'tcphuong'
ORDER BY m.Id
"@
    $reader = $cmd.ExecuteReader()
    $count = 0
    while ($reader.Read()) {
        $count++
        $ic = if ($reader["Icon"] -is [DBNull]) { "-" } else { $reader["Icon"].ToString() }
        Write-Host "  $($reader['Code']) | $($reader['ResourceKey']) | $ic"
    }
    $reader.Close()
    Write-Host "`n  Menu items: $count" -ForegroundColor $(if ($count -gt 0) { "Green" } else { "Red" })

    # Summary
    Write-Host "`n=== SUMMARY ===" -ForegroundColor Cyan
    $cmd.CommandText = "SELECT COUNT(*) FROM sy_actions"
    Write-Host "  sy_actions: $($cmd.ExecuteScalar())"
    $cmd.CommandText = "SELECT COUNT(*) FROM sy_user_roles"
    Write-Host "  sy_user_roles: $($cmd.ExecuteScalar())"
    $cmd.CommandText = "SELECT COUNT(*) FROM sy_role_permissions"
    Write-Host "  sy_role_permissions: $($cmd.ExecuteScalar())"
    $cmd.CommandText = "SELECT COUNT(*) FROM sy_menus"
    Write-Host "  sy_menus: $($cmd.ExecuteScalar())"
    $cmd.CommandText = "SELECT COUNT(*) FROM sy_functions"
    Write-Host "  sy_functions: $($cmd.ExecuteScalar())"
}
catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
finally {
    $conn.Close()
    Write-Host "`nDone!"
}
