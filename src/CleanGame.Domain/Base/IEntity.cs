using CleanGame.Domain.Base.Events;

namespace CleanGame.Domain.Base;

public interface IEntity
{
    Guid Id { get; }
    IReadOnlyCollection<IDomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}