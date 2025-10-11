-- =====================================================
-- BILINGUAL DATA IMPORT
-- Generated from ITExpressLocation.sql
-- 34 provinces, 3321 wards
-- =====================================================

USE UniManage;
GO

-- =====================================================
-- INSERT PROVINCES
-- =====================================================

INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'101', N'Hà Nội', N'Hanoi', N'Thành phố Hà Nội', N'Hanoi City', 'ha-noi', 1, 1, 'VN', NULL, NULL, 1, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'223', N'Bắc Ninh', N'Bac Ninh', N'Tỉnh Bắc Ninh', N'Bac Ninh Province', 'bac-ninh', 2, 1, 'VN', NULL, NULL, 2, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'225', N'Quảng Ninh', N'Quang Ninh', N'Tỉnh Quảng Ninh', N'Quang Ninh Province', 'quang-ninh', 2, 1, 'VN', NULL, NULL, 3, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'103', N'Hải Phòng', N'Hai Phong', N'Thành phố Hải Phòng', N'Hai Phong City', 'hai-phong', 1, 1, 'VN', NULL, NULL, 4, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'109', N'Hưng Yên', N'Hung Yen', N'Tỉnh Hưng Yên', N'Hung Yen Province', 'hung-yen', 2, 1, 'VN', NULL, NULL, 5, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'117', N'Ninh Bình', N'Ninh Binh', N'Tỉnh Ninh Bình', N'Ninh Binh Province', 'ninh-binh', 2, 1, 'VN', NULL, NULL, 6, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'203', N'Cao Bằng', N'Cao Bang', N'Tỉnh Cao Bằng', N'Cao Bang Province', 'cao-bang', 2, 1, 'VN', NULL, NULL, 7, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'211', N'Tuyên Quang', N'Tuyen Quang', N'Tỉnh Tuyên Quang', N'Tuyen Quang Province', 'tuyen-quang', 2, 1, 'VN', NULL, NULL, 8, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'205', N'Lào Cai', N'Lao Cai', N'Tỉnh Lào Cai', N'Lao Cai Province', 'lao-cai', 2, 1, 'VN', NULL, NULL, 9, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'215', N'Thái Nguyên', N'Thai Nguyen', N'Tỉnh Thái Nguyên', N'Thai Nguyen Province', 'thai-nguyen', 2, 1, 'VN', NULL, NULL, 10, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'209', N'Lạng Sơn', N'Lang Son', N'Tỉnh Lạng Sơn', N'Lang Son Province', 'lang-son', 2, 1, 'VN', NULL, NULL, 11, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'217', N'Phú Thọ', N'Phu Tho', N'Tỉnh Phú Thọ', N'Phu Tho Province', 'phu-tho', 2, 1, 'VN', NULL, NULL, 12, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'301', N'Điện Biên', N'Dien Bien', N'Tỉnh Điện Biên', N'Dien Bien Province', 'dien-bien', 2, 1, 'VN', NULL, NULL, 13, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'302', N'Lai Châu', N'Lai Chau', N'Tỉnh Lai Châu', N'Lai Chau Province', 'lai-chau', 2, 1, 'VN', NULL, NULL, 14, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'303', N'Sơn La', N'Son La', N'Tỉnh Sơn La', N'Son La Province', 'son-la', 2, 1, 'VN', NULL, NULL, 15, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'401', N'Thanh Hoá', N'Thanh Hoá', N'Tỉnh Thanh Hoá', N'Thanh Hoá Province', 'thanh-hoa', 2, 3, 'VN', NULL, NULL, 16, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'403', N'Nghệ An', N'Nghe An', N'Tỉnh Nghệ An', N'Nghe An Province', 'nghe-an', 2, 2, 'VN', NULL, NULL, 17, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'405', N'Hà Tĩnh', N'Ha Tinh', N'Tỉnh Hà Tĩnh', N'Ha Tinh Province', 'ha-tinh', 2, 2, 'VN', NULL, NULL, 18, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'409', N'Quảng Trị', N'Quang Tri', N'Tỉnh Quảng Trị', N'Quang Tri Province', 'quang-tri', 2, 2, 'VN', NULL, NULL, 19, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'411', N'Huế', N'Huế', N'Thành phố Huế', N'Huế City', 'hue', 1, 3, 'VN', NULL, NULL, 20, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'501', N'Đà Nẵng', N'Da Nang', N'Thành phố Đà Nẵng', N'Da Nang City', 'da-nang', 1, 2, 'VN', NULL, NULL, 21, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'505', N'Quảng Ngãi', N'Quang Ngai', N'Tỉnh Quảng Ngãi', N'Quang Ngai Province', 'quang-ngai', 2, 2, 'VN', NULL, NULL, 22, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'511', N'Khánh Hòa', N'Khanh Hoa', N'Tỉnh Khánh Hòa', N'Khanh Hoa Province', 'khanh-hoa', 2, 2, 'VN', NULL, NULL, 23, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'603', N'Gia Lai', N'Gia Lai', N'Tỉnh Gia Lai', N'Gia Lai Province', 'gia-lai', 2, 2, 'VN', NULL, NULL, 24, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'605', N'Đắk Lắk', N'Dak Lak', N'Tỉnh Đắk Lắk', N'Dak Lak Province', 'dak-lak', 2, 2, 'VN', NULL, NULL, 25, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'703', N'Lâm Đồng', N'Lam Dong', N'Tỉnh Lâm Đồng', N'Lam Dong Province', 'lam-dong', 2, 2, 'VN', NULL, NULL, 26, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'709', N'Tây Ninh', N'Tay Ninh', N'Tỉnh Tây Ninh', N'Tay Ninh Province', 'tay-ninh', 2, 3, 'VN', NULL, NULL, 27, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'713', N'Đồng Nai', N'Dong Nai', N'Tỉnh Đồng Nai', N'Dong Nai Province', 'dong-nai', 2, 3, 'VN', NULL, NULL, 28, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'701', N'Hồ Chí Minh', N'Ho Chi Minh City', N'Thành phố Hồ Chí Minh', N'Ho Chi Minh City City', 'ho-chi-minh', 1, 3, 'VN', NULL, NULL, 29, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'809', N'Vĩnh Long', N'Vinh Long', N'Tỉnh Vĩnh Long', N'Vinh Long Province', 'vinh-long', 2, 3, 'VN', NULL, NULL, 30, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'803', N'Đồng Tháp', N'Dong Thap', N'Tỉnh Đồng Tháp', N'Dong Thap Province', 'dong-thap', 2, 3, 'VN', NULL, NULL, 31, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'805', N'An Giang', N'An Giang', N'Tỉnh An Giang', N'An Giang Province', 'an-giang', 2, 3, 'VN', NULL, NULL, 32, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'815', N'Cần Thơ', N'Can Tho', N'Thành phố Cần Thơ', N'Can Tho City', 'can-tho', 1, 3, 'VN', NULL, NULL, 33, 1);
INSERT INTO ad_provinces (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, AdministrativeRegionId, CountryCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'823', N'Cà Mau', N'Ca Mau', N'Tỉnh Cà Mau', N'Ca Mau Province', 'ca-mau', 2, 3, 'VN', NULL, NULL, 34, 1);

GO

-- =====================================================
-- INSERT WARDS (3321 total)
-- =====================================================

INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10105001', N'Hoàn Kiếm', N'Hoan Kiem', N'Phường Hoàn Kiếm', N'Hoan Kiem Ward', 'hoan-kiem', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10105002', N'Cửa Nam', N'Cua Nam', N'Phường Cửa Nam', N'Cua Nam Ward', 'cua-nam', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10101003', N'Ba Đình', N'Ba Dinh', N'Phường Ba Đình', N'Ba Dinh Ward', 'ba-dinh', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10101004', N'Ngọc Hà', N'Ngoc Ha', N'Phường Ngọc Hà', N'Ngoc Ha Ward', 'ngoc-ha', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10101005', N'Giảng Võ', N'Giang Vo', N'Phường Giảng Võ', N'Giang Vo Ward', 'giang-vo', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10107006', N'Hai Bà Trưng', N'Hai Ba Trung', N'Phường Hai Bà Trưng', N'Hai Ba Trung Ward', 'hai-ba-trung', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10107007', N'Vĩnh Tuy', N'Vinh Tuy', N'Phường Vĩnh Tuy', N'Vinh Tuy Ward', 'vinh-tuy', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10107008', N'Bạch Mai', N'Bach Mai', N'Phường Bạch Mai', N'Bach Mai Ward', 'bach-mai', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10109009', N'Đống Đa', N'Dong Da', N'Phường Đống Đa', N'Dong Da Ward', 'dong-da', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10109010', N'Kim Liên', N'Kim Lien', N'Phường Kim Liên', N'Kim Lien Ward', 'kim-lien', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10109011', N'Văn Miếu - Quốc Tử Giám', N'Van Mieu - Quoc Tu Giam', N'Phường Văn Miếu - Quốc Tử Giám', N'Van Mieu - Quoc Tu Giam Ward', 'van-mieu-quoc-tu-giam', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10109012', N'Láng', N'Lang', N'Phường Láng', N'Lang Ward', 'lang', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10109013', N'Ô Chợ Dừa', N'O Cho Dua', N'Phường Ô Chợ Dừa', N'O Cho Dua Ward', 'o-cho-dua', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10103014', N'Hồng Hà', N'Hong Ha', N'Phường Hồng Hà', N'Hong Ha Ward', 'hong-ha', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10108015', N'Lĩnh Nam', N'Linh Nam', N'Phường Lĩnh Nam', N'Linh Nam Ward', 'linh-nam', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10108016', N'Hoàng Mai', N'Hoang Mai', N'Phường Hoàng Mai', N'Hoang Mai Ward', 'hoang-mai', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10108017', N'Vĩnh Hưng', N'Vinh Hung', N'Phường Vĩnh Hưng', N'Vinh Hung Ward', 'vinh-hung', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10108018', N'Tương Mai', N'Tuong Mai', N'Phường Tương Mai', N'Tuong Mai Ward', 'tuong-mai', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10108019', N'Định Công', N'Dinh Cong', N'Phường Định Công', N'Dinh Cong Ward', 'dinh-cong', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10123020', N'Hoàng Liệt', N'Hoang Liet', N'Phường Hoàng Liệt', N'Hoang Liet Ward', 'hoang-liet', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10108021', N'Yên Sở', N'Yen So', N'Phường Yên Sở', N'Yen So Ward', 'yen-so', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10111022', N'Thanh Xuân', N'Thanh Xuan', N'Phường Thanh Xuân', N'Thanh Xuan Ward', 'thanh-xuan', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10111023', N'Khương Đình', N'Khuong Dinh', N'Phường Khương Đình', N'Khuong Dinh Ward', 'khuong-dinh', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10111024', N'Phương Liệt', N'Phuong Liet', N'Phường Phương Liệt', N'Phuong Liet Ward', 'phuong-liet', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10113025', N'Cầu Giấy', N'Cau Giay', N'Phường Cầu Giấy', N'Cau Giay Ward', 'cau-giay', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10113026', N'Nghĩa Đô', N'Nghia Do', N'Phường Nghĩa Đô', N'Nghia Do Ward', 'nghia-do', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10113027', N'Yên Hoà', N'Yen Hoa', N'Phường Yên Hoà', N'Yen Hoa Ward', 'yen-hoa', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10103028', N'Tây Hồ', N'Tay Ho', N'Phường Tây Hồ', N'Tay Ho Ward', 'tay-ho', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10157029', N'Phú Thượng', N'Phu Thuong', N'Phường Phú Thượng', N'Phu Thuong Ward', 'phu-thuong', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10157030', N'Tây Tựu', N'Tay Tuu', N'Phường Tây Tựu', N'Tay Tuu Ward', 'tay-tuu', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10157031', N'Phú Diễn', N'Phu Dien', N'Phường Phú Diễn', N'Phu Dien Ward', 'phu-dien', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10157032', N'Xuân Đỉnh', N'Xuan Dinh', N'Phường Xuân Đỉnh', N'Xuan Dinh Ward', 'xuan-dinh', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10157033', N'Đông Ngạc', N'Dong Ngac', N'Phường Đông Ngạc', N'Dong Ngac Ward', 'dong-ngac', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10157034', N'Thượng Cát', N'Thuong Cat', N'Phường Thượng Cát', N'Thuong Cat Ward', 'thuong-cat', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10155035', N'Từ Liêm', N'Tu Liem', N'Phường Từ Liêm', N'Tu Liem Ward', 'tu-liem', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10155036', N'Xuân Phương', N'Xuan Phuong', N'Phường Xuân Phương', N'Xuan Phuong Ward', 'xuan-phuong', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10155037', N'Tây Mỗ', N'Tay Mo', N'Phường Tây Mỗ', N'Tay Mo Ward', 'tay-mo', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10155038', N'Đại Mỗ', N'Dai Mo', N'Phường Đại Mỗ', N'Dai Mo Ward', 'dai-mo', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10106039', N'Long Biên', N'Long Bien', N'Phường Long Biên', N'Long Bien Ward', 'long-bien', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10106040', N'Bồ Đề', N'Bo De', N'Phường Bồ Đề', N'Bo De Ward', 'bo-de', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10106041', N'Việt Hưng', N'Viet Hung', N'Phường Việt Hưng', N'Viet Hung Ward', 'viet-hung', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10106042', N'Phúc Lợi', N'Phuc Loi', N'Phường Phúc Lợi', N'Phuc Loi Ward', 'phuc-loi', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10127043', N'Hà Đông', N'Ha Dong', N'Phường Hà Đông', N'Ha Dong Ward', 'ha-dong', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10127044', N'Dương Nội', N'Duong Noi', N'Phường Dương Nội', N'Duong Noi Ward', 'duong-noi', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10127045', N'Yên Nghĩa', N'Yen Nghia', N'Phường Yên Nghĩa', N'Yen Nghia Ward', 'yen-nghia', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10127046', N'Phú Lương', N'Phu Luong', N'Phường Phú Lương', N'Phu Luong Ward', 'phu-luong', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10127047', N'Kiến Hưng', N'Kien Hung', N'Phường Kiến Hưng', N'Kien Hung Ward', 'kien-hung', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10123048', N'Thanh Trì', N'Thanh Tri', N'Xã Thanh Trì', N'Thanh Tri Commune', 'thanh-tri', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10123049', N'Đại Thanh', N'Dai Thanh', N'Xã Đại Thanh', N'Dai Thanh Commune', 'dai-thanh', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10123050', N'Nam Phù', N'Nam Phu', N'Xã Nam Phù', N'Nam Phu Commune', 'nam-phu', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10123051', N'Ngọc Hồi', N'Ngoc Hoi', N'Xã Ngọc Hồi', N'Ngoc Hoi Commune', 'ngoc-hoi', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10123052', N'Thanh Liệt', N'Thanh Liet', N'Phường Thanh Liệt', N'Thanh Liet Ward', 'thanh-liet', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10143053', N'Thượng Phúc', N'Thuong Phuc', N'Xã Thượng Phúc', N'Thuong Phuc Commune', 'thuong-phuc', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10143054', N'Thường Tín', N'Thuong Tin', N'Xã Thường Tín', N'Thuong Tin Commune', 'thuong-tin', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10143055', N'Chương Dương', N'Chuong Duong', N'Xã Chương Dương', N'Chuong Duong Commune', 'chuong-duong', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10143056', N'Hồng Vân', N'Hong Van', N'Xã Hồng Vân', N'Hong Van Commune', 'hong-van', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10149057', N'Phú Xuyên', N'Phu Xuyen', N'Xã Phú Xuyên', N'Phu Xuyen Commune', 'phu-xuyen', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10149058', N'Phượng Dực', N'Phuong Duc', N'Xã Phượng Dực', N'Phuong Duc Commune', 'phuong-duc', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10149059', N'Chuyên Mỹ', N'Chuyen My', N'Xã Chuyên Mỹ', N'Chuyen My Commune', 'chuyen-my', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10149060', N'Đại Xuyên', N'Dai Xuyen', N'Xã Đại Xuyên', N'Dai Xuyen Commune', 'dai-xuyen', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10141061', N'Thanh Oai', N'Thanh Oai', N'Xã Thanh Oai', N'Thanh Oai Commune', 'thanh-oai', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10141062', N'Bình Minh', N'Binh Minh', N'Xã Bình Minh', N'Binh Minh Commune', 'binh-minh', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10141063', N'Tam Hưng', N'Tam Hung', N'Xã Tam Hưng', N'Tam Hung Commune', 'tam-hung', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10141064', N'Dân Hoà', N'Dan Hoa', N'Xã Dân Hoà', N'Dan Hoa Commune', 'dan-hoa', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10147065', N'Vân Đình', N'Van Dinh', N'Xã Vân Đình', N'Van Dinh Commune', 'van-dinh', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10147066', N'Ứng Thiên', N'Ung Thien', N'Xã Ứng Thiên', N'Ung Thien Commune', 'ung-thien', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10147067', N'Hoà Xá', N'Hoa Xa', N'Xã Hoà Xá', N'Hoa Xa Commune', 'hoa-xa', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10147068', N'Ứng Hoà', N'Ung Hoa', N'Xã Ứng Hoà', N'Ung Hoa Commune', 'ung-hoa', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10145069', N'Mỹ Đức', N'My Duc', N'Xã Mỹ Đức', N'My Duc Commune', 'my-duc', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10145070', N'Hồng Sơn', N'Hong Son', N'Xã Hồng Sơn', N'Hong Son Commune', 'hong-son', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10145071', N'Phúc Sơn', N'Phuc Son', N'Xã Phúc Sơn', N'Phuc Son Commune', 'phuc-son', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10145072', N'Hương Sơn', N'Huong Son', N'Xã Hương Sơn', N'Huong Son Commune', 'huong-son', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10153073', N'Chương Mỹ', N'Chuong My', N'Phường Chương Mỹ', N'Chuong My Ward', 'chuong-my', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10153074', N'Phú Nghĩa', N'Phu Nghia', N'Xã Phú Nghĩa', N'Phu Nghia Commune', 'phu-nghia', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10153075', N'Xuân Mai', N'Xuan Mai', N'Xã Xuân Mai', N'Xuan Mai Commune', 'xuan-mai', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10153076', N'Trần Phú', N'Tran Phu', N'Xã Trần Phú', N'Tran Phu Commune', 'tran-phu', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10153077', N'Hoà Phú', N'Hoa Phu', N'Xã Hoà Phú', N'Hoa Phu Commune', 'hoa-phu', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10153078', N'Quảng Bị', N'Quang Bi', N'Xã Quảng Bị', N'Quang Bi Commune', 'quang-bi', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10151079', N'Minh Châu', N'Minh Chau', N'Xã Minh Châu', N'Minh Chau Commune', 'minh-chau', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10151080', N'Quảng Oai', N'Quang Oai', N'Xã Quảng Oai', N'Quang Oai Commune', 'quang-oai', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10151081', N'Vật Lại', N'Vat Lai', N'Xã Vật Lại', N'Vat Lai Commune', 'vat-lai', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10151082', N'Cổ Đô', N'Co Do', N'Xã Cổ Đô', N'Co Do Commune', 'co-do', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10151083', N'Bất Bạt', N'Bat Bat', N'Xã Bất Bạt', N'Bat Bat Commune', 'bat-bat', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10151084', N'Suối Hai', N'Suoi Hai', N'Xã Suối Hai', N'Suoi Hai Commune', 'suoi-hai', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10151085', N'Ba Vì', N'Ba Vi', N'Xã Ba Vì', N'Ba Vi Commune', 'ba-vi', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10151086', N'Yên Bài', N'Yen Bai', N'Xã Yên Bài', N'Yen Bai Commune', 'yen-bai', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10129087', N'Sơn Tây', N'Son Tay', N'Phường Sơn Tây', N'Son Tay Ward', 'son-tay', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10129088', N'Tùng Thiện', N'Tung Thien', N'Phường Tùng Thiện', N'Tung Thien Ward', 'tung-thien', 7, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10129089', N'Đoài Phương', N'Doai Phuong', N'Xã Đoài Phương', N'Doai Phuong Commune', 'doai-phuong', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10131090', N'Phúc Thọ', N'Phuc Tho', N'Xã Phúc Thọ', N'Phuc Tho Commune', 'phuc-tho', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10131091', N'Phúc Lộc', N'Phuc Loc', N'Xã Phúc Lộc', N'Phuc Loc Commune', 'phuc-loc', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10131092', N'Hát Môn', N'Hat Mon', N'Xã Hát Môn', N'Hat Mon Commune', 'hat-mon', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10135093', N'Thạch Thất', N'Thach That', N'Xã Thạch Thất', N'Thach That Commune', 'thach-that', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10135094', N'Hạ Bằng', N'Ha Bang', N'Xã Hạ Bằng', N'Ha Bang Commune', 'ha-bang', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10135095', N'Tây Phương', N'Tay Phuong', N'Xã Tây Phương', N'Tay Phuong Commune', 'tay-phuong', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10135096', N'Hoà Lạc', N'Hoa Lac', N'Xã Hoà Lạc', N'Hoa Lac Commune', 'hoa-lac', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10135097', N'Yên Xuân', N'Yen Xuan', N'Xã Yên Xuân', N'Yen Xuan Commune', 'yen-xuan', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10139098', N'Quốc Oai', N'Quoc Oai', N'Xã Quốc Oai', N'Quoc Oai Commune', 'quoc-oai', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10139099', N'Hưng Đạo', N'Hung Dao', N'Xã Hưng Đạo', N'Hung Dao Commune', 'hung-dao', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10139100', N'Kiều Phú', N'Kieu Phu', N'Xã Kiều Phú', N'Kieu Phu Commune', 'kieu-phu', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10139101', N'Phú Cát', N'Phu Cat', N'Xã Phú Cát', N'Phu Cat Commune', 'phu-cat', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10137102', N'Hoài Đức', N'Hoai Duc', N'Xã Hoài Đức', N'Hoai Duc Commune', 'hoai-duc', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10137103', N'Dương Hoà', N'Duong Hoa', N'Xã Dương Hoà', N'Duong Hoa Commune', 'duong-hoa', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10137104', N'Sơn Đồng', N'Son Dong', N'Xã Sơn Đồng', N'Son Dong Commune', 'son-dong', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10137105', N'An Khánh', N'An Khanh', N'Xã An Khánh', N'An Khanh Commune', 'an-khanh', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10133106', N'Đan Phượng', N'Dan Phuong', N'Xã Đan Phượng', N'Dan Phuong Commune', 'dan-phuong', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10133107', N'Ô Diên', N'O Dien', N'Xã Ô Diên', N'O Dien Commune', 'o-dien', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10133108', N'Liên Minh', N'Lien Minh', N'Xã Liên Minh', N'Lien Minh Commune', 'lien-minh', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10119109', N'Gia Lâm', N'Gia Lam', N'Xã Gia Lâm', N'Gia Lam Commune', 'gia-lam', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10119110', N'Thuận An', N'Thuan An', N'Xã Thuận An', N'Thuan An Commune', 'thuan-an', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10119111', N'Bát Tràng', N'Bat Trang', N'Xã Bát Tràng', N'Bat Trang Commune', 'bat-trang', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10119112', N'Phù Đổng', N'Phu Dong', N'Xã Phù Đổng', N'Phu Dong Commune', 'phu-dong', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10117113', N'Thư Lâm', N'Thu Lam', N'Xã Thư Lâm', N'Thu Lam Commune', 'thu-lam', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10117114', N'Đông Anh', N'Dong Anh', N'Xã Đông Anh', N'Dong Anh Commune', 'dong-anh', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10117115', N'Phúc Thịnh', N'Phuc Thinh', N'Xã Phúc Thịnh', N'Phuc Thinh Commune', 'phuc-thinh', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10117116', N'Thiên Lộc', N'Thien Loc', N'Xã Thiên Lộc', N'Thien Loc Commune', 'thien-loc', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10117117', N'Vĩnh Thanh', N'Vinh Thanh', N'Xã Vĩnh Thanh', N'Vinh Thanh Commune', 'vinh-thanh', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10125118', N'Mê Linh', N'Me Linh', N'Xã Mê Linh', N'Me Linh Commune', 'me-linh', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10125119', N'Yên Lãng', N'Yen Lang', N'Xã Yên Lãng', N'Yen Lang Commune', 'yen-lang', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10125120', N'Tiến Thắng', N'Tien Thang', N'Xã Tiến Thắng', N'Tien Thang Commune', 'tien-thang', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10125121', N'Quang Minh', N'Quang Minh', N'Xã Quang Minh', N'Quang Minh Commune', 'quang-minh', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10115122', N'Sóc Sơn', N'Soc Son', N'Xã Sóc Sơn', N'Soc Son Commune', 'soc-son', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10115123', N'Đa Phúc', N'Da Phuc', N'Xã Đa Phúc', N'Da Phuc Commune', 'da-phuc', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10115124', N'Nội Bài', N'Noi Bai', N'Xã Nội Bài', N'Noi Bai Commune', 'noi-bai', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10115125', N'Trung Giã', N'Trung Gia', N'Xã Trung Giã', N'Trung Gia Commune', 'trung-gia', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10115126', N'Kim Anh', N'Kim Anh', N'Xã Kim Anh', N'Kim Anh Commune', 'kim-anh', 8, N'101', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22113001', N'Đại Sơn', N'Dai Son', N'Xã Đại Sơn', N'Dai Son Commune', 'dai-son', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22113002', N'Sơn Động', N'Son Dong', N'Xã Sơn Động', N'Son Dong Commune', 'son-dong', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22113003', N'Tây Yên Tử', N'Tay Yen Tu', N'Xã Tây Yên Tử', N'Tay Yen Tu Commune', 'tay-yen-tu', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22113004', N'Dương Hưu', N'Duong Huu', N'Xã Dương Hưu', N'Duong Huu Commune', 'duong-huu', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22113005', N'Yên Định', N'Yen Dinh', N'Xã Yên Định', N'Yen Dinh Commune', 'yen-dinh', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22113006', N'An Lạc', N'An Lac', N'Xã An Lạc', N'An Lac Commune', 'an-lac', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22113007', N'Vân Sơn', N'Van Son', N'Xã Vân Sơn', N'Van Son Commune', 'van-son', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22107008', N'Biển Động', N'Bien Dong', N'Xã Biển Động', N'Bien Dong Commune', 'bien-dong', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22107009', N'Lục Ngạn', N'Luc Ngan', N'Xã Lục Ngạn', N'Luc Ngan Commune', 'luc-ngan', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22107010', N'Đèo Gia', N'Deo Gia', N'Xã Đèo Gia', N'Deo Gia Commune', 'deo-gia', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22107011', N'Sơn Hải', N'Son Hai', N'Xã Sơn Hải', N'Son Hai Commune', 'son-hai', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22107012', N'Tân Sơn', N'Tan Son', N'Xã Tân Sơn', N'Tan Son Commune', 'tan-son', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22107013', N'Biên Sơn', N'Bien Son', N'Xã Biên Sơn', N'Bien Son Commune', 'bien-son', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22107014', N'Sa Lý', N'Sa Ly', N'Xã Sa Lý', N'Sa Ly Commune', 'sa-ly', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22107015', N'Nam Dương', N'Nam Duong', N'Xã Nam Dương', N'Nam Duong Commune', 'nam-duong', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22121016', N'Kiên Lao', N'Kien Lao', N'Xã Kiên Lao', N'Kien Lao Commune', 'kien-lao', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22121017', N'Chũ', N'Chu', N'Phường Chũ', N'Chu Ward', 'chu', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22121018', N'Phượng Sơn', N'Phuong Son', N'Phường Phượng Sơn', N'Phuong Son Ward', 'phuong-son', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22115019', N'Lục Sơn', N'Luc Son', N'Xã Lục Sơn', N'Luc Son Commune', 'luc-son', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22115020', N'Trường Sơn', N'Truong Son', N'Xã Trường Sơn', N'Truong Son Commune', 'truong-son', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22115021', N'Cẩm Lý', N'Cam Ly', N'Xã Cẩm Lý', N'Cam Ly Commune', 'cam-ly', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22115022', N'Đông Phú', N'Dong Phu', N'Xã Đông Phú', N'Dong Phu Commune', 'dong-phu', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22115023', N'Nghĩa Phương', N'Nghia Phuong', N'Xã Nghĩa Phương', N'Nghia Phuong Commune', 'nghia-phuong', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22115024', N'Lục Nam', N'Luc Nam', N'Xã Lục Nam', N'Luc Nam Commune', 'luc-nam', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22115025', N'Bắc Lũng', N'Bac Lung', N'Xã Bắc Lũng', N'Bac Lung Commune', 'bac-lung', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22115026', N'Bảo Đài', N'Bao Dai', N'Xã Bảo Đài', N'Bao Dai Commune', 'bao-dai', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22111027', N'Lạng Giang', N'Lang Giang', N'Xã Lạng Giang', N'Lang Giang Commune', 'lang-giang', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22111028', N'Mỹ Thái', N'My Thai', N'Xã Mỹ Thái', N'My Thai Commune', 'my-thai', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22111029', N'Kép', N'Kep', N'Xã Kép', N'Kep Commune', 'kep', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22111030', N'Tân Dĩnh', N'Tan Dinh', N'Xã Tân Dĩnh', N'Tan Dinh Commune', 'tan-dinh', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22111031', N'Tiên Lục', N'Tien Luc', N'Xã Tiên Lục', N'Tien Luc Commune', 'tien-luc', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22103032', N'Yên Thế', N'Yen The', N'Xã Yên Thế', N'Yen The Commune', 'yen-the', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22103033', N'Bố Hạ', N'Bo Ha', N'Xã Bố Hạ', N'Bo Ha Commune', 'bo-ha', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22103034', N'Đồng Kỳ', N'Dong Ky', N'Xã Đồng Kỳ', N'Dong Ky Commune', 'dong-ky', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22103035', N'Xuân Lương', N'Xuan Luong', N'Xã Xuân Lương', N'Xuan Luong Commune', 'xuan-luong', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22103036', N'Tam Tiến', N'Tam Tien', N'Xã Tam Tiến', N'Tam Tien Commune', 'tam-tien', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22105037', N'Tân Yên', N'Tan Yen', N'Xã Tân Yên', N'Tan Yen Commune', 'tan-yen', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22105038', N'Ngọc Thiện', N'Ngoc Thien', N'Xã Ngọc Thiện', N'Ngoc Thien Commune', 'ngoc-thien', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22105039', N'Nhã Nam', N'Nha Nam', N'Xã Nhã Nam', N'Nha Nam Commune', 'nha-nam', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22105040', N'Phúc Hoà', N'Phuc Hoa', N'Xã Phúc Hoà', N'Phuc Hoa Commune', 'phuc-hoa', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22105041', N'Quang Trung', N'Quang Trung', N'Xã Quang Trung', N'Quang Trung Commune', 'quang-trung', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22109042', N'Hợp Thịnh', N'Hop Thinh', N'Xã Hợp Thịnh', N'Hop Thinh Commune', 'hop-thinh', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22109043', N'Hiệp Hoà', N'Hiep Hoa', N'Xã Hiệp Hoà', N'Hiep Hoa Commune', 'hiep-hoa', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22109044', N'Hoàng Vân', N'Hoang Van', N'Xã Hoàng Vân', N'Hoang Van Commune', 'hoang-van', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22109045', N'Xuân Cẩm', N'Xuan Cam', N'Xã Xuân Cẩm', N'Xuan Cam Commune', 'xuan-cam', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22117046', N'Tự Lạn', N'Tu Lan', N'Phường Tự Lạn', N'Tu Lan Ward', 'tu-lan', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22117047', N'Việt Yên', N'Viet Yen', N'Phường Việt Yên', N'Viet Yen Ward', 'viet-yen', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22117048', N'Nếnh', N'Nenh', N'Phường Nếnh', N'Nenh Ward', 'nenh', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22117049', N'Vân Hà', N'Van Ha', N'Phường Vân Hà', N'Van Ha Ward', 'van-ha', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22101050', N'Đồng Việt', N'Dong Viet', N'Xã Đồng Việt', N'Dong Viet Commune', 'dong-viet', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22101051', N'Bắc Giang', N'Bac Giang', N'Phường Bắc Giang', N'Bac Giang Ward', 'bac-giang', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22101052', N'Đa Mai', N'Da Mai', N'Phường Đa Mai', N'Da Mai Ward', 'da-mai', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22101053', N'Tiền Phong', N'Tien Phong', N'Phường Tiền Phong', N'Tien Phong Ward', 'tien-phong', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22101054', N'Tân An', N'Tan An', N'Phường Tân An', N'Tan An Ward', 'tan-an', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22101055', N'Yên Dũng', N'Yen Dung', N'Phường Yên Dũng', N'Yen Dung Ward', 'yen-dung', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22101056', N'Tân Tiến', N'Tan Tien', N'Phường Tân Tiến', N'Tan Tien Ward', 'tan-tien', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22101057', N'Cảnh Thụy', N'Canh Thuy', N'Phường Cảnh Thụy', N'Canh Thuy Ward', 'canh-thuy', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22301058', N'Kinh Bắc', N'Kinh Bac', N'Phường Kinh Bắc', N'Kinh Bac Ward', 'kinh-bac', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22301059', N'Võ Cường', N'Vo Cuong', N'Phường Võ Cường', N'Vo Cuong Ward', 'vo-cuong', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22301060', N'Vũ Ninh', N'Vu Ninh', N'Phường Vũ Ninh', N'Vu Ninh Ward', 'vu-ninh', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22301061', N'Hạp Lĩnh', N'Hap Linh', N'Phường Hạp Lĩnh', N'Hap Linh Ward', 'hap-linh', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22301062', N'Nam Sơn', N'Nam Son', N'Phường Nam Sơn', N'Nam Son Ward', 'nam-son', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22313063', N'Từ Sơn', N'Tu Son', N'Phường Từ Sơn', N'Tu Son Ward', 'tu-son', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22313064', N'Tam Sơn', N'Tam Son', N'Phường Tam Sơn', N'Tam Son Ward', 'tam-son', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22313065', N'Đồng Nguyên', N'Dong Nguyen', N'Phường Đồng Nguyên', N'Dong Nguyen Ward', 'dong-nguyen', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22313066', N'Phù Khê', N'Phu Khe', N'Phường Phù Khê', N'Phu Khe Ward', 'phu-khe', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22309067', N'Thuận Thành', N'Thuan Thanh', N'Phường Thuận Thành', N'Thuan Thanh Ward', 'thuan-thanh', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22309068', N'Mão Điền', N'Mao Dien', N'Phường Mão Điền', N'Mao Dien Ward', 'mao-dien', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22309069', N'Trạm Lộ', N'Tram Lo', N'Phường Trạm Lộ', N'Tram Lo Ward', 'tram-lo', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22309070', N'Trí Quả', N'Tri Qua', N'Phường Trí Quả', N'Tri Qua Ward', 'tri-qua', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22309071', N'Song Liễu', N'Song Lieu', N'Phường Song Liễu', N'Song Lieu Ward', 'song-lieu', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22309072', N'Ninh Xá', N'Ninh Xa', N'Phường Ninh Xá', N'Ninh Xa Ward', 'ninh-xa', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22305073', N'Quế Võ', N'Que Vo', N'Phường Quế Võ', N'Que Vo Ward', 'que-vo', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22305074', N'Phương Liễu', N'Phuong Lieu', N'Phường Phương Liễu', N'Phuong Lieu Ward', 'phuong-lieu', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22305075', N'Nhân Hoà', N'Nhan Hoa', N'Phường Nhân Hoà', N'Nhan Hoa Ward', 'nhan-hoa', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22305076', N'Đào Viên', N'Dao Vien', N'Phường Đào Viên', N'Dao Vien Ward', 'dao-vien', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22305077', N'Bồng Lai', N'Bong Lai', N'Phường Bồng Lai', N'Bong Lai Ward', 'bong-lai', 7, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22305078', N'Chi Lăng', N'Chi Lang', N'Xã Chi Lăng', N'Chi Lang Commune', 'chi-lang', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22305079', N'Phù Lãng', N'Phu Lang', N'Xã Phù Lãng', N'Phu Lang Commune', 'phu-lang', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22303080', N'Yên Phong', N'Yen Phong', N'Xã Yên Phong', N'Yen Phong Commune', 'yen-phong', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22303081', N'Văn Môn', N'Van Mon', N'Xã Văn Môn', N'Van Mon Commune', 'van-mon', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22303082', N'Tam Giang', N'Tam Giang', N'Xã Tam Giang', N'Tam Giang Commune', 'tam-giang', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22303083', N'Yên Trung', N'Yen Trung', N'Xã Yên Trung', N'Yen Trung Commune', 'yen-trung', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22303084', N'Tam Đa', N'Tam Da', N'Xã Tam Đa', N'Tam Da Commune', 'tam-da', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22307085', N'Tiên Du', N'Tien Du', N'Xã Tiên Du', N'Tien Du Commune', 'tien-du', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22307086', N'Liên Bão', N'Lien Bao', N'Xã Liên Bão', N'Lien Bao Commune', 'lien-bao', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22307087', N'Tân Chi', N'Tan Chi', N'Xã Tân Chi', N'Tan Chi Commune', 'tan-chi', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22307088', N'Đại Đồng', N'Dai Dong', N'Xã Đại Đồng', N'Dai Dong Commune', 'dai-dong', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22307089', N'Phật Tích', N'Phat Tich', N'Xã Phật Tích', N'Phat Tich Commune', 'phat-tich', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22315090', N'Gia Bình', N'Gia Binh', N'Xã Gia Bình', N'Gia Binh Commune', 'gia-binh', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22315091', N'Nhân Thắng', N'Nhan Thang', N'Xã Nhân Thắng', N'Nhan Thang Commune', 'nhan-thang', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22315092', N'Đại Lai', N'Dai Lai', N'Xã Đại Lai', N'Dai Lai Commune', 'dai-lai', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22315093', N'Cao Đức', N'Cao Duc', N'Xã Cao Đức', N'Cao Duc Commune', 'cao-duc', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22315094', N'Đông Cứu', N'Dong Cuu', N'Xã Đông Cứu', N'Dong Cuu Commune', 'dong-cuu', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22311095', N'Lương Tài', N'Luong Tai', N'Xã Lương Tài', N'Luong Tai Commune', 'luong-tai', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22311096', N'Lâm Thao', N'Lam Thao', N'Xã Lâm Thao', N'Lam Thao Commune', 'lam-thao', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22311097', N'Trung Chính', N'Trung Chinh', N'Xã Trung Chính', N'Trung Chinh Commune', 'trung-chinh', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22311098', N'Trung Kênh', N'Trung Kenh', N'Xã Trung Kênh', N'Trung Kenh Commune', 'trung-kenh', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22113099', N'Tuấn Đạo', N'Tuan Dao', N'Xã Tuấn Đạo', N'Tuan Dao Commune', 'tuan-dao', 8, N'223', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22521001', N'An Sinh', N'An Sinh', N'Phường An Sinh', N'An Sinh Ward', 'an-sinh', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22521002', N'Đông Triều', N'Dong Trieu', N'Phường Đông Triều', N'Dong Trieu Ward', 'dong-trieu', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22521003', N'Bình Khê', N'Binh Khe', N'Phường Bình Khê', N'Binh Khe Ward', 'binh-khe', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22521004', N'Mạo Khê', N'Mao Khe', N'Phường Mạo Khê', N'Mao Khe Ward', 'mao-khe', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22521005', N'Hoàng Quế', N'Hoang Que', N'Phường Hoàng Quế', N'Hoang Que Ward', 'hoang-que', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22505006', N'Yên Tử', N'Yen Tu', N'Phường Yên Tử', N'Yen Tu Ward', 'yen-tu', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22505007', N'Vàng Danh', N'Vang Danh', N'Phường Vàng Danh', N'Vang Danh Ward', 'vang-danh', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22505008', N'Uông Bí', N'Uong Bi', N'Phường Uông Bí', N'Uong Bi Ward', 'uong-bi', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22525009', N'Đông Mai', N'Dong Mai', N'Phường Đông Mai', N'Dong Mai Ward', 'dong-mai', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22525010', N'Hiệp Hoà', N'Hiep Hoa', N'Phường Hiệp Hoà', N'Hiep Hoa Ward', 'hiep-hoa', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22525011', N'Quảng Yên', N'Quang Yen', N'Phường Quảng Yên', N'Quang Yen Ward', 'quang-yen', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22525012', N'Hà An', N'Ha An', N'Phường Hà An', N'Ha An Ward', 'ha-an', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22525013', N'Phong Cốc', N'Phong Coc', N'Phường Phong Cốc', N'Phong Coc Ward', 'phong-coc', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22525014', N'Liên Hoà', N'Lien Hoa', N'Phường Liên Hoà', N'Lien Hoa Ward', 'lien-hoa', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22501015', N'Tuần Châu', N'Tuan Chau', N'Phường Tuần Châu', N'Tuan Chau Ward', 'tuan-chau', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22501016', N'Việt Hưng', N'Viet Hung', N'Phường Việt Hưng', N'Viet Hung Ward', 'viet-hung', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22501017', N'Bãi Cháy', N'Bai Chay', N'Phường Bãi Cháy', N'Bai Chay Ward', 'bai-chay', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22501018', N'Hà Tu', N'Ha Tu', N'Phường Hà Tu', N'Ha Tu Ward', 'ha-tu', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22501019', N'Hà Lầm', N'Ha Lam', N'Phường Hà Lầm', N'Ha Lam Ward', 'ha-lam', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22501020', N'Cao Xanh', N'Cao Xanh', N'Phường Cao Xanh', N'Cao Xanh Ward', 'cao-xanh', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22501021', N'Hồng Gai', N'Hong Gai', N'Phường Hồng Gai', N'Hong Gai Ward', 'hong-gai', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22501022', N'Hạ Long', N'Ha Long', N'Phường Hạ Long', N'Ha Long Ward', 'ha-long', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22501023', N'Hoành Bồ', N'Hoanh Bo', N'Phường Hoành Bồ', N'Hoanh Bo Ward', 'hoanh-bo', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22501024', N'Quảng La', N'Quang La', N'Xã Quảng La', N'Quang La Commune', 'quang-la', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22501025', N'Thống Nhất', N'Thong Nhat', N'Xã Thống Nhất', N'Thong Nhat Commune', 'thong-nhat', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22503026', N'Mông Dương', N'Mong Duong', N'Phường Mông Dương', N'Mong Duong Ward', 'mong-duong', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22503027', N'Quang Hanh', N'Quang Hanh', N'Phường Quang Hanh', N'Quang Hanh Ward', 'quang-hanh', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22503028', N'Cẩm Phả', N'Cam Pha', N'Phường Cẩm Phả', N'Cam Pha Ward', 'cam-pha', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22503029', N'Cửa Ông', N'Cua Ong', N'Phường Cửa Ông', N'Cua Ong Ward', 'cua-ong', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22503030', N'Hải Hoà', N'Hai Hoa', N'Xã Hải Hoà', N'Hai Hoa Commune', 'hai-hoa', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22513031', N'Tiên Yên', N'Tien Yen', N'Xã Tiên Yên', N'Tien Yen Commune', 'tien-yen', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22513032', N'Điền Xá', N'Dien Xa', N'Xã Điền Xá', N'Dien Xa Commune', 'dien-xa', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22513033', N'Đông Ngũ', N'Dong Ngu', N'Xã Đông Ngũ', N'Dong Ngu Commune', 'dong-ngu', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22513034', N'Hải Lạng', N'Hai Lang', N'Xã Hải Lạng', N'Hai Lang Commune', 'hai-lang', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22501035', N'Lương Minh', N'Luong Minh', N'Xã Lương Minh', N'Luong Minh Commune', 'luong-minh', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22515036', N'Kỳ Thượng', N'Ky Thuong', N'Xã Kỳ Thượng', N'Ky Thuong Commune', 'ky-thuong', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22515037', N'Ba Chẽ', N'Ba Che', N'Xã Ba Chẽ', N'Ba Che Commune', 'ba-che', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22527038', N'Quảng Tân', N'Quang Tan', N'Xã Quảng Tân', N'Quang Tan Commune', 'quang-tan', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22527039', N'Đầm Hà', N'Dam Ha', N'Xã Đầm Hà', N'Dam Ha Commune', 'dam-ha', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22511040', N'Quảng Hà', N'Quang Ha', N'Xã Quảng Hà', N'Quang Ha Commune', 'quang-ha', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22511041', N'Đường Hoa', N'Duong Hoa', N'Xã Đường Hoa', N'Duong Hoa Commune', 'duong-hoa', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22511042', N'Quảng Đức', N'Quang Duc', N'Xã Quảng Đức', N'Quang Duc Commune', 'quang-duc', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22507043', N'Hoành Mô', N'Hoanh Mo', N'Xã Hoành Mô', N'Hoanh Mo Commune', 'hoanh-mo', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22507044', N'Lục Hồn', N'Luc Hon', N'Xã Lục Hồn', N'Luc Hon Commune', 'luc-hon', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22507045', N'Bình Liêu', N'Binh Lieu', N'Xã Bình Liêu', N'Binh Lieu Commune', 'binh-lieu', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22509046', N'Hải Sơn', N'Hai Son', N'Xã Hải Sơn', N'Hai Son Commune', 'hai-son', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22509047', N'Hải Ninh', N'Hai Ninh', N'Xã Hải Ninh', N'Hai Ninh Commune', 'hai-ninh', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22509048', N'Vĩnh Thực', N'Vinh Thuc', N'Xã Vĩnh Thực', N'Vinh Thuc Commune', 'vinh-thuc', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22509049', N'Móng Cái 1', N'Mong Cai 1', N'Phường Móng Cái 1', N'Mong Cai 1 Ward', 'mong-cai-1', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22509050', N'Móng Cái 2', N'Mong Cai 2', N'Phường Móng Cái 2', N'Mong Cai 2 Ward', 'mong-cai-2', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22509051', N'Móng Cái 3', N'Mong Cai 3', N'Phường Móng Cái 3', N'Mong Cai 3 Ward', 'mong-cai-3', 7, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22517052', N'khu Vân Đồn', N'khu Van Don', N'Đặc khu Vân Đồn', N'khu Van Don Đặc', 'khu-van-don', 2, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22523053', N'khu Cô Tô', N'khu Co To', N'Đặc khu Cô Tô', N'khu Co To Đặc', 'khu-co-to', 2, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'22511054', N'Cái Chiên', N'Cai Chien', N'Xã Cái Chiên', N'Cai Chien Commune', 'cai-chien', 8, N'225', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10311001', N'Thuỷ Nguyên', N'Thuy Nguyen', N'Phường Thuỷ Nguyên', N'Thuy Nguyen Ward', 'thuy-nguyen', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10311002', N'Thiên Hương', N'Thien Huong', N'Phường Thiên Hương', N'Thien Huong Ward', 'thien-huong', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10311003', N'Hoà Bình', N'Hoa Binh', N'Phường Hoà Bình', N'Hoa Binh Ward', 'hoa-binh', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10311004', N'Nam Triệu', N'Nam Trieu', N'Phường Nam Triệu', N'Nam Trieu Ward', 'nam-trieu', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10311005', N'Bạch Đằng', N'Bach Dang', N'Phường Bạch Đằng', N'Bach Dang Ward', 'bach-dang', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10311006', N'Lưu Kiếm', N'Luu Kiem', N'Phường Lưu Kiếm', N'Luu Kiem Ward', 'luu-kiem', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10311007', N'Lê Ích Mộc', N'Le Ich Moc', N'Phường Lê Ích Mộc', N'Le Ich Moc Ward', 'le-ich-moc', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10301008', N'Hồng Bàng', N'Hong Bang', N'Phường Hồng Bàng', N'Hong Bang Ward', 'hong-bang', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10301009', N'Hồng An', N'Hong An', N'Phường Hồng An', N'Hong An Ward', 'hong-an', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10303010', N'Ngô Quyền', N'Ngo Quyen', N'Phường Ngô Quyền', N'Ngo Quyen Ward', 'ngo-quyen', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10303011', N'Gia Viên', N'Gia Vien', N'Phường Gia Viên', N'Gia Vien Ward', 'gia-vien', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10305012', N'Lê Chân', N'Le Chan', N'Phường Lê Chân', N'Le Chan Ward', 'le-chan', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10305013', N'An Biên', N'An Bien', N'Phường An Biên', N'An Bien Ward', 'an-bien', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10304014', N'Hải An', N'Hai An', N'Phường Hải An', N'Hai An Ward', 'hai-an', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10304015', N'Đông Hải', N'Dong Hai', N'Phường Đông Hải', N'Dong Hai Ward', 'dong-hai', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10307016', N'Kiến An', N'Kien An', N'Phường Kiến An', N'Kien An Ward', 'kien-an', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10307017', N'Phù Liễn', N'Phu Lien', N'Phường Phù Liễn', N'Phu Lien Ward', 'phu-lien', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10309018', N'Nam Đồ Sơn', N'Nam Do Son', N'Phường Nam Đồ Sơn', N'Nam Do Son Ward', 'nam-do-son', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10309019', N'Đồ Sơn', N'Do Son', N'Phường Đồ Sơn', N'Do Son Ward', 'do-son', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10327020', N'Hưng Đạo', N'Hung Dao', N'Phường Hưng Đạo', N'Hung Dao Ward', 'hung-dao', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10327021', N'Dương Kinh', N'Duong Kinh', N'Phường Dương Kinh', N'Duong Kinh Ward', 'duong-kinh', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10313022', N'An Dương', N'An Duong', N'Phường An Dương', N'An Duong Ward', 'an-duong', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10313023', N'An Hải', N'An Hai', N'Phường An Hải', N'An Hai Ward', 'an-hai', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10313024', N'An Phong', N'An Phong', N'Phường An Phong', N'An Phong Ward', 'an-phong', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10315025', N'An Hưng', N'An Hung', N'Xã An Hưng', N'An Hung Commune', 'an-hung', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10315026', N'An Khánh', N'An Khanh', N'Xã An Khánh', N'An Khanh Commune', 'an-khanh', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10315027', N'An Quang', N'An Quang', N'Xã An Quang', N'An Quang Commune', 'an-quang', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10315028', N'An Trường', N'An Truong', N'Xã An Trường', N'An Truong Commune', 'an-truong', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10315029', N'An Lão', N'An Lao', N'Xã An Lão', N'An Lao Commune', 'an-lao', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10317030', N'Kiến Thụy', N'Kien Thuy', N'Xã Kiến Thụy', N'Kien Thuy Commune', 'kien-thuy', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10317031', N'Kiến Minh', N'Kien Minh', N'Xã Kiến Minh', N'Kien Minh Commune', 'kien-minh', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10317032', N'Kiến Hải', N'Kien Hai', N'Xã Kiến Hải', N'Kien Hai Commune', 'kien-hai', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10317033', N'Kiến Hưng', N'Kien Hung', N'Xã Kiến Hưng', N'Kien Hung Commune', 'kien-hung', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10317034', N'Nghi Dương', N'Nghi Duong', N'Xã Nghi Dương', N'Nghi Duong Commune', 'nghi-duong', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10319035', N'Quyết Thắng', N'Quyet Thang', N'Xã Quyết Thắng', N'Quyet Thang Commune', 'quyet-thang', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10319036', N'Tiên Lãng', N'Tien Lang', N'Xã Tiên Lãng', N'Tien Lang Commune', 'tien-lang', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10319037', N'Tân Minh', N'Tan Minh', N'Xã Tân Minh', N'Tan Minh Commune', 'tan-minh', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10319038', N'Tiên Minh', N'Tien Minh', N'Xã Tiên Minh', N'Tien Minh Commune', 'tien-minh', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10319039', N'Chấn Hưng', N'Chan Hung', N'Xã Chấn Hưng', N'Chan Hung Commune', 'chan-hung', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10319040', N'Hùng Thắng', N'Hung Thang', N'Xã Hùng Thắng', N'Hung Thang Commune', 'hung-thang', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10321041', N'Vĩnh Bảo', N'Vinh Bao', N'Xã Vĩnh Bảo', N'Vinh Bao Commune', 'vinh-bao', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10321042', N'Nguyễn Bỉnh Khiêm', N'Nguyen Binh Khiem', N'Xã Nguyễn Bỉnh Khiêm', N'Nguyen Binh Khiem Commune', 'nguyen-binh-khiem', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10321043', N'Vĩnh Am', N'Vinh Am', N'Xã Vĩnh Am', N'Vinh Am Commune', 'vinh-am', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10321044', N'Vĩnh Hải', N'Vinh Hai', N'Xã Vĩnh Hải', N'Vinh Hai Commune', 'vinh-hai', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10321045', N'Vĩnh Hoà', N'Vinh Hoa', N'Xã Vĩnh Hoà', N'Vinh Hoa Commune', 'vinh-hoa', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10321046', N'Vĩnh Thịnh', N'Vinh Thinh', N'Xã Vĩnh Thịnh', N'Vinh Thinh Commune', 'vinh-thinh', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10321047', N'Vĩnh Thuận', N'Vinh Thuan', N'Xã Vĩnh Thuận', N'Vinh Thuan Commune', 'vinh-thuan', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10311048', N'Việt Khê', N'Viet Khe', N'Xã Việt Khê', N'Viet Khe Commune', 'viet-khe', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10323049', N'khu Cát Hải', N'khu Cat Hai', N'Đặc khu Cát Hải', N'khu Cat Hai Đặc', 'khu-cat-hai', 2, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10325050', N'khu Bạch Long Vĩ', N'khu Bach Long Vi', N'Đặc khu Bạch Long Vĩ', N'khu Bach Long Vi Đặc', 'khu-bach-long-vi', 2, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10701051', N'Hải Dương', N'Hai Duong', N'Phường Hải Dương', N'Hai Duong Ward', 'hai-duong', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10701052', N'Lê Thanh Nghị', N'Le Thanh Nghi', N'Phường Lê Thanh Nghị', N'Le Thanh Nghi Ward', 'le-thanh-nghi', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10701053', N'Việt Hoà', N'Viet Hoa', N'Phường Việt Hoà', N'Viet Hoa Ward', 'viet-hoa', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10701054', N'Thành Đông', N'Thanh Dong', N'Phường Thành Đông', N'Thanh Dong Ward', 'thanh-dong', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10701055', N'Nam Đồng', N'Nam Dong', N'Phường Nam Đồng', N'Nam Dong Ward', 'nam-dong', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10701056', N'Tân Hưng', N'Tan Hung', N'Phường Tân Hưng', N'Tan Hung Ward', 'tan-hung', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10701057', N'Thạch Khôi', N'Thach Khoi', N'Phường Thạch Khôi', N'Thach Khoi Ward', 'thach-khoi', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10717058', N'Tứ Minh', N'Tu Minh', N'Phường Tứ Minh', N'Tu Minh Ward', 'tu-minh', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10701059', N'Ái Quốc', N'Ai Quoc', N'Phường Ái Quốc', N'Ai Quoc Ward', 'ai-quoc', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10703060', N'Chu Văn An', N'Chu Van An', N'Phường Chu Văn An', N'Chu Van An Ward', 'chu-van-an', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10703061', N'Chí Linh', N'Chi Linh', N'Phường Chí Linh', N'Chi Linh Ward', 'chi-linh', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10703062', N'Trần Hưng Đạo', N'Tran Hung Dao', N'Phường Trần Hưng Đạo', N'Tran Hung Dao Ward', 'tran-hung-dao', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10703063', N'Nguyễn Trãi', N'Nguyen Trai', N'Phường Nguyễn Trãi', N'Nguyen Trai Ward', 'nguyen-trai', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10703064', N'Trần Nhân Tông', N'Tran Nhan Tong', N'Phường Trần Nhân Tông', N'Tran Nhan Tong Ward', 'tran-nhan-tong', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10703065', N'Lê Đại Hành', N'Le Dai Hanh', N'Phường Lê Đại Hành', N'Le Dai Hanh Ward', 'le-dai-hanh', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10709066', N'Kinh Môn', N'Kinh Mon', N'Phường Kinh Môn', N'Kinh Mon Ward', 'kinh-mon', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10709067', N'Nguyễn Đại Năng', N'Nguyen Dai Nang', N'Phường Nguyễn Đại Năng', N'Nguyen Dai Nang Ward', 'nguyen-dai-nang', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10709068', N'Trần Liễu', N'Tran Lieu', N'Phường Trần Liễu', N'Tran Lieu Ward', 'tran-lieu', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10709069', N'Bắc An Phụ', N'Bac An Phu', N'Phường Bắc An Phụ', N'Bac An Phu Ward', 'bac-an-phu', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10709070', N'Phạm Sư Mạnh', N'Pham Su Manh', N'Phường Phạm Sư Mạnh', N'Pham Su Manh Ward', 'pham-su-manh', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10709071', N'Nhị Chiểu', N'Nhi Chieu', N'Phường Nhị Chiểu', N'Nhi Chieu Ward', 'nhi-chieu', 7, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10709072', N'Nam An Phụ', N'Nam An Phu', N'Xã Nam An Phụ', N'Nam An Phu Commune', 'nam-an-phu', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10705073', N'Nam Sách', N'Nam Sach', N'Xã Nam Sách', N'Nam Sach Commune', 'nam-sach', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10705074', N'Thái Tân', N'Thai Tan', N'Xã Thái Tân', N'Thai Tan Commune', 'thai-tan', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10705075', N'Hợp Tiến', N'Hop Tien', N'Xã Hợp Tiến', N'Hop Tien Commune', 'hop-tien', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10705076', N'Trần Phú', N'Tran Phu', N'Xã Trần Phú', N'Tran Phu Commune', 'tran-phu', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10705077', N'An Phú', N'An Phu', N'Xã An Phú', N'An Phu Commune', 'an-phu', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10707078', N'Thanh Hà', N'Thanh Ha', N'Xã Thanh Hà', N'Thanh Ha Commune', 'thanh-ha', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10707079', N'Hà Tây', N'Ha Tay', N'Xã Hà Tây', N'Ha Tay Commune', 'ha-tay', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10707080', N'Hà Bắc', N'Ha Bac', N'Xã Hà Bắc', N'Ha Bac Commune', 'ha-bac', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10707081', N'Hà Nam', N'Ha Nam', N'Xã Hà Nam', N'Ha Nam Commune', 'ha-nam', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10707082', N'Hà Đông', N'Ha Dong', N'Xã Hà Đông', N'Ha Dong Commune', 'ha-dong', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10717083', N'Cẩm Giang', N'Cam Giang', N'Xã Cẩm Giang', N'Cam Giang Commune', 'cam-giang', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10717084', N'Tuệ Tĩnh', N'Tue Tinh', N'Xã Tuệ Tĩnh', N'Tue Tinh Commune', 'tue-tinh', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10717085', N'Mao Điền', N'Mao Dien', N'Xã Mao Điền', N'Mao Dien Commune', 'mao-dien', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10717086', N'Cẩm Giàng', N'Cam Giang', N'Xã Cẩm Giàng', N'Cam Giang Commune', 'cam-giang', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10719087', N'Kẻ Sặt', N'Ke Sat', N'Xã Kẻ Sặt', N'Ke Sat Commune', 'ke-sat', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10719088', N'Bình Giang', N'Binh Giang', N'Xã Bình Giang', N'Binh Giang Commune', 'binh-giang', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10719089', N'Đường An', N'Duong An', N'Xã Đường An', N'Duong An Commune', 'duong-an', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10719090', N'Thượng Hồng', N'Thuong Hong', N'Xã Thượng Hồng', N'Thuong Hong Commune', 'thuong-hong', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10713091', N'Gia Lộc', N'Gia Loc', N'Xã Gia Lộc', N'Gia Loc Commune', 'gia-loc', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10713092', N'Yết Kiêu', N'Yet Kieu', N'Xã Yết Kiêu', N'Yet Kieu Commune', 'yet-kieu', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10713093', N'Gia Phúc', N'Gia Phuc', N'Xã Gia Phúc', N'Gia Phuc Commune', 'gia-phuc', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10713094', N'Trường Tân', N'Truong Tan', N'Xã Trường Tân', N'Truong Tan Commune', 'truong-tan', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10715095', N'Tứ Kỳ', N'Tu Ky', N'Xã Tứ Kỳ', N'Tu Ky Commune', 'tu-ky', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10715096', N'Tân Kỳ', N'Tan Ky', N'Xã Tân Kỳ', N'Tan Ky Commune', 'tan-ky', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10715097', N'Đại Sơn', N'Dai Son', N'Xã Đại Sơn', N'Dai Son Commune', 'dai-son', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10715098', N'Chí Minh', N'Chi Minh', N'Xã Chí Minh', N'Chi Minh Commune', 'chi-minh', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10715099', N'Lạc Phượng', N'Lac Phuong', N'Xã Lạc Phượng', N'Lac Phuong Commune', 'lac-phuong', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10715100', N'Nguyên Giáp', N'Nguyen Giap', N'Xã Nguyên Giáp', N'Nguyen Giap Commune', 'nguyen-giap', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10723101', N'Ninh Giang', N'Ninh Giang', N'Xã Ninh Giang', N'Ninh Giang Commune', 'ninh-giang', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10723102', N'Vĩnh Lại', N'Vinh Lai', N'Xã Vĩnh Lại', N'Vinh Lai Commune', 'vinh-lai', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10723103', N'Khúc Thừa Dụ', N'Khuc Thua Du', N'Xã Khúc Thừa Dụ', N'Khuc Thua Du Commune', 'khuc-thua-du', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10723104', N'Tân An', N'Tan An', N'Xã Tân An', N'Tan An Commune', 'tan-an', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10723105', N'Hồng Châu', N'Hong Chau', N'Xã Hồng Châu', N'Hong Chau Commune', 'hong-chau', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10721106', N'Thanh Miện', N'Thanh Mien', N'Xã Thanh Miện', N'Thanh Mien Commune', 'thanh-mien', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10721107', N'Bắc Thanh Miện', N'Bac Thanh Mien', N'Xã Bắc Thanh Miện', N'Bac Thanh Mien Commune', 'bac-thanh-mien', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10721108', N'Hải Hưng', N'Hai Hung', N'Xã Hải Hưng', N'Hai Hung Commune', 'hai-hung', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10721109', N'Nguyễn Lương Bằng', N'Nguyen Luong Bang', N'Xã Nguyễn Lương Bằng', N'Nguyen Luong Bang Commune', 'nguyen-luong-bang', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10721110', N'Nam Thanh Miện', N'Nam Thanh Mien', N'Xã Nam Thanh Miện', N'Nam Thanh Mien Commune', 'nam-thanh-mien', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10711111', N'Phú Thái', N'Phu Thai', N'Xã Phú Thái', N'Phu Thai Commune', 'phu-thai', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10711112', N'Lai Khê', N'Lai Khe', N'Xã Lai Khê', N'Lai Khe Commune', 'lai-khe', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10711113', N'An Thành', N'An Thanh', N'Xã An Thành', N'An Thanh Commune', 'an-thanh', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10711114', N'Kim Thành', N'Kim Thanh', N'Xã Kim Thành', N'Kim Thanh Commune', 'kim-thanh', 8, N'103', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10901001', N'Phố Hiến', N'Pho Hien', N'Phường Phố Hiến', N'Pho Hien Ward', 'pho-hien', 7, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10901002', N'Sơn Nam', N'Son Nam', N'Phường Sơn Nam', N'Son Nam Ward', 'son-nam', 7, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10901003', N'Hồng Châu', N'Hong Chau', N'Phường Hồng Châu', N'Hong Chau Ward', 'hong-chau', 7, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10903004', N'Mỹ Hào', N'My Hao', N'Phường Mỹ Hào', N'My Hao Ward', 'my-hao', 7, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10903005', N'Đường Hào', N'Duong Hao', N'Phường Đường Hào', N'Duong Hao Ward', 'duong-hao', 7, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10903006', N'Thượng Hồng', N'Thuong Hong', N'Phường Thượng Hồng', N'Thuong Hong Ward', 'thuong-hong', 7, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10901007', N'Tân Hưng', N'Tan Hung', N'Xã Tân Hưng', N'Tan Hung Commune', 'tan-hung', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10913008', N'Hoàng Hoa Thám', N'Hoang Hoa Tham', N'Xã Hoàng Hoa Thám', N'Hoang Hoa Tham Commune', 'hoang-hoa-tham', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10913009', N'Tiên Lữ', N'Tien Lu', N'Xã Tiên Lữ', N'Tien Lu Commune', 'tien-lu', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10913010', N'Tiên Hoa', N'Tien Hoa', N'Xã Tiên Hoa', N'Tien Hoa Commune', 'tien-hoa', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10911011', N'Quang Hưng', N'Quang Hung', N'Xã Quang Hưng', N'Quang Hung Commune', 'quang-hung', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10911012', N'Đoàn Đào', N'Doan Dao', N'Xã Đoàn Đào', N'Doan Dao Commune', 'doan-dao', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10911013', N'Tiên Tiến', N'Tien Tien', N'Xã Tiên Tiến', N'Tien Tien Commune', 'tien-tien', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10911014', N'Tống Trân', N'Tong Tran', N'Xã Tống Trân', N'Tong Tran Commune', 'tong-tran', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10909015', N'Lương Bằng', N'Luong Bang', N'Xã Lương Bằng', N'Luong Bang Commune', 'luong-bang', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10909016', N'Nghĩa Dân', N'Nghia Dan', N'Xã Nghĩa Dân', N'Nghia Dan Commune', 'nghia-dan', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10909017', N'Hiệp Cường', N'Hiep Cuong', N'Xã Hiệp Cường', N'Hiep Cuong Commune', 'hiep-cuong', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10909018', N'Đức Hợp', N'Duc Hop', N'Xã Đức Hợp', N'Duc Hop Commune', 'duc-hop', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10907019', N'Ân Thi', N'An Thi', N'Xã Ân Thi', N'An Thi Commune', 'an-thi', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10907020', N'Xuân Trúc', N'Xuan Truc', N'Xã Xuân Trúc', N'Xuan Truc Commune', 'xuan-truc', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10907021', N'Phạm Ngũ Lão', N'Pham Ngu Lao', N'Xã Phạm Ngũ Lão', N'Pham Ngu Lao Commune', 'pham-ngu-lao', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10907022', N'Nguyễn Trãi', N'Nguyen Trai', N'Xã Nguyễn Trãi', N'Nguyen Trai Commune', 'nguyen-trai', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10907023', N'Hồng Quang', N'Hong Quang', N'Xã Hồng Quang', N'Hong Quang Commune', 'hong-quang', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10905024', N'Khoái Châu', N'Khoai Chau', N'Xã Khoái Châu', N'Khoai Chau Commune', 'khoai-chau', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10905025', N'Triệu Việt Vương', N'Trieu Viet Vuong', N'Xã Triệu Việt Vương', N'Trieu Viet Vuong Commune', 'trieu-viet-vuong', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10905026', N'Việt Tiến', N'Viet Tien', N'Xã Việt Tiến', N'Viet Tien Commune', 'viet-tien', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10905027', N'Chí Minh', N'Chi Minh', N'Xã Chí Minh', N'Chi Minh Commune', 'chi-minh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10905028', N'Châu Ninh', N'Chau Ninh', N'Xã Châu Ninh', N'Chau Ninh Commune', 'chau-ninh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10919029', N'Yên Mỹ', N'Yen My', N'Xã Yên Mỹ', N'Yen My Commune', 'yen-my', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10919030', N'Việt Yên', N'Viet Yen', N'Xã Việt Yên', N'Viet Yen Commune', 'viet-yen', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10919031', N'Hoàn Long', N'Hoan Long', N'Xã Hoàn Long', N'Hoan Long Commune', 'hoan-long', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10919032', N'Nguyễn Văn Linh', N'Nguyen Van Linh', N'Xã Nguyễn Văn Linh', N'Nguyen Van Linh Commune', 'nguyen-van-linh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10917033', N'Như Quỳnh', N'Nhu Quynh', N'Xã Như Quỳnh', N'Nhu Quynh Commune', 'nhu-quynh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10917034', N'Lạc Đạo', N'Lac Dao', N'Xã Lạc Đạo', N'Lac Dao Commune', 'lac-dao', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10917035', N'Đại Đồng', N'Dai Dong', N'Xã Đại Đồng', N'Dai Dong Commune', 'dai-dong', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10915036', N'Nghĩa Trụ', N'Nghia Tru', N'Xã Nghĩa Trụ', N'Nghia Tru Commune', 'nghia-tru', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10915037', N'Phụng Công', N'Phung Cong', N'Xã Phụng Công', N'Phung Cong Commune', 'phung-cong', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10915038', N'Văn Giang', N'Van Giang', N'Xã Văn Giang', N'Van Giang Commune', 'van-giang', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'10915039', N'Mễ Sở', N'Me So', N'Xã Mễ Sở', N'Me So Commune', 'me-so', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11501040', N'Thái Bình', N'Thai Binh', N'Phường Thái Bình', N'Thai Binh Ward', 'thai-binh', 7, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11501041', N'Trần Lãm', N'Tran Lam', N'Phường Trần Lãm', N'Tran Lam Ward', 'tran-lam', 7, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11501042', N'Trần Hưng Đạo', N'Tran Hung Dao', N'Phường Trần Hưng Đạo', N'Tran Hung Dao Ward', 'tran-hung-dao', 7, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11501043', N'Trà Lý', N'Tra Ly', N'Phường Trà Lý', N'Tra Ly Ward', 'tra-ly', 7, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11501044', N'Vũ Phúc', N'Vu Phuc', N'Phường Vũ Phúc', N'Vu Phuc Ward', 'vu-phuc', 7, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11507045', N'Thái Thụy', N'Thai Thuy', N'Xã Thái Thụy', N'Thai Thuy Commune', 'thai-thuy', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11507046', N'Đông Thụy Anh', N'Dong Thuy Anh', N'Xã Đông Thụy Anh', N'Dong Thuy Anh Commune', 'dong-thuy-anh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11507047', N'Bắc Thụy Anh', N'Bac Thuy Anh', N'Xã Bắc Thụy Anh', N'Bac Thuy Anh Commune', 'bac-thuy-anh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11507048', N'Thụy Anh', N'Thuy Anh', N'Xã Thụy Anh', N'Thuy Anh Commune', 'thuy-anh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11507049', N'Nam Thụy Anh', N'Nam Thuy Anh', N'Xã Nam Thụy Anh', N'Nam Thuy Anh Commune', 'nam-thuy-anh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11507050', N'Bắc Thái Ninh', N'Bac Thai Ninh', N'Xã Bắc Thái Ninh', N'Bac Thai Ninh Commune', 'bac-thai-ninh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11507051', N'Thái Ninh', N'Thai Ninh', N'Xã Thái Ninh', N'Thai Ninh Commune', 'thai-ninh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11507052', N'Đông Thái Ninh', N'Dong Thai Ninh', N'Xã Đông Thái Ninh', N'Dong Thai Ninh Commune', 'dong-thai-ninh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11507053', N'Nam Thái Ninh', N'Nam Thai Ninh', N'Xã Nam Thái Ninh', N'Nam Thai Ninh Commune', 'nam-thai-ninh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11507054', N'Tây Thái Ninh', N'Tay Thai Ninh', N'Xã Tây Thái Ninh', N'Tay Thai Ninh Commune', 'tay-thai-ninh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11507055', N'Tây Thụy Anh', N'Tay Thuy Anh', N'Xã Tây Thụy Anh', N'Tay Thuy Anh Commune', 'tay-thuy-anh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11515056', N'Tiền Hải', N'Tien Hai', N'Xã Tiền Hải', N'Tien Hai Commune', 'tien-hai', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11515057', N'Tây Tiền Hải', N'Tay Tien Hai', N'Xã Tây Tiền Hải', N'Tay Tien Hai Commune', 'tay-tien-hai', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11515058', N'Ái Quốc', N'Ai Quoc', N'Xã Ái Quốc', N'Ai Quoc Commune', 'ai-quoc', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11515059', N'Đồng Châu', N'Dong Chau', N'Xã Đồng Châu', N'Dong Chau Commune', 'dong-chau', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11515060', N'Đông Tiền Hải', N'Dong Tien Hai', N'Xã Đông Tiền Hải', N'Dong Tien Hai Commune', 'dong-tien-hai', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11515061', N'Nam Cường', N'Nam Cuong', N'Xã Nam Cường', N'Nam Cuong Commune', 'nam-cuong', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11515062', N'Hưng Phú', N'Hung Phu', N'Xã Hưng Phú', N'Hung Phu Commune', 'hung-phu', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11515063', N'Nam Tiền Hải', N'Nam Tien Hai', N'Xã Nam Tiền Hải', N'Nam Tien Hai Commune', 'nam-tien-hai', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11503064', N'Quỳnh Phụ', N'Quynh Phu', N'Xã Quỳnh Phụ', N'Quynh Phu Commune', 'quynh-phu', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11503065', N'Minh Thọ', N'Minh Tho', N'Xã Minh Thọ', N'Minh Tho Commune', 'minh-tho', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11503066', N'Nguyễn Du', N'Nguyen Du', N'Xã Nguyễn Du', N'Nguyen Du Commune', 'nguyen-du', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11503067', N'Quỳnh An', N'Quynh An', N'Xã Quỳnh An', N'Quynh An Commune', 'quynh-an', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11503068', N'Ngọc Lâm', N'Ngoc Lam', N'Xã Ngọc Lâm', N'Ngoc Lam Commune', 'ngoc-lam', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11503069', N'Đồng Bằng', N'Dong Bang', N'Xã Đồng Bằng', N'Dong Bang Commune', 'dong-bang', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11503070', N'A Sào', N'A Sao', N'Xã A Sào', N'A Sao Commune', 'a-sao', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11503071', N'Phụ Dực', N'Phu Duc', N'Xã Phụ Dực', N'Phu Duc Commune', 'phu-duc', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11503072', N'Tân Tiến', N'Tan Tien', N'Xã Tân Tiến', N'Tan Tien Commune', 'tan-tien', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11505073', N'Hưng Hà', N'Hung Ha', N'Xã Hưng Hà', N'Hung Ha Commune', 'hung-ha', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11505074', N'Tiên La', N'Tien La', N'Xã Tiên La', N'Tien La Commune', 'tien-la', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11505075', N'Lê Quý Đôn', N'Le Quy Don', N'Xã Lê Quý Đôn', N'Le Quy Don Commune', 'le-quy-don', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11505076', N'Hồng Minh', N'Hong Minh', N'Xã Hồng Minh', N'Hong Minh Commune', 'hong-minh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11505077', N'Thần Khê', N'Than Khe', N'Xã Thần Khê', N'Than Khe Commune', 'than-khe', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11505078', N'Diên Hà', N'Dien Ha', N'Xã Diên Hà', N'Dien Ha Commune', 'dien-ha', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11505079', N'Ngự Thiên', N'Ngu Thien', N'Xã Ngự Thiên', N'Ngu Thien Commune', 'ngu-thien', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11505080', N'Long Hưng', N'Long Hung', N'Xã Long Hưng', N'Long Hung Commune', 'long-hung', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11509081', N'Đông Hưng', N'Dong Hung', N'Xã Đông Hưng', N'Dong Hung Commune', 'dong-hung', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11509082', N'Bắc Tiên Hưng', N'Bac Tien Hung', N'Xã Bắc Tiên Hưng', N'Bac Tien Hung Commune', 'bac-tien-hung', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11509083', N'Đông Tiên Hưng', N'Dong Tien Hung', N'Xã Đông Tiên Hưng', N'Dong Tien Hung Commune', 'dong-tien-hung', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11509084', N'Nam Đông Hưng', N'Nam Dong Hung', N'Xã Nam Đông Hưng', N'Nam Dong Hung Commune', 'nam-dong-hung', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11509085', N'Bắc Đông Quan', N'Bac Dong Quan', N'Xã Bắc Đông Quan', N'Bac Dong Quan Commune', 'bac-dong-quan', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11509086', N'Bắc Đông Hưng', N'Bac Dong Hung', N'Xã Bắc Đông Hưng', N'Bac Dong Hung Commune', 'bac-dong-hung', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11509087', N'Đông Quan', N'Dong Quan', N'Xã Đông Quan', N'Dong Quan Commune', 'dong-quan', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11509088', N'Nam Tiên Hưng', N'Nam Tien Hung', N'Xã Nam Tiên Hưng', N'Nam Tien Hung Commune', 'nam-tien-hung', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11509089', N'Tiên Hưng', N'Tien Hung', N'Xã Tiên Hưng', N'Tien Hung Commune', 'tien-hung', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11513090', N'Lê Lợi', N'Le Loi', N'Xã Lê Lợi', N'Le Loi Commune', 'le-loi', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11513091', N'Kiến Xương', N'Kien Xuong', N'Xã Kiến Xương', N'Kien Xuong Commune', 'kien-xuong', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11513092', N'Quang Lịch', N'Quang Lich', N'Xã Quang Lịch', N'Quang Lich Commune', 'quang-lich', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11513093', N'Vũ Quý', N'Vu Quy', N'Xã Vũ Quý', N'Vu Quy Commune', 'vu-quy', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11513094', N'Bình Thanh', N'Binh Thanh', N'Xã Bình Thanh', N'Binh Thanh Commune', 'binh-thanh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11513095', N'Bình Định', N'Binh Dinh', N'Xã Bình Định', N'Binh Dinh Commune', 'binh-dinh', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11513096', N'Hồng Vũ', N'Hong Vu', N'Xã Hồng Vũ', N'Hong Vu Commune', 'hong-vu', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11513097', N'Bình Nguyên', N'Binh Nguyen', N'Xã Bình Nguyên', N'Binh Nguyen Commune', 'binh-nguyen', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11513098', N'Trà Giang', N'Tra Giang', N'Xã Trà Giang', N'Tra Giang Commune', 'tra-giang', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11511099', N'Vũ Thư', N'Vu Thu', N'Xã Vũ Thư', N'Vu Thu Commune', 'vu-thu', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11511100', N'Thư Trì', N'Thu Tri', N'Xã Thư Trì', N'Thu Tri Commune', 'thu-tri', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11511101', N'Tân Thuận', N'Tan Thuan', N'Xã Tân Thuận', N'Tan Thuan Commune', 'tan-thuan', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11511102', N'Thư Vũ', N'Thu Vu', N'Xã Thư Vũ', N'Thu Vu Commune', 'thu-vu', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11511103', N'Vũ Tiên', N'Vu Tien', N'Xã Vũ Tiên', N'Vu Tien Commune', 'vu-tien', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11511104', N'Vạn Xuân', N'Van Xuan', N'Xã Vạn Xuân', N'Van Xuan Commune', 'van-xuan', 8, N'109', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11707001', N'Gia Viễn', N'Gia Vien', N'Xã Gia Viễn', N'Gia Vien Commune', 'gia-vien', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11707002', N'Đại Hoàng', N'Dai Hoang', N'Xã Đại Hoàng', N'Dai Hoang Commune', 'dai-hoang', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11707003', N'Gia Hưng', N'Gia Hung', N'Xã Gia Hưng', N'Gia Hung Commune', 'gia-hung', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11707004', N'Gia Phong', N'Gia Phong', N'Xã Gia Phong', N'Gia Phong Commune', 'gia-phong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11707005', N'Gia Vân', N'Gia Van', N'Xã Gia Vân', N'Gia Van Commune', 'gia-van', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11707006', N'Gia Trấn', N'Gia Tran', N'Xã Gia Trấn', N'Gia Tran Commune', 'gia-tran', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11705007', N'Nho Quan', N'Nho Quan', N'Xã Nho Quan', N'Nho Quan Commune', 'nho-quan', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11705008', N'Gia Lâm', N'Gia Lam', N'Xã Gia Lâm', N'Gia Lam Commune', 'gia-lam', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11705009', N'Gia Tường', N'Gia Tuong', N'Xã Gia Tường', N'Gia Tuong Commune', 'gia-tuong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11705010', N'Phú Sơn', N'Phu Son', N'Xã Phú Sơn', N'Phu Son Commune', 'phu-son', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11705011', N'Cúc Phương', N'Cuc Phuong', N'Xã Cúc Phương', N'Cuc Phuong Commune', 'cuc-phuong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11705012', N'Phú Long', N'Phu Long', N'Xã Phú Long', N'Phu Long Commune', 'phu-long', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11705013', N'Thanh Sơn', N'Thanh Son', N'Xã Thanh Sơn', N'Thanh Son Commune', 'thanh-son', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11705014', N'Quỳnh Lưu', N'Quynh Luu', N'Xã Quỳnh Lưu', N'Quynh Luu Commune', 'quynh-luu', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11713015', N'Yên Khánh', N'Yen Khanh', N'Xã Yên Khánh', N'Yen Khanh Commune', 'yen-khanh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11713016', N'Khánh Nhạc', N'Khanh Nhac', N'Xã Khánh Nhạc', N'Khanh Nhac Commune', 'khanh-nhac', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11713017', N'Khánh Thiện', N'Khanh Thien', N'Xã Khánh Thiện', N'Khanh Thien Commune', 'khanh-thien', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11713018', N'Khánh Hội', N'Khanh Hoi', N'Xã Khánh Hội', N'Khanh Hoi Commune', 'khanh-hoi', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11713019', N'Khánh Trung', N'Khanh Trung', N'Xã Khánh Trung', N'Khanh Trung Commune', 'khanh-trung', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11711020', N'Yên Mô', N'Yen Mo', N'Xã Yên Mô', N'Yen Mo Commune', 'yen-mo', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11711021', N'Yên Từ', N'Yen Tu', N'Xã Yên Từ', N'Yen Tu Commune', 'yen-tu', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11711022', N'Yên Mạc', N'Yen Mac', N'Xã Yên Mạc', N'Yen Mac Commune', 'yen-mac', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11711023', N'Đồng Thái', N'Dong Thai', N'Xã Đồng Thái', N'Dong Thai Commune', 'dong-thai', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11715024', N'Chất Bình', N'Chat Binh', N'Xã Chất Bình', N'Chat Binh Commune', 'chat-binh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11715025', N'Kim Sơn', N'Kim Son', N'Xã Kim Sơn', N'Kim Son Commune', 'kim-son', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11715026', N'Quang Thiện', N'Quang Thien', N'Xã Quang Thiện', N'Quang Thien Commune', 'quang-thien', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11715027', N'Phát Diệm', N'Phat Diem', N'Xã Phát Diệm', N'Phat Diem Commune', 'phat-diem', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11715028', N'Lai Thành', N'Lai Thanh', N'Xã Lai Thành', N'Lai Thanh Commune', 'lai-thanh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11715029', N'Định Hóa', N'Dinh Hoa', N'Xã Định Hóa', N'Dinh Hoa Commune', 'dinh-hoa', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11715030', N'Bình Minh', N'Binh Minh', N'Xã Bình Minh', N'Binh Minh Commune', 'binh-minh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11715031', N'Kim Đông', N'Kim Dong', N'Xã Kim Đông', N'Kim Dong Commune', 'kim-dong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11111032', N'Bình Lục', N'Binh Luc', N'Xã Bình Lục', N'Binh Luc Commune', 'binh-luc', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11111033', N'Bình Mỹ', N'Binh My', N'Xã Bình Mỹ', N'Binh My Commune', 'binh-my', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11111034', N'Bình An', N'Binh An', N'Xã Bình An', N'Binh An Commune', 'binh-an', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11111035', N'Bình Giang', N'Binh Giang', N'Xã Bình Giang', N'Binh Giang Commune', 'binh-giang', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11111036', N'Bình Sơn', N'Binh Son', N'Xã Bình Sơn', N'Binh Son Commune', 'binh-son', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11109037', N'Liêm Hà', N'Liem Ha', N'Xã Liêm Hà', N'Liem Ha Commune', 'liem-ha', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11109038', N'Tân Thanh', N'Tan Thanh', N'Xã Tân Thanh', N'Tan Thanh Commune', 'tan-thanh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11109039', N'Thanh Bình', N'Thanh Binh', N'Xã Thanh Bình', N'Thanh Binh Commune', 'thanh-binh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11109040', N'Thanh Lâm', N'Thanh Lam', N'Xã Thanh Lâm', N'Thanh Lam Commune', 'thanh-lam', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11109041', N'Thanh Liêm', N'Thanh Liem', N'Xã Thanh Liêm', N'Thanh Liem Commune', 'thanh-liem', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11107042', N'Lý Nhân', N'Ly Nhan', N'Xã Lý Nhân', N'Ly Nhan Commune', 'ly-nhan', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11107043', N'Nam Xang', N'Nam Xang', N'Xã Nam Xang', N'Nam Xang Commune', 'nam-xang', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11107044', N'Bắc Lý', N'Bac Ly', N'Xã Bắc Lý', N'Bac Ly Commune', 'bac-ly', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11107045', N'Vĩnh Trụ', N'Vinh Tru', N'Xã Vĩnh Trụ', N'Vinh Tru Commune', 'vinh-tru', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11107046', N'Trần Thương', N'Tran Thuong', N'Xã Trần Thương', N'Tran Thuong Commune', 'tran-thuong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11107047', N'Nhân Hà', N'Nhan Ha', N'Xã Nhân Hà', N'Nhan Ha Commune', 'nhan-ha', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11107048', N'Nam Lý', N'Nam Ly', N'Xã Nam Lý', N'Nam Ly Commune', 'nam-ly', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11309049', N'Nam Trực', N'Nam Truc', N'Xã Nam Trực', N'Nam Truc Commune', 'nam-truc', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11309050', N'Nam Minh', N'Nam Minh', N'Xã Nam Minh', N'Nam Minh Commune', 'nam-minh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11309051', N'Nam Đồng', N'Nam Dong', N'Xã Nam Đồng', N'Nam Dong Commune', 'nam-dong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11309052', N'Nam Ninh', N'Nam Ninh', N'Xã Nam Ninh', N'Nam Ninh Commune', 'nam-ninh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11309053', N'Nam Hồng', N'Nam Hong', N'Xã Nam Hồng', N'Nam Hong Commune', 'nam-hong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11303054', N'Minh Tân', N'Minh Tan', N'Xã Minh Tân', N'Minh Tan Commune', 'minh-tan', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11303055', N'Hiển Khánh', N'Hien Khanh', N'Xã Hiển Khánh', N'Hien Khanh Commune', 'hien-khanh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11303056', N'Vụ Bản', N'Vu Ban', N'Xã Vụ Bản', N'Vu Ban Commune', 'vu-ban', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11303057', N'Liên Minh', N'Lien Minh', N'Xã Liên Minh', N'Lien Minh Commune', 'lien-minh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11307058', N'Ý Yên', N'Y Yen', N'Xã Ý Yên', N'Y Yen Commune', 'y-yen', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11307059', N'Yên Đồng', N'Yen Dong', N'Xã Yên Đồng', N'Yen Dong Commune', 'yen-dong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11307060', N'Yên Cường', N'Yen Cuong', N'Xã Yên Cường', N'Yen Cuong Commune', 'yen-cuong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11307061', N'Vạn Thắng', N'Van Thang', N'Xã Vạn Thắng', N'Van Thang Commune', 'van-thang', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11307062', N'Vũ Dương', N'Vu Duong', N'Xã Vũ Dương', N'Vu Duong Commune', 'vu-duong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11307063', N'Tân Minh', N'Tan Minh', N'Xã Tân Minh', N'Tan Minh Commune', 'tan-minh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11307064', N'Phong Doanh', N'Phong Doanh', N'Xã Phong Doanh', N'Phong Doanh Commune', 'phong-doanh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11311065', N'Cổ Lễ', N'Co Le', N'Xã Cổ Lễ', N'Co Le Commune', 'co-le', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11311066', N'Ninh Giang', N'Ninh Giang', N'Xã Ninh Giang', N'Ninh Giang Commune', 'ninh-giang', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11311067', N'Cát Thành', N'Cat Thanh', N'Xã Cát Thành', N'Cat Thanh Commune', 'cat-thanh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11311068', N'Trực Ninh', N'Truc Ninh', N'Xã Trực Ninh', N'Truc Ninh Commune', 'truc-ninh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11311069', N'Quang Hưng', N'Quang Hung', N'Xã Quang Hưng', N'Quang Hung Commune', 'quang-hung', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11311070', N'Minh Thái', N'Minh Thai', N'Xã Minh Thái', N'Minh Thai Commune', 'minh-thai', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11311071', N'Ninh Cường', N'Ninh Cuong', N'Xã Ninh Cường', N'Ninh Cuong Commune', 'ninh-cuong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11313072', N'Xuân Trường', N'Xuan Truong', N'Xã Xuân Trường', N'Xuan Truong Commune', 'xuan-truong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11313073', N'Xuân Hưng', N'Xuan Hung', N'Xã Xuân Hưng', N'Xuan Hung Commune', 'xuan-hung', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11313074', N'Xuân Giang', N'Xuan Giang', N'Xã Xuân Giang', N'Xuan Giang Commune', 'xuan-giang', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11313075', N'Xuân Hồng', N'Xuan Hong', N'Xã Xuân Hồng', N'Xuan Hong Commune', 'xuan-hong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11319076', N'Hải Hậu', N'Hai Hau', N'Xã Hải Hậu', N'Hai Hau Commune', 'hai-hau', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11319077', N'Hải Anh', N'Hai Anh', N'Xã Hải Anh', N'Hai Anh Commune', 'hai-anh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11319078', N'Hải Tiến', N'Hai Tien', N'Xã Hải Tiến', N'Hai Tien Commune', 'hai-tien', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11319079', N'Hải Hưng', N'Hai Hung', N'Xã Hải Hưng', N'Hai Hung Commune', 'hai-hung', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11319080', N'Hải An', N'Hai An', N'Xã Hải An', N'Hai An Commune', 'hai-an', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11319081', N'Hải Quang', N'Hai Quang', N'Xã Hải Quang', N'Hai Quang Commune', 'hai-quang', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11319082', N'Hải Xuân', N'Hai Xuan', N'Xã Hải Xuân', N'Hai Xuan Commune', 'hai-xuan', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11319083', N'Hải Thịnh', N'Hai Thinh', N'Xã Hải Thịnh', N'Hai Thinh Commune', 'hai-thinh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11315084', N'Giao Minh', N'Giao Minh', N'Xã Giao Minh', N'Giao Minh Commune', 'giao-minh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11315085', N'Giao Hoà', N'Giao Hoa', N'Xã Giao Hoà', N'Giao Hoa Commune', 'giao-hoa', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11315086', N'Giao Thuỷ', N'Giao Thuy', N'Xã Giao Thuỷ', N'Giao Thuy Commune', 'giao-thuy', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11315087', N'Giao Phúc', N'Giao Phuc', N'Xã Giao Phúc', N'Giao Phuc Commune', 'giao-phuc', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11315088', N'Giao Hưng', N'Giao Hung', N'Xã Giao Hưng', N'Giao Hung Commune', 'giao-hung', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11315089', N'Giao Bình', N'Giao Binh', N'Xã Giao Bình', N'Giao Binh Commune', 'giao-binh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11315090', N'Giao Ninh', N'Giao Ninh', N'Xã Giao Ninh', N'Giao Ninh Commune', 'giao-ninh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11317091', N'Đồng Thịnh', N'Dong Thinh', N'Xã Đồng Thịnh', N'Dong Thinh Commune', 'dong-thinh', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11317092', N'Nghĩa Hưng', N'Nghia Hung', N'Xã Nghĩa Hưng', N'Nghia Hung Commune', 'nghia-hung', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11317093', N'Nghĩa Sơn', N'Nghia Son', N'Xã Nghĩa Sơn', N'Nghia Son Commune', 'nghia-son', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11317094', N'Hồng Phong', N'Hong Phong', N'Xã Hồng Phong', N'Hong Phong Commune', 'hong-phong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11317095', N'Quỹ Nhất', N'Quy Nhat', N'Xã Quỹ Nhất', N'Quy Nhat Commune', 'quy-nhat', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11317096', N'Nghĩa Lâm', N'Nghia Lam', N'Xã Nghĩa Lâm', N'Nghia Lam Commune', 'nghia-lam', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11317097', N'Rạng Đông', N'Rang Dong', N'Xã Rạng Đông', N'Rang Dong Commune', 'rang-dong', 8, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11709098', N'Tây Hoa Lư', N'Tay Hoa Lu', N'Phường Tây Hoa Lư', N'Tay Hoa Lu Ward', 'tay-hoa-lu', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11709099', N'Hoa Lư', N'Hoa Lu', N'Phường Hoa Lư', N'Hoa Lu Ward', 'hoa-lu', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11709100', N'Nam Hoa Lư', N'Nam Hoa Lu', N'Phường Nam Hoa Lư', N'Nam Hoa Lu Ward', 'nam-hoa-lu', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11713101', N'Đông Hoa Lư', N'Dong Hoa Lu', N'Phường Đông Hoa Lư', N'Dong Hoa Lu Ward', 'dong-hoa-lu', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11703102', N'Tam Điệp', N'Tam Diep', N'Phường Tam Điệp', N'Tam Diep Ward', 'tam-diep', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11703103', N'Yên Sơn', N'Yen Son', N'Phường Yên Sơn', N'Yen Son Ward', 'yen-son', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11703104', N'Trung Sơn', N'Trung Son', N'Phường Trung Sơn', N'Trung Son Ward', 'trung-son', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11703105', N'Yên Thắng', N'Yen Thang', N'Phường Yên Thắng', N'Yen Thang Ward', 'yen-thang', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11101106', N'Hà Nam', N'Ha Nam', N'Phường Hà Nam', N'Ha Nam Ward', 'ha-nam', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11101107', N'Phủ Lý', N'Phu Ly', N'Phường Phủ Lý', N'Phu Ly Ward', 'phu-ly', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11101108', N'Phù Vân', N'Phu Van', N'Phường Phù Vân', N'Phu Van Ward', 'phu-van', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11101109', N'Châu Sơn', N'Chau Son', N'Phường Châu Sơn', N'Chau Son Ward', 'chau-son', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11101110', N'Liêm Tuyền', N'Liem Tuyen', N'Phường Liêm Tuyền', N'Liem Tuyen Ward', 'liem-tuyen', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11103111', N'Duy Tiên', N'Duy Tien', N'Phường Duy Tiên', N'Duy Tien Ward', 'duy-tien', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11103112', N'Duy Tân', N'Duy Tan', N'Phường Duy Tân', N'Duy Tan Ward', 'duy-tan', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11103113', N'Đồng Văn', N'Dong Van', N'Phường Đồng Văn', N'Dong Van Ward', 'dong-van', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11103114', N'Duy Hà', N'Duy Ha', N'Phường Duy Hà', N'Duy Ha Ward', 'duy-ha', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11103115', N'Tiên Sơn', N'Tien Son', N'Phường Tiên Sơn', N'Tien Son Ward', 'tien-son', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11105116', N'Lê Hồ', N'Le Ho', N'Phường Lê Hồ', N'Le Ho Ward', 'le-ho', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11105117', N'Nguyễn Úy', N'Nguyen Uy', N'Phường Nguyễn Úy', N'Nguyen Uy Ward', 'nguyen-uy', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11105118', N'Lý Thường Kiệt', N'Ly Thuong Kiet', N'Phường Lý Thường Kiệt', N'Ly Thuong Kiet Ward', 'ly-thuong-kiet', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11105119', N'Kim Thanh', N'Kim Thanh', N'Phường Kim Thanh', N'Kim Thanh Ward', 'kim-thanh', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11105120', N'Tam Chúc', N'Tam Chuc', N'Phường Tam Chúc', N'Tam Chuc Ward', 'tam-chuc', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11105121', N'Kim Bảng', N'Kim Bang', N'Phường Kim Bảng', N'Kim Bang Ward', 'kim-bang', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11301122', N'Nam Định', N'Nam Dinh', N'Phường Nam Định', N'Nam Dinh Ward', 'nam-dinh', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11301123', N'Thiên Trường', N'Thien Truong', N'Phường Thiên Trường', N'Thien Truong Ward', 'thien-truong', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11301124', N'Đông A', N'Dong A', N'Phường Đông A', N'Dong A Ward', 'dong-a', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11301125', N'Vị Khê', N'Vi Khe', N'Phường Vị Khê', N'Vi Khe Ward', 'vi-khe', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11301126', N'Thành Nam', N'Thanh Nam', N'Phường Thành Nam', N'Thanh Nam Ward', 'thanh-nam', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11301127', N'Trường Thi', N'Truong Thi', N'Phường Trường Thi', N'Truong Thi Ward', 'truong-thi', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11309128', N'Hồng Quang', N'Hong Quang', N'Phường Hồng Quang', N'Hong Quang Ward', 'hong-quang', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'11301129', N'Mỹ Lộc', N'My Loc', N'Phường Mỹ Lộc', N'My Loc Ward', 'my-loc', 7, N'117', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20301001', N'Thục Phán', N'Thuc Phan', N'Phường Thục Phán', N'Thuc Phan Ward', 'thuc-phan', 7, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20301002', N'Nùng Trí Cao', N'Nung Tri Cao', N'Phường Nùng Trí Cao', N'Nung Tri Cao Ward', 'nung-tri-cao', 7, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20301003', N'Tân Giang', N'Tan Giang', N'Phường Tân Giang', N'Tan Giang Ward', 'tan-giang', 7, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20323004', N'Quảng Lâm', N'Quang Lam', N'Xã Quảng Lâm', N'Quang Lam Commune', 'quang-lam', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20323005', N'Nam Quang', N'Nam Quang', N'Xã Nam Quang', N'Nam Quang Commune', 'nam-quang', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20323006', N'Lý Bôn', N'Ly Bon', N'Xã Lý Bôn', N'Ly Bon Commune', 'ly-bon', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20323007', N'Bảo Lâm', N'Bao Lam', N'Xã Bảo Lâm', N'Bao Lam Commune', 'bao-lam', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20323008', N'Yên Thổ', N'Yen Tho', N'Xã Yên Thổ', N'Yen Tho Commune', 'yen-tho', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20303009', N'Sơn Lộ', N'Son Lo', N'Xã Sơn Lộ', N'Son Lo Commune', 'son-lo', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20303010', N'Hưng Đạo', N'Hung Dao', N'Xã Hưng Đạo', N'Hung Dao Commune', 'hung-dao', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20303011', N'Bảo Lạc', N'Bao Lac', N'Xã Bảo Lạc', N'Bao Lac Commune', 'bao-lac', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20303012', N'Cốc Pàng', N'Coc Pang', N'Xã Cốc Pàng', N'Coc Pang Commune', 'coc-pang', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20303013', N'Cô Ba', N'Co Ba', N'Xã Cô Ba', N'Co Ba Commune', 'co-ba', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20303014', N'Khánh Xuân', N'Khanh Xuan', N'Xã Khánh Xuân', N'Khanh Xuan Commune', 'khanh-xuan', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20303015', N'Xuân Trường', N'Xuan Truong', N'Xã Xuân Trường', N'Xuan Truong Commune', 'xuan-truong', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20303016', N'Huy Giáp', N'Huy Giap', N'Xã Huy Giáp', N'Huy Giap Commune', 'huy-giap', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20313017', N'Ca Thành', N'Ca Thanh', N'Xã Ca Thành', N'Ca Thanh Commune', 'ca-thanh', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20313018', N'Phan Thanh', N'Phan Thanh', N'Xã Phan Thanh', N'Phan Thanh Commune', 'phan-thanh', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20313019', N'Thành Công', N'Thanh Cong', N'Xã Thành Công', N'Thanh Cong Commune', 'thanh-cong', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20313020', N'Tĩnh Túc', N'Tinh Tuc', N'Xã Tĩnh Túc', N'Tinh Tuc Commune', 'tinh-tuc', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20313021', N'Tam Kim', N'Tam Kim', N'Xã Tam Kim', N'Tam Kim Commune', 'tam-kim', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20313022', N'Nguyên Bình', N'Nguyen Binh', N'Xã Nguyên Bình', N'Nguyen Binh Commune', 'nguyen-binh', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20313023', N'Minh Tâm', N'Minh Tam', N'Xã Minh Tâm', N'Minh Tam Commune', 'minh-tam', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20305024', N'Thanh Long', N'Thanh Long', N'Xã Thanh Long', N'Thanh Long Commune', 'thanh-long', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20305025', N'Cần Yên', N'Can Yen', N'Xã Cần Yên', N'Can Yen Commune', 'can-yen', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20305026', N'Thông Nông', N'Thong Nong', N'Xã Thông Nông', N'Thong Nong Commune', 'thong-nong', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20305027', N'Trường Hà', N'Truong Ha', N'Xã Trường Hà', N'Truong Ha Commune', 'truong-ha', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20305028', N'Hà Quảng', N'Ha Quang', N'Xã Hà Quảng', N'Ha Quang Commune', 'ha-quang', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20305029', N'Lũng Nặm', N'Lung Nam', N'Xã Lũng Nặm', N'Lung Nam Commune', 'lung-nam', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20305030', N'Tổng Cọt', N'Tong Cot', N'Xã Tổng Cọt', N'Tong Cot Commune', 'tong-cot', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20315031', N'Nam Tuấn', N'Nam Tuan', N'Xã Nam Tuấn', N'Nam Tuan Commune', 'nam-tuan', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20315032', N'Hoà An', N'Hoa An', N'Xã Hoà An', N'Hoa An Commune', 'hoa-an', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20315033', N'Bạch Đằng', N'Bach Dang', N'Xã Bạch Đằng', N'Bach Dang Commune', 'bach-dang', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20315034', N'Nguyễn Huệ', N'Nguyen Hue', N'Xã Nguyễn Huệ', N'Nguyen Hue Commune', 'nguyen-hue', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20321035', N'Minh Khai', N'Minh Khai', N'Xã Minh Khai', N'Minh Khai Commune', 'minh-khai', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20321036', N'Canh Tân', N'Canh Tan', N'Xã Canh Tân', N'Canh Tan Commune', 'canh-tan', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20321037', N'Kim Đồng', N'Kim Dong', N'Xã Kim Đồng', N'Kim Dong Commune', 'kim-dong', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20321038', N'Thạch An', N'Thach An', N'Xã Thạch An', N'Thach An Commune', 'thach-an', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20321039', N'Đông Khê', N'Dong Khe', N'Xã Đông Khê', N'Dong Khe Commune', 'dong-khe', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20321040', N'Đức Long', N'Duc Long', N'Xã Đức Long', N'Duc Long Commune', 'duc-long', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20317041', N'Phục Hoà', N'Phuc Hoa', N'Xã Phục Hoà', N'Phuc Hoa Commune', 'phuc-hoa', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20317042', N'Bế Văn Đàn', N'Be Van Dan', N'Xã Bế Văn Đàn', N'Be Van Dan Commune', 'be-van-dan', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20317043', N'Độc Lập', N'Doc Lap', N'Xã Độc Lập', N'Doc Lap Commune', 'doc-lap', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20317044', N'Quảng Uyên', N'Quang Uyen', N'Xã Quảng Uyên', N'Quang Uyen Commune', 'quang-uyen', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20317045', N'Hạnh Phúc', N'Hanh Phuc', N'Xã Hạnh Phúc', N'Hanh Phuc Commune', 'hanh-phuc', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20311046', N'Quang Hán', N'Quang Han', N'Xã Quang Hán', N'Quang Han Commune', 'quang-han', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20311047', N'Trà Lĩnh', N'Tra Linh', N'Xã Trà Lĩnh', N'Tra Linh Commune', 'tra-linh', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20311048', N'Quang Trung', N'Quang Trung', N'Xã Quang Trung', N'Quang Trung Commune', 'quang-trung', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20311049', N'Đoài Dương', N'Doai Duong', N'Xã Đoài Dương', N'Doai Duong Commune', 'doai-duong', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20311050', N'Trùng Khánh', N'Trung Khanh', N'Xã Trùng Khánh', N'Trung Khanh Commune', 'trung-khanh', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20311051', N'Đàm Thuỷ', N'Dam Thuy', N'Xã Đàm Thuỷ', N'Dam Thuy Commune', 'dam-thuy', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20311052', N'Đình Phong', N'Dinh Phong', N'Xã Đình Phong', N'Dinh Phong Commune', 'dinh-phong', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20319053', N'Lý Quốc', N'Ly Quoc', N'Xã Lý Quốc', N'Ly Quoc Commune', 'ly-quoc', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20319054', N'Hạ Lang', N'Ha Lang', N'Xã Hạ Lang', N'Ha Lang Commune', 'ha-lang', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20319055', N'Vinh Quý', N'Vinh Quy', N'Xã Vinh Quý', N'Vinh Quy Commune', 'vinh-quy', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20319056', N'Quang Long', N'Quang Long', N'Xã Quang Long', N'Quang Long Commune', 'quang-long', 8, N'203', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21113001', N'Thượng Lâm', N'Thuong Lam', N'Xã Thượng Lâm', N'Thuong Lam Commune', 'thuong-lam', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21113002', N'Lâm Bình', N'Lam Binh', N'Xã Lâm Bình', N'Lam Binh Commune', 'lam-binh', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21113003', N'Minh Quang', N'Minh Quang', N'Xã Minh Quang', N'Minh Quang Commune', 'minh-quang', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21113004', N'Bình An', N'Binh An', N'Xã Bình An', N'Binh An Commune', 'binh-an', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21103005', N'Côn Lôn', N'Con Lon', N'Xã Côn Lôn', N'Con Lon Commune', 'con-lon', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21103006', N'Yên Hoa', N'Yen Hoa', N'Xã Yên Hoa', N'Yen Hoa Commune', 'yen-hoa', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21103007', N'Thượng Nông', N'Thuong Nong', N'Xã Thượng Nông', N'Thuong Nong Commune', 'thuong-nong', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21103008', N'Hồng Thái', N'Hong Thai', N'Xã Hồng Thái', N'Hong Thai Commune', 'hong-thai', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21103009', N'Nà Hang', N'Na Hang', N'Xã Nà Hang', N'Na Hang Commune', 'na-hang', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21105010', N'Tân Mỹ', N'Tan My', N'Xã Tân Mỹ', N'Tan My Commune', 'tan-my', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21105011', N'Yên Lập', N'Yen Lap', N'Xã Yên Lập', N'Yen Lap Commune', 'yen-lap', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21105012', N'Tân An', N'Tan An', N'Xã Tân An', N'Tan An Commune', 'tan-an', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21105013', N'Chiêm Hoá', N'Chiem Hoa', N'Xã Chiêm Hoá', N'Chiem Hoa Commune', 'chiem-hoa', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21105014', N'Hoà An', N'Hoa An', N'Xã Hoà An', N'Hoa An Commune', 'hoa-an', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21105015', N'Kiên Đài', N'Kien Dai', N'Xã Kiên Đài', N'Kien Dai Commune', 'kien-dai', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21105016', N'Tri Phú', N'Tri Phu', N'Xã Tri Phú', N'Tri Phu Commune', 'tri-phu', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21105017', N'Kim Bình', N'Kim Binh', N'Xã Kim Bình', N'Kim Binh Commune', 'kim-binh', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21105018', N'Yên Nguyên', N'Yen Nguyen', N'Xã Yên Nguyên', N'Yen Nguyen Commune', 'yen-nguyen', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21105019', N'Trung Hà', N'Trung Ha', N'Xã Trung Hà', N'Trung Ha Commune', 'trung-ha', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21107020', N'Yên Phú', N'Yen Phu', N'Xã Yên Phú', N'Yen Phu Commune', 'yen-phu', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21107021', N'Bạch Xa', N'Bach Xa', N'Xã Bạch Xa', N'Bach Xa Commune', 'bach-xa', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21107022', N'Phù Lưu', N'Phu Luu', N'Xã Phù Lưu', N'Phu Luu Commune', 'phu-luu', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21107023', N'Hàm Yên', N'Ham Yen', N'Xã Hàm Yên', N'Ham Yen Commune', 'ham-yen', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21107024', N'Bình Xa', N'Binh Xa', N'Xã Bình Xa', N'Binh Xa Commune', 'binh-xa', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21107025', N'Thái Sơn', N'Thai Son', N'Xã Thái Sơn', N'Thai Son Commune', 'thai-son', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21107026', N'Thái Hoà', N'Thai Hoa', N'Xã Thái Hoà', N'Thai Hoa Commune', 'thai-hoa', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21107027', N'Hùng Đức', N'Hung Duc', N'Xã Hùng Đức', N'Hung Duc Commune', 'hung-duc', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21109028', N'Hùng Lợi', N'Hung Loi', N'Xã Hùng Lợi', N'Hung Loi Commune', 'hung-loi', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21109029', N'Trung Sơn', N'Trung Son', N'Xã Trung Sơn', N'Trung Son Commune', 'trung-son', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21109030', N'Thái Bình', N'Thai Binh', N'Xã Thái Bình', N'Thai Binh Commune', 'thai-binh', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21109031', N'Tân Long', N'Tan Long', N'Xã Tân Long', N'Tan Long Commune', 'tan-long', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21109032', N'Xuân Vân', N'Xuan Van', N'Xã Xuân Vân', N'Xuan Van Commune', 'xuan-van', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21109033', N'Lực Hành', N'Luc Hanh', N'Xã Lực Hành', N'Luc Hanh Commune', 'luc-hanh', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21109034', N'Yên Sơn', N'Yen Son', N'Xã Yên Sơn', N'Yen Son Commune', 'yen-son', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21109035', N'Nhữ Khê', N'Nhu Khe', N'Xã Nhữ Khê', N'Nhu Khe Commune', 'nhu-khe', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21109036', N'Kiến Thiết', N'Kien Thiet', N'Xã Kiến Thiết', N'Kien Thiet Commune', 'kien-thiet', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21111037', N'Tân Trào', N'Tan Trao', N'Xã Tân Trào', N'Tan Trao Commune', 'tan-trao', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21111038', N'Minh Thanh', N'Minh Thanh', N'Xã Minh Thanh', N'Minh Thanh Commune', 'minh-thanh', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21111039', N'Sơn Dương', N'Son Duong', N'Xã Sơn Dương', N'Son Duong Commune', 'son-duong', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21111040', N'Bình Ca', N'Binh Ca', N'Xã Bình Ca', N'Binh Ca Commune', 'binh-ca', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21111041', N'Tân Thanh', N'Tan Thanh', N'Xã Tân Thanh', N'Tan Thanh Commune', 'tan-thanh', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21111042', N'Sơn Thuỷ', N'Son Thuy', N'Xã Sơn Thuỷ', N'Son Thuy Commune', 'son-thuy', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21111043', N'Phú Lương', N'Phu Luong', N'Xã Phú Lương', N'Phu Luong Commune', 'phu-luong', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21111044', N'Trường Sinh', N'Truong Sinh', N'Xã Trường Sinh', N'Truong Sinh Commune', 'truong-sinh', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21111045', N'Hồng Sơn', N'Hong Son', N'Xã Hồng Sơn', N'Hong Son Commune', 'hong-son', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21111046', N'Đông Thọ', N'Dong Tho', N'Xã Đông Thọ', N'Dong Tho Commune', 'dong-tho', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21101047', N'Mỹ Lâm', N'My Lam', N'Phường Mỹ Lâm', N'My Lam Ward', 'my-lam', 7, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21101048', N'Minh Xuân', N'Minh Xuan', N'Phường Minh Xuân', N'Minh Xuan Ward', 'minh-xuan', 7, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21101049', N'Nông Tiến', N'Nong Tien', N'Phường Nông Tiến', N'Nong Tien Ward', 'nong-tien', 7, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21101050', N'An Tường', N'An Tuong', N'Phường An Tường', N'An Tuong Ward', 'an-tuong', 7, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21101051', N'Bình Thuận', N'Binh Thuan', N'Phường Bình Thuận', N'Binh Thuan Ward', 'binh-thuan', 7, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20103052', N'Lũng Cú', N'Lung Cu', N'Xã Lũng Cú', N'Lung Cu Commune', 'lung-cu', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20103053', N'Đồng Văn', N'Dong Van', N'Xã Đồng Văn', N'Dong Van Commune', 'dong-van', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20103054', N'Sà Phìn', N'Sa Phin', N'Xã Sà Phìn', N'Sa Phin Commune', 'sa-phin', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20103055', N'Phố Bảng', N'Pho Bang', N'Xã Phố Bảng', N'Pho Bang Commune', 'pho-bang', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20103056', N'Lũng Phìn', N'Lung Phin', N'Xã Lũng Phìn', N'Lung Phin Commune', 'lung-phin', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20105057', N'Sủng Máng', N'Sung Mang', N'Xã Sủng Máng', N'Sung Mang Commune', 'sung-mang', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20105058', N'Sơn Vĩ', N'Son Vi', N'Xã Sơn Vĩ', N'Son Vi Commune', 'son-vi', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20105059', N'Mèo Vạc', N'Meo Vac', N'Xã Mèo Vạc', N'Meo Vac Commune', 'meo-vac', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20105060', N'Khâu Vai', N'Khau Vai', N'Xã Khâu Vai', N'Khau Vai Commune', 'khau-vai', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20105061', N'Niêm Sơn', N'Niem Son', N'Xã Niêm Sơn', N'Niem Son Commune', 'niem-son', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20105062', N'Tát Ngà', N'Tat Nga', N'Xã Tát Ngà', N'Tat Nga Commune', 'tat-nga', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20107063', N'Thắng Mố', N'Thang Mo', N'Xã Thắng Mố', N'Thang Mo Commune', 'thang-mo', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20107064', N'Bạch Đích', N'Bach Dich', N'Xã Bạch Đích', N'Bach Dich Commune', 'bach-dich', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20107065', N'Yên Minh', N'Yen Minh', N'Xã Yên Minh', N'Yen Minh Commune', 'yen-minh', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20107066', N'Mậu Duệ', N'Mau Due', N'Xã Mậu Duệ', N'Mau Due Commune', 'mau-due', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20107067', N'Ngọc Long', N'Ngoc Long', N'Xã Ngọc Long', N'Ngoc Long Commune', 'ngoc-long', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20107068', N'Du Già', N'Du Gia', N'Xã Du Già', N'Du Gia Commune', 'du-gia', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20107069', N'Đường Thượng', N'Duong Thuong', N'Xã Đường Thượng', N'Duong Thuong Commune', 'duong-thuong', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20109070', N'Lùng Tám', N'Lung Tam', N'Xã Lùng Tám', N'Lung Tam Commune', 'lung-tam', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20109071', N'Cán Tỷ', N'Can Ty', N'Xã Cán Tỷ', N'Can Ty Commune', 'can-ty', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20109072', N'Nghĩa Thuận', N'Nghia Thuan', N'Xã Nghĩa Thuận', N'Nghia Thuan Commune', 'nghia-thuan', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20109073', N'Quản Bạ', N'Quan Ba', N'Xã Quản Bạ', N'Quan Ba Commune', 'quan-ba', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20109074', N'Tùng Vài', N'Tung Vai', N'Xã Tùng Vài', N'Tung Vai Commune', 'tung-vai', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20111075', N'Yên Cường', N'Yen Cuong', N'Xã Yên Cường', N'Yen Cuong Commune', 'yen-cuong', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20111076', N'Đường Hồng', N'Duong Hong', N'Xã Đường Hồng', N'Duong Hong Commune', 'duong-hong', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20111077', N'Bắc Mê', N'Bac Me', N'Xã Bắc Mê', N'Bac Me Commune', 'bac-me', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20111078', N'Giáp Trung', N'Giap Trung', N'Xã Giáp Trung', N'Giap Trung Commune', 'giap-trung', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20111079', N'Minh Sơn', N'Minh Son', N'Xã Minh Sơn', N'Minh Son Commune', 'minh-son', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20111080', N'Minh Ngọc', N'Minh Ngoc', N'Xã Minh Ngọc', N'Minh Ngoc Commune', 'minh-ngoc', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20101081', N'Ngọc Đường', N'Ngoc Duong', N'Xã Ngọc Đường', N'Ngoc Duong Commune', 'ngoc-duong', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20101082', N'Hà Giang 1', N'Ha Giang 1', N'Phường Hà Giang 1', N'Ha Giang 1 Ward', 'ha-giang-1', 7, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20101083', N'Hà Giang 2', N'Ha Giang 2', N'Phường Hà Giang 2', N'Ha Giang 2 Ward', 'ha-giang-2', 7, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20115084', N'Lao Chải', N'Lao Chai', N'Xã Lao Chải', N'Lao Chai Commune', 'lao-chai', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20115085', N'Thanh Thuỷ', N'Thanh Thuy', N'Xã Thanh Thuỷ', N'Thanh Thuy Commune', 'thanh-thuy', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20115086', N'Minh Tân', N'Minh Tan', N'Xã Minh Tân', N'Minh Tan Commune', 'minh-tan', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20115087', N'Thuận Hoà', N'Thuan Hoa', N'Xã Thuận Hoà', N'Thuan Hoa Commune', 'thuan-hoa', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20115088', N'Tùng Bá', N'Tung Ba', N'Xã Tùng Bá', N'Tung Ba Commune', 'tung-ba', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20115089', N'Phú Linh', N'Phu Linh', N'Xã Phú Linh', N'Phu Linh Commune', 'phu-linh', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20115090', N'Linh Hồ', N'Linh Ho', N'Xã Linh Hồ', N'Linh Ho Commune', 'linh-ho', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20115091', N'Bạch Ngọc', N'Bach Ngoc', N'Xã Bạch Ngọc', N'Bach Ngoc Commune', 'bach-ngoc', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20115092', N'Vị Xuyên', N'Vi Xuyen', N'Xã Vị Xuyên', N'Vi Xuyen Commune', 'vi-xuyen', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20115093', N'Việt Lâm', N'Viet Lam', N'Xã Việt Lâm', N'Viet Lam Commune', 'viet-lam', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20115094', N'Cao Bồ', N'Cao Bo', N'Xã Cao Bồ', N'Cao Bo Commune', 'cao-bo', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20115095', N'Thượng Sơn', N'Thuong Son', N'Xã Thượng Sơn', N'Thuong Son Commune', 'thuong-son', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20119096', N'Tân Quang', N'Tan Quang', N'Xã Tân Quang', N'Tan Quang Commune', 'tan-quang', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20119097', N'Đồng Tâm', N'Dong Tam', N'Xã Đồng Tâm', N'Dong Tam Commune', 'dong-tam', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20119098', N'Liên Hiệp', N'Lien Hiep', N'Xã Liên Hiệp', N'Lien Hiep Commune', 'lien-hiep', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20119099', N'Bằng Hành', N'Bang Hanh', N'Xã Bằng Hành', N'Bang Hanh Commune', 'bang-hanh', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20119100', N'Bắc Quang', N'Bac Quang', N'Xã Bắc Quang', N'Bac Quang Commune', 'bac-quang', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20119101', N'Hùng An', N'Hung An', N'Xã Hùng An', N'Hung An Commune', 'hung-an', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20119102', N'Vĩnh Tuy', N'Vinh Tuy', N'Xã Vĩnh Tuy', N'Vinh Tuy Commune', 'vinh-tuy', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20119103', N'Đồng Yên', N'Dong Yen', N'Xã Đồng Yên', N'Dong Yen Commune', 'dong-yen', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20118104', N'Tiên Yên', N'Tien Yen', N'Xã Tiên Yên', N'Tien Yen Commune', 'tien-yen', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20118105', N'Xuân Giang', N'Xuan Giang', N'Xã Xuân Giang', N'Xuan Giang Commune', 'xuan-giang', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20118106', N'Bằng Lang', N'Bang Lang', N'Xã Bằng Lang', N'Bang Lang Commune', 'bang-lang', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20118107', N'Yên Thành', N'Yen Thanh', N'Xã Yên Thành', N'Yen Thanh Commune', 'yen-thanh', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20118108', N'Quang Bình', N'Quang Binh', N'Xã Quang Bình', N'Quang Binh Commune', 'quang-binh', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20118109', N'Tân Trịnh', N'Tan Trinh', N'Xã Tân Trịnh', N'Tan Trinh Commune', 'tan-trinh', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20118110', N'Tiên Nguyên', N'Tien Nguyen', N'Xã Tiên Nguyên', N'Tien Nguyen Commune', 'tien-nguyen', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20113111', N'Thông Nguyên', N'Thong Nguyen', N'Xã Thông Nguyên', N'Thong Nguyen Commune', 'thong-nguyen', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20113112', N'Hồ Thầu', N'Ho Thau', N'Xã Hồ Thầu', N'Ho Thau Commune', 'ho-thau', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20113113', N'Nậm Dịch', N'Nam Dich', N'Xã Nậm Dịch', N'Nam Dich Commune', 'nam-dich', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20113114', N'Tân Tiến', N'Tan Tien', N'Xã Tân Tiến', N'Tan Tien Commune', 'tan-tien', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20113115', N'Hoàng Su Phì', N'Hoang Su Phi', N'Xã Hoàng Su Phì', N'Hoang Su Phi Commune', 'hoang-su-phi', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20113116', N'Thàng Tín', N'Thang Tin', N'Xã Thàng Tín', N'Thang Tin Commune', 'thang-tin', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20113117', N'Bản Máy', N'Ban May', N'Xã Bản Máy', N'Ban May Commune', 'ban-may', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20113118', N'Pờ Ly Ngài', N'Po Ly Ngai', N'Xã Pờ Ly Ngài', N'Po Ly Ngai Commune', 'po-ly-ngai', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20117119', N'Xín Mần', N'Xin Man', N'Xã Xín Mần', N'Xin Man Commune', 'xin-man', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20117120', N'Pà Vầy Sủ', N'Pa Vay Su', N'Xã Pà Vầy Sủ', N'Pa Vay Su Commune', 'pa-vay-su', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20117121', N'Nấm Dẩn', N'Nam Dan', N'Xã Nấm Dẩn', N'Nam Dan Commune', 'nam-dan', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20117122', N'Trung Thịnh', N'Trung Thinh', N'Xã Trung Thịnh', N'Trung Thinh Commune', 'trung-thinh', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20117123', N'Quảng Nguyên', N'Quang Nguyen', N'Xã Quảng Nguyên', N'Quang Nguyen Commune', 'quang-nguyen', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20117124', N'Khuôn Lùng', N'Khuon Lung', N'Xã Khuôn Lùng', N'Khuon Lung Commune', 'khuon-lung', 8, N'211', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21309001', N'Khao Mang', N'Khao Mang', N'Xã Khao Mang', N'Khao Mang Commune', 'khao-mang', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21309002', N'Mù Cang Chải', N'Mu Cang Chai', N'Xã Mù Cang Chải', N'Mu Cang Chai Commune', 'mu-cang-chai', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21309003', N'Púng Luông', N'Pung Luong', N'Xã Púng Luông', N'Pung Luong Commune', 'pung-luong', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21315004', N'Tú Lệ', N'Tu Le', N'Xã Tú Lệ', N'Tu Le Commune', 'tu-le', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21317005', N'Trạm Tấu', N'Tram Tau', N'Xã Trạm Tấu', N'Tram Tau Commune', 'tram-tau', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21317006', N'Hạnh Phúc', N'Hanh Phuc', N'Xã Hạnh Phúc', N'Hanh Phuc Commune', 'hanh-phuc', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21317007', N'Phình Hồ', N'Phinh Ho', N'Xã Phình Hồ', N'Phinh Ho Commune', 'phinh-ho', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21303008', N'Nghĩa Lộ', N'Nghia Lo', N'Phường Nghĩa Lộ', N'Nghia Lo Ward', 'nghia-lo', 7, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21303009', N'Trung Tâm', N'Trung Tam', N'Phường Trung Tâm', N'Trung Tam Ward', 'trung-tam', 7, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21303010', N'Cầu Thia', N'Cau Thia', N'Phường Cầu Thia', N'Cau Thia Ward', 'cau-thia', 7, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21303011', N'Liên Sơn', N'Lien Son', N'Xã Liên Sơn', N'Lien Son Commune', 'lien-son', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21315012', N'Gia Hội', N'Gia Hoi', N'Xã Gia Hội', N'Gia Hoi Commune', 'gia-hoi', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21315013', N'Sơn Lương', N'Son Luong', N'Xã Sơn Lương', N'Son Luong Commune', 'son-luong', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21315014', N'Thượng Bằng La', N'Thuong Bang La', N'Xã Thượng Bằng La', N'Thuong Bang La Commune', 'thuong-bang-la', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21315015', N'Chấn Thịnh', N'Chan Thinh', N'Xã Chấn Thịnh', N'Chan Thinh Commune', 'chan-thinh', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21315016', N'Nghĩa Tâm', N'Nghia Tam', N'Xã Nghĩa Tâm', N'Nghia Tam Commune', 'nghia-tam', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21315017', N'Văn Chấn', N'Van Chan', N'Xã Văn Chấn', N'Van Chan Commune', 'van-chan', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21307018', N'Phong Dụ Hạ', N'Phong Du Ha', N'Xã Phong Dụ Hạ', N'Phong Du Ha Commune', 'phong-du-ha', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21307019', N'Châu Quế', N'Chau Que', N'Xã Châu Quế', N'Chau Que Commune', 'chau-que', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21307020', N'Lâm Giang', N'Lam Giang', N'Xã Lâm Giang', N'Lam Giang Commune', 'lam-giang', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21307021', N'Đông Cuông', N'Dong Cuong', N'Xã Đông Cuông', N'Dong Cuong Commune', 'dong-cuong', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21307022', N'Tân Hợp', N'Tan Hop', N'Xã Tân Hợp', N'Tan Hop Commune', 'tan-hop', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21307023', N'Mậu A', N'Mau A', N'Xã Mậu A', N'Mau A Commune', 'mau-a', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21307024', N'Xuân Ái', N'Xuan Ai', N'Xã Xuân Ái', N'Xuan Ai Commune', 'xuan-ai', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21307025', N'Mỏ Vàng', N'Mỏ Vang', N'Xã Mỏ Vàng', N'Mỏ Vang Commune', 'mo-vang', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21305026', N'Lâm Thượng', N'Lam Thuong', N'Xã Lâm Thượng', N'Lam Thuong Commune', 'lam-thuong', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21305027', N'Lục Yên', N'Luc Yen', N'Xã Lục Yên', N'Luc Yen Commune', 'luc-yen', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21305028', N'Tân Lĩnh', N'Tan Linh', N'Xã Tân Lĩnh', N'Tan Linh Commune', 'tan-linh', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21305029', N'Khánh Hoà', N'Khanh Hoa', N'Xã Khánh Hoà', N'Khanh Hoa Commune', 'khanh-hoa', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21305030', N'Phúc Lợi', N'Phuc Loi', N'Xã Phúc Lợi', N'Phuc Loi Commune', 'phuc-loi', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21305031', N'Mường Lai', N'Muong Lai', N'Xã Mường Lai', N'Muong Lai Commune', 'muong-lai', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21313032', N'Cảm Nhân', N'Cam Nhan', N'Xã Cảm Nhân', N'Cam Nhan Commune', 'cam-nhan', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21313033', N'Yên Thành', N'Yen Thanh', N'Xã Yên Thành', N'Yen Thanh Commune', 'yen-thanh', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21313034', N'Thác Bà', N'Thac Ba', N'Xã Thác Bà', N'Thac Ba Commune', 'thac-ba', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21313035', N'Yên Bình', N'Yen Binh', N'Xã Yên Bình', N'Yen Binh Commune', 'yen-binh', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21313036', N'Bảo Ái', N'Bao Ai', N'Xã Bảo Ái', N'Bao Ai Commune', 'bao-ai', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21301037', N'Văn Phú', N'Van Phu', N'Phường Văn Phú', N'Van Phu Ward', 'van-phu', 7, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21301038', N'Yên Bái', N'Yen Bai', N'Phường Yên Bái', N'Yen Bai Ward', 'yen-bai', 7, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21301039', N'Nam Cường', N'Nam Cuong', N'Phường Nam Cường', N'Nam Cuong Ward', 'nam-cuong', 7, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21301040', N'Âu Lâu', N'Au Lau', N'Phường Âu Lâu', N'Au Lau Ward', 'au-lau', 7, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21311041', N'Trấn Yên', N'Tran Yen', N'Xã Trấn Yên', N'Tran Yen Commune', 'tran-yen', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21311042', N'Hưng Khánh', N'Hung Khanh', N'Xã Hưng Khánh', N'Hung Khanh Commune', 'hung-khanh', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21311043', N'Lương Thịnh', N'Luong Thinh', N'Xã Lương Thịnh', N'Luong Thinh Commune', 'luong-thinh', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21311044', N'Việt Hồng', N'Viet Hong', N'Xã Việt Hồng', N'Viet Hong Commune', 'viet-hong', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21311045', N'Quy Mông', N'Quy Mong', N'Xã Quy Mông', N'Quy Mong Commune', 'quy-mong', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20511046', N'Phong Hải', N'Phong Hai', N'Xã Phong Hải', N'Phong Hai Commune', 'phong-hai', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20511047', N'Xuân Quang', N'Xuan Quang', N'Xã Xuân Quang', N'Xuan Quang Commune', 'xuan-quang', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20511048', N'Bảo Thắng', N'Bao Thang', N'Xã Bảo Thắng', N'Bao Thang Commune', 'bao-thang', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20511049', N'Tằng Lỏong', N'Tang Lỏong', N'Xã Tằng Lỏong', N'Tang Lỏong Commune', 'tang-loong', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20511050', N'Gia Phú', N'Gia Phu', N'Xã Gia Phú', N'Gia Phu Commune', 'gia-phu', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20501051', N'Cốc San', N'Coc San', N'Xã Cốc San', N'Coc San Commune', 'coc-san', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20501052', N'Hợp Thành', N'Hop Thanh', N'Xã Hợp Thành', N'Hop Thanh Commune', 'hop-thanh', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20501053', N'Cam Đường', N'Cam Duong', N'Phường Cam Đường', N'Cam Duong Ward', 'cam-duong', 7, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20501054', N'Lào Cai', N'Lao Cai', N'Phường Lào Cai', N'Lao Cai Ward', 'lao-cai', 7, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20507055', N'Mường Hum', N'Muong Hum', N'Xã Mường Hum', N'Muong Hum Commune', 'muong-hum', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20507056', N'Dền Sáng', N'Den Sang', N'Xã Dền Sáng', N'Den Sang Commune', 'den-sang', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20507057', N'Y Tý', N'Y Ty', N'Xã Y Tý', N'Y Ty Commune', 'y-ty', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20507058', N'A Mú Sung', N'A Mu Sung', N'Xã A Mú Sung', N'A Mu Sung Commune', 'a-mu-sung', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20507059', N'Trịnh Tường', N'Trinh Tuong', N'Xã Trịnh Tường', N'Trinh Tuong Commune', 'trinh-tuong', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20507060', N'Bản Xèo', N'Ban Xeo', N'Xã Bản Xèo', N'Ban Xeo Commune', 'ban-xeo', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20507061', N'Bát Xát', N'Bat Xat', N'Xã Bát Xát', N'Bat Xat Commune', 'bat-xat', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20515062', N'Nghĩa Đô', N'Nghia Do', N'Xã Nghĩa Đô', N'Nghia Do Commune', 'nghia-do', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20515063', N'Thượng Hà', N'Thuong Ha', N'Xã Thượng Hà', N'Thuong Ha Commune', 'thuong-ha', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20515064', N'Bảo Yên', N'Bao Yen', N'Xã Bảo Yên', N'Bao Yen Commune', 'bao-yen', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20515065', N'Xuân Hoà', N'Xuan Hoa', N'Xã Xuân Hoà', N'Xuan Hoa Commune', 'xuan-hoa', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20515066', N'Phúc Khánh', N'Phuc Khanh', N'Xã Phúc Khánh', N'Phuc Khanh Commune', 'phuc-khanh', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20515067', N'Bảo Hà', N'Bao Ha', N'Xã Bảo Hà', N'Bao Ha Commune', 'bao-ha', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20519068', N'Võ Lao', N'Vo Lao', N'Xã Võ Lao', N'Vo Lao Commune', 'vo-lao', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20519069', N'Khánh Yên', N'Khanh Yen', N'Xã Khánh Yên', N'Khanh Yen Commune', 'khanh-yen', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20519070', N'Văn Bàn', N'Van Ban', N'Xã Văn Bàn', N'Van Ban Commune', 'van-ban', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20519071', N'Dương Quỳ', N'Duong Quy', N'Xã Dương Quỳ', N'Duong Quy Commune', 'duong-quy', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20519072', N'Chiềng Ken', N'Chieng Ken', N'Xã Chiềng Ken', N'Chieng Ken Commune', 'chieng-ken', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20519073', N'Minh Lương', N'Minh Luong', N'Xã Minh Lương', N'Minh Luong Commune', 'minh-luong', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20519074', N'Nậm Chày', N'Nam Chay', N'Xã Nậm Chày', N'Nam Chay Commune', 'nam-chay', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20513075', N'Mường Bo', N'Muong Bo', N'Xã Mường Bo', N'Muong Bo Commune', 'muong-bo', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20513076', N'Bản Hồ', N'Ban Ho', N'Xã Bản Hồ', N'Ban Ho Commune', 'ban-ho', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20513077', N'Tả Phìn', N'Ta Phin', N'Xã Tả Phìn', N'Ta Phin Commune', 'ta-phin', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20513078', N'Tả Van', N'Ta Van', N'Xã Tả Van', N'Ta Van Commune', 'ta-van', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20513079', N'Sa Pa', N'Sa Pa', N'Phường Sa Pa', N'Sa Pa Ward', 'sa-pa', 7, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20509080', N'Cốc Lầu', N'Coc Lau', N'Xã Cốc Lầu', N'Coc Lau Commune', 'coc-lau', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20509081', N'Bảo Nhai', N'Bao Nhai', N'Xã Bảo Nhai', N'Bao Nhai Commune', 'bao-nhai', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20509082', N'Bản Liền', N'Ban Lien', N'Xã Bản Liền', N'Ban Lien Commune', 'ban-lien', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20509083', N'Bắc Hà', N'Bac Ha', N'Xã Bắc Hà', N'Bac Ha Commune', 'bac-ha', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20509084', N'Tả Củ Tỷ', N'Ta Cu Ty', N'Xã Tả Củ Tỷ', N'Ta Cu Ty Commune', 'ta-cu-ty', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20509085', N'Lùng Phình', N'Lung Phinh', N'Xã Lùng Phình', N'Lung Phinh Commune', 'lung-phinh', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20505086', N'Pha Long', N'Pha Long', N'Xã Pha Long', N'Pha Long Commune', 'pha-long', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20505087', N'Mường Khương', N'Muong Khuong', N'Xã Mường Khương', N'Muong Khuong Commune', 'muong-khuong', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20505088', N'Bản Lầu', N'Ban Lau', N'Xã Bản Lầu', N'Ban Lau Commune', 'ban-lau', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20505089', N'Cao Sơn', N'Cao Son', N'Xã Cao Sơn', N'Cao Son Commune', 'cao-son', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20521090', N'Si Ma Cai', N'Si Ma Cai', N'Xã Si Ma Cai', N'Si Ma Cai Commune', 'si-ma-cai', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20521091', N'Sín Chéng', N'Sin Cheng', N'Xã Sín Chéng', N'Sin Cheng Commune', 'sin-cheng', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21309092', N'Lao Chải', N'Lao Chai', N'Xã Lao Chải', N'Lao Chai Commune', 'lao-chai', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21309093', N'Chế Tạo', N'Che Tao', N'Xã Chế Tạo', N'Che Tao Commune', 'che-tao', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21309094', N'Nậm Có', N'Nam Co', N'Xã Nậm Có', N'Nam Co Commune', 'nam-co', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21317095', N'Tà Xi Láng', N'Ta Xi Lang', N'Xã Tà Xi Láng', N'Ta Xi Lang Commune', 'ta-xi-lang', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21307096', N'Phong Dụ Thượng', N'Phong Du Thuong', N'Xã Phong Dụ Thượng', N'Phong Du Thuong Commune', 'phong-du-thuong', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21315097', N'Cát Thịnh', N'Cat Thinh', N'Xã Cát Thịnh', N'Cat Thinh Commune', 'cat-thinh', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20519098', N'Nậm Xé', N'Nam Xe', N'Xã Nậm Xé', N'Nam Xe Commune', 'nam-xe', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20513099', N'Ngũ Chỉ Sơn', N'Ngu Chi Son', N'Xã Ngũ Chỉ Sơn', N'Ngu Chi Son Commune', 'ngu-chi-son', 8, N'205', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21501001', N'Phan Đình Phùng', N'Phan Dinh Phung', N'Phường Phan Đình Phùng', N'Phan Dinh Phung Ward', 'phan-dinh-phung', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21501002', N'Linh Sơn', N'Linh Son', N'Phường Linh Sơn', N'Linh Son Ward', 'linh-son', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21501003', N'Tích Lương', N'Tich Luong', N'Phường Tích Lương', N'Tich Luong Ward', 'tich-luong', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21501004', N'Gia Sàng', N'Gia Sang', N'Phường Gia Sàng', N'Gia Sang Ward', 'gia-sang', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21501005', N'Quyết Thắng', N'Quyet Thang', N'Phường Quyết Thắng', N'Quyet Thang Ward', 'quyet-thang', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21501006', N'Quan Triều', N'Quan Trieu', N'Phường Quan Triều', N'Quan Trieu Ward', 'quan-trieu', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21501007', N'Tân Cương', N'Tan Cuong', N'Xã Tân Cương', N'Tan Cuong Commune', 'tan-cuong', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21501008', N'Đại Phúc', N'Dai Phuc', N'Xã Đại Phúc', N'Dai Phuc Commune', 'dai-phuc', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21513009', N'Đại Từ', N'Dai Tu', N'Xã Đại Từ', N'Dai Tu Commune', 'dai-tu', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21513010', N'Đức Lương', N'Duc Luong', N'Xã Đức Lương', N'Duc Luong Commune', 'duc-luong', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21513011', N'Phú Thịnh', N'Phu Thinh', N'Xã Phú Thịnh', N'Phu Thinh Commune', 'phu-thinh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21513012', N'La Bằng', N'La Bang', N'Xã La Bằng', N'La Bang Commune', 'la-bang', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21513013', N'Phú Lạc', N'Phu Lac', N'Xã Phú Lạc', N'Phu Lac Commune', 'phu-lac', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21513014', N'An Khánh', N'An Khanh', N'Xã An Khánh', N'An Khanh Commune', 'an-khanh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21513015', N'Quân Chu', N'Quan Chu', N'Xã Quân Chu', N'Quan Chu Commune', 'quan-chu', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21513016', N'Vạn Phú', N'Van Phu', N'Xã Vạn Phú', N'Van Phu Commune', 'van-phu', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21513017', N'Phú Xuyên', N'Phu Xuyen', N'Xã Phú Xuyên', N'Phu Xuyen Commune', 'phu-xuyen', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21517018', N'Phổ Yên', N'Pho Yen', N'Phường Phổ Yên', N'Pho Yen Ward', 'pho-yen', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21517019', N'Vạn Xuân', N'Van Xuan', N'Phường Vạn Xuân', N'Van Xuan Ward', 'van-xuan', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21517020', N'Trung Thành', N'Trung Thanh', N'Phường Trung Thành', N'Trung Thanh Ward', 'trung-thanh', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21517021', N'Phúc Thuận', N'Phuc Thuan', N'Phường Phúc Thuận', N'Phuc Thuan Ward', 'phuc-thuan', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21517022', N'Thành Công', N'Thanh Cong', N'Xã Thành Công', N'Thanh Cong Commune', 'thanh-cong', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21515023', N'Phú Bình', N'Phu Binh', N'Xã Phú Bình', N'Phu Binh Commune', 'phu-binh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21515024', N'Tân Thành', N'Tan Thanh', N'Xã Tân Thành', N'Tan Thanh Commune', 'tan-thanh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21515025', N'Điềm Thụy', N'Diem Thuy', N'Xã Điềm Thụy', N'Diem Thuy Commune', 'diem-thuy', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21515026', N'Kha Sơn', N'Kha Son', N'Xã Kha Sơn', N'Kha Son Commune', 'kha-son', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21515027', N'Tân Khánh', N'Tan Khanh', N'Xã Tân Khánh', N'Tan Khanh Commune', 'tan-khanh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21511028', N'Đồng Hỷ', N'Dong Hy', N'Xã Đồng Hỷ', N'Dong Hy Commune', 'dong-hy', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21511029', N'Quang Sơn', N'Quang Son', N'Xã Quang Sơn', N'Quang Son Commune', 'quang-son', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21511030', N'Trại Cau', N'Trai Cau', N'Xã Trại Cau', N'Trai Cau Commune', 'trai-cau', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21511031', N'Nam Hoà', N'Nam Hoa', N'Xã Nam Hoà', N'Nam Hoa Commune', 'nam-hoa', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21511032', N'Văn Hán', N'Van Han', N'Xã Văn Hán', N'Van Han Commune', 'van-han', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21511033', N'Văn Lăng', N'Van Lang', N'Xã Văn Lăng', N'Van Lang Commune', 'van-lang', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21503034', N'Sông Công', N'Song Cong', N'Phường Sông Công', N'Song Cong Ward', 'song-cong', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21503035', N'Bá Xuyên', N'Ba Xuyen', N'Phường Bá Xuyên', N'Ba Xuyen Ward', 'ba-xuyen', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21503036', N'Bách Quang', N'Bach Quang', N'Phường Bách Quang', N'Bach Quang Ward', 'bach-quang', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21509037', N'Phú Lương', N'Phu Luong', N'Xã Phú Lương', N'Phu Luong Commune', 'phu-luong', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21509038', N'Vô Tranh', N'Vo Tranh', N'Xã Vô Tranh', N'Vo Tranh Commune', 'vo-tranh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21509039', N'Yên Trạch', N'Yen Trach', N'Xã Yên Trạch', N'Yen Trach Commune', 'yen-trach', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21509040', N'Hợp Thành', N'Hop Thanh', N'Xã Hợp Thành', N'Hop Thanh Commune', 'hop-thanh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21505041', N'Định Hóa', N'Dinh Hoa', N'Xã Định Hóa', N'Dinh Hoa Commune', 'dinh-hoa', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21505042', N'Bình Yên', N'Binh Yen', N'Xã Bình Yên', N'Binh Yen Commune', 'binh-yen', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21505043', N'Trung Hội', N'Trung Hoi', N'Xã Trung Hội', N'Trung Hoi Commune', 'trung-hoi', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21505044', N'Phượng Tiến', N'Phuong Tien', N'Xã Phượng Tiến', N'Phuong Tien Commune', 'phuong-tien', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21505045', N'Phú Đình', N'Phu Dinh', N'Xã Phú Đình', N'Phu Dinh Commune', 'phu-dinh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21505046', N'Bình Thành', N'Binh Thanh', N'Xã Bình Thành', N'Binh Thanh Commune', 'binh-thanh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21505047', N'Kim Phượng', N'Kim Phuong', N'Xã Kim Phượng', N'Kim Phuong Commune', 'kim-phuong', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21505048', N'Lam Vỹ', N'Lam Vy', N'Xã Lam Vỹ', N'Lam Vy Commune', 'lam-vy', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21507049', N'Võ Nhai', N'Vo Nhai', N'Xã Võ Nhai', N'Vo Nhai Commune', 'vo-nhai', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21507050', N'Dân Tiến', N'Dan Tien', N'Xã Dân Tiến', N'Dan Tien Commune', 'dan-tien', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21507051', N'Nghinh Tường', N'Nghinh Tuong', N'Xã Nghinh Tường', N'Nghinh Tuong Commune', 'nghinh-tuong', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21507052', N'Thần Sa', N'Than Sa', N'Xã Thần Sa', N'Than Sa Commune', 'than-sa', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21507053', N'La Hiên', N'La Hien', N'Xã La Hiên', N'La Hien Commune', 'la-hien', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21507054', N'Tràng Xá', N'Trang Xa', N'Xã Tràng Xá', N'Trang Xa Commune', 'trang-xa', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20704055', N'Bằng Thành', N'Bang Thanh', N'Xã Bằng Thành', N'Bang Thanh Commune', 'bang-thanh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20704056', N'Nghiên Loan', N'Nghien Loan', N'Xã Nghiên Loan', N'Nghien Loan Commune', 'nghien-loan', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20704057', N'Cao Minh', N'Cao Minh', N'Xã Cao Minh', N'Cao Minh Commune', 'cao-minh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20703058', N'Ba Bể', N'Ba Be', N'Xã Ba Bể', N'Ba Be Commune', 'ba-be', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20703059', N'Chợ Rã', N'Cho Ra', N'Xã Chợ Rã', N'Cho Ra Commune', 'cho-ra', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20703060', N'Phúc Lộc', N'Phuc Loc', N'Xã Phúc Lộc', N'Phuc Loc Commune', 'phuc-loc', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20703061', N'Thượng Minh', N'Thuong Minh', N'Xã Thượng Minh', N'Thuong Minh Commune', 'thuong-minh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20703062', N'Đồng Phúc', N'Dong Phuc', N'Xã Đồng Phúc', N'Dong Phuc Commune', 'dong-phuc', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20713063', N'Yên Bình', N'Yen Binh', N'Xã Yên Bình', N'Yen Binh Commune', 'yen-binh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20705064', N'Bằng Vân', N'Bang Van', N'Xã Bằng Vân', N'Bang Van Commune', 'bang-van', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20705065', N'Ngân Sơn', N'Ngan Son', N'Xã Ngân Sơn', N'Ngan Son Commune', 'ngan-son', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20705066', N'Nà Phặc', N'Na Phac', N'Xã Nà Phặc', N'Na Phac Commune', 'na-phac', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20705067', N'Hiệp Lực', N'Hiep Luc', N'Xã Hiệp Lực', N'Hiep Luc Commune', 'hiep-luc', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20707068', N'Nam Cường', N'Nam Cuong', N'Xã Nam Cường', N'Nam Cuong Commune', 'nam-cuong', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20707069', N'Quảng Bạch', N'Quang Bach', N'Xã Quảng Bạch', N'Quang Bach Commune', 'quang-bach', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20707070', N'Yên Thịnh', N'Yen Thinh', N'Xã Yên Thịnh', N'Yen Thinh Commune', 'yen-thinh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20707071', N'Chợ Đồn', N'Cho Don', N'Xã Chợ Đồn', N'Cho Don Commune', 'cho-don', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20707072', N'Yên Phong', N'Yen Phong', N'Xã Yên Phong', N'Yen Phong Commune', 'yen-phong', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20707073', N'Nghĩa Tá', N'Nghia Ta', N'Xã Nghĩa Tá', N'Nghia Ta Commune', 'nghia-ta', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20711074', N'Phủ Thông', N'Phu Thong', N'Xã Phủ Thông', N'Phu Thong Commune', 'phu-thong', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20711075', N'Cẩm Giàng', N'Cam Giang', N'Xã Cẩm Giàng', N'Cam Giang Commune', 'cam-giang', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20711076', N'Vĩnh Thông', N'Vinh Thong', N'Xã Vĩnh Thông', N'Vinh Thong Commune', 'vinh-thong', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20711077', N'Bạch Thông', N'Bach Thong', N'Xã Bạch Thông', N'Bach Thong Commune', 'bach-thong', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20701078', N'Phong Quang', N'Phong Quang', N'Xã Phong Quang', N'Phong Quang Commune', 'phong-quang', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20701079', N'Đức Xuân', N'Duc Xuan', N'Phường Đức Xuân', N'Duc Xuan Ward', 'duc-xuan', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20701080', N'Bắc Kạn', N'Bac Kan', N'Phường Bắc Kạn', N'Bac Kan Ward', 'bac-kan', 7, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20709081', N'Văn Lang', N'Van Lang', N'Xã Văn Lang', N'Van Lang Commune', 'van-lang', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20709082', N'Cường Lợi', N'Cuong Loi', N'Xã Cường Lợi', N'Cuong Loi Commune', 'cuong-loi', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20709083', N'Na Rì', N'Na Ri', N'Xã Na Rì', N'Na Ri Commune', 'na-ri', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20709084', N'Trần Phú', N'Tran Phu', N'Xã Trần Phú', N'Tran Phu Commune', 'tran-phu', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20709085', N'Côn Minh', N'Con Minh', N'Xã Côn Minh', N'Con Minh Commune', 'con-minh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20709086', N'Xuân Dương', N'Xuan Duong', N'Xã Xuân Dương', N'Xuan Duong Commune', 'xuan-duong', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20713087', N'Tân Kỳ', N'Tan Ky', N'Xã Tân Kỳ', N'Tan Ky Commune', 'tan-ky', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20713088', N'Thanh Mai', N'Thanh Mai', N'Xã Thanh Mai', N'Thanh Mai Commune', 'thanh-mai', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20713089', N'Thanh Thịnh', N'Thanh Thinh', N'Xã Thanh Thịnh', N'Thanh Thinh Commune', 'thanh-thinh', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20713090', N'Chợ Mới', N'Cho Moi', N'Xã Chợ Mới', N'Cho Moi Commune', 'cho-moi', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21507091', N'Sảng Mộc', N'Sang Moc', N'Xã Sảng Mộc', N'Sang Moc Commune', 'sang-moc', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20705092', N'Thượng Quan', N'Thuong Quan', N'Xã Thượng Quan', N'Thuong Quan Commune', 'thuong-quan', 8, N'215', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20903001', N'Thất Khê', N'That Khe', N'Xã Thất Khê', N'That Khe Commune', 'that-khe', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20903002', N'Đoàn Kết', N'Doan Ket', N'Xã Đoàn Kết', N'Doan Ket Commune', 'doan-ket', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20903003', N'Tân Tiến', N'Tan Tien', N'Xã Tân Tiến', N'Tan Tien Commune', 'tan-tien', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20903004', N'Tràng Định', N'Trang Dinh', N'Xã Tràng Định', N'Trang Dinh Commune', 'trang-dinh', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20903005', N'Quốc Khánh', N'Quoc Khanh', N'Xã Quốc Khánh', N'Quoc Khanh Commune', 'quoc-khanh', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20903006', N'Kháng Chiến', N'Khang Chien', N'Xã Kháng Chiến', N'Khang Chien Commune', 'khang-chien', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20903007', N'Quốc Việt', N'Quoc Viet', N'Xã Quốc Việt', N'Quoc Viet Commune', 'quoc-viet', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20907008', N'Bình Gia', N'Binh Gia', N'Xã Bình Gia', N'Binh Gia Commune', 'binh-gia', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20907009', N'Tân Văn', N'Tan Van', N'Xã Tân Văn', N'Tan Van Commune', 'tan-van', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20907010', N'Hồng Phong', N'Hong Phong', N'Xã Hồng Phong', N'Hong Phong Commune', 'hong-phong', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20907011', N'Hoa Thám', N'Hoa Tham', N'Xã Hoa Thám', N'Hoa Tham Commune', 'hoa-tham', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20907012', N'Quý Hoà', N'Quy Hoa', N'Xã Quý Hoà', N'Quy Hoa Commune', 'quy-hoa', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20907013', N'Thiện Hoà', N'Thien Hoa', N'Xã Thiện Hoà', N'Thien Hoa Commune', 'thien-hoa', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20907014', N'Thiện Thuật', N'Thien Thuat', N'Xã Thiện Thuật', N'Thien Thuat Commune', 'thien-thuat', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20907015', N'Thiện Long', N'Thien Long', N'Xã Thiện Long', N'Thien Long Commune', 'thien-long', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20909016', N'Bắc Sơn', N'Bac Son', N'Xã Bắc Sơn', N'Bac Son Commune', 'bac-son', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20909017', N'Hưng Vũ', N'Hung Vu', N'Xã Hưng Vũ', N'Hung Vu Commune', 'hung-vu', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20909018', N'Vũ Lăng', N'Vu Lang', N'Xã Vũ Lăng', N'Vu Lang Commune', 'vu-lang', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20909019', N'Nhất Hoà', N'Nhat Hoa', N'Xã Nhất Hoà', N'Nhat Hoa Commune', 'nhat-hoa', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20909020', N'Vũ Lễ', N'Vu Le', N'Xã Vũ Lễ', N'Vu Le Commune', 'vu-le', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20909021', N'Tân Tri', N'Tan Tri', N'Xã Tân Tri', N'Tan Tri Commune', 'tan-tri', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20911022', N'Văn Quan', N'Van Quan', N'Xã Văn Quan', N'Van Quan Commune', 'van-quan', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20911023', N'Điềm He', N'Diem He', N'Xã Điềm He', N'Diem He Commune', 'diem-he', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20911024', N'Tri Lễ', N'Tri Le', N'Xã Tri Lễ', N'Tri Le Commune', 'tri-le', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20911025', N'Yên Phúc', N'Yen Phuc', N'Xã Yên Phúc', N'Yen Phuc Commune', 'yen-phuc', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20911026', N'Tân Đoàn', N'Tan Doan', N'Xã Tân Đoàn', N'Tan Doan Commune', 'tan-doan', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20913027', N'Khánh Khê', N'Khanh Khe', N'Xã Khánh Khê', N'Khanh Khe Commune', 'khanh-khe', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20905028', N'Na Sầm', N'Na Sam', N'Xã Na Sầm', N'Na Sam Commune', 'na-sam', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20905029', N'Văn Lãng', N'Van Lang', N'Xã Văn Lãng', N'Van Lang Commune', 'van-lang', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20905030', N'Hội Hoan', N'Hoi Hoan', N'Xã Hội Hoan', N'Hoi Hoan Commune', 'hoi-hoan', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20905031', N'Thụy Hùng', N'Thuy Hung', N'Xã Thụy Hùng', N'Thuy Hung Commune', 'thuy-hung', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20905032', N'Hoàng Văn Thụ', N'Hoang Van Thu', N'Xã Hoàng Văn Thụ', N'Hoang Van Thu Commune', 'hoang-van-thu', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20915033', N'Lộc Bình', N'Loc Binh', N'Xã Lộc Bình', N'Loc Binh Commune', 'loc-binh', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20915034', N'Mẫu Sơn', N'Mau Son', N'Xã Mẫu Sơn', N'Mau Son Commune', 'mau-son', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20915035', N'Na Dương', N'Na Duong', N'Xã Na Dương', N'Na Duong Commune', 'na-duong', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20915036', N'Lợi Bác', N'Loi Bac', N'Xã Lợi Bác', N'Loi Bac Commune', 'loi-bac', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20915037', N'Thống Nhất', N'Thong Nhat', N'Xã Thống Nhất', N'Thong Nhat Commune', 'thong-nhat', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20915038', N'Xuân Dương', N'Xuan Duong', N'Xã Xuân Dương', N'Xuan Duong Commune', 'xuan-duong', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20915039', N'Khuất Xá', N'Khuat Xa', N'Xã Khuất Xá', N'Khuat Xa Commune', 'khuat-xa', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20919040', N'Đình Lập', N'Dinh Lap', N'Xã Đình Lập', N'Dinh Lap Commune', 'dinh-lap', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20919041', N'Châu Sơn', N'Chau Son', N'Xã Châu Sơn', N'Chau Son Commune', 'chau-son', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20919042', N'Kiên Mộc', N'Kien Moc', N'Xã Kiên Mộc', N'Kien Moc Commune', 'kien-moc', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20919043', N'Thái Bình', N'Thai Binh', N'Xã Thái Bình', N'Thai Binh Commune', 'thai-binh', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20921044', N'Hữu Lũng', N'Huu Lung', N'Xã Hữu Lũng', N'Huu Lung Commune', 'huu-lung', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20921045', N'Tuấn Sơn', N'Tuan Son', N'Xã Tuấn Sơn', N'Tuan Son Commune', 'tuan-son', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20921046', N'Tân Thành', N'Tan Thanh', N'Xã Tân Thành', N'Tan Thanh Commune', 'tan-thanh', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20921047', N'Vân Nham', N'Van Nham', N'Xã Vân Nham', N'Van Nham Commune', 'van-nham', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20921048', N'Thiện Tân', N'Thien Tan', N'Xã Thiện Tân', N'Thien Tan Commune', 'thien-tan', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20921049', N'Yên Bình', N'Yen Binh', N'Xã Yên Bình', N'Yen Binh Commune', 'yen-binh', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20921050', N'Hữu Liên', N'Huu Lien', N'Xã Hữu Liên', N'Huu Lien Commune', 'huu-lien', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20921051', N'Cai Kinh', N'Cai Kinh', N'Xã Cai Kinh', N'Cai Kinh Commune', 'cai-kinh', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20917052', N'Chi Lăng', N'Chi Lang', N'Xã Chi Lăng', N'Chi Lang Commune', 'chi-lang', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20917053', N'Nhân Lý', N'Nhan Ly', N'Xã Nhân Lý', N'Nhan Ly Commune', 'nhan-ly', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20917054', N'Chiến Thắng', N'Chien Thang', N'Xã Chiến Thắng', N'Chien Thang Commune', 'chien-thang', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20917055', N'Quan Sơn', N'Quan Son', N'Xã Quan Sơn', N'Quan Son Commune', 'quan-son', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20917056', N'Bằng Mạc', N'Bang Mac', N'Xã Bằng Mạc', N'Bang Mac Commune', 'bang-mac', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20917057', N'Vạn Linh', N'Van Linh', N'Xã Vạn Linh', N'Van Linh Commune', 'van-linh', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20913058', N'Đồng Đăng', N'Dong Dang', N'Xã Đồng Đăng', N'Dong Dang Commune', 'dong-dang', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20913059', N'Cao Lộc', N'Cao Loc', N'Xã Cao Lộc', N'Cao Loc Commune', 'cao-loc', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20913060', N'Công Sơn', N'Cong Son', N'Xã Công Sơn', N'Cong Son Commune', 'cong-son', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20913061', N'Ba Sơn', N'Ba Son', N'Xã Ba Sơn', N'Ba Son Commune', 'ba-son', 8, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20901062', N'Tam Thanh', N'Tam Thanh', N'Phường Tam Thanh', N'Tam Thanh Ward', 'tam-thanh', 7, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20901063', N'Lương Văn Tri', N'Luong Van Tri', N'Phường Lương Văn Tri', N'Luong Van Tri Ward', 'luong-van-tri', 7, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20913064', N'Kỳ Lừa', N'Ky Lua', N'Phường Kỳ Lừa', N'Ky Lua Ward', 'ky-lua', 7, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'20901065', N'Đông Kinh', N'Dong Kinh', N'Phường Đông Kinh', N'Dong Kinh Ward', 'dong-kinh', 7, N'209', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21701001', N'Việt Trì', N'Viet Tri', N'Phường Việt Trì', N'Viet Tri Ward', 'viet-tri', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21701002', N'Nông Trang', N'Nong Trang', N'Phường Nông Trang', N'Nong Trang Ward', 'nong-trang', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21701003', N'Thanh Miếu', N'Thanh Mieu', N'Phường Thanh Miếu', N'Thanh Mieu Ward', 'thanh-mieu', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21701004', N'Vân Phú', N'Van Phu', N'Phường Vân Phú', N'Van Phu Ward', 'van-phu', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21701005', N'Hy Cương', N'Hy Cuong', N'Xã Hy Cương', N'Hy Cuong Commune', 'hy-cuong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21721006', N'Lâm Thao', N'Lam Thao', N'Xã Lâm Thao', N'Lam Thao Commune', 'lam-thao', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21721007', N'Xuân Lũng', N'Xuan Lung', N'Xã Xuân Lũng', N'Xuan Lung Commune', 'xuan-lung', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21721008', N'Phùng Nguyên', N'Phung Nguyen', N'Xã Phùng Nguyên', N'Phung Nguyen Commune', 'phung-nguyen', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21721009', N'Bản Nguyên', N'Ban Nguyen', N'Xã Bản Nguyên', N'Ban Nguyen Commune', 'ban-nguyen', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21703010', N'Phong Châu', N'Phong Chau', N'Phường Phong Châu', N'Phong Chau Ward', 'phong-chau', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21703011', N'Phú Thọ', N'Phu Tho', N'Phường Phú Thọ', N'Phu Tho Ward', 'phu-tho', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21703012', N'Âu Cơ', N'Au Co', N'Phường Âu Cơ', N'Au Co Ward', 'au-co', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21711013', N'Phù Ninh', N'Phu Ninh', N'Xã Phù Ninh', N'Phu Ninh Commune', 'phu-ninh', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21711014', N'Dân Chủ', N'Dan Chu', N'Xã Dân Chủ', N'Dan Chu Commune', 'dan-chu', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21711015', N'Phú Mỹ', N'Phu My', N'Xã Phú Mỹ', N'Phu My Commune', 'phu-my', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21711016', N'Trạm Thản', N'Tram Than', N'Xã Trạm Thản', N'Tram Than Commune', 'tram-than', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21711017', N'Bình Phú', N'Binh Phu', N'Xã Bình Phú', N'Binh Phu Commune', 'binh-phu', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21709018', N'Thanh Ba', N'Thanh Ba', N'Xã Thanh Ba', N'Thanh Ba Commune', 'thanh-ba', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21709019', N'Quảng Yên', N'Quang Yen', N'Xã Quảng Yên', N'Quang Yen Commune', 'quang-yen', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21709020', N'Hoàng Cương', N'Hoang Cuong', N'Xã Hoàng Cương', N'Hoang Cuong Commune', 'hoang-cuong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21709021', N'Đông Thành', N'Dong Thanh', N'Xã Đông Thành', N'Dong Thanh Commune', 'dong-thanh', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21709022', N'Chí Tiên', N'Chi Tien', N'Xã Chí Tiên', N'Chi Tien Commune', 'chi-tien', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21709023', N'Liên Minh', N'Lien Minh', N'Xã Liên Minh', N'Lien Minh Commune', 'lien-minh', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21705024', N'Đoan Hùng', N'Doan Hung', N'Xã Đoan Hùng', N'Doan Hung Commune', 'doan-hung', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21705025', N'Tây Cốc', N'Tay Coc', N'Xã Tây Cốc', N'Tay Coc Commune', 'tay-coc', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21705026', N'Chân Mộng', N'Chan Mong', N'Xã Chân Mộng', N'Chan Mong Commune', 'chan-mong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21705027', N'Chí Đám', N'Chi Dam', N'Xã Chí Đám', N'Chi Dam Commune', 'chi-dam', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21705028', N'Bằng Luân', N'Bang Luan', N'Xã Bằng Luân', N'Bang Luan Commune', 'bang-luan', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21707029', N'Hạ Hòa', N'Ha Hoa', N'Xã Hạ Hòa', N'Ha Hoa Commune', 'ha-hoa', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21707030', N'Đan Thượng', N'Dan Thuong', N'Xã Đan Thượng', N'Dan Thuong Commune', 'dan-thuong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21707031', N'Yên Kỳ', N'Yen Ky', N'Xã Yên Kỳ', N'Yen Ky Commune', 'yen-ky', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21707032', N'Vĩnh Chân', N'Vinh Chan', N'Xã Vĩnh Chân', N'Vinh Chan Commune', 'vinh-chan', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21707033', N'Văn Lang', N'Van Lang', N'Xã Văn Lang', N'Van Lang Commune', 'van-lang', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21707034', N'Hiền Lương', N'Hien Luong', N'Xã Hiền Lương', N'Hien Luong Commune', 'hien-luong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21713035', N'Cẩm Khê', N'Cam Khe', N'Xã Cẩm Khê', N'Cam Khe Commune', 'cam-khe', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21713036', N'Phú Khê', N'Phu Khe', N'Xã Phú Khê', N'Phu Khe Commune', 'phu-khe', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21713037', N'Hùng Việt', N'Hung Viet', N'Xã Hùng Việt', N'Hung Viet Commune', 'hung-viet', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21713038', N'Đồng Lương', N'Dong Luong', N'Xã Đồng Lương', N'Dong Luong Commune', 'dong-luong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21713039', N'Tiên Lương', N'Tien Luong', N'Xã Tiên Lương', N'Tien Luong Commune', 'tien-luong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21713040', N'Vân Bán', N'Van Ban', N'Xã Vân Bán', N'Van Ban Commune', 'van-ban', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21717041', N'Tam Nông', N'Tam Nong', N'Xã Tam Nông', N'Tam Nong Commune', 'tam-nong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21717042', N'Thọ Văn', N'Tho Van', N'Xã Thọ Văn', N'Tho Van Commune', 'tho-van', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21717043', N'Vạn Xuân', N'Van Xuan', N'Xã Vạn Xuân', N'Van Xuan Commune', 'van-xuan', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21717044', N'Hiền Quan', N'Hien Quan', N'Xã Hiền Quan', N'Hien Quan Commune', 'hien-quan', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21723045', N'Thanh Thuỷ', N'Thanh Thuy', N'Xã Thanh Thuỷ', N'Thanh Thuy Commune', 'thanh-thuy', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21723046', N'Đào Xá', N'Dao Xa', N'Xã Đào Xá', N'Dao Xa Commune', 'dao-xa', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21723047', N'Tu Vũ', N'Tu Vu', N'Xã Tu Vũ', N'Tu Vu Commune', 'tu-vu', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21719048', N'Thanh Sơn', N'Thanh Son', N'Xã Thanh Sơn', N'Thanh Son Commune', 'thanh-son', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21719049', N'Võ Miếu', N'Vo Mieu', N'Xã Võ Miếu', N'Vo Mieu Commune', 'vo-mieu', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21719050', N'Văn Miếu', N'Van Mieu', N'Xã Văn Miếu', N'Van Mieu Commune', 'van-mieu', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21719051', N'Cự Đồng', N'Cu Dong', N'Xã Cự Đồng', N'Cu Dong Commune', 'cu-dong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21719052', N'Hương Cần', N'Huong Can', N'Xã Hương Cần', N'Huong Can Commune', 'huong-can', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21719053', N'Yên Sơn', N'Yen Son', N'Xã Yên Sơn', N'Yen Son Commune', 'yen-son', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21719054', N'Khả Cửu', N'Kha Cuu', N'Xã Khả Cửu', N'Kha Cuu Commune', 'kha-cuu', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21720055', N'Tân Sơn', N'Tan Son', N'Xã Tân Sơn', N'Tan Son Commune', 'tan-son', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21720056', N'Minh Đài', N'Minh Dai', N'Xã Minh Đài', N'Minh Dai Commune', 'minh-dai', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21720057', N'Lai Đồng', N'Lai Dong', N'Xã Lai Đồng', N'Lai Dong Commune', 'lai-dong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21720058', N'Thu Cúc', N'Thu Cuc', N'Xã Thu Cúc', N'Thu Cuc Commune', 'thu-cuc', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21720059', N'Xuân Đài', N'Xuan Dai', N'Xã Xuân Đài', N'Xuan Dai Commune', 'xuan-dai', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21720060', N'Long Cốc', N'Long Coc', N'Xã Long Cốc', N'Long Coc Commune', 'long-coc', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21715061', N'Yên Lập', N'Yen Lap', N'Xã Yên Lập', N'Yen Lap Commune', 'yen-lap', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21715062', N'Thượng Long', N'Thuong Long', N'Xã Thượng Long', N'Thuong Long Commune', 'thuong-long', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21715063', N'Sơn Lương', N'Son Luong', N'Xã Sơn Lương', N'Son Luong Commune', 'son-luong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21715064', N'Xuân Viên', N'Xuan Vien', N'Xã Xuân Viên', N'Xuan Vien Commune', 'xuan-vien', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21715065', N'Minh Hòa', N'Minh Hoa', N'Xã Minh Hòa', N'Minh Hoa Commune', 'minh-hoa', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21715066', N'Trung Sơn', N'Trung Son', N'Xã Trung Sơn', N'Trung Son Commune', 'trung-son', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21915067', N'Tam Sơn', N'Tam Son', N'Xã Tam Sơn', N'Tam Son Commune', 'tam-son', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21915068', N'Sông Lô', N'Song Lo', N'Xã Sông Lô', N'Song Lo Commune', 'song-lo', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21915069', N'Hải Lựu', N'Hai Luu', N'Xã Hải Lựu', N'Hai Luu Commune', 'hai-luu', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21915070', N'Yên Lãng', N'Yen Lang', N'Xã Yên Lãng', N'Yen Lang Commune', 'yen-lang', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21903071', N'Lập Thạch', N'Lap Thach', N'Xã Lập Thạch', N'Lap Thach Commune', 'lap-thach', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21903072', N'Tiên Lữ', N'Tien Lu', N'Xã Tiên Lữ', N'Tien Lu Commune', 'tien-lu', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21903073', N'Thái Hòa', N'Thai Hoa', N'Xã Thái Hòa', N'Thai Hoa Commune', 'thai-hoa', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21903074', N'Liên Hòa', N'Lien Hoa', N'Xã Liên Hòa', N'Lien Hoa Commune', 'lien-hoa', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21903075', N'Hợp Lý', N'Hop Ly', N'Xã Hợp Lý', N'Hop Ly Commune', 'hop-ly', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21903076', N'Sơn Đông', N'Son Dong', N'Xã Sơn Đông', N'Son Dong Commune', 'son-dong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21904077', N'Tam Đảo', N'Tam Dao', N'Xã Tam Đảo', N'Tam Dao Commune', 'tam-dao', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21904078', N'Đại Đình', N'Dai Dinh', N'Xã Đại Đình', N'Dai Dinh Commune', 'dai-dinh', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21904079', N'Đạo Trù', N'Dao Tru', N'Xã Đạo Trù', N'Dao Tru Commune', 'dao-tru', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21905080', N'Tam Dương', N'Tam Duong', N'Xã Tam Dương', N'Tam Duong Commune', 'tam-duong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21905081', N'Hội Thịnh', N'Hoi Thinh', N'Xã Hội Thịnh', N'Hoi Thinh Commune', 'hoi-thinh', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21905082', N'Hoàng An', N'Hoang An', N'Xã Hoàng An', N'Hoang An Commune', 'hoang-an', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21905083', N'Tam Dương Bắc', N'Tam Duong Bac', N'Xã Tam Dương Bắc', N'Tam Duong Bac Commune', 'tam-duong-bac', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21907084', N'Vĩnh Tường', N'Vinh Tuong', N'Xã Vĩnh Tường', N'Vinh Tuong Commune', 'vinh-tuong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21907085', N'Thổ Tang', N'Tho Tang', N'Xã Thổ Tang', N'Tho Tang Commune', 'tho-tang', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21907086', N'Vĩnh Hưng', N'Vinh Hung', N'Xã Vĩnh Hưng', N'Vinh Hung Commune', 'vinh-hung', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21907087', N'Vĩnh An', N'Vinh An', N'Xã Vĩnh An', N'Vinh An Commune', 'vinh-an', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21907088', N'Vĩnh Phú', N'Vinh Phu', N'Xã Vĩnh Phú', N'Vinh Phu Commune', 'vinh-phu', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21907089', N'Vĩnh Thành', N'Vinh Thanh', N'Xã Vĩnh Thành', N'Vinh Thanh Commune', 'vinh-thanh', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21909090', N'Yên Lạc', N'Yen Lac', N'Xã Yên Lạc', N'Yen Lac Commune', 'yen-lac', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21909091', N'Tề Lỗ', N'Te Lo', N'Xã Tề Lỗ', N'Te Lo Commune', 'te-lo', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21909092', N'Liên Châu', N'Lien Chau', N'Xã Liên Châu', N'Lien Chau Commune', 'lien-chau', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21909093', N'Tam Hồng', N'Tam Hong', N'Xã Tam Hồng', N'Tam Hong Commune', 'tam-hong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21909094', N'Nguyệt Đức', N'Nguyet Duc', N'Xã Nguyệt Đức', N'Nguyet Duc Commune', 'nguyet-duc', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21913095', N'Bình Nguyên', N'Binh Nguyen', N'Xã Bình Nguyên', N'Binh Nguyen Commune', 'binh-nguyen', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21913096', N'Xuân Lãng', N'Xuan Lang', N'Xã Xuân Lãng', N'Xuan Lang Commune', 'xuan-lang', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21913097', N'Bình Xuyên', N'Binh Xuyen', N'Xã Bình Xuyên', N'Binh Xuyen Commune', 'binh-xuyen', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21913098', N'Bình Tuyền', N'Binh Tuyen', N'Xã Bình Tuyền', N'Binh Tuyen Commune', 'binh-tuyen', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21901099', N'Vĩnh Phúc', N'Vinh Phuc', N'Phường Vĩnh Phúc', N'Vinh Phuc Ward', 'vinh-phuc', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21901100', N'Vĩnh Yên', N'Vinh Yen', N'Phường Vĩnh Yên', N'Vinh Yen Ward', 'vinh-yen', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21902101', N'Phúc Yên', N'Phuc Yen', N'Phường Phúc Yên', N'Phuc Yen Ward', 'phuc-yen', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'21902102', N'Xuân Hòa', N'Xuan Hoa', N'Phường Xuân Hòa', N'Xuan Hoa Ward', 'xuan-hoa', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30510103', N'Cao Phong', N'Cao Phong', N'Xã Cao Phong', N'Cao Phong Commune', 'cao-phong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30510104', N'Mường Thàng', N'Muong Thang', N'Xã Mường Thàng', N'Muong Thang Commune', 'muong-thang', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30510105', N'Thung Nai', N'Thung Nai', N'Xã Thung Nai', N'Thung Nai Commune', 'thung-nai', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30503106', N'Đà Bắc', N'Da Bac', N'Xã Đà Bắc', N'Da Bac Commune', 'da-bac', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30503107', N'Cao Sơn', N'Cao Son', N'Xã Cao Sơn', N'Cao Son Commune', 'cao-son', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30503108', N'Đức Nhàn', N'Duc Nhan', N'Xã Đức Nhàn', N'Duc Nhan Commune', 'duc-nhan', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30503109', N'Quy Đức', N'Quy Duc', N'Xã Quy Đức', N'Quy Duc Commune', 'quy-duc', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30503110', N'Tân Pheo', N'Tan Pheo', N'Xã Tân Pheo', N'Tan Pheo Commune', 'tan-pheo', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30503111', N'Tiền Phong', N'Tien Phong', N'Xã Tiền Phong', N'Tien Phong Commune', 'tien-phong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30511112', N'Kim Bôi', N'Kim Boi', N'Xã Kim Bôi', N'Kim Boi Commune', 'kim-boi', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30511113', N'Mường Động', N'Muong Dong', N'Xã Mường Động', N'Muong Dong Commune', 'muong-dong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30511114', N'Dũng Tiến', N'Dung Tien', N'Xã Dũng Tiến', N'Dung Tien Commune', 'dung-tien', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30511115', N'Hợp Kim', N'Hop Kim', N'Xã Hợp Kim', N'Hop Kim Commune', 'hop-kim', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30511116', N'Nật Sơn', N'Nat Son', N'Xã Nật Sơn', N'Nat Son Commune', 'nat-son', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30515117', N'Lạc Sơn', N'Lac Son', N'Xã Lạc Sơn', N'Lac Son Commune', 'lac-son', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30515118', N'Mường Vang', N'Muong Vang', N'Xã Mường Vang', N'Muong Vang Commune', 'muong-vang', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30515119', N'Đại Đồng', N'Dai Dong', N'Xã Đại Đồng', N'Dai Dong Commune', 'dai-dong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30515120', N'Ngọc Sơn', N'Ngoc Son', N'Xã Ngọc Sơn', N'Ngoc Son Commune', 'ngoc-son', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30515121', N'Nhân Nghĩa', N'Nhan Nghia', N'Xã Nhân Nghĩa', N'Nhan Nghia Commune', 'nhan-nghia', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30515122', N'Quyết Thắng', N'Quyet Thang', N'Xã Quyết Thắng', N'Quyet Thang Commune', 'quyet-thang', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30515123', N'Thượng Cốc', N'Thuong Coc', N'Xã Thượng Cốc', N'Thuong Coc Commune', 'thuong-coc', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30515124', N'Yên Phú', N'Yen Phu', N'Xã Yên Phú', N'Yen Phu Commune', 'yen-phu', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30517125', N'Lạc Thủy', N'Lac Thuy', N'Xã Lạc Thủy', N'Lac Thuy Commune', 'lac-thuy', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30517126', N'An Bình', N'An Binh', N'Xã An Bình', N'An Binh Commune', 'an-binh', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30517127', N'An Nghĩa', N'An Nghia', N'Xã An Nghĩa', N'An Nghia Commune', 'an-nghia', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30509128', N'Lương Sơn', N'Luong Son', N'Xã Lương Sơn', N'Luong Son Commune', 'luong-son', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30509129', N'Cao Dương', N'Cao Duong', N'Xã Cao Dương', N'Cao Duong Commune', 'cao-duong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30509130', N'Liên Sơn', N'Lien Son', N'Xã Liên Sơn', N'Lien Son Commune', 'lien-son', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30505131', N'Mai Châu', N'Mai Chau', N'Xã Mai Châu', N'Mai Chau Commune', 'mai-chau', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30505132', N'Bao La', N'Bao La', N'Xã Bao La', N'Bao La Commune', 'bao-la', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30505133', N'Mai Hạ', N'Mai Ha', N'Xã Mai Hạ', N'Mai Ha Commune', 'mai-ha', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30505134', N'Pà Cò', N'Pa Co', N'Xã Pà Cò', N'Pa Co Commune', 'pa-co', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30505135', N'Tân Mai', N'Tan Mai', N'Xã Tân Mai', N'Tan Mai Commune', 'tan-mai', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30513136', N'Tân Lạc', N'Tan Lac', N'Xã Tân Lạc', N'Tan Lac Commune', 'tan-lac', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30513137', N'Mường Bi', N'Muong Bi', N'Xã Mường Bi', N'Muong Bi Commune', 'muong-bi', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30513138', N'Mường Hoa', N'Muong Hoa', N'Xã Mường Hoa', N'Muong Hoa Commune', 'muong-hoa', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30513139', N'Toàn Thắng', N'Toan Thang', N'Xã Toàn Thắng', N'Toan Thang Commune', 'toan-thang', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30513140', N'Vân Sơn', N'Van Son', N'Xã Vân Sơn', N'Van Son Commune', 'van-son', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30519141', N'Yên Thủy', N'Yen Thuy', N'Xã Yên Thủy', N'Yen Thuy Commune', 'yen-thuy', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30519142', N'Lạc Lương', N'Lac Luong', N'Xã Lạc Lương', N'Lac Luong Commune', 'lac-luong', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30519143', N'Yên Trị', N'Yen Tri', N'Xã Yên Trị', N'Yen Tri Commune', 'yen-tri', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30501144', N'Thịnh Minh', N'Thinh Minh', N'Xã Thịnh Minh', N'Thinh Minh Commune', 'thinh-minh', 8, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30501145', N'Hoà Bình', N'Hoa Binh', N'Phường Hoà Bình', N'Hoa Binh Ward', 'hoa-binh', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30501146', N'Kỳ Sơn', N'Ky Son', N'Phường Kỳ Sơn', N'Ky Son Ward', 'ky-son', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30501147', N'Tân Hoà', N'Tan Hoa', N'Phường Tân Hoà', N'Tan Hoa Ward', 'tan-hoa', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30501148', N'Thống Nhất', N'Thong Nhat', N'Phường Thống Nhất', N'Thong Nhat Ward', 'thong-nhat', 7, N'217', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30101001', N'Mường Phăng', N'Muong Phang', N'Xã Mường Phăng', N'Muong Phang Commune', 'muong-phang', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30101002', N'Điện Biên Phủ', N'Dien Bien Phu', N'Phường Điện Biên Phủ', N'Dien Bien Phu Ward', 'dien-bien-phu', 7, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30101003', N'Mường Thanh', N'Muong Thanh', N'Phường Mường Thanh', N'Muong Thanh Ward', 'muong-thanh', 7, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30103004', N'Mường Lay', N'Muong Lay', N'Phường Mường Lay', N'Muong Lay Ward', 'muong-lay', 7, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30117005', N'Thanh Nưa', N'Thanh Nua', N'Xã Thanh Nưa', N'Thanh Nua Commune', 'thanh-nua', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30117006', N'Thanh An', N'Thanh An', N'Xã Thanh An', N'Thanh An Commune', 'thanh-an', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30117007', N'Thanh Yên', N'Thanh Yen', N'Xã Thanh Yên', N'Thanh Yen Commune', 'thanh-yen', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30117008', N'Sam Mứn', N'Sam Mun', N'Xã Sam Mứn', N'Sam Mun Commune', 'sam-mun', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30117009', N'Núa Ngam', N'Nua Ngam', N'Xã Núa Ngam', N'Nua Ngam Commune', 'nua-ngam', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30117010', N'Mường Nhà', N'Muong Nha', N'Xã Mường Nhà', N'Muong Nha Commune', 'muong-nha', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30115011', N'Tuần Giáo', N'Tuan Giao', N'Xã Tuần Giáo', N'Tuan Giao Commune', 'tuan-giao', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30115012', N'Quài Tở', N'Quai To', N'Xã Quài Tở', N'Quai To Commune', 'quai-to', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30115013', N'Mường Mùn', N'Muong Mun', N'Xã Mường Mùn', N'Muong Mun Commune', 'muong-mun', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30115014', N'Pú Nhung', N'Pu Nhung', N'Xã Pú Nhung', N'Pu Nhung Commune', 'pu-nhung', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30115015', N'Chiềng Sinh', N'Chieng Sinh', N'Xã Chiềng Sinh', N'Chieng Sinh Commune', 'chieng-sinh', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30113016', N'Tủa Chùa', N'Tua Chua', N'Xã Tủa Chùa', N'Tua Chua Commune', 'tua-chua', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30113017', N'Sín Chải', N'Sin Chai', N'Xã Sín Chải', N'Sin Chai Commune', 'sin-chai', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30113018', N'Sính Phình', N'Sinh Phinh', N'Xã Sính Phình', N'Sinh Phinh Commune', 'sinh-phinh', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30113019', N'Tủa Thàng', N'Tua Thang', N'Xã Tủa Thàng', N'Tua Thang Commune', 'tua-thang', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30113020', N'Sáng Nhè', N'Sang Nhe', N'Xã Sáng Nhè', N'Sang Nhe Commune', 'sang-nhe', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30111021', N'Na Sang', N'Na Sang', N'Xã Na Sang', N'Na Sang Commune', 'na-sang', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30111022', N'Mường Tùng', N'Muong Tung', N'Xã Mường Tùng', N'Muong Tung Commune', 'muong-tung', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30111023', N'Pa Ham', N'Pa Ham', N'Xã Pa Ham', N'Pa Ham Commune', 'pa-ham', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30111024', N'Nậm Nèn', N'Nam Nen', N'Xã Nậm Nèn', N'Nam Nen Commune', 'nam-nen', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30111025', N'Mường Pồn', N'Muong Pon', N'Xã Mường Pồn', N'Muong Pon Commune', 'muong-pon', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30119026', N'Na Son', N'Na Son', N'Xã Na Son', N'Na Son Commune', 'na-son', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30119027', N'Xa Dung', N'Xa Dung', N'Xã Xa Dung', N'Xa Dung Commune', 'xa-dung', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30119028', N'Pu Nhi', N'Pu Nhi', N'Xã Pu Nhi', N'Pu Nhi Commune', 'pu-nhi', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30119029', N'Mường Luân', N'Muong Luan', N'Xã Mường Luân', N'Muong Luan Commune', 'muong-luan', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30119030', N'Tìa Dình', N'Tia Dinh', N'Xã Tìa Dình', N'Tia Dinh Commune', 'tia-dinh', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30119031', N'Phình Giàng', N'Phinh Giang', N'Xã Phình Giàng', N'Phinh Giang Commune', 'phinh-giang', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30123032', N'Mường Chà', N'Muong Cha', N'Xã Mường Chà', N'Muong Cha Commune', 'muong-cha', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30123033', N'Nà Hỳ', N'Na Hy', N'Xã Nà Hỳ', N'Na Hy Commune', 'na-hy', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30123034', N'Nà Bủng', N'Na Bung', N'Xã Nà Bủng', N'Na Bung Commune', 'na-bung', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30123035', N'Chà Tở', N'Cha To', N'Xã Chà Tở', N'Cha To Commune', 'cha-to', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30123036', N'Si Pa Phìn', N'Si Pa Phin', N'Xã Si Pa Phìn', N'Si Pa Phin Commune', 'si-pa-phin', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30104037', N'Mường Nhé', N'Muong Nhe', N'Xã Mường Nhé', N'Muong Nhe Commune', 'muong-nhe', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30104038', N'Sín Thầu', N'Sin Thau', N'Xã Sín Thầu', N'Sin Thau Commune', 'sin-thau', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30104039', N'Mường Toong', N'Muong Toong', N'Xã Mường Toong', N'Muong Toong Commune', 'muong-toong', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30104040', N'Nậm Kè', N'Nam Ke', N'Xã Nậm Kè', N'Nam Ke Commune', 'nam-ke', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30104041', N'Quảng Lâm', N'Quang Lam', N'Xã Quảng Lâm', N'Quang Lam Commune', 'quang-lam', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30121042', N'Mường Ảng', N'Muong Ang', N'Xã Mường Ảng', N'Muong Ang Commune', 'muong-ang', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30121043', N'Nà Tấu', N'Na Tau', N'Xã Nà Tấu', N'Na Tau Commune', 'na-tau', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30121044', N'Búng Lao', N'Bung Lao', N'Xã Búng Lao', N'Bung Lao Commune', 'bung-lao', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30121045', N'Mường Lạn', N'Muong Lan', N'Xã Mường Lạn', N'Muong Lan Commune', 'muong-lan', 8, N'301', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30209001', N'Mường Kim', N'Muong Kim', N'Xã Mường Kim', N'Muong Kim Commune', 'muong-kim', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30209002', N'Khoen On', N'Khoen On', N'Xã Khoen On', N'Khoen On Commune', 'khoen-on', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30209003', N'Than Uyên', N'Than Uyen', N'Xã Than Uyên', N'Than Uyen Commune', 'than-uyen', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30209004', N'Mường Than', N'Muong Than', N'Xã Mường Than', N'Muong Than Commune', 'muong-than', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30211005', N'Pắc Ta', N'Pac Ta', N'Xã Pắc Ta', N'Pac Ta Commune', 'pac-ta', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30211006', N'Nậm Sỏ', N'Nam Sỏ', N'Xã Nậm Sỏ', N'Nam Sỏ Commune', 'nam-so', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30211007', N'Tân Uyên', N'Tan Uyen', N'Xã Tân Uyên', N'Tan Uyen Commune', 'tan-uyen', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30211008', N'Mường Khoa', N'Muong Khoa', N'Xã Mường Khoa', N'Muong Khoa Commune', 'muong-khoa', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30205009', N'Bản Bo', N'Ban Bo', N'Xã Bản Bo', N'Ban Bo Commune', 'ban-bo', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30205010', N'Bình Lư', N'Binh Lu', N'Xã Bình Lư', N'Binh Lu Commune', 'binh-lu', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30205011', N'Tả Lèng', N'Ta Leng', N'Xã Tả Lèng', N'Ta Leng Commune', 'ta-leng', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30205012', N'Khun Há', N'Khun Ha', N'Xã Khun Há', N'Khun Ha Commune', 'khun-ha', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30202013', N'Tân Phong', N'Tan Phong', N'Phường Tân Phong', N'Tan Phong Ward', 'tan-phong', 7, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30202014', N'Đoàn Kết', N'Doan Ket', N'Phường Đoàn Kết', N'Doan Ket Ward', 'doan-ket', 7, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30203015', N'Sin Suối Hồ', N'Sin Suoi Ho', N'Xã Sin Suối Hồ', N'Sin Suoi Ho Commune', 'sin-suoi-ho', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30203016', N'Phong Thổ', N'Phong Tho', N'Xã Phong Thổ', N'Phong Tho Commune', 'phong-tho', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30203017', N'Sì Lở Lầu', N'Si Lo Lau', N'Xã Sì Lở Lầu', N'Si Lo Lau Commune', 'si-lo-lau', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30203018', N'Dào San', N'Dao San', N'Xã Dào San', N'Dao San Commune', 'dao-san', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30203019', N'Khổng Lào', N'Khong Lao', N'Xã Khổng Lào', N'Khong Lao Commune', 'khong-lao', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30207020', N'Tủa Sín Chải', N'Tua Sin Chai', N'Xã Tủa Sín Chải', N'Tua Sin Chai Commune', 'tua-sin-chai', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30207021', N'Sìn Hồ', N'Sin Ho', N'Xã Sìn Hồ', N'Sin Ho Commune', 'sin-ho', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30207022', N'Hồng Thu', N'Hong Thu', N'Xã Hồng Thu', N'Hong Thu Commune', 'hong-thu', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30207023', N'Nậm Tăm', N'Nam Tam', N'Xã Nậm Tăm', N'Nam Tam Commune', 'nam-tam', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30207024', N'Pu Sam Cáp', N'Pu Sam Cap', N'Xã Pu Sam Cáp', N'Pu Sam Cap Commune', 'pu-sam-cap', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30207025', N'Nậm Cuổi', N'Nam Cuoi', N'Xã Nậm Cuổi', N'Nam Cuoi Commune', 'nam-cuoi', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30207026', N'Nậm Mạ', N'Nam Ma', N'Xã Nậm Mạ', N'Nam Ma Commune', 'nam-ma', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30213027', N'Lê Lợi', N'Le Loi', N'Xã Lê Lợi', N'Le Loi Commune', 'le-loi', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30213028', N'Nậm Hàng', N'Nam Hang', N'Xã Nậm Hàng', N'Nam Hang Commune', 'nam-hang', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30213029', N'Mường Mô', N'Muong Mo', N'Xã Mường Mô', N'Muong Mo Commune', 'muong-mo', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30213030', N'Hua Bum', N'Hua Bum', N'Xã Hua Bum', N'Hua Bum Commune', 'hua-bum', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30213031', N'Pa Tần', N'Pa Tan', N'Xã Pa Tần', N'Pa Tan Commune', 'pa-tan', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30201032', N'Bum Nưa', N'Bum Nua', N'Xã Bum Nưa', N'Bum Nua Commune', 'bum-nua', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30201033', N'Bum Tở', N'Bum To', N'Xã Bum Tở', N'Bum To Commune', 'bum-to', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30201034', N'Mường Tè', N'Muong Te', N'Xã Mường Tè', N'Muong Te Commune', 'muong-te', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30201035', N'Thu Lũm', N'Thu Lum', N'Xã Thu Lũm', N'Thu Lum Commune', 'thu-lum', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30201036', N'Pa Ủ', N'Pa U', N'Xã Pa Ủ', N'Pa U Commune', 'pa-u', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30201037', N'Tà Tổng', N'Ta Tong', N'Xã Tà Tổng', N'Ta Tong Commune', 'ta-tong', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30201038', N'Mù Cả', N'Mu Ca', N'Xã Mù Cả', N'Mu Ca Commune', 'mu-ca', 8, N'302', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30301001', N'Tô Hiệu', N'To Hieu', N'Phường Tô Hiệu', N'To Hieu Ward', 'to-hieu', 7, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30301002', N'Chiềng An', N'Chieng An', N'Phường Chiềng An', N'Chieng An Ward', 'chieng-an', 7, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30301003', N'Chiềng Cơi', N'Chieng Coi', N'Phường Chiềng Cơi', N'Chieng Coi Ward', 'chieng-coi', 7, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30301004', N'Chiềng Sinh', N'Chieng Sinh', N'Phường Chiềng Sinh', N'Chieng Sinh Ward', 'chieng-sinh', 7, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30319005', N'Mộc Châu', N'Moc Chau', N'Phường Mộc Châu', N'Moc Chau Ward', 'moc-chau', 7, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30319006', N'Mộc Sơn', N'Moc Son', N'Phường Mộc Sơn', N'Moc Son Ward', 'moc-son', 7, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30319007', N'Vân Sơn', N'Van Son', N'Phường Vân Sơn', N'Van Son Ward', 'van-son', 7, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30319008', N'Thảo Nguyên', N'Thao Nguyen', N'Phường Thảo Nguyên', N'Thao Nguyen Ward', 'thao-nguyen', 7, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30319009', N'Đoàn Kết', N'Doan Ket', N'Xã Đoàn Kết', N'Doan Ket Commune', 'doan-ket', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30319010', N'Lóng Sập', N'Long Sap', N'Xã Lóng Sập', N'Long Sap Commune', 'long-sap', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30319011', N'Chiềng Sơn', N'Chieng Son', N'Xã Chiềng Sơn', N'Chieng Son Commune', 'chieng-son', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30323012', N'Vân Hồ', N'Van Ho', N'Xã Vân Hồ', N'Van Ho Commune', 'van-ho', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30323013', N'Song Khủa', N'Song Khua', N'Xã Song Khủa', N'Song Khua Commune', 'song-khua', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30323014', N'Tô Múa', N'To Mua', N'Xã Tô Múa', N'To Mua Commune', 'to-mua', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30323015', N'Xuân Nha', N'Xuan Nha', N'Xã Xuân Nha', N'Xuan Nha Commune', 'xuan-nha', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30303016', N'Quỳnh Nhai', N'Quynh Nhai', N'Xã Quỳnh Nhai', N'Quynh Nhai Commune', 'quynh-nhai', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30303017', N'Mường Chiên', N'Muong Chien', N'Xã Mường Chiên', N'Muong Chien Commune', 'muong-chien', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30303018', N'Mường Giôn', N'Muong Gion', N'Xã Mường Giôn', N'Muong Gion Commune', 'muong-gion', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30303019', N'Mường Sại', N'Muong Sai', N'Xã Mường Sại', N'Muong Sai Commune', 'muong-sai', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30307020', N'Thuận Châu', N'Thuan Chau', N'Xã Thuận Châu', N'Thuan Chau Commune', 'thuan-chau', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30307021', N'Chiềng La', N'Chieng La', N'Xã Chiềng La', N'Chieng La Commune', 'chieng-la', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30307022', N'Nậm Lầu', N'Nam Lau', N'Xã Nậm Lầu', N'Nam Lau Commune', 'nam-lau', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30307023', N'Muổi Nọi', N'Muoi Noi', N'Xã Muổi Nọi', N'Muoi Noi Commune', 'muoi-noi', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30307024', N'Mường Khiêng', N'Muong Khieng', N'Xã Mường Khiêng', N'Muong Khieng Commune', 'muong-khieng', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30307025', N'Co Mạ', N'Co Ma', N'Xã Co Mạ', N'Co Ma Commune', 'co-ma', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30307026', N'Bình Thuận', N'Binh Thuan', N'Xã Bình Thuận', N'Binh Thuan Commune', 'binh-thuan', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30307027', N'Mường É', N'Muong E', N'Xã Mường É', N'Muong E Commune', 'muong-e', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30307028', N'Long Hẹ', N'Long He', N'Xã Long Hẹ', N'Long He Commune', 'long-he', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30305029', N'Mường La', N'Muong La', N'Xã Mường La', N'Muong La Commune', 'muong-la', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30305030', N'Chiềng Lao', N'Chieng Lao', N'Xã Chiềng Lao', N'Chieng Lao Commune', 'chieng-lao', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30305031', N'Mường Bú', N'Muong Bu', N'Xã Mường Bú', N'Muong Bu Commune', 'muong-bu', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30305032', N'Chiềng Hoa', N'Chieng Hoa', N'Xã Chiềng Hoa', N'Chieng Hoa Commune', 'chieng-hoa', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30309033', N'Bắc Yên', N'Bac Yen', N'Xã Bắc Yên', N'Bac Yen Commune', 'bac-yen', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30309034', N'Tà Xùa', N'Ta Xua', N'Xã Tà Xùa', N'Ta Xua Commune', 'ta-xua', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30309035', N'Tạ Khoa', N'Ta Khoa', N'Xã Tạ Khoa', N'Ta Khoa Commune', 'ta-khoa', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30309036', N'Xím Vàng', N'Xim Vang', N'Xã Xím Vàng', N'Xim Vang Commune', 'xim-vang', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30309037', N'Pắc Ngà', N'Pac Nga', N'Xã Pắc Ngà', N'Pac Nga Commune', 'pac-nga', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30309038', N'Chiềng Sại', N'Chieng Sai', N'Xã Chiềng Sại', N'Chieng Sai Commune', 'chieng-sai', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30311039', N'Phù Yên', N'Phu Yen', N'Xã Phù Yên', N'Phu Yen Commune', 'phu-yen', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30311040', N'Gia Phù', N'Gia Phu', N'Xã Gia Phù', N'Gia Phu Commune', 'gia-phu', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30311041', N'Tường Hạ', N'Tuong Ha', N'Xã Tường Hạ', N'Tuong Ha Commune', 'tuong-ha', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30311042', N'Mường Cơi', N'Muong Coi', N'Xã Mường Cơi', N'Muong Coi Commune', 'muong-coi', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30311043', N'Mường Bang', N'Muong Bang', N'Xã Mường Bang', N'Muong Bang Commune', 'muong-bang', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30311044', N'Tân Phong', N'Tan Phong', N'Xã Tân Phong', N'Tan Phong Commune', 'tan-phong', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30311045', N'Kim Bon', N'Kim Bon', N'Xã Kim Bon', N'Kim Bon Commune', 'kim-bon', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30317046', N'Yên Châu', N'Yen Chau', N'Xã Yên Châu', N'Yen Chau Commune', 'yen-chau', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30317047', N'Chiềng Hặc', N'Chieng Hac', N'Xã Chiềng Hặc', N'Chieng Hac Commune', 'chieng-hac', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30317048', N'Lóng Phiêng', N'Long Phieng', N'Xã Lóng Phiêng', N'Long Phieng Commune', 'long-phieng', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30317049', N'Yên Sơn', N'Yen Son', N'Xã Yên Sơn', N'Yen Son Commune', 'yen-son', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30313050', N'Chiềng Mai', N'Chieng Mai', N'Xã Chiềng Mai', N'Chieng Mai Commune', 'chieng-mai', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30313051', N'Mai Sơn', N'Mai Son', N'Xã Mai Sơn', N'Mai Son Commune', 'mai-son', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30313052', N'Phiêng Pằn', N'Phieng Pan', N'Xã Phiêng Pằn', N'Phieng Pan Commune', 'phieng-pan', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30313053', N'Chiềng Mung', N'Chieng Mung', N'Xã Chiềng Mung', N'Chieng Mung Commune', 'chieng-mung', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30313054', N'Phiêng Cằm', N'Phieng Cam', N'Xã Phiêng Cằm', N'Phieng Cam Commune', 'phieng-cam', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30313055', N'Mường Chanh', N'Muong Chanh', N'Xã Mường Chanh', N'Muong Chanh Commune', 'muong-chanh', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30313056', N'Tà Hộc', N'Ta Hoc', N'Xã Tà Hộc', N'Ta Hoc Commune', 'ta-hoc', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30313057', N'Chiềng Sung', N'Chieng Sung', N'Xã Chiềng Sung', N'Chieng Sung Commune', 'chieng-sung', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30315058', N'Bó Sinh', N'Bo Sinh', N'Xã Bó Sinh', N'Bo Sinh Commune', 'bo-sinh', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30315059', N'Chiềng Khương', N'Chieng Khuong', N'Xã Chiềng Khương', N'Chieng Khuong Commune', 'chieng-khuong', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30315060', N'Mường Hung', N'Muong Hung', N'Xã Mường Hung', N'Muong Hung Commune', 'muong-hung', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30315061', N'Chiềng Khoong', N'Chieng Khoong', N'Xã Chiềng Khoong', N'Chieng Khoong Commune', 'chieng-khoong', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30315062', N'Mường Lầm', N'Muong Lam', N'Xã Mường Lầm', N'Muong Lam Commune', 'muong-lam', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30315063', N'Nậm Ty', N'Nam Ty', N'Xã Nậm Ty', N'Nam Ty Commune', 'nam-ty', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30315064', N'Sông Mã', N'Song Ma', N'Xã Sông Mã', N'Song Ma Commune', 'song-ma', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30315065', N'Huổi Một', N'Huoi Mot', N'Xã Huổi Một', N'Huoi Mot Commune', 'huoi-mot', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30315066', N'Chiềng Sơ', N'Chieng So', N'Xã Chiềng Sơ', N'Chieng So Commune', 'chieng-so', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30321067', N'Sốp Cộp', N'Sop Cop', N'Xã Sốp Cộp', N'Sop Cop Commune', 'sop-cop', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30321068', N'Púng Bánh', N'Pung Banh', N'Xã Púng Bánh', N'Pung Banh Commune', 'pung-banh', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30319069', N'Tân Yên', N'Tan Yen', N'Xã Tân Yên', N'Tan Yen Commune', 'tan-yen', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30307070', N'Mường Bám', N'Muong Bam', N'Xã Mường Bám', N'Muong Bam Commune', 'muong-bam', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30305071', N'Ngọc Chiến', N'Ngoc Chien', N'Xã Ngọc Chiến', N'Ngoc Chien Commune', 'ngoc-chien', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30311072', N'Suối Tọ', N'Suoi To', N'Xã Suối Tọ', N'Suoi To Commune', 'suoi-to', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30317073', N'Phiêng Khoài', N'Phieng Khoai', N'Xã Phiêng Khoài', N'Phieng Khoai Commune', 'phieng-khoai', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30321074', N'Mường Lạn', N'Muong Lan', N'Xã Mường Lạn', N'Muong Lan Commune', 'muong-lan', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'30321075', N'Mường Lèo', N'Muong Leo', N'Xã Mường Lèo', N'Muong Leo Commune', 'muong-leo', 8, N'303', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40101001', N'Hạc Thành', N'Hac Thanh', N'Phường Hạc Thành', N'Hac Thanh Ward', 'hac-thanh', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40101002', N'Quảng Phú', N'Quang Phu', N'Phường Quảng Phú', N'Quang Phu Ward', 'quang-phu', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40101003', N'Đông Quang', N'Dong Quang', N'Phường Đông Quang', N'Dong Quang Ward', 'dong-quang', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40101004', N'Đông Sơn', N'Dong Son', N'Phường Đông Sơn', N'Dong Son Ward', 'dong-son', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40101005', N'Đông Tiến', N'Dong Tien', N'Phường Đông Tiến', N'Dong Tien Ward', 'dong-tien', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40101006', N'Hàm Rồng', N'Ham Rong', N'Phường Hàm Rồng', N'Ham Rong Ward', 'ham-rong', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40101007', N'Nguyệt Viên', N'Nguyet Vien', N'Phường Nguyệt Viên', N'Nguyet Vien Ward', 'nguyet-vien', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40105008', N'Sầm Sơn', N'Sam Son', N'Phường Sầm Sơn', N'Sam Son Ward', 'sam-son', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40105009', N'Nam Sầm Sơn', N'Nam Sam Son', N'Phường Nam Sầm Sơn', N'Nam Sam Son Ward', 'nam-sam-son', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40103010', N'Bỉm Sơn', N'Bim Son', N'Phường Bỉm Sơn', N'Bim Son Ward', 'bim-son', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40103011', N'Quang Trung', N'Quang Trung', N'Phường Quang Trung', N'Quang Trung Ward', 'quang-trung', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40153012', N'Ngọc Sơn', N'Ngoc Son', N'Phường Ngọc Sơn', N'Ngoc Son Ward', 'ngoc-son', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40153013', N'Tân Dân', N'Tan Dan', N'Phường Tân Dân', N'Tan Dan Ward', 'tan-dan', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40153014', N'Hải Lĩnh', N'Hai Linh', N'Phường Hải Lĩnh', N'Hai Linh Ward', 'hai-linh', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40153015', N'Tĩnh Gia', N'Tinh Gia', N'Phường Tĩnh Gia', N'Tinh Gia Ward', 'tinh-gia', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40153016', N'Đào Duy Tư', N'Dao Duy Tu', N'Phường Đào Duy Tư', N'Dao Duy Tu Ward', 'dao-duy-tu', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40153017', N'Hải Bình', N'Hai Binh', N'Phường Hải Bình', N'Hai Binh Ward', 'hai-binh', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40153018', N'Trúc Lâm', N'Truc Lam', N'Phường Trúc Lâm', N'Truc Lam Ward', 'truc-lam', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40153019', N'Nghi Sơn', N'Nghi Son', N'Phường Nghi Sơn', N'Nghi Son Ward', 'nghi-son', 7, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40153020', N'Các Sơn', N'Cac Son', N'Xã Các Sơn', N'Cac Son Commune', 'cac-son', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40153021', N'Trường Lâm', N'Truong Lam', N'Xã Trường Lâm', N'Truong Lam Commune', 'truong-lam', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40131022', N'Hà Trung', N'Ha Trung', N'Xã Hà Trung', N'Ha Trung Commune', 'ha-trung', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40131023', N'Tống Sơn', N'Tong Son', N'Xã Tống Sơn', N'Tong Son Commune', 'tong-son', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40131024', N'Hà Long', N'Ha Long', N'Xã Hà Long', N'Ha Long Commune', 'ha-long', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40131025', N'Hoạt Giang', N'Hoat Giang', N'Xã Hoạt Giang', N'Hoat Giang Commune', 'hoat-giang', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40131026', N'Lĩnh Toại', N'Linh Toai', N'Xã Lĩnh Toại', N'Linh Toai Commune', 'linh-toai', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40139027', N'Triệu Lộc', N'Trieu Loc', N'Xã Triệu Lộc', N'Trieu Loc Commune', 'trieu-loc', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40139028', N'Đông Thành', N'Dong Thanh', N'Xã Đông Thành', N'Dong Thanh Commune', 'dong-thanh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40139029', N'Hậu Lộc', N'Hau Loc', N'Xã Hậu Lộc', N'Hau Loc Commune', 'hau-loc', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40139030', N'Hoa Lộc', N'Hoa Loc', N'Xã Hoa Lộc', N'Hoa Loc Commune', 'hoa-loc', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40139031', N'Vạn Lộc', N'Van Loc', N'Xã Vạn Lộc', N'Van Loc Commune', 'van-loc', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40133032', N'Nga Sơn', N'Nga Son', N'Xã Nga Sơn', N'Nga Son Commune', 'nga-son', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40133033', N'Nga Thắng', N'Nga Thang', N'Xã Nga Thắng', N'Nga Thang Commune', 'nga-thang', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40133034', N'Hồ Vương', N'Ho Vuong', N'Xã Hồ Vương', N'Ho Vuong Commune', 'ho-vuong', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40133035', N'Tân Tiến', N'Tan Tien', N'Xã Tân Tiến', N'Tan Tien Commune', 'tan-tien', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40133036', N'Nga An', N'Nga An', N'Xã Nga An', N'Nga An Commune', 'nga-an', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40133037', N'Ba Đình', N'Ba Dinh', N'Xã Ba Đình', N'Ba Dinh Commune', 'ba-dinh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40143038', N'Hoằng Hóa', N'Hoang Hoa', N'Xã Hoằng Hóa', N'Hoang Hoa Commune', 'hoang-hoa', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40143039', N'Hoằng Tiến', N'Hoang Tien', N'Xã Hoằng Tiến', N'Hoang Tien Commune', 'hoang-tien', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40143040', N'Hoằng Thanh', N'Hoang Thanh', N'Xã Hoằng Thanh', N'Hoang Thanh Commune', 'hoang-thanh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40143041', N'Hoằng Lộc', N'Hoang Loc', N'Xã Hoằng Lộc', N'Hoang Loc Commune', 'hoang-loc', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40143042', N'Hoằng Châu', N'Hoang Chau', N'Xã Hoằng Châu', N'Hoang Chau Commune', 'hoang-chau', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40143043', N'Hoằng Sơn', N'Hoang Son', N'Xã Hoằng Sơn', N'Hoang Son Commune', 'hoang-son', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40143044', N'Hoằng Phú', N'Hoang Phu', N'Xã Hoằng Phú', N'Hoang Phu Commune', 'hoang-phu', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40143045', N'Hoằng Giang', N'Hoang Giang', N'Xã Hoằng Giang', N'Hoang Giang Commune', 'hoang-giang', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40149046', N'Lưu Vệ', N'Luu Ve', N'Xã Lưu Vệ', N'Luu Ve Commune', 'luu-ve', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40149047', N'Quảng Yên', N'Quang Yen', N'Xã Quảng Yên', N'Quang Yen Commune', 'quang-yen', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40149048', N'Quảng Ngọc', N'Quang Ngoc', N'Xã Quảng Ngọc', N'Quang Ngoc Commune', 'quang-ngoc', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40149049', N'Quảng Ninh', N'Quang Ninh', N'Xã Quảng Ninh', N'Quang Ninh Commune', 'quang-ninh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40149050', N'Quảng Bình', N'Quang Binh', N'Xã Quảng Bình', N'Quang Binh Commune', 'quang-binh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40149051', N'Tiên Trang', N'Tien Trang', N'Xã Tiên Trang', N'Tien Trang Commune', 'tien-trang', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40149052', N'Quảng Chính', N'Quang Chinh', N'Xã Quảng Chính', N'Quang Chinh Commune', 'quang-chinh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40151053', N'Nông Cống', N'Nong Cong', N'Xã Nông Cống', N'Nong Cong Commune', 'nong-cong', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40151054', N'Thắng Lợi', N'Thang Loi', N'Xã Thắng Lợi', N'Thang Loi Commune', 'thang-loi', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40151055', N'Trung Chính', N'Trung Chinh', N'Xã Trung Chính', N'Trung Chinh Commune', 'trung-chinh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40151056', N'Trường Văn', N'Truong Van', N'Xã Trường Văn', N'Truong Van Commune', 'truong-van', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40151057', N'Thăng Bình', N'Thang Binh', N'Xã Thăng Bình', N'Thang Binh Commune', 'thang-binh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40151058', N'Tượng Lĩnh', N'Tuong Linh', N'Xã Tượng Lĩnh', N'Tuong Linh Commune', 'tuong-linh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40151059', N'Công Chính', N'Cong Chinh', N'Xã Công Chính', N'Cong Chinh Commune', 'cong-chinh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40141060', N'Thiệu Hóa', N'Thieu Hoa', N'Xã Thiệu Hóa', N'Thieu Hoa Commune', 'thieu-hoa', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40141061', N'Thiệu Quang', N'Thieu Quang', N'Xã Thiệu Quang', N'Thieu Quang Commune', 'thieu-quang', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40141062', N'Thiệu Tiến', N'Thieu Tien', N'Xã Thiệu Tiến', N'Thieu Tien Commune', 'thieu-tien', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40141063', N'Thiệu Toán', N'Thieu Toan', N'Xã Thiệu Toán', N'Thieu Toan Commune', 'thieu-toan', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40141064', N'Thiệu Trung', N'Thieu Trung', N'Xã Thiệu Trung', N'Thieu Trung Commune', 'thieu-trung', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40135065', N'Yên Định', N'Yen Dinh', N'Xã Yên Định', N'Yen Dinh Commune', 'yen-dinh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40135066', N'Yên Trường', N'Yen Truong', N'Xã Yên Trường', N'Yen Truong Commune', 'yen-truong', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40135067', N'Yên Phú', N'Yen Phu', N'Xã Yên Phú', N'Yen Phu Commune', 'yen-phu', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40135068', N'Quý Lộc', N'Quy Loc', N'Xã Quý Lộc', N'Quy Loc Commune', 'quy-loc', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40135069', N'Yên Ninh', N'Yen Ninh', N'Xã Yên Ninh', N'Yen Ninh Commune', 'yen-ninh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40135070', N'Định Tân', N'Dinh Tan', N'Xã Định Tân', N'Dinh Tan Commune', 'dinh-tan', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40135071', N'Định Hoà', N'Dinh Hoa', N'Xã Định Hoà', N'Dinh Hoa Commune', 'dinh-hoa', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40137072', N'Thọ Xuân', N'Tho Xuan', N'Xã Thọ Xuân', N'Tho Xuan Commune', 'tho-xuan', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40137073', N'Thọ Long', N'Tho Long', N'Xã Thọ Long', N'Tho Long Commune', 'tho-long', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40137074', N'Xuân Hoà', N'Xuan Hoa', N'Xã Xuân Hoà', N'Xuan Hoa Commune', 'xuan-hoa', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40137075', N'Sao Vàng', N'Sao Vang', N'Xã Sao Vàng', N'Sao Vang Commune', 'sao-vang', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40137076', N'Lam Sơn', N'Lam Son', N'Xã Lam Sơn', N'Lam Son Commune', 'lam-son', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40137077', N'Thọ Lập', N'Tho Lap', N'Xã Thọ Lập', N'Tho Lap Commune', 'tho-lap', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40137078', N'Xuân Tín', N'Xuan Tin', N'Xã Xuân Tín', N'Xuan Tin Commune', 'xuan-tin', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40137079', N'Xuân Lập', N'Xuan Lap', N'Xã Xuân Lập', N'Xuan Lap Commune', 'xuan-lap', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40129080', N'Vĩnh Lộc', N'Vinh Loc', N'Xã Vĩnh Lộc', N'Vinh Loc Commune', 'vinh-loc', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40129081', N'Tây Đô', N'Tay Do', N'Xã Tây Đô', N'Tay Do Commune', 'tay-do', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40129082', N'Biện Thượng', N'Bien Thuong', N'Xã Biện Thượng', N'Bien Thuong Commune', 'bien-thuong', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40147083', N'Triệu Sơn', N'Trieu Son', N'Xã Triệu Sơn', N'Trieu Son Commune', 'trieu-son', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40147084', N'Thọ Bình', N'Tho Binh', N'Xã Thọ Bình', N'Tho Binh Commune', 'tho-binh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40147085', N'Thọ Ngọc', N'Tho Ngoc', N'Xã Thọ Ngọc', N'Tho Ngoc Commune', 'tho-ngoc', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40147086', N'Thọ Phú', N'Tho Phu', N'Xã Thọ Phú', N'Tho Phu Commune', 'tho-phu', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40147087', N'Hợp Tiến', N'Hop Tien', N'Xã Hợp Tiến', N'Hop Tien Commune', 'hop-tien', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40147088', N'An Nông', N'An Nong', N'Xã An Nông', N'An Nong Commune', 'an-nong', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40147089', N'Tân Ninh', N'Tan Ninh', N'Xã Tân Ninh', N'Tan Ninh Commune', 'tan-ninh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40147090', N'Đồng Tiến', N'Dong Tien', N'Xã Đồng Tiến', N'Dong Tien Commune', 'dong-tien', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40107091', N'Mường Chanh', N'Muong Chanh', N'Xã Mường Chanh', N'Muong Chanh Commune', 'muong-chanh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40107092', N'Quang Chiểu', N'Quang Chieu', N'Xã Quang Chiểu', N'Quang Chieu Commune', 'quang-chieu', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40107093', N'Tam chung', N'Tam chung', N'Xã Tam chung', N'Tam chung Commune', 'tam-chung', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40107094', N'Mường Lát', N'Muong Lat', N'Xã Mường Lát', N'Muong Lat Commune', 'muong-lat', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40107095', N'Pù Nhi', N'Pu Nhi', N'Xã Pù Nhi', N'Pu Nhi Commune', 'pu-nhi', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40107096', N'Nhi Sơn', N'Nhi Son', N'Xã Nhi Sơn', N'Nhi Son Commune', 'nhi-son', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40107097', N'Mường Lý', N'Muong Ly', N'Xã Mường Lý', N'Muong Ly Commune', 'muong-ly', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40107098', N'Trung Lý', N'Trung Ly', N'Xã Trung Lý', N'Trung Ly Commune', 'trung-ly', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40109099', N'Hồi Xuân', N'Hoi Xuan', N'Xã Hồi Xuân', N'Hoi Xuan Commune', 'hoi-xuan', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40109100', N'Nam Xuân', N'Nam Xuan', N'Xã Nam Xuân', N'Nam Xuan Commune', 'nam-xuan', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40109101', N'Thiên Phủ', N'Thien Phu', N'Xã Thiên Phủ', N'Thien Phu Commune', 'thien-phu', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40109102', N'Hiền Kiệt', N'Hien Kiet', N'Xã Hiền Kiệt', N'Hien Kiet Commune', 'hien-kiet', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40109103', N'Phú Xuân', N'Phu Xuan', N'Xã Phú Xuân', N'Phu Xuan Commune', 'phu-xuan', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40109104', N'Phú Lệ', N'Phu Le', N'Xã Phú Lệ', N'Phu Le Commune', 'phu-le', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40109105', N'Trung Thành', N'Trung Thanh', N'Xã Trung Thành', N'Trung Thanh Commune', 'trung-thanh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40109106', N'Trung Sơn', N'Trung Son', N'Xã Trung Sơn', N'Trung Son Commune', 'trung-son', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40111107', N'Na Mèo', N'Na Meo', N'Xã Na Mèo', N'Na Meo Commune', 'na-meo', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40111108', N'Sơn Thủy', N'Son Thuy', N'Xã Sơn Thủy', N'Son Thuy Commune', 'son-thuy', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40111109', N'Sơn Điện', N'Son Dien', N'Xã Sơn Điện', N'Son Dien Commune', 'son-dien', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40111110', N'Mường Mìn', N'Muong Min', N'Xã Mường Mìn', N'Muong Min Commune', 'muong-min', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40111111', N'Tam Thanh', N'Tam Thanh', N'Xã Tam Thanh', N'Tam Thanh Commune', 'tam-thanh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40111112', N'Tam Lư', N'Tam Lu', N'Xã Tam Lư', N'Tam Lu Commune', 'tam-lu', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40111113', N'Quan Sơn', N'Quan Son', N'Xã Quan Sơn', N'Quan Son Commune', 'quan-son', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40111114', N'Trung Hạ', N'Trung Ha', N'Xã Trung Hạ', N'Trung Ha Commune', 'trung-ha', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40117115', N'Linh Sơn', N'Linh Son', N'Xã Linh Sơn', N'Linh Son Commune', 'linh-son', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40117116', N'Đồng Lương', N'Dong Luong', N'Xã Đồng Lương', N'Dong Luong Commune', 'dong-luong', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40117117', N'Văn Phú', N'Van Phu', N'Xã Văn Phú', N'Van Phu Commune', 'van-phu', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40117118', N'Giao An', N'Giao An', N'Xã Giao An', N'Giao An Commune', 'giao-an', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40117119', N'Yên Khương', N'Yen Khuong', N'Xã Yên Khương', N'Yen Khuong Commune', 'yen-khuong', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40117120', N'Yên Thắng', N'Yen Thang', N'Xã Yên Thắng', N'Yen Thang Commune', 'yen-thang', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40113121', N'Văn Nho', N'Van Nho', N'Xã Văn Nho', N'Van Nho Commune', 'van-nho', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40113122', N'Thiết Ống', N'Thiet Ong', N'Xã Thiết Ống', N'Thiet Ong Commune', 'thiet-ong', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40113123', N'Bá Thước', N'Ba Thuoc', N'Xã Bá Thước', N'Ba Thuoc Commune', 'ba-thuoc', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40113124', N'Cổ Lũng', N'Co Lung', N'Xã Cổ Lũng', N'Co Lung Commune', 'co-lung', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40113125', N'Pù Luông', N'Pu Luong', N'Xã Pù Luông', N'Pu Luong Commune', 'pu-luong', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40113126', N'Điền Lư', N'Dien Lu', N'Xã Điền Lư', N'Dien Lu Commune', 'dien-lu', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40113127', N'Điền Quang', N'Dien Quang', N'Xã Điền Quang', N'Dien Quang Commune', 'dien-quang', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40113128', N'Quý Lương', N'Quy Luong', N'Xã Quý Lương', N'Quy Luong Commune', 'quy-luong', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40121129', N'Ngọc Lặc', N'Ngoc Lac', N'Xã Ngọc Lặc', N'Ngoc Lac Commune', 'ngoc-lac', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40121130', N'Thạch Lập', N'Thach Lap', N'Xã Thạch Lập', N'Thach Lap Commune', 'thach-lap', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40121131', N'Ngọc Liên', N'Ngoc Lien', N'Xã Ngọc Liên', N'Ngoc Lien Commune', 'ngoc-lien', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40121132', N'Minh Sơn', N'Minh Son', N'Xã Minh Sơn', N'Minh Son Commune', 'minh-son', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40121133', N'Nguyệt Ấn', N'Nguyet An', N'Xã Nguyệt Ấn', N'Nguyet An Commune', 'nguyet-an', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40121134', N'Kiên Thọ', N'Kien Tho', N'Xã Kiên Thọ', N'Kien Tho Commune', 'kien-tho', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40115135', N'Cẩm Thạch', N'Cam Thach', N'Xã Cẩm Thạch', N'Cam Thach Commune', 'cam-thach', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40115136', N'Cẩm Thủy', N'Cam Thuy', N'Xã Cẩm Thủy', N'Cam Thuy Commune', 'cam-thuy', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40115137', N'Cẩm Tú', N'Cam Tu', N'Xã Cẩm Tú', N'Cam Tu Commune', 'cam-tu', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40115138', N'Cẩm Vân', N'Cam Van', N'Xã Cẩm Vân', N'Cam Van Commune', 'cam-van', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40115139', N'Cẩm Tân', N'Cam Tan', N'Xã Cẩm Tân', N'Cam Tan Commune', 'cam-tan', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40119140', N'Kim Tân', N'Kim Tan', N'Xã Kim Tân', N'Kim Tan Commune', 'kim-tan', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40119141', N'Vân Du', N'Van Du', N'Xã Vân Du', N'Van Du Commune', 'van-du', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40119142', N'Ngọc Trạo', N'Ngoc Trao', N'Xã Ngọc Trạo', N'Ngoc Trao Commune', 'ngoc-trao', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40119143', N'Thạch Bình', N'Thach Binh', N'Xã Thạch Bình', N'Thach Binh Commune', 'thach-binh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40119144', N'Thành Vinh', N'Thanh Vinh', N'Xã Thành Vinh', N'Thanh Vinh Commune', 'thanh-vinh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40119145', N'Thạch Quảng', N'Thach Quang', N'Xã Thạch Quảng', N'Thach Quang Commune', 'thach-quang', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40125146', N'Như Xuân', N'Nhu Xuan', N'Xã Như Xuân', N'Nhu Xuan Commune', 'nhu-xuan', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40125147', N'Thượng Ninh', N'Thuong Ninh', N'Xã Thượng Ninh', N'Thuong Ninh Commune', 'thuong-ninh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40125148', N'Xuân Bình', N'Xuan Binh', N'Xã Xuân Bình', N'Xuan Binh Commune', 'xuan-binh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40125149', N'Hóa Quỳ', N'Hoa Quy', N'Xã Hóa Quỳ', N'Hoa Quy Commune', 'hoa-quy', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40125150', N'Thanh Quân', N'Thanh Quan', N'Xã Thanh Quân', N'Thanh Quan Commune', 'thanh-quan', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40125151', N'Thanh Phong', N'Thanh Phong', N'Xã Thanh Phong', N'Thanh Phong Commune', 'thanh-phong', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40127152', N'Xuân Du', N'Xuan Du', N'Xã Xuân Du', N'Xuan Du Commune', 'xuan-du', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40127153', N'Mậu Lâm', N'Mau Lam', N'Xã Mậu Lâm', N'Mau Lam Commune', 'mau-lam', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40127154', N'Như Thanh', N'Nhu Thanh', N'Xã Như Thanh', N'Nhu Thanh Commune', 'nhu-thanh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40127155', N'Yên Thọ', N'Yen Tho', N'Xã Yên Thọ', N'Yen Tho Commune', 'yen-tho', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40127156', N'Xuân Thái', N'Xuan Thai', N'Xã Xuân Thái', N'Xuan Thai Commune', 'xuan-thai', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40127157', N'Thanh Kỳ', N'Thanh Ky', N'Xã Thanh Kỳ', N'Thanh Ky Commune', 'thanh-ky', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40123158', N'Bát Mọt', N'Bat Mot', N'Xã Bát Mọt', N'Bat Mot Commune', 'bat-mot', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40123159', N'Yên Nhân', N'Yen Nhan', N'Xã Yên Nhân', N'Yen Nhan Commune', 'yen-nhan', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40123160', N'Lương Sơn', N'Luong Son', N'Xã Lương Sơn', N'Luong Son Commune', 'luong-son', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40123161', N'Thường Xuân', N'Thuong Xuan', N'Xã Thường Xuân', N'Thuong Xuan Commune', 'thuong-xuan', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40123162', N'Luận Thành', N'Luan Thanh', N'Xã Luận Thành', N'Luan Thanh Commune', 'luan-thanh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40123163', N'Tân Thành', N'Tan Thanh', N'Xã Tân Thành', N'Tan Thanh Commune', 'tan-thanh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40123164', N'Vạn Xuân', N'Van Xuan', N'Xã Vạn Xuân', N'Van Xuan Commune', 'van-xuan', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40123165', N'Thắng Lộc', N'Thang Loc', N'Xã Thắng Lộc', N'Thang Loc Commune', 'thang-loc', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40123166', N'Xuân Chinh', N'Xuan Chinh', N'Xã Xuân Chinh', N'Xuan Chinh Commune', 'xuan-chinh', 8, N'401', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40327001', N'Anh Sơn', N'Anh Son', N'Xã Anh Sơn', N'Anh Son Commune', 'anh-son', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40327002', N'Yên Xuân', N'Yen Xuan', N'Xã Yên Xuân', N'Yen Xuan Commune', 'yen-xuan', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40327003', N'Nhân Hoà', N'Nhan Hoa', N'Xã Nhân Hoà', N'Nhan Hoa Commune', 'nhan-hoa', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40327004', N'Anh Sơn Đông', N'Anh Son Dong', N'Xã Anh Sơn Đông', N'Anh Son Dong Commune', 'anh-son-dong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40327005', N'Vĩnh Tường', N'Vinh Tuong', N'Xã Vĩnh Tường', N'Vinh Tuong Commune', 'vinh-tuong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40327006', N'Thành Bình Thọ', N'Thanh Binh Tho', N'Xã Thành Bình Thọ', N'Thanh Binh Tho Commune', 'thanh-binh-tho', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40321007', N'Con Cuông', N'Con Cuong', N'Xã Con Cuông', N'Con Cuong Commune', 'con-cuong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40321008', N'Môn Sơn', N'Mon Son', N'Xã Môn Sơn', N'Mon Son Commune', 'mon-son', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40321009', N'Mậu Thạch', N'Mau Thach', N'Xã Mậu Thạch', N'Mau Thach Commune', 'mau-thach', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40321010', N'Cam Phục', N'Cam Phuc', N'Xã Cam Phục', N'Cam Phuc Commune', 'cam-phuc', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40321011', N'Châu Khê', N'Chau Khe', N'Xã Châu Khê', N'Chau Khe Commune', 'chau-khe', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40321012', N'Bình Chuẩn', N'Binh Chuan', N'Xã Bình Chuẩn', N'Binh Chuan Commune', 'binh-chuan', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40325013', N'Diễn Châu', N'Dien Chau', N'Xã Diễn Châu', N'Dien Chau Commune', 'dien-chau', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40325014', N'Đức Châu', N'Duc Chau', N'Xã Đức Châu', N'Duc Chau Commune', 'duc-chau', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40325015', N'Quảng Châu', N'Quang Chau', N'Xã Quảng Châu', N'Quang Chau Commune', 'quang-chau', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40325016', N'Hải Châu', N'Hai Chau', N'Xã Hải Châu', N'Hai Chau Commune', 'hai-chau', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40325017', N'Tân Châu', N'Tan Chau', N'Xã Tân Châu', N'Tan Chau Commune', 'tan-chau', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40325018', N'An Châu', N'An Chau', N'Xã An Châu', N'An Chau Commune', 'an-chau', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40325019', N'Minh Châu', N'Minh Chau', N'Xã Minh Châu', N'Minh Chau Commune', 'minh-chau', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40325020', N'Hùng Châu', N'Hung Chau', N'Xã Hùng Châu', N'Hung Chau Commune', 'hung-chau', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40329021', N'Đô Lương', N'Do Luong', N'Xã Đô Lương', N'Do Luong Commune', 'do-luong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40329022', N'Bạch Ngọc', N'Bach Ngoc', N'Xã Bạch Ngọc', N'Bach Ngoc Commune', 'bach-ngoc', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40329023', N'Văn Hiến', N'Van Hien', N'Xã Văn Hiến', N'Van Hien Commune', 'van-hien', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40329024', N'Bạch Hà', N'Bach Ha', N'Xã Bạch Hà', N'Bach Ha Commune', 'bach-ha', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40329025', N'Thuần Trung', N'Thuan Trung', N'Xã Thuần Trung', N'Thuan Trung Commune', 'thuan-trung', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40329026', N'Lương Sơn', N'Luong Son', N'Xã Lương Sơn', N'Luong Son Commune', 'luong-son', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40339027', N'Hoàng Mai', N'Hoang Mai', N'Phường Hoàng Mai', N'Hoang Mai Ward', 'hoang-mai', 7, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40339028', N'Tân Mai', N'Tan Mai', N'Phường Tân Mai', N'Tan Mai Ward', 'tan-mai', 7, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40339029', N'Quỳnh Mai', N'Quynh Mai', N'Phường Quỳnh Mai', N'Quynh Mai Ward', 'quynh-mai', 7, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40337030', N'Hưng Nguyên', N'Hung Nguyen', N'Xã Hưng Nguyên', N'Hung Nguyen Commune', 'hung-nguyen', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40337031', N'Yên Trung', N'Yen Trung', N'Xã Yên Trung', N'Yen Trung Commune', 'yen-trung', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40337032', N'Hưng Nguyên Nam', N'Hung Nguyen Nam', N'Xã Hưng Nguyên Nam', N'Hung Nguyen Nam Commune', 'hung-nguyen-nam', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40337033', N'Lam Thành', N'Lam Thanh', N'Xã Lam Thành', N'Lam Thanh Commune', 'lam-thanh', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40309034', N'Mường Xén', N'Muong Xen', N'Xã Mường Xén', N'Muong Xen Commune', 'muong-xen', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40309035', N'Hữu Kiệm', N'Huu Kiem', N'Xã Hữu Kiệm', N'Huu Kiem Commune', 'huu-kiem', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40309036', N'Nậm Cắn', N'Nam Can', N'Xã Nậm Cắn', N'Nam Can Commune', 'nam-can', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40309037', N'Chiêu Lưu', N'Chieu Luu', N'Xã Chiêu Lưu', N'Chieu Luu Commune', 'chieu-luu', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40309038', N'Na Loi', N'Na Loi', N'Xã Na Loi', N'Na Loi Commune', 'na-loi', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40309039', N'Mường Típ', N'Muong Tip', N'Xã Mường Típ', N'Muong Tip Commune', 'muong-tip', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40309040', N'Na Ngoi', N'Na Ngoi', N'Xã Na Ngoi', N'Na Ngoi Commune', 'na-ngoi', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40309041', N'Mỹ Lý', N'My Ly', N'Xã Mỹ Lý', N'My Ly Commune', 'my-ly', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40309042', N'Bắc Lý', N'Bac Ly', N'Xã Bắc Lý', N'Bac Ly Commune', 'bac-ly', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40309043', N'Keng Đu', N'Keng Du', N'Xã Keng Đu', N'Keng Du Commune', 'keng-du', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40309044', N'Huồi Tụ', N'Huoi Tu', N'Xã Huồi Tụ', N'Huoi Tu Commune', 'huoi-tu', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40309045', N'Mường Lống', N'Muong Long', N'Xã Mường Lống', N'Muong Long Commune', 'muong-long', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40335046', N'Vạn An', N'Van An', N'Xã Vạn An', N'Van An Commune', 'van-an', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40335047', N'Nam Đàn', N'Nam Dan', N'Xã Nam Đàn', N'Nam Dan Commune', 'nam-dan', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40335048', N'Đại Huệ', N'Dai Hue', N'Xã Đại Huệ', N'Dai Hue Commune', 'dai-hue', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40335049', N'Thiên Nhẫn', N'Thien Nhan', N'Xã Thiên Nhẫn', N'Thien Nhan Commune', 'thien-nhan', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40335050', N'Kim Liên', N'Kim Lien', N'Xã Kim Liên', N'Kim Lien Commune', 'kim-lien', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40313051', N'Nghĩa Đàn', N'Nghia Dan', N'Xã Nghĩa Đàn', N'Nghia Dan Commune', 'nghia-dan', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40313052', N'Nghĩa Thọ', N'Nghia Tho', N'Xã Nghĩa Thọ', N'Nghia Tho Commune', 'nghia-tho', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40313053', N'Nghĩa Lâm', N'Nghia Lam', N'Xã Nghĩa Lâm', N'Nghia Lam Commune', 'nghia-lam', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40313054', N'Nghĩa Mai', N'Nghia Mai', N'Xã Nghĩa Mai', N'Nghia Mai Commune', 'nghia-mai', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40313055', N'Nghĩa Hưng', N'Nghia Hung', N'Xã Nghĩa Hưng', N'Nghia Hung Commune', 'nghia-hung', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40313056', N'Nghĩa Khánh', N'Nghia Khanh', N'Xã Nghĩa Khánh', N'Nghia Khanh Commune', 'nghia-khanh', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40313057', N'Nghĩa Lộc', N'Nghia Loc', N'Xã Nghĩa Lộc', N'Nghia Loc Commune', 'nghia-loc', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40333058', N'Nghi Lộc', N'Nghi Loc', N'Xã Nghi Lộc', N'Nghi Loc Commune', 'nghi-loc', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40333059', N'Phúc Lộc', N'Phuc Loc', N'Xã Phúc Lộc', N'Phuc Loc Commune', 'phuc-loc', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40333060', N'Đông Lộc', N'Dong Loc', N'Xã Đông Lộc', N'Dong Loc Commune', 'dong-loc', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40333061', N'Trung Lộc', N'Trung Loc', N'Xã Trung Lộc', N'Trung Loc Commune', 'trung-loc', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40333062', N'Thần Lĩnh', N'Than Linh', N'Xã Thần Lĩnh', N'Than Linh Commune', 'than-linh', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40333063', N'Hải Lộc', N'Hai Loc', N'Xã Hải Lộc', N'Hai Loc Commune', 'hai-loc', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40333064', N'Văn Kiều', N'Van Kieu', N'Xã Văn Kiều', N'Van Kieu Commune', 'van-kieu', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40305065', N'Quế Phong', N'Que Phong', N'Xã Quế Phong', N'Que Phong Commune', 'que-phong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40305066', N'Tiền Phong', N'Tien Phong', N'Xã Tiền Phong', N'Tien Phong Commune', 'tien-phong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40305067', N'Tri Lễ', N'Tri Le', N'Xã Tri Lễ', N'Tri Le Commune', 'tri-le', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40305068', N'Mường Quàng', N'Muong Quang', N'Xã Mường Quàng', N'Muong Quang Commune', 'muong-quang', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40305069', N'Thông Thụ', N'Thong Thu', N'Xã Thông Thụ', N'Thong Thu Commune', 'thong-thu', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40307070', N'Quỳ Châu', N'Quy Chau', N'Xã Quỳ Châu', N'Quy Chau Commune', 'quy-chau', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40307071', N'Châu Tiến', N'Chau Tien', N'Xã Châu Tiến', N'Chau Tien Commune', 'chau-tien', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40307072', N'Hùng Chân', N'Hung Chan', N'Xã Hùng Chân', N'Hung Chan Commune', 'hung-chan', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40307073', N'Châu Bình', N'Chau Binh', N'Xã Châu Bình', N'Chau Binh Commune', 'chau-binh', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40311074', N'Quỳ Hợp', N'Quy Hop', N'Xã Quỳ Hợp', N'Quy Hop Commune', 'quy-hop', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40311075', N'Tam Hợp', N'Tam Hop', N'Xã Tam Hợp', N'Tam Hop Commune', 'tam-hop', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40311076', N'Châu Lộc', N'Chau Loc', N'Xã Châu Lộc', N'Chau Loc Commune', 'chau-loc', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40311077', N'Châu Hồng', N'Chau Hong', N'Xã Châu Hồng', N'Chau Hong Commune', 'chau-hong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40311078', N'Mường Ham', N'Muong Ham', N'Xã Mường Ham', N'Muong Ham Commune', 'muong-ham', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40311079', N'Mường Chọng', N'Muong Chong', N'Xã Mường Chọng', N'Muong Chong Commune', 'muong-chong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40311080', N'Minh Hợp', N'Minh Hop', N'Xã Minh Hợp', N'Minh Hop Commune', 'minh-hop', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40317081', N'Quỳnh Lưu', N'Quynh Luu', N'Xã Quỳnh Lưu', N'Quynh Luu Commune', 'quynh-luu', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40317082', N'Quỳnh Văn', N'Quynh Van', N'Xã Quỳnh Văn', N'Quynh Van Commune', 'quynh-van', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40317083', N'Quỳnh Anh', N'Quynh Anh', N'Xã Quỳnh Anh', N'Quynh Anh Commune', 'quynh-anh', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40317084', N'Quỳnh Tam', N'Quynh Tam', N'Xã Quỳnh Tam', N'Quynh Tam Commune', 'quynh-tam', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40317085', N'Quỳnh Phú', N'Quynh Phu', N'Xã Quỳnh Phú', N'Quynh Phu Commune', 'quynh-phu', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40317086', N'Quỳnh Sơn', N'Quynh Son', N'Xã Quỳnh Sơn', N'Quynh Son Commune', 'quynh-son', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40317087', N'Quỳnh Thắng', N'Quynh Thang', N'Xã Quỳnh Thắng', N'Quynh Thang Commune', 'quynh-thang', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40319088', N'Tân Kỳ', N'Tan Ky', N'Xã Tân Kỳ', N'Tan Ky Commune', 'tan-ky', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40319089', N'Tân Phú', N'Tan Phu', N'Xã Tân Phú', N'Tan Phu Commune', 'tan-phu', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40319090', N'Tân An', N'Tan An', N'Xã Tân An', N'Tan An Commune', 'tan-an', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40319091', N'Nghĩa Đồng', N'Nghia Dong', N'Xã Nghĩa Đồng', N'Nghia Dong Commune', 'nghia-dong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40319092', N'Giai Xuân', N'Giai Xuan', N'Xã Giai Xuân', N'Giai Xuan Commune', 'giai-xuan', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40319093', N'Nghĩa Hành', N'Nghia Hanh', N'Xã Nghĩa Hành', N'Nghia Hanh Commune', 'nghia-hanh', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40319094', N'Tiên Đồng', N'Tien Dong', N'Xã Tiên Đồng', N'Tien Dong Commune', 'tien-dong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40314095', N'Thái Hoà', N'Thai Hoa', N'Phường Thái Hoà', N'Thai Hoa Ward', 'thai-hoa', 7, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40314096', N'Tây Hiếu', N'Tay Hieu', N'Phường Tây Hiếu', N'Tay Hieu Ward', 'tay-hieu', 7, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40314097', N'Đông Hiếu', N'Dong Hieu', N'Xã Đông Hiếu', N'Dong Hieu Commune', 'dong-hieu', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40331098', N'Cát Ngạn', N'Cat Ngan', N'Xã Cát Ngạn', N'Cat Ngan Commune', 'cat-ngan', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40331099', N'Tam Đồng', N'Tam Dong', N'Xã Tam Đồng', N'Tam Dong Commune', 'tam-dong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40331100', N'Hạnh Lâm', N'Hanh Lam', N'Xã Hạnh Lâm', N'Hanh Lam Commune', 'hanh-lam', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40331101', N'Sơn Lâm', N'Son Lam', N'Xã Sơn Lâm', N'Son Lam Commune', 'son-lam', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40331102', N'Hoa Quân', N'Hoa Quan', N'Xã Hoa Quân', N'Hoa Quan Commune', 'hoa-quan', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40331103', N'Kim Bảng', N'Kim Bang', N'Xã Kim Bảng', N'Kim Bang Commune', 'kim-bang', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40331104', N'Bích Hào', N'Bich Hao', N'Xã Bích Hào', N'Bich Hao Commune', 'bich-hao', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40331105', N'Đại Đồng', N'Dai Dong', N'Xã Đại Đồng', N'Dai Dong Commune', 'dai-dong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40331106', N'Xuân Lâm', N'Xuan Lam', N'Xã Xuân Lâm', N'Xuan Lam Commune', 'xuan-lam', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40315107', N'Tam Quang', N'Tam Quang', N'Xã Tam Quang', N'Tam Quang Commune', 'tam-quang', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40315108', N'Tam Thái', N'Tam Thai', N'Xã Tam Thái', N'Tam Thai Commune', 'tam-thai', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40315109', N'Tương Dương', N'Tuong Duong', N'Xã Tương Dương', N'Tuong Duong Commune', 'tuong-duong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40315110', N'Lượng Minh', N'Luong Minh', N'Xã Lượng Minh', N'Luong Minh Commune', 'luong-minh', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40315111', N'Yên Na', N'Yen Na', N'Xã Yên Na', N'Yen Na Commune', 'yen-na', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40315112', N'Yên Hoà', N'Yen Hoa', N'Xã Yên Hoà', N'Yen Hoa Commune', 'yen-hoa', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40315113', N'Nga My', N'Nga My', N'Xã Nga My', N'Nga My Commune', 'nga-my', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40315114', N'Hữu Khuông', N'Huu Khuong', N'Xã Hữu Khuông', N'Huu Khuong Commune', 'huu-khuong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40315115', N'Nhôn Mai', N'Nhon Mai', N'Xã Nhôn Mai', N'Nhon Mai Commune', 'nhon-mai', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40301116', N'Trường Vinh', N'Truong Vinh', N'Phường Trường Vinh', N'Truong Vinh Ward', 'truong-vinh', 7, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40301117', N'Thành Vinh', N'Thanh Vinh', N'Phường Thành Vinh', N'Thanh Vinh Ward', 'thanh-vinh', 7, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40301118', N'Vinh Hưng', N'Vinh Hung', N'Phường Vinh Hưng', N'Vinh Hung Ward', 'vinh-hung', 7, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40301119', N'Vinh Phú', N'Vinh Phu', N'Phường Vinh Phú', N'Vinh Phu Ward', 'vinh-phu', 7, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40301120', N'Vinh Lộc', N'Vinh Loc', N'Phường Vinh Lộc', N'Vinh Loc Ward', 'vinh-loc', 7, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40301121', N'Cửa Lò', N'Cua Lo', N'Phường Cửa Lò', N'Cua Lo Ward', 'cua-lo', 7, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40323122', N'Yên Thành', N'Yen Thanh', N'Xã Yên Thành', N'Yen Thanh Commune', 'yen-thanh', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40323123', N'Quan Thành', N'Quan Thanh', N'Xã Quan Thành', N'Quan Thanh Commune', 'quan-thanh', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40323124', N'Hợp Minh', N'Hop Minh', N'Xã Hợp Minh', N'Hop Minh Commune', 'hop-minh', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40323125', N'Vân Tụ', N'Van Tu', N'Xã Vân Tụ', N'Van Tu Commune', 'van-tu', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40323126', N'Vân Du', N'Van Du', N'Xã Vân Du', N'Van Du Commune', 'van-du', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40323127', N'Quang Đồng', N'Quang Dong', N'Xã Quang Đồng', N'Quang Dong Commune', 'quang-dong', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40323128', N'Giai Lạc', N'Giai Lac', N'Xã Giai Lạc', N'Giai Lac Commune', 'giai-lac', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40323129', N'Bình Minh', N'Binh Minh', N'Xã Bình Minh', N'Binh Minh Commune', 'binh-minh', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40323130', N'Đông Thành', N'Dong Thanh', N'Xã Đông Thành', N'Dong Thanh Commune', 'dong-thanh', 8, N'403', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40520001', N'Sông Trí', N'Song Tri', N'Phường Sông Trí', N'Song Tri Ward', 'song-tri', 7, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40520002', N'Hải Ninh', N'Hai Ninh', N'Phường Hải Ninh', N'Hai Ninh Ward', 'hai-ninh', 7, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40520003', N'Hoành Sơn', N'Hoanh Son', N'Phường Hoành Sơn', N'Hoanh Son Ward', 'hoanh-son', 7, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40520004', N'Vũng Áng', N'Vung Ang', N'Phường Vũng Áng', N'Vung Ang Ward', 'vung-ang', 7, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40519005', N'Kỳ Xuân', N'Ky Xuan', N'Xã Kỳ Xuân', N'Ky Xuan Commune', 'ky-xuan', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40519006', N'Kỳ Anh', N'Ky Anh', N'Xã Kỳ Anh', N'Ky Anh Commune', 'ky-anh', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40519007', N'Kỳ Hoa', N'Ky Hoa', N'Xã Kỳ Hoa', N'Ky Hoa Commune', 'ky-hoa', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40519008', N'Kỳ Văn', N'Ky Van', N'Xã Kỳ Văn', N'Ky Van Commune', 'ky-van', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40519009', N'Kỳ Khang', N'Ky Khang', N'Xã Kỳ Khang', N'Ky Khang Commune', 'ky-khang', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40519010', N'Kỳ Lạc', N'Ky Lac', N'Xã Kỳ Lạc', N'Ky Lac Commune', 'ky-lac', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40519011', N'Kỳ Thượng', N'Ky Thuong', N'Xã Kỳ Thượng', N'Ky Thuong Commune', 'ky-thuong', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40515012', N'Cẩm Xuyên', N'Cam Xuyen', N'Xã Cẩm Xuyên', N'Cam Xuyen Commune', 'cam-xuyen', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40515013', N'Thiên Cầm', N'Thien Cam', N'Xã Thiên Cầm', N'Thien Cam Commune', 'thien-cam', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40515014', N'Cẩm Duệ', N'Cam Due', N'Xã Cẩm Duệ', N'Cam Due Commune', 'cam-due', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40515015', N'Cẩm Hưng', N'Cam Hung', N'Xã Cẩm Hưng', N'Cam Hung Commune', 'cam-hung', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40515016', N'Cẩm Lạc', N'Cam Lac', N'Xã Cẩm Lạc', N'Cam Lac Commune', 'cam-lac', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40515017', N'Cẩm Trung', N'Cam Trung', N'Xã Cẩm Trung', N'Cam Trung Commune', 'cam-trung', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40515018', N'Yên Hoà', N'Yen Hoa', N'Xã Yên Hoà', N'Yen Hoa Commune', 'yen-hoa', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40501019', N'Thành Sen', N'Thanh Sen', N'Phường Thành Sen', N'Thanh Sen Ward', 'thanh-sen', 7, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40501020', N'Trần Phú', N'Tran Phu', N'Phường Trần Phú', N'Tran Phu Ward', 'tran-phu', 7, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40501021', N'Hà Huy Tập', N'Ha Huy Tap', N'Phường Hà Huy Tập', N'Ha Huy Tap Ward', 'ha-huy-tap', 7, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40501022', N'Thạch Lạc', N'Thach Lac', N'Xã Thạch Lạc', N'Thach Lac Commune', 'thach-lac', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40501023', N'Đồng Tiến', N'Dong Tien', N'Xã Đồng Tiến', N'Dong Tien Commune', 'dong-tien', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40501024', N'Thạch Khê', N'Thach Khe', N'Xã Thạch Khê', N'Thach Khe Commune', 'thach-khe', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40501025', N'Cẩm Bình', N'Cam Binh', N'Xã Cẩm Bình', N'Cam Binh Commune', 'cam-binh', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40513026', N'Thạch Hà', N'Thach Ha', N'Xã Thạch Hà', N'Thach Ha Commune', 'thach-ha', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40513027', N'Toàn Lưu', N'Toan Luu', N'Xã Toàn Lưu', N'Toan Luu Commune', 'toan-luu', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40513028', N'Việt Xuyên', N'Viet Xuyen', N'Xã Việt Xuyên', N'Viet Xuyen Commune', 'viet-xuyen', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40513029', N'Đông Kinh', N'Dong Kinh', N'Xã Đông Kinh', N'Dong Kinh Commune', 'dong-kinh', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40513030', N'Thạch Xuân', N'Thach Xuan', N'Xã Thạch Xuân', N'Thach Xuan Commune', 'thach-xuan', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40513031', N'Lộc Hà', N'Loc Ha', N'Xã Lộc Hà', N'Loc Ha Commune', 'loc-ha', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40513032', N'Hồng Lộc', N'Hong Loc', N'Xã Hồng Lộc', N'Hong Loc Commune', 'hong-loc', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40513033', N'Mai Phụ', N'Mai Phu', N'Xã Mai Phụ', N'Mai Phu Commune', 'mai-phu', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40511034', N'Can Lộc', N'Can Loc', N'Xã Can Lộc', N'Can Loc Commune', 'can-loc', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40511035', N'Tùng Lộc', N'Tung Loc', N'Xã Tùng Lộc', N'Tung Loc Commune', 'tung-loc', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40511036', N'Gia Hanh', N'Gia Hanh', N'Xã Gia Hanh', N'Gia Hanh Commune', 'gia-hanh', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40511037', N'Trường Lưu', N'Truong Luu', N'Xã Trường Lưu', N'Truong Luu Commune', 'truong-luu', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40511038', N'Xuân Lộc', N'Xuan Loc', N'Xã Xuân Lộc', N'Xuan Loc Commune', 'xuan-loc', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40511039', N'Đồng Lộc', N'Dong Loc', N'Xã Đồng Lộc', N'Dong Loc Commune', 'dong-loc', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40503040', N'Bắc Hồng Lĩnh', N'Bac Hong Linh', N'Phường Bắc Hồng Lĩnh', N'Bac Hong Linh Ward', 'bac-hong-linh', 7, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40503041', N'Nam Hồng Lĩnh', N'Nam Hong Linh', N'Phường Nam Hồng Lĩnh', N'Nam Hong Linh Ward', 'nam-hong-linh', 7, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40505042', N'Tiên Điền', N'Tien Dien', N'Xã Tiên Điền', N'Tien Dien Commune', 'tien-dien', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40505043', N'Nghi Xuân', N'Nghi Xuan', N'Xã Nghi Xuân', N'Nghi Xuan Commune', 'nghi-xuan', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40505044', N'Cổ Đạm', N'Co Dam', N'Xã Cổ Đạm', N'Co Dam Commune', 'co-dam', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40505045', N'Đan Hải', N'Dan Hai', N'Xã Đan Hải', N'Dan Hai Commune', 'dan-hai', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40507046', N'Đức Thọ', N'Duc Tho', N'Xã Đức Thọ', N'Duc Tho Commune', 'duc-tho', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40507047', N'Đức Quang', N'Duc Quang', N'Xã Đức Quang', N'Duc Quang Commune', 'duc-quang', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40507048', N'Đức Đồng', N'Duc Dong', N'Xã Đức Đồng', N'Duc Dong Commune', 'duc-dong', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40507049', N'Đức Thịnh', N'Duc Thinh', N'Xã Đức Thịnh', N'Duc Thinh Commune', 'duc-thinh', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40507050', N'Đức Minh', N'Duc Minh', N'Xã Đức Minh', N'Duc Minh Commune', 'duc-minh', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40509051', N'Hương Sơn', N'Huong Son', N'Xã Hương Sơn', N'Huong Son Commune', 'huong-son', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40509052', N'Sơn Tây', N'Son Tay', N'Xã Sơn Tây', N'Son Tay Commune', 'son-tay', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40509053', N'Tứ Mỹ', N'Tu My', N'Xã Tứ Mỹ', N'Tu My Commune', 'tu-my', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40509054', N'Sơn Giang', N'Son Giang', N'Xã Sơn Giang', N'Son Giang Commune', 'son-giang', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40509055', N'Sơn Tiến', N'Son Tien', N'Xã Sơn Tiến', N'Son Tien Commune', 'son-tien', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40509056', N'Sơn Hồng', N'Son Hong', N'Xã Sơn Hồng', N'Son Hong Commune', 'son-hong', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40509057', N'Kim Hoa', N'Kim Hoa', N'Xã Kim Hoa', N'Kim Hoa Commune', 'kim-hoa', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40521058', N'Vũ Quang', N'Vu Quang', N'Xã Vũ Quang', N'Vu Quang Commune', 'vu-quang', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40521059', N'Mai Hoa', N'Mai Hoa', N'Xã Mai Hoa', N'Mai Hoa Commune', 'mai-hoa', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40521060', N'Thượng Đức', N'Thuong Duc', N'Xã Thượng Đức', N'Thuong Duc Commune', 'thuong-duc', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40517061', N'Hương Khê', N'Huong Khe', N'Xã Hương Khê', N'Huong Khe Commune', 'huong-khe', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40517062', N'Hương Phố', N'Huong Pho', N'Xã Hương Phố', N'Huong Pho Commune', 'huong-pho', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40517063', N'Hương Đô', N'Huong Do', N'Xã Hương Đô', N'Huong Do Commune', 'huong-do', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40517064', N'Hà Linh', N'Ha Linh', N'Xã Hà Linh', N'Ha Linh Commune', 'ha-linh', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40517065', N'Hương Bình', N'Huong Binh', N'Xã Hương Bình', N'Huong Binh Commune', 'huong-binh', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40517066', N'Phúc Trạch', N'Phuc Trach', N'Xã Phúc Trạch', N'Phuc Trach Commune', 'phuc-trach', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40517067', N'Hương Xuân', N'Huong Xuan', N'Xã Hương Xuân', N'Huong Xuan Commune', 'huong-xuan', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40509068', N'Sơn Kim 1', N'Son Kim 1', N'Xã Sơn Kim 1', N'Son Kim 1 Commune', 'son-kim-1', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40509069', N'Sơn Kim 2', N'Son Kim 2', N'Xã Sơn Kim 2', N'Son Kim 2 Commune', 'son-kim-2', 8, N'405', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40701001', N'Đồng Hới', N'Dong Hoi', N'Phường Đồng Hới', N'Dong Hoi Ward', 'dong-hoi', 7, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40701002', N'Đồng Thuận', N'Dong Thuan', N'Phường Đồng Thuận', N'Dong Thuan Ward', 'dong-thuan', 7, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40701003', N'Đồng Sơn', N'Dong Son', N'Phường Đồng Sơn', N'Dong Son Ward', 'dong-son', 7, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40715004', N'Nam Gianh', N'Nam Gianh', N'Xã Nam Gianh', N'Nam Gianh Commune', 'nam-gianh', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40715005', N'Nam Ba Đồn', N'Nam Ba Don', N'Xã Nam Ba Đồn', N'Nam Ba Don Commune', 'nam-ba-don', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40715006', N'Ba Đồn', N'Ba Don', N'Phường Ba Đồn', N'Ba Don Ward', 'ba-don', 7, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40715007', N'Bắc Gianh', N'Bac Gianh', N'Phường Bắc Gianh', N'Bac Gianh Ward', 'bac-gianh', 7, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40705008', N'Dân Hóa', N'Dan Hoa', N'Xã Dân Hóa', N'Dan Hoa Commune', 'dan-hoa', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40705009', N'Kim Điền', N'Kim Dien', N'Xã Kim Điền', N'Kim Dien Commune', 'kim-dien', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40705010', N'Kim Phú', N'Kim Phu', N'Xã Kim Phú', N'Kim Phu Commune', 'kim-phu', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40705011', N'Minh Hóa', N'Minh Hoa', N'Xã Minh Hóa', N'Minh Hoa Commune', 'minh-hoa', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40705012', N'Tân Thành', N'Tan Thanh', N'Xã Tân Thành', N'Tan Thanh Commune', 'tan-thanh', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40703013', N'Tuyên Lâm', N'Tuyen Lam', N'Xã Tuyên Lâm', N'Tuyen Lam Commune', 'tuyen-lam', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40703014', N'Tuyên Sơn', N'Tuyen Son', N'Xã Tuyên Sơn', N'Tuyen Son Commune', 'tuyen-son', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40703015', N'Đồng Lê', N'Dong Le', N'Xã Đồng Lê', N'Dong Le Commune', 'dong-le', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40703016', N'Tuyên Phú', N'Tuyen Phu', N'Xã Tuyên Phú', N'Tuyen Phu Commune', 'tuyen-phu', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40703017', N'Tuyên Bình', N'Tuyen Binh', N'Xã Tuyên Bình', N'Tuyen Binh Commune', 'tuyen-binh', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40703018', N'Tuyên Hóa', N'Tuyen Hoa', N'Xã Tuyên Hóa', N'Tuyen Hoa Commune', 'tuyen-hoa', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40707019', N'Tân Gianh', N'Tan Gianh', N'Xã Tân Gianh', N'Tan Gianh Commune', 'tan-gianh', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40707020', N'Trung Thuần', N'Trung Thuan', N'Xã Trung Thuần', N'Trung Thuan Commune', 'trung-thuan', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40707021', N'Quảng Trạch', N'Quang Trach', N'Xã Quảng Trạch', N'Quang Trach Commune', 'quang-trach', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40707022', N'Hoà Trạch', N'Hoa Trach', N'Xã Hoà Trạch', N'Hoa Trach Commune', 'hoa-trach', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40707023', N'Phú Trạch', N'Phu Trach', N'Xã Phú Trạch', N'Phu Trach Commune', 'phu-trach', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40709024', N'Thượng Trạch', N'Thuong Trach', N'Xã Thượng Trạch', N'Thuong Trach Commune', 'thuong-trach', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40709025', N'Phong Nha', N'Phong Nha', N'Xã Phong Nha', N'Phong Nha Commune', 'phong-nha', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40709026', N'Bắc Trạch', N'Bac Trach', N'Xã Bắc Trạch', N'Bac Trach Commune', 'bac-trach', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40709027', N'Đông Trạch', N'Dong Trach', N'Xã Đông Trạch', N'Dong Trach Commune', 'dong-trach', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40709028', N'Hoàn Lão', N'Hoan Lao', N'Xã Hoàn Lão', N'Hoan Lao Commune', 'hoan-lao', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40709029', N'Bố Trạch', N'Bo Trach', N'Xã Bố Trạch', N'Bo Trach Commune', 'bo-trach', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40709030', N'Nam Trạch', N'Nam Trach', N'Xã Nam Trạch', N'Nam Trach Commune', 'nam-trach', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40711031', N'Quảng Ninh', N'Quang Ninh', N'Xã Quảng Ninh', N'Quang Ninh Commune', 'quang-ninh', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40711032', N'Ninh Châu', N'Ninh Chau', N'Xã Ninh Châu', N'Ninh Chau Commune', 'ninh-chau', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40711033', N'Trường Ninh', N'Truong Ninh', N'Xã Trường Ninh', N'Truong Ninh Commune', 'truong-ninh', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40711034', N'Trường Sơn', N'Truong Son', N'Xã Trường Sơn', N'Truong Son Commune', 'truong-son', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40713035', N'Lệ Thủy', N'Le Thuy', N'Xã Lệ Thủy', N'Le Thuy Commune', 'le-thuy', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40713036', N'Cam Hồng', N'Cam Hong', N'Xã Cam Hồng', N'Cam Hong Commune', 'cam-hong', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40713037', N'Sen Ngư', N'Sen Ngu', N'Xã Sen Ngư', N'Sen Ngu Commune', 'sen-ngu', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40713038', N'Tân Mỹ', N'Tan My', N'Xã Tân Mỹ', N'Tan My Commune', 'tan-my', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40713039', N'Trường Phú', N'Truong Phu', N'Xã Trường Phú', N'Truong Phu Commune', 'truong-phu', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40713040', N'Lệ Ninh', N'Le Ninh', N'Xã Lệ Ninh', N'Le Ninh Commune', 'le-ninh', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40713041', N'Kim Ngân', N'Kim Ngan', N'Xã Kim Ngân', N'Kim Ngan Commune', 'kim-ngan', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40901042', N'Đông Hà', N'Dong Ha', N'Phường Đông Hà', N'Dong Ha Ward', 'dong-ha', 7, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40901043', N'Nam Đông Hà', N'Nam Dong Ha', N'Phường Nam Đông Hà', N'Nam Dong Ha Ward', 'nam-dong-ha', 7, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40903044', N'Quảng Trị', N'Quang Tri', N'Phường Quảng Trị', N'Quang Tri Ward', 'quang-tri', 7, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40905045', N'Vĩnh Linh', N'Vinh Linh', N'Xã Vĩnh Linh', N'Vinh Linh Commune', 'vinh-linh', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40905046', N'Cửa Tùng', N'Cua Tung', N'Xã Cửa Tùng', N'Cua Tung Commune', 'cua-tung', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40905047', N'Vĩnh Hoàng', N'Vinh Hoang', N'Xã Vĩnh Hoàng', N'Vinh Hoang Commune', 'vinh-hoang', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40905048', N'Vĩnh Thủy', N'Vinh Thuy', N'Xã Vĩnh Thủy', N'Vinh Thuy Commune', 'vinh-thuy', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40905049', N'Bến Quan', N'Ben Quan', N'Xã Bến Quan', N'Ben Quan Commune', 'ben-quan', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40907050', N'Cồn Tiên', N'Con Tien', N'Xã Cồn Tiên', N'Con Tien Commune', 'con-tien', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40907051', N'Cửa Việt', N'Cua Viet', N'Xã Cửa Việt', N'Cua Viet Commune', 'cua-viet', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40907052', N'Gio Linh', N'Gio Linh', N'Xã Gio Linh', N'Gio Linh Commune', 'gio-linh', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40907053', N'Bến Hải', N'Ben Hai', N'Xã Bến Hải', N'Ben Hai Commune', 'ben-hai', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40915054', N'Hướng Lập', N'Huong Lap', N'Xã Hướng Lập', N'Huong Lap Commune', 'huong-lap', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40915055', N'Hướng Phùng', N'Huong Phung', N'Xã Hướng Phùng', N'Huong Phung Commune', 'huong-phung', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40915056', N'Khe Sanh', N'Khe Sanh', N'Xã Khe Sanh', N'Khe Sanh Commune', 'khe-sanh', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40915057', N'Tân Lập', N'Tan Lap', N'Xã Tân Lập', N'Tan Lap Commune', 'tan-lap', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40915058', N'Lao Bảo', N'Lao Bao', N'Xã Lao Bảo', N'Lao Bao Commune', 'lao-bao', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40915059', N'Lìa', N'Lia', N'Xã Lìa', N'Lia Commune', 'lia', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40915060', N'A Dơi', N'A Doi', N'Xã A Dơi', N'A Doi Commune', 'a-doi', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40917061', N'La Lay', N'La Lay', N'Xã La Lay', N'La Lay Commune', 'la-lay', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40917062', N'Tà Rụt', N'Ta Rut', N'Xã Tà Rụt', N'Ta Rut Commune', 'ta-rut', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40917063', N'Đakrông', N'Dakrong', N'Xã Đakrông', N'Dakrong Commune', 'dakrong', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40917064', N'Ba Lòng', N'Ba Long', N'Xã Ba Lòng', N'Ba Long Commune', 'ba-long', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40917065', N'Hướng Hiệp', N'Huong Hiep', N'Xã Hướng Hiệp', N'Huong Hiep Commune', 'huong-hiep', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40909066', N'Cam Lộ', N'Cam Lo', N'Xã Cam Lộ', N'Cam Lo Commune', 'cam-lo', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40909067', N'Hiếu Giang', N'Hieu Giang', N'Xã Hiếu Giang', N'Hieu Giang Commune', 'hieu-giang', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40911068', N'Triệu Phong', N'Trieu Phong', N'Xã Triệu Phong', N'Trieu Phong Commune', 'trieu-phong', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40911069', N'Ái Tử', N'Ai Tu', N'Xã Ái Tử', N'Ai Tu Commune', 'ai-tu', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40911070', N'Triệu Bình', N'Trieu Binh', N'Xã Triệu Bình', N'Trieu Binh Commune', 'trieu-binh', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40911071', N'Triệu Cơ', N'Trieu Co', N'Xã Triệu Cơ', N'Trieu Co Commune', 'trieu-co', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40911072', N'Nam Cửa Việt', N'Nam Cua Viet', N'Xã Nam Cửa Việt', N'Nam Cua Viet Commune', 'nam-cua-viet', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40913073', N'Diên Sanh', N'Dien Sanh', N'Xã Diên Sanh', N'Dien Sanh Commune', 'dien-sanh', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40913074', N'Mỹ Thủy', N'My Thuy', N'Xã Mỹ Thủy', N'My Thuy Commune', 'my-thuy', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40913075', N'Hải Lăng', N'Hai Lang', N'Xã Hải Lăng', N'Hai Lang Commune', 'hai-lang', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40913076', N'Vĩnh Định', N'Vinh Dinh', N'Xã Vĩnh Định', N'Vinh Dinh Commune', 'vinh-dinh', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40913077', N'Nam Hải Lăng', N'Nam Hai Lang', N'Xã Nam Hải Lăng', N'Nam Hai Lang Commune', 'nam-hai-lang', 8, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'40919078', N'khu Cồn Cỏ', N'khu Con Cỏ', N'Đặc khu Cồn Cỏ', N'khu Con Cỏ Đặc', 'khu-con-co', 2, N'409', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41109001', N'Thuận An', N'Thuan An', N'Phường Thuận An', N'Thuan An Ward', 'thuan-an', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41119002', N'Hóa Châu', N'Hoa Chau', N'Phường Hóa Châu', N'Hoa Chau Ward', 'hoa-chau', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41109003', N'Mỹ Thượng', N'My Thuong', N'Phường Mỹ Thượng', N'My Thuong Ward', 'my-thuong', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41101004', N'Vỹ Dạ', N'Vy Da', N'Phường Vỹ Dạ', N'Vy Da Ward', 'vy-da', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41101005', N'Thuận Hóa', N'Thuan Hoa', N'Phường Thuận Hóa', N'Thuan Hoa Ward', 'thuan-hoa', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41101006', N'An Cựu', N'An Cuu', N'Phường An Cựu', N'An Cuu Ward', 'an-cuu', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41101007', N'Thủy Xuân', N'Thuy Xuan', N'Phường Thủy Xuân', N'Thuy Xuan Ward', 'thuy-xuan', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41119008', N'Kim Long', N'Kim Long', N'Phường Kim Long', N'Kim Long Ward', 'kim-long', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41119009', N'Hương An', N'Huong An', N'Phường Hương An', N'Huong An Ward', 'huong-an', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41119010', N'Phú Xuân', N'Phu Xuan', N'Phường Phú Xuân', N'Phu Xuan Ward', 'phu-xuan', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41107011', N'Hương Trà', N'Huong Tra', N'Phường Hương Trà', N'Huong Tra Ward', 'huong-tra', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41107012', N'Kim Trà', N'Kim Tra', N'Phường Kim Trà', N'Kim Tra Ward', 'kim-tra', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41111013', N'Thanh Thủy', N'Thanh Thuy', N'Phường Thanh Thủy', N'Thanh Thuy Ward', 'thanh-thuy', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41111014', N'Hương Thủy', N'Huong Thuy', N'Phường Hương Thủy', N'Huong Thuy Ward', 'huong-thuy', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41111015', N'Phú Bài', N'Phu Bai', N'Phường Phú Bài', N'Phu Bai Ward', 'phu-bai', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41103016', N'Phong Điền', N'Phong Dien', N'Phường Phong Điền', N'Phong Dien Ward', 'phong-dien', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41103017', N'Phong Thái', N'Phong Thai', N'Phường Phong Thái', N'Phong Thai Ward', 'phong-thai', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41103018', N'Phong Dinh', N'Phong Dinh', N'Phường Phong Dinh', N'Phong Dinh Ward', 'phong-dinh', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41103019', N'Phong Phú', N'Phong Phu', N'Phường Phong Phú', N'Phong Phu Ward', 'phong-phu', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41105020', N'Phong Quảng', N'Phong Quang', N'Phường Phong Quảng', N'Phong Quang Ward', 'phong-quang', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41105021', N'Đan Điền', N'Dan Dien', N'Xã Đan Điền', N'Dan Dien Commune', 'dan-dien', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41105022', N'Quảng Điền', N'Quang Dien', N'Xã Quảng Điền', N'Quang Dien Commune', 'quang-dien', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41109023', N'Phú Vinh', N'Phu Vinh', N'Xã Phú Vinh', N'Phu Vinh Commune', 'phu-vinh', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41109024', N'Phú Hồ', N'Phu Ho', N'Xã Phú Hồ', N'Phu Ho Commune', 'phu-ho', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41109025', N'Phú Vang', N'Phu Vang', N'Xã Phú Vang', N'Phu Vang Commune', 'phu-vang', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41113026', N'Vinh Lộc', N'Vinh Loc', N'Xã Vinh Lộc', N'Vinh Loc Commune', 'vinh-loc', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41113027', N'Hưng Lộc', N'Hung Loc', N'Xã Hưng Lộc', N'Hung Loc Commune', 'hung-loc', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41113028', N'Lộc An', N'Loc An', N'Xã Lộc An', N'Loc An Commune', 'loc-an', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41113029', N'Phú Lộc', N'Phu Loc', N'Xã Phú Lộc', N'Phu Loc Commune', 'phu-loc', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41113030', N'Chân Mây – Lăng Cô', N'Chan May – Lang Co', N'Xã Chân Mây – Lăng Cô', N'Chan May – Lang Co Commune', 'chan-may-lang-co', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41113031', N'Long Quảng', N'Long Quang', N'Xã Long Quảng', N'Long Quang Commune', 'long-quang', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41113032', N'Nam Đông', N'Nam Dong', N'Xã Nam Đông', N'Nam Dong Commune', 'nam-dong', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41113033', N'Khe Tre', N'Khe Tre', N'Xã Khe Tre', N'Khe Tre Commune', 'khe-tre', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41107034', N'Bình Điền', N'Binh Dien', N'Xã Bình Điền', N'Binh Dien Commune', 'binh-dien', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41115035', N'A Lưới 1', N'A Luoi 1', N'Xã A Lưới 1', N'A Luoi 1 Commune', 'a-luoi-1', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41115036', N'A Lưới 2', N'A Luoi 2', N'Xã A Lưới 2', N'A Luoi 2 Commune', 'a-luoi-2', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41115037', N'A Lưới 3', N'A Luoi 3', N'Xã A Lưới 3', N'A Luoi 3 Commune', 'a-luoi-3', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41115038', N'A Lưới 4', N'A Luoi 4', N'Xã A Lưới 4', N'A Luoi 4 Commune', 'a-luoi-4', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41115039', N'A Lưới 5', N'A Luoi 5', N'Xã A Lưới 5', N'A Luoi 5 Commune', 'a-luoi-5', 8, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'41101040', N'Dương Nỗ', N'Duong No', N'Phường Dương Nỗ', N'Duong No Ward', 'duong-no', 7, N'411', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50101001', N'Hải Châu', N'Hai Chau', N'Phường Hải Châu', N'Hai Chau Ward', 'hai-chau', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50101002', N'Hoà Cường', N'Hoa Cuong', N'Phường Hoà Cường', N'Hoa Cuong Ward', 'hoa-cuong', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50103003', N'Thanh Khê', N'Thanh Khe', N'Phường Thanh Khê', N'Thanh Khe Ward', 'thanh-khe', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50115004', N'An Khê', N'An Khe', N'Phường An Khê', N'An Khe Ward', 'an-khe', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50105005', N'An Hải', N'An Hai', N'Phường An Hải', N'An Hai Ward', 'an-hai', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50105006', N'Sơn Trà', N'Son Tra', N'Phường Sơn Trà', N'Son Tra Ward', 'son-tra', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50107007', N'Ngũ Hành Sơn', N'Ngu Hanh Son', N'Phường Ngũ Hành Sơn', N'Ngu Hanh Son Ward', 'ngu-hanh-son', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50109008', N'Hoà Khánh', N'Hoa Khanh', N'Phường Hoà Khánh', N'Hoa Khanh Ward', 'hoa-khanh', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50109009', N'Hải Vân', N'Hai Van', N'Phường Hải Vân', N'Hai Van Ward', 'hai-van', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50109010', N'Liên Chiểu', N'Lien Chieu', N'Phường Liên Chiểu', N'Lien Chieu Ward', 'lien-chieu', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50115011', N'Cẩm Lệ', N'Cam Le', N'Phường Cẩm Lệ', N'Cam Le Ward', 'cam-le', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50111012', N'Hoà Xuân', N'Hoa Xuan', N'Phường Hoà Xuân', N'Hoa Xuan Ward', 'hoa-xuan', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50111013', N'Hoà Vang', N'Hoa Vang', N'Xã Hoà Vang', N'Hoa Vang Commune', 'hoa-vang', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50111014', N'Hoà Tiến', N'Hoa Tien', N'Xã Hoà Tiến', N'Hoa Tien Commune', 'hoa-tien', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50111015', N'Bà Nà', N'Ba Na', N'Xã Bà Nà', N'Ba Na Commune', 'ba-na', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50113016', N'khu Hoàng Sa', N'khu Hoang Sa', N'Đặc khu Hoàng Sa', N'khu Hoang Sa Đặc', 'khu-hoang-sa', 2, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50325017', N'Núi Thành', N'Nui Thanh', N'Xã Núi Thành', N'Nui Thanh Commune', 'nui-thanh', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50325018', N'Tam Mỹ', N'Tam My', N'Xã Tam Mỹ', N'Tam My Commune', 'tam-my', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50325019', N'Tam Anh', N'Tam Anh', N'Xã Tam Anh', N'Tam Anh Commune', 'tam-anh', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50325020', N'Đức Phú', N'Duc Phu', N'Xã Đức Phú', N'Duc Phu Commune', 'duc-phu', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50325021', N'Tam Xuân', N'Tam Xuan', N'Xã Tam Xuân', N'Tam Xuan Commune', 'tam-xuan', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50325022', N'Tam Hải', N'Tam Hai', N'Xã Tam Hải', N'Tam Hai Commune', 'tam-hai', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50301023', N'Tam Kỳ', N'Tam Ky', N'Phường Tam Kỳ', N'Tam Ky Ward', 'tam-ky', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50301024', N'Quảng Phú', N'Quang Phu', N'Phường Quảng Phú', N'Quang Phu Ward', 'quang-phu', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50301025', N'Hương Trà', N'Huong Tra', N'Phường Hương Trà', N'Huong Tra Ward', 'huong-tra', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50301026', N'Bàn Thạch', N'Ban Thach', N'Phường Bàn Thạch', N'Ban Thach Ward', 'ban-thach', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50302027', N'Tây Hồ', N'Tay Ho', N'Xã Tây Hồ', N'Tay Ho Commune', 'tay-ho', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50302028', N'Chiên Đàn', N'Chien Dan', N'Xã Chiên Đàn', N'Chien Dan Commune', 'chien-dan', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50302029', N'Phú Ninh', N'Phu Ninh', N'Xã Phú Ninh', N'Phu Ninh Commune', 'phu-ninh', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50321030', N'Lãnh Ngọc', N'Lanh Ngoc', N'Xã Lãnh Ngọc', N'Lanh Ngoc Commune', 'lanh-ngoc', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50321031', N'Tiên Phước', N'Tien Phuoc', N'Xã Tiên Phước', N'Tien Phuoc Commune', 'tien-phuoc', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50321032', N'Thạnh Bình', N'Thanh Binh', N'Xã Thạnh Bình', N'Thanh Binh Commune', 'thanh-binh', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50321033', N'Sơn Cẩm Hà', N'Son Cam Ha', N'Xã Sơn Cẩm Hà', N'Son Cam Ha Commune', 'son-cam-ha', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50327034', N'Trà Liên', N'Tra Lien', N'Xã Trà Liên', N'Tra Lien Commune', 'tra-lien', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50327035', N'Trà Giáp', N'Tra Giap', N'Xã Trà Giáp', N'Tra Giap Commune', 'tra-giap', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50327036', N'Trà Tân', N'Tra Tan', N'Xã Trà Tân', N'Tra Tan Commune', 'tra-tan', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50327037', N'Trà Đốc', N'Tra Doc', N'Xã Trà Đốc', N'Tra Doc Commune', 'tra-doc', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50327038', N'Trà My', N'Tra My', N'Xã Trà My', N'Tra My Commune', 'tra-my', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50329039', N'Nam Trà My', N'Nam Tra My', N'Xã Nam Trà My', N'Nam Tra My Commune', 'nam-tra-my', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50329040', N'Trà Tập', N'Tra Tap', N'Xã Trà Tập', N'Tra Tap Commune', 'tra-tap', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50329041', N'Trà Vân', N'Tra Van', N'Xã Trà Vân', N'Tra Van Commune', 'tra-van', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50329042', N'Trà Linh', N'Tra Linh', N'Xã Trà Linh', N'Tra Linh Commune', 'tra-linh', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50329043', N'Trà Leng', N'Tra Leng', N'Xã Trà Leng', N'Tra Leng Commune', 'tra-leng', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50315044', N'Thăng Bình', N'Thang Binh', N'Xã Thăng Bình', N'Thang Binh Commune', 'thang-binh', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50315045', N'Thăng An', N'Thang An', N'Xã Thăng An', N'Thang An Commune', 'thang-an', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50315046', N'Thăng Trường', N'Thang Truong', N'Xã Thăng Trường', N'Thang Truong Commune', 'thang-truong', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50315047', N'Thăng Điền', N'Thang Dien', N'Xã Thăng Điền', N'Thang Dien Commune', 'thang-dien', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50315048', N'Thăng Phú', N'Thang Phu', N'Xã Thăng Phú', N'Thang Phu Commune', 'thang-phu', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50315049', N'Đồng Dương', N'Dong Duong', N'Xã Đồng Dương', N'Dong Duong Commune', 'dong-duong', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50317050', N'Quế Sơn Trung', N'Que Son Trung', N'Xã Quế Sơn Trung', N'Que Son Trung Commune', 'que-son-trung', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50317051', N'Quế Sơn', N'Que Son', N'Xã Quế Sơn', N'Que Son Commune', 'que-son', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50317052', N'Xuân Phú', N'Xuan Phu', N'Xã Xuân Phú', N'Xuan Phu Commune', 'xuan-phu', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50317053', N'Nông Sơn', N'Nong Son', N'Xã Nông Sơn', N'Nong Son Commune', 'nong-son', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50317054', N'Quế Phước', N'Que Phuoc', N'Xã Quế Phước', N'Que Phuoc Commune', 'que-phuoc', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50311055', N'Duy Nghĩa', N'Duy Nghia', N'Xã Duy Nghĩa', N'Duy Nghia Commune', 'duy-nghia', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50311056', N'Nam Phước', N'Nam Phuoc', N'Xã Nam Phước', N'Nam Phuoc Commune', 'nam-phuoc', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50311057', N'Duy Xuyên', N'Duy Xuyen', N'Xã Duy Xuyên', N'Duy Xuyen Commune', 'duy-xuyen', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50311058', N'Thu Bồn', N'Thu Bon', N'Xã Thu Bồn', N'Thu Bon Commune', 'thu-bon', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50309059', N'Điện Bàn', N'Dien Ban', N'Phường Điện Bàn', N'Dien Ban Ward', 'dien-ban', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50309060', N'Điện Bàn Đông', N'Dien Ban Dong', N'Phường Điện Bàn Đông', N'Dien Ban Dong Ward', 'dien-ban-dong', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50309061', N'An Thắng', N'An Thang', N'Phường An Thắng', N'An Thang Ward', 'an-thang', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50309062', N'Điện Bàn Bắc', N'Dien Ban Bac', N'Phường Điện Bàn Bắc', N'Dien Ban Bac Ward', 'dien-ban-bac', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50309063', N'Điện Bàn Tây', N'Dien Ban Tay', N'Xã Điện Bàn Tây', N'Dien Ban Tay Commune', 'dien-ban-tay', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50309064', N'Gò Nổi', N'Go Noi', N'Xã Gò Nổi', N'Go Noi Commune', 'go-noi', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50303065', N'Hội An', N'Hoi An', N'Phường Hội An', N'Hoi An Ward', 'hoi-an', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50303066', N'Hội An Đông', N'Hoi An Dong', N'Phường Hội An Đông', N'Hoi An Dong Ward', 'hoi-an-dong', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50303067', N'Hội An Tây', N'Hoi An Tay', N'Phường Hội An Tây', N'Hoi An Tay Ward', 'hoi-an-tay', 7, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50303068', N'Tân Hiệp', N'Tan Hiep', N'Xã Tân Hiệp', N'Tan Hiep Commune', 'tan-hiep', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50307069', N'Đại Lộc', N'Dai Loc', N'Xã Đại Lộc', N'Dai Loc Commune', 'dai-loc', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50307070', N'Hà Nha', N'Ha Nha', N'Xã Hà Nha', N'Ha Nha Commune', 'ha-nha', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50307071', N'Thượng Đức', N'Thuong Duc', N'Xã Thượng Đức', N'Thuong Duc Commune', 'thuong-duc', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50307072', N'Vu Gia', N'Vu Gia', N'Xã Vu Gia', N'Vu Gia Commune', 'vu-gia', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50307073', N'Phú Thuận', N'Phu Thuan', N'Xã Phú Thuận', N'Phu Thuan Commune', 'phu-thuan', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50313074', N'Thạnh Mỹ', N'Thanh My', N'Xã Thạnh Mỹ', N'Thanh My Commune', 'thanh-my', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50313075', N'Bến Giằng', N'Ben Giang', N'Xã Bến Giằng', N'Ben Giang Commune', 'ben-giang', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50313076', N'Nam Giang', N'Nam Giang', N'Xã Nam Giang', N'Nam Giang Commune', 'nam-giang', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50313077', N'Đắc Pring', N'Dac Pring', N'Xã Đắc Pring', N'Dac Pring Commune', 'dac-pring', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50313078', N'La Dêê', N'La Dee', N'Xã La Dêê', N'La Dee Commune', 'la-dee', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50313079', N'La Êê', N'La Ee', N'Xã La Êê', N'La Ee Commune', 'la-ee', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50305080', N'Sông Vàng', N'Song Vang', N'Xã Sông Vàng', N'Song Vang Commune', 'song-vang', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50305081', N'Sông Kôn', N'Song Kon', N'Xã Sông Kôn', N'Song Kon Commune', 'song-kon', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50305082', N'Đông Giang', N'Dong Giang', N'Xã Đông Giang', N'Dong Giang Commune', 'dong-giang', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50305083', N'Bến Hiên', N'Ben Hien', N'Xã Bến Hiên', N'Ben Hien Commune', 'ben-hien', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50304084', N'Avương', N'Avuong', N'Xã Avương', N'Avuong Commune', 'avuong', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50304085', N'Tây Giang', N'Tay Giang', N'Xã Tây Giang', N'Tay Giang Commune', 'tay-giang', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50304086', N'Hùng Sơn', N'Hung Son', N'Xã Hùng Sơn', N'Hung Son Commune', 'hung-son', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50319087', N'Hiệp Đức', N'Hiep Duc', N'Xã Hiệp Đức', N'Hiep Duc Commune', 'hiep-duc', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50319088', N'Việt An', N'Viet An', N'Xã Việt An', N'Viet An Commune', 'viet-an', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50319089', N'Phước Trà', N'Phuoc Tra', N'Xã Phước Trà', N'Phuoc Tra Commune', 'phuoc-tra', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50323090', N'Khâm Đức', N'Kham Duc', N'Xã Khâm Đức', N'Kham Duc Commune', 'kham-duc', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50323091', N'Phước Năng', N'Phuoc Nang', N'Xã Phước Năng', N'Phuoc Nang Commune', 'phuoc-nang', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50323092', N'Phước Chánh', N'Phuoc Chanh', N'Xã Phước Chánh', N'Phuoc Chanh Commune', 'phuoc-chanh', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50323093', N'Phước Thành', N'Phuoc Thanh', N'Xã Phước Thành', N'Phuoc Thanh Commune', 'phuoc-thanh', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50323094', N'Phước Hiệp', N'Phuoc Hiep', N'Xã Phước Hiệp', N'Phuoc Hiep Commune', 'phuoc-hiep', 8, N'501', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50501001', N'Tịnh Khê', N'Tinh Khe', N'Xã Tịnh Khê', N'Tinh Khe Commune', 'tinh-khe', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50501002', N'Trương Quang Trọng', N'Truong Quang Trong', N'Phường Trương Quang Trọng', N'Truong Quang Trong Ward', 'truong-quang-trong', 7, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50501003', N'An Phú', N'An Phu', N'Xã An Phú', N'An Phu Commune', 'an-phu', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50501004', N'Cẩm Thành', N'Cam Thanh', N'Phường Cẩm Thành', N'Cam Thanh Ward', 'cam-thanh', 7, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50501005', N'Nghĩa Lộ', N'Nghia Lo', N'Phường Nghĩa Lộ', N'Nghia Lo Ward', 'nghia-lo', 7, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50523006', N'Trà Câu', N'Tra Cau', N'Phường Trà Câu', N'Tra Cau Ward', 'tra-cau', 7, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50523007', N'Nguyễn Nghiêm', N'Nguyen Nghiem', N'Xã Nguyễn Nghiêm', N'Nguyen Nghiem Commune', 'nguyen-nghiem', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50523008', N'Đức Phổ', N'Duc Pho', N'Phường Đức Phổ', N'Duc Pho Ward', 'duc-pho', 7, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50523009', N'Khánh Cường', N'Khanh Cuong', N'Xã Khánh Cường', N'Khanh Cuong Commune', 'khanh-cuong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50523010', N'Sa Huỳnh', N'Sa Huynh', N'Phường Sa Huỳnh', N'Sa Huynh Ward', 'sa-huynh', 7, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50505011', N'Bình Minh', N'Binh Minh', N'Xã Bình Minh', N'Binh Minh Commune', 'binh-minh', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50505012', N'Bình Chương', N'Binh Chuong', N'Xã Bình Chương', N'Binh Chuong Commune', 'binh-chuong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50505013', N'Bình Sơn', N'Binh Son', N'Xã Bình Sơn', N'Binh Son Commune', 'binh-son', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50505014', N'Vạn Tường', N'Van Tuong', N'Xã Vạn Tường', N'Van Tuong Commune', 'van-tuong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50505015', N'Đông Sơn', N'Dong Son', N'Xã Đông Sơn', N'Dong Son Commune', 'dong-son', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50509016', N'Trường Giang', N'Truong Giang', N'Xã Trường Giang', N'Truong Giang Commune', 'truong-giang', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50509017', N'Ba Gia', N'Ba Gia', N'Xã Ba Gia', N'Ba Gia Commune', 'ba-gia', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50509018', N'Sơn Tịnh', N'Son Tinh', N'Xã Sơn Tịnh', N'Son Tinh Commune', 'son-tinh', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50509019', N'Thọ Phong', N'Tho Phong', N'Xã Thọ Phong', N'Tho Phong Commune', 'tho-phong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50515020', N'Tư Nghĩa', N'Tu Nghia', N'Xã Tư Nghĩa', N'Tu Nghia Commune', 'tu-nghia', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50515021', N'Vệ Giang', N'Ve Giang', N'Xã Vệ Giang', N'Ve Giang Commune', 've-giang', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50515022', N'Nghĩa Giang', N'Nghia Giang', N'Xã Nghĩa Giang', N'Nghia Giang Commune', 'nghia-giang', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50515023', N'Trà Giang', N'Tra Giang', N'Xã Trà Giang', N'Tra Giang Commune', 'tra-giang', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50517024', N'Nghĩa Hành', N'Nghia Hanh', N'Xã Nghĩa Hành', N'Nghia Hanh Commune', 'nghia-hanh', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50517025', N'Đình Cương', N'Dinh Cuong', N'Xã Đình Cương', N'Dinh Cuong Commune', 'dinh-cuong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50517026', N'Thiện Tín', N'Thien Tin', N'Xã Thiện Tín', N'Thien Tin Commune', 'thien-tin', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50517027', N'Phước Giang', N'Phuoc Giang', N'Xã Phước Giang', N'Phuoc Giang Commune', 'phuoc-giang', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50521028', N'Long Phụng', N'Long Phung', N'Xã Long Phụng', N'Long Phung Commune', 'long-phung', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50521029', N'Mỏ Cày', N'Mỏ Cay', N'Xã Mỏ Cày', N'Mỏ Cay Commune', 'mo-cay', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50521030', N'Mộ Đức', N'Mo Duc', N'Xã Mộ Đức', N'Mo Duc Commune', 'mo-duc', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50521031', N'Lân Phong', N'Lan Phong', N'Xã Lân Phong', N'Lan Phong Commune', 'lan-phong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50507032', N'Trà Bồng', N'Tra Bong', N'Xã Trà Bồng', N'Tra Bong Commune', 'tra-bong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50507033', N'Đông Trà Bồng', N'Dong Tra Bong', N'Xã Đông Trà Bồng', N'Dong Tra Bong Commune', 'dong-tra-bong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50507034', N'Tây Trà', N'Tay Tra', N'Xã Tây Trà', N'Tay Tra Commune', 'tay-tra', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50507035', N'Thanh Bồng', N'Thanh Bong', N'Xã Thanh Bồng', N'Thanh Bong Commune', 'thanh-bong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50507036', N'Cà Đam', N'Ca Dam', N'Xã Cà Đam', N'Ca Dam Commune', 'ca-dam', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50507037', N'Tây Trà Bồng', N'Tay Tra Bong', N'Xã Tây Trà Bồng', N'Tay Tra Bong Commune', 'tay-tra-bong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50513038', N'Sơn Hạ', N'Son Ha', N'Xã Sơn Hạ', N'Son Ha Commune', 'son-ha', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50513039', N'Sơn Linh', N'Son Linh', N'Xã Sơn Linh', N'Son Linh Commune', 'son-linh', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50513040', N'Sơn Hà', N'Son Ha', N'Xã Sơn Hà', N'Son Ha Commune', 'son-ha', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50513041', N'Sơn Thủy', N'Son Thuy', N'Xã Sơn Thủy', N'Son Thuy Commune', 'son-thuy', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50513042', N'Sơn Kỳ', N'Son Ky', N'Xã Sơn Kỳ', N'Son Ky Commune', 'son-ky', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50511043', N'Sơn Tây', N'Son Tay', N'Xã Sơn Tây', N'Son Tay Commune', 'son-tay', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50511044', N'Sơn Tây Thượng', N'Son Tay Thuong', N'Xã Sơn Tây Thượng', N'Son Tay Thuong Commune', 'son-tay-thuong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50511045', N'Sơn Tây Hạ', N'Son Tay Ha', N'Xã Sơn Tây Hạ', N'Son Tay Ha Commune', 'son-tay-ha', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50519046', N'Minh Long', N'Minh Long', N'Xã Minh Long', N'Minh Long Commune', 'minh-long', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50519047', N'Sơn Mai', N'Son Mai', N'Xã Sơn Mai', N'Son Mai Commune', 'son-mai', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50525048', N'Ba Vì', N'Ba Vi', N'Xã Ba Vì', N'Ba Vi Commune', 'ba-vi', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50525049', N'Ba Tô', N'Ba To', N'Xã Ba Tô', N'Ba To Commune', 'ba-to', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50525050', N'Ba Dinh', N'Ba Dinh', N'Xã Ba Dinh', N'Ba Dinh Commune', 'ba-dinh', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50525051', N'Ba Tơ', N'Ba To', N'Xã Ba Tơ', N'Ba To Commune', 'ba-to', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50525052', N'Ba Vinh', N'Ba Vinh', N'Xã Ba Vinh', N'Ba Vinh Commune', 'ba-vinh', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50525053', N'Ba Động', N'Ba Dong', N'Xã Ba Động', N'Ba Dong Commune', 'ba-dong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50525054', N'Đặng Thùy Trâm', N'Dang Thuy Tram', N'Xã Đặng Thùy Trâm', N'Dang Thuy Tram Commune', 'dang-thuy-tram', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50525055', N'Ba Xa', N'Ba Xa', N'Xã Ba Xa', N'Ba Xa Commune', 'ba-xa', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50503056', N'khu Lý Sơn', N'khu Ly Son', N'Đặc khu Lý Sơn', N'khu Ly Son Đặc', 'khu-ly-son', 2, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60101057', N'Kon Tum', N'Kon Tum', N'Phường Kon Tum', N'Kon Tum Ward', 'kon-tum', 7, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60101058', N'Đăk Cấm', N'Dak Cam', N'Phường Đăk Cấm', N'Dak Cam Ward', 'dak-cam', 7, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60101059', N'Đăk BLa', N'Dak BLa', N'Phường Đăk BLa', N'Dak BLa Ward', 'dak-bla', 7, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60101060', N'Ngọk Bay', N'Ngok Bay', N'Xã Ngọk Bay', N'Ngok Bay Commune', 'ngok-bay', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60101061', N'Ia Chim', N'Ia Chim', N'Xã Ia Chim', N'Ia Chim Commune', 'ia-chim', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60101062', N'Đăk Rơ Wa', N'Dak Ro Wa', N'Xã Đăk Rơ Wa', N'Dak Ro Wa Commune', 'dak-ro-wa', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60111063', N'Đăk Pxi', N'Dak Pxi', N'Xã Đăk Pxi', N'Dak Pxi Commune', 'dak-pxi', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60111064', N'Đăk Mar', N'Dak Mar', N'Xã Đăk Mar', N'Dak Mar Commune', 'dak-mar', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60111065', N'Đăk Ui', N'Dak Ui', N'Xã Đăk Ui', N'Dak Ui Commune', 'dak-ui', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60111066', N'Ngọk Réo', N'Ngok Reo', N'Xã Ngọk Réo', N'Ngok Reo Commune', 'ngok-reo', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60111067', N'Đăk Hà', N'Dak Ha', N'Xã Đăk Hà', N'Dak Ha Commune', 'dak-ha', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60107068', N'Ngọk Tụ', N'Ngok Tu', N'Xã Ngọk Tụ', N'Ngok Tu Commune', 'ngok-tu', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60107069', N'Đăk Tô', N'Dak To', N'Xã Đăk Tô', N'Dak To Commune', 'dak-to', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60107070', N'Kon Đào', N'Kon Dao', N'Xã Kon Đào', N'Kon Dao Commune', 'kon-dao', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60115071', N'Đăk Sao', N'Dak Sao', N'Xã Đăk Sao', N'Dak Sao Commune', 'dak-sao', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60115072', N'Đăk Tờ Kan', N'Dak To Kan', N'Xã Đăk Tờ Kan', N'Dak To Kan Commune', 'dak-to-kan', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60115073', N'Tu Mơ Rông', N'Tu Mo Rong', N'Xã Tu Mơ Rông', N'Tu Mo Rong Commune', 'tu-mo-rong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60115074', N'Măng Ri', N'Mang Ri', N'Xã Măng Ri', N'Mang Ri Commune', 'mang-ri', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60105075', N'Bờ Y', N'Bo Y', N'Xã Bờ Y', N'Bo Y Commune', 'bo-y', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60105076', N'Sa Loong', N'Sa Loong', N'Xã Sa Loong', N'Sa Loong Commune', 'sa-loong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60105077', N'Dục Nông', N'Duc Nong', N'Xã Dục Nông', N'Duc Nong Commune', 'duc-nong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60103078', N'Xốp', N'Xop', N'Xã Xốp', N'Xop Commune', 'xop', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60103079', N'Ngọc Linh', N'Ngoc Linh', N'Xã Ngọc Linh', N'Ngoc Linh Commune', 'ngoc-linh', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60103080', N'Đăk Plô', N'Dak Plo', N'Xã Đăk Plô', N'Dak Plo Commune', 'dak-plo', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60103081', N'Đăk Pék', N'Dak Pek', N'Xã Đăk Pék', N'Dak Pek Commune', 'dak-pek', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60103082', N'Đăk Môn', N'Dak Mon', N'Xã Đăk Môn', N'Dak Mon Commune', 'dak-mon', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60113083', N'Sa Thầy', N'Sa Thay', N'Xã Sa Thầy', N'Sa Thay Commune', 'sa-thay', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60113084', N'Sa Bình', N'Sa Binh', N'Xã Sa Bình', N'Sa Binh Commune', 'sa-binh', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60113085', N'Ya Ly', N'Ya Ly', N'Xã Ya Ly', N'Ya Ly Commune', 'ya-ly', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60114086', N'Ia Tơi', N'Ia Toi', N'Xã Ia Tơi', N'Ia Toi Commune', 'ia-toi', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60108087', N'Đăk Kôi', N'Dak Koi', N'Xã Đăk Kôi', N'Dak Koi Commune', 'dak-koi', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60108088', N'Kon Braih', N'Kon Braih', N'Xã Kon Braih', N'Kon Braih Commune', 'kon-braih', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60108089', N'Đăk Rve', N'Dak Rve', N'Xã Đăk Rve', N'Dak Rve Commune', 'dak-rve', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60109090', N'Măng Đen', N'Mang Den', N'Xã Măng Đen', N'Mang Den Commune', 'mang-den', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60109091', N'Măng Bút', N'Mang But', N'Xã Măng Bút', N'Mang But Commune', 'mang-but', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60109092', N'Kon Plông', N'Kon Plong', N'Xã Kon Plông', N'Kon Plong Commune', 'kon-plong', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60103093', N'Đăk Long', N'Dak Long', N'Xã Đăk Long', N'Dak Long Commune', 'dak-long', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60113094', N'Rờ Kơi', N'Ro Koi', N'Xã Rờ Kơi', N'Ro Koi Commune', 'ro-koi', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60113095', N'Mô Rai', N'Mo Rai', N'Xã Mô Rai', N'Mo Rai Commune', 'mo-rai', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60114096', N'Ia Đal', N'Ia Dal', N'Xã Ia Đal', N'Ia Dal Commune', 'ia-dal', 8, N'505', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51101001', N'Nha Trang', N'Nha Trang', N'Phường Nha Trang', N'Nha Trang Ward', 'nha-trang', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51101002', N'Bắc Nha Trang', N'Bac Nha Trang', N'Phường Bắc Nha Trang', N'Bac Nha Trang Ward', 'bac-nha-trang', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51101003', N'Tây Nha Trang', N'Tay Nha Trang', N'Phường Tây Nha Trang', N'Tay Nha Trang Ward', 'tay-nha-trang', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51101004', N'Nam Nha Trang', N'Nam Nha Trang', N'Phường Nam Nha Trang', N'Nam Nha Trang Ward', 'nam-nha-trang', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51109005', N'Bắc Cam Ranh', N'Bac Cam Ranh', N'Phường Bắc Cam Ranh', N'Bac Cam Ranh Ward', 'bac-cam-ranh', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51109006', N'Cam Ranh', N'Cam Ranh', N'Phường Cam Ranh', N'Cam Ranh Ward', 'cam-ranh', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51109007', N'Cam Linh', N'Cam Linh', N'Phường Cam Linh', N'Cam Linh Ward', 'cam-linh', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51109008', N'Ba Ngòi', N'Ba Ngoi', N'Phường Ba Ngòi', N'Ba Ngoi Ward', 'ba-ngoi', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51109009', N'Nam Cam Ranh', N'Nam Cam Ranh', N'Xã Nam Cam Ranh', N'Nam Cam Ranh Commune', 'nam-cam-ranh', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51105010', N'Bắc Ninh Hoà', N'Bac Ninh Hoa', N'Xã Bắc Ninh Hoà', N'Bac Ninh Hoa Commune', 'bac-ninh-hoa', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51105011', N'Ninh Hoà', N'Ninh Hoa', N'Phường Ninh Hoà', N'Ninh Hoa Ward', 'ninh-hoa', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51105012', N'Tân Định', N'Tan Dinh', N'Xã Tân Định', N'Tan Dinh Commune', 'tan-dinh', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51105013', N'Đông Ninh Hoà', N'Dong Ninh Hoa', N'Phường Đông Ninh Hoà', N'Dong Ninh Hoa Ward', 'dong-ninh-hoa', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51105014', N'Hoà Thắng', N'Hoa Thang', N'Phường Hoà Thắng', N'Hoa Thang Ward', 'hoa-thang', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51105015', N'Nam Ninh Hoà', N'Nam Ninh Hoa', N'Xã Nam Ninh Hoà', N'Nam Ninh Hoa Commune', 'nam-ninh-hoa', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51105016', N'Tây Ninh Hoà', N'Tay Ninh Hoa', N'Xã Tây Ninh Hoà', N'Tay Ninh Hoa Commune', 'tay-ninh-hoa', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51105017', N'Hoà Trí', N'Hoa Tri', N'Xã Hoà Trí', N'Hoa Tri Commune', 'hoa-tri', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51103018', N'Đại Lãnh', N'Dai Lanh', N'Xã Đại Lãnh', N'Dai Lanh Commune', 'dai-lanh', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51103019', N'Tu Bông', N'Tu Bong', N'Xã Tu Bông', N'Tu Bong Commune', 'tu-bong', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51103020', N'Vạn Thắng', N'Van Thang', N'Xã Vạn Thắng', N'Van Thang Commune', 'van-thang', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51103021', N'Vạn Ninh', N'Van Ninh', N'Xã Vạn Ninh', N'Van Ninh Commune', 'van-ninh', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51103022', N'Vạn Hưng', N'Van Hung', N'Xã Vạn Hưng', N'Van Hung Commune', 'van-hung', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51107023', N'Diên Khánh', N'Dien Khanh', N'Xã Diên Khánh', N'Dien Khanh Commune', 'dien-khanh', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51107024', N'Diên Lạc', N'Dien Lac', N'Xã Diên Lạc', N'Dien Lac Commune', 'dien-lac', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51107025', N'Diên Điền', N'Dien Dien', N'Xã Diên Điền', N'Dien Dien Commune', 'dien-dien', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51107026', N'Diên Lâm', N'Dien Lam', N'Xã Diên Lâm', N'Dien Lam Commune', 'dien-lam', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51107027', N'Diên Thọ', N'Dien Tho', N'Xã Diên Thọ', N'Dien Tho Commune', 'dien-tho', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51107028', N'Suối Hiệp', N'Suoi Hiep', N'Xã Suối Hiệp', N'Suoi Hiep Commune', 'suoi-hiep', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51117029', N'Cam Lâm', N'Cam Lam', N'Xã Cam Lâm', N'Cam Lam Commune', 'cam-lam', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51117030', N'Suối Dầu', N'Suoi Dau', N'Xã Suối Dầu', N'Suoi Dau Commune', 'suoi-dau', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51117031', N'Cam Hiệp', N'Cam Hiep', N'Xã Cam Hiệp', N'Cam Hiep Commune', 'cam-hiep', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51117032', N'Cam An', N'Cam An', N'Xã Cam An', N'Cam An Commune', 'cam-an', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51111033', N'Bắc Khánh Vĩnh', N'Bac Khanh Vinh', N'Xã Bắc Khánh Vĩnh', N'Bac Khanh Vinh Commune', 'bac-khanh-vinh', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51111034', N'Trung Khánh Vĩnh', N'Trung Khanh Vinh', N'Xã Trung Khánh Vĩnh', N'Trung Khanh Vinh Commune', 'trung-khanh-vinh', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51111035', N'Tây Khánh Vĩnh', N'Tay Khanh Vinh', N'Xã Tây Khánh Vĩnh', N'Tay Khanh Vinh Commune', 'tay-khanh-vinh', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51111036', N'Nam Khánh Vĩnh', N'Nam Khanh Vinh', N'Xã Nam Khánh Vĩnh', N'Nam Khanh Vinh Commune', 'nam-khanh-vinh', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51111037', N'Khánh Vĩnh', N'Khanh Vinh', N'Xã Khánh Vĩnh', N'Khanh Vinh Commune', 'khanh-vinh', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51113038', N'Khánh Sơn', N'Khanh Son', N'Xã Khánh Sơn', N'Khanh Son Commune', 'khanh-son', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51113039', N'Tây Khánh Sơn', N'Tay Khanh Son', N'Xã Tây Khánh Sơn', N'Tay Khanh Son Commune', 'tay-khanh-son', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51113040', N'Đông Khánh Sơn', N'Dong Khanh Son', N'Xã Đông Khánh Sơn', N'Dong Khanh Son Commune', 'dong-khanh-son', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'51115041', N'khu Trường Sa', N'khu Truong Sa', N'Đặc khu Trường Sa', N'khu Truong Sa Đặc', 'khu-truong-sa', 2, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70501042', N'Phan Rang', N'Phan Rang', N'Phường Phan Rang', N'Phan Rang Ward', 'phan-rang', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70501043', N'Đông Hải', N'Dong Hai', N'Phường Đông Hải', N'Dong Hai Ward', 'dong-hai', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70505044', N'Ninh Chử', N'Ninh Chu', N'Phường Ninh Chử', N'Ninh Chu Ward', 'ninh-chu', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70501045', N'Bảo An', N'Bao An', N'Phường Bảo An', N'Bao An Ward', 'bao-an', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70501046', N'Đô Vinh', N'Do Vinh', N'Phường Đô Vinh', N'Do Vinh Ward', 'do-vinh', 7, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70507047', N'Ninh Phước', N'Ninh Phuoc', N'Xã Ninh Phước', N'Ninh Phuoc Commune', 'ninh-phuoc', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70507048', N'Phước Hữu', N'Phuoc Huu', N'Xã Phước Hữu', N'Phuoc Huu Commune', 'phuoc-huu', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70507049', N'Phước Hậu', N'Phuoc Hau', N'Xã Phước Hậu', N'Phuoc Hau Commune', 'phuoc-hau', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70513050', N'Thuận Nam', N'Thuan Nam', N'Xã Thuận Nam', N'Thuan Nam Commune', 'thuan-nam', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70513051', N'Cà Ná', N'Ca Na', N'Xã Cà Ná', N'Ca Na Commune', 'ca-na', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70513052', N'Phước Hà', N'Phuoc Ha', N'Xã Phước Hà', N'Phuoc Ha Commune', 'phuoc-ha', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70513053', N'Phước Dinh', N'Phuoc Dinh', N'Xã Phước Dinh', N'Phuoc Dinh Commune', 'phuoc-dinh', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70505054', N'Ninh Hải', N'Ninh Hai', N'Xã Ninh Hải', N'Ninh Hai Commune', 'ninh-hai', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70505055', N'Xuân Hải', N'Xuan Hai', N'Xã Xuân Hải', N'Xuan Hai Commune', 'xuan-hai', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70505056', N'Vĩnh Hải', N'Vinh Hai', N'Xã Vĩnh Hải', N'Vinh Hai Commune', 'vinh-hai', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70511057', N'Thuận Bắc', N'Thuan Bac', N'Xã Thuận Bắc', N'Thuan Bac Commune', 'thuan-bac', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70511058', N'Công Hải', N'Cong Hai', N'Xã Công Hải', N'Cong Hai Commune', 'cong-hai', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70503059', N'Ninh Sơn', N'Ninh Son', N'Xã Ninh Sơn', N'Ninh Son Commune', 'ninh-son', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70503060', N'Lâm Sơn', N'Lam Son', N'Xã Lâm Sơn', N'Lam Son Commune', 'lam-son', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70503061', N'Anh Dũng', N'Anh Dung', N'Xã Anh Dũng', N'Anh Dung Commune', 'anh-dung', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70503062', N'Mỹ Sơn', N'My Son', N'Xã Mỹ Sơn', N'My Son Commune', 'my-son', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70509063', N'Bác Ái Đông', N'Bac Ai Dong', N'Xã Bác Ái Đông', N'Bac Ai Dong Commune', 'bac-ai-dong', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70509064', N'Bác Ái', N'Bac Ai', N'Xã Bác Ái', N'Bac Ai Commune', 'bac-ai', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70509065', N'Bác Ái Tây', N'Bac Ai Tay', N'Xã Bác Ái Tây', N'Bac Ai Tay Commune', 'bac-ai-tay', 8, N'511', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50701001', N'Quy Nhơn', N'Quy Nhon', N'Phường Quy Nhơn', N'Quy Nhon Ward', 'quy-nhon', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50701002', N'Quy Nhơn Đông', N'Quy Nhon Dong', N'Phường Quy Nhơn Đông', N'Quy Nhon Dong Ward', 'quy-nhon-dong', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50701003', N'Quy Nhơn Tây', N'Quy Nhon Tay', N'Phường Quy Nhơn Tây', N'Quy Nhon Tay Ward', 'quy-nhon-tay', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50701004', N'Quy Nhơn Nam', N'Quy Nhon Nam', N'Phường Quy Nhơn Nam', N'Quy Nhon Nam Ward', 'quy-nhon-nam', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50701005', N'Quy Nhơn Bắc', N'Quy Nhon Bac', N'Phường Quy Nhơn Bắc', N'Quy Nhon Bac Ward', 'quy-nhon-bac', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50717006', N'Bình Định', N'Binh Dinh', N'Phường Bình Định', N'Binh Dinh Ward', 'binh-dinh', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50717007', N'An Nhơn', N'An Nhon', N'Phường An Nhơn', N'An Nhon Ward', 'an-nhon', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50717008', N'An Nhơn Đông', N'An Nhon Dong', N'Phường An Nhơn Đông', N'An Nhon Dong Ward', 'an-nhon-dong', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50717009', N'An Nhơn Nam', N'An Nhon Nam', N'Phường An Nhơn Nam', N'An Nhon Nam Ward', 'an-nhon-nam', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50717010', N'An Nhơn Bắc', N'An Nhon Bac', N'Phường An Nhơn Bắc', N'An Nhon Bac Ward', 'an-nhon-bac', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50717011', N'An Nhơn Tây', N'An Nhon Tay', N'Xã An Nhơn Tây', N'An Nhon Tay Commune', 'an-nhon-tay', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50705012', N'Bồng Sơn', N'Bong Son', N'Phường Bồng Sơn', N'Bong Son Ward', 'bong-son', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50705013', N'Hoài Nhơn', N'Hoai Nhon', N'Phường Hoài Nhơn', N'Hoai Nhon Ward', 'hoai-nhon', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50705014', N'Tam Quan', N'Tam Quan', N'Phường Tam Quan', N'Tam Quan Ward', 'tam-quan', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50705015', N'Hoài Nhơn Đông', N'Hoai Nhon Dong', N'Phường Hoài Nhơn Đông', N'Hoai Nhon Dong Ward', 'hoai-nhon-dong', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50705016', N'Hoài Nhơn Tây', N'Hoai Nhon Tay', N'Phường Hoài Nhơn Tây', N'Hoai Nhon Tay Ward', 'hoai-nhon-tay', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50705017', N'Hoài Nhơn Nam', N'Hoai Nhon Nam', N'Phường Hoài Nhơn Nam', N'Hoai Nhon Nam Ward', 'hoai-nhon-nam', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50705018', N'Hoài Nhơn Bắc', N'Hoai Nhon Bac', N'Phường Hoài Nhơn Bắc', N'Hoai Nhon Bac Ward', 'hoai-nhon-bac', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50713019', N'Phù Cát', N'Phu Cat', N'Xã Phù Cát', N'Phu Cat Commune', 'phu-cat', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50713020', N'Xuân An', N'Xuan An', N'Xã Xuân An', N'Xuan An Commune', 'xuan-an', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50713021', N'Ngô Mây', N'Ngo May', N'Xã Ngô Mây', N'Ngo May Commune', 'ngo-may', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50713022', N'Cát Tiến', N'Cat Tien', N'Xã Cát Tiến', N'Cat Tien Commune', 'cat-tien', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50713023', N'Đề Gi', N'De Gi', N'Xã Đề Gi', N'De Gi Commune', 'de-gi', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50713024', N'Hoà Hội', N'Hoa Hoi', N'Xã Hoà Hội', N'Hoa Hoi Commune', 'hoa-hoi', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50713025', N'Hội Sơn', N'Hoi Son', N'Xã Hội Sơn', N'Hoi Son Commune', 'hoi-son', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50709026', N'Phù Mỹ', N'Phu My', N'Xã Phù Mỹ', N'Phu My Commune', 'phu-my', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50709027', N'An Lương', N'An Luong', N'Xã An Lương', N'An Luong Commune', 'an-luong', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50709028', N'Bình Dương', N'Binh Duong', N'Xã Bình Dương', N'Binh Duong Commune', 'binh-duong', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50709029', N'Phù Mỹ Đông', N'Phu My Dong', N'Xã Phù Mỹ Đông', N'Phu My Dong Commune', 'phu-my-dong', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50709030', N'Phù Mỹ Tây', N'Phu My Tay', N'Xã Phù Mỹ Tây', N'Phu My Tay Commune', 'phu-my-tay', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50709031', N'Phù Mỹ Nam', N'Phu My Nam', N'Xã Phù Mỹ Nam', N'Phu My Nam Commune', 'phu-my-nam', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50709032', N'Phù Mỹ Bắc', N'Phu My Bac', N'Xã Phù Mỹ Bắc', N'Phu My Bac Commune', 'phu-my-bac', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50719033', N'Tuy Phước', N'Tuy Phuoc', N'Xã Tuy Phước', N'Tuy Phuoc Commune', 'tuy-phuoc', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50719034', N'Tuy Phước Đông', N'Tuy Phuoc Dong', N'Xã Tuy Phước Đông', N'Tuy Phuoc Dong Commune', 'tuy-phuoc-dong', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50719035', N'Tuy Phước Tây', N'Tuy Phuoc Tay', N'Xã Tuy Phước Tây', N'Tuy Phuoc Tay Commune', 'tuy-phuoc-tay', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50719036', N'Tuy Phước Bắc', N'Tuy Phuoc Bac', N'Xã Tuy Phước Bắc', N'Tuy Phuoc Bac Commune', 'tuy-phuoc-bac', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50715037', N'Tây Sơn', N'Tay Son', N'Xã Tây Sơn', N'Tay Son Commune', 'tay-son', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50715038', N'Bình Khê', N'Binh Khe', N'Xã Bình Khê', N'Binh Khe Commune', 'binh-khe', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50715039', N'Bình Phú', N'Binh Phu', N'Xã Bình Phú', N'Binh Phu Commune', 'binh-phu', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50715040', N'Bình Hiệp', N'Binh Hiep', N'Xã Bình Hiệp', N'Binh Hiep Commune', 'binh-hiep', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50715041', N'Bình An', N'Binh An', N'Xã Bình An', N'Binh An Commune', 'binh-an', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50707042', N'Hoài Ân', N'Hoai An', N'Xã Hoài Ân', N'Hoai An Commune', 'hoai-an', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50707043', N'Ân Tường', N'An Tuong', N'Xã Ân Tường', N'An Tuong Commune', 'an-tuong', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50707044', N'Kim Sơn', N'Kim Son', N'Xã Kim Sơn', N'Kim Son Commune', 'kim-son', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50707045', N'Vạn Đức', N'Van Duc', N'Xã Vạn Đức', N'Van Duc Commune', 'van-duc', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50707046', N'Ân Hảo', N'An Hao', N'Xã Ân Hảo', N'An Hao Commune', 'an-hao', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50721047', N'Vân Canh', N'Van Canh', N'Xã Vân Canh', N'Van Canh Commune', 'van-canh', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50721048', N'Canh Vinh', N'Canh Vinh', N'Xã Canh Vinh', N'Canh Vinh Commune', 'canh-vinh', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50721049', N'Canh Liên', N'Canh Lien', N'Xã Canh Liên', N'Canh Lien Commune', 'canh-lien', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50711050', N'Vĩnh Thạnh', N'Vinh Thanh', N'Xã Vĩnh Thạnh', N'Vinh Thanh Commune', 'vinh-thanh', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50711051', N'Vĩnh Thịnh', N'Vinh Thinh', N'Xã Vĩnh Thịnh', N'Vinh Thinh Commune', 'vinh-thinh', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50711052', N'Vĩnh Quang', N'Vinh Quang', N'Xã Vĩnh Quang', N'Vinh Quang Commune', 'vinh-quang', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50711053', N'Vĩnh Sơn', N'Vinh Son', N'Xã Vĩnh Sơn', N'Vinh Son Commune', 'vinh-son', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50703054', N'An Hoà', N'An Hoa', N'Xã An Hoà', N'An Hoa Commune', 'an-hoa', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50703055', N'An Lão', N'An Lao', N'Xã An Lão', N'An Lao Commune', 'an-lao', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50703056', N'An Vinh', N'An Vinh', N'Xã An Vinh', N'An Vinh Commune', 'an-vinh', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50703057', N'An Toàn', N'An Toan', N'Xã An Toàn', N'An Toan Commune', 'an-toan', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60301058', N'Pleiku', N'Pleiku', N'Phường Pleiku', N'Pleiku Ward', 'pleiku', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60301059', N'Hội Phú', N'Hoi Phu', N'Phường Hội Phú', N'Hoi Phu Ward', 'hoi-phu', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60301060', N'Thống Nhất', N'Thong Nhat', N'Phường Thống Nhất', N'Thong Nhat Ward', 'thong-nhat', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60301061', N'Diên Hồng', N'Dien Hong', N'Phường Diên Hồng', N'Dien Hong Ward', 'dien-hong', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60301062', N'An Phú', N'An Phu', N'Phường An Phú', N'An Phu Ward', 'an-phu', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60301063', N'Biển Hồ', N'Bien Ho', N'Xã Biển Hồ', N'Bien Ho Commune', 'bien-ho', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60301064', N'Gào', N'Gao', N'Xã Gào', N'Gao Commune', 'gao', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60307065', N'Ia Ly', N'Ia Ly', N'Xã Ia Ly', N'Ia Ly Commune', 'ia-ly', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60307066', N'Chư Păh', N'Chu Pah', N'Xã Chư Păh', N'Chu Pah Commune', 'chu-pah', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60307067', N'Ia Khươl', N'Ia Khuol', N'Xã Ia Khươl', N'Ia Khuol Commune', 'ia-khuol', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60307068', N'Ia Phí', N'Ia Phi', N'Xã Ia Phí', N'Ia Phi Commune', 'ia-phi', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60317069', N'Chư Prông', N'Chu Prong', N'Xã Chư Prông', N'Chu Prong Commune', 'chu-prong', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60317070', N'Bàu Cạn', N'Bau Can', N'Xã Bàu Cạn', N'Bau Can Commune', 'bau-can', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60317071', N'Ia Boòng', N'Ia Boong', N'Xã Ia Boòng', N'Ia Boong Commune', 'ia-boong', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60317072', N'Ia Lâu', N'Ia Lau', N'Xã Ia Lâu', N'Ia Lau Commune', 'ia-lau', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60317073', N'Ia Pia', N'Ia Pia', N'Xã Ia Pia', N'Ia Pia Commune', 'ia-pia', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60317074', N'Ia Tôr', N'Ia Tor', N'Xã Ia Tôr', N'Ia Tor Commune', 'ia-tor', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60319075', N'Chư Sê', N'Chu Se', N'Xã Chư Sê', N'Chu Se Commune', 'chu-se', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60319076', N'Bờ Ngoong', N'Bo Ngoong', N'Xã Bờ Ngoong', N'Bo Ngoong Commune', 'bo-ngoong', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60319077', N'Ia Ko', N'Ia Ko', N'Xã Ia Ko', N'Ia Ko Commune', 'ia-ko', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60319078', N'Albá', N'Alba', N'Xã Albá', N'Alba Commune', 'alba', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60331079', N'Chư Pưh', N'Chu Puh', N'Xã Chư Pưh', N'Chu Puh Commune', 'chu-puh', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60331080', N'Ia Le', N'Ia Le', N'Xã Ia Le', N'Ia Le Commune', 'ia-le', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60331081', N'Ia Hrú', N'Ia Hru', N'Xã Ia Hrú', N'Ia Hru Commune', 'ia-hru', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60311082', N'An Khê', N'An Khe', N'Phường An Khê', N'An Khe Ward', 'an-khe', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60311083', N'An Bình', N'An Binh', N'Phường An Bình', N'An Binh Ward', 'an-binh', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60311084', N'Cửu An', N'Cuu An', N'Xã Cửu An', N'Cuu An Commune', 'cuu-an', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60327085', N'Đak Pơ', N'Dak Po', N'Xã Đak Pơ', N'Dak Po Commune', 'dak-po', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60327086', N'Ya Hội', N'Ya Hoi', N'Xã Ya Hội', N'Ya Hoi Commune', 'ya-hoi', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60303087', N'Kbang', N'Kbang', N'Xã Kbang', N'Kbang Commune', 'kbang', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60303088', N'Kông Bơ La', N'Kong Bo La', N'Xã Kông Bơ La', N'Kong Bo La Commune', 'kong-bo-la', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60303089', N'Tơ Tung', N'To Tung', N'Xã Tơ Tung', N'To Tung Commune', 'to-tung', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60303090', N'Sơn Lang', N'Son Lang', N'Xã Sơn Lang', N'Son Lang Commune', 'son-lang', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60303091', N'Đak Rong', N'Dak Rong', N'Xã Đak Rong', N'Dak Rong Commune', 'dak-rong', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60313092', N'Kông Chro', N'Kong Chro', N'Xã Kông Chro', N'Kong Chro Commune', 'kong-chro', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60313093', N'Ya Ma', N'Ya Ma', N'Xã Ya Ma', N'Ya Ma Commune', 'ya-ma', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60313094', N'Chư Krey', N'Chu Krey', N'Xã Chư Krey', N'Chu Krey Commune', 'chu-krey', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60313095', N'SRó', N'SRo', N'Xã SRó', N'SRo Commune', 'sro', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60313096', N'Đăk Song', N'Dak Song', N'Xã Đăk Song', N'Dak Song Commune', 'dak-song', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60313097', N'Chơ Long', N'Cho Long', N'Xã Chơ Long', N'Cho Long Commune', 'cho-long', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60321098', N'Ayun Pa', N'Ayun Pa', N'Phường Ayun Pa', N'Ayun Pa Ward', 'ayun-pa', 7, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60321099', N'Ia Rbol', N'Ia Rbol', N'Xã Ia Rbol', N'Ia Rbol Commune', 'ia-rbol', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60321100', N'Ia Sao', N'Ia Sao', N'Xã Ia Sao', N'Ia Sao Commune', 'ia-sao', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60329101', N'Phú Thiện', N'Phu Thien', N'Xã Phú Thiện', N'Phu Thien Commune', 'phu-thien', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60329102', N'Chư A Thai', N'Chu A Thai', N'Xã Chư A Thai', N'Chu A Thai Commune', 'chu-a-thai', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60329103', N'Ia Hiao', N'Ia Hiao', N'Xã Ia Hiao', N'Ia Hiao Commune', 'ia-hiao', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60320104', N'Pờ Tó', N'Po To', N'Xã Pờ Tó', N'Po To Commune', 'po-to', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60320105', N'Ia Pa', N'Ia Pa', N'Xã Ia Pa', N'Ia Pa Commune', 'ia-pa', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60320106', N'Ia Tul', N'Ia Tul', N'Xã Ia Tul', N'Ia Tul Commune', 'ia-tul', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60323107', N'Phú Túc', N'Phu Tuc', N'Xã Phú Túc', N'Phu Tuc Commune', 'phu-tuc', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60323108', N'Ia Dreh', N'Ia Dreh', N'Xã Ia Dreh', N'Ia Dreh Commune', 'ia-dreh', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60323109', N'Ia Rsai', N'Ia Rsai', N'Xã Ia Rsai', N'Ia Rsai Commune', 'ia-rsai', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60323110', N'Uar', N'Uar', N'Xã Uar', N'Uar Commune', 'uar', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60325111', N'Đak Đoa', N'Dak Doa', N'Xã Đak Đoa', N'Dak Doa Commune', 'dak-doa', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60325112', N'Kon Gang', N'Kon Gang', N'Xã Kon Gang', N'Kon Gang Commune', 'kon-gang', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60325113', N'Ia Băng', N'Ia Bang', N'Xã Ia Băng', N'Ia Bang Commune', 'ia-bang', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60325114', N'KDang', N'KDang', N'Xã KDang', N'KDang Commune', 'kdang', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60325115', N'Đak Sơmei', N'Dak Somei', N'Xã Đak Sơmei', N'Dak Somei Commune', 'dak-somei', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60305116', N'Mang Yang', N'Mang Yang', N'Xã Mang Yang', N'Mang Yang Commune', 'mang-yang', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60305117', N'Lơ Pang', N'Lo Pang', N'Xã Lơ Pang', N'Lo Pang Commune', 'lo-pang', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60305118', N'Kon Chiêng', N'Kon Chieng', N'Xã Kon Chiêng', N'Kon Chieng Commune', 'kon-chieng', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60305119', N'Hra', N'Hra', N'Xã Hra', N'Hra Commune', 'hra', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60305120', N'Ayun', N'Ayun', N'Xã Ayun', N'Ayun Commune', 'ayun', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60309121', N'Ia Grai', N'Ia Grai', N'Xã Ia Grai', N'Ia Grai Commune', 'ia-grai', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60309122', N'Ia Krái', N'Ia Krai', N'Xã Ia Krái', N'Ia Krai Commune', 'ia-krai', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60309123', N'Ia Hrung', N'Ia Hrung', N'Xã Ia Hrung', N'Ia Hrung Commune', 'ia-hrung', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60315124', N'Đức Cơ', N'Duc Co', N'Xã Đức Cơ', N'Duc Co Commune', 'duc-co', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60315125', N'Ia Dơk', N'Ia Dok', N'Xã Ia Dơk', N'Ia Dok Commune', 'ia-dok', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60315126', N'Ia Krêl', N'Ia Krel', N'Xã Ia Krêl', N'Ia Krel Commune', 'ia-krel', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50701127', N'Nhơn Châu', N'Nhon Chau', N'Xã Nhơn Châu', N'Nhon Chau Commune', 'nhon-chau', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60317128', N'Ia Púch', N'Ia Puch', N'Xã Ia Púch', N'Ia Puch Commune', 'ia-puch', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60317129', N'Ia Mơ', N'Ia Mo', N'Xã Ia Mơ', N'Ia Mo Commune', 'ia-mo', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60315130', N'Ia Pnôn', N'Ia Pnon', N'Xã Ia Pnôn', N'Ia Pnon Commune', 'ia-pnon', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60315131', N'Ia Nan', N'Ia Nan', N'Xã Ia Nan', N'Ia Nan Commune', 'ia-nan', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60315132', N'Ia Dom', N'Ia Dom', N'Xã Ia Dom', N'Ia Dom Commune', 'ia-dom', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60309133', N'Ia Chia', N'Ia Chia', N'Xã Ia Chia', N'Ia Chia Commune', 'ia-chia', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60309134', N'Ia O', N'Ia O', N'Xã Ia O', N'Ia O Commune', 'ia-o', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60303135', N'Krong', N'Krong', N'Xã Krong', N'Krong Commune', 'krong', 8, N'603', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60501001', N'Hoà Phú', N'Hoa Phu', N'Xã Hoà Phú', N'Hoa Phu Commune', 'hoa-phu', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60501002', N'Buôn Ma Thuột', N'Buon Ma Thuot', N'Phường Buôn Ma Thuột', N'Buon Ma Thuot Ward', 'buon-ma-thuot', 7, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60501003', N'Tân An', N'Tan An', N'Phường Tân An', N'Tan An Ward', 'tan-an', 7, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60501004', N'Tân Lập', N'Tan Lap', N'Phường Tân Lập', N'Tan Lap Ward', 'tan-lap', 7, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60501005', N'Thành Nhất', N'Thanh Nhat', N'Phường Thành Nhất', N'Thanh Nhat Ward', 'thanh-nhat', 7, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60501006', N'Ea Kao', N'Ea Kao', N'Phường Ea Kao', N'Ea Kao Ward', 'ea-kao', 7, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60509007', N'Ea Drông', N'Ea Drong', N'Xã Ea Drông', N'Ea Drong Commune', 'ea-drong', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60509008', N'Buôn Hồ', N'Buon Ho', N'Phường Buôn Hồ', N'Buon Ho Ward', 'buon-ho', 7, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60509009', N'Cư Bao', N'Cu Bao', N'Phường Cư Bao', N'Cu Bao Ward', 'cu-bao', 7, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60505010', N'Ea Súp', N'Ea Sup', N'Xã Ea Súp', N'Ea Sup Commune', 'ea-sup', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60505011', N'Ea Rốk', N'Ea Rok', N'Xã Ea Rốk', N'Ea Rok Commune', 'ea-rok', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60505012', N'Ea Bung', N'Ea Bung', N'Xã Ea Bung', N'Ea Bung Commune', 'ea-bung', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60505013', N'Ia Rvê', N'Ia Rve', N'Xã Ia Rvê', N'Ia Rve Commune', 'ia-rve', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60505014', N'Ia Lốp', N'Ia Lop', N'Xã Ia Lốp', N'Ia Lop Commune', 'ia-lop', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60511015', N'Ea Wer', N'Ea Wer', N'Xã Ea Wer', N'Ea Wer Commune', 'ea-wer', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60511016', N'Ea Nuôl', N'Ea Nuol', N'Xã Ea Nuôl', N'Ea Nuol Commune', 'ea-nuol', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60511017', N'Buôn Đôn', N'Buon Don', N'Xã Buôn Đôn', N'Buon Don Commune', 'buon-don', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60513018', N'Ea Kiết', N'Ea Kiet', N'Xã Ea Kiết', N'Ea Kiet Commune', 'ea-kiet', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60513019', N'Ea M’Droh', N'Ea M’Droh', N'Xã Ea M’Droh', N'Ea M’Droh Commune', 'ea-mdroh', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60513020', N'Quảng Phú', N'Quang Phu', N'Xã Quảng Phú', N'Quang Phu Commune', 'quang-phu', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60513021', N'Cuôr Đăng', N'Cuor Dang', N'Xã Cuôr Đăng', N'Cuor Dang Commune', 'cuor-dang', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60513022', N'Cư M’gar', N'Cu M’gar', N'Xã Cư M’gar', N'Cu M’gar Commune', 'cu-mgar', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60513023', N'Ea Tul', N'Ea Tul', N'Xã Ea Tul', N'Ea Tul Commune', 'ea-tul', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60539024', N'Pơng Drang', N'Pong Drang', N'Xã Pơng Drang', N'Pong Drang Commune', 'pong-drang', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60539025', N'Krông Búk', N'Krong Buk', N'Xã Krông Búk', N'Krong Buk Commune', 'krong-buk', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60539026', N'Cư Pơng', N'Cu Pong', N'Xã Cư Pơng', N'Cu Pong Commune', 'cu-pong', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60503027', N'Ea Khăl', N'Ea Khal', N'Xã Ea Khăl', N'Ea Khal Commune', 'ea-khal', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60503028', N'Ea Drăng', N'Ea Drang', N'Xã Ea Drăng', N'Ea Drang Commune', 'ea-drang', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60503029', N'Ea Wy', N'Ea Wy', N'Xã Ea Wy', N'Ea Wy Commune', 'ea-wy', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60503030', N'Ea H’leo', N'Ea H’leo', N'Xã Ea H’leo', N'Ea H’leo Commune', 'ea-hleo', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60503031', N'Ea Hiao', N'Ea Hiao', N'Xã Ea Hiao', N'Ea Hiao Commune', 'ea-hiao', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60507032', N'Krông Năng', N'Krong Nang', N'Xã Krông Năng', N'Krong Nang Commune', 'krong-nang', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60507033', N'Dliê Ya', N'Dlie Ya', N'Xã Dliê Ya', N'Dlie Ya Commune', 'dlie-ya', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60507034', N'Tam Giang', N'Tam Giang', N'Xã Tam Giang', N'Tam Giang Commune', 'tam-giang', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60507035', N'Phú Xuân', N'Phu Xuan', N'Xã Phú Xuân', N'Phu Xuan Commune', 'phu-xuan', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60519036', N'Krông Pắc', N'Krong Pac', N'Xã Krông Pắc', N'Krong Pac Commune', 'krong-pac', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60519037', N'Ea Knuếc', N'Ea Knuec', N'Xã Ea Knuếc', N'Ea Knuec Commune', 'ea-knuec', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60519038', N'Tân Tiến', N'Tan Tien', N'Xã Tân Tiến', N'Tan Tien Commune', 'tan-tien', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60519039', N'Ea Phê', N'Ea Phe', N'Xã Ea Phê', N'Ea Phe Commune', 'ea-phe', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60519040', N'Ea Kly', N'Ea Kly', N'Xã Ea Kly', N'Ea Kly Commune', 'ea-kly', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60519041', N'Vụ Bổn', N'Vu Bon', N'Xã Vụ Bổn', N'Vu Bon Commune', 'vu-bon', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60515042', N'Ea Kar', N'Ea Kar', N'Xã Ea Kar', N'Ea Kar Commune', 'ea-kar', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60515043', N'Ea Ô', N'Ea O', N'Xã Ea Ô', N'Ea O Commune', 'ea-o', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60515044', N'Ea Knốp', N'Ea Knop', N'Xã Ea Knốp', N'Ea Knop Commune', 'ea-knop', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60515045', N'Cư Yang', N'Cu Yang', N'Xã Cư Yang', N'Cu Yang Commune', 'cu-yang', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60515046', N'Ea Păl', N'Ea Pal', N'Xã Ea Păl', N'Ea Pal Commune', 'ea-pal', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60517047', N'M’Drắk', N'M’Drak', N'Xã M’Drắk', N'M’Drak Commune', 'mdrak', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60517048', N'Ea Riêng', N'Ea Rieng', N'Xã Ea Riêng', N'Ea Rieng Commune', 'ea-rieng', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60517049', N'Cư M’ta', N'Cu M’ta', N'Xã Cư M’ta', N'Cu M’ta Commune', 'cu-mta', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60517050', N'Krông Á', N'Krong A', N'Xã Krông Á', N'Krong A Commune', 'krong-a', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60517051', N'Cư Prao', N'Cu Prao', N'Xã Cư Prao', N'Cu Prao Commune', 'cu-prao', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60517052', N'Ea Trang', N'Ea Trang', N'Xã Ea Trang', N'Ea Trang Commune', 'ea-trang', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60525053', N'Hoà Sơn', N'Hoa Son', N'Xã Hoà Sơn', N'Hoa Son Commune', 'hoa-son', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60525054', N'Dang Kang', N'Dang Kang', N'Xã Dang Kang', N'Dang Kang Commune', 'dang-kang', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60525055', N'Krông Bông', N'Krong Bong', N'Xã Krông Bông', N'Krong Bong Commune', 'krong-bong', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60525056', N'Yang Mao', N'Yang Mao', N'Xã Yang Mao', N'Yang Mao Commune', 'yang-mao', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60525057', N'Cư Pui', N'Cu Pui', N'Xã Cư Pui', N'Cu Pui Commune', 'cu-pui', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60531058', N'Liên Sơn Lắk', N'Lien Son Lak', N'Xã Liên Sơn Lắk', N'Lien Son Lak Commune', 'lien-son-lak', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60531059', N'Đắk Liêng', N'Dak Lieng', N'Xã Đắk Liêng', N'Dak Lieng Commune', 'dak-lieng', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60531060', N'Nam Ka', N'Nam Ka', N'Xã Nam Ka', N'Nam Ka Commune', 'nam-ka', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60531061', N'Đắk Phơi', N'Dak Phoi', N'Xã Đắk Phơi', N'Dak Phoi Commune', 'dak-phoi', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60531062', N'Krông Nô', N'Krong No', N'Xã Krông Nô', N'Krong No Commune', 'krong-no', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60537063', N'Ea Ning', N'Ea Ning', N'Xã Ea Ning', N'Ea Ning Commune', 'ea-ning', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60537064', N'Dray Bhăng', N'Dray Bhang', N'Xã Dray Bhăng', N'Dray Bhang Commune', 'dray-bhang', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60537065', N'Ea Ktur', N'Ea Ktur', N'Xã Ea Ktur', N'Ea Ktur Commune', 'ea-ktur', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60523066', N'Krông Ana', N'Krong Ana', N'Xã Krông Ana', N'Krong Ana Commune', 'krong-ana', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60523067', N'Dur Kmăl', N'Dur Kmal', N'Xã Dur Kmăl', N'Dur Kmal Commune', 'dur-kmal', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60523068', N'Ea Na', N'Ea Na', N'Xã Ea Na', N'Ea Na Commune', 'ea-na', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50901069', N'Tuy Hòa', N'Tuy Hoa', N'Phường Tuy Hòa', N'Tuy Hoa Ward', 'tuy-hoa', 7, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50901070', N'Phú Yên', N'Phu Yen', N'Phường Phú Yên', N'Phu Yen Ward', 'phu-yen', 7, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50901071', N'Bình Kiến', N'Binh Kien', N'Phường Bình Kiến', N'Binh Kien Ward', 'binh-kien', 7, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50905072', N'Xuân Thọ', N'Xuan Tho', N'Xã Xuân Thọ', N'Xuan Tho Commune', 'xuan-tho', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50905073', N'Xuân Cảnh', N'Xuan Canh', N'Xã Xuân Cảnh', N'Xuan Canh Commune', 'xuan-canh', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50905074', N'Xuân Lộc', N'Xuan Loc', N'Xã Xuân Lộc', N'Xuan Loc Commune', 'xuan-loc', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50905075', N'Xuân Đài', N'Xuan Dai', N'Phường Xuân Đài', N'Xuan Dai Ward', 'xuan-dai', 7, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50905076', N'Sông Cầu', N'Song Cau', N'Phường Sông Cầu', N'Song Cau Ward', 'song-cau', 7, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50911077', N'Hòa Xuân', N'Hoa Xuan', N'Xã Hòa Xuân', N'Hoa Xuan Commune', 'hoa-xuan', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50911078', N'Đông Hòa', N'Dong Hoa', N'Phường Đông Hòa', N'Dong Hoa Ward', 'dong-hoa', 7, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50911079', N'Hòa Hiệp', N'Hoa Hiep', N'Phường Hòa Hiệp', N'Hoa Hiep Ward', 'hoa-hiep', 7, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50907080', N'Tuy An Bắc', N'Tuy An Bac', N'Xã Tuy An Bắc', N'Tuy An Bac Commune', 'tuy-an-bac', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50907081', N'Tuy An Đông', N'Tuy An Dong', N'Xã Tuy An Đông', N'Tuy An Dong Commune', 'tuy-an-dong', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50907082', N'Ô Loan', N'O Loan', N'Xã Ô Loan', N'O Loan Commune', 'o-loan', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50907083', N'Tuy An Nam', N'Tuy An Nam', N'Xã Tuy An Nam', N'Tuy An Nam Commune', 'tuy-an-nam', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50907084', N'Tuy An Tây', N'Tuy An Tay', N'Xã Tuy An Tây', N'Tuy An Tay Commune', 'tuy-an-tay', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50915085', N'Phú Hòa 1', N'Phu Hoa 1', N'Xã Phú Hòa 1', N'Phu Hoa 1 Commune', 'phu-hoa-1', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50915086', N'Phú Hòa 2', N'Phu Hoa 2', N'Xã Phú Hòa 2', N'Phu Hoa 2 Commune', 'phu-hoa-2', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50912087', N'Tây Hòa', N'Tay Hoa', N'Xã Tây Hòa', N'Tay Hoa Commune', 'tay-hoa', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50912088', N'Hòa Thịnh', N'Hoa Thinh', N'Xã Hòa Thịnh', N'Hoa Thinh Commune', 'hoa-thinh', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50912089', N'Hòa Mỹ', N'Hoa My', N'Xã Hòa Mỹ', N'Hoa My Commune', 'hoa-my', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50912090', N'Sơn Thành', N'Son Thanh', N'Xã Sơn Thành', N'Son Thanh Commune', 'son-thanh', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50909091', N'Sơn Hòa', N'Son Hoa', N'Xã Sơn Hòa', N'Son Hoa Commune', 'son-hoa', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50909092', N'Vân Hòa', N'Van Hoa', N'Xã Vân Hòa', N'Van Hoa Commune', 'van-hoa', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50909093', N'Tây Sơn', N'Tay Son', N'Xã Tây Sơn', N'Tay Son Commune', 'tay-son', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50909094', N'Suối Trai', N'Suoi Trai', N'Xã Suối Trai', N'Suoi Trai Commune', 'suoi-trai', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50913095', N'Ea Ly', N'Ea Ly', N'Xã Ea Ly', N'Ea Ly Commune', 'ea-ly', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50913096', N'Ea Bá', N'Ea Ba', N'Xã Ea Bá', N'Ea Ba Commune', 'ea-ba', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50913097', N'Đức Bình', N'Duc Binh', N'Xã Đức Bình', N'Duc Binh Commune', 'duc-binh', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50913098', N'Sông Hinh', N'Song Hinh', N'Xã Sông Hinh', N'Song Hinh Commune', 'song-hinh', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50903099', N'Xuân Lãnh', N'Xuan Lanh', N'Xã Xuân Lãnh', N'Xuan Lanh Commune', 'xuan-lanh', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50903100', N'Phú Mỡ', N'Phu Mo', N'Xã Phú Mỡ', N'Phu Mo Commune', 'phu-mo', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50903101', N'Xuân Phước', N'Xuan Phuoc', N'Xã Xuân Phước', N'Xuan Phuoc Commune', 'xuan-phuoc', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'50903102', N'Đồng Xuân', N'Dong Xuan', N'Xã Đồng Xuân', N'Dong Xuan Commune', 'dong-xuan', 8, N'605', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70301001', N'Xuân Hương - Đà Lạt', N'Xuan Huong - Da Lat', N'Phường Xuân Hương - Đà Lạt', N'Xuan Huong - Da Lat Ward', 'xuan-huong-da-lat', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70301002', N'Cam Ly - Đà Lạt', N'Cam Ly - Da Lat', N'Phường Cam Ly - Đà Lạt', N'Cam Ly - Da Lat Ward', 'cam-ly-da-lat', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70301003', N'Lâm Viên - Đà Lạt', N'Lam Vien - Da Lat', N'Phường Lâm Viên - Đà Lạt', N'Lam Vien - Da Lat Ward', 'lam-vien-da-lat', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70301004', N'Xuân Trường - Đà Lạt', N'Xuan Truong - Da Lat', N'Phường Xuân Trường - Đà Lạt', N'Xuan Truong - Da Lat Ward', 'xuan-truong-da-lat', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70305005', N'Langbiang - Đà Lạt', N'Langbiang - Da Lat', N'Phường Langbiang - Đà Lạt', N'Langbiang - Da Lat Ward', 'langbiang-da-lat', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70303006', N'1 Bảo Lộc', N'1 Bao Loc', N'Phường 1 Bảo Lộc', N'1 Bao Loc Ward', '1-bao-loc', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70303007', N'2 Bảo Lộc', N'2 Bao Loc', N'Phường 2 Bảo Lộc', N'2 Bao Loc Ward', '2-bao-loc', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70303008', N'3 Bảo Lộc', N'3 Bao Loc', N'Phường 3 Bảo Lộc', N'3 Bao Loc Ward', '3-bao-loc', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70303009', N'B'' Lao', N'B'' Lao', N'Phường B'' Lao', N'B'' Lao Ward', 'b-lao', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70305010', N'Lạc Dương', N'Lac Duong', N'Xã Lạc Dương', N'Lac Duong Commune', 'lac-duong', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70307011', N'Đơn Dương', N'Don Duong', N'Xã Đơn Dương', N'Don Duong Commune', 'don-duong', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70307012', N'Ka Đô', N'Ka Do', N'Xã Ka Đô', N'Ka Do Commune', 'ka-do', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70307013', N'Quảng Lập', N'Quang Lap', N'Xã Quảng Lập', N'Quang Lap Commune', 'quang-lap', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70307014', N'D''Ran', N'D''Ran', N'Xã D''Ran', N'D''Ran Commune', 'dran', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70309015', N'Hiệp Thạnh', N'Hiep Thanh', N'Xã Hiệp Thạnh', N'Hiep Thanh Commune', 'hiep-thanh', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70309016', N'Đức Trọng', N'Duc Trong', N'Xã Đức Trọng', N'Duc Trong Commune', 'duc-trong', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70309017', N'Tân Hội', N'Tan Hoi', N'Xã Tân Hội', N'Tan Hoi Commune', 'tan-hoi', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70309018', N'Tà Hine', N'Ta Hine', N'Xã Tà Hine', N'Ta Hine Commune', 'ta-hine', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70309019', N'Tà Năng', N'Ta Nang', N'Xã Tà Năng', N'Ta Nang Commune', 'ta-nang', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70311020', N'Đinh Văn - Lâm Hà', N'Dinh Van - Lam Ha', N'Xã Đinh Văn - Lâm Hà', N'Dinh Van - Lam Ha Commune', 'dinh-van-lam-ha', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70311021', N'Phú Sơn - Lâm Hà', N'Phu Son - Lam Ha', N'Xã Phú Sơn - Lâm Hà', N'Phu Son - Lam Ha Commune', 'phu-son-lam-ha', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70311022', N'Nam Hà - Lâm Hà', N'Nam Ha - Lam Ha', N'Xã Nam Hà - Lâm Hà', N'Nam Ha - Lam Ha Commune', 'nam-ha-lam-ha', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70311023', N'Nam Ban - Lâm Hà', N'Nam Ban - Lam Ha', N'Xã Nam Ban - Lâm Hà', N'Nam Ban - Lam Ha Commune', 'nam-ban-lam-ha', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70311024', N'Tân Hà - Lâm Hà', N'Tan Ha - Lam Ha', N'Xã Tân Hà - Lâm Hà', N'Tan Ha - Lam Ha Commune', 'tan-ha-lam-ha', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70311025', N'Phúc Thọ - Lâm Hà', N'Phuc Tho - Lam Ha', N'Xã Phúc Thọ - Lâm Hà', N'Phuc Tho - Lam Ha Commune', 'phuc-tho-lam-ha', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70323026', N'Đam Rông 1', N'Dam Rong 1', N'Xã Đam Rông 1', N'Dam Rong 1 Commune', 'dam-rong-1', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70323027', N'Đam Rông 2', N'Dam Rong 2', N'Xã Đam Rông 2', N'Dam Rong 2 Commune', 'dam-rong-2', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70323028', N'Đam Rông 3', N'Dam Rong 3', N'Xã Đam Rông 3', N'Dam Rong 3 Commune', 'dam-rong-3', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70323029', N'Đam Rông 4', N'Dam Rong 4', N'Xã Đam Rông 4', N'Dam Rong 4 Commune', 'dam-rong-4', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70315030', N'Di Linh', N'Di Linh', N'Xã Di Linh', N'Di Linh Commune', 'di-linh', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70315031', N'Hoà Ninh', N'Hoa Ninh', N'Xã Hoà Ninh', N'Hoa Ninh Commune', 'hoa-ninh', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70315032', N'Hoà Bắc', N'Hoa Bac', N'Xã Hoà Bắc', N'Hoa Bac Commune', 'hoa-bac', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70315033', N'Đinh Trang Thượng', N'Dinh Trang Thuong', N'Xã Đinh Trang Thượng', N'Dinh Trang Thuong Commune', 'dinh-trang-thuong', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70315034', N'Bảo Thuận', N'Bao Thuan', N'Xã Bảo Thuận', N'Bao Thuan Commune', 'bao-thuan', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70315035', N'Sơn Điền', N'Son Dien', N'Xã Sơn Điền', N'Son Dien Commune', 'son-dien', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70315036', N'Gia Hiệp', N'Gia Hiep', N'Xã Gia Hiệp', N'Gia Hiep Commune', 'gia-hiep', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70313037', N'Bảo Lâm 1', N'Bao Lam 1', N'Xã Bảo Lâm 1', N'Bao Lam 1 Commune', 'bao-lam-1', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70313038', N'Bảo Lâm 2', N'Bao Lam 2', N'Xã Bảo Lâm 2', N'Bao Lam 2 Commune', 'bao-lam-2', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70313039', N'Bảo Lâm 3', N'Bao Lam 3', N'Xã Bảo Lâm 3', N'Bao Lam 3 Commune', 'bao-lam-3', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70313040', N'Bảo Lâm 4', N'Bao Lam 4', N'Xã Bảo Lâm 4', N'Bao Lam 4 Commune', 'bao-lam-4', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70313041', N'Bảo Lâm 5', N'Bao Lam 5', N'Xã Bảo Lâm 5', N'Bao Lam 5 Commune', 'bao-lam-5', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70317042', N'Đạ Huoai', N'Da Huoai', N'Xã Đạ Huoai', N'Da Huoai Commune', 'da-huoai', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70317043', N'Đạ Huoai 2', N'Da Huoai 2', N'Xã Đạ Huoai 2', N'Da Huoai 2 Commune', 'da-huoai-2', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70317044', N'Đạ Huoai 3', N'Da Huoai 3', N'Xã Đạ Huoai 3', N'Da Huoai 3 Commune', 'da-huoai-3', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70317045', N'Đạ Tẻh', N'Da Teh', N'Xã Đạ Tẻh', N'Da Teh Commune', 'da-teh', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70317046', N'Đạ Tẻh 2', N'Da Teh 2', N'Xã Đạ Tẻh 2', N'Da Teh 2 Commune', 'da-teh-2', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70317047', N'Đạ Tẻh 3', N'Da Teh 3', N'Xã Đạ Tẻh 3', N'Da Teh 3 Commune', 'da-teh-3', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70317048', N'Cát Tiên', N'Cat Tien', N'Xã Cát Tiên', N'Cat Tien Commune', 'cat-tien', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70317049', N'Cát Tiên 2', N'Cat Tien 2', N'Xã Cát Tiên 2', N'Cat Tien 2 Commune', 'cat-tien-2', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70317050', N'Cát Tiên 3', N'Cat Tien 3', N'Xã Cát Tiên 3', N'Cat Tien 3 Commune', 'cat-tien-3', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71501051', N'Hàm Thắng', N'Ham Thang', N'Phường Hàm Thắng', N'Ham Thang Ward', 'ham-thang', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71501052', N'Bình Thuận', N'Binh Thuan', N'Phường Bình Thuận', N'Binh Thuan Ward', 'binh-thuan', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71501053', N'Mũi Né', N'Mui Ne', N'Phường Mũi Né', N'Mui Ne Ward', 'mui-ne', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71501054', N'Phú Thuỷ', N'Phu Thuy', N'Phường Phú Thuỷ', N'Phu Thuy Ward', 'phu-thuy', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71501055', N'Phan Thiết', N'Phan Thiet', N'Phường Phan Thiết', N'Phan Thiet Ward', 'phan-thiet', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71501056', N'Tiến Thành', N'Tien Thanh', N'Phường Tiến Thành', N'Tien Thanh Ward', 'tien-thanh', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71513057', N'La Gi', N'La Gi', N'Phường La Gi', N'La Gi Ward', 'la-gi', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71513058', N'Phước Hội', N'Phuoc Hoi', N'Phường Phước Hội', N'Phuoc Hoi Ward', 'phuoc-hoi', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71501059', N'Tuyên Quang', N'Tuyen Quang', N'Xã Tuyên Quang', N'Tuyen Quang Commune', 'tuyen-quang', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71513060', N'Tân Hải', N'Tan Hai', N'Xã Tân Hải', N'Tan Hai Commune', 'tan-hai', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71503061', N'Vĩnh Hảo', N'Vinh Hao', N'Xã Vĩnh Hảo', N'Vinh Hao Commune', 'vinh-hao', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71503062', N'Liên Hương', N'Lien Huong', N'Xã Liên Hương', N'Lien Huong Commune', 'lien-huong', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71503063', N'Tuy Phong', N'Tuy Phong', N'Xã Tuy Phong', N'Tuy Phong Commune', 'tuy-phong', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71503064', N'Phan Rí Cửa', N'Phan Ri Cua', N'Xã Phan Rí Cửa', N'Phan Ri Cua Commune', 'phan-ri-cua', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71505065', N'Bắc Bình', N'Bac Binh', N'Xã Bắc Bình', N'Bac Binh Commune', 'bac-binh', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71505066', N'Hồng Thái', N'Hong Thai', N'Xã Hồng Thái', N'Hong Thai Commune', 'hong-thai', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71505067', N'Hải Ninh', N'Hai Ninh', N'Xã Hải Ninh', N'Hai Ninh Commune', 'hai-ninh', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71505068', N'Phan Sơn', N'Phan Son', N'Xã Phan Sơn', N'Phan Son Commune', 'phan-son', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71505069', N'Sông Lũy', N'Song Luy', N'Xã Sông Lũy', N'Song Luy Commune', 'song-luy', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71505070', N'Lương Sơn', N'Luong Son', N'Xã Lương Sơn', N'Luong Son Commune', 'luong-son', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71505071', N'Hoà Thắng', N'Hoa Thang', N'Xã Hoà Thắng', N'Hoa Thang Commune', 'hoa-thang', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71507072', N'Đông Giang', N'Dong Giang', N'Xã Đông Giang', N'Dong Giang Commune', 'dong-giang', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71507073', N'La Dạ', N'La Da', N'Xã La Dạ', N'La Da Commune', 'la-da', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71507074', N'Hàm Thuận Bắc', N'Ham Thuan Bac', N'Xã Hàm Thuận Bắc', N'Ham Thuan Bac Commune', 'ham-thuan-bac', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71507075', N'Hàm Thuận', N'Ham Thuan', N'Xã Hàm Thuận', N'Ham Thuan Commune', 'ham-thuan', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71507076', N'Hồng Sơn', N'Hong Son', N'Xã Hồng Sơn', N'Hong Son Commune', 'hong-son', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71507077', N'Hàm Liêm', N'Ham Liem', N'Xã Hàm Liêm', N'Ham Liem Commune', 'ham-liem', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71509078', N'Hàm Thạnh', N'Ham Thanh', N'Xã Hàm Thạnh', N'Ham Thanh Commune', 'ham-thanh', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71509079', N'Hàm Kiệm', N'Ham Kiem', N'Xã Hàm Kiệm', N'Ham Kiem Commune', 'ham-kiem', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71509080', N'Tân Thành', N'Tan Thanh', N'Xã Tân Thành', N'Tan Thanh Commune', 'tan-thanh', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71509081', N'Hàm Thuận Nam', N'Ham Thuan Nam', N'Xã Hàm Thuận Nam', N'Ham Thuan Nam Commune', 'ham-thuan-nam', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71509082', N'Tân Lập', N'Tan Lap', N'Xã Tân Lập', N'Tan Lap Commune', 'tan-lap', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71514083', N'Tân Minh', N'Tan Minh', N'Xã Tân Minh', N'Tan Minh Commune', 'tan-minh', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71514084', N'Hàm Tân', N'Ham Tan', N'Xã Hàm Tân', N'Ham Tan Commune', 'ham-tan', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71514085', N'Sơn Mỹ', N'Son My', N'Xã Sơn Mỹ', N'Son My Commune', 'son-my', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71511086', N'Bắc Ruộng', N'Bac Ruong', N'Xã Bắc Ruộng', N'Bac Ruong Commune', 'bac-ruong', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71511087', N'Nghị Đức', N'Nghi Duc', N'Xã Nghị Đức', N'Nghi Duc Commune', 'nghi-duc', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71511088', N'Đồng Kho', N'Dong Kho', N'Xã Đồng Kho', N'Dong Kho Commune', 'dong-kho', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71511089', N'Tánh Linh', N'Tanh Linh', N'Xã Tánh Linh', N'Tanh Linh Commune', 'tanh-linh', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71511090', N'Suối Kiết', N'Suoi Kiet', N'Xã Suối Kiết', N'Suoi Kiet Commune', 'suoi-kiet', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71515091', N'Nam Thành', N'Nam Thanh', N'Xã Nam Thành', N'Nam Thanh Commune', 'nam-thanh', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71515092', N'Đức Linh', N'Duc Linh', N'Xã Đức Linh', N'Duc Linh Commune', 'duc-linh', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71515093', N'Hoài Đức', N'Hoai Duc', N'Xã Hoài Đức', N'Hoai Duc Commune', 'hoai-duc', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71515094', N'Trà Tân', N'Tra Tan', N'Xã Trà Tân', N'Tra Tan Commune', 'tra-tan', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71517095', N'khu Phú Quý', N'khu Phu Quy', N'Đặc khu Phú Quý', N'khu Phu Quy Đặc', 'khu-phu-quy', 2, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60613096', N'Bắc Gia Nghĩa', N'Bac Gia Nghia', N'Phường Bắc Gia Nghĩa', N'Bac Gia Nghia Ward', 'bac-gia-nghia', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60613097', N'Nam Gia Nghĩa', N'Nam Gia Nghia', N'Phường Nam Gia Nghĩa', N'Nam Gia Nghia Ward', 'nam-gia-nghia', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60613098', N'Đông Gia Nghĩa', N'Dong Gia Nghia', N'Phường Đông Gia Nghĩa', N'Dong Gia Nghia Ward', 'dong-gia-nghia', 7, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60603099', N'Đắk Wil', N'Dak Wil', N'Xã Đắk Wil', N'Dak Wil Commune', 'dak-wil', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60603100', N'Nam Dong', N'Nam Dong', N'Xã Nam Dong', N'Nam Dong Commune', 'nam-dong', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60603101', N'Cư Jút', N'Cu Jut', N'Xã Cư Jút', N'Cu Jut Commune', 'cu-jut', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60607102', N'Thuận An', N'Thuan An', N'Xã Thuận An', N'Thuan An Commune', 'thuan-an', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60607103', N'Đức Lập', N'Duc Lap', N'Xã Đức Lập', N'Duc Lap Commune', 'duc-lap', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60607104', N'Đắk Mil', N'Dak Mil', N'Xã Đắk Mil', N'Dak Mil Commune', 'dak-mil', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60607105', N'Đắk Sắk', N'Dak Sak', N'Xã Đắk Sắk', N'Dak Sak Commune', 'dak-sak', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60605106', N'Nam Đà', N'Nam Da', N'Xã Nam Đà', N'Nam Da Commune', 'nam-da', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60605107', N'Krông Nô', N'Krong No', N'Xã Krông Nô', N'Krong No Commune', 'krong-no', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60605108', N'Nâm Nung', N'Nam Nung', N'Xã Nâm Nung', N'Nam Nung Commune', 'nam-nung', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60605109', N'Quảng Phú', N'Quang Phu', N'Xã Quảng Phú', N'Quang Phu Commune', 'quang-phu', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60609110', N'Đắk song', N'Dak song', N'Xã Đắk song', N'Dak song Commune', 'dak-song', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60609111', N'Đức An', N'Duc An', N'Xã Đức An', N'Duc An Commune', 'duc-an', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60609112', N'Thuận Hạnh', N'Thuan Hanh', N'Xã Thuận Hạnh', N'Thuan Hanh Commune', 'thuan-hanh', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60609113', N'Trường Xuân', N'Truong Xuan', N'Xã Trường Xuân', N'Truong Xuan Commune', 'truong-xuan', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60615114', N'Tà Đùng', N'Ta Dung', N'Xã Tà Đùng', N'Ta Dung Commune', 'ta-dung', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60615115', N'Quảng Khê', N'Quang Khe', N'Xã Quảng Khê', N'Quang Khe Commune', 'quang-khe', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60617116', N'Quảng Tân', N'Quang Tan', N'Xã Quảng Tân', N'Quang Tan Commune', 'quang-tan', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60617117', N'Tuy Đức', N'Tuy Duc', N'Xã Tuy Đức', N'Tuy Duc Commune', 'tuy-duc', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60611118', N'Kiến Đức', N'Kien Duc', N'Xã Kiến Đức', N'Kien Duc Commune', 'kien-duc', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60611119', N'Nhân Cơ', N'Nhan Co', N'Xã Nhân Cơ', N'Nhan Co Commune', 'nhan-co', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60611120', N'Quảng Tín', N'Quang Tin', N'Xã Quảng Tín', N'Quang Tin Commune', 'quang-tin', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70309121', N'Ninh Gia', N'Ninh Gia', N'Xã Ninh Gia', N'Ninh Gia Commune', 'ninh-gia', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60615122', N'Quảng Hoà', N'Quang Hoa', N'Xã Quảng Hoà', N'Quang Hoa Commune', 'quang-hoa', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60615123', N'Quảng Sơn', N'Quang Son', N'Xã Quảng Sơn', N'Quang Son Commune', 'quang-son', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'60617124', N'Quảng Trực', N'Quang Truc', N'Xã Quảng Trực', N'Quang Truc Commune', 'quang-truc', 8, N'703', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80103001', N'Hưng Điền', N'Hung Dien', N'Xã Hưng Điền', N'Hung Dien Commune', 'hung-dien', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80103002', N'Vĩnh Thạnh', N'Vinh Thanh', N'Xã Vĩnh Thạnh', N'Vinh Thanh Commune', 'vinh-thanh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80103003', N'Tân Hưng', N'Tan Hung', N'Xã Tân Hưng', N'Tan Hung Commune', 'tan-hung', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80103004', N'Vĩnh Châu', N'Vinh Chau', N'Xã Vĩnh Châu', N'Vinh Chau Commune', 'vinh-chau', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80105005', N'Tuyên Bình', N'Tuyen Binh', N'Xã Tuyên Bình', N'Tuyen Binh Commune', 'tuyen-binh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80105006', N'Vĩnh Hưng', N'Vinh Hung', N'Xã Vĩnh Hưng', N'Vinh Hung Commune', 'vinh-hung', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80105007', N'Khánh Hưng', N'Khanh Hung', N'Xã Khánh Hưng', N'Khanh Hung Commune', 'khanh-hung', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80129008', N'Tuyên Thạnh', N'Tuyen Thanh', N'Xã Tuyên Thạnh', N'Tuyen Thanh Commune', 'tuyen-thanh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80129009', N'Bình Hiệp', N'Binh Hiep', N'Xã Bình Hiệp', N'Binh Hiep Commune', 'binh-hiep', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80129010', N'Kiến Tường', N'Kien Tuong', N'Phường Kiến Tường', N'Kien Tuong Ward', 'kien-tuong', 7, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80107011', N'Bình Hoà', N'Binh Hoa', N'Xã Bình Hoà', N'Binh Hoa Commune', 'binh-hoa', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80107012', N'Mộc Hoá', N'Moc Hoa', N'Xã Mộc Hoá', N'Moc Hoa Commune', 'moc-hoa', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80109013', N'Hậu Thạnh', N'Hau Thanh', N'Xã Hậu Thạnh', N'Hau Thanh Commune', 'hau-thanh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80109014', N'Nhơn Hoà Lập', N'Nhon Hoa Lap', N'Xã Nhơn Hoà Lập', N'Nhon Hoa Lap Commune', 'nhon-hoa-lap', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80109015', N'Nhơn Ninh', N'Nhon Ninh', N'Xã Nhơn Ninh', N'Nhon Ninh Commune', 'nhon-ninh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80109016', N'Tân Thạnh', N'Tan Thanh', N'Xã Tân Thạnh', N'Tan Thanh Commune', 'tan-thanh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80111017', N'Bình Thành', N'Binh Thanh', N'Xã Bình Thành', N'Binh Thanh Commune', 'binh-thanh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80111018', N'Thạnh Phước', N'Thanh Phuoc', N'Xã Thạnh Phước', N'Thanh Phuoc Commune', 'thanh-phuoc', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80111019', N'Thạnh Hóa', N'Thanh Hoa', N'Xã Thạnh Hóa', N'Thanh Hoa Commune', 'thanh-hoa', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80111020', N'Tân Tây', N'Tan Tay', N'Xã Tân Tây', N'Tan Tay Commune', 'tan-tay', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80119021', N'Thủ Thừa', N'Thu Thua', N'Xã Thủ Thừa', N'Thu Thua Commune', 'thu-thua', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80119022', N'Mỹ An', N'My An', N'Xã Mỹ An', N'My An Commune', 'my-an', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80119023', N'Mỹ Thạnh', N'My Thanh', N'Xã Mỹ Thạnh', N'My Thanh Commune', 'my-thanh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80119024', N'Tân Long', N'Tan Long', N'Xã Tân Long', N'Tan Long Commune', 'tan-long', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80113025', N'Mỹ Quý', N'My Quy', N'Xã Mỹ Quý', N'My Quy Commune', 'my-quy', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80113026', N'Đông Thành', N'Dong Thanh', N'Xã Đông Thành', N'Dong Thanh Commune', 'dong-thanh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80113027', N'Đức Huệ', N'Duc Hue', N'Xã Đức Huệ', N'Duc Hue Commune', 'duc-hue', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80115028', N'An Ninh', N'An Ninh', N'Xã An Ninh', N'An Ninh Commune', 'an-ninh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80115029', N'Hiệp Hoà', N'Hiep Hoa', N'Xã Hiệp Hoà', N'Hiep Hoa Commune', 'hiep-hoa', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80115030', N'Hậu Nghĩa', N'Hau Nghia', N'Xã Hậu Nghĩa', N'Hau Nghia Commune', 'hau-nghia', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80115031', N'Hoà Khánh', N'Hoa Khanh', N'Xã Hoà Khánh', N'Hoa Khanh Commune', 'hoa-khanh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80115032', N'Đức Lập', N'Duc Lap', N'Xã Đức Lập', N'Duc Lap Commune', 'duc-lap', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80115033', N'Mỹ Hạnh', N'My Hanh', N'Xã Mỹ Hạnh', N'My Hanh Commune', 'my-hanh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80115034', N'Đức Hoà', N'Duc Hoa', N'Xã Đức Hoà', N'Duc Hoa Commune', 'duc-hoa', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80117035', N'Thạnh Lợi', N'Thanh Loi', N'Xã Thạnh Lợi', N'Thanh Loi Commune', 'thanh-loi', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80117036', N'Bình Đức', N'Binh Duc', N'Xã Bình Đức', N'Binh Duc Commune', 'binh-duc', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80117037', N'Lương Hoà', N'Luong Hoa', N'Xã Lương Hoà', N'Luong Hoa Commune', 'luong-hoa', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80117038', N'Bến Lức', N'Ben Luc', N'Xã Bến Lức', N'Ben Luc Commune', 'ben-luc', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80117039', N'Mỹ Yên', N'My Yen', N'Xã Mỹ Yên', N'My Yen Commune', 'my-yen', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80125040', N'Long Cang', N'Long Cang', N'Xã Long Cang', N'Long Cang Commune', 'long-cang', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80125041', N'Rạch Kiến', N'Rach Kien', N'Xã Rạch Kiến', N'Rach Kien Commune', 'rach-kien', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80125042', N'Mỹ Lệ', N'My Le', N'Xã Mỹ Lệ', N'My Le Commune', 'my-le', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80125043', N'Tân Lân', N'Tan Lan', N'Xã Tân Lân', N'Tan Lan Commune', 'tan-lan', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80125044', N'Cần Đước', N'Can Duoc', N'Xã Cần Đước', N'Can Duoc Commune', 'can-duoc', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80125045', N'Long Hựu', N'Long Huu', N'Xã Long Hựu', N'Long Huu Commune', 'long-huu', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80127046', N'Phước Lý', N'Phuoc Ly', N'Xã Phước Lý', N'Phuoc Ly Commune', 'phuoc-ly', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80127047', N'Mỹ Lộc', N'My Loc', N'Xã Mỹ Lộc', N'My Loc Commune', 'my-loc', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80127048', N'Cần Giuộc', N'Can Giuoc', N'Xã Cần Giuộc', N'Can Giuoc Commune', 'can-giuoc', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80127049', N'Phước Vĩnh Tây', N'Phuoc Vinh Tay', N'Xã Phước Vĩnh Tây', N'Phuoc Vinh Tay Commune', 'phuoc-vinh-tay', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80127050', N'Tân Tập', N'Tan Tap', N'Xã Tân Tập', N'Tan Tap Commune', 'tan-tap', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80123051', N'Vàm Cỏ', N'Vam Cỏ', N'Xã Vàm Cỏ', N'Vam Cỏ Commune', 'vam-co', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80123052', N'Tân Trụ', N'Tan Tru', N'Xã Tân Trụ', N'Tan Tru Commune', 'tan-tru', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80123053', N'Nhựt Tảo', N'Nhut Tao', N'Xã Nhựt Tảo', N'Nhut Tao Commune', 'nhut-tao', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80121054', N'Thuận Mỹ', N'Thuan My', N'Xã Thuận Mỹ', N'Thuan My Commune', 'thuan-my', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80121055', N'An Lục Long', N'An Luc Long', N'Xã An Lục Long', N'An Luc Long Commune', 'an-luc-long', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80121056', N'Tầm Vu', N'Tam Vu', N'Xã Tầm Vu', N'Tam Vu Commune', 'tam-vu', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80121057', N'Vĩnh Công', N'Vinh Cong', N'Xã Vĩnh Công', N'Vinh Cong Commune', 'vinh-cong', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80101058', N'Long An', N'Long An', N'Phường Long An', N'Long An Ward', 'long-an', 7, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80101059', N'Tân An', N'Tan An', N'Phường Tân An', N'Tan An Ward', 'tan-an', 7, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80101060', N'Khánh Hậu', N'Khanh Hau', N'Phường Khánh Hậu', N'Khanh Hau Ward', 'khanh-hau', 7, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70901061', N'Tân Ninh', N'Tan Ninh', N'Phường Tân Ninh', N'Tan Ninh Ward', 'tan-ninh', 7, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70901062', N'Bình Minh', N'Binh Minh', N'Phường Bình Minh', N'Binh Minh Ward', 'binh-minh', 7, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70907063', N'Ninh Thạnh', N'Ninh Thanh', N'Phường Ninh Thạnh', N'Ninh Thanh Ward', 'ninh-thanh', 7, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70911064', N'Long Hoa', N'Long Hoa', N'Phường Long Hoa', N'Long Hoa Ward', 'long-hoa', 7, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70911065', N'Hoà Thành', N'Hoa Thanh', N'Phường Hoà Thành', N'Hoa Thanh Ward', 'hoa-thanh', 7, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70911066', N'Thanh Điền', N'Thanh Dien', N'Phường Thanh Điền', N'Thanh Dien Ward', 'thanh-dien', 7, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70917067', N'Trảng Bàng', N'Trang Bang', N'Phường Trảng Bàng', N'Trang Bang Ward', 'trang-bang', 7, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70917068', N'An Tịnh', N'An Tinh', N'Phường An Tịnh', N'An Tinh Ward', 'an-tinh', 7, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70915069', N'Gò Dầu', N'Go Dau', N'Phường Gò Dầu', N'Go Dau Ward', 'go-dau', 7, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70915070', N'Gia Lộc', N'Gia Loc', N'Phường Gia Lộc', N'Gia Loc Ward', 'gia-loc', 7, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70917071', N'Hưng Thuận', N'Hung Thuan', N'Xã Hưng Thuận', N'Hung Thuan Commune', 'hung-thuan', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70917072', N'Phước Chỉ', N'Phuoc Chi', N'Xã Phước Chỉ', N'Phuoc Chi Commune', 'phuoc-chi', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70915073', N'Thạnh Đức', N'Thanh Duc', N'Xã Thạnh Đức', N'Thanh Duc Commune', 'thanh-duc', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70915074', N'Phước Thạnh', N'Phuoc Thanh', N'Xã Phước Thạnh', N'Phuoc Thanh Commune', 'phuoc-thanh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70915075', N'Truông Mít', N'Truong Mit', N'Xã Truông Mít', N'Truong Mit Commune', 'truong-mit', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70907076', N'Lộc Ninh', N'Loc Ninh', N'Xã Lộc Ninh', N'Loc Ninh Commune', 'loc-ninh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70907077', N'Cầu Khởi', N'Cau Khoi', N'Xã Cầu Khởi', N'Cau Khoi Commune', 'cau-khoi', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70907078', N'Dương Minh Châu', N'Duong Minh Chau', N'Xã Dương Minh Châu', N'Duong Minh Chau Commune', 'duong-minh-chau', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70905079', N'Tân Đông', N'Tan Dong', N'Xã Tân Đông', N'Tan Dong Commune', 'tan-dong', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70905080', N'Tân Châu', N'Tan Chau', N'Xã Tân Châu', N'Tan Chau Commune', 'tan-chau', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70905081', N'Tân Phú', N'Tan Phu', N'Xã Tân Phú', N'Tan Phu Commune', 'tan-phu', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70905082', N'Tân Hội', N'Tan Hoi', N'Xã Tân Hội', N'Tan Hoi Commune', 'tan-hoi', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70905083', N'Tân Thành', N'Tan Thanh', N'Xã Tân Thành', N'Tan Thanh Commune', 'tan-thanh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70905084', N'Tân Hoà', N'Tan Hoa', N'Xã Tân Hoà', N'Tan Hoa Commune', 'tan-hoa', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70903085', N'Tân Lập', N'Tan Lap', N'Xã Tân Lập', N'Tan Lap Commune', 'tan-lap', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70903086', N'Tân Biên', N'Tan Bien', N'Xã Tân Biên', N'Tan Bien Commune', 'tan-bien', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70903087', N'Thạnh Bình', N'Thanh Binh', N'Xã Thạnh Bình', N'Thanh Binh Commune', 'thanh-binh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70903088', N'Trà Vong', N'Tra Vong', N'Xã Trà Vong', N'Tra Vong Commune', 'tra-vong', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70909089', N'Phước Vinh', N'Phuoc Vinh', N'Xã Phước Vinh', N'Phuoc Vinh Commune', 'phuoc-vinh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70909090', N'Hoà Hội', N'Hoa Hoi', N'Xã Hoà Hội', N'Hoa Hoi Commune', 'hoa-hoi', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70909091', N'Ninh Điền', N'Ninh Dien', N'Xã Ninh Điền', N'Ninh Dien Commune', 'ninh-dien', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70909092', N'Châu Thành', N'Chau Thanh', N'Xã Châu Thành', N'Chau Thanh Commune', 'chau-thanh', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70909093', N'Hảo Đước', N'Hao Duoc', N'Xã Hảo Đước', N'Hao Duoc Commune', 'hao-duoc', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70913094', N'Long Chữ', N'Long Chu', N'Xã Long Chữ', N'Long Chu Commune', 'long-chu', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70913095', N'Long Thuận', N'Long Thuan', N'Xã Long Thuận', N'Long Thuan Commune', 'long-thuan', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70913096', N'Bến Cầu', N'Ben Cau', N'Xã Bến Cầu', N'Ben Cau Commune', 'ben-cau', 8, N'709', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71301001', N'Biên Hoà', N'Bien Hoa', N'Phường Biên Hoà', N'Bien Hoa Ward', 'bien-hoa', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71301002', N'Trấn Biên', N'Tran Bien', N'Phường Trấn Biên', N'Tran Bien Ward', 'tran-bien', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71301003', N'Tam Hiệp', N'Tam Hiep', N'Phường Tam Hiệp', N'Tam Hiep Ward', 'tam-hiep', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71301004', N'Long Bình', N'Long Binh', N'Phường Long Bình', N'Long Binh Ward', 'long-binh', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71301005', N'Trảng Dài', N'Trang Dai', N'Phường Trảng Dài', N'Trang Dai Ward', 'trang-dai', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71301006', N'Hố Nai', N'Ho Nai', N'Phường Hố Nai', N'Ho Nai Ward', 'ho-nai', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71301007', N'Long Hưng', N'Long Hung', N'Phường Long Hưng', N'Long Hung Ward', 'long-hung', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71317008', N'Đại Phước', N'Dai Phuoc', N'Xã Đại Phước', N'Dai Phuoc Commune', 'dai-phuoc', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71317009', N'Nhơn Trạch', N'Nhon Trach', N'Xã Nhơn Trạch', N'Nhon Trach Commune', 'nhon-trach', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71317010', N'Phước An', N'Phuoc An', N'Xã Phước An', N'Phuoc An Commune', 'phuoc-an', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71315011', N'Phước Thái', N'Phuoc Thai', N'Xã Phước Thái', N'Phuoc Thai Commune', 'phuoc-thai', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71315012', N'Long Phước', N'Long Phuoc', N'Xã Long Phước', N'Long Phuoc Commune', 'long-phuoc', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71315013', N'Bình An', N'Binh An', N'Xã Bình An', N'Binh An Commune', 'binh-an', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71315014', N'Long Thành', N'Long Thanh', N'Xã Long Thành', N'Long Thanh Commune', 'long-thanh', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71315015', N'An Phước', N'An Phuoc', N'Xã An Phước', N'An Phuoc Commune', 'an-phuoc', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71308016', N'An Viễn', N'An Vien', N'Xã An Viễn', N'An Vien Commune', 'an-vien', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71308017', N'Bình Minh', N'Binh Minh', N'Xã Bình Minh', N'Binh Minh Commune', 'binh-minh', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71308018', N'Trảng Bom', N'Trang Bom', N'Xã Trảng Bom', N'Trang Bom Commune', 'trang-bom', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71308019', N'Bàu Hàm', N'Bau Ham', N'Xã Bàu Hàm', N'Bau Ham Commune', 'bau-ham', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71308020', N'Hưng Thịnh', N'Hung Thinh', N'Xã Hưng Thịnh', N'Hung Thinh Commune', 'hung-thinh', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71309021', N'Dầu Giây', N'Dau Giay', N'Xã Dầu Giây', N'Dau Giay Commune', 'dau-giay', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71309022', N'Gia Kiệm', N'Gia Kiem', N'Xã Gia Kiệm', N'Gia Kiem Commune', 'gia-kiem', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71305023', N'Thống Nhất', N'Thong Nhat', N'Xã Thống Nhất', N'Thong Nhat Commune', 'thong-nhat', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71302024', N'Bình Lộc', N'Binh Loc', N'Phường Bình Lộc', N'Binh Loc Ward', 'binh-loc', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71302025', N'Bảo Vinh', N'Bao Vinh', N'Phường Bảo Vinh', N'Bao Vinh Ward', 'bao-vinh', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71302026', N'Xuân Lập', N'Xuan Lap', N'Phường Xuân Lập', N'Xuan Lap Ward', 'xuan-lap', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71302027', N'Long Khánh', N'Long Khanh', N'Phường Long Khánh', N'Long Khanh Ward', 'long-khanh', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71302028', N'Hàng Gòn', N'Hang Gon', N'Phường Hàng Gòn', N'Hang Gon Ward', 'hang-gon', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71311029', N'Xuân Quế', N'Xuan Que', N'Xã Xuân Quế', N'Xuan Que Commune', 'xuan-que', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71311030', N'Xuân Đường', N'Xuan Duong', N'Xã Xuân Đường', N'Xuan Duong Commune', 'xuan-duong', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71311031', N'Cẩm Mỹ', N'Cam My', N'Xã Cẩm Mỹ', N'Cam My Commune', 'cam-my', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71311032', N'Sông Ray', N'Song Ray', N'Xã Sông Ray', N'Song Ray Commune', 'song-ray', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71311033', N'Xuân Đông', N'Xuan Dong', N'Xã Xuân Đông', N'Xuan Dong Commune', 'xuan-dong', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71313034', N'Xuân Định', N'Xuan Dinh', N'Xã Xuân Định', N'Xuan Dinh Commune', 'xuan-dinh', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71313035', N'Xuân Phú', N'Xuan Phu', N'Xã Xuân Phú', N'Xuan Phu Commune', 'xuan-phu', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71313036', N'Xuân Lộc', N'Xuan Loc', N'Xã Xuân Lộc', N'Xuan Loc Commune', 'xuan-loc', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71313037', N'Xuân Hoà', N'Xuan Hoa', N'Xã Xuân Hoà', N'Xuan Hoa Commune', 'xuan-hoa', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71313038', N'Xuân Thành', N'Xuan Thanh', N'Xã Xuân Thành', N'Xuan Thanh Commune', 'xuan-thanh', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71313039', N'Xuân Bắc', N'Xuan Bac', N'Xã Xuân Bắc', N'Xuan Bac Commune', 'xuan-bac', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71305040', N'La Ngà', N'La Nga', N'Xã La Ngà', N'La Nga Commune', 'la-nga', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71305041', N'Định Quán', N'Dinh Quan', N'Xã Định Quán', N'Dinh Quan Commune', 'dinh-quan', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71305042', N'Phú Vinh', N'Phu Vinh', N'Xã Phú Vinh', N'Phu Vinh Commune', 'phu-vinh', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71305043', N'Phú Hoà', N'Phu Hoa', N'Xã Phú Hoà', N'Phu Hoa Commune', 'phu-hoa', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71303044', N'Tà Lài', N'Ta Lai', N'Xã Tà Lài', N'Ta Lai Commune', 'ta-lai', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71303045', N'Nam Cát Tiên', N'Nam Cat Tien', N'Xã Nam Cát Tiên', N'Nam Cat Tien Commune', 'nam-cat-tien', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71303046', N'Tân Phú', N'Tan Phu', N'Xã Tân Phú', N'Tan Phu Commune', 'tan-phu', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71303047', N'Phú Lâm', N'Phu Lam', N'Xã Phú Lâm', N'Phu Lam Commune', 'phu-lam', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71307048', N'Trị An', N'Tri An', N'Xã Trị An', N'Tri An Commune', 'tri-an', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71307049', N'Tân An', N'Tan An', N'Xã Tân An', N'Tan An Commune', 'tan-an', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71307050', N'Tân Triều', N'Tan Trieu', N'Phường Tân Triều', N'Tan Trieu Ward', 'tan-trieu', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70710051', N'Minh Hưng', N'Minh Hung', N'Phường Minh Hưng', N'Minh Hung Ward', 'minh-hung', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70710052', N'Chơn Thành', N'Chon Thanh', N'Phường Chơn Thành', N'Chon Thanh Ward', 'chon-thanh', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70710053', N'Nha Bích', N'Nha Bich', N'Xã Nha Bích', N'Nha Bich Commune', 'nha-bich', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70713054', N'Tân Quan', N'Tan Quan', N'Xã Tân Quan', N'Tan Quan Commune', 'tan-quan', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70713055', N'Tân Hưng', N'Tan Hung', N'Xã Tân Hưng', N'Tan Hung Commune', 'tan-hung', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70713056', N'Tân Khai', N'Tan Khai', N'Xã Tân Khai', N'Tan Khai Commune', 'tan-khai', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70713057', N'Minh Đức', N'Minh Duc', N'Xã Minh Đức', N'Minh Duc Commune', 'minh-duc', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70709058', N'Bình Long', N'Binh Long', N'Phường Bình Long', N'Binh Long Ward', 'binh-long', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70709059', N'An Lộc', N'An Loc', N'Phường An Lộc', N'An Loc Ward', 'an-loc', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70705060', N'Lộc Thành', N'Loc Thanh', N'Xã Lộc Thành', N'Loc Thanh Commune', 'loc-thanh', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70705061', N'Lộc Ninh', N'Loc Ninh', N'Xã Lộc Ninh', N'Loc Ninh Commune', 'loc-ninh', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70705062', N'Lộc Hưng', N'Loc Hung', N'Xã Lộc Hưng', N'Loc Hung Commune', 'loc-hung', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70705063', N'Lộc Tấn', N'Loc Tan', N'Xã Lộc Tấn', N'Loc Tan Commune', 'loc-tan', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70705064', N'Lộc Thạnh', N'Loc Thanh', N'Xã Lộc Thạnh', N'Loc Thanh Commune', 'loc-thanh', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70705065', N'Lộc Quang', N'Loc Quang', N'Xã Lộc Quang', N'Loc Quang Commune', 'loc-quang', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70706066', N'Tân Tiến', N'Tan Tien', N'Xã Tân Tiến', N'Tan Tien Commune', 'tan-tien', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70706067', N'Thiện Hưng', N'Thien Hung', N'Xã Thiện Hưng', N'Thien Hung Commune', 'thien-hung', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70706068', N'Hưng Phước', N'Hung Phuoc', N'Xã Hưng Phước', N'Hung Phuoc Commune', 'hung-phuoc', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70715069', N'Phú Nghĩa', N'Phu Nghia', N'Xã Phú Nghĩa', N'Phu Nghia Commune', 'phu-nghia', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70715070', N'Đa Kia', N'Da Kia', N'Xã Đa Kia', N'Da Kia Commune', 'da-kia', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70703071', N'Phước Bình', N'Phuoc Binh', N'Phường Phước Bình', N'Phuoc Binh Ward', 'phuoc-binh', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70703072', N'Phước Long', N'Phuoc Long', N'Phường Phước Long', N'Phuoc Long Ward', 'phuoc-long', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70716073', N'Bình Tân', N'Binh Tan', N'Xã Bình Tân', N'Binh Tan Commune', 'binh-tan', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70716074', N'Long Hà', N'Long Ha', N'Xã Long Hà', N'Long Ha Commune', 'long-ha', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70716075', N'Phú Riềng', N'Phu Rieng', N'Xã Phú Riềng', N'Phu Rieng Commune', 'phu-rieng', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70716076', N'Phú Trung', N'Phu Trung', N'Xã Phú Trung', N'Phu Trung Commune', 'phu-trung', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70711077', N'Đồng Xoài', N'Dong Xoai', N'Phường Đồng Xoài', N'Dong Xoai Ward', 'dong-xoai', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70711078', N'Bình Phước', N'Binh Phuoc', N'Phường Bình Phước', N'Binh Phuoc Ward', 'binh-phuoc', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70701079', N'Thuận Lợi', N'Thuan Loi', N'Xã Thuận Lợi', N'Thuan Loi Commune', 'thuan-loi', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70701080', N'Đồng Tâm', N'Dong Tam', N'Xã Đồng Tâm', N'Dong Tam Commune', 'dong-tam', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70701081', N'Tân Lợi', N'Tan Loi', N'Xã Tân Lợi', N'Tan Loi Commune', 'tan-loi', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70701082', N'Đồng Phú', N'Dong Phu', N'Xã Đồng Phú', N'Dong Phu Commune', 'dong-phu', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70707083', N'Phước Sơn', N'Phuoc Son', N'Xã Phước Sơn', N'Phuoc Son Commune', 'phuoc-son', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70707084', N'Nghĩa Trung', N'Nghia Trung', N'Xã Nghĩa Trung', N'Nghia Trung Commune', 'nghia-trung', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70707085', N'Bù Đăng', N'Bu Dang', N'Xã Bù Đăng', N'Bu Dang Commune', 'bu-dang', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70707086', N'Thọ Sơn', N'Tho Son', N'Xã Thọ Sơn', N'Tho Son Commune', 'tho-son', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70707087', N'Đak Nhau', N'Dak Nhau', N'Xã Đak Nhau', N'Dak Nhau Commune', 'dak-nhau', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70707088', N'Bom Bo', N'Bom Bo', N'Xã Bom Bo', N'Bom Bo Commune', 'bom-bo', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71301089', N'Tam Phước', N'Tam Phuoc', N'Phường Tam Phước', N'Tam Phuoc Ward', 'tam-phuoc', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71301090', N'Phước Tân', N'Phuoc Tan', N'Phường Phước Tân', N'Phuoc Tan Ward', 'phuoc-tan', 7, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71305091', N'Thanh Sơn', N'Thanh Son', N'Xã Thanh Sơn', N'Thanh Son Commune', 'thanh-son', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71303092', N'Đak Lua', N'Dak Lua', N'Xã Đak Lua', N'Dak Lua Commune', 'dak-lua', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71307093', N'Phú Lý', N'Phu Ly', N'Xã Phú Lý', N'Phu Ly Commune', 'phu-ly', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70715094', N'Bù Gia Mập', N'Bu Gia Map', N'Xã Bù Gia Mập', N'Bu Gia Map Commune', 'bu-gia-map', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70715095', N'Đăk Ơ', N'Dak O', N'Xã Đăk Ơ', N'Dak O Commune', 'dak-o', 8, N'713', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71701001', N'Vũng Tàu', N'Vung Tau', N'Phường Vũng Tàu', N'Vung Tau Ward', 'vung-tau', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71701002', N'Tam Thắng', N'Tam Thang', N'Phường Tam Thắng', N'Tam Thang Ward', 'tam-thang', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71701003', N'Rạch Dừa', N'Rach Dua', N'Phường Rạch Dừa', N'Rach Dua Ward', 'rach-dua', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71701004', N'Phước Thắng', N'Phuoc Thang', N'Phường Phước Thắng', N'Phuoc Thang Ward', 'phuoc-thang', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71703005', N'Bà Rịa', N'Ba Ria', N'Phường Bà Rịa', N'Ba Ria Ward', 'ba-ria', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71703006', N'Long Hương', N'Long Huong', N'Phường Long Hương', N'Long Huong Ward', 'long-huong', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71709007', N'Phú Mỹ', N'Phu My', N'Phường Phú Mỹ', N'Phu My Ward', 'phu-my', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71703008', N'Tam Long', N'Tam Long', N'Phường Tam Long', N'Tam Long Ward', 'tam-long', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71709009', N'Tân Thành', N'Tan Thanh', N'Phường Tân Thành', N'Tan Thanh Ward', 'tan-thanh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71709010', N'Tân Phước', N'Tan Phuoc', N'Phường Tân Phước', N'Tan Phuoc Ward', 'tan-phuoc', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71709011', N'Tân Hải', N'Tan Hai', N'Phường Tân Hải', N'Tan Hai Ward', 'tan-hai', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71709012', N'Châu Pha', N'Chau Pha', N'Xã Châu Pha', N'Chau Pha Commune', 'chau-pha', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71705013', N'Ngãi Giao', N'Ngai Giao', N'Xã Ngãi Giao', N'Ngai Giao Commune', 'ngai-giao', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71705014', N'Bình Giã', N'Binh Gia', N'Xã Bình Giã', N'Binh Gia Commune', 'binh-gia', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71705015', N'Kim Long', N'Kim Long', N'Xã Kim Long', N'Kim Long Commune', 'kim-long', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71705016', N'Châu Đức', N'Chau Duc', N'Xã Châu Đức', N'Chau Duc Commune', 'chau-duc', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71705017', N'Xuân Sơn', N'Xuan Son', N'Xã Xuân Sơn', N'Xuan Son Commune', 'xuan-son', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71705018', N'Nghĩa Thành', N'Nghia Thanh', N'Xã Nghĩa Thành', N'Nghia Thanh Commune', 'nghia-thanh', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71707019', N'Hồ Tràm', N'Ho Tram', N'Xã Hồ Tràm', N'Ho Tram Commune', 'ho-tram', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71707020', N'Xuyên Mộc', N'Xuyen Moc', N'Xã Xuyên Mộc', N'Xuyen Moc Commune', 'xuyen-moc', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71707021', N'Hòa Hội', N'Hoa Hoi', N'Xã Hòa Hội', N'Hoa Hoi Commune', 'hoa-hoi', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71707022', N'Bàu Lâm', N'Bau Lam', N'Xã Bàu Lâm', N'Bau Lam Commune', 'bau-lam', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71712023', N'Phước Hải', N'Phuoc Hai', N'Xã Phước Hải', N'Phuoc Hai Commune', 'phuoc-hai', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71712024', N'Long Hải', N'Long Hai', N'Xã Long Hải', N'Long Hai Commune', 'long-hai', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71712025', N'Đất Đỏ', N'Dat Dỏ', N'Xã Đất Đỏ', N'Dat Dỏ Commune', 'dat-do', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71712026', N'Long Điền', N'Long Dien', N'Xã Long Điền', N'Long Dien Commune', 'long-dien', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71713027', N'khu Côn Đảo', N'khu Con Dao', N'Đặc khu Côn Đảo', N'khu Con Dao Đặc', 'khu-con-dao', 2, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71109028', N'Đông Hoà', N'Dong Hoa', N'Phường Đông Hoà', N'Dong Hoa Ward', 'dong-hoa', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71109029', N'Dĩ An', N'Di An', N'Phường Dĩ An', N'Di An Ward', 'di-an', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71109030', N'Tân Đông Hiệp', N'Tan Dong Hiep', N'Phường Tân Đông Hiệp', N'Tan Dong Hiep Ward', 'tan-dong-hiep', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71107031', N'Thuận An', N'Thuan An', N'Phường Thuận An', N'Thuan An Ward', 'thuan-an', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71107032', N'Thuận Giao', N'Thuan Giao', N'Phường Thuận Giao', N'Thuan Giao Ward', 'thuan-giao', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71107033', N'Bình Hoà', N'Binh Hoa', N'Phường Bình Hoà', N'Binh Hoa Ward', 'binh-hoa', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71107034', N'Lái Thiêu', N'Lai Thieu', N'Phường Lái Thiêu', N'Lai Thieu Ward', 'lai-thieu', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71107035', N'An Phú', N'An Phu', N'Phường An Phú', N'An Phu Ward', 'an-phu', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71101036', N'Bình Dương', N'Binh Duong', N'Phường Bình Dương', N'Binh Duong Ward', 'binh-duong', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71101037', N'Chánh Hiệp', N'Chanh Hiep', N'Phường Chánh Hiệp', N'Chanh Hiep Ward', 'chanh-hiep', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71101038', N'Thủ Dầu Một', N'Thu Dau Mot', N'Phường Thủ Dầu Một', N'Thu Dau Mot Ward', 'thu-dau-mot', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71101039', N'Phú Lợi', N'Phu Loi', N'Phường Phú Lợi', N'Phu Loi Ward', 'phu-loi', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71105040', N'Vĩnh Tân', N'Vinh Tan', N'Phường Vĩnh Tân', N'Vinh Tan Ward', 'vinh-tan', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71105041', N'Bình Cơ', N'Binh Co', N'Phường Bình Cơ', N'Binh Co Ward', 'binh-co', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71105042', N'Tân Uyên', N'Tan Uyen', N'Phường Tân Uyên', N'Tan Uyen Ward', 'tan-uyen', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71105043', N'Tân Hiệp', N'Tan Hiep', N'Phường Tân Hiệp', N'Tan Hiep Ward', 'tan-hiep', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71105044', N'Tân Khánh', N'Tan Khanh', N'Phường Tân Khánh', N'Tan Khanh Ward', 'tan-khanh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71103045', N'Hoà Lợi', N'Hoa Loi', N'Phường Hoà Lợi', N'Hoa Loi Ward', 'hoa-loi', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71101046', N'Phú An', N'Phu An', N'Phường Phú An', N'Phu An Ward', 'phu-an', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71113047', N'Tây Nam', N'Tay Nam', N'Phường Tây Nam', N'Tay Nam Ward', 'tay-nam', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71115048', N'Long Nguyên', N'Long Nguyen', N'Phường Long Nguyên', N'Long Nguyen Ward', 'long-nguyen', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71115049', N'Bến Cát', N'Ben Cat', N'Phường Bến Cát', N'Ben Cat Ward', 'ben-cat', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71115050', N'Chánh Phú Hoà', N'Chanh Phu Hoa', N'Phường Chánh Phú Hoà', N'Chanh Phu Hoa Ward', 'chanh-phu-hoa', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71117051', N'Bắc Tân Uyên', N'Bac Tan Uyen', N'Xã Bắc Tân Uyên', N'Bac Tan Uyen Commune', 'bac-tan-uyen', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71117052', N'Thường Tân', N'Thuong Tan', N'Xã Thường Tân', N'Thuong Tan Commune', 'thuong-tan', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71111053', N'An Long', N'An Long', N'Xã An Long', N'An Long Commune', 'an-long', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71111054', N'Phước Thành', N'Phuoc Thanh', N'Xã Phước Thành', N'Phuoc Thanh Commune', 'phuoc-thanh', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71111055', N'Phước Hoà', N'Phuoc Hoa', N'Xã Phước Hoà', N'Phuoc Hoa Commune', 'phuoc-hoa', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71111056', N'Phú Giáo', N'Phu Giao', N'Xã Phú Giáo', N'Phu Giao Commune', 'phu-giao', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71115057', N'Trừ Văn Thố', N'Tru Van Tho', N'Xã Trừ Văn Thố', N'Tru Van Tho Commune', 'tru-van-tho', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71115058', N'Bàu Bàng', N'Bau Bang', N'Xã Bàu Bàng', N'Bau Bang Commune', 'bau-bang', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71113059', N'Minh Thạnh', N'Minh Thanh', N'Xã Minh Thạnh', N'Minh Thanh Commune', 'minh-thanh', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71113060', N'Long Hoà', N'Long Hoa', N'Xã Long Hoà', N'Long Hoa Commune', 'long-hoa', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71113061', N'Dầu Tiếng', N'Dau Tieng', N'Xã Dầu Tiếng', N'Dau Tieng Commune', 'dau-tieng', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71113062', N'Thanh An', N'Thanh An', N'Xã Thanh An', N'Thanh An Commune', 'thanh-an', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70101063', N'Sài Gòn', N'Sai Gon', N'Phường Sài Gòn', N'Sai Gon Ward', 'sai-gon', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70101064', N'Tân Định', N'Tan Dinh', N'Phường Tân Định', N'Tan Dinh Ward', 'tan-dinh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70101065', N'Bến Thành', N'Ben Thanh', N'Phường Bến Thành', N'Ben Thanh Ward', 'ben-thanh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70101066', N'Cầu Ông Lãnh', N'Cau Ong Lanh', N'Phường Cầu Ông Lãnh', N'Cau Ong Lanh Ward', 'cau-ong-lanh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70105067', N'Bàn Cờ', N'Ban Co', N'Phường Bàn Cờ', N'Ban Co Ward', 'ban-co', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70105068', N'Xuân Hoà', N'Xuan Hoa', N'Phường Xuân Hoà', N'Xuan Hoa Ward', 'xuan-hoa', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70105069', N'Nhiêu Lộc', N'Nhieu Loc', N'Phường Nhiêu Lộc', N'Nhieu Loc Ward', 'nhieu-loc', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70107070', N'Xóm Chiếu', N'Xom Chieu', N'Phường Xóm Chiếu', N'Xom Chieu Ward', 'xom-chieu', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70107071', N'Khánh Hội', N'Khanh Hoi', N'Phường Khánh Hội', N'Khanh Hoi Ward', 'khanh-hoi', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70107072', N'Vĩnh Hội', N'Vinh Hoi', N'Phường Vĩnh Hội', N'Vinh Hoi Ward', 'vinh-hoi', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70109073', N'Chợ Quán', N'Cho Quan', N'Phường Chợ Quán', N'Cho Quan Ward', 'cho-quan', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70109074', N'An Đông', N'An Dong', N'Phường An Đông', N'An Dong Ward', 'an-dong', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70109075', N'Chợ Lớn', N'Cho Lon', N'Phường Chợ Lớn', N'Cho Lon Ward', 'cho-lon', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70111076', N'Bình Tây', N'Binh Tay', N'Phường Bình Tây', N'Binh Tay Ward', 'binh-tay', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70111077', N'Bình Tiên', N'Binh Tien', N'Phường Bình Tiên', N'Binh Tien Ward', 'binh-tien', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70111078', N'Bình Phú', N'Binh Phu', N'Phường Bình Phú', N'Binh Phu Ward', 'binh-phu', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70111079', N'Phú Lâm', N'Phu Lam', N'Phường Phú Lâm', N'Phu Lam Ward', 'phu-lam', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70113080', N'Tân Thuận', N'Tan Thuan', N'Phường Tân Thuận', N'Tan Thuan Ward', 'tan-thuan', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70113081', N'Phú Thuận', N'Phu Thuan', N'Phường Phú Thuận', N'Phu Thuan Ward', 'phu-thuan', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70113082', N'Tân Mỹ', N'Tan My', N'Phường Tân Mỹ', N'Tan My Ward', 'tan-my', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70113083', N'Tân Hưng', N'Tan Hung', N'Phường Tân Hưng', N'Tan Hung Ward', 'tan-hung', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70115084', N'Chánh Hưng', N'Chanh Hung', N'Phường Chánh Hưng', N'Chanh Hung Ward', 'chanh-hung', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70115085', N'Phú Định', N'Phu Dinh', N'Phường Phú Định', N'Phu Dinh Ward', 'phu-dinh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70115086', N'Bình Đông', N'Binh Dong', N'Phường Bình Đông', N'Binh Dong Ward', 'binh-dong', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70119087', N'Diên Hồng', N'Dien Hong', N'Phường Diên Hồng', N'Dien Hong Ward', 'dien-hong', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70119088', N'Vườn Lài', N'Vuon Lai', N'Phường Vườn Lài', N'Vuon Lai Ward', 'vuon-lai', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70119089', N'Hoà Hưng', N'Hoa Hung', N'Phường Hoà Hưng', N'Hoa Hung Ward', 'hoa-hung', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70121090', N'Minh Phụng', N'Minh Phung', N'Phường Minh Phụng', N'Minh Phung Ward', 'minh-phung', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70121091', N'Bình Thới', N'Binh Thoi', N'Phường Bình Thới', N'Binh Thoi Ward', 'binh-thoi', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70121092', N'Hoà Bình', N'Hoa Binh', N'Phường Hoà Bình', N'Hoa Binh Ward', 'hoa-binh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70121093', N'Phú Thọ', N'Phu Tho', N'Phường Phú Thọ', N'Phu Tho Ward', 'phu-tho', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70123094', N'Đông Hưng Thuận', N'Dong Hung Thuan', N'Phường Đông Hưng Thuận', N'Dong Hung Thuan Ward', 'dong-hung-thuan', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70123095', N'Trung Mỹ Tây', N'Trung My Tay', N'Phường Trung Mỹ Tây', N'Trung My Tay Ward', 'trung-my-tay', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70123096', N'Tân Thới Hiệp', N'Tan Thoi Hiep', N'Phường Tân Thới Hiệp', N'Tan Thoi Hiep Ward', 'tan-thoi-hiep', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70123097', N'Thới An', N'Thoi An', N'Phường Thới An', N'Thoi An Ward', 'thoi-an', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70123098', N'An Phú Đông', N'An Phu Dong', N'Phường An Phú Đông', N'An Phu Dong Ward', 'an-phu-dong', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70134099', N'An Lạc', N'An Lac', N'Phường An Lạc', N'An Lac Ward', 'an-lac', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70134100', N'Tân Tạo', N'Tan Tao', N'Phường Tân Tạo', N'Tan Tao Ward', 'tan-tao', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70134101', N'Bình Tân', N'Binh Tan', N'Phường Bình Tân', N'Binh Tan Ward', 'binh-tan', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70134102', N'Bình Trị Đông', N'Binh Tri Dong', N'Phường Bình Trị Đông', N'Binh Tri Dong Ward', 'binh-tri-dong', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70134103', N'Bình Hưng Hoà', N'Binh Hung Hoa', N'Phường Bình Hưng Hoà', N'Binh Hung Hoa Ward', 'binh-hung-hoa', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70129104', N'Gia Định', N'Gia Dinh', N'Phường Gia Định', N'Gia Dinh Ward', 'gia-dinh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70129105', N'Bình Thạnh', N'Binh Thanh', N'Phường Bình Thạnh', N'Binh Thanh Ward', 'binh-thanh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70129106', N'Bình Lợi Trung', N'Binh Loi Trung', N'Phường Bình Lợi Trung', N'Binh Loi Trung Ward', 'binh-loi-trung', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70129107', N'Thạnh Mỹ Tây', N'Thanh My Tay', N'Phường Thạnh Mỹ Tây', N'Thanh My Tay Ward', 'thanh-my-tay', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70129108', N'Bình Quới', N'Binh Quoi', N'Phường Bình Quới', N'Binh Quoi Ward', 'binh-quoi', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70125109', N'Hạnh Thông', N'Hanh Thong', N'Phường Hạnh Thông', N'Hanh Thong Ward', 'hanh-thong', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70125110', N'An Nhơn', N'An Nhon', N'Phường An Nhơn', N'An Nhon Ward', 'an-nhon', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70125111', N'Gò Vấp', N'Go Vap', N'Phường Gò Vấp', N'Go Vap Ward', 'go-vap', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70125112', N'An Hội Đông', N'An Hoi Dong', N'Phường An Hội Đông', N'An Hoi Dong Ward', 'an-hoi-dong', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70125113', N'Thông Tây Hội', N'Thong Tay Hoi', N'Phường Thông Tây Hội', N'Thong Tay Hoi Ward', 'thong-tay-hoi', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70125114', N'An Hội Tây', N'An Hoi Tay', N'Phường An Hội Tây', N'An Hoi Tay Ward', 'an-hoi-tay', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70131115', N'Đức Nhuận', N'Duc Nhuan', N'Phường Đức Nhuận', N'Duc Nhuan Ward', 'duc-nhuan', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70131116', N'Cầu Kiệu', N'Cau Kieu', N'Phường Cầu Kiệu', N'Cau Kieu Ward', 'cau-kieu', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70131117', N'Phú Nhuận', N'Phu Nhuan', N'Phường Phú Nhuận', N'Phu Nhuan Ward', 'phu-nhuan', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70127118', N'Tân Sơn Hoà', N'Tan Son Hoa', N'Phường Tân Sơn Hoà', N'Tan Son Hoa Ward', 'tan-son-hoa', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70127119', N'Tân Sơn Nhất', N'Tan Son Nhat', N'Phường Tân Sơn Nhất', N'Tan Son Nhat Ward', 'tan-son-nhat', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70127120', N'Tân Hoà', N'Tan Hoa', N'Phường Tân Hoà', N'Tan Hoa Ward', 'tan-hoa', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70127121', N'Bảy Hiền', N'Bay Hien', N'Phường Bảy Hiền', N'Bay Hien Ward', 'bay-hien', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70127122', N'Tân Bình', N'Tan Binh', N'Phường Tân Bình', N'Tan Binh Ward', 'tan-binh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70127123', N'Tân Sơn', N'Tan Son', N'Phường Tân Sơn', N'Tan Son Ward', 'tan-son', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70128124', N'Tây Thạnh', N'Tay Thanh', N'Phường Tây Thạnh', N'Tay Thanh Ward', 'tay-thanh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70128125', N'Tân Sơn Nhì', N'Tan Son Nhi', N'Phường Tân Sơn Nhì', N'Tan Son Nhi Ward', 'tan-son-nhi', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70128126', N'Phú Thọ Hoà', N'Phu Tho Hoa', N'Phường Phú Thọ Hoà', N'Phu Tho Hoa Ward', 'phu-tho-hoa', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70128127', N'Tân Phú', N'Tan Phu', N'Phường Tân Phú', N'Tan Phu Ward', 'tan-phu', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70128128', N'Phú Thạnh', N'Phu Thanh', N'Phường Phú Thạnh', N'Phu Thanh Ward', 'phu-thanh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70145129', N'Hiệp Bình', N'Hiep Binh', N'Phường Hiệp Bình', N'Hiep Binh Ward', 'hiep-binh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70145130', N'Thủ Đức', N'Thu Duc', N'Phường Thủ Đức', N'Thu Duc Ward', 'thu-duc', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70145131', N'Tam Bình', N'Tam Binh', N'Phường Tam Bình', N'Tam Binh Ward', 'tam-binh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70145132', N'Linh Xuân', N'Linh Xuan', N'Phường Linh Xuân', N'Linh Xuan Ward', 'linh-xuan', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70145133', N'Tăng Nhơn Phú', N'Tang Nhon Phu', N'Phường Tăng Nhơn Phú', N'Tang Nhon Phu Ward', 'tang-nhon-phu', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70145134', N'Long Bình', N'Long Binh', N'Phường Long Bình', N'Long Binh Ward', 'long-binh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70145135', N'Long Phước', N'Long Phuoc', N'Phường Long Phước', N'Long Phuoc Ward', 'long-phuoc', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70145136', N'Long Trường', N'Long Truong', N'Phường Long Trường', N'Long Truong Ward', 'long-truong', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70145137', N'Cát Lái', N'Cat Lai', N'Phường Cát Lái', N'Cat Lai Ward', 'cat-lai', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70145138', N'Bình Trưng', N'Binh Trung', N'Phường Bình Trưng', N'Binh Trung Ward', 'binh-trung', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70145139', N'Phước Long', N'Phuoc Long', N'Phường Phước Long', N'Phuoc Long Ward', 'phuoc-long', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70145140', N'An Khánh', N'An Khanh', N'Phường An Khánh', N'An Khanh Ward', 'an-khanh', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70139141', N'Vĩnh Lộc', N'Vinh Loc', N'Xã Vĩnh Lộc', N'Vinh Loc Commune', 'vinh-loc', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70139142', N'Tân Vĩnh Lộc', N'Tan Vinh Loc', N'Xã Tân Vĩnh Lộc', N'Tan Vinh Loc Commune', 'tan-vinh-loc', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70139143', N'Bình Lợi', N'Binh Loi', N'Xã Bình Lợi', N'Binh Loi Commune', 'binh-loi', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70139144', N'Tân Nhựt', N'Tan Nhut', N'Xã Tân Nhựt', N'Tan Nhut Commune', 'tan-nhut', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70139145', N'Bình Chánh', N'Binh Chanh', N'Xã Bình Chánh', N'Binh Chanh Commune', 'binh-chanh', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70139146', N'Hưng Long', N'Hung Long', N'Xã Hưng Long', N'Hung Long Commune', 'hung-long', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70139147', N'Bình Hưng', N'Binh Hung', N'Xã Bình Hưng', N'Binh Hung Commune', 'binh-hung', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70143148', N'Bình Khánh', N'Binh Khanh', N'Xã Bình Khánh', N'Binh Khanh Commune', 'binh-khanh', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70143149', N'An Thới Đông', N'An Thoi Dong', N'Xã An Thới Đông', N'An Thoi Dong Commune', 'an-thoi-dong', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70143150', N'Cần Giờ', N'Can Gio', N'Xã Cần Giờ', N'Can Gio Commune', 'can-gio', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70135151', N'Củ Chi', N'Cu Chi', N'Xã Củ Chi', N'Cu Chi Commune', 'cu-chi', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70135152', N'Tân An Hội', N'Tan An Hoi', N'Xã Tân An Hội', N'Tan An Hoi Commune', 'tan-an-hoi', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70135153', N'Thái Mỹ', N'Thai My', N'Xã Thái Mỹ', N'Thai My Commune', 'thai-my', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70135154', N'An Nhơn Tây', N'An Nhon Tay', N'Xã An Nhơn Tây', N'An Nhon Tay Commune', 'an-nhon-tay', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70135155', N'Nhuận Đức', N'Nhuan Duc', N'Xã Nhuận Đức', N'Nhuan Duc Commune', 'nhuan-duc', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70135156', N'Phú Hoà Đông', N'Phu Hoa Dong', N'Xã Phú Hoà Đông', N'Phu Hoa Dong Commune', 'phu-hoa-dong', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70135157', N'Bình Mỹ', N'Binh My', N'Xã Bình Mỹ', N'Binh My Commune', 'binh-my', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70137158', N'Đông Thạnh', N'Dong Thanh', N'Xã Đông Thạnh', N'Dong Thanh Commune', 'dong-thanh', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70137159', N'Hóc Môn', N'Hoc Mon', N'Xã Hóc Môn', N'Hoc Mon Commune', 'hoc-mon', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70137160', N'Xuân Thới Sơn', N'Xuan Thoi Son', N'Xã Xuân Thới Sơn', N'Xuan Thoi Son Commune', 'xuan-thoi-son', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70137161', N'Bà Điểm', N'Ba Diem', N'Xã Bà Điểm', N'Ba Diem Commune', 'ba-diem', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70141162', N'Nhà Bè', N'Nha Be', N'Xã Nhà Bè', N'Nha Be Commune', 'nha-be', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70141163', N'Hiệp Phước', N'Hiep Phuoc', N'Xã Hiệp Phước', N'Hiep Phuoc Commune', 'hiep-phuoc', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71701164', N'Long Sơn', N'Long Son', N'Xã Long Sơn', N'Long Son Commune', 'long-son', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71707165', N'Hòa Hiệp', N'Hoa Hiep', N'Xã Hòa Hiệp', N'Hoa Hiep Commune', 'hoa-hiep', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71707166', N'Bình Châu', N'Binh Chau', N'Xã Bình Châu', N'Binh Chau Commune', 'binh-chau', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'71103167', N'Thới Hoà', N'Thoi Hoa', N'Phường Thới Hoà', N'Thoi Hoa Ward', 'thoi-hoa', 7, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'70143168', N'Thạnh An', N'Thanh An', N'Xã Thạnh An', N'Thanh An Commune', 'thanh-an', 8, N'701', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81701037', N'Trà Vinh', N'Tra Vinh', N'Phường Trà Vinh', N'Tra Vinh Ward', 'tra-vinh', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80905001', N'Cái Nhum', N'Cai Nhum', N'Xã Cái Nhum', N'Cai Nhum Commune', 'cai-nhum', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81701036', N'Long Đức', N'Long Duc', N'Phường Long Đức', N'Long Duc Ward', 'long-duc', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80905002', N'Tân Long Hội', N'Tan Long Hoi', N'Xã Tân Long Hội', N'Tan Long Hoi Commune', 'tan-long-hoi', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81701038', N'Nguyệt Hóa', N'Nguyet Hoa', N'Phường Nguyệt Hóa', N'Nguyet Hoa Ward', 'nguyet-hoa', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80905003', N'Nhơn Phú', N'Nhon Phu', N'Xã Nhơn Phú', N'Nhon Phu Commune', 'nhon-phu', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81701039', N'Hòa Thuận', N'Hoa Thuan', N'Phường Hòa Thuận', N'Hoa Thuan Ward', 'hoa-thuan', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80905004', N'Bình Phước', N'Binh Phuoc', N'Xã Bình Phước', N'Binh Phuoc Commune', 'binh-phuoc', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81703042', N'Càng Long', N'Cang Long', N'Xã Càng Long', N'Cang Long Commune', 'cang-long', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80903005', N'An Bình', N'An Binh', N'Xã An Bình', N'An Binh Commune', 'an-binh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81703040', N'An Trường', N'An Truong', N'Xã An Trường', N'An Truong Commune', 'an-truong', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80903006', N'Long Hồ', N'Long Ho', N'Xã Long Hồ', N'Long Ho Commune', 'long-ho', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81703041', N'Tân An', N'Tan An', N'Xã Tân An', N'Tan An Commune', 'tan-an', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80903007', N'Phú Quới', N'Phu Quoi', N'Xã Phú Quới', N'Phu Quoi Commune', 'phu-quoi', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81703043', N'Nhị Long', N'Nhi Long', N'Xã Nhị Long', N'Nhi Long Commune', 'nhi-long', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80901008', N'Thanh Đức', N'Thanh Duc', N'Phường Thanh Đức', N'Thanh Duc Ward', 'thanh-duc', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81703044', N'Bình Phú', N'Binh Phu', N'Xã Bình Phú', N'Binh Phu Commune', 'binh-phu', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80901009', N'Long Châu', N'Long Chau', N'Phường Long Châu', N'Long Chau Ward', 'long-chau', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81705046', N'Châu Thành', N'Chau Thanh', N'Xã Châu Thành', N'Chau Thanh Commune', 'chau-thanh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80901010', N'Phước Hậu', N'Phuoc Hau', N'Phường Phước Hậu', N'Phuoc Hau Ward', 'phuoc-hau', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81705045', N'Song Lộc', N'Song Loc', N'Xã Song Lộc', N'Song Loc Commune', 'song-loc', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80901011', N'Tân Hạnh', N'Tan Hanh', N'Phường Tân Hạnh', N'Tan Hanh Ward', 'tan-hanh', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81705047', N'Hưng Mỹ', N'Hung My', N'Xã Hưng Mỹ', N'Hung My Commune', 'hung-my', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80901012', N'Tân Ngãi', N'Tan Ngai', N'Phường Tân Ngãi', N'Tan Ngai Ward', 'tan-ngai', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81705048', N'Hòa Minh', N'Hoa Minh', N'Xã Hòa Minh', N'Hoa Minh Commune', 'hoa-minh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80913013', N'Quới Thiện', N'Quoi Thien', N'Xã Quới Thiện', N'Quoi Thien Commune', 'quoi-thien', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81705049', N'Long Hòa', N'Long Hoa', N'Xã Long Hòa', N'Long Hoa Commune', 'long-hoa', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80913014', N'Trung Thành', N'Trung Thanh', N'Xã Trung Thành', N'Trung Thanh Commune', 'trung-thanh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81707050', N'Cầu Kè', N'Cau Ke', N'Xã Cầu Kè', N'Cau Ke Commune', 'cau-ke', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80913015', N'Trung Ngãi', N'Trung Ngai', N'Xã Trung Ngãi', N'Trung Ngai Commune', 'trung-ngai', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81707051', N'Phong Thạnh', N'Phong Thanh', N'Xã Phong Thạnh', N'Phong Thanh Commune', 'phong-thanh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80913016', N'Quới An', N'Quoi An', N'Xã Quới An', N'Quoi An Commune', 'quoi-an', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81707052', N'An Phú Tân', N'An Phu Tan', N'Xã An Phú Tân', N'An Phu Tan Commune', 'an-phu-tan', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80913017', N'Trung Hiệp', N'Trung Hiep', N'Xã Trung Hiệp', N'Trung Hiep Commune', 'trung-hiep', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81707053', N'Tam Ngãi', N'Tam Ngai', N'Xã Tam Ngãi', N'Tam Ngai Commune', 'tam-ngai', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80913018', N'Hiếu Phụng', N'Hieu Phung', N'Xã Hiếu Phụng', N'Hieu Phung Commune', 'hieu-phung', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81709056', N'Tiểu Cần', N'Tieu Can', N'Xã Tiểu Cần', N'Tieu Can Commune', 'tieu-can', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80913019', N'Hiếu Thành', N'Hieu Thanh', N'Xã Hiếu Thành', N'Hieu Thanh Commune', 'hieu-thanh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81709054', N'Tân Hòa', N'Tan Hoa', N'Xã Tân Hòa', N'Tan Hoa Commune', 'tan-hoa', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80911020', N'Lục Sỹ Thành', N'Luc Sy Thanh', N'Xã Lục Sỹ Thành', N'Luc Sy Thanh Commune', 'luc-sy-thanh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81709055', N'Hùng Hòa', N'Hung Hoa', N'Xã Hùng Hòa', N'Hung Hoa Commune', 'hung-hoa', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80911021', N'Trà Ôn', N'Tra On', N'Xã Trà Ôn', N'Tra On Commune', 'tra-on', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81709057', N'Tập Ngãi', N'Tap Ngai', N'Xã Tập Ngãi', N'Tap Ngai Commune', 'tap-ngai', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80911022', N'Trà Côn', N'Tra Con', N'Xã Trà Côn', N'Tra Con Commune', 'tra-con', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81711060', N'Cầu Ngang', N'Cau Ngang', N'Xã Cầu Ngang', N'Cau Ngang Commune', 'cau-ngang', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80911023', N'Vĩnh Xuân', N'Vinh Xuan', N'Xã Vĩnh Xuân', N'Vinh Xuan Commune', 'vinh-xuan', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81711058', N'Mỹ Long', N'My Long', N'Xã Mỹ Long', N'My Long Commune', 'my-long', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80911024', N'Hòa Bình', N'Hoa Binh', N'Xã Hòa Bình', N'Hoa Binh Commune', 'hoa-binh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81711059', N'Vinh Kim', N'Vinh Kim', N'Xã Vinh Kim', N'Vinh Kim Commune', 'vinh-kim', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80909025', N'Hòa Hiệp', N'Hoa Hiep', N'Xã Hòa Hiệp', N'Hoa Hiep Commune', 'hoa-hiep', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81711061', N'Nhị Trường', N'Nhi Truong', N'Xã Nhị Trường', N'Nhi Truong Commune', 'nhi-truong', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80909026', N'Tam Bình', N'Tam Binh', N'Xã Tam Bình', N'Tam Binh Commune', 'tam-binh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81711062', N'Hiệp Mỹ', N'Hiep My', N'Xã Hiệp Mỹ', N'Hiep My Commune', 'hiep-my', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80909027', N'Ngãi Tứ', N'Ngai Tu', N'Xã Ngãi Tứ', N'Ngai Tu Commune', 'ngai-tu', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81713066', N'Trà Cú', N'Tra Cu', N'Xã Trà Cú', N'Tra Cu Commune', 'tra-cu', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80909028', N'Song Phú', N'Song Phu', N'Xã Song Phú', N'Song Phu Commune', 'song-phu', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81713063', N'Lưu Nghiệp Anh', N'Luu Nghiep Anh', N'Xã Lưu Nghiệp Anh', N'Luu Nghiep Anh Commune', 'luu-nghiep-anh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80909029', N'Cái Ngang', N'Cai Ngang', N'Xã Cái Ngang', N'Cai Ngang Commune', 'cai-ngang', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81713064', N'Đại An', N'Dai An', N'Xã Đại An', N'Dai An Commune', 'dai-an', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80908030', N'Tân Quới', N'Tan Quoi', N'Xã Tân Quới', N'Tan Quoi Commune', 'tan-quoi', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81713065', N'Hàm Giang', N'Ham Giang', N'Xã Hàm Giang', N'Ham Giang Commune', 'ham-giang', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80908031', N'Tân Lược', N'Tan Luoc', N'Xã Tân Lược', N'Tan Luoc Commune', 'tan-luoc', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81713067', N'Long Hiệp', N'Long Hiep', N'Xã Long Hiệp', N'Long Hiep Commune', 'long-hiep', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80908032', N'Mỹ Thuận', N'My Thuan', N'Xã Mỹ Thuận', N'My Thuan Commune', 'my-thuan', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81713068', N'Tập Sơn', N'Tap Son', N'Xã Tập Sơn', N'Tap Son Commune', 'tap-son', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80907033', N'Bình Minh', N'Binh Minh', N'Phường Bình Minh', N'Binh Minh Ward', 'binh-minh', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81716069', N'Duyên Hải', N'Duyen Hai', N'Phường Duyên Hải', N'Duyen Hai Ward', 'duyen-hai', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80907034', N'Cái Vồn', N'Cai Von', N'Phường Cái Vồn', N'Cai Von Ward', 'cai-von', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81716070', N'Trường Long Hòa', N'Truong Long Hoa', N'Phường Trường Long Hòa', N'Truong Long Hoa Ward', 'truong-long-hoa', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80907035', N'Đông Thành', N'Dong Thanh', N'Phường Đông Thành', N'Dong Thanh Ward', 'dong-thanh', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81716071', N'Long Hữu', N'Long Huu', N'Xã Long Hữu', N'Long Huu Commune', 'long-huu', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81715072', N'Long Thành', N'Long Thanh', N'Xã Long Thành', N'Long Thanh Commune', 'long-thanh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81715073', N'Đông Hải', N'Dong Hai', N'Xã Đông Hải', N'Dong Hai Commune', 'dong-hai', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81715074', N'Long Vĩnh', N'Long Vinh', N'Xã Long Vĩnh', N'Long Vinh Commune', 'long-vinh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81715075', N'Đôn Châu', N'Don Chau', N'Xã Đôn Châu', N'Don Chau Commune', 'don-chau', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81715076', N'Ngũ Lạc', N'Ngu Lac', N'Xã Ngũ Lạc', N'Ngu Lac Commune', 'ngu-lac', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81101077', N'An Hội', N'An Hoi', N'Phường An Hội', N'An Hoi Ward', 'an-hoi', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81101078', N'Phú Khương', N'Phu Khuong', N'Phường Phú Khương', N'Phu Khuong Ward', 'phu-khuong', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81101079', N'Bến Tre', N'Ben Tre', N'Phường Bến Tre', N'Ben Tre Ward', 'ben-tre', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81101080', N'Sơn Đông', N'Son Dong', N'Phường Sơn Đông', N'Son Dong Ward', 'son-dong', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81103081', N'Phú Tân', N'Phu Tan', N'Phường Phú Tân', N'Phu Tan Ward', 'phu-tan', 7, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81103082', N'Phú Túc', N'Phu Tuc', N'Xã Phú Túc', N'Phu Tuc Commune', 'phu-tuc', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81103083', N'Giao Long', N'Giao Long', N'Xã Giao Long', N'Giao Long Commune', 'giao-long', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81103084', N'Tiên Thủy', N'Tien Thuy', N'Xã Tiên Thủy', N'Tien Thuy Commune', 'tien-thuy', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81103085', N'Tân Phú', N'Tan Phu', N'Xã Tân Phú', N'Tan Phu Commune', 'tan-phu', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81105086', N'Phú Phụng', N'Phu Phung', N'Xã Phú Phụng', N'Phu Phung Commune', 'phu-phung', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81105087', N'Chợ Lách', N'Cho Lach', N'Xã Chợ Lách', N'Cho Lach Commune', 'cho-lach', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81105088', N'Vĩnh Thành', N'Vinh Thanh', N'Xã Vĩnh Thành', N'Vinh Thanh Commune', 'vinh-thanh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81105089', N'Hưng Khánh Trung', N'Hung Khanh Trung', N'Xã Hưng Khánh Trung', N'Hung Khanh Trung Commune', 'hung-khanh-trung', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81108090', N'Phước Mỹ Trung', N'Phuoc My Trung', N'Xã Phước Mỹ Trung', N'Phuoc My Trung Commune', 'phuoc-my-trung', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81108091', N'Tân Thành Bình', N'Tan Thanh Binh', N'Xã Tân Thành Bình', N'Tan Thanh Binh Commune', 'tan-thanh-binh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81108092', N'Nhuận Phú Tân', N'Nhuan Phu Tan', N'Xã Nhuận Phú Tân', N'Nhuan Phu Tan Commune', 'nhuan-phu-tan', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81107093', N'Đồng Khởi', N'Dong Khoi', N'Xã Đồng Khởi', N'Dong Khoi Commune', 'dong-khoi', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81107094', N'Mỏ Cày', N'Mỏ Cay', N'Xã Mỏ Cày', N'Mỏ Cay Commune', 'mo-cay', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81107095', N'Thành Thới', N'Thanh Thoi', N'Xã Thành Thới', N'Thanh Thoi Commune', 'thanh-thoi', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81107096', N'An Định', N'An Dinh', N'Xã An Định', N'An Dinh Commune', 'an-dinh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81107097', N'Hương Mỹ', N'Huong My', N'Xã Hương Mỹ', N'Huong My Commune', 'huong-my', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81115098', N'Đại Điền', N'Dai Dien', N'Xã Đại Điền', N'Dai Dien Commune', 'dai-dien', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81115099', N'Quới Điền', N'Quoi Dien', N'Xã Quới Điền', N'Quoi Dien Commune', 'quoi-dien', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81115100', N'Thạnh Phú', N'Thanh Phu', N'Xã Thạnh Phú', N'Thanh Phu Commune', 'thanh-phu', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81115101', N'An Qui', N'An Qui', N'Xã An Qui', N'An Qui Commune', 'an-qui', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81115102', N'Thạnh Hải', N'Thanh Hai', N'Xã Thạnh Hải', N'Thanh Hai Commune', 'thanh-hai', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81115103', N'Thạnh Phong', N'Thanh Phong', N'Xã Thạnh Phong', N'Thanh Phong Commune', 'thanh-phong', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81113104', N'Tân Thủy', N'Tan Thuy', N'Xã Tân Thủy', N'Tan Thuy Commune', 'tan-thuy', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81113105', N'Bảo Thạnh', N'Bao Thanh', N'Xã Bảo Thạnh', N'Bao Thanh Commune', 'bao-thanh', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81113106', N'Ba Tri', N'Ba Tri', N'Xã Ba Tri', N'Ba Tri Commune', 'ba-tri', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81113107', N'Tân Xuân', N'Tan Xuan', N'Xã Tân Xuân', N'Tan Xuan Commune', 'tan-xuan', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81113108', N'Mỹ Chánh Hòa', N'My Chanh Hoa', N'Xã Mỹ Chánh Hòa', N'My Chanh Hoa Commune', 'my-chanh-hoa', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81113109', N'An Ngãi Trung', N'An Ngai Trung', N'Xã An Ngãi Trung', N'An Ngai Trung Commune', 'an-ngai-trung', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81113110', N'An Hiệp', N'An Hiep', N'Xã An Hiệp', N'An Hiep Commune', 'an-hiep', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81109111', N'Hưng Nhượng', N'Hung Nhuong', N'Xã Hưng Nhượng', N'Hung Nhuong Commune', 'hung-nhuong', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81109112', N'Giồng Trôm', N'Giong Trom', N'Xã Giồng Trôm', N'Giong Trom Commune', 'giong-trom', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81109113', N'Tân Hào', N'Tan Hao', N'Xã Tân Hào', N'Tan Hao Commune', 'tan-hao', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81109114', N'Phước Long', N'Phuoc Long', N'Xã Phước Long', N'Phuoc Long Commune', 'phuoc-long', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81109115', N'Lương Phú', N'Luong Phu', N'Xã Lương Phú', N'Luong Phu Commune', 'luong-phu', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81109116', N'Châu Hòa', N'Chau Hoa', N'Xã Châu Hòa', N'Chau Hoa Commune', 'chau-hoa', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81109117', N'Lương Hòa', N'Luong Hoa', N'Xã Lương Hòa', N'Luong Hoa Commune', 'luong-hoa', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81111118', N'Thới Thuận', N'Thoi Thuan', N'Xã Thới Thuận', N'Thoi Thuan Commune', 'thoi-thuan', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81111119', N'Thạnh Phước', N'Thanh Phuoc', N'Xã Thạnh Phước', N'Thanh Phuoc Commune', 'thanh-phuoc', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81111120', N'Bình Đại', N'Binh Dai', N'Xã Bình Đại', N'Binh Dai Commune', 'binh-dai', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81111121', N'Thạnh Trị', N'Thanh Tri', N'Xã Thạnh Trị', N'Thanh Tri Commune', 'thanh-tri', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81111122', N'Lộc Thuận', N'Loc Thuan', N'Xã Lộc Thuận', N'Loc Thuan Commune', 'loc-thuan', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81111123', N'Châu Hưng', N'Chau Hung', N'Xã Châu Hưng', N'Chau Hung Commune', 'chau-hung', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81111124', N'Phú Thuận', N'Phu Thuan', N'Xã Phú Thuận', N'Phu Thuan Commune', 'phu-thuan', 8, N'809', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80701001', N'Mỹ Tho', N'My Tho', N'Phường Mỹ Tho', N'My Tho Ward', 'my-tho', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80701002', N'Đạo Thạnh', N'Dao Thanh', N'Phường Đạo Thạnh', N'Dao Thanh Ward', 'dao-thanh', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80701003', N'Mỹ Phong', N'My Phong', N'Phường Mỹ Phong', N'My Phong Ward', 'my-phong', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80701004', N'Thới Sơn', N'Thoi Son', N'Phường Thới Sơn', N'Thoi Son Ward', 'thoi-son', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80701005', N'Trung An', N'Trung An', N'Phường Trung An', N'Trung An Ward', 'trung-an', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80703006', N'Gò Công', N'Go Cong', N'Phường Gò Công', N'Go Cong Ward', 'go-cong', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80703007', N'Long Thuận', N'Long Thuan', N'Phường Long Thuận', N'Long Thuan Ward', 'long-thuan', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80703008', N'Sơn Qui', N'Son Qui', N'Phường Sơn Qui', N'Son Qui Ward', 'son-qui', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80703009', N'Bình Xuân', N'Binh Xuan', N'Phường Bình Xuân', N'Binh Xuan Ward', 'binh-xuan', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80721010', N'Mỹ Phước Tây', N'My Phuoc Tay', N'Phường Mỹ Phước Tây', N'My Phuoc Tay Ward', 'my-phuoc-tay', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80721011', N'Thanh Hoà', N'Thanh Hoa', N'Phường Thanh Hoà', N'Thanh Hoa Ward', 'thanh-hoa', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80721012', N'Cai Lậy', N'Cai Lay', N'Phường Cai Lậy', N'Cai Lay Ward', 'cai-lay', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80721013', N'Nhị Quý', N'Nhi Quy', N'Phường Nhị Quý', N'Nhi Quy Ward', 'nhi-quy', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80721014', N'Tân Phú', N'Tan Phu', N'Xã Tân Phú', N'Tan Phu Commune', 'tan-phu', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80713015', N'Thanh Hưng', N'Thanh Hung', N'Xã Thanh Hưng', N'Thanh Hung Commune', 'thanh-hung', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80713016', N'An Hữu', N'An Huu', N'Xã An Hữu', N'An Huu Commune', 'an-huu', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80713017', N'Mỹ Lợi', N'My Loi', N'Xã Mỹ Lợi', N'My Loi Commune', 'my-loi', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80713018', N'Mỹ Đức Tây', N'My Duc Tay', N'Xã Mỹ Đức Tây', N'My Duc Tay Commune', 'my-duc-tay', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80713019', N'Mỹ Thiện', N'My Thien', N'Xã Mỹ Thiện', N'My Thien Commune', 'my-thien', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80713020', N'Hậu Mỹ', N'Hau My', N'Xã Hậu Mỹ', N'Hau My Commune', 'hau-my', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80713021', N'Hội Cư', N'Hoi Cu', N'Xã Hội Cư', N'Hoi Cu Commune', 'hoi-cu', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80713022', N'Cái Bè', N'Cai Be', N'Xã Cái Bè', N'Cai Be Commune', 'cai-be', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80709023', N'Bình Phú', N'Binh Phu', N'Xã Bình Phú', N'Binh Phu Commune', 'binh-phu', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80709024', N'Hiệp Đức', N'Hiep Duc', N'Xã Hiệp Đức', N'Hiep Duc Commune', 'hiep-duc', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80709025', N'Ngũ Hiệp', N'Ngu Hiep', N'Xã Ngũ Hiệp', N'Ngu Hiep Commune', 'ngu-hiep', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80709026', N'Long Tiên', N'Long Tien', N'Xã Long Tiên', N'Long Tien Commune', 'long-tien', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80709027', N'Mỹ Thành', N'My Thanh', N'Xã Mỹ Thành', N'My Thanh Commune', 'my-thanh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80709028', N'Thạnh Phú', N'Thanh Phu', N'Xã Thạnh Phú', N'Thanh Phu Commune', 'thanh-phu', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80705029', N'Tân Phước 1', N'Tan Phuoc 1', N'Xã Tân Phước 1', N'Tan Phuoc 1 Commune', 'tan-phuoc-1', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80705030', N'Tân Phước 2', N'Tan Phuoc 2', N'Xã Tân Phước 2', N'Tan Phuoc 2 Commune', 'tan-phuoc-2', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80705031', N'Tân Phước 3', N'Tan Phuoc 3', N'Xã Tân Phước 3', N'Tan Phuoc 3 Commune', 'tan-phuoc-3', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80705032', N'Hưng Thạnh', N'Hung Thanh', N'Xã Hưng Thạnh', N'Hung Thanh Commune', 'hung-thanh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80707033', N'Tân Hương', N'Tan Huong', N'Xã Tân Hương', N'Tan Huong Commune', 'tan-huong', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80707034', N'Châu Thành', N'Chau Thanh', N'Xã Châu Thành', N'Chau Thanh Commune', 'chau-thanh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80707035', N'Long Hưng', N'Long Hung', N'Xã Long Hưng', N'Long Hung Commune', 'long-hung', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80707036', N'Long Định', N'Long Dinh', N'Xã Long Định', N'Long Dinh Commune', 'long-dinh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80707037', N'Vĩnh Kim', N'Vinh Kim', N'Xã Vĩnh Kim', N'Vinh Kim Commune', 'vinh-kim', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80707038', N'Kim Sơn', N'Kim Son', N'Xã Kim Sơn', N'Kim Son Commune', 'kim-son', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80707039', N'Bình Trưng', N'Binh Trung', N'Xã Bình Trưng', N'Binh Trung Commune', 'binh-trung', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80711040', N'Mỹ Tịnh An', N'My Tinh An', N'Xã Mỹ Tịnh An', N'My Tinh An Commune', 'my-tinh-an', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80711041', N'Lương Hoà Lạc', N'Luong Hoa Lac', N'Xã Lương Hoà Lạc', N'Luong Hoa Lac Commune', 'luong-hoa-lac', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80711042', N'Tân Thuận Bình', N'Tan Thuan Binh', N'Xã Tân Thuận Bình', N'Tan Thuan Binh Commune', 'tan-thuan-binh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80711043', N'Chợ Gạo', N'Cho Gao', N'Xã Chợ Gạo', N'Cho Gao Commune', 'cho-gao', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80711044', N'An Thạnh Thủy', N'An Thanh Thuy', N'Xã An Thạnh Thủy', N'An Thanh Thuy Commune', 'an-thanh-thuy', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80711045', N'Bình Ninh', N'Binh Ninh', N'Xã Bình Ninh', N'Binh Ninh Commune', 'binh-ninh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80715046', N'Vĩnh Bình', N'Vinh Binh', N'Xã Vĩnh Bình', N'Vinh Binh Commune', 'vinh-binh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80715047', N'Đồng Sơn', N'Dong Son', N'Xã Đồng Sơn', N'Dong Son Commune', 'dong-son', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80715048', N'Phú Thành', N'Phu Thanh', N'Xã Phú Thành', N'Phu Thanh Commune', 'phu-thanh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80715049', N'Long Bình', N'Long Binh', N'Xã Long Bình', N'Long Binh Commune', 'long-binh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80715050', N'Vĩnh Hựu', N'Vinh Huu', N'Xã Vĩnh Hựu', N'Vinh Huu Commune', 'vinh-huu', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80717051', N'Gò Công Đông', N'Go Cong Dong', N'Xã Gò Công Đông', N'Go Cong Dong Commune', 'go-cong-dong', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80717052', N'Tân Điền', N'Tan Dien', N'Xã Tân Điền', N'Tan Dien Commune', 'tan-dien', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80717053', N'Tân Hoà', N'Tan Hoa', N'Xã Tân Hoà', N'Tan Hoa Commune', 'tan-hoa', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80717054', N'Tân Đông', N'Tan Dong', N'Xã Tân Đông', N'Tan Dong Commune', 'tan-dong', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80717055', N'Gia Thuận', N'Gia Thuan', N'Xã Gia Thuận', N'Gia Thuan Commune', 'gia-thuan', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80719056', N'Tân Thới', N'Tan Thoi', N'Xã Tân Thới', N'Tan Thoi Commune', 'tan-thoi', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80719057', N'Tân Phú Đông', N'Tan Phu Dong', N'Xã Tân Phú Đông', N'Tan Phu Dong Commune', 'tan-phu-dong', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80305058', N'Tân Hồng', N'Tan Hong', N'Xã Tân Hồng', N'Tan Hong Commune', 'tan-hong', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80305059', N'Tân Thành', N'Tan Thanh', N'Xã Tân Thành', N'Tan Thanh Commune', 'tan-thanh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80305060', N'Tân Hộ Cơ', N'Tan Ho Co', N'Xã Tân Hộ Cơ', N'Tan Ho Co Commune', 'tan-ho-co', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80305061', N'An Phước', N'An Phuoc', N'Xã An Phước', N'An Phuoc Commune', 'an-phuoc', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80323062', N'An Bình', N'An Binh', N'Phường An Bình', N'An Binh Ward', 'an-binh', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80323063', N'Hồng Ngự', N'Hong Ngu', N'Phường Hồng Ngự', N'Hong Ngu Ward', 'hong-ngu', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80307064', N'Thường Lạc', N'Thuong Lac', N'Phường Thường Lạc', N'Thuong Lac Ward', 'thuong-lac', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80307065', N'Thường Phước', N'Thuong Phuoc', N'Xã Thường Phước', N'Thuong Phuoc Commune', 'thuong-phuoc', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80307066', N'Long Khánh', N'Long Khanh', N'Xã Long Khánh', N'Long Khanh Commune', 'long-khanh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80307067', N'Long Phú Thuận', N'Long Phu Thuan', N'Xã Long Phú Thuận', N'Long Phu Thuan Commune', 'long-phu-thuan', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80309068', N'An Hoà', N'An Hoa', N'Xã An Hoà', N'An Hoa Commune', 'an-hoa', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80309069', N'Tam Nông', N'Tam Nong', N'Xã Tam Nông', N'Tam Nong Commune', 'tam-nong', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80309070', N'Phú Thọ', N'Phu Tho', N'Xã Phú Thọ', N'Phu Tho Commune', 'phu-tho', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80309071', N'Tràm Chim', N'Tram Chim', N'Xã Tràm Chim', N'Tram Chim Commune', 'tram-chim', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80309072', N'Phú Cường', N'Phu Cuong', N'Xã Phú Cường', N'Phu Cuong Commune', 'phu-cuong', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80309073', N'An Long', N'An Long', N'Xã An Long', N'An Long Commune', 'an-long', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80311074', N'Thanh Bình', N'Thanh Binh', N'Xã Thanh Bình', N'Thanh Binh Commune', 'thanh-binh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80311075', N'Tân Thạnh', N'Tan Thanh', N'Xã Tân Thạnh', N'Tan Thanh Commune', 'tan-thanh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80311076', N'Bình Thành', N'Binh Thanh', N'Xã Bình Thành', N'Binh Thanh Commune', 'binh-thanh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80311077', N'Tân Long', N'Tan Long', N'Xã Tân Long', N'Tan Long Commune', 'tan-long', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80313078', N'Tháp Mười', N'Thap Muoi', N'Xã Tháp Mười', N'Thap Muoi Commune', 'thap-muoi', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80313079', N'Thanh Mỹ', N'Thanh My', N'Xã Thanh Mỹ', N'Thanh My Commune', 'thanh-my', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80313080', N'Mỹ Quí', N'My Qui', N'Xã Mỹ Quí', N'My Qui Commune', 'my-qui', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80313081', N'Đốc Binh Kiều', N'Doc Binh Kieu', N'Xã Đốc Binh Kiều', N'Doc Binh Kieu Commune', 'doc-binh-kieu', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80313082', N'Trường Xuân', N'Truong Xuan', N'Xã Trường Xuân', N'Truong Xuan Commune', 'truong-xuan', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80313083', N'Phương Thịnh', N'Phuong Thinh', N'Xã Phương Thịnh', N'Phuong Thinh Commune', 'phuong-thinh', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80315084', N'Phong Mỹ', N'Phong My', N'Xã Phong Mỹ', N'Phong My Commune', 'phong-my', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80315085', N'Ba Sao', N'Ba Sao', N'Xã Ba Sao', N'Ba Sao Commune', 'ba-sao', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80315086', N'Mỹ Thọ', N'My Tho', N'Xã Mỹ Thọ', N'My Tho Commune', 'my-tho', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80315087', N'Bình Hàng Trung', N'Binh Hang Trung', N'Xã Bình Hàng Trung', N'Binh Hang Trung Commune', 'binh-hang-trung', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80315088', N'Mỹ Hiệp', N'My Hiep', N'Xã Mỹ Hiệp', N'My Hiep Commune', 'my-hiep', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80301089', N'Cao Lãnh', N'Cao Lanh', N'Phường Cao Lãnh', N'Cao Lanh Ward', 'cao-lanh', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80301090', N'Mỹ Ngãi', N'My Ngai', N'Phường Mỹ Ngãi', N'My Ngai Ward', 'my-ngai', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80301091', N'Mỹ Trà', N'My Tra', N'Phường Mỹ Trà', N'My Tra Ward', 'my-tra', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80317092', N'Mỹ An Hưng', N'My An Hung', N'Xã Mỹ An Hưng', N'My An Hung Commune', 'my-an-hung', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80317093', N'Tân Khánh Trung', N'Tan Khanh Trung', N'Xã Tân Khánh Trung', N'Tan Khanh Trung Commune', 'tan-khanh-trung', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80317094', N'Lấp Vò', N'Lap Vo', N'Xã Lấp Vò', N'Lap Vo Commune', 'lap-vo', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80319095', N'Lai Vung', N'Lai Vung', N'Xã Lai Vung', N'Lai Vung Commune', 'lai-vung', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80319096', N'Hoà Long', N'Hoa Long', N'Xã Hoà Long', N'Hoa Long Commune', 'hoa-long', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80319097', N'Phong Hoà', N'Phong Hoa', N'Xã Phong Hoà', N'Phong Hoa Commune', 'phong-hoa', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80303098', N'Sa Đéc', N'Sa Dec', N'Phường Sa Đéc', N'Sa Dec Ward', 'sa-dec', 7, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80319099', N'Tân Dương', N'Tan Duong', N'Xã Tân Dương', N'Tan Duong Commune', 'tan-duong', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80321100', N'Phú Hựu', N'Phu Huu', N'Xã Phú Hựu', N'Phu Huu Commune', 'phu-huu', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80321101', N'Tân Nhuận Đông', N'Tan Nhuan Dong', N'Xã Tân Nhuận Đông', N'Tan Nhuan Dong Commune', 'tan-nhuan-dong', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80321102', N'Tân Phú Trung', N'Tan Phu Trung', N'Xã Tân Phú Trung', N'Tan Phu Trung Commune', 'tan-phu-trung', 8, N'803', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80501001', N'Mỹ Hoà Hưng', N'My Hoa Hung', N'Xã Mỹ Hoà Hưng', N'My Hoa Hung Commune', 'my-hoa-hung', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80501002', N'Long Xuyên', N'Long Xuyen', N'Phường Long Xuyên', N'Long Xuyen Ward', 'long-xuyen', 7, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80501003', N'Bình Đức', N'Binh Duc', N'Phường Bình Đức', N'Binh Duc Ward', 'binh-duc', 7, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80501004', N'Mỹ Thới', N'My Thoi', N'Phường Mỹ Thới', N'My Thoi Ward', 'my-thoi', 7, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80503005', N'Châu Đốc', N'Chau Doc', N'Phường Châu Đốc', N'Chau Doc Ward', 'chau-doc', 7, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80503006', N'Vĩnh Tế', N'Vinh Te', N'Phường Vĩnh Tế', N'Vinh Te Ward', 'vinh-te', 7, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80505007', N'An Phú', N'An Phu', N'Xã An Phú', N'An Phu Commune', 'an-phu', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80505008', N'Vĩnh Hậu', N'Vinh Hau', N'Xã Vĩnh Hậu', N'Vinh Hau Commune', 'vinh-hau', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80505009', N'Nhơn Hội', N'Nhon Hoi', N'Xã Nhơn Hội', N'Nhon Hoi Commune', 'nhon-hoi', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80505010', N'Khánh Bình', N'Khanh Binh', N'Xã Khánh Bình', N'Khanh Binh Commune', 'khanh-binh', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80505011', N'Phú Hữu', N'Phu Huu', N'Xã Phú Hữu', N'Phu Huu Commune', 'phu-huu', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80507012', N'Tân An', N'Tan An', N'Xã Tân An', N'Tan An Commune', 'tan-an', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80507013', N'Châu Phong', N'Chau Phong', N'Xã Châu Phong', N'Chau Phong Commune', 'chau-phong', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80507014', N'Vĩnh Xương', N'Vinh Xuong', N'Xã Vĩnh Xương', N'Vinh Xuong Commune', 'vinh-xuong', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80507015', N'Tân Châu', N'Tan Chau', N'Phường Tân Châu', N'Tan Chau Ward', 'tan-chau', 7, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80507016', N'Long Phú', N'Long Phu', N'Phường Long Phú', N'Long Phu Ward', 'long-phu', 7, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80509017', N'Phú Tân', N'Phu Tan', N'Xã Phú Tân', N'Phu Tan Commune', 'phu-tan', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80509018', N'Phú An', N'Phu An', N'Xã Phú An', N'Phu An Commune', 'phu-an', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80509019', N'Bình Thạnh Đông', N'Binh Thanh Dong', N'Xã Bình Thạnh Đông', N'Binh Thanh Dong Commune', 'binh-thanh-dong', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80509020', N'Chợ Vàm', N'Cho Vam', N'Xã Chợ Vàm', N'Cho Vam Commune', 'cho-vam', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80509021', N'Hoà Lạc', N'Hoa Lac', N'Xã Hoà Lạc', N'Hoa Lac Commune', 'hoa-lac', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80509022', N'Phú Lâm', N'Phu Lam', N'Xã Phú Lâm', N'Phu Lam Commune', 'phu-lam', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80511023', N'Châu Phú', N'Chau Phu', N'Xã Châu Phú', N'Chau Phu Commune', 'chau-phu', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80511024', N'Mỹ Đức', N'My Duc', N'Xã Mỹ Đức', N'My Duc Commune', 'my-duc', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80511025', N'Vĩnh Thạnh Trung', N'Vinh Thanh Trung', N'Xã Vĩnh Thạnh Trung', N'Vinh Thanh Trung Commune', 'vinh-thanh-trung', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80511026', N'Bình Mỹ', N'Binh My', N'Xã Bình Mỹ', N'Binh My Commune', 'binh-my', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80511027', N'Thạnh Mỹ Tây', N'Thanh My Tay', N'Xã Thạnh Mỹ Tây', N'Thanh My Tay Commune', 'thanh-my-tay', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80513028', N'An Cư', N'An Cu', N'Xã An Cư', N'An Cu Commune', 'an-cu', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80513029', N'Núi Cấm', N'Nui Cam', N'Xã Núi Cấm', N'Nui Cam Commune', 'nui-cam', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80513030', N'Tịnh Biên', N'Tinh Bien', N'Phường Tịnh Biên', N'Tinh Bien Ward', 'tinh-bien', 7, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80513031', N'Thới Sơn', N'Thoi Son', N'Phường Thới Sơn', N'Thoi Son Ward', 'thoi-son', 7, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80513032', N'Chi Lăng', N'Chi Lang', N'Phường Chi Lăng', N'Chi Lang Ward', 'chi-lang', 7, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80515033', N'Ba Chúc', N'Ba Chuc', N'Xã Ba Chúc', N'Ba Chuc Commune', 'ba-chuc', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80515034', N'Tri Tôn', N'Tri Ton', N'Xã Tri Tôn', N'Tri Ton Commune', 'tri-ton', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80515035', N'Ô Lâm', N'O Lam', N'Xã Ô Lâm', N'O Lam Commune', 'o-lam', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80515036', N'Cô Tô', N'Co To', N'Xã Cô Tô', N'Co To Commune', 'co-to', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80515037', N'Vĩnh Gia', N'Vinh Gia', N'Xã Vĩnh Gia', N'Vinh Gia Commune', 'vinh-gia', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80519038', N'An Châu', N'An Chau', N'Xã An Châu', N'An Chau Commune', 'an-chau', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80519039', N'Bình Hoà', N'Binh Hoa', N'Xã Bình Hoà', N'Binh Hoa Commune', 'binh-hoa', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80519040', N'Cần Đăng', N'Can Dang', N'Xã Cần Đăng', N'Can Dang Commune', 'can-dang', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80519041', N'Vĩnh Hanh', N'Vinh Hanh', N'Xã Vĩnh Hanh', N'Vinh Hanh Commune', 'vinh-hanh', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80519042', N'Vĩnh An', N'Vinh An', N'Xã Vĩnh An', N'Vinh An Commune', 'vinh-an', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80517043', N'Chợ Mới', N'Cho Moi', N'Xã Chợ Mới', N'Cho Moi Commune', 'cho-moi', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80517044', N'Cù Lao Giêng', N'Cu Lao Gieng', N'Xã Cù Lao Giêng', N'Cu Lao Gieng Commune', 'cu-lao-gieng', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80517045', N'Hội An', N'Hoi An', N'Xã Hội An', N'Hoi An Commune', 'hoi-an', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80517046', N'Long Điền', N'Long Dien', N'Xã Long Điền', N'Long Dien Commune', 'long-dien', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80517047', N'Nhơn Mỹ', N'Nhon My', N'Xã Nhơn Mỹ', N'Nhon My Commune', 'nhon-my', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80517048', N'Long Kiến', N'Long Kien', N'Xã Long Kiến', N'Long Kien Commune', 'long-kien', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80521049', N'Thoại Sơn', N'Thoai Son', N'Xã Thoại Sơn', N'Thoai Son Commune', 'thoai-son', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80521050', N'Óc Eo', N'Oc Eo', N'Xã Óc Eo', N'Oc Eo Commune', 'oc-eo', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80521051', N'Định Mỹ', N'Dinh My', N'Xã Định Mỹ', N'Dinh My Commune', 'dinh-my', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80521052', N'Phú Hoà', N'Phu Hoa', N'Xã Phú Hoà', N'Phu Hoa Commune', 'phu-hoa', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80521053', N'Vĩnh Trạch', N'Vinh Trach', N'Xã Vĩnh Trạch', N'Vinh Trach Commune', 'vinh-trach', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'80521054', N'Tây Phú', N'Tay Phu', N'Xã Tây Phú', N'Tay Phu Commune', 'tay-phu', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81319055', N'Vĩnh Bình', N'Vinh Binh', N'Xã Vĩnh Bình', N'Vinh Binh Commune', 'vinh-binh', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81319056', N'Vĩnh Thuận', N'Vinh Thuan', N'Xã Vĩnh Thuận', N'Vinh Thuan Commune', 'vinh-thuan', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81319057', N'Vĩnh Phong', N'Vinh Phong', N'Xã Vĩnh Phong', N'Vinh Phong Commune', 'vinh-phong', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81327058', N'Vĩnh Hoà', N'Vinh Hoa', N'Xã Vĩnh Hoà', N'Vinh Hoa Commune', 'vinh-hoa', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81327059', N'U Minh Thượng', N'U Minh Thuong', N'Xã U Minh Thượng', N'U Minh Thuong Commune', 'u-minh-thuong', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81317060', N'Đông Hoà', N'Dong Hoa', N'Xã Đông Hoà', N'Dong Hoa Commune', 'dong-hoa', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81317061', N'Tân Thạnh', N'Tan Thanh', N'Xã Tân Thạnh', N'Tan Thanh Commune', 'tan-thanh', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81317062', N'Đông Hưng', N'Dong Hung', N'Xã Đông Hưng', N'Dong Hung Commune', 'dong-hung', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81317063', N'An Minh', N'An Minh', N'Xã An Minh', N'An Minh Commune', 'an-minh', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81317064', N'Vân Khánh', N'Van Khanh', N'Xã Vân Khánh', N'Van Khanh Commune', 'van-khanh', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81315065', N'Tây Yên', N'Tay Yen', N'Xã Tây Yên', N'Tay Yen Commune', 'tay-yen', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81315066', N'Đông Thái', N'Dong Thai', N'Xã Đông Thái', N'Dong Thai Commune', 'dong-thai', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81315067', N'An Biên', N'An Bien', N'Xã An Biên', N'An Bien Commune', 'an-bien', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81313068', N'Định Hoà', N'Dinh Hoa', N'Xã Định Hoà', N'Dinh Hoa Commune', 'dinh-hoa', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81313069', N'Gò Quao', N'Go Quao', N'Xã Gò Quao', N'Go Quao Commune', 'go-quao', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81313070', N'Vĩnh Hoà Hưng', N'Vinh Hoa Hung', N'Xã Vĩnh Hoà Hưng', N'Vinh Hoa Hung Commune', 'vinh-hoa-hung', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81313071', N'Vĩnh Tuy', N'Vinh Tuy', N'Xã Vĩnh Tuy', N'Vinh Tuy Commune', 'vinh-tuy', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81311072', N'Giồng Riềng', N'Giong Rieng', N'Xã Giồng Riềng', N'Giong Rieng Commune', 'giong-rieng', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81311073', N'Thạnh Hưng', N'Thanh Hung', N'Xã Thạnh Hưng', N'Thanh Hung Commune', 'thanh-hung', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81311074', N'Long Thạnh', N'Long Thanh', N'Xã Long Thạnh', N'Long Thanh Commune', 'long-thanh', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81311075', N'Hoà Hưng', N'Hoa Hung', N'Xã Hoà Hưng', N'Hoa Hung Commune', 'hoa-hung', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81311076', N'Ngọc Chúc', N'Ngoc Chuc', N'Xã Ngọc Chúc', N'Ngoc Chuc Commune', 'ngoc-chuc', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81311077', N'Hoà Thuận', N'Hoa Thuan', N'Xã Hoà Thuận', N'Hoa Thuan Commune', 'hoa-thuan', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81307078', N'Tân Hội', N'Tan Hoi', N'Xã Tân Hội', N'Tan Hoi Commune', 'tan-hoi', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81307079', N'Tân Hiệp', N'Tan Hiep', N'Xã Tân Hiệp', N'Tan Hiep Commune', 'tan-hiep', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81307080', N'Thạnh Đông', N'Thanh Dong', N'Xã Thạnh Đông', N'Thanh Dong Commune', 'thanh-dong', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81309081', N'Thạnh Lộc', N'Thanh Loc', N'Xã Thạnh Lộc', N'Thanh Loc Commune', 'thanh-loc', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81309082', N'Châu Thành', N'Chau Thanh', N'Xã Châu Thành', N'Chau Thanh Commune', 'chau-thanh', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81309083', N'Bình An', N'Binh An', N'Xã Bình An', N'Binh An Commune', 'binh-an', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81305084', N'Hòn Đất', N'Hon Dat', N'Xã Hòn Đất', N'Hon Dat Commune', 'hon-dat', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81305085', N'Sơn Kiên', N'Son Kien', N'Xã Sơn Kiên', N'Son Kien Commune', 'son-kien', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81305086', N'Mỹ Thuận', N'My Thuan', N'Xã Mỹ Thuận', N'My Thuan Commune', 'my-thuan', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81305087', N'Bình Sơn', N'Binh Son', N'Xã Bình Sơn', N'Binh Son Commune', 'binh-son', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81305088', N'Bình Giang', N'Binh Giang', N'Xã Bình Giang', N'Binh Giang Commune', 'binh-giang', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81304089', N'Giang Thành', N'Giang Thanh', N'Xã Giang Thành', N'Giang Thanh Commune', 'giang-thanh', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81304090', N'Vĩnh Điều', N'Vinh Dieu', N'Xã Vĩnh Điều', N'Vinh Dieu Commune', 'vinh-dieu', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81303091', N'Hoà Điền', N'Hoa Dien', N'Xã Hoà Điền', N'Hoa Dien Commune', 'hoa-dien', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81303092', N'Kiên Lương', N'Kien Luong', N'Xã Kiên Lương', N'Kien Luong Commune', 'kien-luong', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81303093', N'Sơn Hải', N'Son Hai', N'Xã Sơn Hải', N'Son Hai Commune', 'son-hai', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81303094', N'Hòn Nghệ', N'Hon Nghe', N'Xã Hòn Nghệ', N'Hon Nghe Commune', 'hon-nghe', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81323095', N'khu Kiên Hải', N'khu Kien Hai', N'Đặc khu Kiên Hải', N'khu Kien Hai Đặc', 'khu-kien-hai', 2, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81301096', N'Vĩnh Thông', N'Vinh Thong', N'Phường Vĩnh Thông', N'Vinh Thong Ward', 'vinh-thong', 7, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81301097', N'Rạch Giá', N'Rach Gia', N'Phường Rạch Giá', N'Rach Gia Ward', 'rach-gia', 7, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81325098', N'Hà Tiên', N'Ha Tien', N'Phường Hà Tiên', N'Ha Tien Ward', 'ha-tien', 7, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81325099', N'Tô Châu', N'To Chau', N'Phường Tô Châu', N'To Chau Ward', 'to-chau', 7, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81325100', N'Tiên Hải', N'Tien Hai', N'Xã Tiên Hải', N'Tien Hai Commune', 'tien-hai', 8, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81321101', N'khu Phú Quốc', N'khu Phu Quoc', N'Đặc khu Phú Quốc', N'khu Phu Quoc Đặc', 'khu-phu-quoc', 2, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81321102', N'khu Thổ Châu', N'khu Tho Chau', N'Đặc khu Thổ Châu', N'khu Tho Chau Đặc', 'khu-tho-chau', 2, N'805', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81519001', N'Ninh Kiều', N'Ninh Kieu', N'Phường Ninh Kiều', N'Ninh Kieu Ward', 'ninh-kieu', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81519002', N'Cái Khế', N'Cai Khe', N'Phường Cái Khế', N'Cai Khe Ward', 'cai-khe', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81519003', N'Tân An', N'Tan An', N'Phường Tân An', N'Tan An Ward', 'tan-an', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81519004', N'An Bình', N'An Binh', N'Phường An Bình', N'An Binh Ward', 'an-binh', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81521005', N'Thới An Đông', N'Thoi An Dong', N'Phường Thới An Đông', N'Thoi An Dong Ward', 'thoi-an-dong', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81521006', N'Bình Thủy', N'Binh Thuy', N'Phường Bình Thủy', N'Binh Thuy Ward', 'binh-thuy', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81521007', N'Long Tuyền', N'Long Tuyen', N'Phường Long Tuyền', N'Long Tuyen Ward', 'long-tuyen', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81523008', N'Cái Răng', N'Cai Rang', N'Phường Cái Răng', N'Cai Rang Ward', 'cai-rang', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81523009', N'Hưng Phú', N'Hung Phu', N'Phường Hưng Phú', N'Hung Phu Ward', 'hung-phu', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81505010', N'Ô Môn', N'O Mon', N'Phường Ô Môn', N'O Mon Ward', 'o-mon', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81505011', N'Thới Long', N'Thoi Long', N'Phường Thới Long', N'Thoi Long Ward', 'thoi-long', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81505012', N'Phước Thới', N'Phuoc Thoi', N'Phường Phước Thới', N'Phuoc Thoi Ward', 'phuoc-thoi', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81503013', N'Trung Nhứt', N'Trung Nhut', N'Phường Trung Nhứt', N'Trung Nhut Ward', 'trung-nhut', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81503014', N'Thốt Nốt', N'Thot Not', N'Phường Thốt Nốt', N'Thot Not Ward', 'thot-not', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81503015', N'Thuận Hưng', N'Thuan Hung', N'Phường Thuận Hưng', N'Thuan Hung Ward', 'thuan-hung', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81503016', N'Tân Lộc', N'Tan Loc', N'Phường Tân Lộc', N'Tan Loc Ward', 'tan-loc', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81529017', N'Phong Điền', N'Phong Dien', N'Xã Phong Điền', N'Phong Dien Commune', 'phong-dien', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81529018', N'Nhơn Ái', N'Nhon Ai', N'Xã Nhơn Ái', N'Nhon Ai Commune', 'nhon-ai', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81529019', N'Trường Long', N'Truong Long', N'Xã Trường Long', N'Truong Long Commune', 'truong-long', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81531020', N'Thới Lai', N'Thoi Lai', N'Xã Thới Lai', N'Thoi Lai Commune', 'thoi-lai', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81531021', N'Đông Thuận', N'Dong Thuan', N'Xã Đông Thuận', N'Dong Thuan Commune', 'dong-thuan', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81531022', N'Trường Xuân', N'Truong Xuan', N'Xã Trường Xuân', N'Truong Xuan Commune', 'truong-xuan', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81531023', N'Trường Thành', N'Truong Thanh', N'Xã Trường Thành', N'Truong Thanh Commune', 'truong-thanh', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81527024', N'Cờ Đỏ', N'Co Dỏ', N'Xã Cờ Đỏ', N'Co Dỏ Commune', 'co-do', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81527025', N'Đông Hiệp', N'Dong Hiep', N'Xã Đông Hiệp', N'Dong Hiep Commune', 'dong-hiep', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81527026', N'Thạnh Phú', N'Thanh Phu', N'Xã Thạnh Phú', N'Thanh Phu Commune', 'thanh-phu', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81527027', N'Thới Hưng', N'Thoi Hung', N'Xã Thới Hưng', N'Thoi Hung Commune', 'thoi-hung', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81527028', N'Trung Hưng', N'Trung Hung', N'Xã Trung Hưng', N'Trung Hung Commune', 'trung-hung', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81525029', N'Vĩnh Thạnh', N'Vinh Thanh', N'Xã Vĩnh Thạnh', N'Vinh Thanh Commune', 'vinh-thanh', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81525030', N'Vĩnh Trinh', N'Vinh Trinh', N'Xã Vĩnh Trinh', N'Vinh Trinh Commune', 'vinh-trinh', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81525031', N'Thạnh An', N'Thanh An', N'Xã Thạnh An', N'Thanh An Commune', 'thanh-an', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81525032', N'Thạnh Quới', N'Thanh Quoi', N'Xã Thạnh Quới', N'Thanh Quoi Commune', 'thanh-quoi', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81601033', N'Hỏa Lựu', N'Hỏa Luu', N'Xã Hỏa Lựu', N'Hỏa Luu Commune', 'hoa-luu', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81601034', N'Vị Thanh', N'Vi Thanh', N'Phường Vị Thanh', N'Vi Thanh Ward', 'vi-thanh', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81601035', N'Vị Tân', N'Vi Tan', N'Phường Vị Tân', N'Vi Tan Ward', 'vi-tan', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81609036', N'Vị Thủy', N'Vi Thuy', N'Xã Vị Thủy', N'Vi Thuy Commune', 'vi-thuy', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81609037', N'Vĩnh Thuận Đông', N'Vinh Thuan Dong', N'Xã Vĩnh Thuận Đông', N'Vinh Thuan Dong Commune', 'vinh-thuan-dong', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81609038', N'Vị Thanh 1', N'Vi Thanh 1', N'Xã Vị Thanh 1', N'Vi Thanh 1 Commune', 'vi-thanh-1', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81609039', N'Vĩnh Tường', N'Vinh Tuong', N'Xã Vĩnh Tường', N'Vinh Tuong Commune', 'vinh-tuong', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81611040', N'Vĩnh Viễn', N'Vinh Vien', N'Xã Vĩnh Viễn', N'Vinh Vien Commune', 'vinh-vien', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81611041', N'Xà Phiên', N'Xa Phien', N'Xã Xà Phiên', N'Xa Phien Commune', 'xa-phien', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81611042', N'Lương Tâm', N'Luong Tam', N'Xã Lương Tâm', N'Luong Tam Commune', 'luong-tam', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81612043', N'Long Bình', N'Long Binh', N'Phường Long Bình', N'Long Binh Ward', 'long-binh', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81612044', N'Long Mỹ', N'Long My', N'Phường Long Mỹ', N'Long My Ward', 'long-my', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81612045', N'Long Phú 1', N'Long Phu 1', N'Phường Long Phú 1', N'Long Phu 1 Ward', 'long-phu-1', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81603046', N'Thạnh Xuân', N'Thanh Xuan', N'Xã Thạnh Xuân', N'Thanh Xuan Commune', 'thanh-xuan', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81603047', N'Tân Hoà', N'Tan Hoa', N'Xã Tân Hoà', N'Tan Hoa Commune', 'tan-hoa', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81603048', N'Trường Long Tây', N'Truong Long Tay', N'Xã Trường Long Tây', N'Truong Long Tay Commune', 'truong-long-tay', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81605049', N'Châu Thành', N'Chau Thanh', N'Xã Châu Thành', N'Chau Thanh Commune', 'chau-thanh', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81605050', N'Đông Phước', N'Dong Phuoc', N'Xã Đông Phước', N'Dong Phuoc Commune', 'dong-phuoc', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81605051', N'Phú Hữu', N'Phu Huu', N'Xã Phú Hữu', N'Phu Huu Commune', 'phu-huu', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81607052', N'Đại Thành', N'Dai Thanh', N'Phường Đại Thành', N'Dai Thanh Ward', 'dai-thanh', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81607053', N'Ngã Bảy', N'Nga Bay', N'Phường Ngã Bảy', N'Nga Bay Ward', 'nga-bay', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81608054', N'Tân Bình', N'Tan Binh', N'Xã Tân Bình', N'Tan Binh Commune', 'tan-binh', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81608055', N'Hoà An', N'Hoa An', N'Xã Hoà An', N'Hoa An Commune', 'hoa-an', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81608056', N'Phương Bình', N'Phuong Binh', N'Xã Phương Bình', N'Phuong Binh Commune', 'phuong-binh', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81608057', N'Tân Phước Hưng', N'Tan Phuoc Hung', N'Xã Tân Phước Hưng', N'Tan Phuoc Hung Commune', 'tan-phuoc-hung', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81608058', N'Hiệp Hưng', N'Hiep Hung', N'Xã Hiệp Hưng', N'Hiep Hung Commune', 'hiep-hung', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81608059', N'Phụng Hiệp', N'Phung Hiep', N'Xã Phụng Hiệp', N'Phung Hiep Commune', 'phung-hiep', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81608060', N'Thạnh Hoà', N'Thanh Hoa', N'Xã Thạnh Hoà', N'Thanh Hoa Commune', 'thanh-hoa', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81901061', N'Phú Lợi', N'Phu Loi', N'Phường Phú Lợi', N'Phu Loi Ward', 'phu-loi', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81901062', N'Sóc Trăng', N'Soc Trang', N'Phường Sóc Trăng', N'Soc Trang Ward', 'soc-trang', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81901063', N'Mỹ Xuyên', N'My Xuyen', N'Phường Mỹ Xuyên', N'My Xuyen Ward', 'my-xuyen', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81909064', N'Hoà Tú', N'Hoa Tu', N'Xã Hoà Tú', N'Hoa Tu Commune', 'hoa-tu', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81909065', N'Gia Hoà', N'Gia Hoa', N'Xã Gia Hoà', N'Gia Hoa Commune', 'gia-hoa', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81909066', N'Nhu Gia', N'Nhu Gia', N'Xã Nhu Gia', N'Nhu Gia Commune', 'nhu-gia', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81909067', N'Ngọc Tố', N'Ngoc To', N'Xã Ngọc Tố', N'Ngoc To Commune', 'ngoc-to', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81905068', N'Trường Khánh', N'Truong Khanh', N'Xã Trường Khánh', N'Truong Khanh Commune', 'truong-khanh', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81905069', N'Đại Ngãi', N'Dai Ngai', N'Xã Đại Ngãi', N'Dai Ngai Commune', 'dai-ngai', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81905070', N'Tân Thạnh', N'Tan Thanh', N'Xã Tân Thạnh', N'Tan Thanh Commune', 'tan-thanh', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81905071', N'Long Phú', N'Long Phu', N'Xã Long Phú', N'Long Phu Commune', 'long-phu', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81903072', N'Nhơn Mỹ', N'Nhon My', N'Xã Nhơn Mỹ', N'Nhon My Commune', 'nhon-my', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81903073', N'Phong Nẫm', N'Phong Nam', N'Xã Phong Nẫm', N'Phong Nam Commune', 'phong-nam', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81903074', N'An Lạc Thôn', N'An Lac Thon', N'Xã An Lạc Thôn', N'An Lac Thon Commune', 'an-lac-thon', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81903075', N'Kế Sách', N'Ke Sach', N'Xã Kế Sách', N'Ke Sach Commune', 'ke-sach', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81903076', N'Thới An Hội', N'Thoi An Hoi', N'Xã Thới An Hội', N'Thoi An Hoi Commune', 'thoi-an-hoi', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81903077', N'Đại Hải', N'Dai Hai', N'Xã Đại Hải', N'Dai Hai Commune', 'dai-hai', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81915078', N'Phú Tâm', N'Phu Tam', N'Xã Phú Tâm', N'Phu Tam Commune', 'phu-tam', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81915079', N'An Ninh', N'An Ninh', N'Xã An Ninh', N'An Ninh Commune', 'an-ninh', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81915080', N'Thuận Hoà', N'Thuan Hoa', N'Xã Thuận Hoà', N'Thuan Hoa Commune', 'thuan-hoa', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81915081', N'Hồ Đắc Kiện', N'Ho Dac Kien', N'Xã Hồ Đắc Kiện', N'Ho Dac Kien Commune', 'ho-dac-kien', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81907082', N'Mỹ Tú', N'My Tu', N'Xã Mỹ Tú', N'My Tu Commune', 'my-tu', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81907083', N'Long Hưng', N'Long Hung', N'Xã Long Hưng', N'Long Hung Commune', 'long-hung', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81907084', N'Mỹ Phước', N'My Phuoc', N'Xã Mỹ Phước', N'My Phuoc Commune', 'my-phuoc', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81907085', N'Mỹ Hương', N'My Huong', N'Xã Mỹ Hương', N'My Huong Commune', 'my-huong', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81913086', N'Vĩnh Hải', N'Vinh Hai', N'Xã Vĩnh Hải', N'Vinh Hai Commune', 'vinh-hai', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81913087', N'Lai Hoà', N'Lai Hoa', N'Xã Lai Hoà', N'Lai Hoa Commune', 'lai-hoa', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81913088', N'Vĩnh Phước', N'Vinh Phuoc', N'Phường Vĩnh Phước', N'Vinh Phuoc Ward', 'vinh-phuoc', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81913089', N'Vĩnh Châu', N'Vinh Chau', N'Phường Vĩnh Châu', N'Vinh Chau Ward', 'vinh-chau', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81913090', N'Khánh Hoà', N'Khanh Hoa', N'Phường Khánh Hoà', N'Khanh Hoa Ward', 'khanh-hoa', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81912091', N'Tân Long', N'Tan Long', N'Xã Tân Long', N'Tan Long Commune', 'tan-long', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81912092', N'Ngã Năm', N'Nga Nam', N'Phường Ngã Năm', N'Nga Nam Ward', 'nga-nam', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81912093', N'Mỹ Quới', N'My Quoi', N'Phường Mỹ Quới', N'My Quoi Ward', 'my-quoi', 7, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81911094', N'Phú Lộc', N'Phu Loc', N'Xã Phú Lộc', N'Phu Loc Commune', 'phu-loc', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81911095', N'Vĩnh Lợi', N'Vinh Loi', N'Xã Vĩnh Lợi', N'Vinh Loi Commune', 'vinh-loi', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81911096', N'Lâm Tân', N'Lam Tan', N'Xã Lâm Tân', N'Lam Tan Commune', 'lam-tan', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81917097', N'Thạnh Thới An', N'Thanh Thoi An', N'Xã Thạnh Thới An', N'Thanh Thoi An Commune', 'thanh-thoi-an', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81917098', N'Tài Văn', N'Tai Van', N'Xã Tài Văn', N'Tai Van Commune', 'tai-van', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81917099', N'Liêu Tú', N'Lieu Tu', N'Xã Liêu Tú', N'Lieu Tu Commune', 'lieu-tu', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81917100', N'Lịch Hội Thượng', N'Lich Hoi Thuong', N'Xã Lịch Hội Thượng', N'Lich Hoi Thuong Commune', 'lich-hoi-thuong', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81917101', N'Trần Đề', N'Tran De', N'Xã Trần Đề', N'Tran De Commune', 'tran-de', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81906102', N'An Thạnh', N'An Thanh', N'Xã An Thạnh', N'An Thanh Commune', 'an-thanh', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'81906103', N'Cù Lao Dung', N'Cu Lao Dung', N'Xã Cù Lao Dung', N'Cu Lao Dung Commune', 'cu-lao-dung', 8, N'815', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82301001', N'An Xuyên', N'An Xuyen', N'Phường An Xuyên', N'An Xuyen Ward', 'an-xuyen', 7, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82301002', N'Lý Văn Lâm', N'Ly Van Lam', N'Phường Lý Văn Lâm', N'Ly Van Lam Ward', 'ly-van-lam', 7, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82301003', N'Tân Thành', N'Tan Thanh', N'Phường Tân Thành', N'Tan Thanh Ward', 'tan-thanh', 7, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82301004', N'Hòa Thành', N'Hoa Thanh', N'Phường Hòa Thành', N'Hoa Thanh Ward', 'hoa-thanh', 7, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82311005', N'Tân Thuận', N'Tan Thuan', N'Xã Tân Thuận', N'Tan Thuan Commune', 'tan-thuan', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82311006', N'Tân Tiến', N'Tan Tien', N'Xã Tân Tiến', N'Tan Tien Commune', 'tan-tien', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82311007', N'Tạ An Khương', N'Ta An Khuong', N'Xã Tạ An Khương', N'Ta An Khuong Commune', 'ta-an-khuong', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82311008', N'Trần Phán', N'Tran Phan', N'Xã Trần Phán', N'Tran Phan Commune', 'tran-phan', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82311009', N'Thanh Tùng', N'Thanh Tung', N'Xã Thanh Tùng', N'Thanh Tung Commune', 'thanh-tung', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82311010', N'Đầm Dơi', N'Dam Doi', N'Xã Đầm Dơi', N'Dam Doi Commune', 'dam-doi', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82311011', N'Quách Phẩm', N'Quach Pham', N'Xã Quách Phẩm', N'Quach Pham Commune', 'quach-pham', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82305012', N'U Minh', N'U Minh', N'Xã U Minh', N'U Minh Commune', 'u-minh', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82305013', N'Nguyễn Phích', N'Nguyen Phich', N'Xã Nguyễn Phích', N'Nguyen Phich Commune', 'nguyen-phich', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82305014', N'Khánh Lâm', N'Khanh Lam', N'Xã Khánh Lâm', N'Khanh Lam Commune', 'khanh-lam', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82305015', N'Khánh An', N'Khanh An', N'Xã Khánh An', N'Khanh An Commune', 'khanh-an', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82313016', N'Phan Ngọc Hiển', N'Phan Ngoc Hien', N'Xã Phan Ngọc Hiển', N'Phan Ngoc Hien Commune', 'phan-ngoc-hien', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82313017', N'Đất Mũi', N'Dat Mui', N'Xã Đất Mũi', N'Dat Mui Commune', 'dat-mui', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82313018', N'Tân Ân', N'Tan An', N'Xã Tân Ân', N'Tan An Commune', 'tan-an', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82307019', N'Khánh Bình', N'Khanh Binh', N'Xã Khánh Bình', N'Khanh Binh Commune', 'khanh-binh', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82307020', N'Đá Bạc', N'Da Bac', N'Xã Đá Bạc', N'Da Bac Commune', 'da-bac', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82307021', N'Khánh Hưng', N'Khanh Hung', N'Xã Khánh Hưng', N'Khanh Hung Commune', 'khanh-hung', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82307022', N'Sông Đốc', N'Song Doc', N'Xã Sông Đốc', N'Song Doc Commune', 'song-doc', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82307023', N'Trần Văn Thời', N'Tran Van Thoi', N'Xã Trần Văn Thời', N'Tran Van Thoi Commune', 'tran-van-thoi', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82303024', N'Thới Bình', N'Thoi Binh', N'Xã Thới Bình', N'Thoi Binh Commune', 'thoi-binh', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82303025', N'Trí Phải', N'Tri Phai', N'Xã Trí Phải', N'Tri Phai Commune', 'tri-phai', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82303026', N'Tân Lộc', N'Tan Loc', N'Xã Tân Lộc', N'Tan Loc Commune', 'tan-loc', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82303027', N'Hồ Thị Kỷ', N'Ho Thi Ky', N'Xã Hồ Thị Kỷ', N'Ho Thi Ky Commune', 'ho-thi-ky', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82303028', N'Biển Bạch', N'Bien Bach', N'Xã Biển Bạch', N'Bien Bach Commune', 'bien-bach', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82312029', N'Đất Mới', N'Dat Moi', N'Xã Đất Mới', N'Dat Moi Commune', 'dat-moi', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82312030', N'Năm Căn', N'Nam Can', N'Xã Năm Căn', N'Nam Can Commune', 'nam-can', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82312031', N'Tam Giang', N'Tam Giang', N'Xã Tam Giang', N'Tam Giang Commune', 'tam-giang', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82308032', N'Cái Đôi Vàm', N'Cai Doi Vam', N'Xã Cái Đôi Vàm', N'Cai Doi Vam Commune', 'cai-doi-vam', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82308033', N'Nguyễn Việt Khái', N'Nguyen Viet Khai', N'Xã Nguyễn Việt Khái', N'Nguyen Viet Khai Commune', 'nguyen-viet-khai', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82308034', N'Phú Tân', N'Phu Tan', N'Xã Phú Tân', N'Phu Tan Commune', 'phu-tan', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82308035', N'Phú Mỹ', N'Phu My', N'Xã Phú Mỹ', N'Phu My Commune', 'phu-my', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82309036', N'Lương Thế Trân', N'Luong The Tran', N'Xã Lương Thế Trân', N'Luong The Tran Commune', 'luong-the-tran', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82309037', N'Tân Hưng', N'Tan Hung', N'Xã Tân Hưng', N'Tan Hung Commune', 'tan-hung', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82309038', N'Hưng Mỹ', N'Hung My', N'Xã Hưng Mỹ', N'Hung My Commune', 'hung-my', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82309039', N'Cái Nước', N'Cai Nuoc', N'Xã Cái Nước', N'Cai Nuoc Commune', 'cai-nuoc', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82101040', N'Bạc Liêu', N'Bac Lieu', N'Phường Bạc Liêu', N'Bac Lieu Ward', 'bac-lieu', 7, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82101041', N'Vĩnh Trạch', N'Vinh Trach', N'Phường Vĩnh Trạch', N'Vinh Trach Ward', 'vinh-trach', 7, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82101042', N'Hiệp Thành', N'Hiep Thanh', N'Phường Hiệp Thành', N'Hiep Thanh Ward', 'hiep-thanh', 7, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82107043', N'Giá Rai', N'Gia Rai', N'Phường Giá Rai', N'Gia Rai Ward', 'gia-rai', 7, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82107044', N'Láng Tròn', N'Lang Tron', N'Phường Láng Tròn', N'Lang Tron Ward', 'lang-tron', 7, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82107045', N'Phong Thạnh', N'Phong Thanh', N'Xã Phong Thạnh', N'Phong Thanh Commune', 'phong-thanh', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82103046', N'Hồng Dân', N'Hong Dan', N'Xã Hồng Dân', N'Hong Dan Commune', 'hong-dan', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82103047', N'Vĩnh Lộc', N'Vinh Loc', N'Xã Vĩnh Lộc', N'Vinh Loc Commune', 'vinh-loc', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82103048', N'Ninh Thạnh Lợi', N'Ninh Thanh Loi', N'Xã Ninh Thạnh Lợi', N'Ninh Thanh Loi Commune', 'ninh-thanh-loi', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82103049', N'Ninh Quới', N'Ninh Quoi', N'Xã Ninh Quới', N'Ninh Quoi Commune', 'ninh-quoi', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82111050', N'Gành Hào', N'Ganh Hao', N'Xã Gành Hào', N'Ganh Hao Commune', 'ganh-hao', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82111051', N'Định Thành', N'Dinh Thanh', N'Xã Định Thành', N'Dinh Thanh Commune', 'dinh-thanh', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82111052', N'An Trạch', N'An Trach', N'Xã An Trạch', N'An Trach Commune', 'an-trach', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82111053', N'Long Điền', N'Long Dien', N'Xã Long Điền', N'Long Dien Commune', 'long-dien', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82111054', N'Đông Hải', N'Dong Hai', N'Xã Đông Hải', N'Dong Hai Commune', 'dong-hai', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82106055', N'Hoà Bình', N'Hoa Binh', N'Xã Hoà Bình', N'Hoa Binh Commune', 'hoa-binh', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82106056', N'Vĩnh Mỹ', N'Vinh My', N'Xã Vĩnh Mỹ', N'Vinh My Commune', 'vinh-my', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82106057', N'Vĩnh Hậu', N'Vinh Hau', N'Xã Vĩnh Hậu', N'Vinh Hau Commune', 'vinh-hau', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82109058', N'Phước Long', N'Phuoc Long', N'Xã Phước Long', N'Phuoc Long Commune', 'phuoc-long', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82109059', N'Vĩnh Phước', N'Vinh Phuoc', N'Xã Vĩnh Phước', N'Vinh Phuoc Commune', 'vinh-phuoc', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82109060', N'Phong Hiệp', N'Phong Hiep', N'Xã Phong Hiệp', N'Phong Hiep Commune', 'phong-hiep', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82109061', N'Vĩnh Thanh', N'Vinh Thanh', N'Xã Vĩnh Thanh', N'Vinh Thanh Commune', 'vinh-thanh', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82105062', N'Vĩnh Lợi', N'Vinh Loi', N'Xã Vĩnh Lợi', N'Vinh Loi Commune', 'vinh-loi', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82105063', N'Hưng Hội', N'Hung Hoi', N'Xã Hưng Hội', N'Hung Hoi Commune', 'hung-hoi', 8, N'823', NULL, NULL, 1, 1);
INSERT INTO ad_wards (Code, NameVi, NameEn, FullNameVi, FullNameEn, CodeName, AdministrativeUnitId, ProvinceCode, PhoneCode, ZipCode, SortOrder, IsActive)
VALUES (N'82105064', N'Châu Thới', N'Chau Thoi', N'Xã Châu Thới', N'Chau Thoi Commune', 'chau-thoi', 8, N'823', NULL, NULL, 1, 1);

GO

-- =====================================================
-- VERIFICATION QUERIES
-- =====================================================

SELECT COUNT(*) AS TotalProvinces FROM ad_provinces;
SELECT COUNT(*) AS TotalWards FROM ad_wards;

SELECT p.Code, p.NameVi, p.NameEn, COUNT(w.Code) AS WardCount
FROM ad_provinces p
LEFT JOIN ad_wards w ON w.ProvinceCode = p.Code
GROUP BY p.Code, p.NameVi, p.NameEn
ORDER BY WardCount DESC;

GO