using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniManage.Core.Models.Entities
{
    #region ad_administrative_regions
    /// <summary>
    /// Entity class for table ad_administrative_regions
    /// </summary>
    [Table("ad_administrative_regions")]
    public class ad_administrative_regions
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string NameVi { get; set; }
        [Required]
        [StringLength(255)]
        public string NameEn { get; set; }
        [StringLength(255)]
        public string CodeNameVi { get; set; }
        [StringLength(255)]
        public string CodeNameEn { get; set; }
    }
    #endregion

    #region ad_administrative_units
    /// <summary>
    /// Entity class for table ad_administrative_units
    /// </summary>
    [Table("ad_administrative_units")]
    public class ad_administrative_units
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(255)]
        public string FullNameVi { get; set; }
        [StringLength(255)]
        public string FullNameEn { get; set; }
        [StringLength(255)]
        public string ShortNameVi { get; set; }
        [StringLength(255)]
        public string ShortNameEn { get; set; }
        [StringLength(255)]
        public string CodeNameVi { get; set; }
        [StringLength(255)]
        public string CodeNameEn { get; set; }
    }
    #endregion

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
        [StringLength(255)]
        public string CodeName { get; set; }
        [Required]
        [StringLength(255)]
        public string NameVi { get; set; }
        [StringLength(255)]
        public string NameEn { get; set; }
        [StringLength(255)]
        public string FullName { get; set; }
        [StringLength(255)]
        public string FullNameEn { get; set; }
    }
    #endregion

    #region ad_districts
    /// <summary>
    /// Entity class for table ad_districts
    /// </summary>
    [Table("ad_districts")]
    public class ad_districts
    {
        [Key]
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(255)]
        public string NameVi { get; set; }
        [StringLength(255)]
        public string NameEn { get; set; }
        [StringLength(255)]
        public string FullName { get; set; }
        [StringLength(255)]
        public string FullNameEn { get; set; }
        [StringLength(255)]
        public string CodeName { get; set; }
        [StringLength(20)]
        public string ProvinceCode { get; set; }
        public int? AdministrativeUnitId { get; set; }
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
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Code { get; set; }
        [Required]
        [StringLength(255)]
        public string NameVi { get; set; }
        [StringLength(255)]
        public string NameEn { get; set; }
        [Required]
        [StringLength(255)]
        public string FullNameVi { get; set; }
        [StringLength(255)]
        public string FullNameEn { get; set; }
        [StringLength(255)]
        public string CodeName { get; set; }
        public int? AdministrativeUnitId { get; set; }
        public int? AdministrativeRegionId { get; set; }
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
        [StringLength(255)]
        public string NameVi { get; set; }
        [StringLength(255)]
        public string NameEn { get; set; }
        [StringLength(255)]
        public string FullNameVi { get; set; }
        [StringLength(255)]
        public string FullNameEn { get; set; }
        [StringLength(255)]
        public string CodeName { get; set; }
        [StringLength(20)]
        public string DistrictCode { get; set; }
        public int? AdministrativeUnitId { get; set; }
    }
    #endregion

    #region ms_employees
    /// <summary>
    /// Entity class for table ms_employees
    /// </summary>
    [Table("ms_employees")]
    public class ms_employees
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Key]
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string FullNameVi { get; set; }
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string FullNameEn { get; set; }
        [StringLength(255)]
        public string FirstNameVi { get; set; }
        [StringLength(255)]
        public string FirstNameEn { get; set; }
        [StringLength(255)]
        public string LastNameVi { get; set; }
        [StringLength(255)]
        public string LastNameEn { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string PositionCode { get; set; }
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string Email { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Phone { get; set; }
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string GenderCode { get; set; }
        [Column(TypeName = "varchar")]
        public string Images { get; set; }
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string StatusCode { get; set; }
        [Column(TypeName = "int")]
        public int? TypeCode { get; set; }
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
        public byte[] DataRowVersion { get; set; }
    }
    #endregion

    #region ms_item_color
    /// <summary>
    /// Entity class for table ms_item_color
    /// </summary>
    [Table("ms_item_color")]
    public class ms_item_color
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string NameVi { get; set; }
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string NameEn { get; set; }
    }
    #endregion

    #region ms_item_details
    /// <summary>
    /// Entity class for table ms_item_details
    /// </summary>
    [Table("ms_item_details")]
    public class ms_item_details
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
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

    #region ms_item_size
    /// <summary>
    /// Entity class for table ms_item_size
    /// </summary>
    [Table("ms_item_size")]
    public class ms_item_size
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string NameVi { get; set; }
        [StringLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string NameEn { get; set; }
    }
    #endregion

    #region ms_positions
    /// <summary>
    /// Entity class for table ms_positions
    /// </summary>
    [Table("ms_positions")]
    public class ms_positions
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Key]
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string NameVi { get; set; }
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string NameEn { get; set; }
        [Column(TypeName = "int")]
        public int? Status { get; set; }
        [StringLength(255)]
        [Column(TypeName = "nvarchar(255)")]
        public string Remark { get; set; }
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
        [StringLength(50)]
        public string InsertBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? InsertOn { get; set; }
        [StringLength(50)]
        public string UpdateBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? UpdateOn { get; set; }
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

    #region sy_function_group
    /// <summary>
    /// Entity class for table sy_function_group
    /// </summary>
    [Table("sy_function_group")]
    public class sy_function_group
    {
        [Required]
        public long Id { get; set; }
        [Key]
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Code { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ResourceKey { get; set; }
        [Column(TypeName = "bit")]
        public bool? IsActive { get; set; }
        [Column(TypeName = "int")]
        public int? Sort { get; set; }
        [StringLength(150)]
        [Column(TypeName = "nvarchar(150)")]
        public string Icon { get; set; }
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
        [Column(TypeName = "varchar(50)")]
        public string ActionCode { get; set; }
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string FunctionCode { get; set; }
        /// <summary>
        /// Ngu?i t?o b?n ghi
        /// </summary>
        [StringLength(100)]
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày t?o b?n ghi
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ngu?i c?p nh?t b?n ghi
        /// </summary>
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Ngày c?p nh?t b?n ghi
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// Phiên b?n d? li?u (rowversion)
        /// </summary>
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
        [Required]
        public long Id { get; set; }
        [Key]
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Code { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string ResourceKey { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string GroupCode { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
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
        /// Mã ngôn ng? theo chu?n ISO (ví d?: en, vi, zh-CN)
        /// </summary>
        [Required]
        [StringLength(10)]
        public string LanguageCode { get; set; }
        /// <summary>
        /// Tên d?y d? c?a ngôn ng?
        /// </summary>
        [Required]
        [StringLength(100)]
        public string LanguageName { get; set; }
        /// <summary>
        /// Bi?u tu?ng ngôn ng? (emoji ho?c URL ?nh)
        /// </summary>
        [StringLength(100)]
        public string Icon { get; set; }
        /// <summary>
        /// Ngôn ng? m?c d?nh c?a h? th?ng (1 = có, 0 = không)
        /// </summary>
        [Required]
        [Column(TypeName = "bit")]
        public bool IsDefault { get; set; }
        /// <summary>
        /// Tr?ng thái ngôn ng?: 1 = dang dùng, 0 = không dùng
        /// </summary>
        [Required]
        [Column(TypeName = "bit")]
        public bool IsActive { get; set; }
        /// <summary>
        /// Ngu?i t?o b?n ghi
        /// </summary>
        [StringLength(100)]
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày t?o b?n ghi
        /// </summary>
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// Ngu?i c?p nh?t b?n ghi
        /// </summary>
        [StringLength(100)]
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Ngày c?p nh?t b?n ghi
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
        /// <summary>
        /// Phiên b?n d? li?u (rowversion)
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
        [Required]
        public int Id { get; set; }
        [Key]
        [Required]
        [StringLength(50)]
        public string Code { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
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
        public DateTime? CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
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
        public string Token { get; set; }
        [Required]
        public long UserId { get; set; }
        [Required]
        public DateTime Expiration { get; set; }
        [Required]
        public bool IsRevoked { get; set; }
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
    }
    #endregion

    #region sy_roles
    /// <summary>
    /// Entity class for table sy_roles
    /// </summary>
    [Table("sy_roles")]
    public class sy_roles
    {
        [Required]
        public long Id { get; set; }
        [Key]
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
        [Column(TypeName = "datetime2")]
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
        [Required]
        public int Id { get; set; }
        [StringLength(2147483647)]
        public string Token { get; set; }
        public DateTime? TokenExpiration { get; set; }
        public DateTime? InsertOn { get; set; }
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
        [Required]
        [StringLength(50)]
        public string InsertOn { get; set; }
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

}
