using Microsoft.Extensions.DependencyInjection;
using ToSic.HierarchicalDI.DependencyInjection;
using ToSic.HierarchicalDI.Module;
using ToSic.HierarchicalDI.Page;
using static Xunit.Assert;

namespace ToSic.HierarchicalDI.UnitTests;
public class TestHierarchicalDi(IServiceProvider globalServiceProvider)
{
    private const int MockPageId = 88;

    [Fact]
    public void PageScopedPageInfoIsInitialized()
    {
        // Arrange
        // Create a page scope and prepare shared page context
        var pageSp = globalServiceProvider.SetupPage(MockPageId);

        var pageOfPageScope = pageSp.GetRequiredService<IPageInfo>();
        Equal(MockPageId, pageOfPageScope.PageId);
    }

    [Fact]
    public void PageScopedPageInfosAreInitializedButNotSame()
    {
        var pageSp = globalServiceProvider.SetupPage(MockPageId);
        var pageInfo1 = pageSp.GetRequiredService<IPageInfo>();
        var pageInfo2 = pageSp.GetRequiredService<IPageInfo>();
        Equal(MockPageId, pageInfo1.PageId);
        Equal(MockPageId, pageInfo2.PageId);
        NotSame(pageInfo1, pageInfo2);
    }

    [Fact]
    public void TestTransientService()
    {
        // Arrange
        // Create a page scope and prepare shared page context
        var pageSp = globalServiceProvider.SetupPage(MockPageId);

        var pageOfPageScope = pageSp.GetRequiredService<IPageInfo>();

        // Simulate two modules on the same page, each with its own scope
        var moduleSp1 = ServiceScopeHelpers.CreateModuleScopedServiceProvider(pageSp);
        var moduleSp2 = ServiceScopeHelpers.CreateModuleScopedServiceProvider(pageSp);

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

        // Setup modules

        // Set up the modules in each scope with different IDs
        var moduleOfModuleScope1 = moduleSp1.GetRequiredService<IModuleInfo>();
        ((ModuleInfo)moduleOfModuleScope1).ModuleId = 101;
        var moduleOfModuleScope2 = moduleSp2.GetRequiredService<IModuleInfo>();
        ((ModuleInfo)moduleOfModuleScope2).ModuleId = 102;

        // Simulate retrieving the same module in the first scope
        var moduleOfModScope1A = moduleSp1.GetRequiredService<IModuleInfo>();
        var moduleOfModScope1B = moduleSp1.GetRequiredService<IModuleInfo>();

        // Assert that the module is the same in the first scope
        Same(moduleOfModScope1A, moduleOfModScope1B);
        Equal(moduleOfModScope1A.ModuleId, moduleOfModScope1B.ModuleId);

        // Simulate retrieving the same module in the second scope
        var moduleOfModScope2A = moduleSp2.GetRequiredService<IModuleInfo>();
        var moduleOfModScope2B = moduleSp2.GetRequiredService<IModuleInfo>();

        // Assert that the module is the same in the second scope
        Same(moduleOfModScope2A, moduleOfModScope2B);
        Equal(moduleOfModScope2A.ModuleId, moduleOfModScope2B.ModuleId);

        // Assert that the modules in different scopes are not the same
        NotSame(moduleOfModScope1A, moduleOfModScope2A);
        NotEqual(moduleOfModScope1A.ModuleId, moduleOfModScope2A.ModuleId);
    }
}
