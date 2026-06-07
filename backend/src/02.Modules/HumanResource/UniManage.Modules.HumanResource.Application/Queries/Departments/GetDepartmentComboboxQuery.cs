using MediatR;
using Newtonsoft.Json;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;

namespace UniManage.Modules.HumanResource.Application.Queries.Departments;

#region Query

/// <summary>
/// Query to get list of departments for dropdown
/// </summary>
public sealed class GetDepartmentComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItem>>>
{
}

#endregion

#region Handler

/// <summary>
/// Handler for GetDepartmentComboboxQuery
/// </summary>
public sealed class GetDepartmentComboboxQueryHandler : IRequestHandler<GetDepartmentComboboxQuery, ApiResponse<List<ComboboxItem>>>
{
    public async Task<ApiResponse<List<ComboboxItem>>> Handle(GetDepartmentComboboxQuery request, CancellationToken cancellationToken)
    {
        var log = new ApiLogModel(request.HeaderInfo ?? new HeaderInfo())
        {
            Parameter = new List<LogParamModel>()
        };

        try
        {
            using (var db = new DbContext())
            {
                var isEnglish = request.HeaderInfo?.Language?.StartsWith("en", StringComparison.OrdinalIgnoreCase) == true;

                var departments = await db.QueryAsync<dynamic>(
                    $"""
                    SELECT 
                        Code, 
                        {(isEnglish ? "NameEn" : "NameVi")} AS Name, 
                        Description
                    FROM HrDepartments
                    ORDER BY {(isEnglish ? "NameEn" : "NameVi")}
                    """,
                    cancellationToken: cancellationToken);

                var items = departments.Select(d => new ComboboxItem
                {
                    Code = d.Code,
                    Name = d.Name,
                    Status = 1, // Default active
                    ExtData = new Dictionary<string, object>
                    {
                        ["Description"] = d.Description ?? ""
                    }
                }).ToList();

                var response = ResponseHelper.Success(items);
                log.Result = new { Count = items.Count };
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                UniLogger.Info(JsonConvert.SerializeObject(log));

                return response;
            }
        }
        catch (Exception ex)
        {
            log.IsException = true;
            log.Message = ex.Message;
            log.ReturnCode = 500;
            UniLogger.Error(JsonConvert.SerializeObject(log));

            return ResponseHelper.Error<List<ComboboxItem>>($"Failed to get departments: {ex.Message}");
        }
    }
}

#endregion



