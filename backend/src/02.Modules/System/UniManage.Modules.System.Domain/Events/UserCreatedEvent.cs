using System;
using UniManage.Shared.Domain.Events;

namespace UniManage.Modules.System.Domain.Events
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
