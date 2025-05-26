namespace DotNetNuke.DependencyInjection.Scopes;
public abstract class ScopeDefinition
{
    public abstract string ScopeName { get; }
}

public class ScopeModule : ScopeDefinition
{
    public override string ScopeName => ServiceScopeConstants.ScopeModule;
}

public class ScopePage : ScopeDefinition
{
    public override string ScopeName => ServiceScopeConstants.ScopePage;
}