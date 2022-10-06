using CleanGame.Domain.Base.Events;

namespace CleanGame.Domain.Entities.Players.Events;

public class PlayerLoggedInEvent : IDomainEvent
{
    public Guid UserId { get; }

    public PlayerLoggedInEvent(Guid userId)
    {
        UserId = userId;
    }

}