using Microsoft.Extensions.DependencyInjection;
using ToSic.HierarchicalDI.DiBridges;

namespace ToSic.Sxc.Internal.Plumbing;

/// <summary>
/// Provide service which was generated within the page scope.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PageScopedService<T>(PageScopeAccessor pageScopeAccessor) where T : class
{
    public T Value => pageScopeAccessor.PageServiceProvider.GetRequiredService<T>();
}