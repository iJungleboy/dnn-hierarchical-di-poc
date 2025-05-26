using DotNetNuke.DependencyInjection.Scopes.Accessors;

namespace DotNetNuke.DependencyInjection.Scopes;

/// <summary>
/// Provide service from the current or some parent scope.
/// </summary>
internal class FromScope<TScopeDefinition, T>(IServiceScopeAccessor<TScopeDefinition> moduleScopeAccessor)
    : IFromScope<TScopeDefinition, T> where T : class
    where TScopeDefinition : ScopeDefinition, new()
{
    public T Value => moduleScopeAccessor.ServiceProvider.GetRequiredService<T>();
}