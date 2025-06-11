using DotNetNuke.DependencyInjection.Scopes.Accessors;
using DotNetNuke.DependencyInjection.Scopes.Definitions;
using DotNetNuke.Module;
using DotNetNuke.Page;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DotNetNuke;
internal static class StartupScopes
{
    public static IServiceCollection SetupPageAndModuleScopes(this IServiceCollection services)
    {
        // Scope Initializers
        services.TryAddTransient(typeof(IServiceScopeAccessorInitializer<>), typeof(ServiceScopeAccessorInitializer<>));
        services.TryAddScoped<ServiceScopeAccessorInitializersManager>();

        // Scope Accessors and Scoped Services
        services.TryAddScoped(typeof(IServiceScopeAccessor<>), typeof(ServiceScopeAccessor<>));
        services.TryAddScoped(typeof(IFromScope<,>), typeof(FromScope<,>));

        // Scope Definitions - mainly for debugging
        // These must be initialized when a new scope is created, so that all services in that scope can query it
        services.TryAddScoped<ICurrentScopeDefinition, CurrentScopeDefinition>();
        
        // Page Info
        services.TryAddScoped<PageInfoState>();
        services.TryAddTransient<IPageInfo, PageInfo>();
        services.TryAddTransient<PageInfoInitializerService>();

        // Module Info
        services.TryAddScoped<ModuleInfoState>();
        services.TryAddTransient<IModuleInfo, ModuleInfo>();
        services.TryAddTransient<ModuleInfoInitializerService>();

        return services;
    }
}
