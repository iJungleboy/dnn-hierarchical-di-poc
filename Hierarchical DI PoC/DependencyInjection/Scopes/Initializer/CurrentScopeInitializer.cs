namespace DotNetNuke.DependencyInjection.Scopes.Initializer;
internal class CurrentScopeInitializer(IServiceProvider currentServiceProvider)
{
    public List<IScopeAccessorInitializer> Initializers { get; private set; } = [];

    public void AddInitializer<TScopeDefinition>(bool startFreshScope = false)
        where TScopeDefinition : ScopeDefinition, new()
    {
        var initializer = currentServiceProvider.GetRequiredService<IScopeAccessorInitializer<TScopeDefinition>>();
        if (startFreshScope)
            initializer.StartFreshScope = true;
        Initializers.Add(initializer);
    }

    public void Run(string scopeName, IServiceProvider parentServiceProvider)
    {
        // Check if parent scope has a list of initializers, if so, get them and run them first
        var parentScopeInitializer = parentServiceProvider.GetRequiredService<CurrentScopeInitializer>();
        Initializers = [.. parentScopeInitializer.Initializers, ..Initializers];

        foreach (var initializer in Initializers)
            initializer.Run(scopeName, currentServiceProvider, parentServiceProvider);
    }
}
