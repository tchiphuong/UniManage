using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace UniManage.Api.Extensions;

/// <summary>
/// Extension methods for ControllerBase
/// </summary>
public static class ControllerBaseExtensions
{
    /// <summary>
    /// Get current authenticated username from JWT token claims
    /// </summary>
    public static string GetCurrentUsername(this ControllerBase controller)
    {
        var username = controller.User?.FindFirst(ClaimTypes.Name)?.Value
                    ?? controller.User?.FindFirst("sub")?.Value
                    ?? controller.User?.FindFirst("username")?.Value;

        if (string.IsNullOrEmpty(username))
        {
            throw new UnauthorizedAccessException("Username not found in token claims");
        }

        return username;
    }

    /// <summary>
    /// Get current authenticated user ID from JWT token claims
    /// </summary>
    public static string? GetCurrentUserId(this ControllerBase controller)
    {
        return controller.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value
            ?? controller.User?.FindFirst("sub")?.Value;
    }

    /// <summary>
    /// Check if current user is authenticated
    /// </summary>
    public static bool IsAuthenticated(this ControllerBase controller)
    {
        return controller.User?.Identity?.IsAuthenticated ?? false;
    }
}
