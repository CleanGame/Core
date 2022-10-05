using CleanGame.Domain.Base.Events;

namespace CleanGame.Domain.Entities.Players.Events;

public class PlayerCreatedEvent : DomainEvent
{
    public Guid PlayerId { get; private set; }

    public PlayerCreatedEvent(Guid playerId)
    {
        PlayerId = playerId;
    }
}