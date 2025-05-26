using DotNetNuke.DependencyInjection.Scopes.Accessors;

namespace DotNetNuke.DependencyInjection.Scopes;

/// <summary>
/// Provide service which was generated within the page scope.
/// </summary>
/// <typeparam name="T"></typeparam>
/// <typeparam name="TScopeDefinition"></typeparam>
internal class ScopedService<TScopeDefinition, T>(IServiceScopeAccessor<TScopeDefinition> moduleScopeAccessor)
    : IScopedService<TScopeDefinition, T> where T : class
    where TScopeDefinition : ScopeDefinition, new()
{
    public T Value => moduleScopeAccessor.ServiceProvider.GetRequiredService<T>();
}