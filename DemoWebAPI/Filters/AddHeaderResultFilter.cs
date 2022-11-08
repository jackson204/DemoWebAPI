using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;

namespace DemoWebAPI.Filters;

/// <summary>
/// 
/// </summary>
public class AddHeaderResultFilter : ResultFilterAttribute
{
    private readonly string _name;
    private readonly string _value;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="name"></param>
    /// <param name="value"></param>
    public AddHeaderResultFilter(string name, string value)
    {
        _name = name;
        _value = value;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public override void OnResultExecuting(ResultExecutingContext context)
    {
        base.OnResultExecuting(context);
        context.HttpContext.Response.Headers.Add(_name, new[] {_value});
    }
}