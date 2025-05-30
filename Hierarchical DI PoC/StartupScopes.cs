﻿using DotNetNuke.DependencyInjection.Scopes.Accessors;
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
        services.TryAddTransient(typeof(IScopeAccessorInitializer<>), typeof(ScopeAccessorInitializer<>));
        services.TryAddScoped<CurrentScopeInitializer>();

        // Scope Accessors and Scoped Services
        services.TryAddScoped(typeof(IServiceScopeAccessor<>), typeof(ServiceScopeAccessor<>));
        services.TryAddScoped(typeof(IFromScope<,>), typeof(FromScope<,>));
        
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
