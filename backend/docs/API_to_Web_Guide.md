# Hướng dẫn tích hợp từ API tới Web (Frontend)

Tài liệu này mô tả các bước cần thiết để kết nối và gọi API từ hệ thống Backend (.NET WebApi & IdentityServer) sang hệ thống Web (React/HeroUI).

## 1. Kiến trúc hệ thống

- **IdentityServer (Port 5000/5001)**: Quản lý xác thực (Authentication), cấp phát token (Access Token, Refresh Token).
- **WebApi (Port 60944/60943)**: Cung cấp các endpoints nghiệp vụ. Nhận Access Token từ Web để xác thực quyền (Authorization).
- **Web Frontend**: Ứng dụng người dùng (Next.js/React).

## 2. Luồng đăng nhập (Login Flow)

Quá trình đăng nhập sử dụng **Resource Owner Password Credentials (ROPC)** (mặc dù chuẩn mới recommend Code + PKCE, nhưng hệ thống vẫn hỗ trợ ROPC cho nội bộ).

**Bước 1**: Web gửi POST request tới WebApi `/api/v1/auth/login`

```json
{
  "username": "admin",
  "password": "admin123",
  "deviceId": "web-browser-xxx",
  "deviceType": "Web"
}
```

**Bước 2**: WebApi gọi qua IdentityServer để lấy Token. Sau đó trả về cho Web:

```json
{
  "returnCode": 0,
  "message": "Đăng nhập thành công",
  "data": {
    "accessToken": "ey...",
    "refreshToken": "...",
    "expiresIn": 3600,
    "user": {
      "id": 1,
      "userCode": "admin",
      "displayName": "Quản trị viên"
    }
  }
}
```

**Bước 3**: Web lưu `accessToken` và `refreshToken` vào LocalStorage hoặc Cookie (khuyến nghị `httpOnly` Cookie cho bảo mật).

## 3. Cách gọi API nghiệp vụ

Mọi request từ Web tới các endpoint nghiệp vụ cần đính kèm Header:

```
Authorization: Bearer <accessToken>
```

Nếu Token hết hạn (nhận mã HTTP 401 Unauthorized), Web cần tự động gọi API `/api/v1/auth/refresh` bằng `refreshToken` để lấy `accessToken` mới, sau đó thử lại request bị lỗi (Retry).

## 4. Xử lý Lỗi (Error Handling)

Hệ thống API trả về kết quả theo cấu trúc chuẩn `ApiResponse<T>`:

- HTTP 200/400 đều có thể trả về chung format này (tùy thuộc vào config của Controller).
- Web cần kiểm tra trường `returnCode`. Nếu `returnCode != 0` nghĩa là có lỗi nghiệp vụ (ví dụ: sai mật khẩu, tài khoản bị khóa). Trường `message` chứa thông báo lỗi để hiển thị cho người dùng.

## 5. Danh sách mã lỗi phổ biến

- `returnCode: -1`: Lỗi hệ thống (Exception).
- `returnCode: 101`: Sai tài khoản/mật khẩu.
- `returnCode: 102`: Tài khoản bị khóa.
- (Xem thêm tại file test case hoặc enum `CoreApiReturnCode`).
