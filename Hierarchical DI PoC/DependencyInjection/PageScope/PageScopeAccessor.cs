using System.Diagnostics.CodeAnalysis;

namespace ToSic.HierarchicalDI.DependencyInjection.PageScope;

/// <summary>
/// Special helper to get a ServiceProvider of the page scope, in scenarios where each module has an own scope. 
/// </summary>
/// <remarks>
/// Default constructor will always work, and use the current service provider as the source
/// </remarks>
internal class PageScopeAccessor(IServiceProvider currentServiceProvider) : IPageScopeAccessor
{
    public void AttachPageServiceProvider(IServiceProvider pageServiceProvider)
    {
        ServiceProvider = pageServiceProvider;
        ProvidedInModule = true;
    }

    /// <inheritdoc />
    [field: AllowNull, MaybeNull]
    public IServiceProvider ServiceProvider
    {
        get => field ?? currentServiceProvider;
        private set;
    }

    /// <inheritdoc />
    public bool ProvidedInModule { get; private set; }

}