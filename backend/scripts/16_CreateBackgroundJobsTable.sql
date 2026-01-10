CREATE TABLE sy_background_jobs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    JobCode VARCHAR(50) NOT NULL UNIQUE,  -- Mã định danh job (VD: SYNC_HR_DATA)
    JobName NVARCHAR(255) NOT NULL,       -- Tên hiển thị
    CronExpression VARCHAR(50) NOT NULL,  -- Cấu hình chạy (VD: 0 * * * *)
    IsDisabled BIT DEFAULT 0,             -- 1: Tắt, 0: Bật (Mặc định bật)
    Description NVARCHAR(500),
    
    -- Audit Columns
    CreatedBy NVARCHAR(50),
    CreatedAt DATETIME2(3) DEFAULT SYSUTCDATETIME(),
    UpdatedBy NVARCHAR(50),
    UpdatedAt DATETIME2(3),
    DataRowVersion TIMESTAMP
);

-- Seed data demo
INSERT INTO sy_background_jobs (JobCode, JobName, CronExpression, IsDisabled, Description, CreatedBy)
VALUES 
('SYSTEM_HEALTH_CHECK', 'Health Check System', '*/5 * * * *', 0, 'Check hệ thống mỗi 5 phút', 'SYSTEM'),
('DAILY_REPORT', 'Send Daily Report', '0 8 * * *', 0, 'Gửi báo cáo lúc 8h sáng', 'SYSTEM');
