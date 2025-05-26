using DotNetNuke.DependencyInjection;
using DotNetNuke.Module;
using DotNetNuke.Page;

namespace DotNetNuke.UnitTests;
internal static class UnitTestHelpers
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
            .CreateSubScope<ScopePage>(ServiceScopeConstants.ScopePage);


        pageSp.GetRequiredService<PageInfoInitializerService>()
            .SetupCurrentPage(pageId);
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
            .CreateSubScope<ScopeModule>(ServiceScopeConstants.ScopeModule);

        moduleSp.GetRequiredService<ModuleInfoInitializerService>()
            .SetupCurrentModule(moduleId);
        //moduleOfModuleScope1.ModuleId = moduleId;
        return moduleSp;
    }

}
