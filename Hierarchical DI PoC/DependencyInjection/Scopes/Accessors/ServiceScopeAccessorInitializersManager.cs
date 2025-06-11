using DotNetNuke.DependencyInjection.Scopes.Definitions;

namespace DotNetNuke.DependencyInjection.Scopes.Accessors;

/// <summary>
/// This will coordinate and run initializers for the new (current) scope.
/// </summary>
/// <remarks>
/// This is a scoped service, so all registered initializer will be available for use again later on.
/// </remarks>
/// <param name="currentServiceProvider"></param>
internal class ServiceScopeAccessorInitializersManager(IServiceProvider currentServiceProvider)
{
    /// <summary>
    /// List of all initializers that will be run for this scope.
    /// </summary>
    public List<IServiceScopeAccessorInitializer> Initializers { get; private set; } = [];

    /// <summary>
    /// Get the parent scope initializers to use in this scope as well.
    /// </summary>
    /// <param name="parentServiceProvider"></param>
    public void InheritInitializersFromParent(IServiceProvider parentServiceProvider)
    {
        // Make sure this only happens once
        if (_parentInitializersCloned)
            return;

        if (Initializers.Any())
            throw new InvalidOperationException("Cannot inherit initializers from parent scope, because the current scope already has initializers registered.");

        // Check if parent scope has a list of initializers
        var parentScopeInitializer = parentServiceProvider.GetRequiredService<ServiceScopeAccessorInitializersManager>();

        // Merge the lists; parent initializers will be run first
        Initializers = [.. parentScopeInitializer.Initializers];
        _parentInitializersCloned = true;
    }

    private bool _parentInitializersCloned;

    /// <summary>
    /// Add/Register (or replace) an initializer for this scope.
    /// </summary>
    /// <typeparam name="TScopeDefinition"></typeparam>
    /// <param name="shouldNotInheritState"></param>
    public void AddInitializer<TScopeDefinition>(bool shouldNotInheritState)
        where TScopeDefinition : ScopeDefinition, new()
    {
        var initializer = currentServiceProvider.GetRequiredService<IServiceScopeAccessorInitializer<TScopeDefinition>>();
        initializer.ShouldNotInheritState = shouldNotInheritState;

        // Replace the existing initializer for this type, if it exists; at the same place in the list
        var index = Initializers.FindIndex(i => i is IServiceScopeAccessorInitializer<TScopeDefinition>);
        
        if (index >= 0)
            Initializers[index] = initializer;
        else
            Initializers.Add(initializer);
    }

    public void RunInitializers(string scopeName, IServiceProvider parentServiceProvider)
    {
        foreach (var initializer in Initializers)
            initializer.Run(scopeName, currentServiceProvider, parentServiceProvider);
    }

}
