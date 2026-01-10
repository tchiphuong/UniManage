using System.Collections.Generic;

namespace UniManage.Model.Common;

/// <summary>
/// Standard combobox item model with basic fields and extensible metadata
/// </summary>
public class ComboboxItemDto
{
    /// <summary>
    /// Item value (Id, Code, etc.)
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Display label
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Optional status (0=Inactive, 1=Active, etc.)
    /// </summary>
    public byte? Status { get; set; }

    /// <summary>
    /// Additional fields for advanced scenarios
    /// Example: { "ParentCode": "DEP001", "SortOrder": 10, "Icon": "user" }
    /// </summary>
    public Dictionary<string, object>? Metadata { get; set; }
}

/// <summary>
/// Generic combobox item model with typed value
/// </summary>
public class ComboboxItemDto<TValue>
{
    /// <summary>
    /// Item value (typed)
    /// </summary>
    public TValue Value { get; set; } = default!;

    /// <summary>
    /// Display label
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Optional status (0=Inactive, 1=Active, etc.)
    /// </summary>
    public byte? Status { get; set; }

    /// <summary>
    /// Additional fields for advanced scenarios
    /// </summary>
    public Dictionary<string, object>? Metadata { get; set; }
}
