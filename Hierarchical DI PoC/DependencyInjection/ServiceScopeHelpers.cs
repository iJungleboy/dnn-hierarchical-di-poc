using Microsoft.Extensions.DependencyInjection;
using ToSic.HierarchicalDI.DependencyInjection.PageScope;

namespace ToSic.HierarchicalDI.DependencyInjection;
internal static class ServiceScopeHelpers
{
    public static IServiceProvider CreatePagesScopedServiceProvider(this IServiceProvider globalServiceProvider)
    {
        var pageScope = globalServiceProvider.CreateScope();
        var pageSp = pageScope.ServiceProvider;
        return pageSp;
    }

    public static IServiceProvider CreateModuleScopedServiceProvider(IServiceProvider pageSp)
    {
        var moduleSp = pageSp.CreateScope().ServiceProvider;

        // In the module scope, we initialize the scoped PageScope Accessor and give it the parent scope
        // This is necessary for it to be able to give page-scoped objects
        var innerPageScopeAccessor =  moduleSp.GetRequiredService<IPageScopeAccessor>();
        innerPageScopeAccessor.AttachPageServiceProvider(pageSp);

        return moduleSp;
    }
}
