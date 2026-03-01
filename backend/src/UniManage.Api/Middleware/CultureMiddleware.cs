using System.Globalization;
using UniManage.Core.Contants;

namespace UniManage.Api.Middleware
{
    /// <summary>
    /// Middleware to set request culture based on Accept-Language header.
    /// 
    /// Supported cultures loaded from LanguageData (T4-generated from database).
    /// Không hardcode — khi thêm ngôn ngữ mới vào DB, chỉ cần chạy lại T4 template.
    /// </summary>
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        // Load supported cultures từ LanguageData (T4 generated from DB)
        // Chỉ lấy các ngôn ngữ có IsActive = true
        private static readonly HashSet<string> SupportedCultures = new(
            LanguageData.All
                .Where(lang => lang.IsActive)
                .Select(lang => lang.Code),
            StringComparer.OrdinalIgnoreCase);

        // Default culture = ngôn ngữ có IsDefault = true trong DB
        private static readonly string DefaultCulture =
            LanguageData.All.FirstOrDefault(lang => lang.IsDefault).Code ?? "en-US";

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cultureCode = context.Request.Headers["Accept-Language"].ToString();

            // Nếu header rỗng hoặc culture không được hỗ trợ → dùng default từ DB
            if (string.IsNullOrWhiteSpace(cultureCode) || !SupportedCultures.Contains(cultureCode))
            {
                cultureCode = DefaultCulture;
            }

            try
            {
                var culture = new CultureInfo(cultureCode);
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }
            catch (CultureNotFoundException)
            {
                var fallback = new CultureInfo(DefaultCulture);
                CultureInfo.CurrentCulture = fallback;
                CultureInfo.CurrentUICulture = fallback;
            }

            await _next(context);
        }
    }
}
