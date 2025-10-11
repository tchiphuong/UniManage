# 🗺️ Cập Nhật Dữ Liệu Tỉnh Thành Việt Nam 2025

## 📋 Tổng Quan

Script cập nhật dữ liệu **34 tỉnh/thành phố** của Việt Nam theo **Nghị quyết 202/2025/QH15** (có hiệu lực từ **12/6/2025**).

### 📊 Thống Kê

-   **Tổng số**: 34 tỉnh/thành (sau sắp xếp)
-   **Thành phố trực thuộc TW**: 6
    -   Hà Nội
    -   Hải Phòng
    -   Đà Nẵng
    -   Huế (mới được nâng cấp)
    -   Hồ Chí Minh
    -   Cần Thơ
-   **Tỉnh**: 28

### 🔄 Sắp Xếp Đơn Vị Hành Chính

**11 Tỉnh/Thành KHÔNG sắp xếp:**

-   Miền Bắc: Hà Nội, Cao Bằng, Điện Biên, Lai Châu, Sơn La, Lạng Sơn, Quảng Ninh
-   Miền Trung: Thanh Hóa, Nghệ An, Hà Tĩnh, Huế

**23 Tỉnh/Thành MỚI** (hình thành sau sắp xếp):

-   52 tỉnh/thành cũ → sáp nhập thành 23 đơn vị mới
-   Chính quyền địa phương mới hoạt động từ **1/7/2025**

---

## 🚀 Cách Sử Dụng

### 1. Chạy SQL Script

```powershell
# Từ thư mục backend/scripts/
sqlcmd -S localhost -d UniManage -i UpdateProvinces_2025.sql
```

Hoặc mở file `UpdateProvinces_2025.sql` trong **SQL Server Management Studio** và execute.

### 2. Kiểm Tra Dữ Liệu

```sql
-- Tổng số tỉnh thành
SELECT COUNT(*) AS Total FROM ad_provinces;

-- Theo loại
SELECT
    CASE
        WHEN AdministrativeUnitId = 1 THEN N'Thành phố TW'
        WHEN AdministrativeUnitId = 2 THEN N'Tỉnh'
    END AS Type,
    COUNT(*) AS Count
FROM ad_provinces
GROUP BY AdministrativeUnitId;

-- Top 10 tỉnh thành
SELECT TOP 10
    Code,
    NameVi,
    FullNameVi,
    NameEn
FROM ad_provinces
ORDER BY Code;
```

---

## 🗂️ Cấu Trúc Bảng

### `ad_provinces` (Tỉnh/Thành phố)

| Cột                      | Type          | Mô tả                            |
| ------------------------ | ------------- | -------------------------------- |
| `Id`                     | INT           | Primary Key (IDENTITY)           |
| `Code`                   | NVARCHAR(20)  | Mã tỉnh (01-96)                  |
| `NameVi`                 | NVARCHAR(255) | Tên tiếng Việt (Hà Nội)          |
| `NameEn`                 | NVARCHAR(255) | Tên tiếng Anh (Ha Noi)           |
| `FullNameVi`             | NVARCHAR(255) | Tên đầy đủ TV (Thành phố Hà Nội) |
| `FullNameEn`             | NVARCHAR(255) | Tên đầy đủ TA (Ha Noi City)      |
| `CodeName`               | NVARCHAR(255) | Slug (ha_noi)                    |
| `AdministrativeUnitId`   | INT           | 1=Thành phố TW, 2=Tỉnh           |
| `AdministrativeRegionId` | INT           | Vùng địa lý (1-5)                |

### `ad_districts` (Quận/Huyện)

| Cột                    | Type          | Mô tả                       |
| ---------------------- | ------------- | --------------------------- |
| `Code`                 | NVARCHAR(20)  | Primary Key - Mã quận/huyện |
| `NameVi`               | NVARCHAR(255) | Tên tiếng Việt              |
| `NameEn`               | NVARCHAR(255) | Tên tiếng Anh               |
| `FullName`             | NVARCHAR(255) | Tên đầy đủ TV               |
| `FullNameEn`           | NVARCHAR(255) | Tên đầy đủ TA               |
| `CodeName`             | NVARCHAR(255) | Slug                        |
| `ProvinceCode`         | NVARCHAR(20)  | FK → ad_provinces.Code      |
| `AdministrativeUnitId` | INT           | Loại đơn vị hành chính      |

### `ad_wards` (Phường/Xã)

| Cột                    | Type          | Mô tả                      |
| ---------------------- | ------------- | -------------------------- |
| `Code`                 | NVARCHAR(20)  | Primary Key - Mã phường/xã |
| `NameVi`               | NVARCHAR(255) | Tên tiếng Việt             |
| `NameEn`               | NVARCHAR(255) | Tên tiếng Anh              |
| `FullNameVi`           | NVARCHAR(255) | Tên đầy đủ TV              |
| `FullNameEn`           | NVARCHAR(255) | Tên đầy đủ TA              |
| `CodeName`             | NVARCHAR(255) | Slug                       |
| `DistrictCode`         | NVARCHAR(20)  | FK → ad_districts.Code     |
| `AdministrativeUnitId` | INT           | Loại đơn vị hành chính     |

---

## 🔧 API Endpoints (Đề xuất)

### 1. Lấy Danh Sách Tỉnh/Thành

```http
GET /api/provinces
```

**Response:**

```json
{
    "returnCode": 0,
    "errors": [],
    "message": "Success",
    "data": [
        {
            "id": 1,
            "code": "01",
            "nameVi": "Hà Nội",
            "nameEn": "Ha Noi",
            "fullNameVi": "Thành phố Hà Nội",
            "fullNameEn": "Ha Noi City",
            "codeName": "ha_noi"
        }
    ]
}
```

### 2. Lấy Quận/Huyện Theo Tỉnh

```http
GET /api/provinces/{provinceCode}/districts
```

**Example:** `GET /api/provinces/01/districts`

### 3. Lấy Phường/Xã Theo Quận/Huyện

```http
GET /api/districts/{districtCode}/wards
```

**Example:** `GET /api/districts/001/wards`

---

## 📝 CQRS Implementation

### Query: GetProvinceListQuery

```csharp
// File: UniManage.Application/Queries/Master/Address/GetProvinceListQuery.cs

public class GetProvinceListQuery : IRequest<ApiResponse<List<GetProvinceListQuery.Result>>>
{
    public string? Keyword { get; set; }
    public int? RegionId { get; set; }

    public class Result
    {
        public int Id { get; set; }
        public string Code { get; set; } = default!;
        public string NameVi { get; set; } = default!;
        public string NameEn { get; set; } = default!;
        public string FullNameVi { get; set; } = default!;
        public string FullNameEn { get; set; } = default!;
        public string CodeName { get; set; } = default!;
    }
}

public class GetProvinceListQueryHandler
    : IRequestHandler<GetProvinceListQuery, ApiResponse<List<GetProvinceListQuery.Result>>>
{
    public async Task<ApiResponse<List<GetProvinceListQuery.Result>>> Handle(
        GetProvinceListQuery request,
        CancellationToken ct)
    {
        using (var dbContext = new DbContext())
        {
            var sql = @"
                SELECT
                    Id, Code, NameVi, NameEn,
                    FullNameVi, FullNameEn, CodeName
                FROM ad_provinces
                WHERE (@Keyword IS NULL
                    OR NameVi LIKE '%' + @Keyword + '%'
                    OR NameEn LIKE '%' + @Keyword + '%'
                    OR Code LIKE @Keyword + '%')
                AND (@RegionId IS NULL
                    OR AdministrativeRegionId = @RegionId)
                ORDER BY Code";

            var results = await dbContext.connection.QueryAsync<GetProvinceListQuery.Result>(
                sql,
                new { request.Keyword, request.RegionId });

            return ApiResponse<List<GetProvinceListQuery.Result>>.Success(results.ToList());
        }
    }
}
```

### Query: GetDistrictsByProvinceQuery

```csharp
// File: UniManage.Application/Queries/Master/Address/GetDistrictsByProvinceQuery.cs

public class GetDistrictsByProvinceQuery : IRequest<ApiResponse<List<GetDistrictsByProvinceQuery.Result>>>
{
    public string ProvinceCode { get; set; } = default!;

    public class Result
    {
        public string Code { get; set; } = default!;
        public string NameVi { get; set; } = default!;
        public string NameEn { get; set; } = default!;
        public string FullName { get; set; } = default!;
    }
}

public class GetDistrictsByProvinceQueryHandler
    : IRequestHandler<GetDistrictsByProvinceQuery, ApiResponse<List<GetDistrictsByProvinceQuery.Result>>>
{
    public async Task<ApiResponse<List<GetDistrictsByProvinceQuery.Result>>> Handle(
        GetDistrictsByProvinceQuery request,
        CancellationToken ct)
    {
        using (var dbContext = new DbContext())
        {
            var sql = @"
                SELECT
                    Code, NameVi, NameEn, FullName
                FROM ad_districts
                WHERE ProvinceCode = @ProvinceCode
                ORDER BY Code";

            var results = await dbContext.connection.QueryAsync<GetDistrictsByProvinceQuery.Result>(
                sql,
                new { request.ProvinceCode });

            return ApiResponse<List<GetDistrictsByProvinceQuery.Result>>.Success(results.ToList());
        }
    }
}
```

### Controller: AddressController

```csharp
// File: UniManage.Api/Controllers/Master/AddressController.cs

[ApiController]
[Route("api/provinces")]
public class AddressController : ControllerBase
{
    private readonly IMediator _mediator;

    public AddressController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Lấy danh sách tỉnh/thành phố
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<ApiResponse<List<GetProvinceListQuery.Result>>>> GetProvinces(
        [FromQuery] GetProvinceListQuery request,
        CancellationToken ct)
    {
        var response = await _mediator.Send(request, ct);
        return Ok(response);
    }

    /// <summary>
    /// Lấy danh sách quận/huyện theo tỉnh
    /// </summary>
    [HttpGet("{provinceCode}/districts")]
    public async Task<ActionResult<ApiResponse<List<GetDistrictsByProvinceQuery.Result>>>> GetDistricts(
        string provinceCode,
        CancellationToken ct)
    {
        var request = new GetDistrictsByProvinceQuery { ProvinceCode = provinceCode };
        var response = await _mediator.Send(request, ct);
        return Ok(response);
    }
}
```

---

## 🔍 Truy Vấn Hữu Ích

### Tìm tỉnh theo tên

```sql
SELECT * FROM ad_provinces
WHERE NameVi LIKE N'%Hà%'
   OR NameEn LIKE '%Ha%';
```

### Lấy tất cả quận/huyện của Hà Nội

```sql
SELECT d.*
FROM ad_districts d
WHERE d.ProvinceCode = '01'
ORDER BY d.Code;
```

### Lấy cây địa chỉ đầy đủ (Tỉnh → Quận → Phường)

```sql
SELECT
    p.FullNameVi AS Province,
    d.FullName AS District,
    w.FullNameVi AS Ward
FROM ad_wards w
INNER JOIN ad_districts d ON w.DistrictCode = d.Code
INNER JOIN ad_provinces p ON d.ProvinceCode = p.Code
WHERE p.Code = '01' -- Hà Nội
ORDER BY d.Code, w.Code;
```

---

## 📚 Tham Khảo

-   **Nguồn dữ liệu**: [Cổng thông tin Chính phủ](https://xaydungchinhsach.chinhphu.vn/)
-   **Nghị quyết**: 202/2025/QH15 (Quốc hội khóa XV)
-   **Hiệu lực**: 12/6/2025
-   **Chính quyền mới hoạt động**: 1/7/2025
-   **Cập nhật**: 11-10-2025

---

## ⚠️ Lưu Ý

1. **Backup trước khi chạy script** nếu đã có dữ liệu quan trọng
2. Script có thể **xóa dữ liệu cũ** (comment dòng DELETE nếu muốn giữ lại)
3. **Dữ liệu 34 tỉnh thành mới** theo sắp xếp Nghị quyết 202/2025/QH15
4. **Chính quyền mới** chính thức hoạt động từ **1/7/2025**
5. Dữ liệu **quận/huyện** và **phường/xã** cũng thay đổi theo (cần script riêng)
6. **AdministrativeUnitId** và **AdministrativeRegionId** cần có bảng lookup riêng
7. Một số tỉnh được **nâng cấp thành thành phố** (ví dụ: Huế)

---

## 📞 Support

Nếu cần thêm script cho **Quận/Huyện** hoặc **Phường/Xã**, vui lòng liên hệ team backend.

**Next Steps:**

-   [ ] Tạo script Quận/Huyện (713 records)
-   [ ] Tạo script Phường/Xã (~11,000 records)
-   [ ] Implement API endpoints
-   [ ] Tạo dropdown component cho Frontend
