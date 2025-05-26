namespace DotNetNuke.Module;

internal interface IModuleInfo
{
    int ModuleId { get; } // Simulating a module ID, replace with actual logic if needed
    IModuleInfo? ParentModule { get; } // => realModuleInfo.Parent;
}