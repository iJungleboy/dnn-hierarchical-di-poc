namespace DotNetNuke.Module;

/// <summary>
/// Simulate the setup of a page info
/// </summary>
internal class ModuleInfoInitializerService(ModuleInfoReal pageInfo)
{
    public void SetupCurrentModule(int moduleId)
    {
        pageInfo.ModuleId = moduleId;
    }
}
