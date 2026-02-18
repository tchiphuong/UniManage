# UniManage

## Giới thiệu

**UniManage** là một hệ thống quản lý toàn diện giúp doanh nghiệp tối ưu hóa quy trình vận hành, quản lý nhân sự và kiểm soát dữ liệu. Dự án được thiết kế với kiến trúc hiện đại, hiệu năng cao và bảo mật.

## Cấu trúc dự án

```
UniManage/                         # Monorepo root
├── backend/                      # Backend .NET 9
│   ├── src/                     # Backend source code
│   │   ├── UniManage.Api/       # REST API (Controllers, Middleware)
│   │   ├── UniManage.Application/ # CQRS (Commands, Queries, Handlers)
│   │   ├── UniManage.Core/      # Database, Models, Infrastructure
│   │   ├── UniManage.Model/     # Shared Entities, DTOs, Enums
│   │   ├── UniManage.Resource/  # Localization (T4 Templates)
│   │   ├── UniManage.IdentityServer/ # Authentication & Authorization
│   │   └── UniManage.sln        # Backend solution
│   │
│   └── scripts/                # Database Migration Scripts (*.sql)
│
├── frontend/                     # Frontend
│   └── uni-manage/              # Next.js Dashboard Application
│       ├── src/                 # Source code
│       ├── public/              # Static assets
│       └── package.json         # Dependencies
│
├── postman/                      # API Collections & Environments
│   ├── collections/             # Postman Collections
│   └── environments/            # Postman Environments
│
├── .github/                     # GitHub configs
│   └── workflows/              # CI/CD pipelines
│
├── .gitignore                   # Git ignore rules
└── README.md                    # Main documentation (this file)
```

## Công nghệ sử dụng

### Backend

- **Core**: ASP.NET Core (.NET 9)
- **Database**: SQL Server
- **ORM**: Entity Framework Core 9.0 + Dapper (Hybrid approach)
    - **EF Core**: CRUD operations (Create, Read, Update, Delete)
    - **Dapper**: Complex queries, joins, aggregations, high-performance scenarios
- **Architecture**: Clean Architecture + CQRS + MediatR
- **Authentication**: Duende IdentityServer (Custom Stores with Dapper)
- **Logging**: log4net (SiftingAppender - log per day & API)
- **Validation**: FluentValidation
- **Jobs**: Hangfire (SQL Server storage)

### Frontend

- **Framework**: Next.js 15 (App Router)
- **Library**: React 19
- **Language**: TypeScript
- **Styling**: Tailwind CSS v4
- **UI Component Library**: HeroUI v3 (formerly NextUI)
- **State Management**: Zustand / React Context
- **Data Fetching**: TanStack Query (React Query) v5
- **Icons**: Iconify

## Tính năng chính

### 1️⃣ Hệ thống (System)

- Quản lý người dùng (Users)
- Quản lý vai trò & phân quyền (Roles & Permissions)
- Quản lý menu & cấu hình hệ thống
- Localization (Đa ngôn ngữ: VI/EN)

### 2️⃣ Quản lý Nhân sự (HR) - _Planned_

- Quản lý phòng ban, chức vụ
- Hồ sơ nhân viên

## Cách cài đặt

### Yêu cầu hệ thống

- **Backend**: .NET 9 SDK, SQL Server 2019+
- **Frontend**: Node.js 20+ LTS

### 1. Backend Setup

1.  **Cấu hình Database**:
    - Tạo database `UniManage` và `UniManageLog`.
    - Chạy các script migration trong thư mục `backend/scripts/`. Thứ tự chạy xem tại `backend/scripts/README.md`.
2.  **Cấu hình Encryption (Production)**:
    - Sử dụng tool trong `UniManage.Core` để mã hóa connection string.
    - Command: `dotnet run --project backend/src/UniManage.Core -- encrypt "your_password"`
3.  **Chạy IdentityServer** (cho Authentication):
    ```bash
    cd backend/src
    dotnet run --project UniManage.IdentityServer
    ```
    IdentityServer sẽ chạy tại: `http://localhost:5000`
4.  **Chạy API**:
    ```bash
    cd backend/src
    dotnet run --project UniManage.Api
    ```
    API sẽ chạy tại: `http://localhost:5297`

### 2. Frontend Setup

1.  **Di chuyển vào thư mục frontend**:

    ```bash
    cd frontend/uni-manage
    ```

2.  **Cài đặt dependencies**:

    ```bash
    npm install
    ```

3.  **Cấu hình môi trường**:
    - Copy file `.env.example` thành `.env.local` (hoặc tạo mới).
    - Cập nhật biến môi trường:
        ```env
        NEXT_PUBLIC_API_URL=http://localhost:5297/api/v1
        ```

4.  **Chạy development server**:
    ```bash
    npm run dev
    ```
    Truy cập: `http://localhost:3000`

## API Documentation

- **Swagger**: `http://localhost:5297/swagger` (khi chạy Backend ở mode Development)
- **Postman**: Import collection từ thư mục `postman/collections/` vào Postman để test.
- **Authentication**: Tất cả API endpoints yêu cầu JWT Bearer token từ IdentityServer. Đăng nhập qua `/connect/token` endpoint của IdentityServer để lấy access token.

## Development Guidelines

- **Backend**: Tuân thủ CQRS. Command thay đổi dữ liệu, Query chỉ đọc. Luôn Validate input.
- **Frontend**: Sử dụng HeroUI components. Ưu tiên Server Components ở đâu có thể. Client Components dùng cho tương tác UI.

## License

Private / Proprietary
