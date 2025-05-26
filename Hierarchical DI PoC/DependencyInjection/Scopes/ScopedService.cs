using DotNetNuke.DependencyInjection.Scopes.Accessors;

namespace DotNetNuke.DependencyInjection.Scopes;

/// <summary>
/// Provide service from the current or some parent scope.
/// </summary>
/// <typeparam name="T">The service type to retrieve, must be registered in DI.</typeparam>
/// <typeparam name="TScopeDefinition">The definition class for this scope.</typeparam>
internal class ScopedService<TScopeDefinition, T>(IServiceScopeAccessor<TScopeDefinition> moduleScopeAccessor)
    : IScopedService<TScopeDefinition, T> where T : class
    where TScopeDefinition : ScopeDefinition, new()
{
    public T Value => moduleScopeAccessor.ServiceProvider.GetRequiredService<T>();
}