# Schema Migration với Tên Song Ngữ Anh-Việt

## 📋 Tổng quan

Migration này tạo lại toàn bộ schema địa danh với **tên song ngữ Anh-Việt** để hỗ trợ đa ngôn ngữ.

## 🎯 Schema Design

### Cấu trúc 2 cấp (2-level)

```
Country (Quốc gia)
└── Province (Tỉnh/Thành phố)
    └── Ward (Xã/Phường/Đặc khu)
```

**Bỏ District/Huyện** theo Nghị quyết 202/2025/QH15.

### Primary Key Design

-   ✅ **Code as PRIMARY KEY** thay vì Id
-   Lý do: Code ổn định, không thay đổi khi reseed database
-   Id vẫn được lưu nhưng chỉ là index phụ

## 📊 Bảng dữ liệu

### 1. ad_countries (Quốc gia)

```sql
CREATE TABLE ad_countries(
    Code NVARCHAR(20) PRIMARY KEY,      -- 'VN'
    NameVi NVARCHAR(100),               -- 'Việt Nam'
    NameEn NVARCHAR(100),               -- 'Vietnam'
    FullNameVi NVARCHAR(200),           -- 'Cộng hòa Xã hội chủ nghĩa Việt Nam'
    FullNameEn NVARCHAR(200),           -- 'Socialist Republic of Vietnam'
    CodeName VARCHAR(100),              -- 'vietnam' (slug)
    PhoneCode NVARCHAR(20),             -- '+84'
    ...
);
```

### 2. ad_administrative_regions (Vùng địa lý)

```sql
CREATE TABLE ad_administrative_regions(
    Id INT PRIMARY KEY,
    NameVi NVARCHAR(100),               -- 'Miền Bắc'
    NameEn NVARCHAR(100),               -- 'Northern'
    CodeName VARCHAR(100),              -- 'mien-bac'
    ...
);
```

**Data mẫu:**

-   1: Miền Bắc / Northern
-   2: Miền Trung / Central
-   3: Miền Nam / Southern

### 3. ad_administrative_units (Đơn vị hành chính)

```sql
CREATE TABLE ad_administrative_units(
    Id INT PRIMARY KEY,
    NameVi NVARCHAR(100),               -- 'Thành phố trực thuộc trung ương'
    NameEn NVARCHAR(100),               -- 'Municipality'
    ShortNameVi NVARCHAR(50),           -- 'Thành phố'
    ShortNameEn NVARCHAR(50),           -- 'City'
    ...
);
```

**Data mẫu:**
| Id | NameVi | NameEn | ShortNameVi | ShortNameEn |
|----|--------|--------|-------------|-------------|
| 1 | Thành phố trực thuộc trung ương | Municipality | Thành phố | City |
| 2 | Tỉnh | Province | Tỉnh | Province |
| 7 | Phường | Ward | Phường | Ward |
| 8 | Xã | Commune | Xã | Commune |
| 9 | Thị trấn | Township | Thị trấn | Township |
| 10 | Đặc khu | Special Zone | Đặc khu | Special Zone |

### 4. ad_provinces (Tỉnh/Thành phố)

```sql
CREATE TABLE ad_provinces(
    Code NVARCHAR(20) PRIMARY KEY,      -- '101' (Hà Nội)
    NameVi NVARCHAR(100),               -- 'Hà Nội'
    NameEn NVARCHAR(100),               -- 'Hanoi'
    FullNameVi NVARCHAR(200),           -- 'Thành phố Hà Nội'
    FullNameEn NVARCHAR(200),           -- 'Hanoi City'
    CodeName VARCHAR(100),              -- 'ha-noi'
    AdministrativeUnitId INT,           -- FK -> ad_administrative_units
    AdministrativeRegionId INT,         -- FK -> ad_administrative_regions
    CountryCode NVARCHAR(20),           -- FK -> ad_countries (default 'VN')
    ...
);
```

**Ví dụ:**

```sql
INSERT INTO ad_provinces
(Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode)
VALUES
('101', 'Hà Nội', 'Hanoi', 'Thành phố Hà Nội', 'Hanoi City', 'ha-noi', 1, 1, 'VN');
```

### 5. ad_wards (Xã/Phường/Đặc khu)

```sql
CREATE TABLE ad_wards(
    Code NVARCHAR(20) PRIMARY KEY,      -- '10105001'
    NameVi NVARCHAR(100),               -- 'Hoàn Kiếm'
    NameEn NVARCHAR(100),               -- 'Hoan Kiem'
    FullNameVi NVARCHAR(200),           -- 'Phường Hoàn Kiếm'
    FullNameEn NVARCHAR(200),           -- 'Hoan Kiem Ward'
    CodeName VARCHAR(100),              -- 'hoan-kiem'
    AdministrativeUnitId INT,           -- FK -> ad_administrative_units
    ProvinceCode NVARCHAR(20),          -- FK -> ad_provinces.Code
    ...
);
```

**Ví dụ:**

```sql
INSERT INTO ad_wards
(Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode)
VALUES
('10105001', 'Hoàn Kiếm', 'Hoan Kiem', 'Phường Hoàn Kiếm', 'Hoan Kiem Ward', 'hoan-kiem', 7, '101');
```

## 🔄 Translation Logic

### Province Names

-   Dùng dictionary ánh xạ chính thức:
    -   "Hà Nội" → "Hanoi"
    -   "Hồ Chí Minh" → "Ho Chi Minh City"
    -   "Đà Nẵng" → "Da Nang"

### Ward Names

-   Transliterate Vietnamese → ASCII:
    -   "Hoàn Kiếm" → "Hoan Kiem"
    -   "Ngọc Hà" → "Ngoc Ha"
    -   "Ba Đình" → "Ba Dinh"

### FullName Generation

-   FullNameVi: `{Type} {Name}` → "Phường Hoàn Kiếm"
-   FullNameEn: `{Name} {TypeEN}` → "Hoan Kiem Ward"

### CodeName (Slug)

-   Convert Vietnamese → lowercase ASCII
-   Replace spaces với `-`
-   "Hoàn Kiếm" → "hoan-kiem"

## 📁 Files

### 1. MigrateToITExpressSchema.sql

-   **DROP** old tables: `ad_wards`, `ad_districts`, `ad_provinces`, etc.
-   **CREATE** new tables với tên song ngữ
-   **INSERT** reference data:
    -   Countries (VN)
    -   Administrative Regions (3 miền)
    -   Administrative Units (10 loại)

### 2. ImportBilingualData.sql

-   Generated bởi `transform_to_bilingual.py`
-   **34 provinces** với NameVi + NameEn
-   **3321 wards** với FullNameVi + FullNameEn
-   All data từ ITExpressLocation.sql

### 3. transform_to_bilingual.py

-   Parse ITExpressLocation.sql
-   Translate Vietnamese → English
-   Generate INSERT statements
-   Features:
    -   Regex-based parser (handle Vietnamese characters)
    -   Province translation dictionary
    -   Ward transliteration
    -   SQL escaping

## 🚀 Execution Plan

### Step 1: Drop & Create Schema

```powershell
sqlcmd -S "TCPHUONG\SQLEXPRESS" -d UniManage -U uni_manager -P "uni_manager@2024" -i "MigrateToITExpressSchema.sql"
```

### Step 2: Import Data

```powershell
sqlcmd -S "TCPHUONG\SQLEXPRESS" -d UniManage -U uni_manager -P "uni_manager@2024" -i "ImportBilingualData.sql"
```

### Step 3: Verify

```sql
-- Check counts
SELECT COUNT(*) AS TotalProvinces FROM ad_provinces;  -- Expect: 34
SELECT COUNT(*) AS TotalWards FROM ad_wards;          -- Expect: 3321

-- Check distribution
SELECT p.Code, p.NameVi, p.NameEn, COUNT(w.Code) AS WardCount
FROM ad_provinces p
LEFT JOIN ad_wards w ON w.ProvinceCode = p.Code
GROUP BY p.Code, p.NameVi, p.NameEn
ORDER BY WardCount DESC;

-- Sample data
SELECT TOP 5 * FROM ad_provinces;
SELECT TOP 5 * FROM ad_wards;
```

## ✅ Validation Checklist

-   [ ] 34 provinces imported
-   [ ] 3321 wards imported
-   [ ] All NameVi fields populated
-   [ ] All NameEn fields populated
-   [ ] Foreign keys working (ProvinceCode → Province.Code)
-   [ ] AdministrativeUnitId mapped correctly
-   [ ] AdministrativeRegionId mapped correctly (Miền Bắc/Trung/Nam)
-   [ ] CodeName slugs generated
-   [ ] Vietnamese characters display correctly

## 🎨 UI Usage Examples

### Dropdown Province (Vietnamese)

```typescript
provinces.map((p) => ({
    value: p.code,
    label: p.nameVi, // "Hà Nội"
}));
```

### Dropdown Province (English)

```typescript
provinces.map((p) => ({
    value: p.code,
    label: p.nameEn, // "Hanoi"
}));
```

### Display Full Name

```typescript
// Vietnamese
`${province.fullNameVi}`// English // "Thành phố Hà Nội"
`${province.fullNameEn}`; // "Hanoi City"
```

### SEO-friendly URL

```typescript
`/province/${province.codeName}` // "/province/ha-noi"
`/ward/${ward.codeName}`; // "/ward/hoan-kiem"
```

## 📊 Statistics

| Item                   | Count  | Status |
| ---------------------- | ------ | ------ |
| Countries              | 1 (VN) | ✅     |
| Administrative Regions | 3      | ✅     |
| Administrative Units   | 10     | ✅     |
| Provinces              | 34     | ✅     |
| Wards                  | 3321   | ✅     |

## 🔗 Relationships

```
ad_countries (Code='VN')
    ↓ (FK: CountryCode)
ad_provinces (Code='101')
    ↓ (FK: ProvinceCode)
ad_wards (Code='10105001')
```

## 📝 Notes

1. **Code Stability**: Sử dụng Code làm PK thay vì Id để tránh vấn đề khi reseed database
2. **Bilingual Support**: NameVi + NameEn cho đa ngôn ngữ
3. **SEO**: CodeName slug cho URL thân thiện
4. **Administrative Context**: AdministrativeUnitId + AdministrativeRegionId cho filtering
5. **2-level Structure**: Bỏ District theo Nghị quyết 202/2025/QH15

## 🎯 Next Steps

1. Execute migration scripts
2. Update backend models (C# entities)
3. Update API endpoints (CQRS handlers)
4. Update frontend dropdowns
5. Test bilingual display
6. Update documentation

---

**Generated:** 2025-10-11  
**Source:** ITExpressLocation.sql (34 provinces + 3321 wards)  
**Schema:** Code-based PK + Bilingual names
