namespace CleanGame.Domain.Base;

public interface IAuditableEntity : IEntity
{
    DateTime Created { get; set; }
    DateTime? Updated { get; set; }
}