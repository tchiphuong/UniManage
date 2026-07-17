using Microsoft.OpenApi;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace UniManage.WebApi.Filters;

/// <summary>
/// Filter to hide specific parameters from Swagger documentation
/// </summary>
public class SwaggerIgnoreFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        if (operation.Parameters == null) return;

        // Remove parameters related to HeaderInfo which are internal/system properties
        var parametersToRemove = operation.Parameters
            .Where(p => p.Name != null && (p.Name.StartsWith("HeaderInfo.") || p.Name == "HeaderInfo"))
            .ToList();

        foreach (var parameter in parametersToRemove)
        {
            operation.Parameters.Remove(parameter);
        }
    }


}


