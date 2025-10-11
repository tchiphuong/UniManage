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
            ("fr-FR", @"Fran�ais", "????", false, true),
            ("de-DE", @"Deutsch", "????", false, true),
            ("es-ES", @"Espa�ol", "????", false, true),
            ("ko-KR", @"???", "????", false, true),
            ("ru-RU", @"???????", "????", false, true),
            ("zh-HK", @"????(??)", "????", false, true),
            ("zh-TW", @"????(??)", "????", false, true),
            ("th-TH", @"???", "????", false, true),
            ("lo-LA", @"???????", "????", false, true),
            ("pt-PT", @"Portugu�s", "????", false, true),
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
