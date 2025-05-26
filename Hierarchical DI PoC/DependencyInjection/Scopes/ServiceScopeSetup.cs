namespace DotNetNuke.DependencyInjection.Scopes;
internal class ServiceScopeSetup(IServiceProvider currentServiceProvider)
{
    public void Run()
    {
        currentServiceProvider.GetRequiredService<IPageScopeAccessor>()
            .SetupServiceProvider(currentServiceProvider, ServiceScopeConstants.ScopePage);
    }
}
