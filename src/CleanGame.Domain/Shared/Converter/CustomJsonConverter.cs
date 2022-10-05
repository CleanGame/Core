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

        var methodInfo =
            typeof(T).BaseType?.GetMethod("GetFromJsonObject", BindingFlags.Static | BindingFlags.NonPublic) ??
            typeof(T).BaseType?.BaseType?.GetMethod("GetFromJsonObject", BindingFlags.Static | BindingFlags.NonPublic);
        var res = methodInfo?.Invoke(null, new object?[] { obj }) as T;
        return res;
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType());
    }
}