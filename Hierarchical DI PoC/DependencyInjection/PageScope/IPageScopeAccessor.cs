namespace ToSic.HierarchicalDI.DependencyInjection.PageScope;

/// <summary>
/// Special helper to get a ServiceProvider of the page scope, in scenarios where inner scopes are used, like for each module.
/// </summary>
public interface IPageScopeAccessor
{
    internal void AttachPageServiceProvider(IServiceProvider pageServiceProvider);

    /// <summary>
    /// The ServiceProvider.
    /// When in the page scope, it's the default service provider of the page scope.
    /// When in a deeper scope (Module, Content-Block, etc.) it's the page scoped service provider.
    /// </summary>
    IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Determines if this page-scope accessor is from the PageDI or from the Module
    /// More for internal use, in case we have trouble debugging
    /// </summary>
    bool ProvidedInModule { get; }
}