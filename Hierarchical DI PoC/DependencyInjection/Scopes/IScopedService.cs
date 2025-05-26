namespace DotNetNuke.DependencyInjection.Scopes;

internal interface IScopedService<TScopeDefinition, out T>
    where T : class
    where TScopeDefinition : ScopeDefinition, new()
{
    T Value { get; }
}