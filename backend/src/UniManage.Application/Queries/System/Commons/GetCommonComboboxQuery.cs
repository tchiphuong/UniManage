using FluentValidation;
using MediatR;
using System.Text;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.Commons
{
    #region Query

    public sealed class GetCommonComboboxQuery : BaseQuery, IRequest<ApiResponse<List<ComboboxModel>>>
    {
        public string TypeKey { get; init; } = default!;
        public string? Keyword { get; set; }
    }

    #endregion

    #region Validator

    public sealed class GetCommonComboboxQueryValidator : AbstractValidator<GetCommonComboboxQuery>
    {
        public GetCommonComboboxQueryValidator()
        {
            RuleFor(x => x.TypeKey)
                .NotEmpty().WithMessage(CoreResource.validation_required);
        }
    }

    #endregion

    #region Handler

    public sealed class GetCommonComboboxQueryHandler : IRequestHandler<GetCommonComboboxQuery, ApiResponse<List<ComboboxModel>>>
    {
        public async Task<ApiResponse<List<ComboboxModel>>> Handle(GetCommonComboboxQuery request, CancellationToken ct)
        {
            var log = new CoreLogModel(request.HeaderInfo ?? new HeaderInfo())
            {
                Parameter = new List<CoreParamModel>
                {
                    new CoreParamModel(nameof(request.TypeKey), request.TypeKey),
                    new CoreParamModel(nameof(request.Keyword), request.Keyword)
                }
            };

            try
            {
                using (var db = new DbContext())
                {
                    var isEnglish = request.HeaderInfo?.Language?.StartsWith("en", StringComparison.OrdinalIgnoreCase) == true;
                    var nameColumn = isEnglish ? "ValueNameEn" : "ValueNameVi";

                    var query = new StringBuilder();
                    query.AppendLine($@"
                        SELECT 
                            ValueKey, 
                            {nameColumn} AS ValueName,
                            Sort
                        FROM sy_commons
                        WHERE TypeKey = @TypeKey 
                            AND Status = 1");

                    if (!string.IsNullOrEmpty(request.Keyword))
                    {
                        query.AppendLine($"AND (ValueKey LIKE @Keyword OR {nameColumn} LIKE @Keyword)");
                    }

                    query.AppendLine($"ORDER BY Sort, {nameColumn}");

                    var commons = await db.QueryAsync<dynamic>(
                        query.ToString(),
                        new
                        {
                            request.TypeKey,
                            Keyword = string.IsNullOrEmpty(request.Keyword) ? null : $"%{request.Keyword}%"
                        },
                        ct);

                    var items = commons.Select(c => new ComboboxModel
                    {
                        Code = c.ValueKey,
                        Name = c.ValueName,
                        Status = 1,
                        ExtData = new Dictionary<string, object>
                        {
                            ["Sort"] = c.Sort
                        }
                    }).ToList();

                    var response = ResponseHelper.Success(items);
                    log.Result = new { Count = items.Count, TypeKey = request.TypeKey };
                    log.ReturnCode = response.ReturnCode;
                    log.Message = response.Message;
                    UniLogManager.WriteApiLog(log);

                    return response;
                }
            }
            catch (Exception ex)
            {
                log.IsException = true;
                log.Message = ex.ToString();
                log.ReturnCode = CoreApiReturnCode.ExceptionOccurred;
                UniLogManager.WriteApiLog(log);

                UniLogger.Error($"Error getting common combobox: {ex.Message}", ex);
                return ResponseHelper.Error<List<ComboboxModel>>(CoreResource.common_exceptionOccurred);
            }
        }
    }

    #endregion
}
