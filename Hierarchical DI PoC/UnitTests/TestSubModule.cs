using DotNetNuke.Module;
using DotNetNuke.DependencyInjection;

namespace DotNetNuke.UnitTests;
public class TestSubModule(IServiceProvider globalServiceProvider)
{
    [Fact]
    public void NormalModuleHasNoParent()
    {
        var first = ArrangeModuleServiceProvider(101).GetRequiredService<IModuleInfo>();
        Null(first.ParentModule);
    }

    private IServiceProvider ArrangeModuleServiceProvider(int moduleId)
    {
        var moduleSp = globalServiceProvider
            .SetupPage(999)
            .SetupModule(moduleId);
        return moduleSp;
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
        => Equal(10001, ArrangeSubModule(moduleId, subModuleId).ModuleId);

    [Theory]
    [InlineData(101, 10001)]
    public void SubModuleHasParent(int moduleId, int subModuleId)
        => NotNull(ArrangeSubModule(moduleId, subModuleId).ParentModule);

    [Theory]
    [InlineData(101, 10001)]
    public void SubModuleParentHasCorrectId(int moduleId, int subModuleId)
        => Equal(101, ArrangeSubModule(moduleId, subModuleId).ParentModule!.ModuleId);

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
}
