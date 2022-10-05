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
        var cacheEntities = GetType().Assembly.GetTypes()
            .Where(_ => _.GetCustomAttribute<CacheEntityAttribute>() is not null);

        var methodInfo = GetType().GetMethod("AddEntityConvertor", BindingFlags.NonPublic | BindingFlags.Instance);

        var serializers = new Dictionary<Type, JsonSerializerContext>();
        foreach (var item in cacheEntities)
        {
            dynamic converter = methodInfo!
                .MakeGenericMethod(item)
                .Invoke(this, null)!;

            serializers.Add(converter.Key, converter.Value);
        }

        return serializers;
    }

    private KeyValuePair<Type, JsonSerializerContext> AddEntityConvertor<TEntity>()
        where TEntity : Entity<TEntity>
    {
        var config = new JsonSerializerOptions()
        {
            Converters =
            {
                GetConverter<TEntity>()
            }
        };

        return new KeyValuePair<Type, JsonSerializerContext>(typeof(TEntity), new CustomJsonSerializer(config));
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