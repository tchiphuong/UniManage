using System;
using System.Collections.Generic;
using UniManage.Shared.Domain.Primitives;
using UniManage.Shared.Domain.Events;

namespace UniManage.Shared.Domain
{
    public partial class SyUsers : IAggregateRoot
    {
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public static SyUsers Create(string username, string email, string passwordHash, string status, string createdBy)
        {
            var user = new SyUsers
            {
                Username = username,
                Email = email,
                Password = passwordHash,
                Status = status,
                CreatedBy = createdBy,
                CreatedAt = DateTime.Now
            };

            return user;
        }

        public void ChangePassword(string newPasswordHash, string updatedBy)
        {
            Password = newPasswordHash;
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.Now;
        }

        public void LockAccount(string updatedBy)
        {
            Status = "2"; // Locked
            UpdatedBy = updatedBy;
            UpdatedAt = DateTime.Now;
        }
    }
}
