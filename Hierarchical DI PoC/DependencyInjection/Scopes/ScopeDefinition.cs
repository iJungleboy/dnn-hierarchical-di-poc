namespace DotNetNuke.DependencyInjection.Scopes;

/// <summary>
/// A scope definition, which is used to identify the scope type and its name.
/// </summary>
/// <param name="ScopeName"></param>
/// <remarks>
/// These objects are used to differentiate between different scopes in the dependency injection system, such as root, page, and module scopes.
/// </remarks>
public abstract record ScopeDefinition(string ScopeName);

public record ScopeRoot() : ScopeDefinition(ServiceScopeConstants.ScopeRoot);

public record ScopePage() : ScopeDefinition(ServiceScopeConstants.ScopePage);

public record ScopeModule() : ScopeDefinition(ServiceScopeConstants.ScopeModule);

