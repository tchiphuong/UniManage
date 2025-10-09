using UniManage.Core.Models; // Đảm bảo reference đúng namespace nếu tách file

namespace UniManage.Core.Models
{
    public class CoreBaseCommand
    {
        /// <summary>
        /// Thông tin caller lấy từ header (user/service/role/...).
        /// </summary>
        public HeaderInfo HeaderInfo { get; set; } = new HeaderInfo();
    }
}
