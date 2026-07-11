using System.Collections.Generic;
using System.Text.Json.Serialization;
using MediatR;

namespace UniManage.Shared.Application.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortDirection
    {
        Asc,
        Desc
    }

    public class BaseModel
    {
        /// <summary>
        /// Request header information for logging
        /// </summary>
        [JsonIgnore]
        public HeaderInfo HeaderInfo { get; set; } = new();
    }

    /// <summary>
    /// Marker interface for commands that require automatic transaction management via TransactionBehavior
    /// </summary>
    public interface ITransactionalCommand { }

    /// <summary>
    /// Marker interface for commands/queries that require automatic logging via LoggingBehavior
    /// </summary>
    public interface ILoggableCommand { }

    /// <summary>
    /// Base class for all commands providing common properties and automatic Behaviors
    /// </summary>
    public abstract class BaseCommand : BaseModel, ITransactionalCommand, ILoggableCommand
    {
    }

    /// <summary>
    /// Base class for single-item or condition-based queries (get by id, get by code, check exists, combobox...)
    /// Only provides HeaderInfo for logging context
    /// </summary>
    public abstract class BaseQuery
    {
        /// <summary>
        /// Request header information for logging
        /// </summary>
        public HeaderInfo HeaderInfo { get; set; } = new();
    }

    /// <summary>
    /// Base class for list/paged queries providing pagination, search, and sort properties
    /// </summary>
    public abstract class BaseListQuery : BaseQuery
    {
        /// <summary>
        /// Optional keyword for searching
        /// </summary>
        public string? Keyword { get; set; }

        /// <summary>
        /// Fields to search in when using Keyword (comma-separated, e.g., "Username,Email,DisplayName")
        /// If null/empty, query handler will use default search fields
        /// </summary>
        public string? SearchFields { get; set; }

        /// <summary>
        /// Page number, starting from 1
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// Items per page
        /// </summary>
        public int PageSize { get; set; } = 20;

        /// <summary>
        /// Calculated offset for pagination (skip count)
        /// </summary>
        public int Offset => (PageIndex - 1) * PageSize;

        /// <summary>
        /// Sort field
        /// </summary>
        public string? SortBy { get; set; }

        /// <summary>
        /// Sort direction (Asc/Desc)
        /// </summary>
        public SortDirection? SortDirection { get; set; }
    }

    /// <summary>
    /// Base class for export queries providing search and sort properties without pagination
    /// </summary>
    public abstract class BaseExportQuery : BaseQuery
    {
        public string? Keyword { get; set; }
        public string? SearchFields { get; set; }
        public string? SortBy { get; set; }
        public SortDirection? SortDirection { get; set; }
    }

    /// <summary>
    /// Base class for import commands providing generic items list and overwrite flag
    /// </summary>
    public class BaseImportCommand<T> : BaseCommand, IRequest<ApiResponse<bool>>
    {
        public List<T> Items { get; init; } = new();
        public bool IsOverwrite { get; init; }
    }
}

