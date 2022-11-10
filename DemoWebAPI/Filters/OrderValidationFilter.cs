using DemoWebAPI.Model;
using DemoWebAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoWebAPI.Filters;

/// <summary>
/// 
/// </summary>
public class OrderValidationFilter : ActionFilterAttribute
{
    ///https://learn.microsoft.com/zh-tw/dotnet/api/microsoft.aspnetcore.mvc.filters.actionfilterattribute?view=aspnetcore-7.0
    /// <summary>
    /// 在動作執行之前呼叫，在模型系結完成之後呼叫。
    /// </summary>
    /// <param name="context"></param>
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        var contextActionArguments = context.ActionArguments;
        //找尋[OrderVaildationFilter]的Action Argument
        KeyValuePair<string, object?> order = contextActionArguments.FirstOrDefault(s => s.Key == "order2");
        if (order.Value != null && (order.Value as Order)?.Name == "phone")
        {
            context.ModelState.AddModelError("errorD", "order name was wrong .");
            context.ModelState.AddModelError("errorC", "order name was wrong .");
            context.ModelState.AddModelError("errorB", "order name was wrong .");
            context.ModelState.AddModelError("errorA", "order name was wrong .");
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}