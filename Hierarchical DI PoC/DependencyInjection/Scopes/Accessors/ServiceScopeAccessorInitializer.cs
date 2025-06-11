using DotNetNuke.DependencyInjection.Scopes.Definitions;

namespace DotNetNuke.DependencyInjection.Scopes.Accessors;

/// <inheritdoc />
internal class ServiceScopeAccessorInitializer<TScopeDefinition>
    : IServiceScopeAccessorInitializer<TScopeDefinition>
    where TScopeDefinition : ScopeDefinition, new()
{
    /// <inheritdoc />
    public bool ShouldNotInheritState { get; set; }

    /// <inheritdoc />
    public void Run(string currentScopeName, IServiceProvider currentServiceProvider, IServiceProvider parentServiceProvider)
    {
        // Within the current scope, generate a fresh scope accessor for the current scope definition.
        // This will be initialized below.
        var newScopeAccessor = currentServiceProvider.GetRequiredService<IServiceScopeAccessor<TScopeDefinition>>();

        // Check if the parent service provider already has this same scope accessor registered.
        // This will always return a valid instance, but if it's misplaced, it will not be initialized yet.
        var parentScopeAccessor = parentServiceProvider.GetRequiredService<IServiceScopeAccessor<TScopeDefinition>>();

        // If the accessor on the parent was never initialized, then it should not be used.
        // Because in this case it was just created since we requested it, but no initializer has ever run,
        // so it should be ignored.
        var parentScopeIsValid = parentScopeAccessor.IsInitialized;

        // If the parent is the root scope, then we need to use the current service provider
        // Otherwise we're already in a deeper scope, and we should use the one provided by the previous scope accessor
        var sourceScopeServiceProvider = parentScopeIsValid && !ShouldNotInheritState
            ? parentScopeAccessor.ServiceProvider
            : currentServiceProvider;

        // Initialize the new scope accessor with the service provider and current name
        newScopeAccessor.Setup(sourceScopeServiceProvider, currentScopeName);
    }

}
