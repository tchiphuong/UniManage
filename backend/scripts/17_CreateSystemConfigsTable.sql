CREATE TABLE sy_configs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    ConfigCode VARCHAR(50) NOT NULL UNIQUE,  -- Mã cấu hình (VD: SYSTEM_MAINTENANCE_MODE)
    ConfigName NVARCHAR(255) NOT NULL,       -- Tên hiển thị
    ConfigValue NVARCHAR(MAX),               -- Giá trị (Text/JSON/Number)
    DataType VARCHAR(20) DEFAULT 'STRING',   -- STRING, INT, BOOL, JSON, DATETIME
    Description NVARCHAR(500),
    IsSystem BIT DEFAULT 0,                  -- 1: Cấu hình hệ thống (không cho xóa), 0: User custom
    
    -- Audit Columns
    CreatedBy NVARCHAR(50),
    CreatedAt DATETIME2(3) DEFAULT SYSUTCDATETIME(),
    UpdatedBy NVARCHAR(50),
    UpdatedAt DATETIME2(3),
    DataRowVersion TIMESTAMP
);

-- Seed data for Background Jobs Global Switch
INSERT INTO sy_configs (ConfigCode, ConfigName, ConfigValue, DataType, IsSystem, Description, CreatedBy)
VALUES 
('JOB_SCHEDULER_ENABLED', 'Bật/Tắt Hệ thống Job', 'true', 'BOOL', 1, 'Master switch for all background jobs', 'SYSTEM'),
('SYSTEM_DEFAULT_LANGUAGE', 'Ngôn ngữ mặc định', 'vi-VN', 'STRING', 1, 'Ngôn ngữ mặc định của hệ thống', 'SYSTEM');
