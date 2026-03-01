Add-Type -AssemblyName System.Data

$connStr = "Data Source=TCPHUONG\SQLEXPRESS;Initial Catalog=UniManage;User ID=uni_manager;Password=uni_manager@2024;TrustServerCertificate=True;Connect Timeout=10"
$conn = New-Object System.Data.SqlClient.SqlConnection($connStr)

try {
    $conn.Open()
    $cmd = $conn.CreateCommand()

    Write-Host "=== Seeding sy_function_mapping ===" -ForegroundColor Cyan

    # Available actions
    $cmd.CommandText = "SELECT Code FROM sy_actions WHERE IsActive = 1 ORDER BY Sort"
    $reader = $cmd.ExecuteReader()
    $actions = @()
    while ($reader.Read()) { $actions += $reader['Code'].ToString() }
    $reader.Close()
    Write-Host "Actions: $($actions -join ', ')"

    # All child functions (not group functions)
    $childFunctions = @('DASHBOARD','HR_EMPLOYEE','HR_DEPARTMENT','HR_POSITION','HR_ATTENDANCE','HR_SALARY','SA_CUSTOMER','SA_ORDER','IT_ITEM','SY_USER','SY_ROLE','SY_CONFIG')

    # Define which actions each function supports
    # Dashboard: VIEW only
    # CRUD modules: VIEW, CREATE, UPDATE, DELETE
    # System modules: VIEW, CREATE, UPDATE, DELETE, EXPORT, IMPORT
    $functionActions = @{
        'DASHBOARD'     = @('VIEW')
        'HR_EMPLOYEE'   = @('VIEW','CREATE','UPDATE','DELETE','EXPORT','IMPORT')
        'HR_DEPARTMENT' = @('VIEW','CREATE','UPDATE','DELETE')
        'HR_POSITION'   = @('VIEW','CREATE','UPDATE','DELETE')
        'HR_ATTENDANCE' = @('VIEW','CREATE','UPDATE','DELETE','EXPORT')
        'HR_SALARY'     = @('VIEW','CREATE','UPDATE','DELETE','EXPORT')
        'SA_CUSTOMER'   = @('VIEW','CREATE','UPDATE','DELETE','EXPORT')
        'SA_ORDER'      = @('VIEW','CREATE','UPDATE','DELETE','EXPORT')
        'IT_ITEM'       = @('VIEW','CREATE','UPDATE','DELETE','EXPORT','IMPORT')
        'SY_USER'       = @('VIEW','CREATE','UPDATE','DELETE')
        'SY_ROLE'       = @('VIEW','CREATE','UPDATE','DELETE')
        'SY_CONFIG'     = @('VIEW','UPDATE')
    }

    $insertCount = 0
    $skipCount = 0

    foreach ($func in $childFunctions) {
        $actionsForFunc = $functionActions[$func]
        foreach ($action in $actionsForFunc) {
            $cmd.CommandText = "SELECT COUNT(*) FROM sy_function_mapping WHERE FunctionCode = '$func' AND ActionCode = '$action'"
            if ($cmd.ExecuteScalar() -eq 0) {
                $cmd.CommandText = "INSERT INTO sy_function_mapping (ActionCode, FunctionCode, CreatedBy, CreatedAt) VALUES ('$action', '$func', 'SYSTEM', GETDATE())"
                try {
                    $cmd.ExecuteNonQuery() | Out-Null
                    $insertCount++
                }
                catch {
                    Write-Host "  FAILED: $func -> $action : $($_.Exception.Message)" -ForegroundColor Red
                }
            } else {
                $skipCount++
            }
        }
    }

    Write-Host "`nInserted: $insertCount, Skipped: $skipCount" -ForegroundColor Green

    # Verify
    Write-Host "`n=== RESULT ===" -ForegroundColor Cyan
    $cmd.CommandText = @"
SELECT fm.FunctionCode, fm.ActionCode
FROM sy_function_mapping fm
ORDER BY fm.FunctionCode, fm.ActionCode
"@
    $reader = $cmd.ExecuteReader()
    $currentFunc = ""
    while ($reader.Read()) {
        $func = $reader['FunctionCode'].ToString()
        $action = $reader['ActionCode'].ToString()
        if ($func -ne $currentFunc) {
            $currentFunc = $func
            Write-Host ""
            Write-Host -NoNewline "  $func : " -ForegroundColor Yellow
        }
        Write-Host -NoNewline "$action " -ForegroundColor Green
    }
    $reader.Close()

    $cmd.CommandText = "SELECT COUNT(*) FROM sy_function_mapping"
    Write-Host "`n`n  Total: $($cmd.ExecuteScalar()) mappings" -ForegroundColor Cyan
}
catch {
    Write-Host "ERROR: $($_.Exception.Message)" -ForegroundColor Red
}
finally {
    $conn.Close()
    Write-Host "`nDone!"
}
