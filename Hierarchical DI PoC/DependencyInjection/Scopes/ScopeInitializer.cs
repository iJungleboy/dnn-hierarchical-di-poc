using Microsoft.Extensions.DependencyInjection;

namespace DotNetNuke.DependencyInjection.Scopes;
internal class ScopeInitializer<TScopeAccessor> : IScopeInitializer<TScopeAccessor> where TScopeAccessor : IServiceScopeAccessor
{
    public void Run(string currentName, IServiceProvider currentServiceProvider, IServiceProvider parentServiceProvider)
    {
        // Generate the new scope accessor, which will need to be initialized
        var newScopeAccessor = currentServiceProvider.GetRequiredService<TScopeAccessor>();

        // Check if the parent service provider has this service registered
        var parentScopeAccessor = parentServiceProvider.GetRequiredService<TScopeAccessor>();

        // If the parent is the root scope, then we need to use the current service provider
        // Otherwise we're already in a deeper scope, and we should use the one provided by the previous scope accessor
        var pageScopeServiceProvider = parentScopeAccessor.IsValid
            ? parentScopeAccessor.ServiceProvider
            : currentServiceProvider;

        // Initialize the new scope accessor with the service provider and current name
        newScopeAccessor.SetupServiceProvider(pageScopeServiceProvider, currentName);
    }
}
