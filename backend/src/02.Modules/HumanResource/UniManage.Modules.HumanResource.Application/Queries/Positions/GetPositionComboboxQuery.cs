using MediatR;
using Newtonsoft.Json;
using UniManage.Shared.Application.Models;
using UniManage.Shared.Infrastructure.Database;
using UniManage.Shared.Infrastructure.Logging;
using UniManage.Shared.Infrastructure.Utilities;

namespace UniManage.Modules.HumanResource.Application.Queries.Positions;

#region Query

/// <summary>
/// Query to get list of positions for dropdown
/// </summary>
public sealed class GetPositionComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItem>>>
{
}

#endregion

#region Handler

/// <summary>
/// Handler for GetPositionComboboxQuery
/// </summary>
public sealed class GetPositionComboboxQueryHandler : IRequestHandler<GetPositionComboboxQuery, ApiResponse<List<ComboboxItem>>>
{
    public async Task<ApiResponse<List<ComboboxItem>>> Handle(GetPositionComboboxQuery request, CancellationToken cancellationToken)
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

                var positions = await db.QueryAsync<dynamic>(
                    $"""
                    SELECT 
                        PositionCode AS Code, 
                        {(isEnglish ? "NameEn" : "NameVi")} AS Name, 
                        Description
                    FROM HrPositions
                    ORDER BY {(isEnglish ? "NameEn" : "NameVi")}
                    """,
                    cancellationToken: cancellationToken);

                var items = positions.Select(p => new ComboboxItem
                {
                    Code = p.Code,
                    Name = p.Name,
                    Status = 1, // Default active
                    ExtData = new Dictionary<string, object>
                    {
                        ["Description"] = p.Description ?? ""
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

            return ResponseHelper.Error<List<ComboboxItem>>($"Failed to get positions: {ex.Message}");
        }
    }
}

#endregion




