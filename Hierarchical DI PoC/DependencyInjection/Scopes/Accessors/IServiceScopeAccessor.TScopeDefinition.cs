namespace DotNetNuke.DependencyInjection.Scopes.Accessors;

public interface IServiceScopeAccessor<TScopeDefinition>
    : IServiceScopeAccessor
    where TScopeDefinition : ScopeDefinition, new();
