using System.Text.Json;

namespace UniManage.Core.Helpers
{
    public static class TranslateHelper
    {
        public static async Task<string?> TranslateTextAsync(string text, string targetLanguage)
        {
            if (string.IsNullOrWhiteSpace(text)) return string.Empty;
            if (string.IsNullOrWhiteSpace(targetLanguage)) throw new ArgumentException("Target language must be specified.", nameof(targetLanguage));

            try
            {
                string url = $"https://translate.googleapis.com/translate_a/single?client=gtx&sl=auto&tl={targetLanguage}&dt=t&q={Uri.EscapeDataString(text)}";

                using var httpClient = new HttpClient();
                var response = await httpClient.GetStringAsync(url);

                using var doc = JsonDocument.Parse(response);
                var root = doc.RootElement;

                return root[0][0][0].GetString();
            }
            catch
            {
                return null;
            }
        }
    }
}