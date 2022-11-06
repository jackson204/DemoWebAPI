using DemoWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")] //設定回傳的Media type的格式
public class WeatherForecastController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly IMyService _myService;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, IMyService myService)
    {
        _logger = logger;
        _myService = myService;
    }

    /// <summary>獲取天氣預報</summary>
    /// <remarks>補充說明</remarks>
    /// <response code = "400">無資料</response>
    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        _myService.SendMessage("Hi DI");
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
    }
}