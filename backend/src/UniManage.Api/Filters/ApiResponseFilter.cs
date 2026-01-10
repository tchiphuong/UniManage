using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UniManage.Model.Common;

namespace UniManage.Api.Filters;

/// <summary>
/// Filter tự động convert ApiResponse returnCode thành HTTP status code
/// </summary>
public class ApiResponseFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        // Nothing to do before action executes
    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Result is ObjectResult objectResult && objectResult.Value != null)
        {
            var valueType = objectResult.Value.GetType();

            // Check if result is ApiResponse<T>
            if (valueType.IsGenericType && valueType.GetGenericTypeDefinition() == typeof(ApiResponse<>))
            {
                var returnCodeProperty = valueType.GetProperty("ReturnCode");
                if (returnCodeProperty != null)
                {
                    var returnCode = (int)(returnCodeProperty.GetValue(objectResult.Value) ?? 0);

                    // Map returnCode to HTTP status code
                    objectResult.StatusCode = returnCode switch
                    {
                        0 => 200,           // Success
                        400 => 400,         // Bad Request
                        401 => 401,         // Unauthorized
                        403 => 403,         // Forbidden
                        404 => 404,         // Not Found
                        409 => 409,         // Conflict
                        429 => 429,         // Too Many Requests
                        500 => 500,         // Internal Server Error
                        _ => returnCode >= 400 ? returnCode : 200
                    };
                }
            }
        }
    }
}
