# Test Cases: Auth & Login

| Test Case ID | Tên Test Case | Mô tả | Đầu vào | Kết quả mong đợi | Trạng thái |
|---|---|---|---|---|---|
| **Đăng nhập cơ bản** | | | | | |
| TC_AUTH_01 | Đăng nhập thành công | Kiểm tra hệ thống cấp token khi user điền đúng user/pass | Username: `admin`, Pass: `admin123` | HTTP 200, trả về Access Token, Refresh Token, UserInfo | ✅ |
| TC_AUTH_02 | Đăng nhập thất bại - Sai mật khẩu | Kiểm tra hệ thống từ chối khi nhập sai pass | Username: `admin`, Pass: `wrong` | HTTP 400, thông báo sai mật khẩu (invalid_grant) | ✅ |
| TC_AUTH_03 | Đăng nhập thất bại - Username không tồn tại | Kiểm tra phản hồi khi nhập user chưa đăng ký | Username: `not_exist`, Pass: `test` | HTTP 400, thông báo tài khoản không tồn tại | |
| TC_AUTH_04 | Khóa tài khoản do nhập sai nhiều lần | Kiểm tra cơ chế tự động khóa account (Brute-force protection) | Nhập sai pass 5 lần liên tiếp | Tài khoản bị khóa trong 30 phút, HTTP 400 | ✅ |
| TC_AUTH_05 | Đăng nhập khi tài khoản bị vô hiệu hóa | Kiểm tra hệ thống từ chối tài khoản có Status = 'inactive' | Username: tài khoản bị khóa | HTTP 400, thông báo tài khoản đang bị khóa/vô hiệu hóa | ✅ |
| **Bảo mật & Rate Limiting** | | | | | |
| TC_AUTH_06 | Rate Limiting (Quá tải yêu cầu) | Chống spam API đăng nhập | Gọi API /auth/login 10 lần/giây | HTTP 429 Too Many Requests | |
| TC_AUTH_07 | Kiểm tra SQL Injection | Thử bypass đăng nhập bằng SQL injection | Username: `' OR 1=1 --` | HTTP 400, không bị lỗi server, từ chối đăng nhập | |
| **Refresh Token & Đăng xuất** | | | | | |
| TC_AUTH_08 | Refresh Token thành công | Cấp lại access token mới khi token cũ hết hạn | Refresh Token hợp lệ | HTTP 200, trả về Access Token mới | |
| TC_AUTH_09 | Refresh Token thất bại - Token hết hạn | Dùng refresh token đã hết hạn | Refresh Token hết hạn | HTTP 400, yêu cầu đăng nhập lại | |
| TC_AUTH_10 | Đăng xuất thành công | Thu hồi (revoke) token hiện tại | Access Token & Refresh Token hợp lệ | HTTP 200, Refresh token bị vô hiệu hóa | |
| **Đăng nhập Mạng xã hội & Sinh trắc học** | | | | | |
| TC_AUTH_11 | Đăng nhập Social (Google) | Đăng nhập qua token Google | Google ID Token hợp lệ | HTTP 200, tạo account hoặc đăng nhập | |
| TC_AUTH_12 | Biometric Challenge | Xin mã nonce để ký sinh trắc | Username, DeviceID | HTTP 200, trả về chuỗi challenge ngẫu nhiên | |
| TC_AUTH_13 | Đăng nhập Biometric thành công | Đăng nhập bằng FaceID/Vân tay | Chữ ký hợp lệ từ device | HTTP 200, trả về Token | |
| **Quên mật khẩu & Đổi mật khẩu** | | | | | |
| TC_AUTH_14 | Gửi yêu cầu quên mật khẩu | Gửi link reset mật khẩu qua email | Email hợp lệ của user | HTTP 200, email được gửi đi | |
| TC_AUTH_15 | Đổi mật khẩu thành công | User đổi mật khẩu từ trong app | Pass cũ đúng, pass mới hợp lệ | HTTP 200, mật khẩu được cập nhật | |

