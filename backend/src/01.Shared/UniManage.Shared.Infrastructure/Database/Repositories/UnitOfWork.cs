using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UniManage.Shared.Domain.Interfaces;
using UniManage.Shared.Domain.Entities;
using DbContext = UniManage.Shared.Infrastructure.Database.DbContext;

namespace UniManage.Shared.Infrastructure.Database.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly DbContext _dbContext;
        private readonly IMediator _mediator;
        private bool _disposed;

        public UnitOfWork(DbContext dbContext, IMediator mediator)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            await DispatchDomainEventsAsync();
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }

        private async Task DispatchDomainEventsAsync()
        {
            var domainEntities = _dbContext.ChangeTracker
                .Entries<BaseEntity>()
                .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any())
                .ToList();

            var domainEvents = domainEntities
                .SelectMany(x => x.Entity.DomainEvents)
                .ToList();

            domainEntities.ForEach(entity => entity.Entity.ClearDomainEvents());

            foreach (var domainEvent in domainEvents)
            {
                await _mediator.Publish(domainEvent);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
                _disposed = true;
            }
        }
    }
}

