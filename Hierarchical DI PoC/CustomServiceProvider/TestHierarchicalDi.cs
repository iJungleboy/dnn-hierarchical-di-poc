using Microsoft.Extensions.DependencyInjection;
using ToSic.HierarchicalDI.DiBridges;
using ToSic.HierarchicalDI.Module;
using ToSic.HierarchicalDI.Page;

namespace ToSic.HierarchicalDI.CustomServiceProvider;
public class TestHierarchicalDi
{
    [Fact]
    public void TestTransientService()
    {
        // Arrange
        var serviceProvider = new ServiceCollection()
            .SetupPageAndModuleScopes()
            .BuildServiceProvider();

        // Create a page scope and prepare shared page context
        var pageSp = serviceProvider.CreatePagesScopedServiceProvider();//.CreateScope();
        //var pageSp = pageScope.ServiceProvider;
        var pageOfPageScope = pageSp.GetRequiredService<PageInfoReal>();
        // Initialize the page info...
        pageOfPageScope.PageId = 88;

        // Simulate two modules on the same page, each with its own scope
        var moduleSp1 = ServiceScopeHelpers.CreateModuleScopedServiceProvider(pageSp);
        var moduleSp2 = ServiceScopeHelpers.CreateModuleScopedServiceProvider(pageSp);

        // Act
        var pageOfModuleScope1 = moduleSp1.GetRequiredService<IPageInfo>();
        var pageOfModuleScope2 = moduleSp2.GetRequiredService<IPageInfo>();

        // Assert all share the same pageId
        Assert.Equal(pageOfPageScope.PageId, pageOfModuleScope1.PageId);
        Assert.Equal(pageOfPageScope.PageId, pageOfModuleScope2.PageId);

        // Assert that they are not the same instance
        Assert.NotSame(pageOfModuleScope1, pageOfModuleScope2);
        Assert.NotSame(pageOfPageScope, pageOfModuleScope1);
        Assert.NotSame(pageOfPageScope, pageOfModuleScope2);

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
        Assert.Same(moduleOfModScope1A, moduleOfModScope1B);
        Assert.Equal(moduleOfModScope1A.ModuleId, moduleOfModScope1B.ModuleId);

        // Simulate retrieving the same module in the second scope
        var moduleOfModScope2A = moduleSp2.GetRequiredService<IModuleInfo>();
        var moduleOfModScope2B = moduleSp2.GetRequiredService<IModuleInfo>();

        // Assert that the module is the same in the second scope
        Assert.Same(moduleOfModScope2A, moduleOfModScope2B);
        Assert.Equal(moduleOfModScope2A.ModuleId, moduleOfModScope2B.ModuleId);

        // Assert that the modules in different scopes are not the same
        Assert.NotSame(moduleOfModScope1A, moduleOfModScope2A);
        Assert.NotEqual(moduleOfModScope1A.ModuleId, moduleOfModScope2A.ModuleId);
    }
}
