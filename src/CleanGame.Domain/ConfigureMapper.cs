using System.Reflection;
using CleanGame.Domain.Entities.Players.Enums;
using CleanGame.Domain.Shared.Extensions;

namespace CleanGame.Domain;

public class ConfigureMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //config.Default.EnumMappingStrategy(EnumMappingStrategy.ByName);
        /*config.NewConfig<JsonNode, PlayerStatusType>()
            .MapWith(node => node.ToEnum<PlayerStatusType>());*/

        SearchForEnums(config);
    }

    private void SearchForEnums(TypeAdapterConfig config)
    {
        var enums = GetType().Assembly.GetTypes()
            .Where(_ => _.IsEnum);

        var methodInfo = GetType().GetMethod("AddEnumMapping", BindingFlags.NonPublic | BindingFlags.Instance);

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