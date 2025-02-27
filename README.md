# UniManage

## Giới thiệu

### Description
**UniManage** là một hệ thống quản lý giúp doanh nghiệp tối ưu hóa quy trình đặt hàng, theo dõi nhà cung cấp và kiểm soát dữ liệu sản phẩm. Dự án tập trung vào việc cung cấp giao diện thân thiện và API mạnh mẽ để quản lý chuỗi cung ứng hiệu quả.

**UniManage** là một hệ thống quản lý giúp doanh nghiệp tối ưu hóa quy trình đặt hàng, theo dõi nhà cung cấp và kiểm soát dữ liệu sản phẩm. Dự án được thiết kế với kiến trúc hiện đại, sử dụng ASP.NET cho backend và AngularJS cho frontend.

## Công nghệ sử dụng

### Backend:

- ASP.NET Web API
- SQL Server
- Dapper (ORM nhẹ)
- Migration Data
- Redis Cache
- JWT Authentication
- IIS (Internet Information Services)

### Frontend:

- AngularJS
- Tailwind CSS
- Select2
- Flatpickr
- NgMask

### DevOps:

- Docker
- CI/CD với GitHub Actions

## Tính năng chính

### 1️⃣ Quản lý nhà cung cấp
- Thêm, sửa, xóa nhà cung cấp
- Theo dõi lịch sử giao dịch

### 2️⃣ Quản lý đơn hàng
- Tạo đơn hàng, cập nhật trạng thái
- Lịch sử chỉnh sửa đơn hàng
- Theo dõi tiến độ xử lý

### 3️⃣ Quản lý sản phẩm
- Danh mục sản phẩm theo nhà cung cấp
- Giá nhập, đơn vị, số lượng tối thiểu

### 4️⃣ Thống kê & báo cáo
- Biểu đồ hiệu suất nhà cung cấp
- Tổng hợp sản phẩm nhập theo thời gian

## Cách cài đặt

### Yêu cầu hệ thống:

- .NET 8 SDK
- Node.js 16+
- SQL Server
- Docker (tùy chọn)
- IIS (Internet Information Services)

### Chạy Backend

```bash
cd backend
 dotnet restore
 dotnet run
```

### Triển khai Backend trên IIS

1. Mở **IIS Manager**.
2. Thêm **Application Pool** mới và chọn .NET CLR version phù hợp.
3. Thêm **Website** mới, trỏ đến thư mục publish của ứng dụng.
4. Cấu hình **Bindings** để chạy trên cổng mong muốn.
5. Khởi động website và kiểm tra.

### Chạy Frontend

```bash
cd frontend
npm install
npm start
```

## API Documentation

API được viết theo chuẩn RESTful, tài liệu chi tiết có thể xem tại: `http://localhost:5000/swagger`

