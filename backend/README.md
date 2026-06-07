# UniManage Backend

Kho lưu trữ này chứa mã nguồn backend cho hệ thống **UniManage**, được xây dựng với **ASP.NET Core 9**, **Entity Framework Core 9.0** và **Dapper**.

## 🏗 Kiến Trúc Hệ Thống (Architecture)

Solution được xây dựng tuân thủ nghiêm ngặt theo các nguyên tắc của **Clean Architecture** kết hợp với pattern **CQRS** (Command Query Responsibility Segregation) và **Modular Monolith**:

```text
src/
├── 01.Shared/                                    # Tầng dùng chung cho toàn bộ hệ thống
│   ├── UniManage.Shared.Domain/                  # Domain primitives, base entities, value objects
│   ├── UniManage.Shared.Application/             # Models dùng chung (ApiResponse, FieldError, PagedResult, PagingInfo)
│   ├── UniManage.Shared.Infrastructure/          # Database Context, Pipelines, Utilities, Logging
│   └── UniManage.Shared.Resource/                # Đa ngôn ngữ (Localization, T4 Templates cho resources)
│
├── 02.Modules/                                   # Các module nghiệp vụ (mỗi module độc lập 3 tầng)
│   ├── System/                                   # Quản lý hệ thống (Users, Roles, Menus, Permissions, Auth)
│   │   ├── UniManage.Modules.System.Domain/
│   │   ├── UniManage.Modules.System.Application/
│   │   └── UniManage.Modules.System.Infrastructure/
│   ├── Master/                                   # Danh mục chung (Units, Countries, Provinces, Currencies)
│   │   ├── UniManage.Modules.Master.Domain/
│   │   ├── UniManage.Modules.Master.Application/
│   │   └── UniManage.Modules.Master.Infrastructure/
│   ├── HumanResource/                            # Nhân sự (Employees, Departments, Positions, Attendance)
│   │   ├── UniManage.Modules.HumanResource.Domain/
│   │   ├── UniManage.Modules.HumanResource.Application/
│   │   └── UniManage.Modules.HumanResource.Infrastructure/
│   ├── Inventory/                                # Kho hàng (Products, Items, Stock)
│   │   ├── UniManage.Modules.Inventory.Domain/
│   │   ├── UniManage.Modules.Inventory.Application/
│   │   └── UniManage.Modules.Inventory.Infrastructure/
│   ├── Sales/                                    # Bán hàng (Customers, Orders)
│   │   ├── UniManage.Modules.Sales.Domain/
│   │   ├── UniManage.Modules.Sales.Application/
│   │   └── UniManage.Modules.Sales.Infrastructure/
│   └── Workflow/                                 # Quy trình duyệt (Approval Routes, Requests)
│       ├── UniManage.Modules.Workflow.Domain/
│       ├── UniManage.Modules.Workflow.Application/
│       └── UniManage.Modules.Workflow.Infrastructure/
│
└── 03.Apps/                                      # Tầng ứng dụng (Host, Entry point)
    ├── UniManage.WebApi/                         # API Server (Controllers, Middleware, Filters, DI)
    ├── UniManage.IdentityServer/                 # Xác thực (Duende IdentityServer + Dapper Stores)
    └── UniManage.Worker/                         # Background Jobs (Hangfire)
```

### Nguyên tắc Module
- Mỗi module có **3 tầng độc lập**: Domain → Application → Infrastructure.
- Module chỉ phụ thuộc **Shared** (01.Shared), không phụ thuộc chéo module khác.
- Controllers trong `03.Apps/UniManage.WebApi/` gọi đến Application của từng module qua **MediatR**.

## 🛠 Công Nghệ Sử Dụng (Tech Stack)

- **Framework**: .NET 9
- **Cơ sở dữ liệu**: SQL Server
- **ORM**: Entity Framework Core 9.0 + Dapper (Giải pháp Hybrid)
    - **EF Core**: Dùng cho các thao tác CRUD (Tạo, Đọc Id, Cập nhật, Xóa) có Transaction.
    - **Dapper**: Dùng cho các truy vấn phức tạp, JOIN nhiều bảng, báo cáo cần hiệu suất cao.
- **Xác thực (Auth)**: Duende IdentityServer (Tự viết Custom Stores bằng Dapper)
- **CQRS**: MediatR
- **Validation**: FluentValidation
- **Logging**: log4net (Phân chia log theo ngày và API: `yyyy-MM-dd/{api}.log`)
- **Background Jobs**: Hangfire

## 📦 API Response Format

### Chuẩn Response (ApiResponse\<T\>)
```json
{
  "returnCode": 0,
  "message": "Thao tác thành công",
  "data": { },
  "errors": [],
  "traceId": "abc-123",
  "isSuccess": true
}
```

### Validation Error (400)
Trường `errors` là mảng `FieldError` — mỗi field có thể chứa nhiều lỗi:
```json
{
  "returnCode": 400,
  "message": "Validation failed",
  "data": null,
  "errors": [
    { "field": "username", "messages": ["Tên đăng nhập không được bỏ trống."] },
    { "field": "email", "messages": ["Email không đúng định dạng.", "Email đã được sử dụng."] }
  ]
}
```

### Paged Response (PagedResponse\<T\>)
```json
{
  "returnCode": 0,
  "message": "Lấy danh sách thành công",
  "data": {
    "items": [],
    "paging": {
      "pageIndex": 1,
      "pageSize": 20,
      "totalItems": 100,
      "totalPages": 5
    }
  },
  "errors": []
}
```

## 🔒 Kiến Trúc Bảo Mật (Security Architecture)

API áp dụng nguyên tắc **Zero Trust Architecture**. Mọi request đều bị xem là không an toàn cho đến khi được xác thực.

### Xác thực & Phân quyền (Authentication & Authorization)
- **Global `[Authorize]`** — Tất cả API bắt buộc xác thực theo mặc định.
- Chỉ dùng `[AllowAnonymous]` cho các hàm cụ thể (đăng nhập, quên mật khẩu, health check...).
- Xác thực bằng **JWT Bearer** token thông qua IdentityServer (kiểm tra chữ ký, hạn dùng, issuer, audience).
- **Phòng chống IDOR (Insecure Direct Object Reference)** — Danh tính người dùng (`Username`, `UserId`) luôn được đọc từ JWT token, tuyệt đối không lấy từ body/query request.

### CORS (Cross-Origin Resource Security)
- **Danh sách trắng (Strict whitelist)** — Chỉ những tên miền được cấp phép mới gọi được API.
- Cấu hình qua `appsettings.json`.

### HTTP Security Headers
Mọi phản hồi đều được gắn các header bảo mật:
- `X-Content-Type-Options: nosniff` (Chống MIME sniffing)
- `X-Frame-Options: DENY` (Chống Clickjacking)
- `Content-Security-Policy: default-src 'self'` (Chống XSS)
- `Strict-Transport-Security` (Bắt buộc dùng HTTPS)

### Quản lý Rate Limiting
Bảo vệ hệ thống qua nhiều lớp để chống DoS và Brute-force:
- Global: 100 req/min trên mỗi IP.
- Login: 5 req/min trên mỗi IP.
- Nhạy cảm (quên mật khẩu, reset pass): 10 req/min trên mỗi IP.

### Bảo mật dữ liệu & Mật khẩu
- Mật khẩu mã hóa bằng **BCrypt** (Work factor 12).
- Masking (ẩn) toàn bộ dữ liệu nhạy cảm (password, token, authorization) trong file Log.
- Khóa request body quá lớn (Max 10MB) và log body quá lớn (Truncate ở 64KB).

## 🔐 Mã Hóa Cấu Hình (Security & Encryption)

Các thông tin nhạy cảm trong `appsettings.json` (như mật khẩu Database) phải được mã hóa AES-256 ở trạng thái nghỉ.

### Biến môi trường bắt buộc
> **⚠️ QUAN TRỌNG**: Các biến này PHẢI được thiết lập trước khi chạy ứng dụng.

| Biến | Mục đích | Ví dụ |
| --------------------------- | ------------------------------ | ---------------------------- |
| `UNIMANAGE_ENCRYPTION_SEED` | Khóa gốc để giải mã chuỗi | `MyS3cur3Se3d!2024` |
| `ASPNETCORE_ENVIRONMENT`    | Môi trường khởi chạy | `Development` / `Production` |

### Cách mã hóa/giải mã cấu hình
```bash
# Thiết lập key trước tiên
setx UNIMANAGE_ENCRYPTION_SEED "your-secret-seed-value"
```

## 📝 Tiêu Chuẩn Code (Coding Conventions)

### Nguyên tắc CQRS
Phân tách triệt để luồng **Ghi** (Commands) và luồng **Đọc** (Queries).

#### Commands (Luồng Ghi)
- **Mục đích**: Thay đổi trạng thái dữ liệu (Tạo, Sửa, Xóa).
- **Base Class**: Bắt buộc kế thừa `BaseCommand` (Chứa sẵn `HeaderInfo`).
- **Transaction**: Luôn sử dụng `DbContext(openTransaction: true)`.
- **Luồng xử lý**: `Command -> Validator -> Handler -> SaveChanges() -> Commit()`.
- **Trả về**: `ApiResponse<T>`.

#### Queries (Luồng Đọc)
- **Mục đích**: Lấy dữ liệu, không làm thay đổi trạng thái.
- **Base Class**: 
  - `BaseQuery` cho Single item / Combobox.
  - `BaseListQuery` cho Danh sách phân trang (Đã có sẵn logic Offset, PageIndex, Keyword).
- **Transaction**: **KHÔNG** mở Transaction (`DbContext()`).
- **Trả về**: `ApiResponse<T>` hoặc `PagedResponse<T>`.

### Quy Tắc Viết Code Chung
- **Controller mỏng**: Luôn khởi tạo và gán `HeaderInfo` trước khi gọi `_mediator.Send()`. Không xử lý logic tại Controller.
- **Utilities First**: Bắt buộc tái sử dụng code trong `UniManage.Shared.Infrastructure/Utilities/` (`PasswordHelper`, `ValidationHelper`, `ResponseHelper`...) trước khi viết logic mới.
- **ResponseHelper**: Trả kết quả thống nhất thông qua `ResponseHelper.Success()`, `ResponseHelper.Error()`, `ResponseHelper.PagedSuccess()`. Tham số luôn viết trên cùng 1 dòng.
- **Resources**: Sử dụng `CoreResource` cho mọi text, cảnh báo, nhãn dán gửi về người dùng.

## 🚀 Hướng Dẫn Cài Đặt (Getting Started)

### Yêu cầu hệ thống
- .NET 9 SDK
- SQL Server
- Đã cấu hình biến môi trường `UNIMANAGE_ENCRYPTION_SEED`

### Khởi tạo Database
1. Khởi tạo 2 database rỗng: `UniManage` và `UniManageLog` trong SQL Server.
2. Chạy các file migrations (script SQL) nằm trong thư mục `../database/`.

### Chạy API
```bash
cd src/03.Apps/UniManage.WebApi
dotnet run
```
URL mặc định: `http://localhost:5297`

### Chạy IdentityServer (Auth)
```bash
cd src/03.Apps/UniManage.IdentityServer
dotnet run
```
URL mặc định: `http://localhost:5000`

## 🚢 Danh Sách Kiểm Tra Khi Lên Production (Deployment Checklist)

- [ ] Cấu hình biến môi trường `UNIMANAGE_ENCRYPTION_SEED`.
- [ ] Set `ASPNETCORE_ENVIRONMENT=Production`.
- [ ] Đã mã hóa toàn bộ mật khẩu DB trong `appsettings.json` (bắt đầu bằng `ENC:`).
- [ ] Thiết lập `Cors:AllowedOrigins` chứa danh sách domain thực tế.
- [ ] Tắt Swagger (Set `Swagger:Enabled` = `false`).
- [ ] Set `Database:TrustServerCertificate` = `false`.
- [ ] Kiểm tra hệ thống tự động đẩy file Log ra thư mục `logs/yyyy-MM-dd/`.
