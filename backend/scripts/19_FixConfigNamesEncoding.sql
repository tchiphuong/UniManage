-- 19_FixConfigNamesEncoding.sql
-- Purpose: Fix encoding issues (Mojibake) in ConfigName column

-- 1. Fix JOB_SCHEDULER_ENABLED
UPDATE sy_configs
SET ConfigName = N'Bật/Tắt Hệ thống Job'
WHERE ConfigCode = 'JOB_SCHEDULER_ENABLED';

-- 2. Fix SYSTEM_DEFAULT_LANGUAGE
UPDATE sy_configs
SET ConfigName = N'Ngôn ngữ mặc định',
    Description = N'Ngôn ngữ mặc định của hệ thống'
WHERE ConfigCode = 'SYSTEM_DEFAULT_LANGUAGE';

-- Just in case: Standardize JOB_TOKEN_CLEANUP_CRON name if needed (Optional, keeping English is fine too but Vietnamese is consistent)
-- UPDATE sy_configs
-- SET ConfigName = N'Lịch dọn dẹp Token'
-- WHERE ConfigCode = 'JOB_TOKEN_CLEANUP_CRON';
