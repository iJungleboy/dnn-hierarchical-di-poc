using Microsoft.Extensions.DependencyInjection;
using ToSic.HierarchicalDI.DependencyInjection;
using ToSic.HierarchicalDI.Page;

namespace ToSic.HierarchicalDI.UnitTests;
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
        var pageSp = globalServiceProvider
            .CreatePagesScopedServiceProvider();
        pageSp.GetRequiredService<PageInfoInitializerService>()
            .SetupCurrentPage(pageId);
        return pageSp;
    }

}
