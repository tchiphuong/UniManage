namespace UniManage.Shared.Infrastructure.Constant
{
    /// <summary>
    /// Auto-generated từ bảng SyFunctions trong database.
    /// Không chỉnh sửa thủ công — chạy lại T4 template để cập nhật.
    /// </summary>
    public static class CoreFunction
    {
        /// <summary>
        /// func.group.hr
        /// </summary>
        public const string Hr = "HR";

        /// <summary>
        /// func.hr.attendance
        /// </summary>
        public const string HrAttendance = "HR_ATTENDANCE";

        /// <summary>
        /// func.hr.department
        /// </summary>
        public const string HrDepartment = "HR_DEPARTMENT";

        /// <summary>
        /// func.hr.employee
        /// </summary>
        public const string HrEmployee = "HR_EMPLOYEE";

        /// <summary>
        /// func.hr.position
        /// </summary>
        public const string HrPosition = "HR_POSITION";

        /// <summary>
        /// func.hr.salary
        /// </summary>
        public const string HrSalary = "HR_SALARY";

        /// <summary>
        /// func.group.inventory
        /// </summary>
        public const string Inventory = "INVENTORY";

        /// <summary>
        /// func.it.item
        /// </summary>
        public const string ItItem = "IT_ITEM";

        /// <summary>
        /// func.dashboard
        /// </summary>
        public const string Dashboard = "DASHBOARD";

        /// <summary>
        /// func.group.main
        /// </summary>
        public const string Main = "MAIN";

        /// <summary>
        /// func.sa.customer
        /// </summary>
        public const string SaCustomer = "SA_CUSTOMER";

        /// <summary>
        /// func.sa.order
        /// </summary>
        public const string SaOrder = "SA_ORDER";

        /// <summary>
        /// func.group.sales
        /// </summary>
        public const string Sales = "SALES";

        /// <summary>
        /// func.sy.config
        /// </summary>
        public const string SyConfig = "SY_CONFIG";

        /// <summary>
        /// func.sy.role
        /// </summary>
        public const string SyRole = "SY_ROLE";

        /// <summary>
        /// func.sy.user
        /// </summary>
        public const string SyUser = "SY_USER";

        /// <summary>
        /// func.group.system
        /// </summary>
        public const string System = "SYSTEM";

        public static readonly List<(string Code, string ResourceKey)> All = new List<(string, string)>
        {
            ("HR", "func.group.hr"),
            ("HR_ATTENDANCE", "func.hr.attendance"),
            ("HR_DEPARTMENT", "func.hr.department"),
            ("HR_EMPLOYEE", "func.hr.employee"),
            ("HR_POSITION", "func.hr.position"),
            ("HR_SALARY", "func.hr.salary"),
            ("INVENTORY", "func.group.inventory"),
            ("IT_ITEM", "func.it.item"),
            ("DASHBOARD", "func.dashboard"),
            ("MAIN", "func.group.main"),
            ("SA_CUSTOMER", "func.sa.customer"),
            ("SA_ORDER", "func.sa.order"),
            ("SALES", "func.group.sales"),
            ("SY_CONFIG", "func.sy.config"),
            ("SY_ROLE", "func.sy.role"),
            ("SY_USER", "func.sy.user"),
            ("SYSTEM", "func.group.system"),
        };
    }

    /// <summary>
    /// Auto-generated từ bảng SyActions trong database.
    /// Không chỉnh sửa thủ công — chạy lại T4 template để cập nhật.
    /// </summary>
    public static class CoreAction
    {
        /// <summary>
        /// action.view
        /// </summary>
        public const string View = "VIEW";

        /// <summary>
        /// action.create
        /// </summary>
        public const string Create = "CREATE";

        /// <summary>
        /// action.update
        /// </summary>
        public const string Update = "UPDATE";

        /// <summary>
        /// action.delete
        /// </summary>
        public const string Delete = "DELETE";

        /// <summary>
        /// action.export
        /// </summary>
        public const string Export = "EXPORT";

        /// <summary>
        /// action.import
        /// </summary>
        public const string Import = "IMPORT";

        public static readonly List<(string Code, string ResourceKey)> All = new List<(string, string)>
        {
            ("VIEW", "action.view"),
            ("CREATE", "action.create"),
            ("UPDATE", "action.update"),
            ("DELETE", "action.delete"),
            ("EXPORT", "action.export"),
            ("IMPORT", "action.import"),
        };
    }
}


