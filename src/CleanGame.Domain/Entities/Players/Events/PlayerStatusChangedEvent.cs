using CleanGame.Domain.Base.Events;
using CleanGame.Domain.Entities.Players.Enums;

namespace CleanGame.Domain.Entities.Players.Events;

public class PlayerStatusChangedEvent : IDomainEvent
{
    public Guid PlayerId { get; private set; }
    public PlayerStatusType Status { get; private set; }

    public PlayerStatusChangedEvent(Guid playerId, PlayerStatusType status)
    {
        PlayerId = playerId;
        Status = status;
    }
    
}