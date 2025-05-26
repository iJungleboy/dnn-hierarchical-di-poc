namespace DotNetNuke.Module;
internal class ModuleInfo(ModuleInfoReal realModuleInfo) : IModuleInfo
{
    public int ModuleId => realModuleInfo.ModuleId; // Simulating a module ID, replace with actual logic if needed
    // You can add more properties or methods related to module information here
}
