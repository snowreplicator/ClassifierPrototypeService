using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Prototype.ClassifierPrototypeService.Bll.Common;

namespace Prototype.ClassifierPrototypeService.Application.Common;

public interface IRequestContext
{
    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task CancelTransactionAsync();

    Task DispatchDomainEventsInSeparateScopeAsync(HashSet<IDomainEvent> domainEvents);
}
