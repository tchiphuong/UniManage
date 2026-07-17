---
name: UniManage Feature Workflow
description: Tiêu chuẩn và quy trình 5 bước bắt buộc khi phát triển một tính năng/module mới (từ Frontend, Backend, E2E Test đến Tài liệu) cho hệ thống UniManage.
---

# UniManage Feature Implementation Workflow

Đây là bộ quy tắc bắt buộc (Mandatory Rules) định hướng kiến trúc và quy trình làm việc dành cho các AI Agent khi được yêu cầu xây dựng một màn hình hoặc tính năng hoàn chỉnh.

## 1. Nguyên tắc Đặt tên (Naming Conventions)

- **Database & Backend**: Sử dụng đầy đủ tiền tố của Module (VD: `SyUsers`, `CdCategories`).
- **API & UI Routes**: **BẮT BUỘC cắt bỏ tiền tố** để URL gọn gàng, thân thiện và đạt chuẩn RESTful.
  - ❌ Lỗi: `/api/v1/syusers`, `app/system/syusers/page.tsx`
  - ✅ Đúng: `/api/v1/users`, `app/system/users/page.tsx`
- **Interfaces / Models (Frontend)**: Nên giữ lại tiền tố gốc (VD: `export interface SyUser { ... }`) để Dev dễ dàng map chính xác với cấu trúc dưới Database và Backend.

## 2. Quy trình 5 Bước Triển khai (5-Step Production Workflow)

Khi nhận yêu cầu "Làm trang X", Agent PHẢI tự động thiết kế và thực thi theo trình tự 5 bước sau, không được phép đốt cháy giai đoạn:

### Bước 1: Khảo sát Backend (WebAPI)
- Dùng công cụ (grep_search) tìm Controller tương ứng (VD: `SyUsersController.cs`).
- Kiểm tra các Endpoints (GET, POST, PUT, DELETE) đã có sẵn hay chưa.
- Lấy chính xác cấu trúc payload Request/Response từ các file `CQRS` Commands/Queries.

### Bước 2: Setup Types & Services (Frontend)
- Cập nhật/Tạo file định nghĩa kiểu dữ liệu trong thư mục `types/` (VD: `types/system.ts`).
- Tạo file API Service tại thư mục `services/` (VD: `services/system/user.service.ts`).
- Service phải bọc các lệnh gọi qua class `apiClient` của Frontend để hỗ trợ nhét Token tự động và hứng lỗi chuẩn.

### Bước 3: Phát triển Giao diện (Frontend UI)
Sử dụng thư viện **HeroUI v3** và tuân thủ chặt chẽ `HeroUI v3 Guidelines` Skill:
- **Routes**: Đặt tại thư mục `app/` (VD: `app/system/users/page.tsx`).
- **Thành phần chính trên một màn hình quản lý**:
  1. `Toolbar`: Vùng Header chứa ô Search, Filter trạng thái và nút Thêm mới.
  2. `Data Table`: Component Table (`@heroui/react`) xử lý phân trang (Server-side Pagination).
  3. `Modals`: Tạo tách biệt `FormModal` (dùng React Hook Form + Zod) và `ConfirmModal` (cảnh báo xóa dữ liệu).

### Bước 4: Kiểm thử tự động (E2E Testing)
Bảo chứng chất lượng cho Product bằng mã test tự động thay vì test tay:
- **File test**: Tạo file test tại `e2e-tests/` (VD: `system.users.spec.ts`).
- **Chuẩn mực Playwright**:
  - Dùng **Page Object Model (POM)** tách logic tương tác UI ra khỏi spec.
  - Không hardcode thông tin đăng nhập; dùng `process.env`.
  - Phủ đủ các luồng: Xem danh sách, Thêm mới, Sửa, Xoá, và Validate Form lỗi.

### Bước 5: Soạn thảo Tài liệu (User Manual)
Tính năng chỉ hoàn tất khi có tài liệu hướng dẫn:
- **Nơi lưu**: Tạo file hướng dẫn tại `docs/manuals/` (VD: `UserManagement_Manual.md`).
- **Nội dung**: Giới thiệu mục đích, hướng dẫn từng thao tác cụ thể cho End User.
