-- Script xóa các resource bị lỗi font và chèn lại với mã hóa đúng
-- UTF-8 encoding check

BEGIN TRANSACTION;

BEGIN TRY
    -- Xóa các resource bị lỗi font dựa trên ResourceKey và LanguageCode = 'vi-VN'
    DELETE FROM sy_resources 
    WHERE LanguageCode = 'vi-VN' 
    AND ResourceKey IN (
        'common.concurrencyError', 'lbl.status', 'lbl.employeeCode', 
        'lbl.role', 'lbl.roleCode', 'lbl.roleName', 
        'lbl.description', 'lbl.config', 'lbl.configValue', 
        'validation.invalidStatus'
    );

    -- Tạm thời xóa cả bản tiếng Anh để đảm bảo insert đồng bộ (hoặc chỉ insert nếu chưa có)
    -- Nhưng mục tiêu chính là sửa bản tiếng Việt

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

    -- Insert/Update cho vi-VN
    INSERT INTO sy_resources (ResourceKey, LanguageCode, SourceLanguage, ResourceValue, CreatedBy, CreatedAt)
    SELECT r.ResourceKey, 'vi-VN', 'en-US', r.ResourceValue_VI, 'SYSTEM', GETDATE()
    FROM @Resources r
    WHERE NOT EXISTS (
        SELECT 1 FROM sy_resources 
        WHERE ResourceKey = r.ResourceKey AND LanguageCode = 'vi-VN'
    );

    -- Đảm bảo en-US cũng có (nếu chưa có)
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
    DECLARE @ErrorMessage NVARCHAR(4000) = ERROR_MESSAGE();
    PRINT 'Error occurred: ' + @ErrorMessage;
END CATCH
