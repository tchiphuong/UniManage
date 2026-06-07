-- 1. Thêm key common_unauthorized vào bảng sy_resources
IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'common_unauthorized' AND LanguageCode = 'vi-VN')
BEGIN
    INSERT INTO [dbo].[sy_resources]
           ([ResourceKey], [ResourceValue], [SourceLanguage], [LanguageCode], [CreatedBy], [CreatedAt])
     VALUES
           ('common_unauthorized', N'Bạn không có quyền truy cập vào tài nguyên này.', 'vi-VN', 'vi-VN', 'system', GETDATE())
END

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'common_unauthorized' AND LanguageCode = 'en-US')
BEGIN
    INSERT INTO [dbo].[sy_resources]
           ([ResourceKey], [ResourceValue], [SourceLanguage], [LanguageCode], [CreatedBy], [CreatedAt])
     VALUES
           ('common_unauthorized', N'You are not authorized to access this resource.', 'vi-VN', 'en-US', 'system', GETDATE())
END

-- 2. Thêm các thông số Company Settings cơ bản vào sy_configs
DECLARE @ConfigKeys TABLE (Code VARCHAR(50), Name NVARCHAR(255), Value NVARCHAR(MAX));

INSERT INTO @ConfigKeys (Code, Name, Value) VALUES 
('COMPANY_NAME', N'Tên công ty', N'UniManage JSC'),
('COMPANY_ADDRESS', N'Địa chỉ công ty', N'123 Đường Công Nghệ, Quận 1, TP.HCM'),
('COMPANY_PHONE', N'Số điện thoại', N'0123456789'),
('COMPANY_EMAIL', N'Email liên hệ', N'contact@unimanage.local'),
('COMPANY_TAX_CODE', N'Mã số thuế', N'0123456789'),
('COMPANY_LOGO', N'Logo công ty', N'/assets/images/logo.png');

DECLARE @Code VARCHAR(50), @Name NVARCHAR(255), @Value NVARCHAR(MAX);
DECLARE db_cursor CURSOR FOR SELECT Code, Name, Value FROM @ConfigKeys;

OPEN db_cursor;
FETCH NEXT FROM db_cursor INTO @Code, @Name, @Value;

WHILE @@FETCH_STATUS = 0  
BEGIN  
    IF NOT EXISTS (SELECT 1 FROM sy_configs WHERE ConfigCode = @Code)
    BEGIN
        INSERT INTO sy_configs (
            Uuid, ConfigCode, ConfigName, ConfigValue, DataType, IsSystem, 
            CreatedBy, CreatedAt
        )
        VALUES (
            NEWID(), @Code, @Name, @Value, 'STRING', 0, 
            'system', GETDATE()
        );
    END
    FETCH NEXT FROM db_cursor INTO @Code, @Name, @Value;
END;

CLOSE db_cursor;  
DEALLOCATE db_cursor;  
