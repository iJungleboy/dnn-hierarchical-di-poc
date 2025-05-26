using Microsoft.Extensions.DependencyInjection;
using ToSic.HierarchicalDI.DependencyInjection;

namespace ToSic.HierarchicalDI.UnitTests;
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.SetupPageAndModuleScopes();
    }
}
