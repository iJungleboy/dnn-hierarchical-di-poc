using DotNetNuke.Module;
using DotNetNuke.DependencyInjection;

namespace DotNetNuke.UnitTests;
public class TestSubModule(IServiceProvider globalServiceProvider)
{
    private IServiceProvider ArrangeModuleServiceProvider(int moduleId)
    {
        var moduleSp = globalServiceProvider
            .SetupPage(999)
            .SetupModule(moduleId);
        return moduleSp;
    }

    [Fact]
    public void NormalModuleHasNoParent()
    {
        var first = ArrangeModuleServiceProvider(101).GetRequiredService<IModuleInfo>();
        Null(first.ParentModule);
    }

    private (IServiceProvider MainSp, IServiceProvider SubSp) ArrangeSubModuleServiceProviders(int moduleId, int subModuleId)
    {
        var moduleSp = ArrangeModuleServiceProvider(moduleId);
        var subModuleSp = moduleSp
            .SetupModuleInModule(subModuleId);
        return (moduleSp, subModuleSp);
    }

    private IModuleInfo ArrangeSubModule(int moduleId, int subModuleId)
    {
        var submoduleServiceProvider = ArrangeSubModuleServiceProviders(moduleId, subModuleId).SubSp;
        return submoduleServiceProvider.GetRequiredService<IModuleInfo>();
    }

    [Theory]
    [InlineData(101, 10001)]
    public void SubModuleHasCorrectId(int moduleId, int subModuleId)
        => Equal(subModuleId, ArrangeSubModule(moduleId, subModuleId).ModuleId);

    [Theory]
    [InlineData(101, 10001)]
    public void SubModuleHasParent(int moduleId, int subModuleId)
        => NotNull(ArrangeSubModule(moduleId, subModuleId).ParentModule);

    [Theory]
    [InlineData(101, 10001)]
    public void SubModuleHasParentButNoGrandparent(int moduleId, int subModuleId)
        => Null(ArrangeSubModule(moduleId, subModuleId).ParentModule!.ParentModule);

    [Theory]
    [InlineData(101, 10001)]
    public void SubModuleParentHasCorrectId(int moduleId, int subModuleId)
        => Equal(moduleId, ArrangeSubModule(moduleId, subModuleId).ParentModule!.ModuleId);

    private IModuleInfo ArrangeModuleWithSubmodule(int moduleId, int subModuleId)
    {
        var submoduleServiceProvider = ArrangeSubModuleServiceProviders(moduleId, subModuleId).MainSp;
        return submoduleServiceProvider.GetRequiredService<IModuleInfo>();
    }

    [Theory]
    [InlineData(101, 10001)]
    public void ModuleWithSubModuleHasOwnId(int moduleId, int subModuleId)
        => Equal(moduleId, ArrangeModuleWithSubmodule(moduleId, subModuleId).ModuleId);

    [Theory]
    [InlineData(101, 10001)]
    public void ModuleWithSubModuleHasNoParent(int moduleId, int subModuleId)
        => Null(ArrangeModuleWithSubmodule(moduleId, subModuleId).ParentModule);

    private (IServiceProvider MainSp, IServiceProvider SubSp, IServiceProvider SubSubSp)
        ArrangeSubSubModuleServiceProviders(int moduleId, int subModuleId, int subSubModuleId)
    {
        var moduleSp = ArrangeModuleServiceProvider(moduleId);
        var subModuleSp = moduleSp
            .SetupModuleInModule(subModuleId);
        var subSubModuleSp = subModuleSp
            .SetupModuleInModule(subSubModuleId);
        return (moduleSp, subModuleSp, subSubModuleSp);
    }

    private IModuleInfo ArrangeSubSubModule(int moduleId, int subModuleId, int subSubModuleId)
    {
        var submoduleServiceProvider = ArrangeSubSubModuleServiceProviders(moduleId, subModuleId, subSubModuleId).SubSubSp;
        return submoduleServiceProvider.GetRequiredService<IModuleInfo>();
    }


    [Theory]
    [InlineData(101, 10001, 20001)]
    public void SubSubModuleHasCorrectId(int moduleId, int subModuleId, int subSubModuleId)
        => Equal(subSubModuleId, ArrangeSubSubModule(moduleId, subModuleId, subSubModuleId).ModuleId);

    [Theory]
    [InlineData(101, 10001, 20001)]
    public void SubSubModuleHasParent(int moduleId, int subModuleId, int subSubModuleId)
        => NotNull(ArrangeSubSubModule(moduleId, subModuleId, subSubModuleId).ParentModule);

    [Theory]
    [InlineData(101, 10001, 20001)]
    public void SubSubModuleHasParentParent(int moduleId, int subModuleId, int subSubModuleId)
        => NotNull(ArrangeSubSubModule(moduleId, subModuleId, subSubModuleId).ParentModule!.ParentModule);

    [Theory]
    [InlineData(101, 10001, 20001)]
    public void SubSubModuleHasParentParentButNoGrandparent(int moduleId, int subModuleId, int subSubModuleId)
        => Null(ArrangeSubSubModule(moduleId, subModuleId, subSubModuleId).ParentModule!.ParentModule!.ParentModule);

    [Theory]
    [InlineData(101, 10001, 20001)]
    public void SubSubModuleParentHasCorrectId(int moduleId, int subModuleId, int subSubModuleId)
        => Equal(subModuleId, ArrangeSubSubModule(moduleId, subModuleId, subSubModuleId).ParentModule!.ModuleId);

    [Theory]
    [InlineData(101, 10001, 20001)]
    public void SubSubModuleParentParentHasCorrectId(int moduleId, int subModuleId, int subSubModuleId)
        => Equal(moduleId, ArrangeSubSubModule(moduleId, subModuleId, subSubModuleId).ParentModule!.ParentModule!.ModuleId);

}
