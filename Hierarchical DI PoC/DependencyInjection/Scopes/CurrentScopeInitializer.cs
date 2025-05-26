using Microsoft.Extensions.DependencyInjection;

namespace DotNetNuke.DependencyInjection.Scopes;
internal class CurrentScopeInitializer(IServiceProvider currentServiceProvider)
{
    public List<IScopeInitializer> Initializers { get; private set; } = [];

    public void Add(IScopeInitializer initializer)
    {
        if (initializer == null)
            throw new ArgumentNullException(nameof(initializer), "Initializer cannot be null.");
        Initializers.Add(initializer);
    }

    public void Add<TInitializer>() where TInitializer : IServiceScopeAccessor
    {
        var initializer = currentServiceProvider.GetRequiredService<IScopeInitializer<TInitializer>>();
        Add(initializer);
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
