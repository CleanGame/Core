namespace CleanGame.Domain.Base;

public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
    where T : AuditableEntity<T>
{
    public DateTime Created { get; set; }
    public long CreatorId { get; set; }
    public DateTime? Updated { get; set; }
    public long? UpdaterId { get; set; }

    protected AuditableEntity()
    {
    }
}