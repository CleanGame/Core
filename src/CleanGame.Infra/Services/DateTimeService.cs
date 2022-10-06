using CleanGame.Domain.Shared.Interfaces;

namespace CleanGame.Infra.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.UtcNow;
}