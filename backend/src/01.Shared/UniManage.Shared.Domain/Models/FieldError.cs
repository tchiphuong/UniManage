namespace UniManage.Shared.Domain.Models;

/// <summary>
/// Represents validation error for a specific field
/// </summary>
public record FieldError(string Field, IList<string> Messages)
{
    /// <summary>
    /// Creates a field error with single message
    /// </summary>
    /// <param name="field">Field name</param>
    /// <param name="message">Error message</param>
    public FieldError(string field, string message) : this(field, new List<string> { message })
    {
    }
}

