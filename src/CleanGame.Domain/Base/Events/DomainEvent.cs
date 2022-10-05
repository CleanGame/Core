using MediatR;

namespace CleanGame.Domain.Base.Events;

public class DomainEvent : INotification
{
    public DateTime EventTime { get; private set; }

    protected DomainEvent()
    {
        EventTime = DateTime.UtcNow;
    }
}
