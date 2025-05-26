using DotNetNuke.DependencyInjection.Scopes.Initializer;

namespace DotNetNuke.DependencyInjection;
internal static class ServiceScopeHelpers
{
    public static IServiceProvider CreateSubScope<TScopeDefinition>(this IServiceProvider globalServiceProvider)
        where TScopeDefinition : ScopeDefinition, new()
    {
        // Create a new scope and get its service provider
        var newScope = globalServiceProvider.CreateScope();
        var newServiceProvider = newScope.ServiceProvider;

        // Get the scope initializer, add the new scope accessor so it too will be initialized, and run
        var initializer = newServiceProvider.GetRequiredService<CurrentScopeInitializer>();
        initializer.AddInitializer<TScopeDefinition>();
        initializer.Run(new TScopeDefinition().ScopeName, globalServiceProvider);

        // Return the new service provider
        return newServiceProvider;
    }
}
