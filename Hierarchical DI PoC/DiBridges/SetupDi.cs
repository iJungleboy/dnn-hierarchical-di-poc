using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using ToSic.HierarchicalDI.Module;
using ToSic.HierarchicalDI.Page;

namespace ToSic.HierarchicalDI.DiBridges;
internal static class SetupDi
{
    public static IServiceCollection SetupPageAndModuleScopes(this IServiceCollection services)
    {
        services.TryAddScoped<PageScopeAccessor>();
        services.AddTransient(typeof(IPageScopedService<>), typeof(PageScopedService<>));
        services.TryAddScoped<PageInfoReal>();
        services.TryAddTransient<PageInfoInitializerService>();
        services.TryAddTransient<IPageInfo, PageInfo>();
        services.TryAddScoped<IModuleInfo, ModuleInfo>();

        return services;
    }
}
