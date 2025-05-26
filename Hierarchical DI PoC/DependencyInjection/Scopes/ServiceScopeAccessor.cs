using System.Diagnostics.CodeAnalysis;

namespace DotNetNuke.DependencyInjection.Scopes;

/// <summary>
/// Special helper to get a ServiceProvider of a specified scope, in scenarios where child scopes need to access the parents scope's services, like for each module or content block.
/// </summary>
internal abstract class ServiceScopeAccessor(string accessedScopeName) : IServiceScopeAccessor
{
    public void SetupServiceProvider(IServiceProvider serviceProvider, string currentScopeName)
    {
        ServiceProvider = serviceProvider;
        CurrentScopeName = currentScopeName;
    }

    /// <inheritdoc />
    [field: AllowNull, MaybeNull]
    public IServiceProvider ServiceProvider
    {
        get => field ?? throw new InvalidOperationException($"The {nameof(ServiceProvider)} was not initialized for the '{nameof(IServiceScopeAccessor)}' accessing scope '{AccessedScopeName}'");
        private set;
    }

    public virtual string AccessedScopeName => accessedScopeName;

    public string CurrentScopeName { get; private set; } = "unknown";

}