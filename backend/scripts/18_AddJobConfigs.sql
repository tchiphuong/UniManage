-- 18_AddJobConfigs.sql
-- Purpose: Add default configuration for TokenCleanupJob cron schedule

IF NOT EXISTS (SELECT 1 FROM sy_configs WHERE ConfigCode = 'JOB_TOKEN_CLEANUP_CRON')
BEGIN
    INSERT INTO sy_configs (ConfigCode, ConfigName, ConfigValue, DataType, IsSystem, Description, CreatedBy)
    VALUES (
        'JOB_TOKEN_CLEANUP_CRON', 
        'Token Cleanup Schedule', 
        '*/15 * * * *', -- Default: Every 15 minutes
        'STRING', 
        1, 
        'Cron expression for TokenCleanupJob (Default: */15 * * * *)', 
        'SYSTEM'
    );
END
