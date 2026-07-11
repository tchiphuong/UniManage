using System.Collections.Generic;
using System.Linq;

namespace UniManage.Tools.CodeGen
{
    public static partial class Templates
    {
        public static string GetModelsTemplate(string module, string entity, List<ColumnInfo> allColumns, List<string> gridCols)
        {
            var listProps = gridCols
                .Select(colName => allColumns.FirstOrDefault(c => c.Name == colName))
                .Where(c => c != null)
                .Select(c => 
                {
                    var suffix = (c!.CSharpType == "string" && !c.IsNullable) ? " = default!;" : "";
                    return $"        public {c.CSharpType} {c.Name} {{ get; init; }}{suffix}";
                });

            if (!gridCols.Contains("Id"))
            {
                listProps = listProps.Prepend("        public long Id { get; init; }");
            }

            var detailProps = allColumns
                .Select(c => 
                {
                    var suffix = (c.CSharpType == "string" && !c.IsNullable) ? " = default!;" : "";
                    return $"        public {c.CSharpType} {c.Name} {{ get; init; }}{suffix}";
                });

            return $@"using System;

namespace UniManage.Modules.{module}.Application.Models.{entity}
{{
    /// <summary>
    /// Model danh sách {entity}
    /// </summary>
    public sealed class {entity}ListModel
    {{
{string.Join("\n", listProps)}
    }}

    /// <summary>
    /// Model chi tiết {entity}
    /// </summary>
    public sealed class {entity}DetailModel
    {{
{string.Join("\n", detailProps)}
    }}
}}";
        }
    }
}
