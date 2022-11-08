using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoWebAPI.Filters;

public sealed class VersionResourceFilter : Attribute, IResourceFilter
{
    //https://learn.microsoft.com/zh-tw/aspnet/core/mvc/controllers/filters?view=aspnetcore-6.0
    // 模型系結之前執行程式碼
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        if (!context.HttpContext.Request.Path.Value.ToLower().Contains("v2"))
        {
            context.Result = new BadRequestObjectResult(new
            {
                Version = new[] {"This version of the API has expired. "}
            });
        }
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
    }
}