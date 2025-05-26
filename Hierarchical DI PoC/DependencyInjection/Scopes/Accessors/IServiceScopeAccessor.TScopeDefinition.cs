namespace DotNetNuke.DependencyInjection.Scopes.Accessors;
public interface IServiceScopeAccessor<TScopeDefinition>
    where TScopeDefinition : ScopeDefinition, new();
