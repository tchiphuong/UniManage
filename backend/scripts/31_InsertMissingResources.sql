-- Script bổ sung resource còn thiếu vào database UniManage
-- Default language: en-US, common: vi-VN

BEGIN TRANSACTION;

BEGIN TRY
    -- Danh sách các resource cần bổ sung
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

    -- Insert cho en-US (Default)
    INSERT INTO sy_resources (ResourceKey, LanguageCode, SourceLanguage, ResourceValue, CreatedBy, CreatedAt)
    SELECT r.ResourceKey, 'en-US', 'en-US', r.ResourceValue_EN, 'SYSTEM', GETDATE()
    FROM @Resources r
    WHERE NOT EXISTS (
        SELECT 1 FROM sy_resources 
        WHERE ResourceKey = r.ResourceKey AND LanguageCode = 'en-US'
    );

    -- Insert cho vi-VN
    INSERT INTO sy_resources (ResourceKey, LanguageCode, SourceLanguage, ResourceValue, CreatedBy, CreatedAt)
    SELECT r.ResourceKey, 'vi-VN', 'en-US', r.ResourceValue_VI, 'SYSTEM', GETDATE()
    FROM @Resources r
    WHERE NOT EXISTS (
        SELECT 1 FROM sy_resources 
        WHERE ResourceKey = r.ResourceKey AND LanguageCode = 'vi-VN'
    );

    COMMIT TRANSACTION;
    PRINT 'Inserted missing resources successfully.';
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0 ROLLBACK TRANSACTION;
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    PRINT 'Error occurred: ' + @ErrorMessage;
END CATCH
