using Microsoft.Extensions.DependencyInjection;

namespace DotNetNuke.DependencyInjection.PageScope;

/// <summary>
/// Provide service which was generated within the page scope.
/// </summary>
/// <typeparam name="T"></typeparam>
internal class PageScopedService<T>(IPageScopeAccessor pageScopeAccessor) : IPageScopedService<T> where T : class
{
    public T Value => pageScopeAccessor.ServiceProvider.GetRequiredService<T>();
}