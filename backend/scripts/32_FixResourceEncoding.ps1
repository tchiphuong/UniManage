# PowerShell script to fix resource encoding in UniManage database

$connectionString = "Server=TCPHUONG\SQLEXPRESS;Database=UniManage;User Id=uni_manager;Password=uni_manager@2024;TrustServerCertificate=True;"
$query = @"
BEGIN TRANSACTION;
BEGIN TRY
    -- Delete old corrupted records
    DELETE FROM sy_resources 
    WHERE LanguageCode = 'vi-VN' 
    AND ResourceKey IN (
        'common.concurrencyError', 'lbl.status', 'lbl.employeeCode', 
        'lbl.role', 'lbl.roleCode', 'lbl.roleName', 
        'lbl.description', 'lbl.config', 'lbl.configValue', 
        'validation.invalidStatus'
    );

    -- Resources to insert
    -- ResourceKey, Value_EN, Value_VI
    DECLARE @Resources TABLE (
        ResourceKey NVARCHAR(255),
        ResourceValue_EN NVARCHAR(MAX),
        ResourceValue_VI NVARCHAR(MAX)
    );

    INSERT INTO @Resources (ResourceKey, ResourceValue_EN, ResourceValue_VI)
    VALUES 
    ('common.concurrencyError', 'Data has been modified by another user. Please reload the data and try again.', N'Dữ liệu đã được thay đổi bởi người dùng khác. Vui lòng tải lại trang và thử lại.'),
    ('lbl.status', 'Status', N'Trạng thái'),
    ('lbl.employeeCode', 'Employee code', N'Mã nhân viên'),
    ('lbl.role', 'Role', N'Vai trò'),
    ('lbl.roleCode', 'Role code', N'Mã vai trò'),
    ('lbl.roleName', 'Role name', N'Tên vai trò'),
    ('lbl.description', 'Description', N'Mô tả'),
    ('lbl.config', 'Config', N'Cấu hình'),
    ('lbl.configValue', 'Config value', N'Giá trị cấu hình'),
    ('validation.invalidStatus', 'Invalid status', N'Trạng thái không hợp lệ');

    -- Insert vi-VN
    INSERT INTO sy_resources (ResourceKey, LanguageCode, SourceLanguage, ResourceValue, CreatedBy, CreatedAt)
    SELECT r.ResourceKey, 'vi-VN', 'en-US', r.ResourceValue_VI, 'SYSTEM', GETDATE()
    FROM @Resources r
    WHERE NOT EXISTS (
        SELECT 1 FROM sy_resources 
        WHERE ResourceKey = r.ResourceKey AND LanguageCode = 'vi-VN'
    );

    -- Insert en-US
    INSERT INTO sy_resources (ResourceKey, LanguageCode, SourceLanguage, ResourceValue, CreatedBy, CreatedAt)
    SELECT r.ResourceKey, 'en-US', 'en-US', r.ResourceValue_EN, 'SYSTEM', GETDATE()
    FROM @Resources r
    WHERE NOT EXISTS (
        SELECT 1 FROM sy_resources 
        WHERE ResourceKey = r.ResourceKey AND LanguageCode = 'en-US'
    );

    COMMIT TRANSACTION;
    PRINT 'Fixed and inserted resources successfully.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    THROW;
END CATCH
"@

try {
    $connection = New-Object System.Data.SqlClient.SqlConnection($connectionString)
    $command = $connection.CreateCommand()
    $command.CommandText = $query
    $connection.Open()
    $command.ExecuteNonQuery()
    $connection.Close()
    Write-Host "Success: Database resources updated with correct encoding."
} catch {
    Write-Error "Failed to update database: $($_.Exception.Message)"
    exit 1
}
