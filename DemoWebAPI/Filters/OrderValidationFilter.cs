using DemoWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoWebAPI.Filters;

/// <summary>
/// 
/// </summary>
public class OrderValidationFilter : ActionFilterAttribute
{
    /// <summary>
    /// 
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
            context.ModelState.AddModelError("error", "order name was wrong .");
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}