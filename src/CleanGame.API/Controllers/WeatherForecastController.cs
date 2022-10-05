using Microsoft.AspNetCore.Mvc;
using CleanGame.Domain.Entities.Players;
using CleanGame.Domain.Shared.Interfaces;

namespace CleanGame.UI.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ICache _cache;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ICache cache)
    {
        _logger = logger;
        _cache = cache;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> Get()
    {
        var p = new Player("0912", "test", null);
        p.SetActive();
        await _cache.AddOrUpdateAsync("test", p, TimeSpan.FromHours(1));
        var a = await _cache.GetAsync<Player>("test");

        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}