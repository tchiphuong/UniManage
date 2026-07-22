using Dapper;
using FluentValidation;
using MediatR;
using UniManage.Shared.Application.Interfaces;
using UniManage.Shared.Resource;

namespace UniManage.Shared.Application.Modules.SySystem.Queries 
{
    #region Query

    /// <summary>
    /// Query to retrieve the navigation bar tree structure.
    /// Returns a 3-level hierarchy: Module → Function Group → Function.
    /// Only returns items the user has permission to access.
    /// </summary>
    public sealed class GetNavbarTreeQuery : BaseListQuery, IRequest<ApiResponse<List<GetNavbarTreeQuery.ModuleNode>>>
    {
        /// <summary>
        /// Represents a module node (Level 1) in the navbar tree.
        /// </summary>
        public sealed class ModuleNode
        {
            public string Code { get; init; } = default!;
            public string ResourceKey { get; init; } = default!;
            public string? Icon { get; init; }
            public int Sort { get; init; }
            public List<GroupNode> Children { get; set; } = new();
        }

        /// <summary>
        /// Represents a function group node (Level 2) in the navbar tree.
        /// </summary>
        public sealed class GroupNode
        {
            public string Code { get; init; } = default!;
            public string ResourceKey { get; init; } = default!;
            public string? Icon { get; init; }
            public int Sort { get; init; }
            public List<FunctionNode> Children { get; set; } = new();
        }

        /// <summary>
        /// Represents a function node (Level 3 - leaf) in the navbar tree.
        /// </summary>
        public sealed class FunctionNode
        {
            public string Code { get; init; } = default!;
            public string ResourceKey { get; init; } = default!;
            public string? Icon { get; init; }
            public int SortOrder { get; init; }
        }
    }

    #endregion

    #region Validator

    public sealed class GetNavbarTreeQueryValidator : AbstractValidator<GetNavbarTreeQuery>
    {
        public GetNavbarTreeQueryValidator()
        {
            RuleFor(x => x.HeaderInfo)
                .NotEmpty()
                .WithMessage(string.Format(CoreResource.validation_required, "HeaderInfo"))
                .DependentRules(() =>
                {
                    RuleFor(x => x.HeaderInfo!.Username)
                        .NotEmpty()
                        .WithMessage(string.Format(CoreResource.validation_required, CoreResource.lbl_username));
                });
        }
    }

    #endregion

    #region Handler

    /// <summary>
    /// Handler that builds a 3-level navbar tree from sy_modules → sy_function_groups → sy_functions,
    /// filtered by the current user's role permissions.
    /// </summary>
    public sealed class GetNavbarTreeQueryHandler : IRequestHandler<GetNavbarTreeQuery, ApiResponse<List<GetNavbarTreeQuery.ModuleNode>>>
    {
        public async Task<ApiResponse<List<GetNavbarTreeQuery.ModuleNode>>> Handle(GetNavbarTreeQuery request, CancellationToken ct)
        {
            using (var dbContext = new DbContext())
            {
                var username = request.HeaderInfo!.Username;

                // Lấy danh sách FunctionCode mà user có quyền truy cập
                var permittedFunctionsSql = @"
                    SELECT DISTINCT rp.FunctionCode
                    FROM sy_role_permissions rp
                    INNER JOIN sy_user_roles ur ON rp.RoleCode = ur.RoleCode
                    WHERE ur.Username = @Username";

                var permittedFunctions = (await dbContext.QueryAsync<string>(
                    permittedFunctionsSql, new { Username = username }, ct)).ToHashSet();

                if (!permittedFunctions.Any())
                {
                    return ResponseHelper.Forbidden<List<GetNavbarTreeQuery.ModuleNode>>();
                }

                // Lấy tất cả functions mà user có quyền (cấp 3)
                var functionsSql = @"
                    SELECT f.Code, f.ResourceKey, f.Icon, f.SortOrder, f.GroupCode
                    FROM sy_functions f
                    INNER JOIN sy_function_groups fg ON f.GroupCode = fg.Code
                    WHERE f.Code IN @PermittedCodes
                    AND f.IsActive = 1
                    ORDER BY f.SortOrder";

                var functions = (await dbContext.QueryAsync<dynamic>(
                    functionsSql, new { PermittedCodes = permittedFunctions }, ct)).ToList();

                if (!functions.Any())
                {
                    return ResponseHelper.Forbidden<List<GetNavbarTreeQuery.ModuleNode>>();
                }

                // Lấy GroupCodes cần hiển thị
                var neededGroupCodes = functions.Select(f => (string)f.GroupCode).Distinct().ToList();

                // Lấy tất cả function groups cần thiết (cấp 2)
                var groupsSql = @"
                    SELECT fg.Code, fg.ResourceKey, fg.Icon, fg.Sort, fg.ModuleCode
                    FROM sy_function_groups fg
                    WHERE fg.Code IN @GroupCodes
                    AND fg.IsActive = 1
                    ORDER BY fg.Sort";

                var groups = (await dbContext.QueryAsync<dynamic>(
                    groupsSql, new { GroupCodes = neededGroupCodes }, ct)).ToList();

                // Lấy ModuleCodes cần hiển thị
                var neededModuleCodes = groups.Select(g => (string)g.ModuleCode).Distinct().ToList();

                // Lấy tất cả modules cần thiết (cấp 1)
                var modulesSql = @"
                    SELECT m.Code, m.ResourceKey, m.Icon, m.Sort
                    FROM sy_modules m
                    WHERE m.Code IN @ModuleCodes
                    AND m.IsActive = 1
                    ORDER BY m.Sort";

                var modules = (await dbContext.QueryAsync<dynamic>(
                    modulesSql, new { ModuleCodes = neededModuleCodes }, ct)).ToList();

                // Build tree: Module → Group → Function
                var result = new List<GetNavbarTreeQuery.ModuleNode>();

                foreach (var mod in modules)
                {
                    var moduleNode = new GetNavbarTreeQuery.ModuleNode
                    {
                        Code = mod.Code,
                        ResourceKey = mod.ResourceKey,
                        Icon = mod.Icon,
                        Sort = mod.Sort
                    };

                    var moduleGroups = groups
                        .Where(g => (string)g.ModuleCode == mod.Code)
                        .OrderBy(g => (int)g.Sort);

                    foreach (var grp in moduleGroups)
                    {
                        var groupNode = new GetNavbarTreeQuery.GroupNode
                        {
                            Code = grp.Code,
                            ResourceKey = grp.ResourceKey,
                            Icon = grp.Icon,
                            Sort = grp.Sort
                        };

                        var groupFunctions = functions
                            .Where(f => (string)f.GroupCode == grp.Code)
                            .OrderBy(f => (int)f.SortOrder);

                        foreach (var func in groupFunctions)
                        {
                            groupNode.Children.Add(new GetNavbarTreeQuery.FunctionNode
                            {
                                Code = func.Code,
                                ResourceKey = func.ResourceKey,
                                Icon = func.Icon,
                                SortOrder = func.SortOrder
                            });
                        }

                        // Chỉ thêm group nếu có ít nhất 1 function
                        if (groupNode.Children.Any())
                        {
                            moduleNode.Children.Add(groupNode);
                        }
                    }

                    // Chỉ thêm module nếu có ít nhất 1 group
                    if (moduleNode.Children.Any())
                    {
                        result.Add(moduleNode);
                    }
                }

                return ResponseHelper.Success(result, CoreResource.common_getSuccess);
            }
        }
    }

    #endregion
}