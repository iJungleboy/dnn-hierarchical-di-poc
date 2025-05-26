using DotNetNuke.DependencyInjection;
using DotNetNuke.Page;

namespace DotNetNuke.UnitTests;
public class TestPageInfoInScopeModule(IServiceProvider globalServiceProvider)
{
    public const int MockPageId = 88;

    private (IPageInfo ofPageScope, IPageInfo ofModuleScope) ArrangePageInfosOfPageAndModuleScope(int pageId, int moduleId)
    {
        // Arrange
        // Create a page scope and prepare shared page context
        var pageSp = globalServiceProvider.SetupPage(pageId);

        // Simulate two modules on the same page, each with its own scope
        var moduleSp1 = pageSp.SetupModule(moduleId);

        // Act
        var pageOfPageScope = pageSp.GetRequiredService<IPageInfo>();
        var pageOfModuleScope = moduleSp1.GetRequiredService<IPageInfo>();
        return (pageOfPageScope, pageOfModuleScope);
    }

    [Fact]
    public void PageInfoHasSameIdInPageAndModuleScope()
    {
        var (pageOfPageScope, pageOfModuleScope1) = ArrangePageInfosOfPageAndModuleScope(21, 101);

        // Assert all share the same pageId
        Equal(21, pageOfPageScope.PageId);
        Equal(pageOfPageScope.PageId, pageOfModuleScope1.PageId);
    }

    [Fact]
    public void PageInfoFromPageAndModuleScopeAreNotSame()
    {
        var (pageOfPageScope, pageOfModuleScope1) = ArrangePageInfosOfPageAndModuleScope(21, 101);
        // Assert that they are not the same instance
        NotSame(pageOfPageScope, pageOfModuleScope1);
    }

    [Theory]
    [InlineData(21)]
    [InlineData(42)]
    public void PageInfoFromMultipleModuleScopes(int pageId)
    {
        // Arrange
        // Create a page scope and prepare shared page context
        // Simulate two modules on the same page, each with its own scope
        var pageSp = globalServiceProvider.SetupPage(pageId);
        var moduleSp1 = pageSp.SetupModule(101);
        var moduleSp2 = pageSp.SetupModule(102);

        // Act
        var pageOfModuleScope1 = moduleSp1.GetRequiredService<IPageInfo>();
        var pageOfModuleScope2 = moduleSp2.GetRequiredService<IPageInfo>();

        // Assert all share the same pageId
        Equal(pageId, pageOfModuleScope1.PageId);
        Equal(pageId, pageOfModuleScope2.PageId);

        // Assert that they are not the same instance
        NotSame(pageOfModuleScope1, pageOfModuleScope2);
    }
}
