-- ============================================================
-- Migration: 20_CreateErpTasksTable.sql
-- Description: Creates the ERP task management table
-- ============================================================

CREATE TABLE erp_tasks (
    Id                      BIGINT          IDENTITY(1,1)   PRIMARY KEY,
    TaskCode                NVARCHAR(50)    NOT NULL,                           -- Unique code e.g. TASK-20240101-0001
    Title                   NVARCHAR(500)   NOT NULL,                           -- Task title
    Description             NVARCHAR(MAX)   NULL,                               -- Detailed description
    Status                  VARCHAR(50)     NOT NULL DEFAULT 'todo',             -- todo/in_progress/done/cancelled
    Priority                VARCHAR(20)     NOT NULL DEFAULT 'medium',           -- low/medium/high/urgent
    AssigneeEmployeeCode    NVARCHAR(50)    NULL,                               -- Assigned employee code
    AssigneeUsername        NVARCHAR(100)   NULL,                               -- Assigned username
    ReporterUsername        NVARCHAR(100)   NOT NULL,                           -- Who created the task
    DueDate                 DATETIME2(3)    NULL,                               -- Deadline
    StartDate               DATETIME2(3)    NULL,                               -- Expected start date
    CompletedAt             DATETIME2(3)    NULL,                               -- Set when status → done
    Tags                    NVARCHAR(500)   NULL,                               -- Comma-separated tags
    EstimatedHours          DECIMAL(5,2)    NULL,                               -- Estimated effort in hours
    ActualHours             DECIMAL(5,2)    NULL,                               -- Actual hours spent
    DepartmentCode          NVARCHAR(50)    NULL,                               -- Related department
    CreatedBy               NVARCHAR(100)   NOT NULL,
    CreatedAt               DATETIME2(3)    NOT NULL DEFAULT GETDATE(),
    UpdatedBy               NVARCHAR(100)   NOT NULL,
    UpdatedAt               DATETIME2(3)    NOT NULL DEFAULT GETDATE(),
    DataRowVersion          ROWVERSION      NOT NULL                             -- Optimistic concurrency token
);
GO

-- Unique constraint: one task code per record
CREATE UNIQUE INDEX IX_ErpTasks_TaskCode
    ON erp_tasks (TaskCode);
GO

-- Speeds up board/kanban queries that filter by status
CREATE INDEX IX_ErpTasks_Status
    ON erp_tasks (Status);
GO

-- Speeds up "my tasks" queries
CREATE INDEX IX_ErpTasks_AssigneeEmployeeCode
    ON erp_tasks (AssigneeEmployeeCode);
GO

-- Speeds up due-date range queries / overdue reports
CREATE INDEX IX_ErpTasks_DueDate
    ON erp_tasks (DueDate);
GO
