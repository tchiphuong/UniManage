# Cập nhật Xã/Phường - Cơ cấu 34 Tỉnh/Thành phố mới

## 📋 Tổng quan

Sau khi sắp xếp sáp nhập theo **Nghị quyết 202/2025/QH15**, Việt Nam có cơ cấu hành chính mới:

-   **34 tỉnh/thành phố** (giảm từ 63)
-   **3321 xã/phường/đặc khu** (trực thuộc tỉnh/thành phố)

⚠️ **LƯU Ý**: Hệ thống **BỎ CẤP QUẬN/HUYỆN** - Xã/Phường trực thuộc Tỉnh/Thành phố

## 📊 Thống kê 3321 đơn vị cấp xã

### Phân bổ theo tỉnh/thành phố

| STT | Tỉnh/Thành phố | Xã  | Phường | Đặc khu | Tổng     |
| --- | -------------- | --- | ------ | ------- | -------- |
| 1   | An Giang       | 85  | 14     | 3       | **102**  |
| 2   | Bắc Ninh       | 66  | 33     | 0       | **99**   |
| 3   | Cà Mau         | 55  | 9      | 0       | **64**   |
| 4   | Cần Thơ        | 72  | 31     | 0       | **103**  |
| 5   | Cao Bằng       | 53  | 3      | 0       | **56**   |
| 6   | Đà Nẵng        | 70  | 23     | 1       | **94**   |
| 7   | Đắk Lắk        | 88  | 14     | 0       | **102**  |
| 8   | Điện Biên      | 42  | 3      | 0       | **45**   |
| 9   | Đồng Nai       | 72  | 23     | 0       | **95**   |
| 10  | Đồng Tháp      | 82  | 20     | 0       | **102**  |
| 11  | Gia Lai        | 110 | 25     | 0       | **135**  |
| 12  | Hà Nội         | 75  | 51     | 0       | **126**  |
| 13  | Hà Tĩnh        | 60  | 9      | 0       | **69**   |
| 14  | Hải Phòng      | 67  | 45     | 2       | **114**  |
| 15  | Hưng Yên       | 93  | 11     | 0       | **104**  |
| 16  | Huế            | 19  | 21     | 0       | **40**   |
| 17  | Khánh Hòa      | 48  | 16     | 1       | **65**   |
| 18  | Lai Châu       | 36  | 2      | 0       | **38**   |
| 19  | Lâm Đồng       | 103 | 20     | 1       | **124**  |
| 20  | Lạng Sơn       | 61  | 4      | 0       | **65**   |
| 21  | Lào Cai        | 89  | 10     | 0       | **99**   |
| 22  | Nghệ An        | 119 | 11     | 0       | **130**  |
| 23  | ...            | ... | ...    | ...     | ...      |
|     | **TỔNG**       |     |        |         | **3321** |

## 🗂️ Cấu trúc Database

### 1. Bảng `ad_provinces` (34 tỉnh)

✅ **Đã hoàn thành** - File: `UpdateProvinces_2025.sql`

### 2. Bảng `ad_wards` (3321 Xã/Phường/Đặc khu)

⚠️ **Đang thiếu dữ liệu**

```sql
CREATE TABLE ad_wards (
    Code NVARCHAR(20) PRIMARY KEY,
    NameVi NVARCHAR(255) NOT NULL,
    NameEn NVARCHAR(255),
    FullNameVi NVARCHAR(255),
    FullNameEn NVARCHAR(255),
    CodeName NVARCHAR(255),           -- Slug: phuong-ben-nghe, xa-tan-phu
    ProvinceCode NVARCHAR(20) NOT NULL, -- ⚠️ Trực thuộc tỉnh (không qua quận/huyện)
    AdministrativeUnitId INT,          -- 7=Phường, 8=Xã, 9=Thị trấn, 10=Đặc khu
    CONSTRAINT FK_Ward_Province FOREIGN KEY (ProvinceCode)
        REFERENCES ad_provinces(Code)
);
```

**Administrative Unit IDs:**

-   `7` - Phường
-   `8` - Xã
-   `9` - Thị trấn
-   `10` - Đặc khu

⚠️ **Thay đổi quan trọng**:

-   Bỏ bảng `ad_districts` (quận/huyện)
-   Cột `DistrictCode` → đổi thành `ProvinceCode`
-   Xã/Phường trực thuộc Tỉnh/Thành phố

## 📚 Nguồn dữ liệu chính thức

### Trang web chính thức

🔗 [Danh sách 3321 đơn vị hành chính cấp xã](https://xaydungchinhsach.chinhphu.vn/danh-sach-3321-don-vi-hanh-chinh-cap-xa-tai-34-tinh-thanh-sau-sap-xep-sap-nhap-119250710102358656.htm)

### Nghị quyết riêng cho từng tỉnh

Mỗi tỉnh có 1 nghị quyết riêng chi tiết cách sắp xếp:

-   **Nghị quyết 1654/NQ-UBTVQH15** - An Giang
-   **Nghị quyết 1655/NQ-UBTVQH15** - Cà Mau
-   **Nghị quyết 1656/NQ-UBTVQH15** - Hà Nội
-   **Nghị quyết 1657/NQ-UBTVQH15** - Cao Bằng
-   ... (34 nghị quyết tổng cộng)

## 🛠️ Cách parse dữ liệu

### Phương án 1: Parse từ website chính thức (Khuyến nghị)

```python
# Sử dụng script: parse_districts_wards.py
python backend/scripts/parse_districts_wards.py
```

### Phương án 2: Thủ công từng tỉnh

1. Truy cập link nghị quyết của từng tỉnh
2. Copy danh sách xã/phường/đặc khu
3. Parse text → JSON → SQL

Ví dụ An Giang (102 đơn vị):

```
Sau khi sắp xếp, tỉnh An Giang có 102 đơn vị hành chính cấp xã,
gồm 85 xã, 14 phường và 03 đặc khu
```

### Phương án 3: API tìm kiếm địa chỉ

Một số API công khai:

-   https://provinces.open-api.vn
-   https://vnf-api.vercel.app

⚠️ **Lưu ý**: API public có thể chưa cập nhật theo cơ cấu 34 tỉnh mới

## 📝 Ví dụ SQL Insert

### Insert Ward (Xã/Phường) - Trực thuộc Tỉnh

```sql
-- Phường Bến Nghé, TP.HCM (Code tỉnh: 79)
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, CodeName, ProvinceCode, AdministrativeUnitId)
VALUES ('79001', N'Phường Bến Nghé', 'Ben Nghe Ward', N'Phường Bến Nghé', 'phuong-ben-nghe', '79', 7);

-- Phường Đa Kao, TP.HCM
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, CodeName, ProvinceCode, AdministrativeUnitId)
VALUES ('79002', N'Phường Đa Kao', 'Da Kao Ward', N'Phường Đa Kao', 'phuong-da-kao', '79', 7);

-- Xã Tân Phú, An Giang (Code tỉnh: 89)
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, CodeName, ProvinceCode, AdministrativeUnitId)
VALUES ('89001', N'Xã Tân Phú', 'Tan Phu Commune', N'Xã Tân Phú', 'xa-tan-phu', '89', 8);

-- Đặc khu, TP.HCM
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, CodeName, ProvinceCode, AdministrativeUnitId)
VALUES ('79999', N'Đặc khu Sài Gòn', 'Saigon Special Zone', N'Đặc khu Sài Gòn', 'dac-khu-sai-gon', '79', 10);
```

**Quy tắc mã code:**

-   Format: `{ProvinceCode}{SequentialNumber}`
-   Ví dụ: `79001`, `79002`, `79003`, ... (TP.HCM)
-   Ví dụ: `01001`, `01002`, ... (Hà Nội)

## 🚀 Hướng dẫn sử dụng

### Bước 1: Parse dữ liệu

```powershell
cd backend/scripts
python parse_wards.py  # Đổi tên từ parse_districts_wards.py
```

### Bước 2: Chạy SQL scripts

```sql
-- 1. Cập nhật tỉnh/thành phố (đã xong)
:r UpdateProvinces_2025.sql
GO

-- 2. Cập nhật xã/phường/đặc khu (3321 đơn vị)
-- ⚠️ BỎ BƯỚC DISTRICTS - không có quận/huyện
:r UpdateWards_2025.sql
GO
```

### Bước 3: Verify dữ liệu

```sql
-- Kiểm tra tổng số
SELECT
    (SELECT COUNT(*) FROM ad_provinces) AS Provinces,
    (SELECT COUNT(*) FROM ad_wards) AS Wards;
-- Expected: 34 | 3321

-- Kiểm tra quan hệ Tỉnh → Xã/Phường
SELECT
    p.NameVi AS Province,
    COUNT(w.Code) AS WardCount
FROM ad_provinces p
LEFT JOIN ad_wards w ON w.ProvinceCode = p.Code
GROUP BY p.Code, p.NameVi
ORDER BY p.Code;

-- Kiểm tra phân loại xã/phường/đặc khu
SELECT
    CASE AdministrativeUnitId
        WHEN 7 THEN N'Phường'
        WHEN 8 THEN N'Xã'
        WHEN 9 THEN N'Thị trấn'
        WHEN 10 THEN N'Đặc khu'
        ELSE N'Khác'
    END AS LoaiDonVi,
    COUNT(*) AS SoLuong
FROM ad_wards
GROUP BY AdministrativeUnitId;
```

## 📅 Timeline

| Mốc thời gian | Sự kiện                                         |
| ------------- | ----------------------------------------------- |
| 12/6/2025     | Nghị quyết có hiệu lực pháp luật                |
| 1/7/2025      | Chính quyền các đơn vị mới chính thức hoạt động |
| Q3/2025       | Hoàn tất các thủ tục hành chính                 |

## ⚠️ Lưu ý quan trọng

1. **Backup dữ liệu** trước khi chạy scripts update
2. **Test trên môi trường dev** trước khi apply lên production
3. **Cập nhật dần dần**: Tỉnh → Huyện → Xã
4. **Kiểm tra foreign keys**: Đảm bảo ProvinceCode, DistrictCode tồn tại
5. **Encoding**: Tất cả file SQL phải là UTF-8 với BOM

## 🔗 Liên kết tham khảo

-   [Nghị quyết 202/2025/QH15](https://xaydungchinhsach.chinhphu.vn)
-   [README Provinces](./README_PROVINCES.md)
-   [Update Provinces SQL](./UpdateProvinces_2025.sql)

## 📞 Hỗ trợ

Nếu gặp vấn đề trong quá trình parse hoặc import dữ liệu, vui lòng:

1. Kiểm tra logs trong console
2. Verify dữ liệu trên website chính thức
3. So sánh với nghị quyết gốc của từng tỉnh

---

**Cập nhật lần cuối**: Tháng 12/2024  
**Trạng thái**: ⚠️ Đang thu thập dữ liệu quận/huyện và xã/phường
