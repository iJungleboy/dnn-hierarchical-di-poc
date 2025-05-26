using DotNetNuke.DependencyInjection.PageScope;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetNuke.DependencyInjection;
internal static class ServiceScopeHelpers
{
    public static IServiceProvider CreatePagesScopedServiceProvider(this IServiceProvider globalServiceProvider)
    {
        var pageScope = globalServiceProvider.CreateScope();
        var pageSp = pageScope.ServiceProvider;

        // Make sure the page scope has the PageScopeAccessor initialized
        pageSp.GetRequiredService<IPageScopeAccessor>()
            .AttachPageScopedServiceProvider(pageSp, "page");
        return pageSp;
    }

    public static IServiceProvider CreateModuleScopedServiceProvider(IServiceProvider pageSp)
    {
        var moduleSp = pageSp.CreateScope().ServiceProvider;

        // In the module scope, we initialize the scoped PageScope Accessor and give it the parent scope
        // This is necessary for it to be able to give page-scoped objects
        moduleSp.GetRequiredService<IPageScopeAccessor>()
            .AttachPageScopedServiceProvider(pageSp, "module");

        return moduleSp;
    }
}
