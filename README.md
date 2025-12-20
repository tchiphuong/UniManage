# UniManage

## Giới thiệu

**UniManage** là một hệ thống quản lý giúp doanh nghiệp tối ưu hóa quy trình đặt hàng, theo dõi nhà cung cấp và kiểm soát dữ liệu sản phẩm. Dự án được thiết kế với kiến trúc hiện đại, sử dụng ASP.NET cho backend và AngularJS cho frontend.

## Cấu trúc dự án

```
UniManage/                         # Monorepo root
├── backend/                      # Backend .NET 9
│   ├── src/                     # Backend source code
│   │   ├── UniManage.Api/       # REST API (Controllers, Middleware)
│   │   ├── UniManage.Application/ # CQRS (Commands, Queries, Handlers)
│   │   ├── UniManage.Core/      # Database, Models, Infrastructure
│   │   ├── UniManage.Resource/  # Localization (T4 Templates)
│   │   ├── UniManage.IdentityServer/ # Authentication & Authorization
│   │   └── UniManage.sln        # Backend solution
│   │
│   ├── docs/                    # Backend documentation
│   │   └── examples/           # Code examples
│   │
│   └── scripts/                # Backend scripts (migrations, tools)
│
├── frontend/                     # Frontend (Angular/React)
│   ├── src/                     # Frontend source code
│   ├── public/                  # Static assets (planned)
│   ├── package.json             # npm dependencies (planned)
│   └── README.md                # Frontend documentation
│
├── .github/                     # GitHub configs
│   ├── workflows/              # CI/CD pipelines
│   └── copilot-instructions.md # Copilot settings
│
├── .gitignore                   # Git ignore rules (monorepo)
└── README.md                    # Main documentation (this file)
```

**Lưu ý cấu trúc Monorepo:**

-   `backend/src/` - Backend .NET 9 source code
-   `frontend/src/` - Frontend React/Angular source code (planned)
-   Cả 2 đều trong cùng 1 Git repository (Monorepo)
-   CI/CD sẽ có 2 pipelines riêng cho backend và frontend

## Công nghệ sử dụng

### Backend:

-   **ASP.NET Core**: .NET 9
-   **Database**: SQL Server
-   **ORM**: Dapper (không dùng EF Core)
-   **Pattern**: CQRS + MediatR
-   **Authentication**: Duende IdentityServer (custom stores với Dapper)
-   **Logging**: log4net (SiftingAppender - log theo ngày & API)
-   **Jobs**: Hangfire (SQL Server storage)
-   **Validation**: FluentValidation
-   **Localization**: T4 Template + Database (sy_resources, sy_languages)

### Frontend:

-   **Framework**: React 18+ / Angular 17+ (To be decided)
-   **Language**: TypeScript
-   **Styling**: Tailwind CSS
-   **State Management**: Redux Toolkit / NgRx
-   **API Client**: Axios / Angular HttpClient
-   **Build Tool**: Vite / Angular CLI

### DevOps:

-   Docker
-   CI/CD với GitHub Actions

## Tính năng chính

### 1️⃣ Quản lý nhà cung cấp

-   Thêm, sửa, xóa nhà cung cấp
-   Theo dõi lịch sử giao dịch

### 2️⃣ Quản lý đơn hàng

-   Tạo đơn hàng, cập nhật trạng thái
-   Lịch sử chỉnh sửa đơn hàng
-   Theo dõi tiến độ xử lý

### 3️⃣ Quản lý sản phẩm

-   Danh mục sản phẩm theo nhà cung cấp
-   Giá nhập, đơn vị, số lượng tối thiểu

### 4️⃣ Thống kê & báo cáo

-   Biểu đồ hiệu suất nhà cung cấp
-   Tổng hợp sản phẩm nhập theo thời gian

## Cách cài đặt

### Yêu cầu hệ thống:

**Backend:**

-   .NET 9 SDK
-   SQL Server 2019+
-   PowerShell 5.1+ (cho scripts)
-   Visual Studio 2022 hoặc VS Code

**Frontend:**

-   Node.js 18+ LTS
-   npm 9+ hoặc yarn
-   Modern browser (Chrome, Firefox, Edge, Safari)

**Optional:**

-   Docker & Docker Compose

### 1. Clone repository

```bash
git clone https://github.com/tchiphuong/UniManage.git
cd UniManage
```

### 2. Cấu hình Backend

#### a. Tạo database:

```sql
CREATE DATABASE UniManage;
CREATE DATABASE UniManageLog;
```

#### Tạo user:

```sql
CREATE LOGIN uni_manager WITH PASSWORD = 'YOUR_STRONG_PASSWORD';
USE UniManage;
CREATE USER uni_manager FOR LOGIN uni_manager;
ALTER ROLE db_owner ADD MEMBER uni_manager;
```

#### Chạy migration:

```bash
# Chạy script SQL migrations
cd src/UniManage.Core/Migrations
# Xem README.md trong folder Migrations để biết thêm chi tiết
# Chạy RunAllMigrations.sql hoặc từng file migration theo thứ tự
```

### 3. Cấu hình Connection String

Cập nhật `src/UniManage.Core/appsettings.Development.json`:

```json
{
    "Database": {
        "Server": "YOUR_SERVER_NAME",
        "DefaultDatabase": "UniManage",
        "LogDatabase": "UniManageLog",
        "UserId": "uni_manager",
        "Password": "YOUR_DB_PASSWORD",
        "TrustServerCertificate": true
    }
}
```

### 4. Generate Resources từ Database

```bash
cd src/UniManage.Resource
.\GenerateCoreResource.ps1
```

### 5. Build & Run

```bash
cd src
dotnet build
dotnet run --project UniManage.Api
```

API sẽ chạy tại: `https://localhost:5001`

### 6. Setup Frontend

Frontend đã được tạo sẵn với **React 18 + TypeScript + Vite + Tailwind CSS**.

#### a. Install dependencies:

```bash
cd frontend
npm install
```

#### b. Tạo file `.env`:

```bash
# Copy .env.example
cp .env.example .env

# Edit .env (Windows)
notepad .env
```

Nội dung `.env`:

```env
VITE_API_BASE_URL=http://localhost:5000/api
```

#### c. Chạy development server:

```bash
npm run dev
```

Frontend sẽ chạy tại: `http://localhost:3000`

#### d. Build production:

```bash
npm run build
```

Build output: `frontend/dist/`

### 7. Chạy cả Backend & Frontend

**Terminal 1 - Backend:**

```bash
cd backend/src
dotnet run --project UniManage.Api
```

**Terminal 2 - Frontend:**

```bash
cd frontend
npm run dev
```

Access:

-   Frontend: http://localhost:3000
-   Backend API: http://localhost:5000/api
-   API Docs: http://localhost:5000/swagger (nếu có)
    npm install axios # For React

# Angular uses HttpClient built-in

# Start development server

npm run dev # React (port 5173)

# or

ng serve # Angular (port 4200)

````

Frontend documentation: See `frontend/README.md`

## Frontend Development (Separate Documentation)

Chi tiết về frontend development xem tại: [`frontend/README.md`](./frontend/README.md)

**Quick links:**

-   API Integration: See frontend/README.md
-   Authentication Flow: OpenID Connect with IdentityServer
-   UI Components: Tailwind CSS + Custom components
-   State Management: Redux Toolkit (React) / NgRx (Angular)

## Database Schema

### Chuẩn thiết kế database:

-   **PK**: Id (INT IDENTITY hoặc BIGINT)
-   **Text**: NVARCHAR; thời gian: DATETIME2(3)
-   **Concurrency**: ROWVERSION → cột DataRowVersion (map byte[])
-   **Audit columns** (mọi bảng): CreatedBy, CreatedAt, UpdatedBy, UpdatedAt, DataRowVersion
-   **Naming**: Hậu tố cột Amount/Number/Name/Code/Qty/Date/Rate
-   **Index**: IX*{Table}*{Column}
-   **Encryption**: Mật khẩu được mã hóa AES-256 (xem `README_ENCRYPTION.md`)

### Core System Tables:

-   **sy_languages**: Ngôn ngữ hệ thống (vi-VN, en-US)
-   **sy_resources**: Resources đa ngôn ngữ (cho T4 Template)
-   **sy_users**: Users & authentication
-   **sy_roles**: Roles & permissions
-   **sy_menus**: Menu structure
-   **sy_functions**: Function permissions

### Master Data Tables:

-   **md_provinces**: Tỉnh/thành phố
-   **md_districts**: Quận/huyện
-   **md_wards**: Phường/xã
-   **md_employees**: Nhân viên
-   **md_units**: Đơn vị tổ chức

Chi tiết xem trong `docs/DATABASE_ENCRYPTION.md`

## API Documentation

API được viết theo chuẩn RESTful với response format chuẩn:

### Standard Response Format:

```json
{
    "returnCode": 0,
    "errors": [],
    "message": "Operation completed successfully",
    "data": {}
}
````

### Paged Response Format:

```json
{
    "returnCode": 0,
    "errors": [],
    "message": "Successfully retrieved list",
    "data": {
        "items": [],
        "paging": {
            "pageIndex": 1,
            "pageSize": 20,
            "totalItems": 100,
            "totalPages": 5
        }
    }
}
```

Tài liệu chi tiết có thể xem tại: `http://localhost:5001/swagger`

## Localization & Resources

UniManage hỗ trợ đa ngôn ngữ thông qua T4 Template + Database.

### Cấu trúc:

-   **sy_languages**: Danh sách ngôn ngữ (vi-VN, en-US, ...)
-   **sy_resources**: Resource key-value theo từng ngôn ngữ

### Regenerate Resources:

Mỗi khi cập nhật resources trong database, chạy:

```bash
cd src/UniManage.Resource
.\GenerateCoreResource.ps1
```

Script sẽ generate `CoreResource.cs` từ database.

### Sử dụng trong code:

```csharp
// Static resource
string message = CoreResource.Validation_msg_Required;

// Dynamic resource với ResourceManager
var resManager = new ResourceManager(dbContext);
string translated = await resManager.GetAsync("Validation_msg_Required", "vi-VN");
```

## Logging

Sử dụng **log4net** với SiftingAppender:

-   Log theo ngày: `logs/yyyy-MM-dd/{api}.log`
-   Error log: `logs/yyyy-MM-dd/error-{api}.log`
-   Mỗi API endpoint có file log riêng
-   Tự động mask dữ liệu nhạy cảm (password, token, secret)

Config: `src/UniManage.Core/log4net.config`

## 🔐 Database Configuration Encryption

UniManage hỗ trợ mã hóa password trong configuration file với AES-256.

### Cấu trúc Configuration

#### Development (Plain text):

```json
{
    "Database": {
        "Server": "YOUR_SERVER_NAME",
        "DefaultDatabase": "UniManage",
        "UserId": "uni_manager",
        "Password": "YOUR_DB_PASSWORD",
        "TrustServerCertificate": true
    }
}
```

#### Production (Encrypted):

```json
{
    "Database": {
        "Server": "TCPHUONG\\SQLEXPRESS",
        "DefaultDatabase": "UniManage",
        "UserId": "uni_manager",
        "Password": "ENC:J5xK8mQ3p9YzWfA7bN2kR8tU6vC4xD1e...",
        "TrustServerCertificate": true
    }
}
```

### Quick Start - Encryption

#### 1. Set Encryption Seed:

```powershell
# PowerShell (User-level - permanent)
[Environment]::SetEnvironmentVariable("UNIMANAGE_ENCRYPTION_SEED", "YOUR_SECRET_SEED_HERE", "User")

# Hoặc session hiện tại
$env:UNIMANAGE_ENCRYPTION_SEED = "YOUR_SECRET_SEED_HERE"
```

#### 2. Encrypt Password:

```powershell
# Cách 1: Dùng script (Recommended)
.\scripts\EncryptPassword.ps1

# Cách 2: Command line
cd src\UniManage.Core
dotnet run -- encrypt "YOUR_DB_PASSWORD"
```

Output:

```
Encrypted value:
ENC:J5xK8mQ3p9YzWfA7bN2kR8tU6vC4xD1e...
```

#### 3. Update Config:

Copy encrypted value vào `appsettings.json`:

```json
{
    "Database": {
        "Password": "ENC:J5xK8mQ3p9YzWfA7bN2kR8tU6vC4xD1e..."
    }
}
```

#### 4. Test Connection:

```powershell
dotnet run --project src\UniManage.Core
```

### Encryption Mechanism

-   **Algorithm**: AES-256
-   **Key Derivation**: SHA-256 hash của `MachineName + UNIMANAGE_ENCRYPTION_SEED`
-   **Prefix**: `ENC:` để nhận biết encrypted values
-   **Auto Decrypt**: DbContext tự động decrypt khi connect database

### Security Best Practices

**Development:**

-   ✅ Plain text password OK
-   ✅ File trong `.gitignore`

**Production:**

-   ✅ **Bắt buộc** encrypt password
-   ✅ Set `UNIMANAGE_ENCRYPTION_SEED` trên production server (Machine-level)
-   ✅ Backup encryption seed ở nơi an toàn
-   ✅ Rotate seed định kỳ (3-6 tháng)

**Troubleshooting:**

```powershell
# Error: "Failed to decrypt" → Check seed
echo $env:UNIMANAGE_ENCRYPTION_SEED

# Decrypt để verify
dotnet run -- decrypt "ENC:yourvalue"

# Re-encrypt với seed mới
dotnet run -- encrypt "newpassword"
```

## Development Guidelines

### CQRS Pattern:

-   **Command**: Thay đổi dữ liệu (Create, Update, Delete)
    -   Handler có transaction (TransactionBehavior)
    -   Validation với FluentValidation
    -   Return ApiResponse<T>
-   **Query**: Chỉ đọc dữ liệu
    -   Không có transaction
    -   Return PagedResponse<T> hoặc ApiResponse<T>

### Database Access:

```csharp
// Command (with transaction)
using (var dbContext = new DbContext(openTransaction: true))
{
    try
    {
        // Execute commands
        await dbContext.CommitAsync();
    }
    catch
    {
        await dbContext.RollbackAsync();
        throw;
    }
}

// Query (no transaction)
using (var dbContext = new DbContext())
{
    var result = await dbContext.QueryAsync<T>(...);
    return result;
}
```

### Code Style:

-   Controllers mỏng, chỉ gọi MediatR
-   1 Command/Query = 1 Handler
-   Có XML comments cho public members
-   Không dùng `SELECT *`
-   Tham số hóa SQL queries (tránh SQL injection)

## Contributing

1. Fork repository
2. Tạo feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit changes (`git commit -m 'Add AmazingFeature'`)
4. Push to branch (`git push origin feature/AmazingFeature`)
5. Tạo Pull Request

## License

This project is licensed under the MIT License.

## Contact

**Project Owner**: Trần Chí Phương (tchiphuong)

**Repository**: https://github.com/tchiphuong/UniManage
