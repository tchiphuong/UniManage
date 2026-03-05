-- SQL Script to seed missing resources for Auth module
-- Fixed version with SourceLanguage column

BEGIN TRANSACTION;

DECLARE @Now DATETIME = GETDATE();
DECLARE @User NVARCHAR(50) = 'SYSTEM';

-- 1. Insert/Update labels (lbl.)
IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.username' AND LanguageCode = 'vi-VN')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.username', N'Tên đăng nhập', 'en-US', 'vi-VN', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.username' AND LanguageCode = 'en-US')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.username', 'Username', 'en-US', 'en-US', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.password' AND LanguageCode = 'vi-VN')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.password', N'Mật khẩu', 'en-US', 'vi-VN', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.password' AND LanguageCode = 'en-US')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.password', 'Password', 'en-US', 'en-US', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.oldPassword' AND LanguageCode = 'vi-VN')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.oldPassword', N'Mật khẩu cũ', 'en-US', 'vi-VN', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.oldPassword' AND LanguageCode = 'en-US')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.oldPassword', 'Old password', 'en-US', 'en-US', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.newPassword' AND LanguageCode = 'vi-VN')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.newPassword', N'Mật khẩu mới', 'en-US', 'vi-VN', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.newPassword' AND LanguageCode = 'en-US')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.newPassword', 'New password', 'en-US', 'en-US', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.confirmPassword' AND LanguageCode = 'vi-VN')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.confirmPassword', N'Xác nhận mật khẩu', 'en-US', 'vi-VN', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.confirmPassword' AND LanguageCode = 'en-US')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.confirmPassword', 'Confirm password', 'en-US', 'en-US', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.resetToken' AND LanguageCode = 'vi-VN')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.resetToken', N'Mã khôi phục', 'en-US', 'vi-VN', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.resetToken' AND LanguageCode = 'en-US')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.resetToken', 'Reset token', 'en-US', 'en-US', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.emailOrUsername' AND LanguageCode = 'vi-VN')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.emailOrUsername', N'Email hoặc tên đăng nhập', 'en-US', 'vi-VN', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.emailOrUsername' AND LanguageCode = 'en-US')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.emailOrUsername', 'Email or Username', 'en-US', 'en-US', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.userIdentity' AND LanguageCode = 'vi-VN')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.userIdentity', N'Định danh người dùng', 'en-US', 'vi-VN', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.userIdentity' AND LanguageCode = 'en-US')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.userIdentity', 'User identity', 'en-US', 'en-US', @User, @Now, @User, @Now);

-- 2. Insert/Update custom validation messages (validation.)
IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'validation.passwordComplexity' AND LanguageCode = 'vi-VN')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('validation.passwordComplexity', N'Mật khẩu phải chứa ít nhất một chữ hoa, một chữ thường, một số và một ký tự đặc biệt', 'en-US', 'vi-VN', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'validation.passwordComplexity' AND LanguageCode = 'en-US')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('validation.passwordComplexity', 'Password must contain at least one uppercase, one lowercase, one digit, and one special character', 'en-US', 'en-US', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'validation.confirmPasswordMismatch' AND LanguageCode = 'vi-VN')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('validation.confirmPasswordMismatch', N'Xác nhận mật khẩu không khớp với mật khẩu mới', 'en-US', 'vi-VN', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'validation.confirmPasswordMismatch' AND LanguageCode = 'en-US')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('validation.confirmPasswordMismatch', 'Confirm password does not match new password', 'en-US', 'en-US', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'validation.newPasswordDifferent' AND LanguageCode = 'vi-VN')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('validation.newPasswordDifferent', N'Mật khẩu mới phải khác mật khẩu cũ', 'en-US', 'vi-VN', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'validation.newPasswordDifferent' AND LanguageCode = 'en-US')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('validation.newPasswordDifferent', 'New password must be different from old password', 'en-US', 'en-US', @User, @Now, @User, @Now);

COMMIT TRANSACTION;
GO
