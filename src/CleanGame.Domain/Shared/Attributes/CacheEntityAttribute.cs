namespace CleanGame.Domain.Shared.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class CacheEntityAttribute : Attribute
{
    public string Prefix { get; }

    public CacheEntityAttribute(string prefix)
    {
        Prefix = prefix;
    }

}