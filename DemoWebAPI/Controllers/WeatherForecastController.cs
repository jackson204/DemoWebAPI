using DemoWebAPI.Filters;
using DemoWebAPI.Model;
using DemoWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers;

[ApiController]
[Route("[controller]")]        //https://learn.microsoft.com/zh-tw/aspnet/core/web-api/?view=aspnetcore-6.0#attribute-routing-requirement
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

    // [FromBody] - 從 Request body 取值  當套用該屬性時,如果物件還有其他的屬性會忽略其他屬性，一律使用 body ex:CreateOrder()
    // [FromForm] - 從 Request body 中的表單資料取值
    // [FromHeader] - 從 Request header 取值
    // [FromQuery] - 從 Query String 取值
    // [FromRoute] - 從目前要求的路由變數取值
    // [FromServices] - 從 DI 容器中取得服務物件

    // [HttpGet, Route("/QueryOrderProduct/{oid}/product")]//QueryOrderProduct/1/product?pid=22
    // public string GetOrderProduct(int oid, [FromQuery] int pid)
    // {
    //     if (pid == default)
    //     {
    //         return $"OrderId = {oid}";
    //     }
    //     return $"OrderId = {oid} Product = {pid}";
    // }

    [HttpGet, Route("/QueryOrderProduct/{oid}/product")] //QueryOrderProduct/1/product?pid=22
    public string GetOrderProduct([FromQuery] Order order)
    {
        if (order == null)
        {
            return "parameter are null";
        }
        if (order.ProductId == default)
        {
            return $"OrderId = {order.Id} , Name = {order.Name} , Price = {order.Price}";
        }
        return $"OrderId = {order.Id} , Name = {order.Name} , Price = {order.Price} ,ProductId = {order.ProductId}";
    }

    //ActionFilter
    // [HttpPost, Route("/CreateOrder")]
    // [OrderValidationFilter]
    // public string CreateOrder([FromBody] Order order2)
    // {
    //     if (order2 == null)
    //     {
    //         return "parameter are null";
    //     }
    //
    //     return $" Create Success . OrderId = {order2.Id}";
    // }

    [HttpPost, Route("/CreateOrder")]
    public string CreateOrder([FromBody] Order order2)
    {
        if (order2 == null)
        {
            return "parameter are null";
        }
    
        return $" Create Success . OrderId = {order2.Id}";
    }
    
    [HttpPost, Route("/CreateOrder/V2")]
    public string CreateOrderV2([FromBody] Order order2)
    {
        if (order2 == null)
        {
            return "parameter are null";
        }

        return $" Create Success . OrderId = {order2.Id}";
    }

    [HttpPut, Route("/UpdateOrder")]
    public string UpdateOrder([FromBody] Order order)
    {
        if (order == null)
        {
            return "parameter are null";
        }

        return $" Update Success . OrderId = {order.Id}";
    }
}