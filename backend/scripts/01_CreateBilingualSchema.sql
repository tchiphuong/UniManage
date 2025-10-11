-- =====================================================
-- MIGRATION: Tạo lại schema với tên song ngữ Anh-Việt
-- Cấu trúc: Country -> Province -> Ward (2-level)
-- Primary Key: Code (ổn định, không auto-increment)
-- =====================================================

USE UniManage;
GO

-- Drop old tables and constraints
DROP TABLE IF EXISTS ad_wards;
DROP TABLE IF EXISTS ad_districts;
DROP TABLE IF EXISTS ad_provinces;
DROP TABLE IF EXISTS ad_administrative_units;
DROP TABLE IF EXISTS ad_administrative_regions;
DROP TABLE IF EXISTS ad_countries;
GO

-- =====================================================
-- 1. COUNTRIES (Quốc gia)
-- =====================================================
CREATE TABLE ad_countries(
    Code NVARCHAR(20) NOT NULL PRIMARY KEY,
    NameVi NVARCHAR(100) NOT NULL,
    NameEn NVARCHAR(100) NOT NULL,
    FullNameVi NVARCHAR(200) NULL,
    FullNameEn NVARCHAR(200) NULL,
    CodeName VARCHAR(100) NULL, -- Slug for URL
    PhoneCode NVARCHAR(20) NULL,
    SortOrder INT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2(3) NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2(3) NULL
);
GO

-- =====================================================
-- 2. ADMINISTRATIVE REGIONS (Vùng địa lý)
-- =====================================================
CREATE TABLE ad_administrative_regions(
    Id INT NOT NULL PRIMARY KEY,
    NameVi NVARCHAR(100) NOT NULL,
    NameEn NVARCHAR(100) NOT NULL,
    CodeName VARCHAR(100) NULL,
    SortOrder INT NULL DEFAULT 0
);
GO

-- =====================================================
-- 3. ADMINISTRATIVE UNITS (Đơn vị hành chính)
-- =====================================================
CREATE TABLE ad_administrative_units(
    Id INT NOT NULL PRIMARY KEY,
    NameVi NVARCHAR(100) NOT NULL,
    NameEn NVARCHAR(100) NOT NULL,
    ShortNameVi NVARCHAR(50) NULL,
    ShortNameEn NVARCHAR(50) NULL,
    CodeName VARCHAR(100) NULL,
    SortOrder INT NULL DEFAULT 0
);
GO

-- =====================================================
-- 4. PROVINCES (Tỉnh/Thành phố)
-- =====================================================
CREATE TABLE ad_provinces(
    Code NVARCHAR(20) NOT NULL PRIMARY KEY, -- PK: Code thay vì Id
    NameVi NVARCHAR(100) NOT NULL,
    NameEn NVARCHAR(100) NOT NULL,
    FullNameVi NVARCHAR(200) NULL,
    FullNameEn NVARCHAR(200) NULL,
    CodeName VARCHAR(100) NULL, -- Slug: ha-noi, ho-chi-minh
    AdministrativeUnitId INT NULL, -- FK to ad_administrative_units (Thành phố/Tỉnh)
    AdministrativeRegionId INT NULL, -- FK to ad_administrative_regions (Miền Bắc/Trung/Nam)
    CountryCode NVARCHAR(20) NOT NULL DEFAULT 'VN', -- FK to ad_countries
    PhoneCode NVARCHAR(20) NULL,
    ZipCode NVARCHAR(20) NULL,
    SortOrder INT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2(3) NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2(3) NULL,
    CONSTRAINT FK_Province_Country FOREIGN KEY (CountryCode) REFERENCES ad_countries(Code),
    CONSTRAINT FK_Province_Unit FOREIGN KEY (AdministrativeUnitId) REFERENCES ad_administrative_units(Id),
    CONSTRAINT FK_Province_Region FOREIGN KEY (AdministrativeRegionId) REFERENCES ad_administrative_regions(Id)
);
GO

-- =====================================================
-- 5. WARDS (Xã/Phường/Thị trấn/Đặc khu)
-- 2-level structure: Province -> Ward (NO District)
-- =====================================================
CREATE TABLE ad_wards(
    Code NVARCHAR(20) NOT NULL PRIMARY KEY, -- PK: Code thay vì Id
    NameVi NVARCHAR(100) NOT NULL,
    NameEn NVARCHAR(100) NOT NULL,
    FullNameVi NVARCHAR(200) NULL,
    FullNameEn NVARCHAR(200) NULL,
    CodeName VARCHAR(100) NULL, -- Slug
    AdministrativeUnitId INT NULL, -- FK: Phường/Xã/Thị trấn/Đặc khu
    ProvinceCode NVARCHAR(20) NOT NULL, -- FK to ad_provinces.Code
    PhoneCode NVARCHAR(20) NULL,
    ZipCode NVARCHAR(20) NULL,
    SortOrder INT NOT NULL DEFAULT 0,
    IsActive BIT NOT NULL DEFAULT 1,
    CreatedAt DATETIME2(3) NOT NULL DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2(3) NULL,
    CONSTRAINT FK_Ward_Province FOREIGN KEY (ProvinceCode) REFERENCES ad_provinces(Code),
    CONSTRAINT FK_Ward_Unit FOREIGN KEY (AdministrativeUnitId) REFERENCES ad_administrative_units(Id)
);
GO

-- =====================================================
-- CREATE INDEXES
-- =====================================================
CREATE INDEX IX_provinces_country ON ad_provinces(CountryCode);
CREATE INDEX IX_provinces_region ON ad_provinces(AdministrativeRegionId);
CREATE INDEX IX_provinces_unit ON ad_provinces(AdministrativeUnitId);
CREATE INDEX IX_provinces_codename ON ad_provinces(CodeName);
CREATE INDEX IX_provinces_active ON ad_provinces(IsActive);

CREATE INDEX IX_wards_province ON ad_wards(ProvinceCode);
CREATE INDEX IX_wards_unit ON ad_wards(AdministrativeUnitId);
CREATE INDEX IX_wards_codename ON ad_wards(CodeName);
CREATE INDEX IX_wards_active ON ad_wards(IsActive);
GO

-- =====================================================
-- INSERT REFERENCE DATA
-- =====================================================

-- Countries
INSERT INTO ad_countries (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, PhoneCode, SortOrder)
VALUES ('VN', N'Việt Nam', 'Vietnam', N'Cộng hòa Xã hội chủ nghĩa Việt Nam', 'Socialist Republic of Vietnam', 'vietnam', '+84', 1);
GO

-- Administrative Regions (Miền)
INSERT INTO ad_administrative_regions (Id, NameVi, NameEn, CodeName, SortOrder) VALUES
(1, N'Miền Bắc', 'Northern', 'mien-bac', 1),
(2, N'Miền Trung', 'Central', 'mien-trung', 2),
(3, N'Miền Nam', 'Southern', 'mien-nam', 3);
GO

-- Administrative Units
INSERT INTO ad_administrative_units (Id, NameVi, NameEn, ShortNameVi, ShortNameEn, CodeName, SortOrder) VALUES
(1, N'Thành phố trực thuộc trung ương', 'Municipality', N'Thành phố', 'City', 'thanh-pho-truc-thuoc-tw', 1),
(2, N'Tỉnh', 'Province', N'Tỉnh', 'Province', 'tinh', 2),
(3, N'Huyện', 'District', N'Huyện', 'District', 'huyen', 3),
(4, N'Quận', 'Urban District', N'Quận', 'District', 'quan', 4),
(5, N'Thành phố thuộc tỉnh', 'Provincial City', N'Thành phố', 'City', 'thanh-pho-thuoc-tinh', 5),
(6, N'Thị xã', 'Town', N'Thị xã', 'Town', 'thi-xa', 6),
(7, N'Phường', 'Ward', N'Phường', 'Ward', 'phuong', 7),
(8, N'Xã', 'Commune', N'Xã', 'Commune', 'xa', 8),
(9, N'Thị trấn', 'Township', N'Thị trấn', 'Township', 'thi-tran', 9),
(10, N'Đặc khu', 'Special Zone', N'Đặc khu', 'Special Zone', 'dac-khu', 10);
GO

PRINT '✅ Schema với tên song ngữ đã tạo xong!';
PRINT 'Next: Import provinces và wards từ ITExpressLocation.sql';
GO
