using System.Diagnostics.CodeAnalysis;
using DotNetNuke.DependencyInjection.Scopes.Definitions;

namespace DotNetNuke.DependencyInjection.Scopes.Accessors;

/// <inheritdoc />
internal class ServiceScopeAccessor<TScopeDefinition>
    : IServiceScopeAccessor<TScopeDefinition>
    where TScopeDefinition : ScopeDefinition, new()
{
    [field: AllowNull, MaybeNull]
    public ScopeDefinition ScopeDefinition => field ??= new TScopeDefinition();

    /// <inheritdoc />
    public void Setup(IServiceProvider serviceProvider, string currentScopeName)
    {
        ServiceProvider = serviceProvider;
        CurrentScopeName = currentScopeName;
        IsInitialized = true;
    }

    /// <inheritdoc />
    [field: AllowNull, MaybeNull]
    public IServiceProvider ServiceProvider
    {
        get => field ?? throw new InvalidOperationException($"The {nameof(ServiceProvider)} was not initialized for the '{GetType().Name}'; accessing scope '{ScopeDefinition.ScopeName}'");
        private set;
    }

    /// <inheritdoc />
    public bool IsInitialized { get; private set; }

    /// <inheritdoc />
    public string AccessedScopeName => ScopeDefinition.ScopeName;

    /// <inheritdoc />
    public string CurrentScopeName { get; private set; } = ServiceScopeConstants.ScopeNotInitialized;
}