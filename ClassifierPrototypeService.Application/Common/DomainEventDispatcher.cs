using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Prototype.ClassifierPrototypeService.Bll.Common;

namespace Prototype.ClassifierPrototypeService.Application.Common;

public class DomainEventDispatcher
{
    private readonly IMediator _mediator;

    public DomainEventDispatcher(IMediator mediator)
    {
        _mediator = mediator;

    }

    public async Task DispatchAsync(IEnumerable<IDomainEvent> domainEvents)
    {
        IEnumerable<Task> publishTasks = domainEvents.Select(e => _mediator.Publish(e));
        await Task.WhenAll(publishTasks);
    }
}
