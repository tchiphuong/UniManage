using System.Text.Json.Serialization;

namespace UniManage.Shared.Domain.Models;

/// <summary>
/// Model ghi log API t?p trung
/// </summary>
public class ApiLogModel
{
    public HeaderInfo? HeaderInfo { get; set; }

    /// <summary>
    /// Danh sách tham s? d?u vào (Chu?n m?i: Parameter)
    /// </summary>
    public List<LogParamModel> Parameter { get; set; } = new();

    /// <summary>
    /// Alias cho Parameter d? tuong thích v?i các Handler cu
    /// </summary>
    [JsonIgnore]
    public List<LogParamModel> Parameters 
    { 
        get => Parameter; 
        set => Parameter = value; 
    }

    public object? Result { get; set; }
    
    public string? Message { get; set; }

    /// <summary>
    /// Ðánh d?u ngo?i l? x?y ra
    /// </summary>
    public bool IsException { get; set; }

    /// <summary>
    /// Mã ph?n h?i API
    /// </summary>
    public int ReturnCode { get; set; }

    public string? Method { get; set; }
    
    public string? Path { get; set; }

    public ApiLogModel() { }

    public ApiLogModel(HeaderInfo? headerInfo)
    {
        HeaderInfo = headerInfo;
    }
}

public class LogParamModel
{
    public string Name { get; set; } = string.Empty;
    public object? Value { get; set; }

    public LogParamModel(string name, object? value)
    {
        Name = name;
        Value = value;
    }

    public LogParamModel() { }
}
