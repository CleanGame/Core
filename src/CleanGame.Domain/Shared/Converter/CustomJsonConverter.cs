using System.Reflection;
using System.Text.Json;
using CleanGame.Domain.Base;

namespace CleanGame.Domain.Shared.Converter;

public class CustomJsonConverter<T> : JsonConverter<T>
    where T : Entity<T>
{
    public override T? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var obj = JsonSerializer.Deserialize<JsonObject>(ref reader);

        var entity = typeof(T).BaseType;
        while (entity is not null)
        {
            if (entity == typeof(Entity<T>))
            {
                break;
            }

            entity = entity.BaseType;
        }

        if (entity is null)
        {
            return default;
        }
        
        var methodInfo = entity?.GetMethod(nameof(Entity<T>.GetFromJsonObject),
                             BindingFlags.Static | BindingFlags.NonPublic);
        var res = methodInfo?.Invoke(null, new object?[] { obj }) as T;
        return res;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType());
    }
}