namespace DotNetNuke.UnitTests;

/// <summary>
/// xUnit startup class for configuring services in the test project.
/// </summary>
public class Startup
{
#pragma warning disable CA1822
    public void ConfigureServices(IServiceCollection services)
#pragma warning restore CA1822
    {
        services.SetupPageAndModuleScopes();
    }
}
