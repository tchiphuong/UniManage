# Quy Chuẩn Lập Trình (Coding Standards)

Tài liệu này quy định các chuẩn mực mã nguồn bắt buộc phải tuân thủ trong dự án UniManage áp dụng cho cả Backend (.NET) và Frontend (Next.js/React).

---

## 1. Quy Tắc Đặt Tên (Naming Conventions)

| Tầng / Thành phần | Quy tắc đặt tên | Ví dụ |
| :--- | :--- | :--- |
| **Database Table** | Thêm Prefix mô-đun (PascalCase) | `SyUsers`, `SyRoles`, `HrEmployees`, `CdCategories` |
| **Backend Entity** | Giữ nguyên tên bảng DB | `SyUsers`, `SyRoles` |
| **API Endpoints** | **Bỏ Prefix**, danh từ số nhiều | `/api/v1/users`, `/api/v1/roles`, `/api/v1/employees` |
| **Frontend Route** | **Bỏ Prefix**, chữ thường | `app/system/users/page.tsx`, `app/system/roles/page.tsx` |
| **Frontend Type** | Giữ Prefix (để khớp với DB DTO) | `export interface SyUserModel { ... }` |

---

## 2. Quy Chuẩn Backend (.NET 9)

### 2.1. Kiến Trúc CQRS & MediatR

- **Thin Controller**: Controller chỉ thực hiện routing, nhận Request và chuyển tiếp qua `_mediator.Send()`. Không viết logic nghiệp vụ trong Controller.
- **ILoggableCommand Interface**: Tất cả Command và Query **bắt buộc** phải triển khai interface `ILoggableCommand`.
  - **Cấm**: Không viết mã log thủ công (`_logger.LogInformation...`) bên trong Handler. Logging Behavior sẽ tự động ghi vết audit log cho mọi request.
- **Phần Tóm Tắt Validator**: Validator không tạo file nhỏ lẻ mà được gom nhóm tập trung vào các file validator của mô-đun:
  - `{Module}QueriesValidators.cs` (Cho các truy vấn Query)
  - `{Module}CommandValidators.cs` (Cho các lệnh Command)
- **Phân Trang Query**: Các Query danh sách phải kế thừa `BaseListQuery` và trả về cấu trúc `PagedResponse<T>`.

### 2.2. Documentation Code Comments

- **Tất cả Doc Comments** (`/// <summary>`, `/** */`) trong mã nguồn Backend và Frontend **BẮT BUỘC viết bằng tiếng Anh**.
  *Lý do: Đảm bảo khả năng đọc hiểu chuẩn quốc tế khi IDE hiển thị Intellisense/Tooltip.*
- Inline comment (`//`) có thể sử dụng Tiếng Việt để ghi chú nhanh.

---

## 3. Quy Chuẩn Frontend (Next.js 15 & HeroUI v3)

### 3.1. UI Component Wrapper Pattern (MANDATORY)

- **KHÔNG BÁO GIỜ** import trực tiếp các UI component thô (như `Button`, `Input`, `Table`, `Modal`...) từ thư viện gốc `@heroui/react` tại các trang tính năng (`app/...`).
- **LUÔN LUÔN** tạo hoặc sử dụng component wrapper bọc lại trong thư mục `@/components/common` (ví dụ: `components/common/button.tsx`).
- Việc này giúp quy định sẵn default props, variant, kích thước và màu sắc chuẩn theo design system của dự án.

### 3.2. Co-location Pattern Cho Feature Components

- Tránh đưa các component chỉ dùng cho 1 màn hình cụ thể vào thư mục `components/` toàn cục.
- Đặt các modal form, drawer, custom card riêng của tính năng ngay tại thư mục chứa trang đó:
  - Ví dụ: `app/system/users/components/user-form-modal.tsx`

### 3.3. Xử Lý API & Token

- **API Success Condition**: Cấu trúc phản hồi API của UniManage sử dụng `Result<T>`. Điều kiện gọi thành công là **`returnCode === 0`** (KHÔNG PHẢI HTTP Status 200).
- **useApiHandler**: Bắt buộc sử dụng hook `useApiHandler()` cùng helper `executeApiCall()` hoặc `handleResponse`/`handleError` để xử lý các cuộc gọi API.
- **Không parse lỗi thủ công**: Không tự viết `try-catch` rồi truy cập `err.response.data` thủ công ở từng component, hãy sử dụng `handleApiError` từ `@/lib/utils`.

### 3.4. Đa Ngôn Ngữ (Internationalization - i18n)

- **CẤM HARDCODE TEXT**: Tuyệt đối không hardcode các chuỗi văn bản Tiếng Anh hay Tiếng Việt trên giao diện UI (kể cả văn bản fallback như `'User'`, `'Cancel'`).
- **Sử dụng `t()` function**: Tất cả văn bản hiển thị cho người dùng phải đi qua hàm dịch `t('key')` của thư viện i18n.
- Nếu key dịch chưa tồn tại, lập tức khai báo bổ sung vào 2 file JSON ngôn ngữ:
  - `frontend/uni-manage/messages/vi.json`
  - `frontend/uni-manage/messages/en.json`
