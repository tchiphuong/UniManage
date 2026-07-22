# Hướng Dẫn Cài Đặt Môi Trường Phát Triển (Setup Guide)

Tài liệu này hướng dẫn chi tiết các bước cài đặt, cấu hình và khởi chạy dự án UniManage trên máy cục bộ (Local Development Environment).

---

## 1. Yêu Cầu Tiền Đề (Prerequisites)

Trước khi bắt đầu, hãy đảm bảo máy tính của bạn đã cài đặt các công cụ sau:

- **.NET 9 SDK**: [Download .NET 9](https://dotnet.microsoft.com/download/dotnet/9.0)
- **Node.js**: Phiên bản LTS 20.x hoặc mới hơn [Download Node.js](https://nodejs.org/)
- **Microsoft SQL Server**: Phiên bản 2019 / 2022 hoặc SQL Server Express.
- **SQL Server Management Studio (SSMS)** hoặc **Azure Data Studio**: Dùng để quản lý CSDL.
- **Git**: Quản lý mã nguồn.

---

## 2. Cài Đặt Cơ Sở Dữ Liệu (Database Setup)

1. Mở SSMS hoặc Azure Data Studio, kết nối đến SQL Server instance của bạn.
2. Tạo 2 Cơ sở dữ liệu mới:
   - `UniManage` (CSDL chính chứa thông tin nghiệp vụ và hệ thống)
   - `UniManageLog` (CSDL lưu trữ log audit và nhật ký hoạt động)
3. Chạy các script khởi tạo database trong thư mục `backend/scripts/`. 
   *(Lưu ý: Đọc file `backend/scripts/README.md` để chạy script đúng thứ tự số thứ tự file).*

---

## 3. Cài Đặt & Khởi Chạy Backend (.NET 9)

### Bước 1: Cấu hình Connection String

Mở file `appsettings.Development.json` trong dự án `UniManage.WebApi` và `UniManage.IdentityServer`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=UniManage;Trusted_Connection=True;TrustServerCertificate=True;",
    "LogConnection": "Server=YOUR_SERVER;Database=UniManageLog;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Bước 2: Restore & Build Solution

Mở Terminal tại thư mục gốc của dự án và thực thi:

```bash
cd backend/src
dotnet restore
dotnet build UniManage.sln
```

### Bước 3: Khởi chạy IdentityServer (Cổng Xác Thực)

Mở terminal mới và khởi chạy ứng dụng IdentityServer:

```bash
cd backend/src/03.Apps/UniManage.IdentityServer
dotnet run
```
- Server xác thực sẽ lắng nghe tại địa chỉ: `http://localhost:5000`

### Bước 4: Khởi chạy WebApi (API Service)

Mở terminal mới và khởi chạy ứng dụng WebApi:

```bash
cd backend/src/03.Apps/UniManage.WebApi
dotnet run
```
- API Service sẽ lắng nghe tại địa chỉ: `http://localhost:5297`
- Tài liệu Swagger UI: `http://localhost:5297/swagger` (chỉ hiển thị ở môi trường Development).

---

## 4. Cài Đặt & Khởi Chạy Frontend (Next.js 15)

### Bước 1: Di chuyển vào thư mục ứng dụng Frontend

```bash
cd frontend/uni-manage
```

### Bước 2: Cài đặt Dependencies

```bash
npm install
```

### Bước 3: Cấu hình Biến Môi Trường (.env.local)

Tạo file `.env.local` trong thư mục `frontend/uni-manage` với nội dung:

```env
NEXT_PUBLIC_API_URL=http://localhost:5297/api/v1
NEXT_PUBLIC_IDENTITY_URL=http://localhost:5000
```

### Bước 4: Khởi chạy Dev Server

```bash
npm run dev
```
- Ứng dụng Web sẽ chạy tại: `http://localhost:3000`

---

## 5. Khởi Chạy Nhanh Bằng Batch Script (Windows)

Dự án cung cấp sẵn các script khởi động nhanh tại thư mục gốc `UniManage/`:

- **`run-all.bat`**: Khởi chạy đồng thời IdentityServer, WebApi và Frontend Next.js.
- **`stop-all.bat`**: Dừng toàn bộ các tiến trình đang chạy của dự án.

```cmd
# Khởi chạy tất cả dịch vụ
run-all.bat

# Dừng tất cả dịch vụ
stop-all.bat
```
