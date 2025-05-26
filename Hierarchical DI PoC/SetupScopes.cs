using DotNetNuke.DependencyInjection.PageScope;
using DotNetNuke.Module;
using DotNetNuke.Page;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DotNetNuke;
internal static class SetupScopes
{
    public static IServiceCollection SetupPageAndModuleScopes(this IServiceCollection services)
    {
        services.TryAddScoped<IPageScopeAccessor, PageScopeAccessor>();
        services.AddTransient(typeof(IPageScopedService<>), typeof(PageScopedService<>));
        
        services.TryAddScoped<PageInfoReal>();
        services.TryAddTransient<IPageInfo, PageInfo>();
        services.TryAddTransient<PageInfoInitializerService>();

        services.TryAddScoped<ModuleInfoReal>();
        services.TryAddScoped<IModuleInfo, ModuleInfo>();

        return services;
    }
}
