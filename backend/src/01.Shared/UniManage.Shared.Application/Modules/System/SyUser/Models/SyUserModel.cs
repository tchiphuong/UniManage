namespace UniManage.Shared.Application.Modules.SyUser.Models
{
    public class SyUserModel
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public int Status { get; set; }
    }
}

