using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Prototype.ClassifierPrototypeService.Application.Common;
using Prototype.ClassifierPrototypeService.Bll.Common;
using Prototype.ClassifierPrototypeService.Infrastructure.DBContext;

namespace Prototype.ClassifierPrototypeService.Infrastructure.common;


public class RequestContext : IRequestContext, System.IDisposable
{
    private readonly ILogger<RequestContext> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;
    private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

    private ApplicationDbContext _dbContext;
    public ApplicationDbContext ApplicationDbContext => _dbContext ??= _dbContextFactory.CreateDbContext();


    public RequestContext(ILogger<RequestContext> logger,
            IServiceScopeFactory serviceScopeFactory,
            IDbContextFactory<ApplicationDbContext> dbContextFactory) 
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
        _dbContextFactory = dbContextFactory;
    }

    private readonly SemaphoreSlim _transactionLocker = new(1, 1);
    private IDbContextTransaction _transaction;
    
    public async Task BeginTransactionAsync()
    {
        await _transactionLocker.WaitAsync();

        if (_transaction != null)
            throw new InfrastructureLayerException("Current started transaction has not been completed", Error.S101DoubleTransaction);

        _transaction = await ApplicationDbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await ApplicationDbContext.SaveChangesAsync(false);

        var changedEntities = ApplicationDbContext.ChangeTracker.Entries()
            .Where(entry => entry.Entity is Entity)
            .Select(entry => entry.Entity as Entity)
            .ToList();

        HashSet<IDomainEvent> domainEvents = new();
        foreach (Entity entity in changedEntities)
        {
            foreach (IDomainEvent domainEvent in entity.DomainEvents)
            {
                domainEvents.Add(domainEvent);
            }
        }

        await _transaction.CommitAsync();

        _ = DispatchDomainEventsInSeparateScopeAsync(domainEvents);

        ApplicationDbContext.ChangeTracker.AcceptAllChanges();

        await DisposeDbContextAsync();
    }

    public async Task CancelTransactionAsync()
    {
        if (_transaction is not null)
        {
            await _transaction.RollbackAsync();
        }

        await DisposeDbContextAsync();
    }

    private async Task DisposeDbContextAsync()
    {
        if (_dbContext is not null)
        {
            await _dbContext.DisposeAsync();
            _dbContext = null;
        }

        if (_transaction is not null)
        {
            await _transaction.DisposeAsync();
            _transaction = null;
            _transactionLocker.Release();
        }
    }

    public async Task DispatchDomainEventsInSeparateScopeAsync(HashSet<IDomainEvent> domainEvents)
    {
        if (domainEvents is {Count: 0})
            return;

        await Task.WhenAll(domainEvents.Select(async domainEvent =>
        {
            await using AsyncServiceScope scope = _serviceScopeFactory.CreateAsyncScope();
            
            IRequestContext scopedRequestContext = scope.ServiceProvider.GetRequiredService<IRequestContext>();

            DomainEventDispatcher scopedDomainEventDispatcher =
                scope.ServiceProvider.GetRequiredService<DomainEventDispatcher>();

            await scopedRequestContext.BeginTransactionAsync();

            await scopedDomainEventDispatcher.DispatchAsync(new List<IDomainEvent> {domainEvent})
                .ContinueWith(t =>
                {
                    if (!t.IsCompletedSuccessfully)
                    {
                        t.Exception.Flatten().Handle(e =>
                        {
                            _logger.LogError(e, "Domain event processing error");
                            return true;
                        });
                    }
                });

            await scopedRequestContext.CommitTransactionAsync();
        }));
    }

    public async void Dispose() 
        => await DisposeDbContextAsync();
}
