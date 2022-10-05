namespace CleanGame.Domain.Shared.Extensions;

internal static class EnumExt
{
    public static T ToEnum<T>(this JsonNode node)
        where T : struct, IConvertible
    {
        return typeof(T).IsEnum
            ? (T)Enum.Parse(typeof(T), node.ToString())
            : default;
    }
    
    public static object? ToEnum(JsonNode node, Type enumType)
    {
        return enumType.IsEnum 
            ? Enum.Parse(enumType, node.ToString())
            : default;
    }
}