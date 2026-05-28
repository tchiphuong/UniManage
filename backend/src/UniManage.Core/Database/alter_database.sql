-- Add Uuid to sy_users
ALTER TABLE sy_users ADD Uuid UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID();
GO
-- Add Avatar to sy_users
ALTER TABLE sy_users ADD Avatar NVARCHAR(500) NULL;
GO
-- Add Preferences to sy_users
ALTER TABLE sy_users ADD Preferences NVARCHAR(MAX) NULL;
GO
-- Create Unique Index for sy_users Uuid
CREATE UNIQUE NONCLUSTERED INDEX IX_sy_users_Uuid ON sy_users(Uuid);
GO

-- Add Uuid to sy_configs
ALTER TABLE sy_configs ADD Uuid UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID();
GO
-- Create Unique Index for sy_configs Uuid
CREATE UNIQUE NONCLUSTERED INDEX IX_sy_configs_Uuid ON sy_configs(Uuid);
GO
