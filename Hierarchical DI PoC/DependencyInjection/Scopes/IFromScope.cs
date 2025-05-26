namespace DotNetNuke.DependencyInjection.Scopes;

internal interface IFromScope<TScopeDefinition, out T>
    where T : class
    where TScopeDefinition : ScopeDefinition, new()
{
    T Value { get; }
}