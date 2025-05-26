namespace DotNetNuke.DependencyInjection.Scopes;
public abstract class ScopeDefinition(string scopeName)
{
    public string ScopeName => scopeName;
}

public class ScopeRoot() : ScopeDefinition(ServiceScopeConstants.ScopeRoot);

public class ScopePage() : ScopeDefinition(ServiceScopeConstants.ScopePage);

public class ScopeModule() : ScopeDefinition(ServiceScopeConstants.ScopeModule);

