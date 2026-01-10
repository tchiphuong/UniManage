using MediatR;
using Newtonsoft.Json;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Application.Queries.HR.Departments;

#region Query

/// <summary>
/// Query to get list of departments for dropdown
/// </summary>
public sealed class GetDepartmentComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItemDto>>>
{
}

#endregion

#region Handler

/// <summary>
/// Handler for GetDepartmentComboboxQuery
/// </summary>
public sealed class GetDepartmentComboboxQueryHandler : IRequestHandler<GetDepartmentComboboxQuery, ApiResponse<List<ComboboxItemDto>>>
{
    public async Task<ApiResponse<List<ComboboxItemDto>>> Handle(GetDepartmentComboboxQuery request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo ?? new HeaderInfo())
        {
            Parameter = new List<CoreParamModel>()
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
                    FROM hr_departments
                    ORDER BY {(isEnglish ? "NameEn" : "NameVi")}
                    """,
                    cancellationToken: ct);

                var items = departments.Select(d => new ComboboxItemDto
                {
                    Value = d.Code,
                    Label = d.Name,
                    Status = 1, // Default active
                    Metadata = new Dictionary<string, object>
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
            log.IsException = 1;
            log.Message = ex.Message;
            log.ReturnCode = 500;
            UniLogger.Error(JsonConvert.SerializeObject(log));

            return ResponseHelper.Error<List<ComboboxItemDto>>($"Failed to get departments: {ex.Message}");
        }
    }
}

#endregion
