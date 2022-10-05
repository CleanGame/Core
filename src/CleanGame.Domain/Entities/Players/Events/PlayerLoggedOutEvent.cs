using CleanGame.Domain.Base.Events;

namespace CleanGame.Domain.Entities.Players.Events;

public class PlayerLoggedOutEvent : DomainEvent
{
    public Guid PlayerId { get; }

    public PlayerLoggedOutEvent(Guid playerId)
    {
        PlayerId = playerId;
    }

}