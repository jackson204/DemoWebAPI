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
    [HttpGet, Route("GetWeatherForecast")] //WeatherForecast/GetWeatherForecast
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

    //寫法一 :[HttpPost, Route("[action]/{id}")] //WeatherForecast/Create/1
    [HttpPost, Route("/insert/{id}")] //insert/1
    public string Create(int id)
    {
        return $"create id = {id}";
    }

    //使用action 關鍵字會印出方法名稱
    [HttpGet, Route("[action]")] //WeatherForecast/GetAll
    public string GetAll()
    {
        return $"Get All";
    }
}