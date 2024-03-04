using System;
using MediatR;

namespace Prototype.ClassifierPrototypeService.Bll.Common;

public interface IDomainEvent : INotification
{
    DateTime OccuredOn { get; }
}
