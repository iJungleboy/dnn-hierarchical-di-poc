using Microsoft.Extensions.DependencyInjection;

namespace ToSic.HierarchicalDI.DiBridges;
internal class ServiceScopeHelpers
{
    public static IServiceProvider CreateModuleScopedServiceProvider(IServiceProvider pageSp)
    {
        var moduleSp = pageSp.CreateScope().ServiceProvider;

        // In the module scope, we initialize the scoped PageScope Accessor and give it the parent scope
        // This is necessary for it to be able to give page-scoped objects
        var innerPageScopeAccessor =  moduleSp.GetRequiredService<PageScopeAccessor>();
        innerPageScopeAccessor.AttachPageServiceProvider(pageSp);

        return moduleSp;
    }
}
