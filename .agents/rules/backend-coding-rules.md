---
description: Mandatory coding conventions, architecture patterns, and RESTful standards for UniManage Backend.
alwaysOn: true
---

# UniManage Backend Rules (STRICT MODE)

**BẮT BUỘC (MANDATORY)**: Áp dụng rule này cho **100% các file C#** được tạo mới hoặc chỉnh sửa trong dự án UniManage. Nếu vi phạm, output sẽ bị coi là FAILED.

## 1. TÀI LIỆU VÀ COMMENTS (CRITICAL)
- **TUYỆT ĐỐI BẮT BUỘC**: TẤT CẢ các file code C# (Interface, Class, Model) đều **PHẢI CÓ** XML Summary comment (`<summary>...</summary>`).
- Áp dụng cho: Mọi `public class`, `public interface`, `public method`, `public property`, và `public field`.
- **KHÔNG NGOẠI LỆ**: Các properties trong file Model/DTO cũng phải comment từng property một.
- Comment logic phức tạp bằng tiếng Việt. KHÔNG đưa lịch sử chat vào comment.

## 2. BẮT BUỘC ĐỌC SKILL (MANDATORY SKILL USAGE)
- **TRƯỚC KHI BẮT ĐẦU CODE BACKEND**: Bạn (Agent) **BẮT BUỘC** phải gọi tool `view_file` để đọc nội dung file `.agents/skills/unimanage-backend/SKILL.md`.
- File này chứa các hướng dẫn chuyên sâu về kiến trúc (CQRS, EF Core + Dapper) và format code không thể bỏ qua. Nếu bạn sửa code backend mà chưa đọc file này, đó là LỖI NGHIÊM TRỌNG.

## 2. QUY TẮC ĐẶT TÊN (NAMING CONVENTION)
- Tất cả các class Model thao tác với DB hoặc trả về API **PHẢI** dùng hậu tố `{Tên}Model` (ví dụ: `ClientDbModel`, `IdentityUserModel`).
- **CẤM TUYỆT ĐỐI** hậu tố `Dto`.
- Command: `VerbNounCommand` (VD: `CreateUserCommand`).
- Query: `Get/List/FindNounQuery` (VD: `ListUsersQuery`).

## 3. KIẾN TRÚC CQRS VÀ BASE CLASSES
- **Command**: Chỉ thay đổi dữ liệu, không trả về entity đầy đủ (chỉ trả Id/Result). Bắt buộc kế thừa `BaseCommand`.
- **Query**: Chỉ đọc, trả về Model/DTO cho UI. Kế thừa `BaseListQuery` (danh sách) hoặc `BaseQuery` (object đơn).
- **Controller**: Chức năng routing mỏng, chỉ gọi `_mediator.Send(...)`. Kế thừa `BaseController` và truyền `HeaderInfo`.
- **1 Handler = 1 Command/Query**.
- **CẤM** dùng `SELECT *` trong mọi trường hợp.

## 4. DATABASE VÀ SQL FORMATTING (DAPPER & EF CORE)
- **CẤM**: Khai báo `using var dbContext = new DbContext(...)` NẰM BÊN TRONG block `try-catch`.
- **PHẢI**: Khai báo block `using (var dbContext = new DbContext(...))` BỌC BÊN NGOÀI block `try-catch` (có transaction).
- **KHI DÙNG RAW SQL (`@`"..."``)**: Xuống dòng cho TỪNG field trong `SELECT`, `INSERT`, `UPDATE` và canh lề.
- **KHI BUILD SQL ĐỘNG**: **BẮT BUỘC** sử dụng `System.Text.StringBuilder`. Cấm dùng `string.Join` hoặc toán tử `+`.

## 5. RESTful API STANDARDS
- **URL PHẢI LÀ DANH TỪ SỐ NHIỀU**, CẤM dùng động từ.
  - ĐÚNG: `GET /api/v1/users`, `GET /api/v1/users/{id}`, `POST /api/v1/users`, `PUT /api/v1/users/{id}`
  - SAI: `GET /api/v1/users/getList`, `POST /api/v1/users/create`, `GET /api/v1/menus/tree`
- Response phải tuân theo chuẩn bọc trong `ApiResponse<T>` hoặc `PagedResponse<T>`.
- **POSTMAN UPDATE**: MỖI LẦN tạo hoặc sửa đổi API, BẮT BUỘC phải cập nhật lại Postman Collection (thêm request, ghi chú tham số, url) và lưu vào thư mục `postman` của dự án.

## 6. UTILITIES VÀ HELPERS (KHÔNG TỰ VIẾT LẠI)
- **DatabaseHelper**: Dùng cho transaction (`ExecuteWithTransactionAsync`), `QueryPagingAsync`.
- **ResponseHelper**: Dùng cho mọi API response (`Success()`, `Error()`, `PagedSuccess()`).
- **PasswordHelper, ValidationHelper, StringHelper, DateTimeHelper, FileHelper, QueryHelper, TranslateHelper**: Phải sử dụng thay vì tự code.

## 7. CÁC TIÊU CHUẨN KHÁC
- **Validation**: Dùng FluentValidation với `DependentRules()` cho chuỗi phức tạp.
- **Localization**: Dùng `CoreResource` (T4 generated) cho TẤT CẢ API messages và label lỗi.
- **Logging**: Dùng `nameof(request.PropertyName)` cho tên tham số trong `CoreParamModel`.
- **Caching**: TẤT CẢ cache key phải lấy từ `CacheKeyConstant.cs`, không hardcode chuỗi string cho cache.
- **T4 Entities**: Bắt buộc build từ T4 template, không tự tạo class Entity thủ công.
- **Social Auth**: Sử dụng Strategy Pattern (`ISocialAuthProvider`).
