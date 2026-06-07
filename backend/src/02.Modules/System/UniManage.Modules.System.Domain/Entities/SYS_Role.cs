using UniManage.Shared.Domain.Entities;

namespace UniManage.Modules.System.Domain.Entities
{
    public class SYS_Role : BaseEntity
    {
        public string Code { get; set; } = string.Empty;
        
        /// <summary>
        /// Tên đa ngôn ngữ dạng JSON
        /// </summary>
        public string NameLocalized { get; set; } = "{}";
        
        public string Description { get; set; } = string.Empty;
    }
}

