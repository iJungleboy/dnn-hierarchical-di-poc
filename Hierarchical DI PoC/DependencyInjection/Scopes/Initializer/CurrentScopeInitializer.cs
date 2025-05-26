namespace DotNetNuke.DependencyInjection.Scopes.Initializer;
internal class CurrentScopeInitializer(IServiceProvider currentServiceProvider)
{
    public List<IScopeAccessorInitializer> Initializers { get; private set; } = [];

    public void SetupFromParent(IServiceProvider parentServiceProvider)
    {
        if (_parentInitializersCloned)
            return;
        // Check if parent scope has a list of initializers, if so, get them and run them first
        var parentScopeInitializer = parentServiceProvider.GetRequiredService<CurrentScopeInitializer>();
        Initializers = [.. parentScopeInitializer.Initializers, ..Initializers];
        _parentInitializersCloned = true;
    }

    private bool _parentInitializersCloned;

    public void AddInitializer<TScopeDefinition>(bool shouldNotInheritState)
        where TScopeDefinition : ScopeDefinition, new()
    {
        var initializer = currentServiceProvider.GetRequiredService<IScopeAccessorInitializer<TScopeDefinition>>();
        initializer.ShouldNotInheritState = shouldNotInheritState;

        // Replace the existing initializer for this type, if it exists; at the same place in the list
        var index = Initializers.FindIndex(i => i is IScopeAccessorInitializer<TScopeDefinition>);
        if (index >= 0)
            Initializers[index] = initializer;
        else
            Initializers.Add(initializer);
    }

    public void Run(string scopeName, IServiceProvider parentServiceProvider)
    {
        foreach (var initializer in Initializers)
            initializer.Run(scopeName, currentServiceProvider, parentServiceProvider);
    }

}
