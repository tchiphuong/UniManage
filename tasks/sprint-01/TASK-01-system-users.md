# TASK-01: Tối Ưu & Kiểm Thử Màn Hình Quản Lý Người Dùng (SyUsers)

- **Mã Task**: `TASK-01`
- **Mô-đun**: `System`
- **Người thực hiện**: Fullstack Dev / QA
- **Estimate**: `4 Story Points`
- **Trạng thái**: 🟡 In Progress

---

## 📝 Mô Tả Công Việc

Tối ưu hóa mã nguồn Backend (CQRS + Dapper Query) và Frontend (`app/(admin)/system/users/page.tsx`), kiểm tra các quy tắc Validation, i18n và hoàn thiện luồng Thêm/Sửa/Xóa người dùng.

---

## 📋 Danh Sách Sub-Tasks (Checklist)

### 1. Backend (.NET 9 CQRS)
- [ ] Kiểm tra `SyUsersController.cs` đã khai báo Route `/api/v1/users` và attribute `[Authorize]`.
- [ ] Đảm bảo `CreateSyUserCommand` và `UpdateSyUserCommand` triển khai `ILoggableCommand`.
- [ ] Kiểm tra truy vấn danh sách `GetSyUsersListQuery` dùng Dapper phân trang mượt mà.
- [ ] Verify FluentValidation rules trong `SystemCommandValidators.cs`.

### 2. Frontend (Next.js 15 & HeroUI v3)
- [ ] Kiểm tra `app/(admin)/system/users/page.tsx` sử dụng đúng `useApiHandler()`.
- [ ] Đảm bảo UI components bọc từ `@/components/common` (không import thô từ heroui).
- [ ] Kiểm tra ô tìm kiếm Username/Email dùng `useDebounce` (500ms).
- [ ] Verify đủ keys dịch trong `messages/vi.json` & `messages/en.json`.

---

## 🧪 Acceptance Criteria (Điều Kiện Nghiệm Thu)

1. Giao diện render danh sách người dùng phân trang chính xác.
2. Tìm kiếm theo từ khóa hiển thị đúng kết quả sau 500ms debounce.
3. Tạo mới thành công người dùng và tự động refresh danh sách.
4. Xóa người dùng có bật Modal xác nhận và thông báo thành công.
