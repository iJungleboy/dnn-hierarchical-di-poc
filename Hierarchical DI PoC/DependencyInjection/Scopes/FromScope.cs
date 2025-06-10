using DotNetNuke.DependencyInjection.Scopes.Accessors;

namespace DotNetNuke.DependencyInjection.Scopes;

/// <inheritdoc />
internal class FromScope<TScopeDefinition, TService>(IServiceScopeAccessor<TScopeDefinition> moduleScopeAccessor)
    : IFromScope<TScopeDefinition, TService>
    where TService : class
    where TScopeDefinition : ScopeDefinition, new()
{
    public TService Value => moduleScopeAccessor.ServiceProvider.GetRequiredService<TService>();
}