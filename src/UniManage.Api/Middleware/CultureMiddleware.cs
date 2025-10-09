using System.Globalization;

namespace UniManage.Api.Middleware
{
    public class CultureMiddleware
    {
        private readonly RequestDelegate _next;

        public CultureMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var cultureCode = context.Request.Headers["Accept-Language"].ToString();
            if (string.IsNullOrWhiteSpace(cultureCode))
            {
                cultureCode = "en-US"; // fallback
            }

            try
            {
                var culture = new CultureInfo(cultureCode);
                CultureInfo.CurrentCulture = culture;
                CultureInfo.CurrentUICulture = culture;
            }
            catch (CultureNotFoundException)
            {
                var fallback = new CultureInfo("en-US");
                CultureInfo.CurrentCulture = fallback;
                CultureInfo.CurrentUICulture = fallback;
            }

            await _next(context);
        }
    }
}
