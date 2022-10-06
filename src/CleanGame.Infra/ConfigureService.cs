using CleanGame.Application.Shared.Interfaces;
using CleanGame.Domain.Shared.Interfaces;
using CleanGame.Infra.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Hangfire;
using Hangfire.MemoryStorage;
using Microsoft.AspNetCore.Builder;
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
        services.AddSingleton<IDateTime, DateTimeService>();

        AddHangfire(services, configuration);        
        
        return services;
    }
    
    private static void AddHangfire(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(setting => setting
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseMemoryStorage()
        );

        var queue = new List<string>();
        configuration.Bind("Hangfire:Queue", queue);
        services.AddHangfireServer(options => options.Queues = queue.ToArray());
    }
    
    public static IApplicationBuilder AddInfraApplication(this IApplicationBuilder app)
    {
        app.UseHangfireDashboard("/hangfire", new DashboardOptions()
        {
            //Authorization = new[] { new DashboardNoAuthorizationFilter() }
        });
        return app;
    }
}