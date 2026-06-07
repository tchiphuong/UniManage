using System.Text.Json.Serialization;

namespace UniManage.Shared.Application.Models;

/// <summary>
/// Model ghi log API tập trung
/// </summary>
public class ApiLogModel
{
    public HeaderInfo? HeaderInfo { get; set; }

    /// <summary>
    /// Danh sách tham số đầu vào (Chuẩn mới: Parameter)
    /// </summary>
    public List<LogParamModel> Parameter { get; set; } = new();

    /// <summary>
    /// Alias cho Parameter để tương thích với các Handler cũ
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
    /// Đánh dấu ngoại lệ xảy ra
    /// </summary>
    public bool IsException { get; set; }

    /// <summary>
    /// Mã phản hồi API
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
