using DotNetNuke.DependencyInjection.Scopes.Definitions;
using DotNetNuke.Module;
using DotNetNuke.Page;

namespace DotNetNuke.DependencyInjection;
internal static class ServiceScopes
{
    /// <summary>
    /// Create a page scope and prepare shared page context.
    /// </summary>
    /// <param name="globalServiceProvider"></param>
    /// <param name="pageId"></param>
    /// <returns></returns>
    internal static IServiceProvider SetupPage(this IServiceProvider globalServiceProvider, int pageId)
    {
        // Create the scope and directly the service provider for the page scope
        var pageSp = globalServiceProvider
            .CreateScope<ScopePage>(false);


        var currentPage = pageSp.GetRequiredService<PageInfoInitializerService>();
        currentPage.SetupCurrentPage(pageId);
        return pageSp;
    }

    /// <summary>
    /// Create a page scope and prepare shared page context.
    /// </summary>
    /// <param name="pageServiceProvider"></param>
    /// <param name="moduleId"></param>
    /// <returns></returns>
    internal static IServiceProvider SetupModule(this IServiceProvider pageServiceProvider, int moduleId)
    {
        // Create the scope and directly the service provider for the page scope
        var moduleSp = pageServiceProvider
            .CreateScope<ScopeModule>(false);

        var currentModule = moduleSp.GetRequiredService<ModuleInfoInitializerService>();
        currentModule.SetupCurrentModule(moduleId);
        return moduleSp;
    }

    /// <summary>
    /// Create a page scope and prepare shared page context.
    /// </summary>
    /// <param name="moduleServiceProvider"></param>
    /// <param name="moduleId"></param>
    /// <returns></returns>
    internal static IServiceProvider SetupModuleInModule(this IServiceProvider moduleServiceProvider, int moduleId)
    {
        // Create the scope and directly the service provider for the page scope
        var subModuleSp = moduleServiceProvider
            .CreateScope<ScopeModule>(true);

        // Get main module definition from module scope
        var parentModuleInfo = moduleServiceProvider.GetRequiredService<ModuleInfoState>();

        var submodule = subModuleSp.GetRequiredService<ModuleInfoInitializerService>();
        submodule.SetupSubmodule(moduleId, parentModuleInfo);
        return subModuleSp;
    }

}
