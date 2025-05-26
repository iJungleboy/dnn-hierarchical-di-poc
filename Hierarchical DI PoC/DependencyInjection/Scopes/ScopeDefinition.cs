namespace DotNetNuke.DependencyInjection.Scopes;
public abstract record ScopeDefinition(string ScopeName);

public record ScopeRoot() : ScopeDefinition(ServiceScopeConstants.ScopeRoot);

public record ScopePage() : ScopeDefinition(ServiceScopeConstants.ScopePage);

public record ScopeModule() : ScopeDefinition(ServiceScopeConstants.ScopeModule);

