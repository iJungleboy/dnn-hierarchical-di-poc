using DotNetNuke.Module;

namespace DotNetNuke.UnitTests.TestSubModules;
public class TestNoSubmodule(IServiceProvider globalServiceProvider)
{
    
    [Fact]
    public void NormalModuleHasNoParent()
    {
        var first = globalServiceProvider
            .ArrangeModuleServiceProvider(101)
            .GetRequiredService<IModuleInfo>();
        Null(first.ParentModule);
    }

}
