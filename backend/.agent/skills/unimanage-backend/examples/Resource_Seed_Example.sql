USE [UniManage]
GO

-- =============================================
-- VÍ DỤ MẪU SEED RESOURCE CHUẨN (UniManage)
-- =============================================

-- Bước 1: Xóa nếu đã tồn tại để tránh Duplicate
DELETE FROM [dbo].[sy_resources] WHERE [ResourceKey] IN ('user.username', 'auth.password')
GO

-- Bước 2: Thêm mới (Luôn đủ 2 ngôn ngữ và SourceLanguage NOT NULL)
-- MODULE: USER
INSERT INTO [dbo].[sy_resources] ([ResourceKey], [ResourceValue], [SourceLanguage], [LanguageCode], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt])
VALUES ('user.username', N'Tên người dùng', 'vi-VN', 'vi-VN', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())

INSERT INTO [dbo].[sy_resources] ([ResourceKey], [ResourceValue], [SourceLanguage], [LanguageCode], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt])
VALUES ('user.username', N'Username', 'vi-VN', 'en-US', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())

-- MODULE: AUTH
INSERT INTO [dbo].[sy_resources] ([ResourceKey], [ResourceValue], [SourceLanguage], [LanguageCode], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt])
VALUES ('auth.password', N'Mật khẩu', 'vi-VN', 'vi-VN', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())

INSERT INTO [dbo].[sy_resources] ([ResourceKey], [ResourceValue], [SourceLanguage], [LanguageCode], [CreatedBy], [CreatedAt], [UpdatedBy], [UpdatedAt])
VALUES ('auth.password', N'Password', 'vi-VN', 'en-US', 'SYSTEM', GETDATE(), 'SYSTEM', GETDATE())

GO
PRINT 'Seed Resource Example completed successfully.'
