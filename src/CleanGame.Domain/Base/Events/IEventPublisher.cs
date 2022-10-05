namespace CleanGame.Domain.Base.Events;

public interface IEventPublisher
{
    Task PublishAsync(DomainEvent @event);
}