-- Seed missing resource keys for API Responses in Auth Module
DECLARE @viVN NVARCHAR(10) = 'vi-VN';
DECLARE @enUS NVARCHAR(10) = 'en-US';

-- 1. auth.invalidLogin
IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'auth.invalidLogin' AND LanguageCode = @viVN)
INSERT INTO sy_resources (ResourceKey, ResourceValue, LanguageCode, SourceLanguage) 
VALUES ('auth.invalidLogin', N'Tên đăng nhập hoặc mật khẩu không đúng', @viVN, @viVN);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'auth.invalidLogin' AND LanguageCode = @enUS)
INSERT INTO sy_resources (ResourceKey, ResourceValue, LanguageCode, SourceLanguage) 
VALUES ('auth.invalidLogin', 'Invalid username or password', @enUS, @viVN);

-- 2. auth.userNotFound
IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'auth.userNotFound' AND LanguageCode = @viVN)
INSERT INTO sy_resources (ResourceKey, ResourceValue, LanguageCode, SourceLanguage) 
VALUES ('auth.userNotFound', N'Người dùng không tồn tại', @viVN, @viVN);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'auth.userNotFound' AND LanguageCode = @enUS)
INSERT INTO sy_resources (ResourceKey, ResourceValue, LanguageCode, SourceLanguage) 
VALUES ('auth.userNotFound', 'User not found', @enUS, @viVN);

-- 3. auth.oldPasswordIncorrect
IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'auth.oldPasswordIncorrect' AND LanguageCode = @viVN)
INSERT INTO sy_resources (ResourceKey, ResourceValue, LanguageCode, SourceLanguage) 
VALUES ('auth.oldPasswordIncorrect', N'Mật khẩu cũ không chính xác', @viVN, @viVN);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'auth.oldPasswordIncorrect' AND LanguageCode = @enUS)
INSERT INTO sy_resources (ResourceKey, ResourceValue, LanguageCode, SourceLanguage) 
VALUES ('auth.oldPasswordIncorrect', 'Old password is incorrect', @enUS, @viVN);

-- 4. auth.accountInactive
IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'auth.accountInactive' AND LanguageCode = @viVN)
INSERT INTO sy_resources (ResourceKey, ResourceValue, LanguageCode, SourceLanguage) 
VALUES ('auth.accountInactive', N'Tài khoản đang bị khóa hoặc không hoạt động', @viVN, @viVN);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'auth.accountInactive' AND LanguageCode = @enUS)
INSERT INTO sy_resources (ResourceKey, ResourceValue, LanguageCode, SourceLanguage) 
VALUES ('auth.accountInactive', 'Account is inactive or locked', @enUS, @viVN);

-- 5. auth.invalidResetToken
IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'auth.invalidResetToken' AND LanguageCode = @viVN)
INSERT INTO sy_resources (ResourceKey, ResourceValue, LanguageCode, SourceLanguage) 
VALUES ('auth.invalidResetToken', N'Mã khôi phục không hợp lệ hoặc đã hết hạn', @viVN, @viVN);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'auth.invalidResetToken' AND LanguageCode = @enUS)
INSERT INTO sy_resources (ResourceKey, ResourceValue, LanguageCode, SourceLanguage) 
VALUES ('auth.invalidResetToken', 'Invalid or expired reset token', @enUS, @viVN);

-- 6. auth.emailNotFound
IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'auth.emailNotFound' AND LanguageCode = @viVN)
INSERT INTO sy_resources (ResourceKey, ResourceValue, LanguageCode, SourceLanguage) 
VALUES ('auth.emailNotFound', N'Không tìm thấy email cho người dùng này', @viVN, @viVN);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'auth.emailNotFound' AND LanguageCode = @enUS)
INSERT INTO sy_resources (ResourceKey, ResourceValue, LanguageCode, SourceLanguage) 
VALUES ('auth.emailNotFound', 'Email not found for this user', @enUS, @viVN);

-- 7. auth.accountLocked
IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'auth.accountLocked' AND LanguageCode = @viVN)
INSERT INTO sy_resources (ResourceKey, ResourceValue, LanguageCode, SourceLanguage) 
VALUES ('auth.accountLocked', N'Tài khoản đã bị tạm khóa do nhập sai nhiều lần. Vui lòng thử lại sau 30 phút.', @viVN, @viVN);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'auth.accountLocked' AND LanguageCode = @enUS)
INSERT INTO sy_resources (ResourceKey, ResourceValue, LanguageCode, SourceLanguage) 
VALUES ('auth.accountLocked', 'Account is temporarily locked due to many failed attempts. Please try again after 30 minutes.', @enUS, @viVN);
