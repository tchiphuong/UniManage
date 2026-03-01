using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniManage.Model.Attributes;

namespace UniManage.Model.Entities
{
    #region ac_accounts
    /// <summary>
    /// Entity class for table ac_accounts
    /// </summary>
    [Table("ac_accounts")]
    
    public class ac_accounts
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string AccountCode { get; set; }
        [Required]
        [StringLength(255)]
        public string AccountName { get; set; }
        [StringLength(255)]
        public string AccountNameEn { get; set; }
        [Required]
        [StringLength(50)]
        public string AccountType { get; set; }
        [ReferencedKey(nameof(ac_accounts.AccountCode))]
        [StringLength(50)]
        public string ParentAccountCode { get; set; }
        [Required]
        [Column(TypeName = "tinyint")]
        public byte Level { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsDetail { get; set; }
        [Required]
        [StringLength(10)]
        public string NormalBalance { get; set; }
        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Currency { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for ac_accounts (ParentAccountCode)
        /// </summary>
        public virtual ac_accounts? ac_accountsNavigation { get; set; }

        /// <summary>
        /// Collection navigation property for ac_accounts
        /// </summary>
        public virtual ICollection<ac_accounts>? ac_accountsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for ac_bank_accounts
        /// </summary>
        public virtual ICollection<ac_bank_accounts>? ac_bank_accountsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for ac_journal_entry_lines
        /// </summary>
        public virtual ICollection<ac_journal_entry_lines>? ac_journal_entry_linesCollection { get; set; }
    }
    #endregion

    #region ac_bank_accounts
    /// <summary>
    /// Entity class for table ac_bank_accounts
    /// </summary>
    [Table("ac_bank_accounts")]
    
    public class ac_bank_accounts
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string AccountCode { get; set; }
        [Required]
        [StringLength(255)]
        public string BankName { get; set; }
        [StringLength(255)]
        public string BankBranch { get; set; }
        [Required]
        [StringLength(50)]
        public string AccountNumber { get; set; }
        [Required]
        [StringLength(255)]
        public string AccountName { get; set; }
        [Required]
        [StringLength(50)]
        public string AccountType { get; set; }
        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Currency { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }
        [ReferencedKey(nameof(ac_accounts.AccountCode))]
        [Required]
        [StringLength(50)]
        public string GLAccountCode { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for ac_accounts (GLAccountCode)
        /// </summary>
        public virtual ac_accounts? ac_accounts { get; set; }

        /// <summary>
        /// Collection navigation property for ac_bank_transactions
        /// </summary>
        public virtual ICollection<ac_bank_transactions>? ac_bank_transactionsCollection { get; set; }
    }
    #endregion

    #region ac_bank_transactions
    /// <summary>
    /// Entity class for table ac_bank_transactions
    /// </summary>
    [Table("ac_bank_transactions")]
    
    public class ac_bank_transactions
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string TransactionNumber { get; set; }
        [ReferencedKey(nameof(ac_bank_accounts.AccountCode))]
        [Required]
        [StringLength(50)]
        public string BankAccountCode { get; set; }
        [Required]
        public DateTime TransactionDate { get; set; }
        [Required]
        [StringLength(50)]
        public string TransactionType { get; set; }
        [Required]
        public decimal Amount { get; set; }
        [StringLength(100)]
        public string ReferenceNumber { get; set; }
        [StringLength(255)]
        public string Payee { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [ForeignKey(nameof(ac_journal_entries))]
        public long? JournalEntryId { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for ac_bank_accounts (BankAccountCode)
        /// </summary>
        public virtual ac_bank_accounts? ac_bank_accounts { get; set; }

        /// <summary>
        /// Navigation property for ac_journal_entries (JournalEntryId)
        /// </summary>
        public virtual ac_journal_entries? ac_journal_entries { get; set; }
    }
    #endregion

    #region ac_depreciation_schedule
    /// <summary>
    /// Entity class for table ac_depreciation_schedule
    /// </summary>
    [Table("ac_depreciation_schedule")]
    
    public class ac_depreciation_schedule
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ReferencedKey(nameof(ac_fixed_assets.AssetCode))]
        [Required]
        [StringLength(50)]
        public string AssetCode { get; set; }
        [ReferencedKey(nameof(ac_fiscal_periods.PeriodCode))]
        [Required]
        [StringLength(50)]
        public string PeriodCode { get; set; }
        [Required]
        public DateTime DepreciationDate { get; set; }
        [Required]
        public decimal DepreciationAmount { get; set; }
        [Required]
        public decimal AccumulatedDepreciation { get; set; }
        [Required]
        public decimal BookValue { get; set; }
        [ForeignKey(nameof(ac_journal_entries))]
        public long? JournalEntryId { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for ac_fixed_assets (AssetCode)
        /// </summary>
        public virtual ac_fixed_assets? ac_fixed_assets { get; set; }

        /// <summary>
        /// Navigation property for ac_journal_entries (JournalEntryId)
        /// </summary>
        public virtual ac_journal_entries? ac_journal_entries { get; set; }

        /// <summary>
        /// Navigation property for ac_fiscal_periods (PeriodCode)
        /// </summary>
        public virtual ac_fiscal_periods? ac_fiscal_periods { get; set; }
    }
    #endregion

    #region ac_fiscal_periods
    /// <summary>
    /// Entity class for table ac_fiscal_periods
    /// </summary>
    [Table("ac_fiscal_periods")]
    
    public class ac_fiscal_periods
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ReferencedKey(nameof(ac_fiscal_years.FiscalYearCode))]
        [Required]
        [StringLength(50)]
        public string FiscalYearCode { get; set; }
        [Required]
        [StringLength(50)]
        public string PeriodCode { get; set; }
        [Required]
        [StringLength(255)]
        public string PeriodName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsClosed { get; set; }
        public DateTime? ClosedDate { get; set; }
        [StringLength(255)]
        public string ClosedBy { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for ac_fiscal_years (FiscalYearCode)
        /// </summary>
        public virtual ac_fiscal_years? ac_fiscal_years { get; set; }

        /// <summary>
        /// Collection navigation property for ac_depreciation_schedule
        /// </summary>
        public virtual ICollection<ac_depreciation_schedule>? ac_depreciation_scheduleCollection { get; set; }

        /// <summary>
        /// Collection navigation property for ac_journal_entries
        /// </summary>
        public virtual ICollection<ac_journal_entries>? ac_journal_entriesCollection { get; set; }
    }
    #endregion

    #region ac_fiscal_years
    /// <summary>
    /// Entity class for table ac_fiscal_years
    /// </summary>
    [Table("ac_fiscal_years")]
    
    public class ac_fiscal_years
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FiscalYearCode { get; set; }
        [Required]
        [StringLength(255)]
        public string FiscalYearName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Collection navigation property for ac_fiscal_periods
        /// </summary>
        public virtual ICollection<ac_fiscal_periods>? ac_fiscal_periodsCollection { get; set; }
    }
    #endregion

    #region ac_fixed_assets
    /// <summary>
    /// Entity class for table ac_fixed_assets
    /// </summary>
    [Table("ac_fixed_assets")]
    
    public class ac_fixed_assets
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetCode { get; set; }
        [Required]
        [StringLength(255)]
        public string AssetName { get; set; }
        [Required]
        [StringLength(50)]
        public string AssetCategory { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [Required]
        public decimal PurchaseCost { get; set; }
        [Required]
        [StringLength(50)]
        public string DepreciationMethod { get; set; }
        [Required]
        public int UsefulLife { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal SalvageValue { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal AccumulatedDepreciation { get; set; }
        public decimal? BookValue { get; set; }
        [StringLength(50)]
        public string Department { get; set; }
        [StringLength(255)]
        public string Location { get; set; }
        [StringLength(100)]
        public string SerialNumber { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        public DateTime? DisposalDate { get; set; }
        public decimal? DisposalAmount { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Collection navigation property for ac_depreciation_schedule
        /// </summary>
        public virtual ICollection<ac_depreciation_schedule>? ac_depreciation_scheduleCollection { get; set; }
    }
    #endregion

    #region ac_journal_entries
    /// <summary>
    /// Entity class for table ac_journal_entries
    /// </summary>
    [Table("ac_journal_entries")]
    
    public class ac_journal_entries
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string EntryNumber { get; set; }
        [Required]
        public DateTime EntryDate { get; set; }
        [ReferencedKey(nameof(ac_fiscal_periods.PeriodCode))]
        [Required]
        [StringLength(50)]
        public string PeriodCode { get; set; }
        [Required]
        [StringLength(50)]
        public string EntryType { get; set; }
        [StringLength(50)]
        public string ReferenceType { get; set; }
        public long? ReferenceId { get; set; }
        [StringLength(100)]
        public string ReferenceNumber { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public decimal TotalDebit { get; set; }
        [Required]
        public decimal TotalCredit { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        public DateTime? PostedDate { get; set; }
        [StringLength(255)]
        public string PostedBy { get; set; }
        public DateTime? ReversedDate { get; set; }
        [StringLength(255)]
        public string ReversedBy { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for ac_fiscal_periods (PeriodCode)
        /// </summary>
        public virtual ac_fiscal_periods? ac_fiscal_periods { get; set; }

        /// <summary>
        /// Collection navigation property for ac_bank_transactions
        /// </summary>
        public virtual ICollection<ac_bank_transactions>? ac_bank_transactionsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for ac_depreciation_schedule
        /// </summary>
        public virtual ICollection<ac_depreciation_schedule>? ac_depreciation_scheduleCollection { get; set; }

        /// <summary>
        /// Collection navigation property for ac_journal_entry_lines
        /// </summary>
        public virtual ICollection<ac_journal_entry_lines>? ac_journal_entry_linesCollection { get; set; }
    }
    #endregion

    #region ac_journal_entry_lines
    /// <summary>
    /// Entity class for table ac_journal_entry_lines
    /// </summary>
    [Table("ac_journal_entry_lines")]
    
    public class ac_journal_entry_lines
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ForeignKey(nameof(ac_journal_entries))]
        [Required]
        public long EntryId { get; set; }
        [Required]
        public int LineNumber { get; set; }
        [ReferencedKey(nameof(ac_accounts.AccountCode))]
        [Required]
        [StringLength(50)]
        public string AccountCode { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DebitAmount { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CreditAmount { get; set; }
        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Currency { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 6)")]
        public decimal ExchangeRate { get; set; }
        [StringLength(50)]
        public string Department { get; set; }
        [StringLength(50)]
        public string CostCenter { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for ac_accounts (AccountCode)
        /// </summary>
        public virtual ac_accounts? ac_accounts { get; set; }

        /// <summary>
        /// Navigation property for ac_journal_entries (EntryId)
        /// </summary>
        public virtual ac_journal_entries? ac_journal_entries { get; set; }
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
        [StringLength(50)]
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
        [StringLength(50)]
        public string CurrencyCode { get; set; }
        [StringLength(100)]
        public string CurrencyName { get; set; }
        [StringLength(50)]
        public string CountryCode3 { get; set; }
        [StringLength(10)]
        public string CountryNumber { get; set; }
        [StringLength(50)]
        public string InternetCountryCode { get; set; }
        [StringLength(100)]
        public string Flags { get; set; }

        /// <summary>
        /// Collection navigation property for ad_provinces
        /// </summary>
        public virtual ICollection<ad_provinces>? ad_provincesCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_suppliers
        /// </summary>
        public virtual ICollection<pr_suppliers>? pr_suppliersCollection { get; set; }
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
        [ForeignKey(nameof(ad_countries))]
        [Required]
        [StringLength(20)]
        public string CountryCode { get; set; }
        [StringLength(50)]
        public string PhoneCode { get; set; }
        [StringLength(50)]
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

        /// <summary>
        /// Navigation property for ad_countries (CountryCode)
        /// </summary>
        public virtual ad_countries? ad_countries { get; set; }

        /// <summary>
        /// Collection navigation property for ad_wards
        /// </summary>
        public virtual ICollection<ad_wards>? ad_wardsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_suppliers
        /// </summary>
        public virtual ICollection<pr_suppliers>? pr_suppliersCollection { get; set; }
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
        [ForeignKey(nameof(ad_provinces))]
        [Required]
        [StringLength(20)]
        public string ProvinceCode { get; set; }
        [StringLength(50)]
        public string PhoneCode { get; set; }
        [StringLength(50)]
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

        /// <summary>
        /// Navigation property for ad_provinces (ProvinceCode)
        /// </summary>
        public virtual ad_provinces? ad_provinces { get; set; }

        /// <summary>
        /// Collection navigation property for pr_suppliers
        /// </summary>
        public virtual ICollection<pr_suppliers>? pr_suppliersCollection { get; set; }
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
        [ReferencedKey(nameof(hr_employees.EmployeeCode))]
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

        /// <summary>
        /// Navigation property for hr_employees (EmployeeCode)
        /// </summary>
        public virtual hr_employees? hr_employees { get; set; }
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

        /// <summary>
        /// Collection navigation property for hr_employees
        /// </summary>
        public virtual ICollection<hr_employees>? hr_employeesCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_requisitions
        /// </summary>
        public virtual ICollection<pr_requisitions>? pr_requisitionsCollection { get; set; }
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
        [ReferencedKey(nameof(hr_employees.EmployeeCode))]
        [Required]
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [ReferencedKey(nameof(hr_work_shifts.Code))]
        [Required]
        [StringLength(20)]
        public string WorkShiftCode { get; set; }
        [Required]
        public DateTime WorkDate { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Navigation property for hr_employees (EmployeeCode)
        /// </summary>
        public virtual hr_employees? hr_employees { get; set; }

        /// <summary>
        /// Navigation property for hr_work_shifts (WorkShiftCode)
        /// </summary>
        public virtual hr_work_shifts? hr_work_shifts { get; set; }
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
        [ReferencedKey(nameof(hr_departments.Code))]
        [StringLength(50)]
        public string DepartmentCode { get; set; }
        [ReferencedKey(nameof(hr_positions.Code))]
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

        /// <summary>
        /// Navigation property for hr_departments (DepartmentCode)
        /// </summary>
        public virtual hr_departments? hr_departments { get; set; }

        /// <summary>
        /// Navigation property for hr_positions (PositionCode)
        /// </summary>
        public virtual hr_positions? hr_positions { get; set; }

        /// <summary>
        /// Collection navigation property for hr_attendance
        /// </summary>
        public virtual ICollection<hr_attendance>? hr_attendanceCollection { get; set; }

        /// <summary>
        /// Collection navigation property for hr_employee_shifts
        /// </summary>
        public virtual ICollection<hr_employee_shifts>? hr_employee_shiftsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for hr_salaries
        /// </summary>
        public virtual ICollection<hr_salaries>? hr_salariesCollection { get; set; }

        /// <summary>
        /// Collection navigation property for sa_orders
        /// </summary>
        public virtual ICollection<sa_orders>? sa_ordersCollection { get; set; }

        /// <summary>
        /// Collection navigation property for sy_users
        /// </summary>
        public virtual ICollection<sy_users>? sy_usersCollection { get; set; }

        /// <summary>
        /// Collection navigation property for wf_approval_route_approver
        /// </summary>
        public virtual ICollection<wf_approval_route_approver>? wf_approval_route_approverCollection { get; set; }

        /// <summary>
        /// Collection navigation property for wf_request
        /// </summary>
        public virtual ICollection<wf_request>? wf_requestCollection { get; set; }

        /// <summary>
        /// Collection navigation property for wf_request_approval
        /// </summary>
        public virtual ICollection<wf_request_approval>? wf_request_approvalCollection { get; set; }
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
        public string Code { get; set; }
        [Required]
        [StringLength(150)]
        public string NameVi { get; set; }
        [Required]
        [StringLength(150)]
        public string NameEn { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
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

        /// <summary>
        /// Collection navigation property for hr_employees
        /// </summary>
        public virtual ICollection<hr_employees>? hr_employeesCollection { get; set; }
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
        [ReferencedKey(nameof(hr_employees.EmployeeCode))]
        [Required]
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [Required]
        public decimal SalaryAmount { get; set; }
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

        /// <summary>
        /// Navigation property for hr_employees (EmployeeCode)
        /// </summary>
        public virtual hr_employees? hr_employees { get; set; }

        /// <summary>
        /// Collection navigation property for hr_salary_history
        /// </summary>
        public virtual ICollection<hr_salary_history>? hr_salary_historyCollection { get; set; }
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
        [ForeignKey(nameof(hr_salaries))]
        [Required]
        public int SalaryId { get; set; }
        [Required]
        public decimal SalaryAmount { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        public DateTime? ToDate { get; set; }
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

        /// <summary>
        /// Navigation property for hr_salaries (SalaryId)
        /// </summary>
        public virtual hr_salaries? hr_salaries { get; set; }
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

        /// <summary>
        /// Collection navigation property for hr_employee_shifts
        /// </summary>
        public virtual ICollection<hr_employee_shifts>? hr_employee_shiftsCollection { get; set; }
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
        [StringLength(50)]
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

        /// <summary>
        /// Collection navigation property for it_items
        /// </summary>
        public virtual ICollection<it_items>? it_itemsCollection { get; set; }
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
        [ForeignKey(nameof(it_item_category))]
        public int? ParentId { get; set; }

        /// <summary>
        /// Navigation property for it_item_category (ParentId)
        /// </summary>
        public virtual it_item_category? it_item_categoryNavigation { get; set; }

        /// <summary>
        /// Collection navigation property for it_item_category
        /// </summary>
        public virtual ICollection<it_item_category>? it_item_categoryCollection { get; set; }

        /// <summary>
        /// Collection navigation property for it_items
        /// </summary>
        public virtual ICollection<it_items>? it_itemsCollection { get; set; }
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

        /// <summary>
        /// Collection navigation property for it_items
        /// </summary>
        public virtual ICollection<it_items>? it_itemsCollection { get; set; }
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
        [ReferencedKey(nameof(it_items.Code))]
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

        /// <summary>
        /// Navigation property for it_items (ItemCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }
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
        [ReferencedKey(nameof(it_items.Code))]
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

        /// <summary>
        /// Navigation property for it_items (ItemCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }
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
        [ReferencedKey(nameof(it_items.Code))]
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

        /// <summary>
        /// Navigation property for it_items (ItemCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }
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
        [ReferencedKey(nameof(it_items.Code))]
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

        /// <summary>
        /// Navigation property for it_items (ItemCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }
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
        [ReferencedKey(nameof(it_items.Code))]
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; }
        [Required]
        public int Rating { get; set; }
        public string Comment { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Navigation property for it_items (ItemCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }
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
        [ForeignKey(nameof(it_item_size))]
        public int? ParentId { get; set; }

        /// <summary>
        /// Navigation property for it_item_size (ParentId)
        /// </summary>
        public virtual it_item_size? it_item_sizeNavigation { get; set; }

        /// <summary>
        /// Collection navigation property for it_item_size
        /// </summary>
        public virtual ICollection<it_item_size>? it_item_sizeCollection { get; set; }

        /// <summary>
        /// Collection navigation property for it_items
        /// </summary>
        public virtual ICollection<it_items>? it_itemsCollection { get; set; }
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

        /// <summary>
        /// Collection navigation property for it_item_tag_map
        /// </summary>
        public virtual ICollection<it_item_tag_map>? it_item_tag_mapCollection { get; set; }
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
        [ReferencedKey(nameof(it_items.Code))]
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; }
        [ForeignKey(nameof(it_item_tag))]
        [Required]
        public int TagId { get; set; }

        /// <summary>
        /// Navigation property for it_items (ItemCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }

        /// <summary>
        /// Navigation property for it_item_tag (TagId)
        /// </summary>
        public virtual it_item_tag? it_item_tag { get; set; }
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
        [ReferencedKey(nameof(it_item_brand.Code))]
        [StringLength(20)]
        public string BrandCode { get; set; }
        [ReferencedKey(nameof(it_item_category.Code))]
        [StringLength(20)]
        public string CategoryCode { get; set; }
        [ReferencedKey(nameof(it_item_color.Code))]
        [StringLength(20)]
        public string ColorCode { get; set; }
        [ReferencedKey(nameof(it_item_size.Code))]
        [StringLength(20)]
        public string SizeCode { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Navigation property for it_item_brand (BrandCode)
        /// </summary>
        public virtual it_item_brand? it_item_brand { get; set; }

        /// <summary>
        /// Navigation property for it_item_category (CategoryCode)
        /// </summary>
        public virtual it_item_category? it_item_category { get; set; }

        /// <summary>
        /// Navigation property for it_item_color (ColorCode)
        /// </summary>
        public virtual it_item_color? it_item_color { get; set; }

        /// <summary>
        /// Navigation property for it_item_size (SizeCode)
        /// </summary>
        public virtual it_item_size? it_item_size { get; set; }

        /// <summary>
        /// Collection navigation property for it_item_details
        /// </summary>
        public virtual ICollection<it_item_details>? it_item_detailsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for it_item_image
        /// </summary>
        public virtual ICollection<it_item_image>? it_item_imageCollection { get; set; }

        /// <summary>
        /// Collection navigation property for it_item_inventory
        /// </summary>
        public virtual ICollection<it_item_inventory>? it_item_inventoryCollection { get; set; }

        /// <summary>
        /// Collection navigation property for it_item_price
        /// </summary>
        public virtual ICollection<it_item_price>? it_item_priceCollection { get; set; }

        /// <summary>
        /// Collection navigation property for it_item_review
        /// </summary>
        public virtual ICollection<it_item_review>? it_item_reviewCollection { get; set; }

        /// <summary>
        /// Collection navigation property for it_item_tag_map
        /// </summary>
        public virtual ICollection<it_item_tag_map>? it_item_tag_mapCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_bom
        /// </summary>
        public virtual ICollection<mf_bom>? mf_bomCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_bom_items
        /// </summary>
        public virtual ICollection<mf_bom_items>? mf_bom_itemsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_cost_actuals
        /// </summary>
        public virtual ICollection<mf_cost_actuals>? mf_cost_actualsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_cost_standards
        /// </summary>
        public virtual ICollection<mf_cost_standards>? mf_cost_standardsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_material_issues
        /// </summary>
        public virtual ICollection<mf_material_issues>? mf_material_issuesCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_production_order_materials
        /// </summary>
        public virtual ICollection<mf_production_order_materials>? mf_production_order_materialsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_production_orders
        /// </summary>
        public virtual ICollection<mf_production_orders>? mf_production_ordersCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_purchase_order_items
        /// </summary>
        public virtual ICollection<pr_purchase_order_items>? pr_purchase_order_itemsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_requisition_items
        /// </summary>
        public virtual ICollection<pr_requisition_items>? pr_requisition_itemsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_rfq_items
        /// </summary>
        public virtual ICollection<pr_rfq_items>? pr_rfq_itemsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for sa_order_items
        /// </summary>
        public virtual ICollection<sa_order_items>? sa_order_itemsCollection { get; set; }
    }
    #endregion

    #region mf_bom
    /// <summary>
    /// Entity class for table mf_bom
    /// </summary>
    [Table("mf_bom")]
    
    public class mf_bom
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string BOMCode { get; set; }
        [ReferencedKey(nameof(it_items.Code))]
        [Required]
        [StringLength(50)]
        public string ProductCode { get; set; }
        [Required]
        [StringLength(255)]
        public string ProductName { get; set; }
        [Required]
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string BOMVersion { get; set; }
        [Required]
        public DateTime EffectiveDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string BOMType { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal BaseQuantity { get; set; }
        [Required]
        [StringLength(50)]
        public string BaseUnitCode { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(1000)]
        public string Notes { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for it_items (ProductCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }

        /// <summary>
        /// Collection navigation property for mf_bom_items
        /// </summary>
        public virtual ICollection<mf_bom_items>? mf_bom_itemsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_bom_operations
        /// </summary>
        public virtual ICollection<mf_bom_operations>? mf_bom_operationsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_production_orders
        /// </summary>
        public virtual ICollection<mf_production_orders>? mf_production_ordersCollection { get; set; }
    }
    #endregion

    #region mf_bom_items
    /// <summary>
    /// Entity class for table mf_bom_items
    /// </summary>
    [Table("mf_bom_items")]
    
    public class mf_bom_items
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ForeignKey(nameof(mf_bom))]
        [Required]
        public long BOMId { get; set; }
        [Required]
        public int LineNumber { get; set; }
        [ReferencedKey(nameof(it_items.Code))]
        [Required]
        [StringLength(50)]
        public string ComponentCode { get; set; }
        [Required]
        [StringLength(255)]
        public string ComponentName { get; set; }
        [Required]
        [StringLength(50)]
        public string ComponentType { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitCode { get; set; }
        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal ScrapRate { get; set; }
        public int? LeadTime { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsCritical { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for mf_bom (BOMId)
        /// </summary>
        public virtual mf_bom? mf_bom { get; set; }

        /// <summary>
        /// Navigation property for it_items (ComponentCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }

        /// <summary>
        /// Collection navigation property for mf_production_order_materials
        /// </summary>
        public virtual ICollection<mf_production_order_materials>? mf_production_order_materialsCollection { get; set; }
    }
    #endregion

    #region mf_bom_operations
    /// <summary>
    /// Entity class for table mf_bom_operations
    /// </summary>
    [Table("mf_bom_operations")]
    
    public class mf_bom_operations
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ForeignKey(nameof(mf_bom))]
        [Required]
        public long BOMId { get; set; }
        [Required]
        public int OperationSeq { get; set; }
        [Required]
        [StringLength(50)]
        public string OperationCode { get; set; }
        [Required]
        [StringLength(255)]
        public string OperationName { get; set; }
        [ReferencedKey(nameof(mf_work_centers.WorkCenterCode))]
        [Required]
        [StringLength(50)]
        public string WorkCenterCode { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int SetupTime { get; set; }
        [Required]
        public int RunTime { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int WaitTime { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int MoveTime { get; set; }
        public decimal? LaborCostPerHour { get; set; }
        public decimal? MachineCostPerHour { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for mf_bom (BOMId)
        /// </summary>
        public virtual mf_bom? mf_bom { get; set; }

        /// <summary>
        /// Navigation property for mf_work_centers (WorkCenterCode)
        /// </summary>
        public virtual mf_work_centers? mf_work_centers { get; set; }

        /// <summary>
        /// Collection navigation property for mf_production_order_operations
        /// </summary>
        public virtual ICollection<mf_production_order_operations>? mf_production_order_operationsCollection { get; set; }
    }
    #endregion

    #region mf_cost_actuals
    /// <summary>
    /// Entity class for table mf_cost_actuals
    /// </summary>
    [Table("mf_cost_actuals")]
    
    public class mf_cost_actuals
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ForeignKey(nameof(mf_production_orders))]
        [Required]
        public long ProductionOrderId { get; set; }
        [ReferencedKey(nameof(it_items.Code))]
        [Required]
        [StringLength(50)]
        public string ProductCode { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MaterialCost { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LaborCost { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OverheadCost { get; set; }
        public decimal? TotalCost { get; set; }
        [Required]
        public decimal ProducedQty { get; set; }
        public decimal? UnitCost { get; set; }
        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Currency { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CalculatedDate { get; set; }
        [StringLength(255)]
        public string CalculatedBy { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for it_items (ProductCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }

        /// <summary>
        /// Navigation property for mf_production_orders (ProductionOrderId)
        /// </summary>
        public virtual mf_production_orders? mf_production_orders { get; set; }
    }
    #endregion

    #region mf_cost_standards
    /// <summary>
    /// Entity class for table mf_cost_standards
    /// </summary>
    [Table("mf_cost_standards")]
    
    public class mf_cost_standards
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ReferencedKey(nameof(it_items.Code))]
        [Required]
        [StringLength(50)]
        public string ProductCode { get; set; }
        [Required]
        public DateTime EffectiveDate { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal MaterialCost { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal LaborCost { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OverheadCost { get; set; }
        public decimal? TotalCost { get; set; }
        [Required]
        [StringLength(10)]
        [Column(TypeName = "varchar(10)")]
        public string Currency { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for it_items (ProductCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }
    }
    #endregion

    #region mf_material_issues
    /// <summary>
    /// Entity class for table mf_material_issues
    /// </summary>
    [Table("mf_material_issues")]
    
    public class mf_material_issues
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string IssueNumber { get; set; }
        [ForeignKey(nameof(mf_production_orders))]
        [Required]
        public long OrderId { get; set; }
        [Required]
        public DateTime IssueDate { get; set; }
        [ReferencedKey(nameof(it_items.Code))]
        [Required]
        [StringLength(50)]
        public string MaterialCode { get; set; }
        [Required]
        public decimal IssuedQty { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitCode { get; set; }
        [Required]
        [StringLength(255)]
        public string IssuedBy { get; set; }
        [Required]
        [StringLength(255)]
        public string IssuedTo { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for it_items (MaterialCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }

        /// <summary>
        /// Navigation property for mf_production_orders (OrderId)
        /// </summary>
        public virtual mf_production_orders? mf_production_orders { get; set; }
    }
    #endregion

    #region mf_production_order_materials
    /// <summary>
    /// Entity class for table mf_production_order_materials
    /// </summary>
    [Table("mf_production_order_materials")]
    
    public class mf_production_order_materials
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ForeignKey(nameof(mf_production_orders))]
        [Required]
        public long OrderId { get; set; }
        [ForeignKey(nameof(mf_bom_items))]
        [Required]
        public long BOMItemId { get; set; }
        [Required]
        public int LineNumber { get; set; }
        [ReferencedKey(nameof(it_items.Code))]
        [Required]
        [StringLength(50)]
        public string MaterialCode { get; set; }
        [Required]
        [StringLength(255)]
        public string MaterialName { get; set; }
        [Required]
        public decimal RequiredQty { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal IssuedQty { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal ReturnedQty { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal ScrapQty { get; set; }
        public decimal? RemainingQty { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitCode { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for mf_bom_items (BOMItemId)
        /// </summary>
        public virtual mf_bom_items? mf_bom_items { get; set; }

        /// <summary>
        /// Navigation property for it_items (MaterialCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }

        /// <summary>
        /// Navigation property for mf_production_orders (OrderId)
        /// </summary>
        public virtual mf_production_orders? mf_production_orders { get; set; }
    }
    #endregion

    #region mf_production_order_operations
    /// <summary>
    /// Entity class for table mf_production_order_operations
    /// </summary>
    [Table("mf_production_order_operations")]
    
    public class mf_production_order_operations
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ForeignKey(nameof(mf_production_orders))]
        [Required]
        public long OrderId { get; set; }
        [ForeignKey(nameof(mf_bom_operations))]
        [Required]
        public long BOMOperationId { get; set; }
        [Required]
        public int OperationSeq { get; set; }
        [Required]
        [StringLength(50)]
        public string OperationCode { get; set; }
        [Required]
        [StringLength(255)]
        public string OperationName { get; set; }
        [ReferencedKey(nameof(mf_work_centers.WorkCenterCode))]
        [Required]
        [StringLength(50)]
        public string WorkCenterCode { get; set; }
        [Required]
        public DateTime PlannedStartDate { get; set; }
        [Required]
        public DateTime PlannedEndDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        [Required]
        public int PlannedDuration { get; set; }
        public int? ActualDuration { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(255)]
        public string AssignedTo { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal CompletedQty { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal ScrapQty { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for mf_bom_operations (BOMOperationId)
        /// </summary>
        public virtual mf_bom_operations? mf_bom_operations { get; set; }

        /// <summary>
        /// Navigation property for mf_production_orders (OrderId)
        /// </summary>
        public virtual mf_production_orders? mf_production_orders { get; set; }

        /// <summary>
        /// Navigation property for mf_work_centers (WorkCenterCode)
        /// </summary>
        public virtual mf_work_centers? mf_work_centers { get; set; }
    }
    #endregion

    #region mf_production_orders
    /// <summary>
    /// Entity class for table mf_production_orders
    /// </summary>
    [Table("mf_production_orders")]
    
    public class mf_production_orders
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string OrderNumber { get; set; }
        [ReferencedKey(nameof(mf_bom.BOMCode))]
        [Required]
        [StringLength(50)]
        public string BOMCode { get; set; }
        [ReferencedKey(nameof(it_items.Code))]
        [Required]
        [StringLength(50)]
        public string ProductCode { get; set; }
        [Required]
        [StringLength(255)]
        public string ProductName { get; set; }
        [Required]
        public decimal PlannedQty { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal CompletedQty { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal ScrapQty { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitCode { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        public DateTime? ActualStartDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Priority { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(255)]
        public string ReleasedBy { get; set; }
        public DateTime? ReleasedDate { get; set; }
        [StringLength(50)]
        public string ReferenceType { get; set; }
        public long? ReferenceId { get; set; }
        [StringLength(1000)]
        public string Notes { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for mf_bom (BOMCode)
        /// </summary>
        public virtual mf_bom? mf_bom { get; set; }

        /// <summary>
        /// Navigation property for it_items (ProductCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }

        /// <summary>
        /// Collection navigation property for mf_cost_actuals
        /// </summary>
        public virtual ICollection<mf_cost_actuals>? mf_cost_actualsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_material_issues
        /// </summary>
        public virtual ICollection<mf_material_issues>? mf_material_issuesCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_production_order_materials
        /// </summary>
        public virtual ICollection<mf_production_order_materials>? mf_production_order_materialsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_production_order_operations
        /// </summary>
        public virtual ICollection<mf_production_order_operations>? mf_production_order_operationsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_production_receipts
        /// </summary>
        public virtual ICollection<mf_production_receipts>? mf_production_receiptsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_qc_inspections
        /// </summary>
        public virtual ICollection<mf_qc_inspections>? mf_qc_inspectionsCollection { get; set; }
    }
    #endregion

    #region mf_production_receipts
    /// <summary>
    /// Entity class for table mf_production_receipts
    /// </summary>
    [Table("mf_production_receipts")]
    
    public class mf_production_receipts
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ReceiptNumber { get; set; }
        [ForeignKey(nameof(mf_production_orders))]
        [Required]
        public long OrderId { get; set; }
        [Required]
        public DateTime ReceiptDate { get; set; }
        [Required]
        public decimal ReceivedQty { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal ScrapQty { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitCode { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string InspectionStatus { get; set; }
        [StringLength(255)]
        public string InspectedBy { get; set; }
        public DateTime? InspectedDate { get; set; }
        [Required]
        [StringLength(255)]
        public string ReceivedBy { get; set; }
        [StringLength(50)]
        public string WarehouseCode { get; set; }
        [StringLength(50)]
        public string LocationCode { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for mf_production_orders (OrderId)
        /// </summary>
        public virtual mf_production_orders? mf_production_orders { get; set; }
    }
    #endregion

    #region mf_qc_inspection_results
    /// <summary>
    /// Entity class for table mf_qc_inspection_results
    /// </summary>
    [Table("mf_qc_inspection_results")]
    
    public class mf_qc_inspection_results
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ForeignKey(nameof(mf_qc_inspections))]
        [Required]
        public long InspectionId { get; set; }
        [Required]
        [StringLength(50)]
        public string CheckpointCode { get; set; }
        [Required]
        [StringLength(255)]
        public string CheckpointName { get; set; }
        public decimal? SpecificationMin { get; set; }
        public decimal? SpecificationMax { get; set; }
        public decimal? ActualValue { get; set; }
        [Required]
        [StringLength(50)]
        public string Result { get; set; }
        [StringLength(500)]
        public string Remarks { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for mf_qc_inspections (InspectionId)
        /// </summary>
        public virtual mf_qc_inspections? mf_qc_inspections { get; set; }
    }
    #endregion

    #region mf_qc_inspections
    /// <summary>
    /// Entity class for table mf_qc_inspections
    /// </summary>
    [Table("mf_qc_inspections")]
    
    public class mf_qc_inspections
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string InspectionNumber { get; set; }
        [ForeignKey(nameof(mf_production_orders))]
        [Required]
        public long ProductionOrderId { get; set; }
        [Required]
        public DateTime InspectionDate { get; set; }
        [Required]
        [StringLength(50)]
        public string InspectionType { get; set; }
        [Required]
        [StringLength(255)]
        public string InspectorId { get; set; }
        [Required]
        public int SampleSize { get; set; }
        [Required]
        public int PassedQty { get; set; }
        [Required]
        public int FailedQty { get; set; }
        [Required]
        [StringLength(50)]
        public string Status { get; set; }
        [StringLength(1000)]
        public string Remarks { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for mf_production_orders (ProductionOrderId)
        /// </summary>
        public virtual mf_production_orders? mf_production_orders { get; set; }

        /// <summary>
        /// Collection navigation property for mf_qc_inspection_results
        /// </summary>
        public virtual ICollection<mf_qc_inspection_results>? mf_qc_inspection_resultsCollection { get; set; }
    }
    #endregion

    #region mf_work_center_schedules
    /// <summary>
    /// Entity class for table mf_work_center_schedules
    /// </summary>
    [Table("mf_work_center_schedules")]
    
    public class mf_work_center_schedules
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ReferencedKey(nameof(mf_work_centers.WorkCenterCode))]
        [Required]
        [StringLength(50)]
        public string WorkCenterCode { get; set; }
        [Required]
        [StringLength(50)]
        public string ShiftCode { get; set; }
        [Required]
        public byte DayOfWeek { get; set; }
        [Required]
        public TimeSpan AvailableFrom { get; set; }
        [Required]
        public TimeSpan AvailableTo { get; set; }
        [Required]
        [Column(TypeName = "bit")]
        public bool IsWorking { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for mf_work_centers (WorkCenterCode)
        /// </summary>
        public virtual mf_work_centers? mf_work_centers { get; set; }
    }
    #endregion

    #region mf_work_centers
    /// <summary>
    /// Entity class for table mf_work_centers
    /// </summary>
    [Table("mf_work_centers")]
    
    public class mf_work_centers
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkCenterCode { get; set; }
        [Required]
        [StringLength(255)]
        public string WorkCenterName { get; set; }
        [Required]
        [StringLength(50)]
        public string WorkCenterType { get; set; }
        [StringLength(50)]
        public string Department { get; set; }
        [Required]
        public decimal Capacity { get; set; }
        [Required]
        [StringLength(50)]
        public string CapacityUOM { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal CostPerHour { get; set; }
        [StringLength(50)]
        public string DefaultShiftCode { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Collection navigation property for mf_bom_operations
        /// </summary>
        public virtual ICollection<mf_bom_operations>? mf_bom_operationsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_production_order_operations
        /// </summary>
        public virtual ICollection<mf_production_order_operations>? mf_production_order_operationsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for mf_work_center_schedules
        /// </summary>
        public virtual ICollection<mf_work_center_schedules>? mf_work_center_schedulesCollection { get; set; }
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

    #region pr_goods_receipt_items
    /// <summary>
    /// Entity class for table pr_goods_receipt_items
    /// </summary>
    [Table("pr_goods_receipt_items")]
    
    public class pr_goods_receipt_items
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ForeignKey(nameof(pr_goods_receipts))]
        [Required]
        public long ReceiptId { get; set; }
        [ForeignKey(nameof(pr_purchase_order_items))]
        [Required]
        public long POItemId { get; set; }
        [Required]
        public int LineNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; }
        [Required]
        public decimal ReceivedQuantity { get; set; }
        [Required]
        public decimal AcceptedQuantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal RejectedQuantity { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitCode { get; set; }
        [StringLength(500)]
        public string RejectionReason { get; set; }
        [StringLength(500)]
        public string Notes { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for pr_purchase_order_items (POItemId)
        /// </summary>
        public virtual pr_purchase_order_items? pr_purchase_order_items { get; set; }

        /// <summary>
        /// Navigation property for pr_goods_receipts (ReceiptId)
        /// </summary>
        public virtual pr_goods_receipts? pr_goods_receipts { get; set; }
    }
    #endregion

    #region pr_goods_receipts
    /// <summary>
    /// Entity class for table pr_goods_receipts
    /// </summary>
    [Table("pr_goods_receipts")]
    
    public class pr_goods_receipts
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ReceiptNumber { get; set; }
        [ForeignKey(nameof(pr_purchase_orders))]
        [Required]
        public long POId { get; set; }
        [Required]
        public DateTime ReceiptDate { get; set; }
        [Required]
        [StringLength(255)]
        public string ReceivedBy { get; set; }
        [StringLength(100)]
        public string DeliveryNote { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(255)]
        public string InspectedBy { get; set; }
        public DateTime? InspectedDate { get; set; }
        [StringLength(1000)]
        public string Notes { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for pr_purchase_orders (POId)
        /// </summary>
        public virtual pr_purchase_orders? pr_purchase_orders { get; set; }

        /// <summary>
        /// Collection navigation property for pr_goods_receipt_items
        /// </summary>
        public virtual ICollection<pr_goods_receipt_items>? pr_goods_receipt_itemsCollection { get; set; }
    }
    #endregion

    #region pr_purchase_order_items
    /// <summary>
    /// Entity class for table pr_purchase_order_items
    /// </summary>
    [Table("pr_purchase_order_items")]
    
    public class pr_purchase_order_items
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ForeignKey(nameof(pr_purchase_orders))]
        [Required]
        public long POId { get; set; }
        [Required]
        public int LineNumber { get; set; }
        [ReferencedKey(nameof(it_items.Code))]
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; }
        [Required]
        [StringLength(255)]
        public string ItemName { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18, 3)")]
        public decimal ReceivedQuantity { get; set; }
        public decimal? RemainingQuantity { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitCode { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal TaxRate { get; set; }
        public decimal? TaxAmount { get; set; }
        [StringLength(1000)]
        public string Specifications { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for it_items (ItemCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }

        /// <summary>
        /// Navigation property for pr_purchase_orders (POId)
        /// </summary>
        public virtual pr_purchase_orders? pr_purchase_orders { get; set; }

        /// <summary>
        /// Collection navigation property for pr_goods_receipt_items
        /// </summary>
        public virtual ICollection<pr_goods_receipt_items>? pr_goods_receipt_itemsCollection { get; set; }
    }
    #endregion

    #region pr_purchase_orders
    /// <summary>
    /// Entity class for table pr_purchase_orders
    /// </summary>
    [Table("pr_purchase_orders")]
    
    public class pr_purchase_orders
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string PONumber { get; set; }
        [Required]
        public DateTime PODate { get; set; }
        [ForeignKey(nameof(pr_requisitions))]
        public long? RequisitionId { get; set; }
        [ForeignKey(nameof(pr_quotations))]
        public long? QuotationId { get; set; }
        [ReferencedKey(nameof(pr_suppliers.SupplierCode))]
        [Required]
        [StringLength(50)]
        public string SupplierCode { get; set; }
        [Required]
        public DateTime DeliveryDate { get; set; }
        [StringLength(500)]
        public string DeliveryAddress { get; set; }
        [Required]
        public int PaymentTerms { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal TaxRate { get; set; }
        [Required]
        public decimal TaxAmount { get; set; }
        public decimal? GrandTotal { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(255)]
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        [StringLength(1000)]
        public string Notes { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for pr_quotations (QuotationId)
        /// </summary>
        public virtual pr_quotations? pr_quotations { get; set; }

        /// <summary>
        /// Navigation property for pr_requisitions (RequisitionId)
        /// </summary>
        public virtual pr_requisitions? pr_requisitions { get; set; }

        /// <summary>
        /// Navigation property for pr_suppliers (SupplierCode)
        /// </summary>
        public virtual pr_suppliers? pr_suppliers { get; set; }

        /// <summary>
        /// Collection navigation property for pr_goods_receipts
        /// </summary>
        public virtual ICollection<pr_goods_receipts>? pr_goods_receiptsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_purchase_order_items
        /// </summary>
        public virtual ICollection<pr_purchase_order_items>? pr_purchase_order_itemsCollection { get; set; }
    }
    #endregion

    #region pr_quotation_items
    /// <summary>
    /// Entity class for table pr_quotation_items
    /// </summary>
    [Table("pr_quotation_items")]
    
    public class pr_quotation_items
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ForeignKey(nameof(pr_quotations))]
        [Required]
        public long QuotationId { get; set; }
        [ForeignKey(nameof(pr_rfq_items))]
        [Required]
        public long RFQItemId { get; set; }
        [Required]
        public int LineNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; }
        [Required]
        [StringLength(255)]
        public string ItemName { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitCode { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public int? LeadTime { get; set; }
        [StringLength(1000)]
        public string Specifications { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for pr_quotations (QuotationId)
        /// </summary>
        public virtual pr_quotations? pr_quotations { get; set; }

        /// <summary>
        /// Navigation property for pr_rfq_items (RFQItemId)
        /// </summary>
        public virtual pr_rfq_items? pr_rfq_items { get; set; }
    }
    #endregion

    #region pr_quotations
    /// <summary>
    /// Entity class for table pr_quotations
    /// </summary>
    [Table("pr_quotations")]
    
    public class pr_quotations
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string QuotationNumber { get; set; }
        [ForeignKey(nameof(pr_rfq))]
        [Required]
        public long RFQId { get; set; }
        [ReferencedKey(nameof(pr_suppliers.SupplierCode))]
        [Required]
        [StringLength(50)]
        public string SupplierCode { get; set; }
        [Required]
        public DateTime QuotationDate { get; set; }
        [Required]
        public DateTime ValidUntil { get; set; }
        [Required]
        public int PaymentTerms { get; set; }
        [StringLength(255)]
        public string DeliveryTerms { get; set; }
        [Required]
        public decimal TotalAmount { get; set; }
        [Required]
        [Column(TypeName = "decimal(5, 2)")]
        public decimal TaxRate { get; set; }
        [Required]
        public decimal TaxAmount { get; set; }
        public decimal? GrandTotal { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(1000)]
        public string Notes { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for pr_rfq (RFQId)
        /// </summary>
        public virtual pr_rfq? pr_rfq { get; set; }

        /// <summary>
        /// Navigation property for pr_suppliers (SupplierCode)
        /// </summary>
        public virtual pr_suppliers? pr_suppliers { get; set; }

        /// <summary>
        /// Collection navigation property for pr_purchase_orders
        /// </summary>
        public virtual ICollection<pr_purchase_orders>? pr_purchase_ordersCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_quotation_items
        /// </summary>
        public virtual ICollection<pr_quotation_items>? pr_quotation_itemsCollection { get; set; }
    }
    #endregion

    #region pr_requisition_items
    /// <summary>
    /// Entity class for table pr_requisition_items
    /// </summary>
    [Table("pr_requisition_items")]
    
    public class pr_requisition_items
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ForeignKey(nameof(pr_requisitions))]
        [Required]
        public long RequisitionId { get; set; }
        [Required]
        public int LineNumber { get; set; }
        [ReferencedKey(nameof(it_items.Code))]
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; }
        [Required]
        [StringLength(255)]
        public string ItemName { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitCode { get; set; }
        public decimal? EstimatedPrice { get; set; }
        public decimal? EstimatedAmount { get; set; }
        [StringLength(1000)]
        public string Specifications { get; set; }
        [Required]
        public DateTime NeededDate { get; set; }
        [StringLength(500)]
        public string Purpose { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for it_items (ItemCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }

        /// <summary>
        /// Navigation property for pr_requisitions (RequisitionId)
        /// </summary>
        public virtual pr_requisitions? pr_requisitions { get; set; }
    }
    #endregion

    #region pr_requisitions
    /// <summary>
    /// Entity class for table pr_requisitions
    /// </summary>
    [Table("pr_requisitions")]
    
    public class pr_requisitions
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string RequisitionNumber { get; set; }
        [Required]
        public DateTime RequisitionDate { get; set; }
        [Required]
        [StringLength(255)]
        public string RequestedBy { get; set; }
        [ReferencedKey(nameof(hr_departments.Code))]
        [Required]
        [StringLength(50)]
        public string DepartmentCode { get; set; }
        [Required]
        public DateTime RequiredDate { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Priority { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(255)]
        public string ApprovedBy { get; set; }
        public DateTime? ApprovedDate { get; set; }
        [StringLength(255)]
        public string RejectedBy { get; set; }
        public DateTime? RejectedDate { get; set; }
        [StringLength(500)]
        public string RejectionReason { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for hr_departments (DepartmentCode)
        /// </summary>
        public virtual hr_departments? hr_departments { get; set; }

        /// <summary>
        /// Collection navigation property for pr_purchase_orders
        /// </summary>
        public virtual ICollection<pr_purchase_orders>? pr_purchase_ordersCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_requisition_items
        /// </summary>
        public virtual ICollection<pr_requisition_items>? pr_requisition_itemsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_rfq
        /// </summary>
        public virtual ICollection<pr_rfq>? pr_rfqCollection { get; set; }
    }
    #endregion

    #region pr_rfq
    /// <summary>
    /// Entity class for table pr_rfq
    /// </summary>
    [Table("pr_rfq")]
    
    public class pr_rfq
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string RFQNumber { get; set; }
        [Required]
        public DateTime RFQDate { get; set; }
        [ForeignKey(nameof(pr_requisitions))]
        public long? RequisitionId { get; set; }
        [Required]
        public DateTime DueDate { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for pr_requisitions (RequisitionId)
        /// </summary>
        public virtual pr_requisitions? pr_requisitions { get; set; }

        /// <summary>
        /// Collection navigation property for pr_quotations
        /// </summary>
        public virtual ICollection<pr_quotations>? pr_quotationsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_rfq_items
        /// </summary>
        public virtual ICollection<pr_rfq_items>? pr_rfq_itemsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_rfq_suppliers
        /// </summary>
        public virtual ICollection<pr_rfq_suppliers>? pr_rfq_suppliersCollection { get; set; }
    }
    #endregion

    #region pr_rfq_items
    /// <summary>
    /// Entity class for table pr_rfq_items
    /// </summary>
    [Table("pr_rfq_items")]
    
    public class pr_rfq_items
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ForeignKey(nameof(pr_rfq))]
        [Required]
        public long RFQId { get; set; }
        [Required]
        public int LineNumber { get; set; }
        [ReferencedKey(nameof(it_items.Code))]
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; }
        [Required]
        [StringLength(255)]
        public string ItemName { get; set; }
        [Required]
        public decimal Quantity { get; set; }
        [Required]
        [StringLength(50)]
        public string UnitCode { get; set; }
        [StringLength(1000)]
        public string Specifications { get; set; }
        [Required]
        public DateTime RequiredDate { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for it_items (ItemCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }

        /// <summary>
        /// Navigation property for pr_rfq (RFQId)
        /// </summary>
        public virtual pr_rfq? pr_rfq { get; set; }

        /// <summary>
        /// Collection navigation property for pr_quotation_items
        /// </summary>
        public virtual ICollection<pr_quotation_items>? pr_quotation_itemsCollection { get; set; }
    }
    #endregion

    #region pr_rfq_suppliers
    /// <summary>
    /// Entity class for table pr_rfq_suppliers
    /// </summary>
    [Table("pr_rfq_suppliers")]
    
    public class pr_rfq_suppliers
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ForeignKey(nameof(pr_rfq))]
        [Required]
        public long RFQId { get; set; }
        [ReferencedKey(nameof(pr_suppliers.SupplierCode))]
        [Required]
        [StringLength(50)]
        public string SupplierCode { get; set; }
        [Required]
        public DateTime InvitationDate { get; set; }
        public DateTime? ResponseDate { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for pr_rfq (RFQId)
        /// </summary>
        public virtual pr_rfq? pr_rfq { get; set; }

        /// <summary>
        /// Navigation property for pr_suppliers (SupplierCode)
        /// </summary>
        public virtual pr_suppliers? pr_suppliers { get; set; }
    }
    #endregion

    #region pr_supplier_ratings
    /// <summary>
    /// Entity class for table pr_supplier_ratings
    /// </summary>
    [Table("pr_supplier_ratings")]
    
    public class pr_supplier_ratings
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ReferencedKey(nameof(pr_suppliers.SupplierCode))]
        [Required]
        [StringLength(50)]
        public string SupplierCode { get; set; }
        [Required]
        public DateTime RatingDate { get; set; }
        [Required]
        public decimal QualityScore { get; set; }
        [Required]
        public decimal DeliveryScore { get; set; }
        [Required]
        public decimal PriceScore { get; set; }
        [Required]
        public decimal ServiceScore { get; set; }
        public decimal? OverallScore { get; set; }
        [StringLength(1000)]
        public string Comments { get; set; }
        [StringLength(255)]
        public string RatedBy { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for pr_suppliers (SupplierCode)
        /// </summary>
        public virtual pr_suppliers? pr_suppliers { get; set; }
    }
    #endregion

    #region pr_suppliers
    /// <summary>
    /// Entity class for table pr_suppliers
    /// </summary>
    [Table("pr_suppliers")]
    
    public class pr_suppliers
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string SupplierCode { get; set; }
        [Required]
        [StringLength(255)]
        public string SupplierName { get; set; }
        [StringLength(255)]
        public string ContactPerson { get; set; }
        [StringLength(255)]
        public string Email { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(50)]
        public string TaxCode { get; set; }
        [StringLength(500)]
        public string Address { get; set; }
        [ForeignKey(nameof(ad_provinces))]
        [StringLength(20)]
        public string ProvinceCode { get; set; }
        [ForeignKey(nameof(ad_wards))]
        [StringLength(20)]
        public string WardCode { get; set; }
        [ForeignKey(nameof(ad_countries))]
        [StringLength(20)]
        public string CountryCode { get; set; }
        public int? PaymentTerms { get; set; }
        public decimal? CreditLimit { get; set; }
        public decimal? Rating { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        [StringLength(1000)]
        public string Notes { get; set; }
        [StringLength(255)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(255)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] RowVersion { get; set; }

        /// <summary>
        /// Navigation property for ad_countries (CountryCode)
        /// </summary>
        public virtual ad_countries? ad_countries { get; set; }

        /// <summary>
        /// Navigation property for ad_provinces (ProvinceCode)
        /// </summary>
        public virtual ad_provinces? ad_provinces { get; set; }

        /// <summary>
        /// Navigation property for ad_wards (WardCode)
        /// </summary>
        public virtual ad_wards? ad_wards { get; set; }

        /// <summary>
        /// Collection navigation property for pr_purchase_orders
        /// </summary>
        public virtual ICollection<pr_purchase_orders>? pr_purchase_ordersCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_quotations
        /// </summary>
        public virtual ICollection<pr_quotations>? pr_quotationsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_rfq_suppliers
        /// </summary>
        public virtual ICollection<pr_rfq_suppliers>? pr_rfq_suppliersCollection { get; set; }

        /// <summary>
        /// Collection navigation property for pr_supplier_ratings
        /// </summary>
        public virtual ICollection<pr_supplier_ratings>? pr_supplier_ratingsCollection { get; set; }
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

        /// <summary>
        /// Collection navigation property for sa_orders
        /// </summary>
        public virtual ICollection<sa_orders>? sa_ordersCollection { get; set; }
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
        [ReferencedKey(nameof(sa_orders.OrderCode))]
        [Required]
        [StringLength(50)]
        public string OrderCode { get; set; }
        [ReferencedKey(nameof(it_items.Code))]
        [Required]
        [StringLength(50)]
        public string ItemCode { get; set; }
        [Required]
        [Column(TypeName = "int")]
        public int Quantity { get; set; }
        [Required]
        public decimal UnitPrice { get; set; }
        public decimal? TotalPrice { get; set; }

        /// <summary>
        /// Navigation property for it_items (ItemCode)
        /// </summary>
        public virtual it_items? it_items { get; set; }

        /// <summary>
        /// Navigation property for sa_orders (OrderCode)
        /// </summary>
        public virtual sa_orders? sa_orders { get; set; }
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
        [ReferencedKey(nameof(sa_customers.CustomerCode))]
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
        [ReferencedKey(nameof(hr_employees.EmployeeCode))]
        [Required]
        [StringLength(50)]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Navigation property for sa_customers (CustomerCode)
        /// </summary>
        public virtual sa_customers? sa_customers { get; set; }

        /// <summary>
        /// Navigation property for hr_employees (EmployeeCode)
        /// </summary>
        public virtual hr_employees? hr_employees { get; set; }

        /// <summary>
        /// Collection navigation property for sa_order_items
        /// </summary>
        public virtual ICollection<sa_order_items>? sa_order_itemsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for sa_payments
        /// </summary>
        public virtual ICollection<sa_payments>? sa_paymentsCollection { get; set; }
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
        [ReferencedKey(nameof(sa_orders.OrderCode))]
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

        /// <summary>
        /// Navigation property for sa_orders (OrderCode)
        /// </summary>
        public virtual sa_orders? sa_orders { get; set; }
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

        /// <summary>
        /// Collection navigation property for sy_function_mapping
        /// </summary>
        public virtual ICollection<sy_function_mapping>? sy_function_mappingCollection { get; set; }

        /// <summary>
        /// Collection navigation property for sy_role_permissions
        /// </summary>
        public virtual ICollection<sy_role_permissions>? sy_role_permissionsCollection { get; set; }
    }
    #endregion

    #region sy_background_jobs
    /// <summary>
    /// Entity class for table sy_background_jobs
    /// </summary>
    [Table("sy_background_jobs")]
    
    public class sy_background_jobs
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string JobCode { get; set; }
        [Required]
        [StringLength(255)]
        public string JobName { get; set; }
        [Required]
        [StringLength(50)]
        public string CronExpression { get; set; }
        [Column(TypeName = "bit")]
        public bool? IsDisabled { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
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

    #region sy_configs
    /// <summary>
    /// Entity class for table sy_configs
    /// </summary>
    [Table("sy_configs")]
    
    public class sy_configs
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string ConfigCode { get; set; }
        [Required]
        [StringLength(255)]
        public string ConfigName { get; set; }
        public string ConfigValue { get; set; }
        [StringLength(20)]
        [Column(TypeName = "varchar(20)")]
        public string DataType { get; set; }
        [StringLength(500)]
        public string Description { get; set; }
        [Column(TypeName = "bit")]
        public bool? IsSystem { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime? CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }
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
        [ForeignKey(nameof(sy_actions))]
        [Required]
        [StringLength(50)]
        public string ActionCode { get; set; }
        [ReferencedKey(nameof(sy_functions.Code))]
        [StringLength(50)]
        public string FunctionCode { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [Required]
        public byte[] DataRowVersion { get; set; }

        /// <summary>
        /// Navigation property for sy_actions (ActionCode)
        /// </summary>
        public virtual sy_actions? sy_actions { get; set; }

        /// <summary>
        /// Navigation property for sy_functions (FunctionCode)
        /// </summary>
        public virtual sy_functions? sy_functions { get; set; }
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
        [ReferencedKey(nameof(sy_functions.Code))]
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

        /// <summary>
        /// Navigation property for sy_functions (GroupCode)
        /// </summary>
        public virtual sy_functions? sy_functionsNavigation { get; set; }

        /// <summary>
        /// Collection navigation property for sy_function_mapping
        /// </summary>
        public virtual ICollection<sy_function_mapping>? sy_function_mappingCollection { get; set; }

        /// <summary>
        /// Collection navigation property for sy_functions
        /// </summary>
        public virtual ICollection<sy_functions>? sy_functionsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for sy_menus
        /// </summary>
        public virtual ICollection<sy_menus>? sy_menusCollection { get; set; }

        /// <summary>
        /// Collection navigation property for sy_role_permissions
        /// </summary>
        public virtual ICollection<sy_role_permissions>? sy_role_permissionsCollection { get; set; }
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
        public string Code { get; set; }
        [StringLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string Functions { get; set; }
        [Column(TypeName = "varbinary")]
        public byte[] Images { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? CreatedAt { get; set; }
        [StringLength(50)]
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
        [StringLength(50)]
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
        [StringLength(50)]
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

        /// <summary>
        /// Collection navigation property for sy_resources
        /// </summary>
        [NotMapped]
        public virtual ICollection<sy_resources>? sy_resourcesCollection { get; set; }
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
        [ReferencedKey(nameof(sy_functions.Code))]
        [StringLength(50)]
        public string FunctionCode { get; set; }
        [ReferencedKey(nameof(sy_menus.Code))]
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

        /// <summary>
        /// Navigation property for sy_functions (FunctionCode)
        /// </summary>
        public virtual sy_functions? sy_functions { get; set; }

        /// <summary>
        /// Navigation property for sy_menus (ParentCode)
        /// </summary>
        public virtual sy_menus? sy_menusNavigation { get; set; }

        /// <summary>
        /// Collection navigation property for sy_menus
        /// </summary>
        public virtual ICollection<sy_menus>? sy_menusCollection { get; set; }
    }
    #endregion

    #region sy_password_reset_tokens
    /// <summary>
    /// Entity class for table sy_password_reset_tokens
    /// </summary>
    [Table("sy_password_reset_tokens")]
    
    public class sy_password_reset_tokens
    {
        [Key]
        [Required]
        public long Id { get; set; }
        [ReferencedKey(nameof(sy_users.Username))]
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [Required]
        [StringLength(100)]
        public string Token { get; set; }
        [Required]
        public DateTime ExpiresAt { get; set; }
        public DateTime? UsedAt { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Navigation property for sy_users (Username)
        /// </summary>
        public virtual sy_users? sy_users { get; set; }
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
        [ReferencedKey(nameof(sy_users.Username))]
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }
        [Required]
        public string Token { get; set; }
        [ForeignKey(nameof(sy_users))]
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

        /// <summary>
        /// Navigation property for sy_users (UserId)
        /// </summary>
        public virtual sy_users? Usersy_users { get; set; }

        /// <summary>
        /// Navigation property for sy_users (UserName)
        /// </summary>
        public virtual sy_users? UserNamesy_users { get; set; }
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
        [ReferencedKey(nameof(sy_languages.LanguageCode))]
        [Required]
        [StringLength(10)]
        [Column(TypeName = "nvarchar(10)")]
        public string SourceLanguage { get; set; }
        [ReferencedKey(nameof(sy_languages.LanguageCode))]
        [Required]
        [StringLength(10)]
        public string LanguageCode { get; set; }
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

        /// <summary>
        /// Navigation property for sy_languages (LanguageCode)
        /// </summary>
        public virtual sy_languages? Languagesy_languages { get; set; }

        /// <summary>
        /// Navigation property for sy_languages (SourceLanguage)
        /// </summary>
        public virtual sy_languages? SourceLanguagesy_languages { get; set; }
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
        [ReferencedKey(nameof(sy_roles.Code))]
        [Required]
        [StringLength(50)]
        public string RoleCode { get; set; }
        [ReferencedKey(nameof(sy_functions.Code))]
        [Required]
        [StringLength(50)]
        public string FunctionCode { get; set; }
        [ForeignKey(nameof(sy_actions))]
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

        /// <summary>
        /// Navigation property for sy_actions (ActionCode)
        /// </summary>
        public virtual sy_actions? sy_actions { get; set; }

        /// <summary>
        /// Navigation property for sy_functions (FunctionCode)
        /// </summary>
        public virtual sy_functions? sy_functions { get; set; }

        /// <summary>
        /// Navigation property for sy_roles (RoleCode)
        /// </summary>
        public virtual sy_roles? sy_roles { get; set; }
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

        /// <summary>
        /// Collection navigation property for sy_role_permissions
        /// </summary>
        public virtual ICollection<sy_role_permissions>? sy_role_permissionsCollection { get; set; }

        /// <summary>
        /// Collection navigation property for sy_user_roles
        /// </summary>
        public virtual ICollection<sy_user_roles>? sy_user_rolesCollection { get; set; }

        /// <summary>
        /// Collection navigation property for sy_users
        /// </summary>
        public virtual ICollection<sy_users>? sy_usersCollection { get; set; }
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
        [ReferencedKey(nameof(sy_users.Username))]
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

        /// <summary>
        /// Navigation property for sy_users (Username)
        /// </summary>
        public virtual sy_users? sy_users { get; set; }
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
        [ReferencedKey(nameof(sy_users.Username))]
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

        /// <summary>
        /// Navigation property for sy_users (UserName)
        /// </summary>
        public virtual sy_users? sy_users { get; set; }
    }
    #endregion

    #region sy_user_roles
    /// <summary>
    /// Entity class for table sy_user_roles
    /// </summary>
    [Table("sy_user_roles")]
    [PrimaryKey(nameof(Username), nameof(RoleCode))]
    public class sy_user_roles
    {
        [ReferencedKey(nameof(sy_users.Username))]
        [Required]
        [StringLength(50)]
        public string Username { get; set; }
        [ReferencedKey(nameof(sy_roles.Code))]
        [Required]
        [StringLength(50)]
        public string RoleCode { get; set; }
        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Navigation property for sy_roles (RoleCode)
        /// </summary>
        public virtual sy_roles? sy_roles { get; set; }

        /// <summary>
        /// Navigation property for sy_users (Username)
        /// </summary>
        public virtual sy_users? sy_users { get; set; }
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
        [ReferencedKey(nameof(hr_employees.EmployeeCode))]
        [StringLength(50)]
        public string EmployeeCode { get; set; }
        [ReferencedKey(nameof(sy_roles.Code))]
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

        /// <summary>
        /// Navigation property for hr_employees (EmployeeCode)
        /// </summary>
        public virtual hr_employees? hr_employees { get; set; }

        /// <summary>
        /// Navigation property for sy_roles (RoleCode)
        /// </summary>
        public virtual sy_roles? sy_roles { get; set; }

        /// <summary>
        /// Collection navigation property for sy_password_reset_tokens
        /// </summary>
        public virtual ICollection<sy_password_reset_tokens>? sy_password_reset_tokensCollection { get; set; }

        /// <summary>
        /// Collection navigation property for sy_refresh_tokens
        /// </summary>
        [NotMapped]
        public virtual ICollection<sy_refresh_tokens>? sy_refresh_tokensCollection { get; set; }

        /// <summary>
        /// Collection navigation property for sy_token_revoked
        /// </summary>
        public virtual ICollection<sy_token_revoked>? sy_token_revokedCollection { get; set; }

        /// <summary>
        /// Collection navigation property for sy_tokens_management
        /// </summary>
        public virtual ICollection<sy_tokens_management>? sy_tokens_managementCollection { get; set; }

        /// <summary>
        /// Collection navigation property for sy_user_roles
        /// </summary>
        public virtual ICollection<sy_user_roles>? sy_user_rolesCollection { get; set; }
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
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Collection navigation property for wf_approval_route_level
        /// </summary>
        public virtual ICollection<wf_approval_route_level>? wf_approval_route_levelCollection { get; set; }

        /// <summary>
        /// Collection navigation property for wf_request
        /// </summary>
        public virtual ICollection<wf_request>? wf_requestCollection { get; set; }
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
        [ForeignKey(nameof(wf_approval_route_level))]
        [Required]
        public int ApprovalRouteLevelId { get; set; }
        [ReferencedKey(nameof(hr_employees.EmployeeCode))]
        [StringLength(50)]
        public string ApproverEmployeeCode { get; set; }
        [Required]
        [StringLength(100)]
        public string ApproverUsername { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Navigation property for wf_approval_route_level (ApprovalRouteLevelId)
        /// </summary>
        public virtual wf_approval_route_level? wf_approval_route_level { get; set; }

        /// <summary>
        /// Navigation property for hr_employees (ApproverEmployeeCode)
        /// </summary>
        public virtual hr_employees? hr_employees { get; set; }
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
        [ForeignKey(nameof(wf_approval_route))]
        [Required]
        public int ApprovalRouteId { get; set; }
        [Required]
        public int LevelOrder { get; set; }
        [Required]
        [StringLength(255)]
        public string LevelName { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Navigation property for wf_approval_route (ApprovalRouteId)
        /// </summary>
        public virtual wf_approval_route? wf_approval_route { get; set; }

        /// <summary>
        /// Collection navigation property for wf_approval_route_approver
        /// </summary>
        public virtual ICollection<wf_approval_route_approver>? wf_approval_route_approverCollection { get; set; }

        /// <summary>
        /// Collection navigation property for wf_request_approval
        /// </summary>
        public virtual ICollection<wf_request_approval>? wf_request_approvalCollection { get; set; }
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
        [ReferencedKey(nameof(hr_employees.EmployeeCode))]
        [StringLength(50)]
        public string ApplicantEmployeeCode { get; set; }
        [Required]
        [StringLength(100)]
        public string ApplicantUsername { get; set; }
        [ForeignKey(nameof(wf_approval_route))]
        public int? ApprovalRouteId { get; set; }
        [Required]
        [StringLength(50)]
        [Column(TypeName = "varchar(50)")]
        public string Status { get; set; }
        public string RequestData { get; set; }
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Navigation property for hr_employees (ApplicantEmployeeCode)
        /// </summary>
        public virtual hr_employees? hr_employees { get; set; }

        /// <summary>
        /// Navigation property for wf_approval_route (ApprovalRouteId)
        /// </summary>
        public virtual wf_approval_route? wf_approval_route { get; set; }

        /// <summary>
        /// Collection navigation property for wf_request_approval
        /// </summary>
        public virtual ICollection<wf_request_approval>? wf_request_approvalCollection { get; set; }
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
        [ForeignKey(nameof(wf_request))]
        [Required]
        public int RequestId { get; set; }
        [ForeignKey(nameof(wf_approval_route_level))]
        [Required]
        public int ApprovalRouteLevelId { get; set; }
        [ReferencedKey(nameof(hr_employees.EmployeeCode))]
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
        [StringLength(50)]
        public string CreatedBy { get; set; }
        [Required]
        [Column(TypeName = "datetime")]
        public DateTime CreatedAt { get; set; }
        [StringLength(50)]
        public string UpdatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Navigation property for wf_approval_route_level (ApprovalRouteLevelId)
        /// </summary>
        public virtual wf_approval_route_level? wf_approval_route_level { get; set; }

        /// <summary>
        /// Navigation property for hr_employees (ApproverEmployeeCode)
        /// </summary>
        public virtual hr_employees? hr_employees { get; set; }

        /// <summary>
        /// Navigation property for wf_request (RequestId)
        /// </summary>
        public virtual wf_request? wf_request { get; set; }
    }
    #endregion

}
