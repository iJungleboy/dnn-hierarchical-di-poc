namespace DotNetNuke.UnitTests.TestSubModules;
public class TestSubSubModule(IServiceProvider globalServiceProvider)
{

    [Theory]
    [InlineData(101, 10001, 20001)]
    public void SubSubModuleHasCorrectId(int moduleId, int subModuleId, int subSubModuleId)
        => Equal(subSubModuleId, globalServiceProvider.ArrangeSubSubModule(moduleId, subModuleId, subSubModuleId).ModuleId);

    [Theory]
    [InlineData(101, 10001, 20001)]
    public void SubSubModuleHasParent(int moduleId, int subModuleId, int subSubModuleId)
        => NotNull(globalServiceProvider.ArrangeSubSubModule(moduleId, subModuleId, subSubModuleId).ParentModule);

    [Theory]
    [InlineData(101, 10001, 20001)]
    public void SubSubModuleHasParentParent(int moduleId, int subModuleId, int subSubModuleId)
        => NotNull(globalServiceProvider.ArrangeSubSubModule(moduleId, subModuleId, subSubModuleId).ParentModule!.ParentModule);

    [Theory]
    [InlineData(101, 10001, 20001)]
    public void SubSubModuleHasParentParentButNoGrandparent(int moduleId, int subModuleId, int subSubModuleId)
        => Null(globalServiceProvider.ArrangeSubSubModule(moduleId, subModuleId, subSubModuleId).ParentModule!.ParentModule!.ParentModule);

    [Theory]
    [InlineData(101, 10001, 20001)]
    public void SubSubModuleParentHasCorrectId(int moduleId, int subModuleId, int subSubModuleId)
        => Equal(subModuleId, globalServiceProvider.ArrangeSubSubModule(moduleId, subModuleId, subSubModuleId).ParentModule!.ModuleId);

    [Theory]
    [InlineData(101, 10001, 20001)]
    public void SubSubModuleParentParentHasCorrectId(int moduleId, int subModuleId, int subSubModuleId)
        => Equal(moduleId, globalServiceProvider.ArrangeSubSubModule(moduleId, subModuleId, subSubModuleId).ParentModule!.ParentModule!.ModuleId);

}
