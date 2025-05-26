using System.Diagnostics.CodeAnalysis;

namespace DotNetNuke.DependencyInjection.Scopes.Accessors;

/// <summary>
/// Special helper to get a ServiceProvider of a specified scope, in scenarios where child scopes need to access the parents scope's services, like for each module or content block.
/// </summary>
internal class ServiceScopeAccessor<TScopeDefinition>
    : IServiceScopeAccessor<TScopeDefinition>
    where TScopeDefinition : ScopeDefinition, new()
{
    [field: AllowNull, MaybeNull]
    public ScopeDefinition ScopeDefinition => field ??= new TScopeDefinition();

    public void SetupServiceProvider(IServiceProvider serviceProvider, string currentScopeName)
    {
        ServiceProvider = serviceProvider;
        CurrentScopeName = currentScopeName;
        IsValid = true;
    }

    /// <inheritdoc />
    [field: AllowNull, MaybeNull]
    public IServiceProvider ServiceProvider
    {
        get => field ?? throw new InvalidOperationException($"The {nameof(ServiceProvider)} was not initialized for the '{nameof(IServiceScopeAccessor)}' accessing scope '{ScopeDefinition.ScopeName}'");
        private set;
    }

    public bool IsValid { get; private set; }

    public string AccessedScopeName => ScopeDefinition.ScopeName;

    public string CurrentScopeName { get; private set; } = ServiceScopeConstants.ScopeNotInitialized;

}