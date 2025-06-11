namespace DotNetNuke.DependencyInjection.Scopes.Definitions;

public interface ICurrentScopeDefinition
{
    internal ScopeDefinition Definition { get; set; }

    string ScopeName { get; }

    bool RestartsScopeWip { get; internal set; }
}