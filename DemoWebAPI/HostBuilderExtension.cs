using Autofac;
using DemoWebAPI.Model;
using DemoWebAPI.Services;

namespace DemoWebAPI;

public static class HostBuilderExtension
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="builder"></param>
    public static void AutofacRegister(this ContainerBuilder builder)
    {
        builder.RegisterType<MyService>().As<IMyService>().InstancePerLifetimeScope();
    }
}