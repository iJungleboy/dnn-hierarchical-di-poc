namespace DotNetNuke.DependencyInjection.Scopes.Initializer;
internal class CurrentScopeInitializer(IServiceProvider currentServiceProvider)
{
    public List<IScopeInitializer> Initializers { get; private set; } = [];

    public void AddInitializer<TInitializer>() where TInitializer : IServiceScopeAccessor
    {
        var initializer = currentServiceProvider.GetRequiredService<IScopeInitializer<TInitializer>>();
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
