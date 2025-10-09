namespace UniManage.Core.Models;

/// <summary>
/// Base class for all commands providing common properties
/// </summary>
public abstract class BaseCommand
{
    /// <summary>
    /// Request header information for logging
    /// </summary>
    public HeaderInfo HeaderInfo { get; set; } = new();
}
