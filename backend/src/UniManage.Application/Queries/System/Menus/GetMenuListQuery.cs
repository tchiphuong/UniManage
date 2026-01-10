using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Core.Constant;
using UniManage.Core.Database;
using UniManage.Core.Logging;
using UniManage.Core.Utilities;
using UniManage.Model.Common;
using UniManage.Resource;

namespace UniManage.Application.Queries.System.Menus;

#region Query

public sealed class GetMenuListQuery : BaseQuery, IRequest<ApiResponse<List<GetMenuListQuery.Response>>>
{
    public sealed class Response
    {
        public int Id { get; init; }
        public string Code { get; init; } = default!;
        public string FunctionCode { get; init; } = default!;
        public string? ParentCode { get; init; }
        public string ResourceKey { get; init; } = default!;
        public string? Url { get; init; }
        public string? Icon { get; init; }
        public List<Response> Children { get; set; } = new();
    }
}

#endregion

#region Validator

public sealed class GetMenuListQueryValidator : AbstractValidator<GetMenuListQuery>
{
    public GetMenuListQueryValidator()
    {
        RuleFor(x => x.HeaderInfo)
            .NotNull().WithMessage("HeaderInfo is required")
            .DependentRules(() =>
            {
                RuleFor(x => x.HeaderInfo!.Username)
                    .NotEmpty().WithMessage("Username is required");
            });
    }
}

#endregion

#region Handler

public sealed class GetMenuListQueryHandler : IRequestHandler<GetMenuListQuery, ApiResponse<List<GetMenuListQuery.Response>>>
{
    public async Task<ApiResponse<List<GetMenuListQuery.Response>>> Handle(GetMenuListQuery request, CancellationToken ct)
    {
        var log = new CoreLogModel(request.HeaderInfo)
        {
            Parameter = new List<CoreParamModel>
            {
                new CoreParamModel("Username", request.HeaderInfo?.Username ?? "Anonymous")
            }
        };

        using (var dbContext = new DbContext())
        {
            try
            {
                var username = request.HeaderInfo!.Username;

                // Lấy menus user có quyền truy cập
                var userMenusSql = @"SELECT DISTINCT m.Id, m.Code, m.FunctionCode, m.ParentCode, m.ResourceKey, m.Url, m.Icon 
                           FROM sy_menus m
                           INNER JOIN sy_functions f ON m.FunctionCode = f.Code
                           INNER JOIN sy_role_permissions rp ON f.Code = rp.FunctionCode
                           INNER JOIN sy_user_roles ur ON rp.RoleCode = ur.RoleCode
                           INNER JOIN sy_users u ON ur.Username = u.Username
                           WHERE u.Username = @Username";

                var userMenus = (await dbContext.QueryAsync<GetMenuListQuery.Response>(userMenusSql, new { Username = username }, ct)).ToList();

                // Nếu user không có quyền truy cập bất kỳ menu nào → 403 Forbidden
                if (!userMenus.Any())
                {
                    var forbiddenResponse = ResponseHelper.Forbidden<List<GetMenuListQuery.Response>>();
                    log.Result = forbiddenResponse;
                    log.ReturnCode = forbiddenResponse.ReturnCode;
                    log.Message = forbiddenResponse.Message;
                    UniLogManager.WriteApiLog(log);
                    return forbiddenResponse;
                }

                // Lấy tất cả parent codes đệ quy để đảm bảo tree structure hoàn chỉnh
                var allMenuCodes = userMenus.Select(m => m.Code).ToHashSet();
                var parentCodes = userMenus.Where(m => !string.IsNullOrEmpty(m.ParentCode)).Select(m => m.ParentCode!).ToHashSet();
                var missingParentCodes = parentCodes.Except(allMenuCodes).ToList();

                // Nếu có parent menus thiếu, lấy chúng từ DB (đệ quy lấy tất cả parents)
                var allMenus = userMenus;
                if (missingParentCodes.Any())
                {
                    var parentMenusSql = @"WITH MenuHierarchy AS (
                                            SELECT Id, Code, FunctionCode, ParentCode, ResourceKey, Url, Icon
                                            FROM sy_menus
                                            WHERE Code IN @Codes
                                            UNION ALL
                                            SELECT m.Id, m.Code, m.FunctionCode, m.ParentCode, m.ResourceKey, m.Url, m.Icon
                                            FROM sy_menus m
                                            INNER JOIN MenuHierarchy mh ON m.Code = mh.ParentCode
                                        )
                                        SELECT DISTINCT Id, Code, FunctionCode, ParentCode, ResourceKey, Url, Icon 
                                        FROM MenuHierarchy";

                    var parentMenus = (await dbContext.QueryAsync<GetMenuListQuery.Response>(parentMenusSql, new { Codes = missingParentCodes }, ct)).ToList();
                    allMenus = userMenus.Concat(parentMenus).GroupBy(m => m.Code).Select(g => g.First()).ToList();
                }

                // Build tree structure đệ quy
                var menuDict = allMenus.ToDictionary(m => m.Code);
                var rootMenus = new List<GetMenuListQuery.Response>();

                foreach (var menu in allMenus)
                {
                    if (string.IsNullOrEmpty(menu.ParentCode))
                    {
                        rootMenus.Add(menu);
                    }
                    else if (menuDict.TryGetValue(menu.ParentCode, out var parent))
                    {
                        parent.Children.Add(menu);
                    }
                }

                var response = ResponseHelper.Success(rootMenus, CoreResource.Common_msg_GetSuccess);
                log.Result = response;
                log.ReturnCode = response.ReturnCode;
                log.Message = response.Message;
                UniLogManager.WriteApiLog(log);

                return response;
            }
            catch (Exception ex)
            {
                UniLogger.Error($"Error retrieving menu tree: {ex.Message}", ex);

                var response = ResponseHelper.Error<List<GetMenuListQuery.Response>>(CoreResource.Common_msg_ExceptionOccurred);
                log.IsException = 1;
                log.Message = ex.Message;
                log.ReturnCode = response.ReturnCode;
                UniLogManager.WriteApiLog(log);

                return response;
            }
        }
    }
}

#endregion
