using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using System.Diagnostics;
using System.Text;
using Serilog;
using Serilog.Context;

namespace UniManage.Api.Middleware;

/// <summary>
/// Middleware to log API requests and responses (Serilog implementation)
/// </summary>
public class ApiLoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly string[] _sensitiveData = { "password", "token", "secret", "authorization" };

    public ApiLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var sw = Stopwatch.StartNew();
        var originalBody = context.Response.Body;
        var requestBody = string.Empty;

        // Extract Context Info
        var correlationId = GetCorrelationId(context);
        var user = context.User?.Identity?.Name ?? "anonymous";
        var method = context.Request.Method;
        var path = context.Request.GetDisplayUrl();
        var apiName = GetApiName(context);
        var ipAddress = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";

        // Push properties to LogContext for Sifting/Map sink
        using (LogContext.PushProperty("ApiName", apiName))
        using (LogContext.PushProperty("Cid", correlationId))
        using (LogContext.PushProperty("Username", user))
        using (LogContext.PushProperty("Method", method))
        using (LogContext.PushProperty("Path", path))
        using (LogContext.PushProperty("IpAddress", ipAddress))
        {
            try
            {
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

                // Capture Headers
                var headers = new StringBuilder();
                foreach (var header in context.Request.Headers)
                {
                    // Skip sensitive headers or mask them
                    if (header.Key.Equals("Authorization", StringComparison.OrdinalIgnoreCase) ||
                        header.Key.Equals("Cookie", StringComparison.OrdinalIgnoreCase))
                    {
                        headers.AppendLine($"{header.Key}: ***");
                    }
                    else
                    {
                        headers.AppendLine($"{header.Key}: {header.Value}");
                    }
                }

                // Log masked request (Logic kept but handled in final summary)
                var maskedRequest = MaskSensitiveData(requestBody);
                if (maskedRequest.Length > 64000)
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

                // Log masked response
                var maskedResponse = MaskSensitiveData(responseBody);
                if (maskedResponse.Length > 64000)
                {
                    maskedResponse = maskedResponse[..64000] + "... [truncated]";
                }

                var sb = new StringBuilder();
                sb.AppendLine($"[{correlationId}] SUMMARY: {method} {path} -> {status} ({elapsed}ms)");
                sb.AppendLine($"IP: {ipAddress}");
                sb.AppendLine("HEADERS:");
                sb.AppendLine(headers.ToString().TrimEnd());
                sb.AppendLine("REQUEST BODY:");
                sb.AppendLine(string.IsNullOrWhiteSpace(maskedRequest) ? "(empty)" : maskedRequest);
                sb.AppendLine("RESPONSE BODY:");
                sb.AppendLine(string.IsNullOrWhiteSpace(maskedResponse) ? "(empty)" : maskedResponse);

                // Additional properties for final log
                using (LogContext.PushProperty("Status", status))
                using (LogContext.PushProperty("ExecutionTime", elapsed))
                {
                    Log.Information(sb.ToString());
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
    }

    private string GetApiName(HttpContext context)
    {
        string apiName = "general";
        var endpoint = context.GetEndpoint();
        
        if (endpoint is RouteEndpoint routeEndpoint)
        {
            // Use Route Pattern (e.g. "api/users/{id}") instead of Controller-Action
            var routePattern = routeEndpoint.RoutePattern.RawText;
            if (!string.IsNullOrEmpty(routePattern))
            {
                apiName = $"{routePattern}_{context.Request.Method}"
                    .Replace("/", "_")
                    .Replace("{", "")
                    .Replace("}", "")
                    .ToLower();
            }
        }
        else if (endpoint != null)
        {
            var routeValues = context.GetRouteData().Values;
            if (routeValues.TryGetValue("controller", out var controller) &&
                routeValues.TryGetValue("action", out var action))
            {
                apiName = $"{controller}-{action}".ToLower();
            }
        }
        return apiName;
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
