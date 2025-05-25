using Microsoft.Extensions.DependencyInjection;
using ToSic.HierarchicalDI.Page;

namespace ToSic.HierarchicalDI.CustomServiceProvider;
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddTransient<MyTransient>();
        //services
        //    .AddTransient<ITestSwitchableService, TestSwitchableFallback>()
        //    .AddTransient<ITestSwitchableService, TestSwitchableKeep>()
        //    .AddTransient<ITestSwitchableService, TestSwitchableSkip>()
        //    .AddLibCore();
    }
}
