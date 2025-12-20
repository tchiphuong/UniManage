using System.Text.Json.Serialization;

namespace UniManage.Model.Common
{
    public class BaseModel
    {
        /// <summary>
        /// Request header information for logging
        /// </summary>
        [JsonIgnore]
        public HeaderInfo HeaderInfo { get; set; } = new();
    }

    /// <summary>
    /// Base class for all commands providing common properties
    /// </summary>
    public abstract class BaseCommand : BaseModel
    {
    }

    /// <summary>
    /// Base class for all queries providing common properties
    /// </summary>
    public abstract class BaseQuery
    {
        /// <summary>
        /// Request header information for logging
        /// </summary>
        public HeaderInfo HeaderInfo { get; set; } = new();

        /// <summary>
        /// Optional keyword for searching
        /// </summary>
        public string? Keyword { get; set; }

        /// <summary>
        /// Page number, starting from 1
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Items per page
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// Sort field
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Sort direction (ASC/DESC)
        /// </summary>
        public string? SortDirection { get; set; }
    }
}
