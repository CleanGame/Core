using System.Reflection;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;
using CleanGame.Domain.Shared.Converter;
using StackExchange.Redis.Extensions.System.Text.Json;

namespace CleanGame.Domain;

public static class ConfigureServices
{
    public static IServiceCollection AddDomainServices(this IServiceCollection services)
    {
        AddMapper(services);
        
        services.AddTransient<ICacheSerializationContext, CustomSerializationContext>();
        
        return services;
    }

    private static void AddMapper(IServiceCollection services)
    {
        //TypeAdapterConfig.GlobalSettings.Default.MapToConstructor(true);
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());
        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
    }

}