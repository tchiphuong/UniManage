using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniManage.Core.Database
{
    #region ad_countries
    /// <summary>
    /// Entity class for table ad_countries
    /// </summary>
    [Table("ad_countries")]
    public class ad_countries
    {
        [Key]
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string NameVi { get; set; }
        [Required]
        [StringLength(100)]
        public string NameEn { get; set; }
        [StringLength(200)]
        public string FullNameVi { get; set; }
        [StringLength(200)]
        public string FullNameEn { get; set; }
        [StringLength(100)]
        public string CodeName { get; set; }
        [StringLength(20)]
        public string PhoneCode { get; set; }
        [Column(TypeName = "int")]
        public int? SortOrder { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [StringLength(100)]
        public string CountryType { get; set; }
        [StringLength(100)]
        public string CountrySubType { get; set; }
        [StringLength(100)]
        public string Sovereignty { get; set; }
        [StringLength(100)]
        public string Capital { get; set; }
        [StringLength(100)]
        public string CurrencyCode { get; set; }
        [StringLength(100)]
        public string CurrencyName { get; set; }
        [StringLength(10)]
        public string CountryCode3 { get; set; }
        [StringLength(10)]
        public string CountryNumber { get; set; }
        [StringLength(100)]
        public string InternetCountryCode { get; set; }
        [StringLength(100)]
        public string Flags { get; set; }
    }
    #endregion

    #region ad_provinces
    /// <summary>
    /// Entity class for table ad_provinces
    /// </summary>
    [Table("ad_provinces")]
    public class ad_provinces
    {
        [Key]
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string NameVi { get; set; }
        [Required]
        [StringLength(100)]
        public string NameEn { get; set; }
        [StringLength(200)]
        public string FullNameVi { get; set; }
        [StringLength(200)]
        public string FullNameEn { get; set; }
        [StringLength(100)]
        public string CodeName { get; set; }
        public int? AdministrativeUnitId { get; set; }
        public int? AdministrativeRegionId { get; set; }
        [Required]
        [StringLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string CountryCode { get; set; }
        [StringLength(20)]
        public string PhoneCode { get; set; }
        [StringLength(20)]
        public string ZipCode { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int SortOrder { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion

    #region ad_wards
    /// <summary>
    /// Entity class for table ad_wards
    /// </summary>
    [Table("ad_wards")]
    public class ad_wards
    {
        [Key]
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string NameVi { get; set; }
        [Required]
        [StringLength(100)]
        public string NameEn { get; set; }
        [StringLength(200)]
        public string FullNameVi { get; set; }
        [StringLength(200)]
        public string FullNameEn { get; set; }
        [StringLength(100)]
        public string CodeName { get; set; }
        public int? AdministrativeUnitId { get; set; }
        [Required]
        [StringLength(20)]
        public string ProvinceCode { get; set; }
        [StringLength(20)]
        public string PhoneCode { get; set; }
        [StringLength(20)]
        public string ZipCode { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int SortOrder { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion

    #region hr_attendance
    /// <summary>
    /// Entity class for table hr_attendance
    /// </summary>
    [Table("hr_attendance")]
    public class hr_attendance
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [Required]
        public DateTime AttendanceDate { get; set; }
        public DateTime? CheckInTime { get; set; }
        public DateTime? CheckOutTime { get; set; }
        [Required]
        [StringLength(20)]
        public string Status { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region hr_departments
    /// <summary>
    /// Entity class for table hr_departments
    /// </summary>
    [Table("hr_departments")]
    public class hr_departments
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string NameVi { get; set; }
        [Required]
        [StringLength(100)]
        public string NameEn { get; set; }
        [StringLength(255)]
        public string Description { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region hr_employee_shifts
    /// <summary>
    /// Entity class for table hr_employee_shifts
    /// </summary>
    [Table("hr_employee_shifts")]
    public class hr_employee_shifts
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [Required]
        [StringLength(20)]
        public string WorkShiftCode { get; set; }
        [Required]
        public DateTime WorkDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion

    #region hr_employees
    /// <summary>
    /// Entity class for table hr_employees
    /// </summary>
    [Table("hr_employees")]
    public class hr_employees
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [Required]
        [StringLength(150)]
        public string FullName { get; set; }
        [StringLength(20)]
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(50)]
        public string DepartmentCode { get; set; }
        [StringLength(50)]
        public string PositionCode { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region hr_positions
    /// <summary>
    /// Entity class for table hr_positions
    /// </summary>
    [Table("hr_positions")]
    public class hr_positions
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string PositionCode { get; set; }
        [Required]
        [StringLength(150)]
        public string NameVi { get; set; }
        [Required]
        [StringLength(150)]
        public string NameEn { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region hr_salaries
    /// <summary>
    /// Entity class for table hr_salaries
    /// </summary>
    [Table("hr_salaries")]
    public class hr_salaries
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [Required]
        public decimal SalaryAmount { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region hr_salary_history
    /// <summary>
    /// Entity class for table hr_salary_history
    /// </summary>
    [Table("hr_salary_history")]
    public class hr_salary_history
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int SalaryId { get; set; }
        [Required]
        public decimal SalaryAmount { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region hr_work_shifts
    /// <summary>
    /// Entity class for table hr_work_shifts
    /// </summary>
    [Table("hr_work_shifts")]
    public class hr_work_shifts
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        public TimeSpan StartTime { get; set; }
        [Required]
        public TimeSpan EndTime { get; set; }
        [StringLength(250)]
        public string Description { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion

    #region is_api_resources
    /// <summary>
    /// Entity class for table is_api_resources
    /// </summary>
    [Table("is_api_resources")]
    public class is_api_resources
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string DisplayName { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public string Scopes { get; set; }
        public string UserClaims { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool Enabled { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region is_api_scopes
    /// <summary>
    /// Entity class for table is_api_scopes
    /// </summary>
    [Table("is_api_scopes")]
    public class is_api_scopes
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string DisplayName { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public string UserClaims { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool Enabled { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool Required { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool Emphasize { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool ShowInDiscoveryDocument { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region is_clients
    /// <summary>
    /// Entity class for table is_clients
    /// </summary>
    [Table("is_clients")]
    public class is_clients
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string ClientId { get; set; }
        [StringLength(200)]
        public string ClientName { get; set; }
        public string ClientSecrets { get; set; }
        [Required]
        public string AllowedGrantTypes { get; set; }
        public string AllowedScopes { get; set; }
        public string RedirectUris { get; set; }
        public string PostLogoutRedirectUris { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool AllowOfflineAccess { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int AccessTokenLifetime { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int RefreshTokenUsage { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int RefreshTokenExpiration { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int SlidingRefreshTokenLifetime { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int AbsoluteRefreshTokenLifetime { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool Enabled { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool RequirePkce { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool AllowPlainTextPkce { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region is_device_codes
    /// <summary>
    /// Entity class for table is_device_codes
    /// </summary>
    [Table("is_device_codes")]
    public class is_device_codes
    {
        [Key]
        [Required]
        [StringLength(200)]
        public string UserCode { get; set; }
        [Required]
        [StringLength(200)]
        public string DeviceCode { get; set; }
        [StringLength(200)]
        public string SubjectId { get; set; }
        [StringLength(100)]
        public string SessionId { get; set; }
        [Required]
        [StringLength(200)]
        public string ClientId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        [Required]
        public DateTime Expiration { get; set; }
        [Required]
        public string Data { get; set; }
    }
    #endregion

    #region is_identity_resources
    /// <summary>
    /// Entity class for table is_identity_resources
    /// </summary>
    [Table("is_identity_resources")]
    public class is_identity_resources
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [StringLength(200)]
        public string DisplayName { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        public string UserClaims { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool Enabled { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool Required { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool Emphasize { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool ShowInDiscoveryDocument { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region is_persisted_grants
    /// <summary>
    /// Entity class for table is_persisted_grants
    /// </summary>
    [Table("is_persisted_grants")]
    public class is_persisted_grants
    {
        [Key]
        [Required]
        [StringLength(200)]
        public string Key { get; set; }
        [Required]
        [StringLength(50)]
        public string Type { get; set; }
        [StringLength(200)]
        public string SubjectId { get; set; }
        [StringLength(100)]
        public string SessionId { get; set; }
        [Required]
        [StringLength(200)]
        public string ClientId { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        public DateTime CreationTime { get; set; }
        public DateTime? Expiration { get; set; }
        public DateTime? ConsumedTime { get; set; }
        [Required]
        public string Data { get; set; }
    }
    #endregion

    #region is_server_side_sessions
    /// <summary>
    /// Entity class for table is_server_side_sessions
    /// </summary>
    [Table("is_server_side_sessions")]
    public class is_server_side_sessions
    {
        [Key]
        [Required]
        [StringLength(200)]
        public string Key { get; set; }
        [Required]
        [StringLength(100)]
        public string Scheme { get; set; }
        [Required]
        [StringLength(200)]
        public string SubjectId { get; set; }
        [StringLength(100)]
        public string SessionId { get; set; }
        [StringLength(200)]
        public string DisplayName { get; set; }
        [Required]
        public DateTime Created { get; set; }
        [Required]
        public DateTime Renewed { get; set; }
        public DateTime? Expires { get; set; }
        [Required]
        public string Data { get; set; }
    }
    #endregion

    #region it_item_brand
    /// <summary>
    /// Entity class for table it_item_brand
    /// </summary>
    [Table("it_item_brand")]
    public class it_item_brand
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
    }
    #endregion

    #region it_item_category
    /// <summary>
    /// Entity class for table it_item_category
    /// </summary>
    [Table("it_item_category")]
    public class it_item_category
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
    #endregion

    #region it_item_color
    /// <summary>
    /// Entity class for table it_item_color
    /// </summary>
    [Table("it_item_color")]
    public class it_item_color
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
    #endregion

    #region it_item_details
    /// <summary>
    /// Entity class for table it_item_details
    /// </summary>
    [Table("it_item_details")]
    public class it_item_details
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ItemCode { get; set; }
        [Column(TypeName = "int")]
        public int? Type { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Key { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string ValueVi { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string ValueEn { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string InsertBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? InsertOn { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string UpdateBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? UpdateOn { get; set; }
    }
    #endregion

    #region it_item_image
    /// <summary>
    /// Entity class for table it_item_image
    /// </summary>
    [Table("it_item_image")]
    public class it_item_image
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; }
        [Required]
        [StringLength(500)]
        public string ImageUrl { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsThumbnail { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int SortOrder { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
    }
    #endregion

    #region it_item_inventory
    /// <summary>
    /// Entity class for table it_item_inventory
    /// </summary>
    [Table("it_item_inventory")]
    public class it_item_inventory
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Quantity { get; set; }
        [StringLength(100)]
        public string WarehouseLocation { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime UpdatedAt { get; set; }
    }
    #endregion

    #region it_item_price
    /// <summary>
    /// Entity class for table it_item_price
    /// </summary>
    [Table("it_item_price")]
    public class it_item_price
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
    }
    #endregion

    #region it_item_review
    /// <summary>
    /// Entity class for table it_item_review
    /// </summary>
    [Table("it_item_review")]
    public class it_item_review
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; }
        [Required]
        public int Rating { get; set; }
        public string Comment { get; set; }
        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
    }
    #endregion

    #region it_item_size
    /// <summary>
    /// Entity class for table it_item_size
    /// </summary>
    [Table("it_item_size")]
    public class it_item_size
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public int? ParentId { get; set; }
    }
    #endregion

    #region it_item_tag
    /// <summary>
    /// Entity class for table it_item_tag
    /// </summary>
    [Table("it_item_tag")]
    public class it_item_tag
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Tag { get; set; }
    }
    #endregion

    #region it_item_tag_map
    /// <summary>
    /// Entity class for table it_item_tag_map
    /// </summary>
    [Table("it_item_tag_map")]
    public class it_item_tag_map
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; }
        [Required]
        public int TagId { get; set; }
    }
    #endregion

    #region it_items
    /// <summary>
    /// Entity class for table it_items
    /// </summary>
    [Table("it_items")]
    public class it_items
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
        public string Description { get; set; }
        [StringLength(20)]
        public string BrandCode { get; set; }
        [StringLength(20)]
        public string CategoryCode { get; set; }
        [StringLength(20)]
        public string ColorCode { get; set; }
        [StringLength(20)]
        public string SizeCode { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
    }
    #endregion

    #region ms_currencies
    /// <summary>
    /// Entity class for table ms_currencies
    /// </summary>
    [Table("ms_currencies")]
    public class ms_currencies
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(10)]
        public string Code { get; set; }
        [Required]
        [StringLength(100)]
        public string NameVi { get; set; }
        [Required]
        [StringLength(100)]
        public string NameEn { get; set; }
        [StringLength(50)]
        public string Symbol { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
        [Required]
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion

    #region ms_units
    /// <summary>
    /// Entity class for table ms_units
    /// </summary>
    [Table("ms_units")]
    public class ms_units
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(100)]
        public string NameVi { get; set; }
        [StringLength(100)]
        public string NameEn { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region sa_customers
    /// <summary>
    /// Entity class for table sa_customers
    /// </summary>
    [Table("sa_customers")]
    public class sa_customers
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomerCode { get; set; }
        [Required]
        [StringLength(150)]
        public string FullName { get; set; }
        [StringLength(20)]
        public string PhoneNumber { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
        [StringLength(250)]
        public string Address { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion

    #region sa_order_items
    /// <summary>
    /// Entity class for table sa_order_items
    /// </summary>
    [Table("sa_order_items")]
    public class sa_order_items
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string OrderCode { get; set; }
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }
    }
    #endregion

    #region sa_orders
    /// <summary>
    /// Entity class for table sa_orders
    /// </summary>
    [Table("sa_orders")]
    public class sa_orders
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string OrderCode { get; set; }
        [Required]
        [StringLength(50)]
        public string CustomerCode { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? OrderDate { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? TotalAmount { get; set; }
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string Status { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        [StringLength(50)]
        public string EmployeeCode { get; set; }
    }
    #endregion

    #region sa_payments
    /// <summary>
    /// Entity class for table sa_payments
    /// </summary>
    [Table("sa_payments")]
    public class sa_payments
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string PaymentCode { get; set; }
        [Required]
        [StringLength(50)]
        public string OrderCode { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? PaymentDate { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [StringLength(50)]
        public string PaymentMethod { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion

    #region sy_actions
    /// <summary>
    /// Entity class for table sy_actions
    /// </summary>
    [Table("sy_actions")]
    public class sy_actions
    {
        [Required]
        public int Id { get; set; }
        [Key]
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        [Required]
        [StringLength(50)]
        public string ResourceKey { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [Required]
        public short Sort { get; set; }
        [StringLength(150)]
        public string Icon { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }
    }
    #endregion

    #region sy_commons
    /// <summary>
    /// Entity class for table sy_commons
    /// </summary>
    [Table("sy_commons")]
    public class sy_commons
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string TypeKey { get; set; }
        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string TypeNameVi { get; set; }
        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string TypeNameEn { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ValueKey { get; set; }
        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string ValueNameVi { get; set; }
        [Required]
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string ValueNameEn { get; set; }
        [Required]
        [Column(TypeName = "smallint")]
        public short Sort { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool Status { get; set; }
    }
    #endregion

    #region sy_function_mapping
    /// <summary>
    /// Entity class for table sy_function_mapping
    /// </summary>
    [Table("sy_function_mapping")]
    public class sy_function_mapping
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ActionCode { get; set; }
        [StringLength(50)]
        public string FunctionCode { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region sy_functions
    /// <summary>
    /// Entity class for table sy_functions
    /// </summary>
    [Table("sy_functions")]
    public class sy_functions
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        [Required]
        [StringLength(50)]
        public string ResourceKey { get; set; }
        [Required]
        [StringLength(50)]
        public string GroupCode { get; set; }
        [Required]
        public bool IsActive { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }
    }
    #endregion

    #region sy_images
    /// <summary>
    /// Entity class for table sy_images
    /// </summary>
    [Table("sy_images")]
    public class sy_images
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Functions { get; set; }
        [Column(TypeName = "varbinary")]
        public byte[] Images { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? UpdatedAt { get; set; }
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region sy_languages
    /// <summary>
    /// Entity class for table sy_languages
    /// </summary>
    [Table("sy_languages")]
    public class sy_languages
    {
        [Key]
        [Required]
        public int Id { get; set; }
        /// <summary>
        /// Mã ngôn ngữ theo chuẩn ISO (ví dụ: en, vi, zh-CN)
        /// </summary>
        [Required]
        [StringLength(10)]
        public string LanguageCode { get; set; }
        /// <summary>
        /// Tên đầy đủ của ngôn ngữ
        /// </summary>
        [Required]
        [StringLength(100)]
        public string LanguageName { get; set; }
        /// <summary>
        /// Biểu tượng ngôn ngữ (emoji hoặc URL ảnh)
        /// </summary>
        [StringLength(100)]
        public string Icon { get; set; }
        /// <summary>
        /// Ngôn ngữ mặc định của hệ thống (1 = có, 0 = không)
        /// </summary>
        [Required]
        [Column(TypeName = "bit")]
        public bool IsDefault { get; set; }
        /// <summary>
        /// Trạng thái ngôn ngữ: 1 = đang dùng, 0 = không dùng
        /// </summary>
        [Required]
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
        /// <summary>
        /// Người tạo bản ghi
        /// </summary>
        [StringLength(100)]
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày tạo bản ghi
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Người cập nhật bản ghi
        /// </summary>
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Ngày cập nhật bản ghi
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// Phiên bản dữ liệu (rowversion)
        /// </summary>
        [Required]
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region sy_menus
    /// <summary>
    /// Entity class for table sy_menus
    /// </summary>
    [Table("sy_menus")]
    public class sy_menus
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(50)]
        public string FunctionCode { get; set; }
        [StringLength(50)]
        public string ParentCode { get; set; }
        [Required]
        [StringLength(50)]
        public string ResourceKey { get; set; }
        [StringLength(500)]
        public string Url { get; set; }
        [StringLength(100)]
        public string Icon { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }
    }
    #endregion

    #region sy_refresh_tokens
    /// <summary>
    /// Entity class for table sy_refresh_tokens
    /// </summary>
    [Table("sy_refresh_tokens")]
    public class sy_refresh_tokens
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        public string Token { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public DateTime Expiration { get; set; }
        [Required]
        public bool IsRevoked { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }
    }
    #endregion

    #region sy_resources
    /// <summary>
    /// Entity class for table sy_resources
    /// </summary>
    [Table("sy_resources")]
    public class sy_resources
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ResourceKey { get; set; }
        [Required]
        public string ResourceValue { get; set; }
        [Required]
        [StringLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        public string SourceLanguage { get; set; }
        [Required]
        [StringLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        public string LanguageCode { get; set; }
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region sy_role_permissions
    /// <summary>
    /// Entity class for table sy_role_permissions
    /// </summary>
    [Table("sy_role_permissions")]
    public class sy_role_permissions
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string RoleCode { get; set; }
        [Required]
        [StringLength(50)]
        public string FunctionCode { get; set; }
        [Required]
        [StringLength(50)]
        public string ActionCode { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }
    }
    #endregion

    #region sy_roles
    /// <summary>
    /// Entity class for table sy_roles
    /// </summary>
    [Table("sy_roles")]
    public class sy_roles
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(250)]
        public string NameVi { get; set; }
        [StringLength(250)]
        public string NameEn { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        public DateTime? CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region sy_token_revoked
    /// <summary>
    /// Entity class for table sy_token_revoked
    /// </summary>
    [Table("sy_token_revoked")]
    public class sy_token_revoked
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(50)]
        public string Username { get; set; }
        public string Token { get; set; }
        public DateTime? TokenExpiration { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }
    }
    #endregion

    #region sy_tokens_management
    /// <summary>
    /// Entity class for table sy_tokens_management
    /// </summary>
    [Table("sy_tokens_management")]
    public class sy_tokens_management
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        [StringLength(500)]
        public string Token { get; set; }
        [Required]
        public DateTime TokenExpiration { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }
    }
    #endregion

    #region sy_users
    /// <summary>
    /// Entity class for table sy_users
    /// </summary>
    [Table("sy_users")]
    public class sy_users
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [StringLength(50)]
        public string Username { get; set; }
        [StringLength(150)]
        public string Password { get; set; }
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [StringLength(50)]
        public string RoleCode { get; set; }
        [StringLength(50)]
        public string Status { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }
        [StringLength(100)]
        public string Email { get; set; }
    }
    #endregion

    #region sysdiagrams
    /// <summary>
    /// Entity class for table sysdiagrams
    /// </summary>
    [Table("sysdiagrams")]
    public class sysdiagrams
    {
        [Required]
        [StringLength(128)]
        public string name { get; set; }
        [Required]
        public int principal_id { get; set; }
        [Key]
        [Required]
        public int diagram_id { get; set; }
        public int? version { get; set; }
        public byte[] definition { get; set; }
    }
    #endregion

    #region wf_approval_route
    /// <summary>
    /// Entity class for table wf_approval_route
    /// </summary>
    [Table("wf_approval_route")]
    public class wf_approval_route
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string RequestTypeKey { get; set; }
        [Required]
        [StringLength(255)]
        public string RouteName { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion

    #region wf_approval_route_approver
    /// <summary>
    /// Entity class for table wf_approval_route_approver
    /// </summary>
    [Table("wf_approval_route_approver")]
    public class wf_approval_route_approver
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ApprovalRouteLevelId { get; set; }
        [Required]
        [StringLength(50)]
        public string ApproverEmployeeCode { get; set; }
        [Required]
        [StringLength(100)]
        public string ApproverUsername { get; set; }
        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion

    #region wf_approval_route_level
    /// <summary>
    /// Entity class for table wf_approval_route_level
    /// </summary>
    [Table("wf_approval_route_level")]
    public class wf_approval_route_level
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int ApprovalRouteId { get; set; }
        [Required]
        public int LevelOrder { get; set; }
        [Required]
        [StringLength(255)]
        public string LevelName { get; set; }
        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion

    #region wf_request
    /// <summary>
    /// Entity class for table wf_request
    /// </summary>
    [Table("wf_request")]
    public class wf_request
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string RequestTypeKey { get; set; }
        [Required]
        [StringLength(50)]
        public string ApplicantEmployeeCode { get; set; }
        [Required]
        [StringLength(100)]
        public string ApplicantUsername { get; set; }
        public int? ApprovalRouteId { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        public string RequestData { get; set; }
        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion

    #region wf_request_approval
    /// <summary>
    /// Entity class for table wf_request_approval
    /// </summary>
    [Table("wf_request_approval")]
    public class wf_request_approval
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public int RequestId { get; set; }
        [Required]
        public int ApprovalRouteLevelId { get; set; }
        [Required]
        [StringLength(50)]
        public string ApproverEmployeeCode { get; set; }
        [Required]
        [StringLength(100)]
        public string ApproverUsername { get; set; }
        [Required]
        [StringLength(50)]
        public string ApprovalStatus { get; set; }
        [StringLength(1000)]
        public string ApprovalComment { get; set; }
        public DateTime? ApprovedAt { get; set; }
        [Required]
        [StringLength(100)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
    #endregion

}
