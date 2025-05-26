using DotNetNuke.DependencyInjection.Scopes.Initializer;
using DotNetNuke.Module;
using DotNetNuke.Page;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DotNetNuke;
internal static class StartupScopes
{
    public static IServiceCollection SetupPageAndModuleScopes(this IServiceCollection services)
    {
        // Scope Initializers
        services.TryAddTransient(typeof(IScopeInitializer<>), typeof(ScopeAccessorInitializer<>));
        services.TryAddScoped<CurrentScopeInitializer>();

        // Page Scopes
        services.TryAddScoped<IPageScopeAccessor, PageScopeAccessor>();
        services.AddTransient(typeof(IPageScopedService<>), typeof(PageScopedService<>));
        
        // Page Info
        services.TryAddScoped<PageInfoReal>();
        services.TryAddTransient<IPageInfo, PageInfo>();
        services.TryAddTransient<PageInfoInitializerService>();

        // Module Scopes
        services.TryAddScoped<IModuleScopeAccessor, ModuleScopeAccessor>();
        services.AddTransient(typeof(IModuleScopedService<>), typeof(ModuleScopedService<>));

        // Module Info
        services.TryAddScoped<ModuleInfoReal>();
        services.TryAddTransient<IModuleInfo, ModuleInfo>();

        return services;
    }
}
