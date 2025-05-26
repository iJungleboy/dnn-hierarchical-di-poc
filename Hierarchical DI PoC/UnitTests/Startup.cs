using Microsoft.Extensions.DependencyInjection;

namespace DotNetNuke.UnitTests;
public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.SetupPageAndModuleScopes();
    }
}
