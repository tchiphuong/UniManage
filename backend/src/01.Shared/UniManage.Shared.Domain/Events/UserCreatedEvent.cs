using System;
using UniManage.Shared.Domain.Events;

namespace UniManage.Shared.Domain.Events
{
    public class UserCreatedEvent : IDomainEvent
    {
        public Guid UserId { get; }

        public UserCreatedEvent(Guid userId)
        {
            UserId = userId;
        }
    }
}
