using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CleanGame.Domain.Shared.Interfaces;
using CleanGame.Infra.Shared;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.System.Text.Json;

namespace CleanGame.Infra;

public static class ConfigureServices
{
    public static IServiceCollection AddInfraServices(this IServiceCollection services, IConfiguration configuration)
    {
        var redisConfiguration = configuration.GetSection("Redis").Get<RedisConfiguration>();

        services.AddStackExchangeRedisExtensions<SystemTextJsonSerializer>(redisConfiguration);

        services.AddSingleton<ICache, CacheService>();

        return services;
    }
}