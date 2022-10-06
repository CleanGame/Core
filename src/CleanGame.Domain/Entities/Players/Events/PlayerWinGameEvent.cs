using CleanGame.Domain.Base.Events;

namespace CleanGame.Domain.Entities.Players.Events;

public class PlayerWinGameEvent : IDomainEvent
{
    public Guid PlayerId { get; }
    public Guid GameId { get; }

    public PlayerWinGameEvent(Guid playerId, Guid gameId)
    {
        PlayerId = playerId;
        GameId = gameId;
    }
}