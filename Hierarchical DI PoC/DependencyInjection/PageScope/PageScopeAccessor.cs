using System.Diagnostics.CodeAnalysis;

namespace DotNetNuke.DependencyInjection.PageScope;

/// <summary>
/// Special helper to get a ServiceProvider of the page scope, in scenarios where each module has an own scope. 
/// </summary>
/// <remarks>
/// Default constructor will always work, and use the current service provider as the source
/// </remarks>
internal class PageScopeAccessor : IPageScopeAccessor
{
    public void AttachPageScopedServiceProvider(IServiceProvider pageServiceProvider, string scopeName)
    {
        ServiceProvider = pageServiceProvider;
        CurrentScopeName = scopeName;
    }

    /// <inheritdoc />
    [field: AllowNull, MaybeNull]
    public IServiceProvider ServiceProvider
    {
        get => field ?? throw new InvalidOperationException($"The {nameof(ServiceProvider)} was not initialized for the {nameof(IPageScopeAccessor)}");
        private set;
    }

    public string AccessedScopeName => "page";

    public string CurrentScopeName { get; private set; } = "unknown";

}