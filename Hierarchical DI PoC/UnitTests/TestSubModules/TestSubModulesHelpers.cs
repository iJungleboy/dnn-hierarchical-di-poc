using DotNetNuke.DependencyInjection;
using DotNetNuke.Module;

namespace DotNetNuke.UnitTests.TestSubModules;
internal static class TestSubModulesHelpers
{
    internal static IServiceProvider ArrangeModuleServiceProvider(this IServiceProvider globalServiceProvider, int moduleId)
    {
        var moduleSp = globalServiceProvider
            .SetupPage(999)
            .SetupModule(moduleId);
        return moduleSp;
    }

    internal static (IServiceProvider MainSp, IServiceProvider SubSp)
        ArrangeSubModuleServiceProviders(this IServiceProvider globalServiceProvider, int moduleId, int subModuleId)
    {
        var moduleSp = globalServiceProvider
            .ArrangeModuleServiceProvider(moduleId);
        var subModuleSp = moduleSp
            .SetupModuleInModule(subModuleId);
        return (moduleSp, subModuleSp);
    }

    internal static IModuleInfo ArrangeSubModule(this IServiceProvider globalServiceProvider, int moduleId, int subModuleId)
    {
        var submoduleServiceProvider = globalServiceProvider.ArrangeSubModuleServiceProviders(moduleId, subModuleId).SubSp;
        return submoduleServiceProvider.GetRequiredService<IModuleInfo>();
    }

    internal static IModuleInfo ArrangeModuleWithSubmodule(this IServiceProvider globalServiceProvider, int moduleId, int subModuleId)
    {
        var submoduleServiceProvider = globalServiceProvider.ArrangeSubModuleServiceProviders(moduleId, subModuleId).MainSp;
        return submoduleServiceProvider.GetRequiredService<IModuleInfo>();
    }

    internal static (IServiceProvider MainSp, IServiceProvider SubSp, IServiceProvider SubSubSp)
        ArrangeSubSubModuleServiceProviders(this IServiceProvider globalServiceProvider, int moduleId, int subModuleId, int subSubModuleId)
    {
        var moduleSp = globalServiceProvider
            .ArrangeModuleServiceProvider(moduleId);
        var subModuleSp = moduleSp
            .SetupModuleInModule(subModuleId);
        var subSubModuleSp = subModuleSp
            .SetupModuleInModule(subSubModuleId);
        return (moduleSp, subModuleSp, subSubModuleSp);
    }

    internal static IModuleInfo ArrangeSubSubModule(this IServiceProvider globalServiceProvider, int moduleId, int subModuleId, int subSubModuleId)
    {
        var submoduleServiceProvider = globalServiceProvider.ArrangeSubSubModuleServiceProviders(moduleId, subModuleId, subSubModuleId).SubSubSp;
        return submoduleServiceProvider.GetRequiredService<IModuleInfo>();
    }

}
