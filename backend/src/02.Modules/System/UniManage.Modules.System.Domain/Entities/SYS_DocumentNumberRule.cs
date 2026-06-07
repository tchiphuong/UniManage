using UniManage.Shared.Domain.Entities;

namespace UniManage.Modules.System.Domain.Entities
{
    public class SYS_DocumentNumberRule : BaseEntity
    {
        public string EntityName { get; set; } = string.Empty;
        public string Prefix { get; set; } = string.Empty;
        
        /// <summary>
        /// Định dạng chuỗi số, ví dụ: 0000 (tức là 4 chữ số)
        /// </summary>
        public string Format { get; set; } = "0000";
        
        public int CurrentNumber { get; set; } = 0;
        
        /// <summary>
        /// Reset theo năm (true = mỗi năm bắt đầu lại từ 1)
        /// </summary>
        public bool ResetYearly { get; set; } = true;
    }
}

