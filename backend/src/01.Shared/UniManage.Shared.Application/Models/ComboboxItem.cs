namespace UniManage.Shared.Application.Models
{
    /// <summary>
    /// Common model for combobox items with Code, Name, Extended Data, and Color.
    /// </summary>
    public class ComboboxItem
    {
        /// <summary>
        /// Unique value or identifier (e.g., ID, Code).
        /// </summary>
        public string Code { get; set; } = string.Empty;

        /// <summary>
        /// Display text.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Additional metadata or extended information.
        /// </summary>
        public object? ExtData { get; set; }

        /// <summary>
        /// Background color (hex, rgba, etc.).
        /// </summary>
        public string? BgColor { get; set; }

        /// <summary>
        /// Text color (hex, rgba, etc.).
        /// </summary>
        public string? TextColor { get; set; }

        /// <summary>
        /// Indicates if the item is selected (useful for Edit/Multi-select cases).
        /// </summary>
        public bool? IsSelected { get; set; }

        /// <summary>
        /// Item status (e.g., 0=Inactive, 1=Active).
        /// </summary>
        public byte? Status { get; set; }
    }

    /// <summary>
    /// Generic variation of the common combobox model.
    /// </summary>
    /// <typeparam name="TCode">Data type for the Code property.</typeparam>
    public class ComboboxItem<TCode>
    {
        /// <summary>
        /// Typed unique value or identifier.
        /// </summary>
        public TCode Code { get; set; } = default!;

        /// <summary>
        /// Display text.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Additional metadata or extended information.
        /// </summary>
        public object? ExtData { get; set; }

        /// <summary>
        /// Background color.
        /// </summary>
        public string? BgColor { get; set; }

        /// <summary>
        /// Text color.
        /// </summary>
        public string? TextColor { get; set; }

        /// <summary>
        /// Indicates if the item is selected.
        /// </summary>
        public bool? IsSelected { get; set; }

        /// <summary>
        /// Item status.
        /// </summary>
        public byte? Status { get; set; }
    }
}


