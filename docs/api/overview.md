# Tổng Quan REST API Documentation

Tài liệu này mô tả định dạng giao tiếp RESTful API, chuẩn phản hồi dữ liệu và các quy tắc làm việc với API trong hệ thống UniManage.

---

## 1. Thông Tin Chung

- **Base URL (Development)**: `http://localhost:5297/api/v1`
- **IdentityServer URL**: `http://localhost:5000`
- **Format dữ liệu**: JSON (`Content-Type: application/json`)
- **Authentication**: JWT Bearer Token (`Authorization: Bearer <JWT_TOKEN>`)
- **Swagger Documentation**: `http://localhost:5297/swagger`

---

## 2. Cấu Trúc Phản Hồi Chuẩn (`Result<T>`)

Tất cả các API trong hệ thống UniManage đều trả về một cấu trúc phản hồi chuẩn hóa `Result<T>`:

### 2.1. Phản Hồi Thành Công (Success)

> [!IMPORTANT]
> Quy tắc vàng: Phản hồi được tính là THÀNH CÔNG khi và chỉ khi **`returnCode === 0`**.

```json
{
  "returnCode": 0,
  "message": "Thao tác thực hiện thành công",
  "data": {
    "uuid": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
    "username": "admin",
    "email": "admin@unimanage.com"
  },
  "errors": null
}
```

### 2.2. Phản Hồi Thất Bại / Lỗi Nghiệp Vụ (Error)

Khi xảy ra lỗi validation hoặc lỗi nghiệp vụ, `returnCode` sẽ mang giá trị khác 0 (ví dụ: `400`, `401`, `403`, `500`...):

```json
{
  "returnCode": 400,
  "message": "Dữ liệu đầu vào không hợp lệ",
  "data": null,
  "errors": [
    {
      "field": "email",
      "message": "Email không đúng định dạng"
    }
  ]
}
```

---

## 3. Cấu Trúc Phản Hồi Phân Trang (`PagedResult<T>`)

Các API truy vấn danh sách (Query List) sẽ trả về danh sách kèm thông tin phân trang:

```json
{
  "returnCode": 0,
  "message": "Thành công",
  "data": {
    "items": [
      { "uuid": "...", "username": "user1" },
      { "uuid": "...", "username": "user2" }
    ],
    "pageNumber": 1,
    "pageSize": 10,
    "totalCount": 45,
    "totalPages": 5,
    "hasPreviousPage": false,
    "hasNextPage": true
  },
  "errors": null
}
```

---

## 4. Xác Thực (Authentication Flow)

### 4.1. Đăng Nhập Lấy Token

- **Endpoint**: `POST http://localhost:5000/connect/token`
- **Header**: `Content-Type: application/x-www-form-urlencoded`
- **Body Parameters**:
  - `grant_type`: `password`
  - `client_id`: `unimanage_web`
  - `client_secret`: `secret`
  - `username`: `<Tên đăng nhập>`
  - `password`: `<Mật khẩu>`
  - `scope`: `openid profile api.unimanage offline_access`

### 4.2. Gửi Bearer Token Trong Request

Trong tất cả các yêu cầu tiếp theo gửi đến `UniManage.WebApi`, bắt buộc kèm theo Authorization Header:

```http
GET /api/v1/users HTTP/1.1
Host: localhost:5297
Authorization: Bearer eyJhbGciOiJSUzI1NiIsImtpZCI6...
```

---

## 5. Danh Sách Endpoint Module Hệ Thống (System Module)

| Phương thức | Endpoint | Mô tả |
| :--- | :--- | :--- |
| **POST** | `/api/v1/auth/login` | Đăng nhập tài khoản |
| **POST** | `/api/v1/auth/refresh-token` | Làm mới Access Token |
| **GET** | `/api/v1/users` | Lấy danh sách người dùng (có phân trang & lọc) |
| **GET** | `/api/v1/users/{uuid}` | Chi tiết người dùng theo UUID |
| **POST** | `/api/v1/users` | Tạo mới tài khoản người dùng |
| **PUT** | `/api/v1/users/{uuid}` | Cập nhật thông tin người dùng |
| **DELETE**| `/api/v1/users/{uuid}` | Xóa người dùng |
| **GET** | `/api/v1/roles` | Lấy danh sách vai trò hệ thống |
| **POST** | `/api/v1/roles` | Tạo mới vai trò & gán quyền |
| **GET** | `/api/v1/configs` | Lấy danh sách cấu hình hệ thống |
