using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace CleanGame.Domain.Shared.Converter;

public class CustomJsonSerializer : JsonSerializerContext
{
    public CustomJsonSerializer(JsonSerializerOptions? options) : base(options)
    {
    }

    public override JsonTypeInfo? GetTypeInfo(Type type)
    {
        return default;
    }

    protected override JsonSerializerOptions? GeneratedSerializerOptions => Options;
}