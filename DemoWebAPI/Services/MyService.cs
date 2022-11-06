namespace DemoWebAPI.Services;

public class MyService : IMyService
{
    private ILogger<MyService> _logger;

    public MyService(ILogger<MyService> logger)
    {
        _logger = logger;
    }

    public void SendMessage(string message)
    {
        _logger.LogInformation("call Log");
        Console.WriteLine($"call Send {message}");
    }
}