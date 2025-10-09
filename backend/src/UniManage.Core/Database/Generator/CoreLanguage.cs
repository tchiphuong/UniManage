namespace UniManage.Core.Models
{
    public static class LanguageData
    {
        public static readonly List<(string Code, string Name, string Icon, bool IsDefault, bool IsActive)> All = new List<(string, string, string, bool, bool)>
        {
            ("vi-VN", @"Ti?ng Vi?t", "????", false, true),
            ("en-US", @"English", "????", true, true),
            ("zh-CN", @"??(??)", "????", false, false),
            ("ja-JP", @"???", "????", false, true),
        };

        public const string VI_VN = "vi-VN";
        public const string EN_US = "en-US";
        public const string ZH_CN = "zh-CN";
        public const string JA_JP = "ja-JP";
    }
}
