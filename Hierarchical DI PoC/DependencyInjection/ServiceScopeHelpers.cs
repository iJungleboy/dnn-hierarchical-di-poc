using DotNetNuke.DependencyInjection.Scopes;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNuke.DependencyInjection;
internal static class ServiceScopeHelpers
{
    public static IServiceProvider CreatePageScopedServiceProvider(this IServiceProvider globalServiceProvider)
    {
        var pageScope = globalServiceProvider.CreateScope();
        var pageSp = pageScope.ServiceProvider;

        // Make sure the page scope has the PageScopeAccessor initialized
        var initializer = pageSp.GetRequiredService<CurrentScopeInitializer>();
        var pageInitializer = pageSp.GetRequiredService<IScopeInitializer<IPageScopeAccessor>>();
        initializer.Add(pageInitializer);
        initializer.Run(ServiceScopeConstants.ScopePage, pageSp, globalServiceProvider);
        pageSp.GetRequiredService<IScopeInitializer<IPageScopeAccessor>>()
            .Run(ServiceScopeConstants.ScopePage, pageSp, globalServiceProvider);
        return pageSp;
    }

    public static IServiceProvider CreateModuleScopedServiceProvider(this IServiceProvider pageServiceProvider)
    {
        var moduleSp = pageServiceProvider.CreateScope().ServiceProvider;

        // In the module scope, we initialize the scoped PageScope Accessor and give it the parent scope
        // This is necessary for it to be able to give page-scoped objects
        moduleSp.GetRequiredService<IScopeInitializer<IPageScopeAccessor>>()
            .Run(ServiceScopeConstants.ScopeModule, moduleSp, pageServiceProvider);

        moduleSp.GetRequiredService<IScopeInitializer<IModuleScopeAccessor>>()
            .Run(ServiceScopeConstants.ScopeModule, moduleSp, pageServiceProvider);

        return moduleSp;
    }
}
