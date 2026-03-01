Add-Type -AssemblyName System.Data

$connStr = "Data Source=TCPHUONG\SQLEXPRESS;Initial Catalog=UniManage;User ID=uni_manager;Password=uni_manager@2024;TrustServerCertificate=True;Connect Timeout=10"
$conn = New-Object System.Data.SqlClient.SqlConnection($connStr)

try {
    $conn.Open()
    $cmd = $conn.CreateCommand()

    # Same query as GetMenuListQuery handler (without DISTINCT ORDER BY conflict)
    $cmd.CommandText = @"
SELECT m.Id, m.Code, m.FunctionCode, m.ParentCode, m.ResourceKey, m.Url, m.Icon
FROM sy_menus m
INNER JOIN sy_functions f ON m.FunctionCode = f.Code
INNER JOIN sy_role_permissions rp ON f.Code = rp.FunctionCode
INNER JOIN sy_user_roles ur ON rp.RoleCode = ur.RoleCode
WHERE ur.Username = 'tcphuong'
ORDER BY m.Id
"@
    $reader = $cmd.ExecuteReader()
    Write-Host "=== Menu items for tcphuong ===" -ForegroundColor Cyan
    $count = 0
    while ($reader.Read()) {
        $count++
        $pc = if ($reader["ParentCode"] -is [DBNull]) { "-" } else { $reader["ParentCode"].ToString() }
        $ic = if ($reader["Icon"] -is [DBNull]) { "-" } else { $reader["Icon"].ToString() }
        $url = if ($reader["Url"] -is [DBNull]) { "-" } else { $reader["Url"].ToString() }
        Write-Host ("  {0,-18} {1,-16} {2,-25} {3,-30} {4}" -f $reader['Code'], $pc, $reader['ResourceKey'], $url, $ic)
    }
    $reader.Close()
    Write-Host "`n  Total: $count menu items" -ForegroundColor Green
    Write-Host "  (Root groups will be auto-fetched by handler's CTE query)" -ForegroundColor Gray
}
catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
finally {
    $conn.Close()
}
