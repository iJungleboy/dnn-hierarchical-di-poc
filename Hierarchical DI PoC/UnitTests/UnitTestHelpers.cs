using DotNetNuke.DependencyInjection;
using DotNetNuke.Module;
using DotNetNuke.Page;
using Microsoft.Extensions.DependencyInjection;

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
            .CreatePageScopedServiceProvider();

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
            .CreateModuleScopedServiceProvider();

        var moduleOfModuleScope1 = moduleSp.GetRequiredService<ModuleInfoReal>();
        moduleOfModuleScope1.ModuleId = moduleId;
        return moduleSp;
    }

}
