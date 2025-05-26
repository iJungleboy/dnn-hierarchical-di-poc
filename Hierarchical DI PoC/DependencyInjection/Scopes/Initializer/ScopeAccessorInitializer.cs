using DotNetNuke.DependencyInjection.Scopes.Accessors;

namespace DotNetNuke.DependencyInjection.Scopes.Initializer;
internal class ScopeAccessorInitializer<TScopeDefinition>
    : IScopeAccessorInitializer<TScopeDefinition>
    where TScopeDefinition : ScopeDefinition, new()
{
    public bool ShouldNotInheritState { get; set; }

    public void Run(string currentName, IServiceProvider currentServiceProvider, IServiceProvider parentServiceProvider)
    {
        // Generate the new scope accessor, which will need to be initialized
        var newScopeAccessor = currentServiceProvider.GetRequiredService<IServiceScopeAccessor<TScopeDefinition>>();

        // Check if the parent service provider has this service registered
        var parentScopeAccessor = parentServiceProvider.GetRequiredService<IServiceScopeAccessor<TScopeDefinition>>();

        // If the parent is the root scope, then we need to use the current service provider
        // Otherwise we're already in a deeper scope, and we should use the one provided by the previous scope accessor
        var pageScopeServiceProvider = parentScopeAccessor.IsValid && !ShouldNotInheritState
            ? parentScopeAccessor.ServiceProvider
            : currentServiceProvider;

        // Initialize the new scope accessor with the service provider and current name
        newScopeAccessor.SetupServiceProvider(pageScopeServiceProvider, currentName);
    }

}
