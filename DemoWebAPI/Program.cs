using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DemoWebAPI;
using DemoWebAPI.Filters;
using DemoWebAPI.Model;
using DemoWebAPI.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// 透過 builder.Services 將服務加入 DI 容器

// Add services to the container.
// builder.Services.AddLogging(options =>
// {
//     options.AddDebug();
// });
builder.Services.AddControllers(
        options => options.Filters.Add<VersionResourceFilter>()
        )
    .ConfigureApiBehaviorOptions(options =>
    {
        // 關閉驗證失敗時自動 HTTP 400 回應
        options.SuppressModelStateInvalidFilter = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//https://ithelp.ithome.com.tw/articles/10195190
//Swagger 產生器是負責取得 API 的規格並產生 SwaggerDocument 物件。
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo()
    {
        Title = "ToDo API",
        Description = "An ASP.NET Core Web API for managing ToDo items",
    });
    var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, fileName));
});

#region 寫法一: Autofac的DI 註冊
//https: //ithelp.ithome.com.tw/articles/10193172
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.AutofacRegister();
});

#endregion

#region 寫法二: 微軟的DI 註冊

// builder.Services.AddScoped<IMyService, MyService>();

#endregion

// 建立 WebApplication 物件
var app = builder.Build();

// Configure the HTTP request pipeline. 透過 app 設定 Middlewares (HTTP request pipeline)
if (app.Environment.IsDevelopment())
{
    //Swagger Middleware 負責路由，提供 SwaggerDocument 物件。
    // 可以從 URL 查看 Swagger 產生器產生的 SwaggerDocument 物件。
    app.UseSwagger();
    //SwaggerUI 是負責將 SwaggerDocument 物件變成漂亮的介面。
    app.UseSwaggerUI();
}
//https://ithelp.ithome.com.tw/articles/10192682
// app.Use(async (context, next) => 
// {
//     Console.WriteLine("First Middleware in.");
//     // await context.Response.WriteAsync("First Middleware in. \r\n");
//     await next.Invoke();
//     Console.WriteLine("First Middleware out");
//     // await context.Response.WriteAsync("First Middleware out. \r\n");
// });

app.UseHttpsRedirection();

app.UseAuthorization();

//Map 是能用來處理一些簡單路由的 Middleware，可依照不同的 URL 指向不同的 Run 及註冊不同的 Use。
app.MapControllers();

// https://localhost:7119/
app.Map("/", () => "test WebAPi");

app.Run();