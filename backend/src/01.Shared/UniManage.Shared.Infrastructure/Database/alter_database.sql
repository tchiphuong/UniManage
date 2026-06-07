-- Add Uuid to SyUsers
ALTER TABLE SyUsers ADD Uuid UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID();
GO
-- Add Avatar to SyUsers
ALTER TABLE SyUsers ADD Avatar NVARCHAR(500) NULL;
GO
-- Add Preferences to SyUsers
ALTER TABLE SyUsers ADD Preferences NVARCHAR(MAX) NULL;
GO
-- Create Unique Index for SyUsers Uuid
CREATE UNIQUE NONCLUSTERED INDEX IX_SyUsers_Uuid ON SyUsers(Uuid);
GO

-- Add Uuid to SyConfigs
ALTER TABLE SyConfigs ADD Uuid UNIQUEIDENTIFIER NOT NULL DEFAULT NEWID();
GO
-- Create Unique Index for SyConfigs Uuid
CREATE UNIQUE NONCLUSTERED INDEX IX_SyConfigs_Uuid ON SyConfigs(Uuid);
GO
