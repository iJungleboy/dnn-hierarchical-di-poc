using DotNetNuke.DependencyInjection.Scopes.Accessors;
using DotNetNuke.DependencyInjection.Scopes.Definitions;

namespace DotNetNuke.DependencyInjection;
internal static class ServiceScopeHelpers
{
    public static IServiceProvider CreateScope<TScopeDefinition>(this IServiceProvider parentServiceProvider, bool shouldNotInheritDefinition = false)
        where TScopeDefinition : ScopeDefinition, new()
    {
        // Create a new scope and get its service provider
        var newScope = parentServiceProvider.CreateScope();
        var newServiceProvider = newScope.ServiceProvider;

        // Start by ensuring that the scope definition is initialized
        var scopeInfo = newServiceProvider.GetRequiredService<ICurrentScopeDefinition>();
        // Tell the scope definition "where" it is, so it can be queried later on for debugging purposes
        scopeInfo.Definition = new TScopeDefinition();
        scopeInfo.RestartsScopeWip = shouldNotInheritDefinition;


        // Get the scope initializer, add the new scope accessor so it too will be initialized, and run
        var scopeManager = newServiceProvider.GetRequiredService<ServiceScopeAccessorInitializersManager>();
        scopeManager.InheritInitializersFromParent(parentServiceProvider);
        scopeManager.AddInitializer<TScopeDefinition>(shouldNotInheritDefinition);
        scopeManager.RunInitializers(new TScopeDefinition().ScopeName, parentServiceProvider);

        // Return the new service provider
        return newServiceProvider;
    }
}
