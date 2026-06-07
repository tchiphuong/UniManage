# UniManage Backend

Kho lưu trữ này chứa mã nguồn backend cho hệ thống **UniManage**, được xây dựng với **ASP.NET Core 9**, **Entity Framework Core 9.0** và **Dapper**.

## 🏗 Kiến Trúc Hệ Thống (Architecture)

Solution được xây dựng tuân thủ nghiêm ngặt theo các nguyên tắc của **Clean Architecture** kết hợp với pattern **CQRS** (Command Query Responsibility Segregation):

```text
src/
├── UniManage.Api/           # Tầng Giao diện (Controllers mỏng, Middleware, DI, Auth)
├── UniManage.Application/   # Tầng Nghiệp vụ (Commands, Queries, Validators, Pipelines)
├── UniManage.Core/          # Tầng Cơ sở hạ tầng & Tiện ích (Database Context, Logging, Utils)
├── UniManage.Model/         # Tầng Thực thể (Entities từ T4, DTOs, Enums, Response Models)
├── UniManage.IdentityServer/# Tầng Xác thực (Duende IdentityServer + Dapper Stores)
├── UniManage.Resource/      # Tầng Đa ngôn ngữ (Localization, T4 Templates cho resources)
└── UniManage.Worker/        # Tầng Xử lý nền (Background Jobs với Hangfire)
```

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

# Mã hóa chuỗi
dotnet run --project src/UniManage.Core -- encrypt "your_password_here"

# Giải mã thử nghiệm
dotnet run --project src/UniManage.Core -- decrypt "ENC:your_encrypted_string"
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
- **Utilities First**: Bắt buộc tái sử dụng code trong thư mục `UniManage.Core/Utilities/` (`PasswordHelper`, `ValidationHelper`, `DatabaseHelper`...) trước khi viết logic mới.
- **ResponseHelper**: Trả kết quả thống nhất thông qua hàm `ResponseHelper.Success()`, `ResponseHelper.Error()`, `ResponseHelper.PagedSuccess()`. Tham số luôn viết trên cùng 1 dòng.
- **Resources**: Sử dụng `CoreResource` cho mọi text, cảnh báo, nhãn dán gửi về người dùng.

## 🚀 Hướng Dẫn Cài Đặt (Getting Started)

### Yêu cầu hệ thống
- .NET 9 SDK
- SQL Server
- Đã cấu hình biến môi trường `UNIMANAGE_ENCRYPTION_SEED`

### Khởi tạo Database
1. Khởi tạo 2 database rỗng: `UniManage` và `UniManageLog` trong SQL Server.
2. Chạy các file migrations (script SQL) nằm trong thư mục `../scripts/`. (Xem chi tiết thứ tự tại `../scripts/README.md`).

### Chạy API
```bash
cd src/UniManage.Api
dotnet run
```
URL mặc định: `http://localhost:5297`

### Chạy IdentityServer (Auth)
```bash
cd src/UniManage.IdentityServer
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
