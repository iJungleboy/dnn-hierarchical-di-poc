using DotNetNuke.DependencyInjection;
using DotNetNuke.Module;

namespace DotNetNuke.UnitTests.TestModules;
internal static class TestModuleHelpers
{
    internal static (IModuleInfo first, IModuleInfo second)
        ArrangeTwoModuleInfoFromSameModuleScope(this IServiceProvider globalServiceProvider, int moduleId)
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


    internal static (IModuleInfo first, IModuleInfo second)
        ArrangeTwoModuleInfoFromDifferentModuleScopes(this IServiceProvider globalServiceProvider, int moduleId1, int moduleId2)
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
}
