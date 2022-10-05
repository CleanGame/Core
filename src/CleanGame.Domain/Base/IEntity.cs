using CleanGame.Domain.Base.Events;
using CleanGame.Domain.Base.Events;

namespace CleanGame.Domain.Base;

public interface IEntity
{
    Guid Id { get; }
    IReadOnlyCollection<DomainEvent> DomainEvents { get; }
    void ClearDomainEvents();
}