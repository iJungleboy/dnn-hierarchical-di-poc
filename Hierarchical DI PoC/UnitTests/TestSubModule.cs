using DotNetNuke.Module;
using DotNetNuke.DependencyInjection;

namespace DotNetNuke.UnitTests;
public class TestSubModule(IServiceProvider globalServiceProvider)
{
    [Fact]
    public void NormalModuleHasNoParent()
    {
        var moduleSp = globalServiceProvider
            .SetupPage(999)
            .SetupModule(101);

        var first = moduleSp.GetRequiredService<IModuleInfo>();
        Null(first.ParentModule);
    }

    private IServiceProvider ArrangeSubModule(int moduleId, int subModuleId)
    {
        var moduleSp = globalServiceProvider
            .SetupPage(999)
            .SetupModule(moduleId)
            .SetupModuleInModule(subModuleId);
        return moduleSp;
    }

    [Theory]
    [InlineData(101, 10001)]
    public void SubModuleHasParent(int moduleId, int subModuleId)
    {
        var moduleSp = ArrangeSubModule(moduleId, subModuleId);
        var first = moduleSp.GetRequiredService<IModuleInfo>();
        Equal(10001, first.ModuleId);
        NotNull(first.ParentModule);
        Equal(101, first.ParentModule.ModuleId);
    }
}
