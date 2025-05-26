using DotNetNuke.DependencyInjection.Scopes.Accessors;

namespace DotNetNuke.DependencyInjection.Scopes;

/// <summary>
/// Provide service which was generated within the page scope.
/// </summary>
/// <typeparam name="T"></typeparam>
internal class PageScopedService<T>(IServiceScopeAccessor<ScopePage> pageScopeAccessor) : IPageScopedService<T> where T : class
{
    public T Value => pageScopeAccessor.ServiceProvider.GetRequiredService<T>();
}