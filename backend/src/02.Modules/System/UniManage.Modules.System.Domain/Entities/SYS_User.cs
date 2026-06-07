using System;
using UniManage.Shared.Domain.Entities;
using UniManage.Shared.Domain.Primitives;
using UniManage.Modules.System.Domain.Events;

namespace UniManage.Modules.System.Domain.Entities
{
    public class SYS_User : BaseEntity, IAggregateRoot
    {
        public string Username { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public string FullName { get; private set; } = string.Empty;
        public string Status { get; private set; } = string.Empty;
        public DateTime? LastLoginAt { get; private set; }

        // Required by EF Core
        protected SYS_User() { }

        private SYS_User(Guid id, string username, string email, string passwordHash, string fullName, string status)
        {
            Id = id;
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            FullName = fullName;
            Status = status;
        }

        public static SYS_User Create(string username, string email, string passwordHash, string fullName, string status)
        {
            var user = new SYS_User(Guid.NewGuid(), username, email, passwordHash, fullName, status);
            user.AddDomainEvent(new UserCreatedEvent(user.Id));
            return user;
        }

        public void ChangePassword(string newPasswordHash)
        {
            PasswordHash = newPasswordHash;
            UpdatedAt = DateTime.Now;
        }

        public void LockAccount()
        {
            Status = "2"; // Locked
            UpdatedAt = DateTime.Now;
        }

        public void UpdateProfile(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
            UpdatedAt = DateTime.Now;
        }

        public void RecordLogin()
        {
            LastLoginAt = DateTime.Now;
        }
    }
}
