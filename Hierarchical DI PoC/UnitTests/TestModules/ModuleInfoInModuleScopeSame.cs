namespace DotNetNuke.UnitTests.TestModules;
public class ModuleInfoInModuleScopeSame(IServiceProvider globalServiceProvider)
{

    [Theory]
    [InlineData(101)]
    public void ModuleInfoHaveSameId(int moduleId)
    {
        var (first, second) = globalServiceProvider.ArrangeTwoModuleInfoFromSameModuleScope(moduleId);
        Equal(moduleId, first.ModuleId);
        Equal(moduleId, second.ModuleId);
    }

    [Theory]
    [InlineData(101)]
    public void ModuleInfoNotSame(int moduleId)
    {
        var (first, second) = globalServiceProvider.ArrangeTwoModuleInfoFromSameModuleScope(moduleId);
        NotSame(first, second);
    }

}
