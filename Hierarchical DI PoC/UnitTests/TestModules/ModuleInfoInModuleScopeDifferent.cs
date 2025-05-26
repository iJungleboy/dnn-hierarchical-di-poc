namespace DotNetNuke.UnitTests.TestModules;
public class ModuleInfoInModuleScopeDifferent(IServiceProvider globalServiceProvider)
{

    [Theory]
    [InlineData(101, 102)]
    public void ModuleInfoAreSeparate(int moduleId1, int moduleId2)
    {
        // Arrange
        var (first, second) = globalServiceProvider.ArrangeTwoModuleInfoFromDifferentModuleScopes(moduleId1, moduleId2);

        // Assert that the modules in different scopes are not the same
        NotSame(first, second);
        NotEqual(first.ModuleId, second.ModuleId);
    }

    [Theory]
    [InlineData(101, 102)]
    public void ModuleInfoKeepOwnValue(int moduleId1, int moduleId2)
    {
        // Arrange
        var (first, second) = globalServiceProvider.ArrangeTwoModuleInfoFromDifferentModuleScopes(moduleId1, moduleId2);

        // Assert that the modules in different scopes are not the same
        Equal(moduleId1, first.ModuleId);
        Equal(moduleId2, second.ModuleId);
        NotEqual(first.ModuleId, second.ModuleId);
    }

    [Theory]
    [InlineData(101, 102)]
    public void ModuleInfoAreNotSame(int moduleId1, int moduleId2)
    {
        var (first, second) = globalServiceProvider.ArrangeTwoModuleInfoFromDifferentModuleScopes(moduleId1, moduleId2);
        NotSame(first, second);
    }
}
