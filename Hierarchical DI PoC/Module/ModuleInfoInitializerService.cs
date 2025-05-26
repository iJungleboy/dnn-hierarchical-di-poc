namespace DotNetNuke.Module;

/// <summary>
/// Simulate the setup of a page info
/// </summary>
internal class ModuleInfoInitializerService(ModuleInfoReal moduleInfo)
{
    public void SetupCurrentModule(int moduleId)
    {
        moduleInfo.ModuleId = moduleId;
    }
}
