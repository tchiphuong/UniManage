# Tổng Quan Kiến Trúc Hệ Thống UniManage

Tài liệu này mô tả chi tiết kiến trúc tổng thể, mô hình thiết kế và các giải pháp công nghệ được áp dụng trong dự án UniManage.

---

## 1. Mô Hình Tổng Thể (Monorepo)

UniManage được tổ chức dưới dạng monorepo để quản lý toàn bộ các thành phần hệ thống trong cùng một repository:

```
UniManage/
├── backend/                  # Mã nguồn Backend (.NET 9)
├── frontend/                 # Mã nguồn Frontend (Next.js 15)
├── database/                 # Script cơ sở dữ liệu & migration
├── postman/                  # Collections & Environment test API
├── auto_test/                # Test tự động (Playwright E2E)
└── docs/                     # Tài liệu kỹ thuật & hướng dẫn
```

---

## 2. Kiến Trúc Backend (.NET 9)

Backend của UniManage áp dụng kiến trúc **Clean Architecture** kết hợp mô hình **CQRS (Command Query Responsibility Segregation)** và kiến trúc **Modular Monolith**.

### 2.1. Phân Tầng Mã Nguồn (`backend/src/`)

- **`01.Shared/`**: Thư viện dùng chung cho toàn bộ hệ thống
  - `UniManage.Shared.Domain`: Đóng gói các Entity, Value Object, Domain Event và Domain Interface.
  - `UniManage.Shared.Application`: Chứa logic nghiệp vụ xử lý CQRS (Commands, Queries, Handlers, Validators), MediatR Pipeline Behaviors (`LoggingBehavior`, `ValidationBehavior`).
  - `UniManage.Shared.Infrastructure`: Triển khai các dịch vụ hạ tầng (EF Core DbContext, Dapper Repositories, Redis/Memory Cache, Hangfire Job Scheduler, log4net Appender).
  - `UniManage.Shared.Resource`: Chứa các tài nguyên đa ngôn ngữ (Localization resources: VI, EN).
- **`02.Tools/`**: Các công cụ tiện ích (Code Generator, Mã hóa chuỗi kết nối).
- **`03.Apps/`**: Các ứng dụng chạy thực tế (Entry points)
  - `UniManage.WebApi`: RESTful API Gateway chính cung cấp dịch vụ cho Client (Web/Mobile).
  - `UniManage.IdentityServer`: Server xác thực & cấp phát token OAuth2/OpenID Connect (sử dụng Duende IdentityServer).
  - `UniManage.Worker`: Background Service chạy các tác vụ định kỳ qua Hangfire.

### 2.2. Mô Hình Hybrid ORM (EF Core + Dapper)

Hệ thống kết hợp sức mạnh của 2 ORM hàng đầu:
1. **EF Core 9.0**: Dùng cho các thao tác **CRUD đơn giản**, quản lý Entity State, Change Tracking và Transaction Scope khi ghi dữ liệu (Command side).
2. **Dapper**: Dùng cho các truy vấn **Query đọc dữ liệu phức tạp**, JOIN nhiều bảng, phân trang server-side hiệu năng cao và các câu lệnh SQL tùy biến (Query side).

### 2.3. CQRS & Pipeline Logging

- Mọi thao tác ghi/sửa/xóa dữ liệu được đóng gói thành **Command**.
- Mọi thao tác đọc dữ liệu được đóng gói thành **Query**.
- Tất cả Command & Query phải triển khai interface `ILoggableCommand` để cho phép `LoggingBehavior` tự động ghi vết (Audit Log) lịch sử thao tác của người dùng thông qua **log4net** (chia log theo ngày và endpoint API) mà không cần viết mã log thủ công trong Handler.

---

## 3. Kiến Trúc Frontend (Next.js 15 + HeroUI v3)

Frontend của UniManage được thiết kế hiện đại, tối ưu trải nghiệm người dùng và chuẩn hóa component.

### 3.1. Stack Công Nghệ

- **Framework**: Next.js 15 (App Router architecture)
- **UI Library**: HeroUI v3 (React 19 + Tailwind CSS v4)
- **State Management**: Zustand (Global UI State) & TanStack Query v5 (Server Data Cache)
- **Validation**: `react-hook-form` + `zod`
- **Localization**: Custom i18n provider hỗ trợ Đa ngôn ngữ (Tiếng Việt `vi` / Tiếng Anh `en`).

### 3.2. Cấu Trúc Thiết Kế UI Component

- **Common Component Wrapper Pattern**: Không sử dụng trực tiếp các UI component thô từ `@heroui/react` tại trang tính năng. Mọi component UI (Button, Input, Modal, Table...) được bọc (wrap) tại `@/components/common` để quy định trước default props, style theme đồng nhất.
- **Co-location Pattern**: Các component chỉ thuộc về 1 trang cụ thể (ví dụ Modal form tạo người dùng) được đặt ngay trong thư mục tính năng đó (`app/system/users/components/`) chứ không đưa ra ngoài thư mục chung.

---

## 4. Cơ Chế Xác Thực & Bảo Mật

- **IdentityServer**: Quản lý tài khoản, đăng nhập, cấp phát JWT Access Token & Refresh Token.
- **Dapper Custom Store**: IdentityServer truy vấn trực tiếp bảng người dùng (`SyUsers`) và vai trò (`SyRoles`) thông qua Dapper để đạt hiệu năng tối đa.
- **API Bearer Token**: Mọi API trong `UniManage.WebApi` (trừ các endpoint public/auth) đều bắt buộc gửi kèm header `Authorization: Bearer <JWT_TOKEN>`.
- **Phân Quyền (RBAC)**: Phân quyền theo Vai trò (Role-Based) và Quyền chi tiết (Permission-Based) trên từng endpoint.
