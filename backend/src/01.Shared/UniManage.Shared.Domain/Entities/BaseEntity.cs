using System;
using System.Collections.Generic;
using UniManage.Shared.Domain.Events;


namespace UniManage.Shared.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        
        public Guid? CreatedByUserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public Guid? UpdatedByUserId { get; set; }
        public DateTime? UpdatedAt { get; set; }
        
        public Guid? DeletedByUserId { get; set; }
        public DateTime? DeletedAt { get; set; }
        
        public bool IsDeleted { get; set; }
        private readonly List<IDomainEvent> _domainEvents = new();
        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        public byte[] DataRowVersion { get; set; } = Array.Empty<byte>();
    }
}

