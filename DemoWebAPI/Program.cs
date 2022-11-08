using System.Reflection;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using DemoWebAPI;
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
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        // 關閉驗證失敗時自動 HTTP 400 回應
        options.SuppressModelStateInvalidFilter = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
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
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// https://localhost:7119/
app.Map("/", () => "test WebAPi");

app.Run();