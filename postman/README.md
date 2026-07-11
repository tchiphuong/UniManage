# Hướng dẫn sử dụng Postman Workspace - UniManage

Thư mục này chứa các file cấu hình, biến môi trường và collection Postman để test các API của dự án UniManage.

## 📂 Cấu trúc thư mục

- `/collections`: Chứa các file collection chính của dự án (ví dụ: `UniManage.postman_collection.json`).
- `/environments`: Chứa file biến môi trường (ví dụ: `UniManage.postman_environment.json`), giúp thay đổi linh hoạt các thông số như `baseUrl`, `accessToken`, v.v.
- `/globals`: Chứa các biến dùng chung cho toàn bộ workspace.
- `UniManage_IdentityServer.postman_collection.json`: Collection chuyên dụng để test luồng IdentityServer (Discovery, Client Credentials, Authorization Code + PKCE, UserInfo).

## 🚀 Cách cài đặt

1. Mở ứng dụng Postman.
2. Tại màn hình chính của Postman, chọn **Import** (hoặc nhấn `Ctrl + O` / `Cmd + O`).
3. Kéo thả hoặc chọn toàn bộ các file `.json` trong thư mục này (bao gồm collection và environment) để đưa vào Postman.

## ⚙️ Cấu hình Environment (Môi trường)

Sau khi import thành công, góc trên bên phải của màn hình Postman sẽ có một menu thả xuống để chọn Environment.
- Vui lòng chọn **UniManage Development** (hoặc môi trường tương ứng bạn vừa import).
- Bấm vào biểu tượng con mắt (Environment quick look) kế bên để kiểm tra và cập nhật lại các biến nếu cần. 
- Một số biến quan trọng cần chú ý:
  - `baseUrl`: Đường dẫn gốc của API (vd: `http://localhost:5297`).
  - `identity_url`: Đường dẫn của IdentityServer (vd: `https://localhost:5001`).
  - `api_url`: Đường dẫn của backend API.
  - Các biến liên quan đến user test: `testUserId`, `testUsername`,...

## 🔐 Hướng dẫn lấy Access Token

### 1. Test với API thông thường (UniManage Collection)
- Nếu dùng collection `UniManage`, hãy đảm bảo bạn đã lấy được token thông qua API Login (nếu có trong collection) hoặc dùng tab Authorization. Token thường sẽ được tự động lưu vào biến `accessToken` thông qua script ở tab **Tests** của API Login.

### 2. Test luồng Identity Server (Duende IdentityServer)
Sử dụng collection `UniManage IdentityServer`:
- **Luồng Machine-to-Machine (Client Credentials)**: Gọi API *Get Token (Client Credentials)*. Token sẽ tự động được set vào biến `access_token`.
- **Luồng Authorization Code + PKCE** (Luồng mặc định của dự án): 
  - Mở request *Get Token (Authorization Code + PKCE)*.
  - Chuyển sang tab **Authorization**.
  - Cuộn xuống dưới và bấm nút **Get New Access Token**.
  - Postman sẽ bật trình duyệt web nội bộ để bạn tiến hành đăng nhập.
  - Sau khi đăng nhập thành công, Postman sẽ nhận token. Bạn hãy chọn **Use Token**.

## 📝 Lưu ý

- Các giá trị `accessToken` và `refreshToken` nên được lưu ở cột **Current Value** trong Postman thay vì **Initial Value** để tránh bị commit nhầm thông tin nhạy cảm lên source code.
- Nếu bạn có bổ sung thêm API mới vào collection, nhớ Export lại ra file JSON và ghi đè vào thư mục này để đồng bộ với team nha!
