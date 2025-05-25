namespace ToSic.HierarchicalDI.Module;
internal class ModuleInfo : IModuleInfo
{
    public int ModuleId { get; set;  } = new Random().Next(); // Simulating a module ID, replace with actual logic if needed
    // You can add more properties or methods related to module information here
}
