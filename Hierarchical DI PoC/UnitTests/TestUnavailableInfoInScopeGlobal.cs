using DotNetNuke.Module;
using DotNetNuke.Page;

namespace DotNetNuke.UnitTests;
public class TestUnavailableInfoInScopeGlobal(IServiceProvider globalServiceProvider)
{
    /// <summary>
    /// Assert that we cannot get a page info in the global scope.
    /// This is to avoid placing page scoped services in the global scope,
    /// </summary>
    [Fact]
    public void PageInfoInGlobalScopeNotAllowed()
    {
        var globalPageInfo = globalServiceProvider.GetRequiredService<IPageInfo>();
        Throws<InvalidOperationException>(() => globalPageInfo.PageId);
    }

    /// <summary>
    /// Assert that we cannot get a page info in the global scope.
    /// This is to avoid placing page scoped services in the global scope,
    /// </summary>
    [Fact]
    public void ModuleInfoInGlobalScopeNotAllowed()
    {
        var globalPageInfo = globalServiceProvider.GetRequiredService<IModuleInfo>();
        Throws<InvalidOperationException>(() => globalPageInfo.ModuleId);
    }

}
