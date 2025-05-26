using DotNetNuke.DependencyInjection.Scopes;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNuke.DependencyInjection;
internal static class ServiceScopeHelpers
{
    public static IServiceProvider CreateSubScope<TScopeAccessor>(this IServiceProvider globalServiceProvider, string scopeName)
        where TScopeAccessor : IServiceScopeAccessor
    {
        var newScope = globalServiceProvider.CreateScope();
        var newServiceProvider = newScope.ServiceProvider;

        // Make sure the page scope has the PageScopeAccessor initialized
        var initializer = newServiceProvider.GetRequiredService<CurrentScopeInitializer>();
        initializer.Add<TScopeAccessor>();
        initializer.Run(scopeName, globalServiceProvider);
        return newServiceProvider;
    }

    //public static IServiceProvider CreatePageScopedServiceProvider(this IServiceProvider globalServiceProvider)
    //{
    //    var newScope = globalServiceProvider.CreateScope();
    //    var newServiceProvider = newScope.ServiceProvider;

    //    // Make sure the page scope has the PageScopeAccessor initialized
    //    var initializer = newServiceProvider.GetRequiredService<CurrentScopeInitializer>();
    //    initializer.Add<IPageScopeAccessor>();
    //    initializer.Run(ServiceScopeConstants.ScopePage, globalServiceProvider);
    //    return newServiceProvider;
    //}

    //public static IServiceProvider CreateModuleScopedServiceProvider(this IServiceProvider pageServiceProvider)
    //{
    //    var moduleSp = pageServiceProvider.CreateScope().ServiceProvider;

    //    // In the module scope, we initialize the scoped PageScope Accessor and give it the parent scope
    //    // This is necessary for it to be able to give page-scoped objects

    //    var initializer = moduleSp.GetRequiredService<CurrentScopeInitializer>();
    //    initializer.Add<IModuleScopeAccessor>();
    //    initializer.Run(ServiceScopeConstants.ScopePage, pageServiceProvider);

    //    //moduleSp.GetRequiredService<IScopeInitializer<IPageScopeAccessor>>()
    //    //    .Run(ServiceScopeConstants.ScopeModule, moduleSp, pageServiceProvider);

    //    //moduleSp.GetRequiredService<IScopeInitializer<IModuleScopeAccessor>>()
    //    //    .Run(ServiceScopeConstants.ScopeModule, moduleSp, pageServiceProvider);

    //    return moduleSp;
    //}
}
