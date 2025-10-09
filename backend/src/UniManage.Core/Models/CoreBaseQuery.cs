using UniManage.Core.Models; // Đảm bảo reference đúng namespace nếu tách file

namespace UniManage.Core.Models
{
    /// <summary>
    /// Base query cho mọi API, luôn có thông tin Caller.
    /// </summary>
    public class CoreBaseQuery
    {
        /// <summary>
        /// Thông tin header lấy từ header (user/service/role/...).
        /// </summary>
        public HeaderInfo HeaderInfo { get; set; } = new HeaderInfo();

        // Phân trang cơ bản
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 20;
        public int Offset => (PageIndex - 1) * PageSize;

        // Sắp xếp
        public string? SortBy { get; set; }
        public string? SortDirection { get; set; } // "asc" hoặc "desc"
    }
}
