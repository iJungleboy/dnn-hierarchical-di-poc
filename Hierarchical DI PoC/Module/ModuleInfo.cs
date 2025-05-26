namespace DotNetNuke.Module;
internal class ModuleInfo(IFromScope<ScopeModule, ModuleInfoState> realModuleInfo) : IModuleInfo
{
    public int ModuleId => realModuleInfo.Value.ModuleId; // Simulating a module ID, replace with actual logic if needed
    // You can add more properties or methods related to module information here

    public IModuleInfo? ParentModule => realModuleInfo.Value.ParentModule;
}
