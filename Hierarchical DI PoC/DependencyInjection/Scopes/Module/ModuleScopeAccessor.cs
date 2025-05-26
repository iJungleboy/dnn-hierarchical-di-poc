namespace DotNetNuke.DependencyInjection.Scopes;

/// <summary>
/// Special helper to get a ServiceProvider of the page scope, in scenarios where each module has an own scope. 
/// </summary>
/// <remarks>
/// Default constructor will always work, and use the current service provider as the source
/// </remarks>
internal class ModuleScopeAccessor()
    : ServiceScopeAccessor(ServiceScopeConstants.ScopeModule),
        IModuleScopeAccessor;