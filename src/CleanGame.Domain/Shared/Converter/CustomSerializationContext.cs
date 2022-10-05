using System.Reflection;
using System.Text.Json;
using CleanGame.Domain.Base;
using CleanGame.Domain.Entities.Players;
using StackExchange.Redis.Extensions.System.Text.Json;

namespace CleanGame.Domain.Shared.Converter;

public class CustomSerializationContext : ICacheSerializationContext
{
    public Dictionary<Type, JsonSerializerContext> GetContexts()
    {
        var config = new JsonSerializerOptions()
        {
            Converters =
            {
                GetConverter<Player>()
            }
        };

        return new Dictionary<Type, JsonSerializerContext>()
        {
            { typeof(Player), new CustomJsonSerializer(config) }
        };
    }

    private static JsonConverter<T> GetConverter<T>()
        where T : Entity<T>
    {
        var constructorInfo = typeof(T)?.GetConstructors(BindingFlags.NonPublic | BindingFlags.Instance)
            .OrderByDescending(_ => _.GetParameters().Length)
            .First();

        if (constructorInfo is null)
        {
            return new CustomJsonConverter<T>();
        }
        
        TypeAdapterConfig<JsonObject, T>.NewConfig().MapToConstructor(constructorInfo);
        TypeAdapterConfig<JsonNode, T>.NewConfig().MapToConstructor(constructorInfo);

        return new CustomJsonConverter<T>();
    }
}