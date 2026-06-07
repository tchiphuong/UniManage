namespace UniManage.Shared.Infrastructure.Constant
{
    public static class LanguageData
    {
        public static readonly List<(string Code, string Name, string Icon, bool IsDefault, bool IsActive)> All = new List<(string, string, string, bool, bool)>
        {
            ("de-DE", @"Deutsch", "🇩🇪", false, true),
            ("en-US", @"English", "🇺🇸", false, true),
            ("es-ES", @"Español", "🇪🇸", false, true),
            ("fr-FR", @"Français", "🇫🇷", false, true),
            ("it-IT", @"Italiano", "🇮🇹", false, true),
            ("pt-PT", @"Português", "🇵🇹", false, true),
            ("vi-VN", @"Tiếng Việt", "🇻🇳", true, true),
            ("ru-RU", @"Русский", "🇷🇺", false, true),
            ("ar-AE", @"العربية (الإمارات العربية المتحدة)", "🇦🇪", false, true),
            ("hi-IN", @"हिन्दी", "🇮🇳", false, true),
            ("th-TH", @"ไทย", "🇹🇭", false, true),
            ("lo-LA", @"ພາສາລາວ", "🇱🇦", false, true),
            ("zh-CN", @"中文（简体）", "🇨🇳", false, false),
            ("ko-KR", @"한국어", "🇰🇷", false, true),
            ("ja-JP", @"日本語", "🇯🇵", false, true),
            ("zh-TW", @"繁體中文（台灣）", "🇹🇼", false, true),
            ("zh-HK", @"繁體中文（香港）", "🇭🇰", false, true),
        };

        public const string DE_DE = "de-DE";
        public const string EN_US = "en-US";
        public const string ES_ES = "es-ES";
        public const string FR_FR = "fr-FR";
        public const string IT_IT = "it-IT";
        public const string PT_PT = "pt-PT";
        public const string VI_VN = "vi-VN";
        public const string RU_RU = "ru-RU";
        public const string AR_AE = "ar-AE";
        public const string HI_IN = "hi-IN";
        public const string TH_TH = "th-TH";
        public const string LO_LA = "lo-LA";
        public const string ZH_CN = "zh-CN";
        public const string KO_KR = "ko-KR";
        public const string JA_JP = "ja-JP";
        public const string ZH_TW = "zh-TW";
        public const string ZH_HK = "zh-HK";
    }
}

