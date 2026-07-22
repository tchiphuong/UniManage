# Tài Liệu Thiết Kế Hệ Thống (System Design Document)

Tài liệu này mô tả thiết kế kiến trúc kỹ thuật chi tiết, sơ đồ thành phần, mô hình dữ liệu (ERD) và cơ chế vận hành của hệ thống UniManage.

---

## 1. Sơ Đồ Kiến Trúc Thành Phần (System Component Diagram)

Hệ thống UniManage bao gồm các thành phần chính tương tác với nhau:

```mermaid
graph TD
    Client[Web Client - Next.js 15 / HeroUI v3] -->|HTTP / REST API| WebApi[UniManage.WebApi - .NET 9]
    Client -->|OAuth2 / OIDC| Identity[UniManage.IdentityServer - Duende]
    WebApi -->|Mediator Pipeline| Application[Application Layer - CQRS]
    Application -->|EF Core 9 - Commands| DB[(SQL Server - UniManage DB)]
    Application -->|Dapper - Fast Read Queries| DB
    Application -->|log4net Audit Log| LogDB[(SQL Server - UniManageLog DB)]
    Worker[UniManage.Worker - Background Tasks] -->|Hangfire Job Scheduler| DB
```

### Chi Tiết Các Thành Phần:

1. **Next.js 15 Frontend**: Giao diện người dùng render trên Server (SSR) & Client, sử dụng HeroUI v3 component wrapper system.
2. **UniManage.WebApi**: RESTful Gateway tiếp nhận các yêu cầu từ Client, thực hiện xác thực Bearer JWT và phân tuyến qua MediatR.
3. **UniManage.IdentityServer**: Dịch vụ xác thực tập trung OAuth2/OpenID Connect cấp phát JWT Token.
4. **Clean Architecture & CQRS**: Phân tách luồng xử lý Command (Ghi/Sửa/Xóa qua EF Core) và Query (Đọc phân trang qua Dapper).
5. **UniManage.Worker**: Tiến trình chạy ngầm thực thi các Job định kỳ (gửi email, tính lương, dọn dẹp file rác) qua Hangfire.

---

## 2. Sơ Đồ Thực Thể Cơ Sở Dữ Liệu (Database ERD & Schema Design)

Mô hình dữ liệu cốt lõi của Module Hệ thống (System Module):

```mermaid
erDiagram
    SyUsers ||--o{ SyUserRoles : "thuộc"
    SyRoles ||--o{ SyUserRoles : "được gán"
    SyRoles ||--o{ SyRolePermissions : "chứa quyền"
    SyPermissions ||--o{ SyRolePermissions : "được phân"
    
    SyUsers {
        uuid Uuid PK
        string Username
        string PasswordHash
        string Email
        string FullName
        bool IsActive
        datetime CreatedAt
    }

    SyRoles {
        uuid Uuid PK
        string RoleCode
        string RoleName
        string Description
    }

    SyUserRoles {
        uuid UserUuid FK
        uuid RoleUuid FK
    }

    SyPermissions {
        string PermissionCode PK
        string PermissionName
        string Module
    }

    SyRolePermissions {
        uuid RoleUuid FK
        string PermissionCode FK
    }

    SyConfigs {
        string ConfigKey PK
        string ConfigValue
        string Description
    }

    SyFiles {
        uuid Uuid PK
        string FileName
        string FilePath
        long FileSize
        string ContentType
    }
```

---

## 3. Sơ Đồ Luồng Xác Thực & Cấp Phát Token (Authentication Sequence Diagram)

Quy trình đăng nhập và tương tác API bảo mật sử dụng JWT Bearer Token:

```mermaid
sequenceDiagram
    autonumber
    actor User as Người dùng / Web App
    participant IS as IdentityServer (:5000)
    participant API as WebApi Gateway (:5297)
    participant DB as SQL Server (UniManage DB)

    User->>IS: POST /connect/token (username, password)
    IS->>DB: Truy vấn người dùng & vai trò (Dapper)
    DB-->>IS: Trả về thông tin người dùng hợp lệ
    IS-->>User: Cấp phát JWT Access Token & Refresh Token
    
    User->>API: GET /api/v1/users (Header: Authorization Bearer JWT)
    API->>API: Validating JWT Token Signature & Claims
    API->>DB: Thực thi Query (Dapper)
    DB-->>API: Trả về danh sách dữ liệu
    API-->>User: HTTP 200 { returnCode: 0, data: [...] }
```

---

## 4. Mô Hình Hybrid ORM (EF Core + Dapper)

Hệ thống tận dụng ưu điểm của cả 2 ORM hàng đầu:

```
                  ┌───────────────────────────────────────────┐
                  │          CQRS Mediated Requests           │
                  └─────────────────────┬─────────────────────┘
                                        │
                    ┌───────────────────┴───────────────────┐
                    ▼                                       ▼
        [ Commands (Write/Update) ]             [ Queries (Read Only) ]
                    │                                       │
        [ EF Core 9.0 DbContext ]               [ Dapper IDbConnection ]
                    │                                       │
      • Change Tracking                       • Raw SQL / Stored Procedures
      • Unit of Work & Transactions           • Fast Multimapping / Joins
      • Audit Logging via Behavior            • High Performance Pagination
                    │                                       │
                    └───────────────────┬───────────────────┘
                                        ▼
                            [( SQL Server Database )]
```
