using MediatR;
using CleanGame.Domain.Base.Events;

namespace CleanGame.Domain.Base.Events;

public interface IDomainEventHandler<in TDomainEvent> : INotificationHandler<TDomainEvent>
    where TDomainEvent : DomainEvent
{
}