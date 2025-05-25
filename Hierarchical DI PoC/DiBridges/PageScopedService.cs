using Microsoft.Extensions.DependencyInjection;

namespace ToSic.HierarchicalDI.DiBridges;

/// <summary>
/// Provide service which was generated within the page scope.
/// </summary>
/// <typeparam name="T"></typeparam>
public class PageScopedService<T>(PageScopeAccessor pageScopeAccessor) : IPageScopedService<T> where T : class
{
    public T Value => pageScopeAccessor.PageServiceProvider.GetRequiredService<T>();
}