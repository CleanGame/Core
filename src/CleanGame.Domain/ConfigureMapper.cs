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

        var methodInfo = config.GetType().GetMethods()
            .First(_ => _.Name == "NewConfig" && _.IsGenericMethod);
        var jsonNodeType = typeof(JsonNode);
        
        foreach (var item in enums)
        {
            dynamic callMethod= methodInfo
                .MakeGenericMethod(jsonNodeType, item)
                .Invoke(config,null)!;

            //callMethod.MapWith(node => EnumExt.ToEnum(node,item))

            /*config.ForType(typeof(JsonNode), item).Config.
                
                Map("", () => EnumExt.ToEnum(source, item));*/

            /*config.NewConfig(typeof(JsonNode), item).Config
                .MapWith(Enum<PlayerStatusType>());*/
        }
    }
}