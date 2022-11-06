using DemoWebAPI.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace DemoWebAPI.Filters;

public class OrderVaildationFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        base.OnActionExecuting(context);
        var contextActionArguments = context.ActionArguments;
        var order = contextActionArguments.FirstOrDefault(s => s.Key == "order");
        if (order.Value != null && (order.Value as Order).Name == "phone")
        {
            context.ModelState.AddModelError("error", "order name was wrong .");
            context.Result = new BadRequestObjectResult(context.ModelState);
        }
    }
}