namespace CleanGame.API;

public static class ConfigureServices
{
    public static IServiceCollection AddUIServices(this IServiceCollection services)
    {
        services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static WebApplication AddUIApplication(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }
}