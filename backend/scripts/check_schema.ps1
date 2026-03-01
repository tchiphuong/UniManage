Add-Type -AssemblyName System.Data

$connStr = "Data Source=TCPHUONG\SQLEXPRESS;Initial Catalog=UniManage;User ID=uni_manager;Password=uni_manager@2024;TrustServerCertificate=True;Connect Timeout=10"
$conn = New-Object System.Data.SqlClient.SqlConnection($connStr)

try {
    $conn.Open()
    $cmd = $conn.CreateCommand()

    # User info
    Write-Host "=== admin user ===" -ForegroundColor Cyan
    $cmd.CommandText = "SELECT Id, Username, EmployeeCode, RoleCode, Status, Email FROM sy_users WHERE Username = 'admin'"
    $reader = $cmd.ExecuteReader()
    while ($reader.Read()) {
        for ($i = 0; $i -lt $reader.FieldCount; $i++) {
            $v = if ($reader.IsDBNull($i)) { "NULL" } else { $reader.GetValue($i).ToString() }
            Write-Host "  $($reader.GetName($i)): $v"
        }
    }
    $reader.Close()

    # User roles
    Write-Host "`n=== sy_user_roles (admin) ===" -ForegroundColor Cyan
    $cmd.CommandText = "SELECT * FROM sy_user_roles WHERE Username = 'admin'"
    $reader = $cmd.ExecuteReader()
    $hasData = $false
    while ($reader.Read()) {
        $hasData = $true
        $vals = @()
        for ($i = 0; $i -lt $reader.FieldCount; $i++) {
            $v = if ($reader.IsDBNull($i)) { "NULL" } else { $reader.GetValue($i).ToString() }
            $vals += "$($reader.GetName($i))=$v"
        }
        Write-Host "  $($vals -join ' | ')"
    }
    if (-not $hasData) { Write-Host "  (empty)" -ForegroundColor Red }
    $reader.Close()

    # Menu query test
    Write-Host "`n=== Menu query for admin ===" -ForegroundColor Cyan
    $cmd.CommandText = @"
SELECT m.Code, m.FunctionCode, m.ParentCode, m.ResourceKey, m.Url
FROM sy_menus m
INNER JOIN sy_functions f ON m.FunctionCode = f.Code
INNER JOIN sy_role_permissions rp ON f.Code = rp.FunctionCode
INNER JOIN sy_user_roles ur ON rp.RoleCode = ur.RoleCode
WHERE ur.Username = 'admin'
ORDER BY m.Id
"@
    $reader = $cmd.ExecuteReader()
    $count = 0
    while ($reader.Read()) {
        $count++
        $pc = if ($reader["ParentCode"] -is [DBNull]) { "-" } else { $reader["ParentCode"].ToString() }
        $url = if ($reader["Url"] -is [DBNull]) { "-" } else { $reader["Url"].ToString() }
        Write-Host "  $($reader['Code']) | $pc | $($reader['ResourceKey']) | $url"
    }
    $reader.Close()
    Write-Host "`n  Total: $count items" -ForegroundColor $(if ($count -gt 0) { "Green" } else { "Red" })
}
catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
finally {
    $conn.Close()
}
