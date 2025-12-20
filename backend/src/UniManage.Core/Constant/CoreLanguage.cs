namespace UniManage.Core.Contants
{
    public static class LanguageData
    {
        public static readonly List<(string Code, string Name, string Icon, bool IsDefault, bool IsActive)> All = new List<(string, string, string, bool, bool)>
        {
            ("vi-VN", @"Tiếng Việt", "🇻🇳", false, true),
            ("en-US", @"English", "🇺🇸", true, true),
            ("zh-CN", @"中文（简体）", "🇨🇳", false, false),
            ("ja-JP", @"日本語", "🇯🇵", false, true),
            ("fr-FR", @"Français", "🇫🇷", false, true),
            ("de-DE", @"Deutsch", "🇩🇪", false, true),
            ("es-ES", @"Español", "🇪🇸", false, true),
            ("ko-KR", @"한국어", "🇰🇷", false, true),
            ("ru-RU", @"Русский", "🇷🇺", false, true),
            ("zh-HK", @"繁體中文（香港）", "🇭🇰", false, true),
            ("zh-TW", @"繁體中文（台灣）", "🇹🇼", false, true),
            ("th-TH", @"ไทย", "🇹🇭", false, true),
            ("lo-LA", @"ພາສາລາວ", "🇱🇦", false, true),
            ("pt-PT", @"Português", "🇵🇹", false, true),
        };

        public const string VI_VN = "vi-VN";
        public const string EN_US = "en-US";
        public const string ZH_CN = "zh-CN";
        public const string JA_JP = "ja-JP";
        public const string FR_FR = "fr-FR";
        public const string DE_DE = "de-DE";
        public const string ES_ES = "es-ES";
        public const string KO_KR = "ko-KR";
        public const string RU_RU = "ru-RU";
        public const string ZH_HK = "zh-HK";
        public const string ZH_TW = "zh-TW";
        public const string TH_TH = "th-TH";
        public const string LO_LA = "lo-LA";
        public const string PT_PT = "pt-PT";
    }
}
