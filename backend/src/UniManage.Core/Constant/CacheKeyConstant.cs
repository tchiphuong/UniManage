namespace UniManage.Core.Constant
{
    /// <summary>
    /// Chứa danh sách các cache key pattern được sử dụng trong hệ thống
    /// </summary>
    public static class CacheKeyConstant
    {
        public static class System
        {
            public const string ComboboxUsers = "system:combobox:users:{0}";
            public const string ComboboxUsersPattern = "system:combobox:users*";
            public const string Permissions = "system:permissions:{0}";
            public const string CurrentUserProfile = "system:profile:{0}";
        }
    }
}
