using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DemoWebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class ErrorController : ControllerBase
{
    private readonly IHostEnvironment _hostEnvironment;
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(IHostEnvironment hostEnvironment, ILogger<ErrorController> logger)
    {
        _hostEnvironment = hostEnvironment;
        _logger = logger;
    }

    [Route("/v2/error") , ApiExplorerSettings(IgnoreApi = true)]
    public IActionResult Get()
    {
        var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
        if (_hostEnvironment.EnvironmentName != "Development")
        {
            _logger.LogError($"{context.Error.Message} , {context.Error.StackTrace}");
            return Problem();
        }
        return Problem(title: context.Error.Message, detail: context.Error.StackTrace);
    }
}