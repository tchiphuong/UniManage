-- SQL Script to seed missing lbl.email resource
-- Run this script in UniManage database

BEGIN TRANSACTION;

DECLARE @Now DATETIME = GETDATE();
DECLARE @User NVARCHAR(50) = 'SYSTEM';

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.email' AND LanguageCode = 'vi-VN')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.email', N'Email', 'en-US', 'vi-VN', @User, @Now, @User, @Now);

IF NOT EXISTS (SELECT 1 FROM sy_resources WHERE ResourceKey = 'lbl.email' AND LanguageCode = 'en-US')
    INSERT INTO sy_resources (ResourceKey, ResourceValue, SourceLanguage, LanguageCode, CreatedBy, CreatedAt, UpdatedBy, UpdatedAt)
    VALUES ('lbl.email', 'Email', 'en-US', 'en-US', @User, @Now, @User, @Now);

COMMIT TRANSACTION;
GO
