namespace DotNetNuke.UnitTests.TestSubModules;
public class TestSubModule(IServiceProvider globalServiceProvider)
{

    [Theory]
    [InlineData(101, 10001)]
    public void SubModuleHasCorrectId(int moduleId, int subModuleId)
        => Equal(subModuleId, globalServiceProvider.ArrangeSubModule(moduleId, subModuleId).ModuleId);

    [Theory]
    [InlineData(101, 10001)]
    public void SubModuleHasParent(int moduleId, int subModuleId)
        => NotNull(globalServiceProvider.ArrangeSubModule(moduleId, subModuleId).ParentModule);

    [Theory]
    [InlineData(101, 10001)]
    public void SubModuleHasParentButNoGrandparent(int moduleId, int subModuleId)
        => Null(globalServiceProvider.ArrangeSubModule(moduleId, subModuleId).ParentModule!.ParentModule);

    [Theory]
    [InlineData(101, 10001)]
    public void SubModuleParentHasCorrectId(int moduleId, int subModuleId)
        => Equal(moduleId, globalServiceProvider.ArrangeSubModule(moduleId, subModuleId).ParentModule!.ModuleId);

    [Theory]
    [InlineData(101, 10001)]
    public void ModuleWithSubModuleHasOwnId(int moduleId, int subModuleId)
        => Equal(moduleId, globalServiceProvider.ArrangeModuleWithSubmodule(moduleId, subModuleId).ModuleId);

    [Theory]
    [InlineData(101, 10001)]
    public void ModuleWithSubModuleHasNoParent(int moduleId, int subModuleId)
        => Null(globalServiceProvider.ArrangeModuleWithSubmodule(moduleId, subModuleId).ParentModule);




}
