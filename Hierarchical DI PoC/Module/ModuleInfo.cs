using DotNetNuke.DependencyInjection.Scopes;

namespace DotNetNuke.Module;
internal class ModuleInfo(IModuleScopedService<ModuleInfoReal> realModuleInfo) : IModuleInfo
{
    public int ModuleId => realModuleInfo.Value.ModuleId; // Simulating a module ID, replace with actual logic if needed
    // You can add more properties or methods related to module information here
}
