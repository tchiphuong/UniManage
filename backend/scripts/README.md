# 🗺️ Bilingual Address Schema Migration

Migration dữ liệu địa danh sang schema song ngữ Anh-Việt theo Nghị quyết 202/2025/QH15.

## 📋 Execution Order

Thực thi theo thứ tự sau:

### 1️⃣ **Create Schema**

```bash
sqlcmd -S "TCPHUONG\SQLEXPRESS" -d UniManage -U uni_manager -P "uni_manager@2024" \
  -i "01_CreateBilingualSchema.sql"
```

**Actions:**

-   DROP old tables (ad_countries, ad_provinces, ad_wards, etc.)
-   CREATE new bilingual schema (5 tables)
-   INSERT reference data:
    -   1 Country (VN - Vietnam)
    -   3 Administrative Regions (Miền Bắc/Trung/Nam)
    -   10 Administrative Units (Thành phố, Tỉnh, Phường, Xã, etc.)

---

### 2️⃣ **Import Provinces & Wards**

```bash
sqlcmd -S "TCPHUONG\SQLEXPRESS" -d UniManage -U uni_manager -P "uni_manager@2024" \
  -i "02_ImportProvincesWards.sql"
```

**Data:**

-   ✅ **34 provinces** (Nghị quyết 202/2025/QH15)
    -   6 Cities: Hà Nội, TP.HCM, Hải Phòng, Đà Nẵng, Cần Thơ, Thủ Đô Hà Nội
    -   28 Provinces
-   ✅ **3,321 wards** (2-level structure: Province → Ward, NO districts)

**Bilingual columns:**

-   `NameVi` - Tên tiếng Việt (e.g., "Hà Nội", "Phường Hoàn Kiếm")
-   `NameEn` - Tên tiếng Anh (e.g., "Ha Noi", "Hoan Kiem Ward")
-   `FullNameVi` - Tên đầy đủ tiếng Việt (e.g., "Thành phố Hà Nội")
-   `FullNameEn` - Tên đầy đủ tiếng Anh (e.g., "Ha Noi City")

---

### 3️⃣ **Import Countries**

```bash
sqlcmd -S "TCPHUONG\SQLEXPRESS" -d UniManage -U uni_manager -P "uni_manager@2024" \
  -i "03_ImportCountries.sql"
```

**Data:**

-   ✅ **227 countries** with full schema (18 fields)
-   ✅ **30+ Vietnamese translations** (e.g., "Việt Nam", "Hoa Kỳ", "Trung Quốc")
-   ✅ Extended fields:
    -   `CountryType`, `CountrySubType`, `Sovereignty`
    -   `Capital`, `CurrencyCode`, `CurrencyName`
    -   `PhoneCode`, `CountryCode3`, `CountryNumber`
    -   `InternetCountryCode`, `Flags`

**Sample data:**
| Code | NameVi | NameEn | Capital | Currency | Phone |
|------|--------|--------|---------|----------|-------|
| VN | Việt Nam | Vietnam | Hanoi | VND | +84 |
| US | Hoa Kỳ | United States | Washington | USD | +1 |
| CN | Trung Quốc | China | Beijing | CNY | +86 |
| JP | Nhật Bản | Japan | Tokyo | JPY | +81 |

---

## 📊 Database Schema

### 2-Level Structure

```
ad_countries (Code PK)
└── ad_provinces (Code PK, CountryCode FK)
    └── ad_wards (Code PK, ProvinceCode FK)
```

### Reference Tables

-   `ad_administrative_regions` - 3 regions (Miền Bắc/Trung/Nam)
-   `ad_administrative_units` - 10 units (Thành phố, Tỉnh, Phường, Xã, etc.)

### Primary Key Design

-   ✅ **Code as PRIMARY KEY** (stable, no auto-increment)
-   ❌ No `Id` for primary key (Id kept as index only)

---

## 📁 Files

| File                           | Size   | Description                |
| ------------------------------ | ------ | -------------------------- |
| `00_SourceData_ITExpress.sql`  | 742 KB | Source data from ITExpress |
| `01_CreateBilingualSchema.sql` | 7 KB   | Schema migration           |
| `02_ImportProvincesWards.sql`  | 959 KB | 34 provinces + 3321 wards  |
| `03_ImportCountries.sql`       | 110 KB | 227 countries              |

---

## 📝 Documentation

-   **MIGRATION_BILINGUAL_SCHEMA.md** - Schema design & architecture
-   **MIGRATION_EXECUTION_REPORT.md** - Execution results & statistics
-   **README_PROVINCES.md** - Province migration details
-   **README_DISTRICTS_WARDS.md** - Districts/Wards migration details

---

## ✅ Verification Queries

```sql
-- Check counts
SELECT 'Countries' AS Entity, COUNT(*) AS Total FROM ad_countries
UNION ALL
SELECT 'Provinces', COUNT(*) FROM ad_provinces
UNION ALL
SELECT 'Wards', COUNT(*) FROM ad_wards;

-- Expected results:
-- Countries: 227
-- Provinces: 34
-- Wards: 3321

-- Check bilingual data
SELECT Code, NameVi, NameEn, Capital, PhoneCode
FROM ad_countries
WHERE Code IN ('VN','US','CN','JP','TH')
ORDER BY Code;

-- Check provinces
SELECT Code, NameVi, NameEn, FullNameVi, FullNameEn
FROM ad_provinces
ORDER BY Code;

-- Check wards sample
SELECT TOP 10 Code, NameVi, NameEn, ProvinceCode
FROM ad_wards
ORDER BY Code;
```

---

## 🔧 Troubleshooting

### Issue: Truncation errors

**Solution:** Already fixed - column sizes increased to NVARCHAR(50-100)

### Issue: FK constraint errors

**Solution:** Already fixed - FK disable/enable logic in scripts

### Issue: Duplicate key errors

**Solution:** Scripts include DELETE before INSERT

---

## 📅 Migration History

-   **2025-10-11** - Initial bilingual schema migration
    -   Created 5 tables with bilingual columns
    -   Imported 34 provinces (Nghị quyết 202/2025/QH15)
    -   Imported 3,321 wards (2-level structure)
    -   Imported 227 countries with full schema
    -   Vietnamese translations for 30+ countries

---

## 🎯 Next Steps

1. Update C# entities to match new schema
2. Update API responses with bilingual data
3. Implement language switching in frontend
4. Add more Vietnamese translations for remaining countries
5. Create API endpoints for:
    - `GET /api/countries?lang=vi|en`
    - `GET /api/provinces?lang=vi|en`
    - `GET /api/wards?provinceCode={code}&lang=vi|en`
