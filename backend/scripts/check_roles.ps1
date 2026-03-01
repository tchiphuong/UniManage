Add-Type -AssemblyName System.Data

$connStr = "Data Source=TCPHUONG\SQLEXPRESS;Initial Catalog=UniManage;User ID=uni_manager;Password=uni_manager@2024;TrustServerCertificate=True;Connect Timeout=10"
$conn = New-Object System.Data.SqlClient.SqlConnection($connStr)

try {
    $conn.Open()
    $cmd = $conn.CreateCommand()

    # sy_roles columns
    Write-Host "=== sy_roles COLUMNS ===" -ForegroundColor Cyan
    $cmd.CommandText = "SELECT COLUMN_NAME, DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'sy_roles' ORDER BY ORDINAL_POSITION"
    $reader = $cmd.ExecuteReader()
    while ($reader.Read()) { Write-Host "  $($reader['COLUMN_NAME']) ($($reader['DATA_TYPE']))" }
    $reader.Close()

    # sy_roles data
    Write-Host "`n=== sy_roles DATA ===" -ForegroundColor Cyan
    $cmd.CommandText = "SELECT * FROM sy_roles"
    $reader = $cmd.ExecuteReader()
    $cols = @()
    for ($i = 0; $i -lt $reader.FieldCount; $i++) { $cols += $reader.GetName($i) }
    Write-Host "  Columns: $($cols -join ', ')"
    while ($reader.Read()) {
        $vals = @()
        for ($i = 0; $i -lt $reader.FieldCount; $i++) {
            $v = if ($reader.IsDBNull($i)) { "NULL" } else { $reader.GetValue($i).ToString() }
            $vals += "$($cols[$i])=$v"
        }
        Write-Host "  $($vals -join ' | ')"
    }
    $reader.Close()

    # sy_user_roles data
    Write-Host "`n=== sy_user_roles ===" -ForegroundColor Cyan
    $cmd.CommandText = "SELECT * FROM sy_user_roles"
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
    if (-not $hasData) { Write-Host "  (empty)" }
    $reader.Close()

    # sy_role_permissions count and data
    Write-Host "`n=== sy_role_permissions ===" -ForegroundColor Cyan
    $cmd.CommandText = "SELECT COUNT(*) FROM sy_role_permissions"
    Write-Host "  Count: $($cmd.ExecuteScalar())"

    # sy_users
    Write-Host "`n=== sy_users ===" -ForegroundColor Cyan
    $cmd.CommandText = "SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'sy_users' ORDER BY ORDINAL_POSITION"
    $reader = $cmd.ExecuteReader()
    $userCols = @()
    while ($reader.Read()) { $userCols += $reader['COLUMN_NAME'].ToString() }
    $reader.Close()
    Write-Host "  Columns: $($userCols -join ', ')"

    $cmd.CommandText = "SELECT Username, Status FROM sy_users"
    $reader = $cmd.ExecuteReader()
    while ($reader.Read()) {
        Write-Host "  $($reader['Username']) | Status: $($reader['Status'])"
    }
    $reader.Close()
}
catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
finally {
    $conn.Close()
}
