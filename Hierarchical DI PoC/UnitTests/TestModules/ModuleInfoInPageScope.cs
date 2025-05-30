﻿using DotNetNuke.DependencyInjection;
using DotNetNuke.Module;

namespace DotNetNuke.UnitTests.TestModules;
public class ModuleInfoInPageScope(IServiceProvider globalServiceProvider)
{
    /// <summary>
    /// Assert that we cannot get a module info in the page scope.
    /// This is to avoid placing page scoped services in the global scope,
    /// </summary>
    [Fact]
    public void ModuleInfoInPageScopeNotAllowed()
    {
        // Create a page scope and prepare shared page context
        var pageSp = globalServiceProvider.SetupPage(21);

        var pageLevelModuleInfo = pageSp.GetRequiredService<IModuleInfo>();
        Throws<InvalidOperationException>(() => pageLevelModuleInfo.ModuleId);
    }

}
