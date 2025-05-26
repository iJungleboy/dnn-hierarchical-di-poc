using DotNetNuke.DependencyInjection.Scopes.Accessors;
using DotNetNuke.DependencyInjection.Scopes.Initializer;

namespace DotNetNuke.DependencyInjection;
internal static class ServiceScopeHelpers
{
    public static IServiceProvider CreateSubScope<TScopeAccessor>(this IServiceProvider globalServiceProvider, string scopeName)
        where TScopeAccessor : IServiceScopeAccessor
    {
        // Create a new scope and get its service provider
        var newScope = globalServiceProvider.CreateScope();
        var newServiceProvider = newScope.ServiceProvider;

        // Get the scope initializer, add the new scope accessor so it too will be initialized, and run
        var initializer = newServiceProvider.GetRequiredService<CurrentScopeInitializer>();
        initializer.AddInitializer<TScopeAccessor>();
        initializer.Run(scopeName, globalServiceProvider);

        // Return the new service provider
        return newServiceProvider;
    }
}
