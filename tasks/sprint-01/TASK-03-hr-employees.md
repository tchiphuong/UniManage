# TASK-03: Xây Dựng Màn Hình Quản Lý Hồ Sơ Nhân Viên (HrEmployees)

- **Mã Task**: `TASK-03`
- **Mô-đun**: `Human Resources`
- **Người thực hiện**: Fullstack Dev
- **Estimate**: `5 Story Points`
- **Trạng thái**: ⚪ Todo

---

## 📝 Mô Tả Công Việc

Phát triển toàn trình tính năng Quản lý Hồ sơ Nhân viên (`HrEmployees`): CSDL Migration ➔ Backend CQRS API ➔ Frontend UI HeroUI v3.

---

## 📋 Danh Sách Sub-Tasks (Checklist)

### 1. Database & Backend
- [ ] Tạo script migration SQL `02_HR_HrEmployees.sql` (bảng `HrEmployees`, `HrDepartments`, `HrPositions`).
- [ ] Tạo Entities, Request/Response DTOs trong `UniManage.Shared.Domain`.
- [ ] Dựng CQRS Commands (Create, Update, Delete) & Queries (GetList Dapper).
- [ ] Khai báo `HrEmployeesController.cs` với Route `/api/v1/employees`.

### 2. Frontend
- [ ] Khai báo interface `HrEmployeeModel` trong `types/hr.ts`.
- [ ] Tạo `employee.service.ts` trong `services/hr/`.
- [ ] Dựng trang `app/(admin)/hr/employees/page.tsx` kèm Toolbar, Table, Pagination.
- [ ] Dựng `employee-form-modal.tsx` chọn Phòng ban, Chức vụ và Hợp đồng.

---

## 🧪 Acceptance Criteria (Điều Kiện Nghiệm Thu)

1. Tạo mới nhân viên có chọn phòng ban và chức vụ thành công.
2. Tìm kiếm nhân viên theo Mã NV, Họ tên hoặc Số điện thoại.
3. Chỉnh sửa thông tin nhân viên và cập nhật CSDL.
