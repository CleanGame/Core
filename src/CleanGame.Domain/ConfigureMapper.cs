using System.Reflection;

namespace CleanGame.Domain;

public class ConfigureMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        SearchForEnums(config);
    }

    private void SearchForEnums(TypeAdapterConfig config)
    {
        var enums = GetType().Assembly.GetTypes()
            .Where(_ => _.IsEnum);

        var methodInfo = GetType().GetMethod(nameof(AddEnumMapping), BindingFlags.NonPublic | BindingFlags.Instance);

        foreach (var item in enums)
        {
            methodInfo!
                .MakeGenericMethod(item)
                .Invoke(this, new object[] { config });
        }
    }

    private void AddEnumMapping<T>(TypeAdapterConfig config)
        where T : struct, IConvertible
    {
        config.NewConfig<JsonNode, T>()
            .MapWith(node => node.ToEnum<T>());
    }
}