using CleanGame.Domain.Base.Events;

namespace CleanGame.Domain.Entities.Players.Events;

public class PlayerLoggedOutEvent : IDomainEvent
{
    public Guid PlayerId { get; }

    public PlayerLoggedOutEvent(Guid playerId)
    {
        PlayerId = playerId;
    }

}