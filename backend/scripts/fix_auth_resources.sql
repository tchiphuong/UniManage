-- SQL Script to FIX corrupted resources for Auth module
-- This script UPDATES existing records with correct UTF-8 values

BEGIN TRANSACTION;

DECLARE @Now DATETIME = GETDATE();
DECLARE @User NVARCHAR(50) = 'SYSTEM';

-- Update labels (lbl.)
UPDATE sy_resources SET ResourceValue = N'Tên đăng nhập', UpdatedAt = @Now WHERE ResourceKey = 'lbl.username' AND LanguageCode = 'vi-VN';
UPDATE sy_resources SET ResourceValue = N'Mật khẩu', UpdatedAt = @Now WHERE ResourceKey = 'lbl.password' AND LanguageCode = 'vi-VN';
UPDATE sy_resources SET ResourceValue = N'Mật khẩu cũ', UpdatedAt = @Now WHERE ResourceKey = 'lbl.oldPassword' AND LanguageCode = 'vi-VN';
UPDATE sy_resources SET ResourceValue = N'Mật khẩu mới', UpdatedAt = @Now WHERE ResourceKey = 'lbl.newPassword' AND LanguageCode = 'vi-VN';
UPDATE sy_resources SET ResourceValue = N'Xác nhận mật khẩu', UpdatedAt = @Now WHERE ResourceKey = 'lbl.confirmPassword' AND LanguageCode = 'vi-VN';
UPDATE sy_resources SET ResourceValue = N'Mã khôi phục', UpdatedAt = @Now WHERE ResourceKey = 'lbl.resetToken' AND LanguageCode = 'vi-VN';
UPDATE sy_resources SET ResourceValue = N'Email hoặc tên đăng nhập', UpdatedAt = @Now WHERE ResourceKey = 'lbl.emailOrUsername' AND LanguageCode = 'vi-VN';
UPDATE sy_resources SET ResourceValue = N'Định danh người dùng', UpdatedAt = @Now WHERE ResourceKey = 'lbl.userIdentity' AND LanguageCode = 'vi-VN';

-- Update validation messages
UPDATE sy_resources SET ResourceValue = N'Mật khẩu phải chứa ít nhất một chữ hoa, một chữ thường, một số và một ký tự đặc biệt', UpdatedAt = @Now WHERE ResourceKey = 'validation.passwordComplexity' AND LanguageCode = 'vi-VN';
UPDATE sy_resources SET ResourceValue = N'Xác nhận mật khẩu không khớp với mật khẩu mới', UpdatedAt = @Now WHERE ResourceKey = 'validation.confirmPasswordMismatch' AND LanguageCode = 'vi-VN';
UPDATE sy_resources SET ResourceValue = N'Mật khẩu mới phải khác mật khẩu cũ', UpdatedAt = @Now WHERE ResourceKey = 'validation.newPasswordDifferent' AND LanguageCode = 'vi-VN';

COMMIT TRANSACTION;
GO
