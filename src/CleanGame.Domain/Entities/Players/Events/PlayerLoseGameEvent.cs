using CleanGame.Domain.Base.Events;

namespace CleanGame.Domain.Entities.Players.Events;

public class PlayerLoseGameEvent : DomainEvent
{
    public Guid PlayerId { get; }
    public Guid GameId { get; }

    public PlayerLoseGameEvent(Guid playerId, Guid gameId)
    {
        PlayerId = playerId;
        GameId = gameId;
    }
    
}