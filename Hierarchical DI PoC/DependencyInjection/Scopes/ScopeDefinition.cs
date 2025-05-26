namespace DotNetNuke.DependencyInjection.Scopes;
public abstract class ScopeDefinition
{
    public abstract string ScopeName { get; }

    //public abstract Type ScopeAccessorType { get; }
}

public class ScopeModule : ScopeDefinition
{
    public override string ScopeName => ServiceScopeConstants.ScopeModule;
    //public override Type ScopeAccessorType { get; init; }
}

public class ScopePage : ScopeDefinition
{
    public override string ScopeName => ServiceScopeConstants.ScopePage;
    //public override Type ScopeAccessorType { get; init; }
}