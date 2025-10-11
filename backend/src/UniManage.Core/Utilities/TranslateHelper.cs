using System.Text.Json;

namespace UniManage.Core.Utilities
{
    /// <summary>
    /// Helper functions for text translation and localization
    /// </summary>
    public static class TranslateHelper
    {
        /// <summary>
        /// Translates text using Google Translate API (free tier)
        /// </summary>
        /// <param name="text">Text to translate</param>
        /// <param name="targetLanguage">Target language code (e.g., 'vi', 'en')</param>
        /// <param name="sourceLanguage">Source language code (default: 'auto')</param>
        /// <returns>Translated text or null if translation fails</returns>
        public static async Task<string?> TranslateTextAsync(
            string text,
            string targetLanguage,
            string sourceLanguage = "auto")
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            if (string.IsNullOrWhiteSpace(targetLanguage))
                throw new ArgumentException("Target language must be specified.", nameof(targetLanguage));

            try
            {
                string url = $"https://translate.googleapis.com/translate_a/single" +
                            $"?client=gtx&sl={sourceLanguage}&tl={targetLanguage}&dt=t" +
                            $"&q={Uri.EscapeDataString(text)}";

                using var httpClient = new HttpClient();
                httpClient.Timeout = TimeSpan.FromSeconds(10);

                var response = await httpClient.GetStringAsync(url);

                using var doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                return root[0][0][0].GetString();
            }
            catch (Exception ex)
            {
                // Log error if needed
                Console.WriteLine($"Translation error: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Translates multiple texts in batch
        /// </summary>
        /// <param name="texts">List of texts to translate</param>
        /// <param name="targetLanguage">Target language code</param>
        /// <param name="sourceLanguage">Source language code</param>
        /// <returns>Dictionary mapping original text to translated text</returns>
        public static async Task<Dictionary<string, string?>> TranslateTextsAsync(
            IEnumerable<string> texts,
            string targetLanguage,
            string sourceLanguage = "auto")
        {
            var results = new Dictionary<string, string?>();

            foreach (var text in texts)
            {
                if (!string.IsNullOrWhiteSpace(text))
                {
                    var translated = await TranslateTextAsync(text, targetLanguage, sourceLanguage);
                    results[text] = translated;

                    // Add small delay to avoid rate limiting
                    await Task.Delay(100);
                }
            }

            return results;
        }

        /// <summary>
        /// Vietnamese language constants
        /// </summary>
        public static class Languages
        {
            public const string Vietnamese = "vi";
            public const string English = "en";
            public const string Chinese = "zh";
            public const string Japanese = "ja";
            public const string Korean = "ko";
            public const string French = "fr";
            public const string German = "de";
            public const string Spanish = "es";
            public const string Auto = "auto";
        }

        /// <summary>
        /// Common Vietnamese translations for system messages
        /// </summary>
        public static class CommonTranslations
        {
            public static readonly Dictionary<string, string> EnglishToVietnamese = new()
            {
                // Status messages
                { "Success", "Thành công" },
                { "Failed", "Thất bại" },
                { "Error", "Lỗi" },
                { "Warning", "Cảnh báo" },
                { "Information", "Thông tin" },
                
                // CRUD operations
                { "Create", "Tạo mới" },
                { "Update", "Cập nhật" },
                { "Delete", "Xóa" },
                { "View", "Xem" },
                { "List", "Danh sách" },
                { "Search", "Tìm kiếm" },
                
                // Common fields
                { "Name", "Tên" },
                { "Code", "Mã" },
                { "Description", "Mô tả" },
                { "Status", "Trạng thái" },
                { "Date", "Ngày" },
                { "Time", "Thời gian" },
                { "User", "Người dùng" },
                { "Email", "Email" },
                { "Phone", "Điện thoại" },
                { "Address", "Địa chỉ" },
                
                // Validation messages
                { "Required", "Bắt buộc" },
                { "Invalid format", "Định dạng không hợp lệ" },
                { "Too long", "Quá dài" },
                { "Too short", "Quá ngắn" },
                { "Already exists", "Đã tồn tại" },
                { "Not found", "Không tìm thấy" },
                
                // Actions
                { "Save", "Lưu" },
                { "Cancel", "Hủy" },
                { "Confirm", "Xác nhận" },
                { "Yes", "Có" },
                { "No", "Không" },
                { "OK", "Đồng ý" },
                
                // Time periods
                { "Today", "Hôm nay" },
                { "Yesterday", "Hôm qua" },
                { "Tomorrow", "Ngày mai" },
                { "This week", "Tuần này" },
                { "This month", "Tháng này" },
                { "This year", "Năm này" }
            };

            /// <summary>
            /// Gets Vietnamese translation for common English terms
            /// </summary>
            /// <param name="englishText">English text</param>
            /// <returns>Vietnamese translation or original text if not found</returns>
            public static string GetVietnameseTranslation(string englishText)
            {
                return EnglishToVietnamese.GetValueOrDefault(englishText, englishText);
            }
        }

        /// <summary>
        /// Formats message with Vietnamese localization
        /// </summary>
        /// <param name="messageTemplate">Message template with {0}, {1} placeholders</param>
        /// <param name="args">Arguments to format</param>
        /// <returns>Formatted Vietnamese message</returns>
        public static string FormatVietnameseMessage(string messageTemplate, params object[] args)
        {
            // Translate common terms in arguments
            var translatedArgs = args.Select(arg =>
            {
                if (arg is string strArg)
                {
                    return CommonTranslations.GetVietnameseTranslation(strArg);
                }
                return arg;
            }).ToArray();

            return string.Format(messageTemplate, translatedArgs);
        }

        /// <summary>
        /// Detects if text is likely Vietnamese
        /// </summary>
        /// <param name="text">Text to check</param>
        /// <returns>True if text appears to be Vietnamese</returns>
        public static bool IsVietnameseText(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return false;

            // Check for Vietnamese diacritics
            var vietnameseChars = "àáạảãâầấậẩẫăằắặẳẵèéẹẻẽêềếệểễìíịỉĩòóọỏõôồốộổỗơờớợởỡùúụủũưừứựửữỳýỵỷỹđ";
            var upperVietnameseChars = vietnameseChars.ToUpperInvariant();

            var vietnameseCharCount = text.Count(c =>
                vietnameseChars.Contains(c) || upperVietnameseChars.Contains(c));

            // If more than 5% of characters are Vietnamese diacritics, likely Vietnamese
            return vietnameseCharCount > 0 && (double)vietnameseCharCount / text.Length > 0.05;
        }

        /// <summary>
        /// Removes Vietnamese diacritics for search/comparison purposes
        /// </summary>
        /// <param name="text">Vietnamese text</param>
        /// <returns>Text without diacritics</returns>
        public static string RemoveVietnameseDiacritics(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            var diacriticsMap = new Dictionary<char, char>
            {
                {'à', 'a'}, {'á', 'a'}, {'ạ', 'a'}, {'ả', 'a'}, {'ã', 'a'},
                {'â', 'a'}, {'ầ', 'a'}, {'ấ', 'a'}, {'ậ', 'a'}, {'ẩ', 'a'}, {'ẫ', 'a'},
                {'ă', 'a'}, {'ằ', 'a'}, {'ắ', 'a'}, {'ặ', 'a'}, {'ẳ', 'a'}, {'ẵ', 'a'},
                {'è', 'e'}, {'é', 'e'}, {'ẹ', 'e'}, {'ẻ', 'e'}, {'ẽ', 'e'},
                {'ê', 'e'}, {'ề', 'e'}, {'ế', 'e'}, {'ệ', 'e'}, {'ể', 'e'}, {'ễ', 'e'},
                {'ì', 'i'}, {'í', 'i'}, {'ị', 'i'}, {'ỉ', 'i'}, {'ĩ', 'i'},
                {'ò', 'o'}, {'ó', 'o'}, {'ọ', 'o'}, {'ỏ', 'o'}, {'õ', 'o'},
                {'ô', 'o'}, {'ồ', 'o'}, {'ố', 'o'}, {'ộ', 'o'}, {'ổ', 'o'}, {'ỗ', 'o'},
                {'ơ', 'o'}, {'ờ', 'o'}, {'ớ', 'o'}, {'ợ', 'o'}, {'ở', 'o'}, {'ỡ', 'o'},
                {'ù', 'u'}, {'ú', 'u'}, {'ụ', 'u'}, {'ủ', 'u'}, {'ũ', 'u'},
                {'ư', 'u'}, {'ừ', 'u'}, {'ứ', 'u'}, {'ự', 'u'}, {'ử', 'u'}, {'ữ', 'u'},
                {'ỳ', 'y'}, {'ý', 'y'}, {'ỵ', 'y'}, {'ỷ', 'y'}, {'ỹ', 'y'},
                {'đ', 'd'}
            };

            var result = new System.Text.StringBuilder();
            foreach (var c in text.ToLowerInvariant())
            {
                result.Append(diacriticsMap.GetValueOrDefault(c, c));
            }

            return result.ToString();
        }
    }
}