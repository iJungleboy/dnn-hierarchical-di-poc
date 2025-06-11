using DotNetNuke.Page;

namespace DotNetNuke.UnitTests.TestPages;
public class TestPageInfoInGlobalScope(IServiceProvider globalServiceProvider)
{
    /// <summary>
    /// Assert that we cannot get a page info in the global scope.
    /// This is to avoid placing page scoped services in the global scope,
    /// </summary>
    [Fact]
    public void PageInfoDefaultInGlobalScopeNotAllowed()
    {
        var globalPageInfo = globalServiceProvider.GetRequiredService<IPageInfo>();
        Throws<InvalidOperationException>(() => globalPageInfo.PageId);
    }
    /// <summary>
    /// Assert that we cannot get a page info in the global scope.
    /// This is to avoid placing page scoped services in the global scope,
    /// </summary>
    [Fact]
    public void PageInfoScopedInGlobalScopeNotAllowed()
    {
        // In this mode it already throws when trying to create the service
        Throws<InvalidOperationException>(globalServiceProvider.GetRequiredService<IPageInfoWhichCanOnlyLiveOnPageScope>);
    }

}
