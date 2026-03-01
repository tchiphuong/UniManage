Add-Type -AssemblyName System.Data

$connStr = "Data Source=TCPHUONG\SQLEXPRESS;Initial Catalog=UniManage;User ID=uni_manager;Password=uni_manager@2024;TrustServerCertificate=True;Connect Timeout=10"
$conn = New-Object System.Data.SqlClient.SqlConnection($connStr)

try {
    $conn.Open()
    Write-Host "Connected OK" -ForegroundColor Green
    
    $cmd = $conn.CreateCommand()

    # ========================================
    # STEP 1: Seed sy_functions (if empty)
    # GroupCode is self-referencing FK to sy_functions.Code
    # Must insert parent groups FIRST, then children
    # ========================================
    $cmd.CommandText = "SELECT COUNT(*) FROM sy_functions"
    $funcCount = $cmd.ExecuteScalar()
    Write-Host "sy_functions count: $funcCount"

    if ($funcCount -eq 0) {
        Write-Host "`nSeeding sy_functions..." -ForegroundColor Yellow

        # Step 1a: Insert GROUP records first (GroupCode references self)
        $cmd.CommandText = @"
INSERT INTO sy_functions (Code, ResourceKey, GroupCode, IsActive, CreatedBy, CreatedAt)
VALUES
    ('MAIN',      'func.group.main',      'MAIN',      1, 'SYSTEM', SYSUTCDATETIME()),
    ('HR',        'func.group.hr',        'HR',        1, 'SYSTEM', SYSUTCDATETIME()),
    ('SALES',     'func.group.sales',     'SALES',     1, 'SYSTEM', SYSUTCDATETIME()),
    ('INVENTORY', 'func.group.inventory', 'INVENTORY', 1, 'SYSTEM', SYSUTCDATETIME()),
    ('SYSTEM',    'func.group.system',    'SYSTEM',    1, 'SYSTEM', SYSUTCDATETIME())
"@
        $cmd.ExecuteNonQuery() | Out-Null
        Write-Host "  Inserted 5 group functions" -ForegroundColor Green

        # Step 1b: Insert child functions (GroupCode references parent)
        $cmd.CommandText = @"
INSERT INTO sy_functions (Code, ResourceKey, GroupCode, IsActive, CreatedBy, CreatedAt)
VALUES
    ('DASHBOARD',     'func.dashboard',      'MAIN',      1, 'SYSTEM', SYSUTCDATETIME()),
    ('HR_EMPLOYEE',   'func.hr.employee',    'HR',        1, 'SYSTEM', SYSUTCDATETIME()),
    ('HR_DEPARTMENT', 'func.hr.department',  'HR',        1, 'SYSTEM', SYSUTCDATETIME()),
    ('HR_POSITION',   'func.hr.position',    'HR',        1, 'SYSTEM', SYSUTCDATETIME()),
    ('HR_ATTENDANCE', 'func.hr.attendance',  'HR',        1, 'SYSTEM', SYSUTCDATETIME()),
    ('HR_SALARY',     'func.hr.salary',      'HR',        1, 'SYSTEM', SYSUTCDATETIME()),
    ('SA_CUSTOMER',   'func.sa.customer',    'SALES',     1, 'SYSTEM', SYSUTCDATETIME()),
    ('SA_ORDER',      'func.sa.order',       'SALES',     1, 'SYSTEM', SYSUTCDATETIME()),
    ('IT_ITEM',       'func.it.item',        'INVENTORY', 1, 'SYSTEM', SYSUTCDATETIME()),
    ('SY_USER',       'func.sy.user',        'SYSTEM',    1, 'SYSTEM', SYSUTCDATETIME()),
    ('SY_ROLE',       'func.sy.role',        'SYSTEM',    1, 'SYSTEM', SYSUTCDATETIME()),
    ('SY_CONFIG',     'func.sy.config',      'SYSTEM',    1, 'SYSTEM', SYSUTCDATETIME())
"@
        $cmd.ExecuteNonQuery() | Out-Null
        Write-Host "  Inserted 12 child functions" -ForegroundColor Green
    }

    # ========================================
    # STEP 2: Clear & reseed sy_menus
    # ========================================
    $cmd.CommandText = "DELETE FROM sy_menus WHERE ParentCode IS NOT NULL"
    $cmd.ExecuteNonQuery() | Out-Null
    $cmd.CommandText = "DELETE FROM sy_menus WHERE ParentCode IS NULL"
    $cmd.ExecuteNonQuery() | Out-Null
    $cmd.CommandText = "DBCC CHECKIDENT ('sy_menus', RESEED, 0)"
    $cmd.ExecuteNonQuery() | Out-Null
    Write-Host "`nCleared sy_menus" -ForegroundColor Yellow

    # ========================================
    # STEP 3: INSERT root groups (no FunctionCode)
    # ========================================
    $cmd.CommandText = @"
INSERT INTO sy_menus (Code, FunctionCode, ParentCode, ResourceKey, Url, Icon, CreatedBy, CreatedAt)
VALUES
    ('GRP_MAIN',      NULL, NULL, 'menu.group.main',      NULL, NULL, 'SYSTEM', SYSUTCDATETIME()),
    ('GRP_HR',        NULL, NULL, 'menu.group.hr',        NULL, NULL, 'SYSTEM', SYSUTCDATETIME()),
    ('GRP_SALES',     NULL, NULL, 'menu.group.sales',     NULL, NULL, 'SYSTEM', SYSUTCDATETIME()),
    ('GRP_INVENTORY', NULL, NULL, 'menu.group.inventory', NULL, NULL, 'SYSTEM', SYSUTCDATETIME()),
    ('GRP_SYSTEM',    NULL, NULL, 'menu.group.system',    NULL, NULL, 'SYSTEM', SYSUTCDATETIME())
"@
    $cmd.ExecuteNonQuery() | Out-Null
    Write-Host "Inserted 5 root groups" -ForegroundColor Green

    # MAIN items
    $cmd.CommandText = @"
INSERT INTO sy_menus (Code, FunctionCode, ParentCode, ResourceKey, Url, Icon, CreatedBy, CreatedAt)
VALUES ('MNU_DASHBOARD', 'DASHBOARD', 'GRP_MAIN', 'menu.dashboard', '/dashboard', 'fa-tachometer-alt', 'SYSTEM', SYSUTCDATETIME())
"@
    $cmd.ExecuteNonQuery() | Out-Null

    # HR items
    $cmd.CommandText = @"
INSERT INTO sy_menus (Code, FunctionCode, ParentCode, ResourceKey, Url, Icon, CreatedBy, CreatedAt)
VALUES
    ('MNU_EMPLOYEES',   'HR_EMPLOYEE',   'GRP_HR', 'menu.employees',   '/dashboard/hr/employees',   'fa-users',      'SYSTEM', SYSUTCDATETIME()),
    ('MNU_DEPARTMENTS', 'HR_DEPARTMENT', 'GRP_HR', 'menu.departments', '/dashboard/hr/departments', 'fa-user-group', 'SYSTEM', SYSUTCDATETIME()),
    ('MNU_POSITIONS',   'HR_POSITION',   'GRP_HR', 'menu.positions',   '/dashboard/hr/positions',   'fa-briefcase',  'SYSTEM', SYSUTCDATETIME()),
    ('MNU_ATTENDANCE',  'HR_ATTENDANCE', 'GRP_HR', 'menu.attendance',  '/dashboard/hr/attendance',  'fa-clock',      'SYSTEM', SYSUTCDATETIME()),
    ('MNU_SALARIES',    'HR_SALARY',     'GRP_HR', 'menu.salaries',    '/dashboard/hr/salaries',    'fa-banknotes',  'SYSTEM', SYSUTCDATETIME())
"@
    $cmd.ExecuteNonQuery() | Out-Null

    # SALES items
    $cmd.CommandText = @"
INSERT INTO sy_menus (Code, FunctionCode, ParentCode, ResourceKey, Url, Icon, CreatedBy, CreatedAt)
VALUES
    ('MNU_CUSTOMERS', 'SA_CUSTOMER', 'GRP_SALES', 'menu.customers', '/dashboard/sales/customers', 'fa-heart',        'SYSTEM', SYSUTCDATETIME()),
    ('MNU_ORDERS',    'SA_ORDER',    'GRP_SALES', 'menu.orders',    '/dashboard/sales/orders',    'fa-shopping-cart', 'SYSTEM', SYSUTCDATETIME())
"@
    $cmd.ExecuteNonQuery() | Out-Null

    # INVENTORY items
    $cmd.CommandText = @"
INSERT INTO sy_menus (Code, FunctionCode, ParentCode, ResourceKey, Url, Icon, CreatedBy, CreatedAt)
VALUES ('MNU_ITEMS', 'IT_ITEM', 'GRP_INVENTORY', 'menu.items', '/dashboard/inventory/items', 'fa-box', 'SYSTEM', SYSUTCDATETIME())
"@
    $cmd.ExecuteNonQuery() | Out-Null

    # SYSTEM items
    $cmd.CommandText = @"
INSERT INTO sy_menus (Code, FunctionCode, ParentCode, ResourceKey, Url, Icon, CreatedBy, CreatedAt)
VALUES
    ('MNU_USERS',    'SY_USER',   'GRP_SYSTEM', 'menu.users',    '/dashboard/system/users',    'fa-user-circle',  'SYSTEM', SYSUTCDATETIME()),
    ('MNU_ROLES',    'SY_ROLE',   'GRP_SYSTEM', 'menu.roles',    '/dashboard/system/roles',    'fa-check-square', 'SYSTEM', SYSUTCDATETIME()),
    ('MNU_SETTINGS', 'SY_CONFIG', 'GRP_SYSTEM', 'menu.settings', '/dashboard/system/settings', 'fa-cogs',         'SYSTEM', SYSUTCDATETIME())
"@
    $cmd.ExecuteNonQuery() | Out-Null
    Write-Host "Inserted 12 menu items" -ForegroundColor Green

    # ========================================
    # VERIFY
    # ========================================
    Write-Host "`n=== sy_menus RESULT ===" -ForegroundColor Cyan
    $cmd.CommandText = @"
SELECT m.Id, m.Code, m.FunctionCode, m.ParentCode, m.ResourceKey, m.Icon,
    CASE WHEN m.ParentCode IS NULL THEN 'GROUP' ELSE 'ITEM' END AS [Type],
    CASE 
        WHEN m.FunctionCode IS NULL THEN '-'
        WHEN EXISTS(SELECT 1 FROM sy_functions f WHERE f.Code = m.FunctionCode) THEN 'OK'
        ELSE 'MISS' 
    END AS [FK]
FROM sy_menus m ORDER BY m.Id
"@
    $reader = $cmd.ExecuteReader()
    Write-Host ("{0,-4} {1,-18} {2,-16} {3,-16} {4,-25} {5,-18} {6,-6} {7}" -f "Id", "Code", "FuncCode", "Parent", "ResourceKey", "Icon", "Type", "FK")
    Write-Host ("-" * 108)
    while ($reader.Read()) {
        $fc = if ($reader["FunctionCode"] -is [DBNull]) { "-" } else { $reader["FunctionCode"].ToString() }
        $pc = if ($reader["ParentCode"] -is [DBNull]) { "-" } else { $reader["ParentCode"].ToString() }
        $ic = if ($reader["Icon"] -is [DBNull]) { "-" } else { $reader["Icon"].ToString() }
        Write-Host ("{0,-4} {1,-18} {2,-16} {3,-16} {4,-25} {5,-18} {6,-6} {7}" -f $reader["Id"], $reader["Code"], $fc, $pc, $reader["ResourceKey"], $ic, $reader["Type"], $reader["FK"])
    }
    $reader.Close()

    $cmd.CommandText = "SELECT COUNT(*) FROM sy_menus"
    $m = $cmd.ExecuteScalar()
    $cmd.CommandText = "SELECT COUNT(*) FROM sy_functions"
    $f = $cmd.ExecuteScalar()
    Write-Host "`nTotal: $m menus, $f functions" -ForegroundColor Green
}
catch {
    Write-Host "`nERROR: $($_.Exception.Message)" -ForegroundColor Red
}
finally {
    $conn.Close()
    Write-Host "Done!"
}
