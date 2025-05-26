using DotNetNuke.Module;

namespace DotNetNuke.UnitTests.TestModules;
public class ModuleInfoInGlobalScope(IServiceProvider globalServiceProvider)
{
    /// <summary>
    /// Assert that we cannot get a page info in the global scope.
    /// This is to avoid placing page scoped services in the global scope,
    /// </summary>
    [Fact]
    public void ModuleInfoInGlobalScopeNotAllowed()
    {
        var globalPageInfo = globalServiceProvider.GetRequiredService<IModuleInfo>();
        Throws<InvalidOperationException>(() => globalPageInfo.ModuleId);
    }

}
