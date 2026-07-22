# TASK-02: Xây Dựng Màn Hình Quản Lý Vai Trò & Phân Quyền (SyRoles)

- **Mã Task**: `TASK-02`
- **Mô-đun**: `System`
- **Người thực hiện**: Fullstack Dev / QA
- **Estimate**: `4 Story Points`
- **Trạng thái**: ⚪ Todo

---

## 📝 Mô Tả Công Việc

Phát triển màn hình Quản lý Vai trò (`SyRoles`) và Cây phân quyền (Permission Tree), cho phép tạo mới vai trò, gán danh sách quyền và cập nhật quyền hạn cho tài khoản.

---

## 📋 Danh Sách Sub-Tasks (Checklist)

### 1. Backend (.NET 9 CQRS)
- [ ] Kiểm tra API `SyRolesController.cs` (GET `/api/v1/roles`, POST, PUT, DELETE).
- [ ] Xây dựng Command `AssignRolePermissionsCommand` gán danh sách `PermissionCode`.
- [ ] Dựng Query `GetRolePermissionTreeQuery` trả về cây phân quyền theo phân hệ.

### 2. Frontend (Next.js 15 & HeroUI v3)
- [ ] Dựng trang `app/(admin)/system/roles/page.tsx`.
- [ ] Dựng component `role-permission-tree.tsx` dạng Checkbox Tree cho phép chọn quyền.
- [ ] Tạo `role-form-modal.tsx` để nhập Tên vai trò và Mô tả.
- [ ] Khai báo keys i18n `system.roles...` trong `vi.json` & `en.json`.

---

## 🧪 Acceptance Criteria (Điều Kiện Nghiệm Thu)

1. Hiển thị danh sách các vai trò hiện có trong CSDL.
2. Cho phép tích chọn/bỏ chọn các quyền trên cây phân quyền và lưu thành công.
3. Người dùng thuộc vai trò đó khi đăng nhập lại có đúng các quyền được gán.
