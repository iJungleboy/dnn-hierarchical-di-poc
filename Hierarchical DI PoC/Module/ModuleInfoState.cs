namespace DotNetNuke.Module;
internal class ModuleInfoState : IModuleInfo
{
    public int ModuleId { get; set; } = -1; // Simulating a module ID, replace with actual logic if needed
    // You can add more properties or methods related to module information here

    public IModuleInfo? ParentModule { get; set; }
}
