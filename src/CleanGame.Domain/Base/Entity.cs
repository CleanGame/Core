using CleanGame.Domain.Base.Events;

namespace CleanGame.Domain.Base;

public abstract class Entity<T> : IEntity
    where T : Entity<T>
{
    public Guid Id { get; protected set; }

    #region Event collection

    private readonly List<IDomainEvent> _domainEvents = new();

    [NotMapped] 
    [JsonIgnore] 
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    public void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }

    public void RemoveDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Remove(domainEvent);
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    #endregion

    #region Operators

    public override bool Equals(object? obj)
    {
        var compareTo = obj as Entity<T>;

        if (ReferenceEquals(this, compareTo))
            return true;

        if (ReferenceEquals(null, compareTo))
            return false;

        return Id.Equals(compareTo.Id);
    }

    public static bool operator ==(Entity<T> a, Entity<T> b)
    {
        if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            return true;

        if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            return false;

        return a.Equals(b);
    }

    public static bool operator !=(Entity<T> a, Entity<T> b) => !(a == b);

    public override string ToString()
    {
        return $"{GetType().Name} [Id={Id}]";
    }

    public override int GetHashCode()
    {
        return (GetType().GetHashCode() * 907) + Id.GetHashCode();
    }

    #endregion

    internal static T GetFromJsonObject(JsonObject jsonObject)
    {
        return jsonObject.Adapt<T>();
    }
}