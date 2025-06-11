using DotNetNuke.DependencyInjection.Scopes.Accessors;
using DotNetNuke.DependencyInjection.Scopes.Definitions;
using DotNetNuke.Module;
using DotNetNuke.Page;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DotNetNuke;
internal static class StartupScopes
{
    public static IServiceCollection SetupScopeSystem(this IServiceCollection services)
    {
        // *** Scope Initializers ***
        // These initializers will be run when a new scope is created, to set up the scope accessors and definitions.
        services.TryAddTransient(typeof(IServiceScopeAccessorInitializer<>), typeof(ServiceScopeAccessorInitializer<>));
        // This is a manager that will run all initializers for the current scope, and can be used to add new initializers.
        services.TryAddScoped<ServiceScopeAccessorInitializersManager>();

        // *** Scope Accessors and Scoped Services ***
        // The accessor contains the service provider for the current scope, and is used to access services registered in that scope.
        services.TryAddScoped(typeof(IServiceScopeAccessor<>), typeof(ServiceScopeAccessor<>));
        // The FromScope service is used to access services registered from a specific scope (it uses the IServiceScopeAccessor internally).
        services.TryAddScoped(typeof(IFromScope<,>), typeof(FromScope<,>));

        // *** Scope Definitions - mainly for debugging ***
        // These must be setup when a new scope is created, so that all services in that scope can query it
        services.TryAddScoped<ICurrentScopeDefinition, CurrentScopeDefinition>();
        
        return services;
    }

    public static IServiceCollection SetupPageAndModuleScopes(this IServiceCollection services)
    {
        // *** Page Info ***
        services.TryAddScoped<PageInfoState>();
        services.TryAddTransient<IPageInfo, PageInfo>();
        services.TryAddTransient<PageInfoInitializerService>();

        // *** Experimental - Page Info which can only live on Page Scope ***
        // The "real" object which is scoped, and _should_ only ever be accessed from the page scope.
        // This registration uses the internal class, so it can only be accessed from internal code.
        services.TryAddScoped<PageInfoWhichCanOnlyLiveOnPageScope>();
        // The interface to access the object
        services.TryAddTransient<IPageInfoWhichCanOnlyLiveOnPageScope>(sp =>
            // Retrieve the service from the page scope, which is the only place it should be accessed from
            sp.GetRequiredService<IFromScope<ScopePage, PageInfoWhichCanOnlyLiveOnPageScope>>().Value
        );

        // Module Info
        services.TryAddScoped<ModuleInfoState>();
        services.TryAddTransient<IModuleInfo, ModuleInfo>();
        services.TryAddTransient<ModuleInfoInitializerService>();

        return services;
    }

}
