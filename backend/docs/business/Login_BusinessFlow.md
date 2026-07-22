# Business Flow: Authentication & Login

## Mục đích

Quản lý luồng xác thực người dùng và phân quyền truy cập (RBAC) thông qua IdentityServer và JWT.

## Quy trình nghiệp vụ

1. **Người dùng đăng nhập**:
   - Hệ thống (WebApi) nhận thông tin (Username, Password).
   - Kiểm tra trạng thái User (Active, Blocked).
   - Xác minh Identity qua IdentityServer (chuẩn OAuth2 ROPC/PKCE).
2. **Cấp phát quyền**:
   - Nếu thành công: Lấy Role và Permission gán vào Claims của token.
   - Ghi nhận Audit Log.
3. **Cơ chế Refresh**:
   - Access Token có hạn ngắn (ví dụ: 1h).
   - Web App tự động gia hạn qua `/api/v1/auth/refresh` bằng Refresh Token trước khi token hết hạn.
