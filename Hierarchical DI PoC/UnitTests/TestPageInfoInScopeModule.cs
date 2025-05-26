using DotNetNuke.Page;
using Microsoft.Extensions.DependencyInjection;
using static Xunit.Assert;

namespace DotNetNuke.UnitTests;
public class TestPageInfoInScopeModule(IServiceProvider globalServiceProvider)
{
    public const int MockPageId = 88;

    [Fact]
    public void TestTransientService()
    {
        // Arrange
        // Create a page scope and prepare shared page context
        var pageSp = globalServiceProvider.SetupPage(MockPageId);

        var pageOfPageScope = pageSp.GetRequiredService<IPageInfo>();

        // Simulate two modules on the same page, each with its own scope
        var moduleSp1 = pageSp.SetupModule(101);
        var moduleSp2 = pageSp.SetupModule(102);

        // Act
        var pageOfModuleScope1 = moduleSp1.GetRequiredService<IPageInfo>();
        var pageOfModuleScope2 = moduleSp2.GetRequiredService<IPageInfo>();

        // Assert all share the same pageId
        Equal(pageOfPageScope.PageId, pageOfModuleScope1.PageId);
        Equal(pageOfPageScope.PageId, pageOfModuleScope2.PageId);

        // Assert that they are not the same instance
        NotSame(pageOfModuleScope1, pageOfModuleScope2);
        NotSame(pageOfPageScope, pageOfModuleScope1);
        NotSame(pageOfPageScope, pageOfModuleScope2);

    }
}
