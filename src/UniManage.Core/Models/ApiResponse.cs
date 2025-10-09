namespace UniManage.Core.Models;

/// <summary>
/// Standard API response
/// </summary>
public class ApiResponse<T>
{
    /// <summary>
    /// Return code: 0 = Success, others = Error
    /// </summary>
    public int ReturnCode { get; set; }

    /// <summary>
    /// Error messages if any
    /// </summary>
    public List<string> Errors { get; set; } = new();

    /// <summary>
    /// Response message
    /// </summary>
    public string Message { get; set; } = string.Empty;

    /// <summary>
    /// Response data
    /// </summary>
    public T? Data { get; set; }

    public ApiResponse() { }

    public ApiResponse(T? data = default, string message = "")
    {
        ReturnCode = 0;
        Message = message;
        Data = data;
    }

    public static ApiResponse<T> Success(T? data = default, string message = "")
    {
        return new ApiResponse<T>
        {
            ReturnCode = 0,
            Message = message,
            Data = data
        };
    }

    public static ApiResponse<T> Fail(IEnumerable<string> errors, string message = "")
    {
        return new ApiResponse<T>
        {
            ReturnCode = -1,
            Message = message,
            Errors = errors.ToList()
        };
    }

    public static ApiResponse<T> Fail(string error, string message = "")
    {
        return new ApiResponse<T>
        {
            ReturnCode = -1,
            Message = message,
            Errors = new List<string> { error }
        };
    }
}
