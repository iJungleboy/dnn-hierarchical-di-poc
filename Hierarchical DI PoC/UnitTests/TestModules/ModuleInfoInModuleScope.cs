using DotNetNuke.DependencyInjection;
using DotNetNuke.Module;

namespace DotNetNuke.UnitTests.TestModules;
public class ModuleInfoInModuleScope(IServiceProvider globalServiceProvider)
{
    private (IModuleInfo first, IModuleInfo second) ArrangeTwoModuleInfoFromSameModuleScope(int moduleId)
    {
        // Arrange
        // Create a page scope and prepare shared page context
        var pageSp = globalServiceProvider.SetupPage(999);

        // Simulate two modules on the same page, each with its own scope
        var moduleSp = pageSp.SetupModule(moduleId);

        // Simulate retrieving the same module in the first scope
        var first = moduleSp.GetRequiredService<IModuleInfo>();
        var second = moduleSp.GetRequiredService<IModuleInfo>();
        return (first, second);
    }

    [Theory]
    [InlineData(101)]
    public void ModuleInfoOnSameScopeHaveSameId(int moduleId)
    {
        var (first, second) = ArrangeTwoModuleInfoFromSameModuleScope(moduleId);
        Equal(moduleId, first.ModuleId);
        Equal(moduleId, second.ModuleId);
    }

    [Theory]
    [InlineData(101)]
    public void ModuleInfoOnSameScopeNotSame(int moduleId)
    {
        var (first, second) = ArrangeTwoModuleInfoFromSameModuleScope(moduleId);
        NotSame(first, second);
    }

    private (IModuleInfo first, IModuleInfo second) ArrangeTwoModuleInfoFromDifferentModuleScopes(int moduleId1, int moduleId2)
    {
        // Arrange
        // Create a page scope and prepare shared page context
        var pageSp = globalServiceProvider.SetupPage(999);

        // Simulate two modules on the same page, each with its own scope
        var moduleSp1 = pageSp.SetupModule(moduleId1);
        var moduleSp2 = pageSp.SetupModule(moduleId2);

        // Simulate retrieving the same module in the first scope
        var moduleInfoScope1 = moduleSp1.GetRequiredService<IModuleInfo>();

        // Simulate retrieving the same module in the second scope
        var moduleInfoScope2 = moduleSp2.GetRequiredService<IModuleInfo>();
        return (moduleInfoScope1, moduleInfoScope2);
    }


    [Theory]
    [InlineData(101, 102)]
    public void ModuleInfoFromSeparateScopesAreSeparate(int moduleId1, int moduleId2)
    {
        // Arrange
        var (first, second) = ArrangeTwoModuleInfoFromDifferentModuleScopes(moduleId1, moduleId2);

        // Assert that the modules in different scopes are not the same
        NotSame(first, second);
        NotEqual(first.ModuleId, second.ModuleId);
    }

    [Theory]
    [InlineData(101, 102)]
    public void ModuleInfoFromSeparateScopesKeepOwnValue(int moduleId1, int moduleId2)
    {
        // Arrange
        var (first, second) = ArrangeTwoModuleInfoFromDifferentModuleScopes(moduleId1, moduleId2);

        // Assert that the modules in different scopes are not the same
        Equal(moduleId1, first.ModuleId);
        Equal(moduleId2, second.ModuleId);
        NotEqual(first.ModuleId, second.ModuleId);
    }

    [Theory]
    [InlineData(101, 102)]
    public void ModuleInfoFromSeparateScopesAreNotSame(int moduleId1, int moduleId2)
    {
        var (first, second) = ArrangeTwoModuleInfoFromDifferentModuleScopes(moduleId1, moduleId2);
        NotSame(first, second);
    }
}
