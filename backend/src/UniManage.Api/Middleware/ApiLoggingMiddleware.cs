using log4net;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using System.Text;

namespace UniManage.Api.Middleware;

/// <summary>
/// Middleware to log API requests and responses
/// </summary>
public class ApiLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILog _logger;
    private readonly string[] _sensitiveData = { "password", "token", "secret", "authorization" };

    public ApiLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
        _logger = LogManager.GetLogger(typeof(ApiLoggingMiddleware));
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        var originalBody = context.Response.Body;
        var requestBody = string.Empty;

        try
        {
            // Log request
            var correlationId = GetCorrelationId(context);
            var user = context.User?.Identity?.Name ?? "anonymous";
            var method = context.Request.Method;
            var path = context.Request.GetDisplayUrl();

            // Determine API name for log file
            string apiName = "general";
            var endpoint = context.GetEndpoint();
            if (endpoint != null)
            {
                var routeValues = context.GetRouteData().Values;
                if (routeValues.TryGetValue("controller", out var controller) &&
                    routeValues.TryGetValue("action", out var action))
                {
                    apiName = $"{controller}-{action}".ToLower();
                }
            }

            // Set log4net context
            LogicalThreadContext.Properties["api"] = apiName;
            LogicalThreadContext.Properties["cid"] = correlationId;
            LogicalThreadContext.Properties["user"] = user;
            LogicalThreadContext.Properties["method"] = method;
            LogicalThreadContext.Properties["path"] = path;

            // Capture request body for non-GET methods
            if (!HttpMethods.IsGet(context.Request.Method))
            {
                context.Request.EnableBuffering();
                using var reader = new StreamReader(
                    context.Request.Body,
                    encoding: Encoding.UTF8,
                    detectEncodingFromByteOrderMarks: false,
                    leaveOpen: true);
                requestBody = await reader.ReadToEndAsync();

                // Reset position to allow re-reading
                context.Request.Body.Position = 0;
            }

            // Log masked request
            var maskedRequest = MaskSensitiveData(requestBody);
            if (maskedRequest.Length > 64000) // Truncate if > 64KB
            {
                maskedRequest = maskedRequest[..64000] + "... [truncated]";
            }

            // Capture response
            using var memStream = new MemoryStream();
            context.Response.Body = memStream;

            await _next(context);

            memStream.Position = 0;
            var responseBody = await new StreamReader(memStream).ReadToEndAsync();
            memStream.Position = 0;
            await memStream.CopyToAsync(originalBody);

            sw.Stop();
            var status = context.Response.StatusCode;
            var elapsed = sw.ElapsedMilliseconds;

            // Update context for response log
            LogicalThreadContext.Properties["status"] = status;
            LogicalThreadContext.Properties["ms"] = elapsed;

            // Log masked response
            var maskedResponse = MaskSensitiveData(responseBody);
            if (maskedResponse.Length > 64000)
            {
                maskedResponse = maskedResponse[..64000] + "... [truncated]";
            }

            var sb = new StringBuilder();
            sb.AppendLine($"[{correlationId}] SUMMARY: {method} {path} -> {status} ({elapsed}ms)");
            sb.AppendLine("REQUEST BODY:");
            sb.AppendLine(string.IsNullOrWhiteSpace(maskedRequest) ? "(empty)" : maskedRequest);
            sb.AppendLine("RESPONSE BODY:");
            sb.AppendLine(string.IsNullOrWhiteSpace(maskedResponse) ? "(empty)" : maskedResponse);

            _logger.Info(sb.ToString());
        }
        finally
        {
            context.Response.Body = originalBody;
        }
    }

    private string GetCorrelationId(HttpContext context)
    {
        if (context.Request.Headers.TryGetValue("X-Correlation-Id", out StringValues correlationId))
        {
            return correlationId.ToString();
        }
        return Guid.NewGuid().ToString("N");
    }

    private string MaskSensitiveData(string input)
    {
        if (string.IsNullOrEmpty(input)) return input;

        var result = input;
        foreach (var term in _sensitiveData)
        {
            var pattern = $@"""{term}""\s*:\s*""[^""]+""";
            result = System.Text.RegularExpressions.Regex.Replace(
                result,
                pattern,
                $@"""{term}"":""***""",
                System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }
        return result;
    }
}
