using System.Diagnostics.CodeAnalysis;

namespace DotNetNuke.DependencyInjection.Scopes.Definitions;
internal record CurrentScopeDefinition : ICurrentScopeDefinition
{
    [field: AllowNull, MaybeNull]
    public /* actually internal */ ScopeDefinition Definition
    {
        get => field ??= new ScopeRoot();
        set;
    }

    public string ScopeName => Definition.ScopeName;

    public bool RestartsScopeWip { get; /* actually internal */ set; }
}
