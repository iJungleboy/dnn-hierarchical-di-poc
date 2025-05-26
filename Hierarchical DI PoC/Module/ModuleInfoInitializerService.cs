namespace DotNetNuke.Module;

/// <summary>
/// Simulate the setup of a page info
/// </summary>
internal class ModuleInfoInitializerService(ModuleInfoState moduleInfo)
{
    public void SetupCurrentModule(int moduleId)
    {
        moduleInfo.ModuleId = moduleId;
    }
    
    public void SetupSubmodule(int mainModuleId, IModuleInfo parentModule)
    {
        SetupCurrentModule(mainModuleId);
        moduleInfo.ParentModule = parentModule;
    }
}
