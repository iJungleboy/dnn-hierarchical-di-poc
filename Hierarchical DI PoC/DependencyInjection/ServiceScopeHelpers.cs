using DotNetNuke.DependencyInjection.Scopes.Accessors;

namespace DotNetNuke.DependencyInjection;
internal static class ServiceScopeHelpers
{
    public static IServiceProvider CreateScope<TScopeDefinition>(this IServiceProvider parentServiceProvider, bool shouldNotInheritDefinition = false)
        where TScopeDefinition : ScopeDefinition, new()
    {
        // Create a new scope and get its service provider
        var newScope = parentServiceProvider.CreateScope();
        var newServiceProvider = newScope.ServiceProvider;

        // Get the scope initializer, add the new scope accessor so it too will be initialized, and run
        var initializer = newServiceProvider.GetRequiredService<ServiceScopeAccessorInitializersManager>();
        initializer.InheritFromParent(parentServiceProvider);
        initializer.AddInitializer<TScopeDefinition>(shouldNotInheritDefinition);
        initializer.Run(new TScopeDefinition().ScopeName, parentServiceProvider);

        // Return the new service provider
        return newServiceProvider;
    }
}
