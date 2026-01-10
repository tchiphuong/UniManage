using MediatR;
using Newtonsoft.Json;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;

namespace UniManage.Application.Queries.HR.Positions;

#region Query

/// <summary>
/// Query to get list of positions for dropdown
/// </summary>
public sealed class GetPositionComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxItemDto>>>
{
}

#endregion

#region Handler

/// <summary>
/// Handler for GetPositionComboboxQuery
/// </summary>
public sealed class GetPositionComboboxQueryHandler : IRequestHandler<GetPositionComboboxQuery, ApiResponse<List<ComboboxItemDto>>>
{
    public async Task<ApiResponse<List<ComboboxItemDto>>> Handle(GetPositionComboboxQuery request, CancellationToken ct)
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

                var positions = await db.QueryAsync<dynamic>(
                    $"""
                    SELECT 
                        PositionCode AS Code, 
                        {(isEnglish ? "NameEn" : "NameVi")} AS Name, 
                        Description
                    FROM hr_positions
                    ORDER BY {(isEnglish ? "NameEn" : "NameVi")}
                    """,
                    cancellationToken: ct);

                var items = positions.Select(p => new ComboboxItemDto
                {
                    Value = p.Code,
                    Label = p.Name,
                    Status = 1, // Default active
                    Metadata = new Dictionary<string, object>
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
            log.IsException = 1;
            log.Message = ex.Message;
            log.ReturnCode = 500;
            UniLogger.Error(JsonConvert.SerializeObject(log));

            return ResponseHelper.Error<List<ComboboxItemDto>>($"Failed to get positions: {ex.Message}");
        }
    }
}

#endregion
