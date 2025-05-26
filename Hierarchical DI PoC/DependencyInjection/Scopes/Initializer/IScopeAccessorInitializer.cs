namespace DotNetNuke.DependencyInjection.Scopes.Initializer;

/// <summary>
/// Helper to initialize a scope accessor for a specific scope type.
/// </summary>
/// <remarks>
/// This will always be used with the generic scoped initializer.
/// It only has the non-generic version to allow placing in a list with other initializers.
/// </remarks>
internal interface IScopeAccessorInitializer
{
    /// <summary>
    /// Run the initializer for the specific scope accessor.
    /// </summary>
    /// <param name="currentScopeName"></param>
    /// <param name="currentServiceProvider"></param>
    /// <param name="parentServiceProvider"></param>
    void Run(string currentScopeName, IServiceProvider currentServiceProvider, IServiceProvider parentServiceProvider);

    /// <summary>
    /// Allow this specific scope to not inherit state from the parent scope.
    /// </summary>
    /// <remarks>
    /// Using this requires that certain services be re-created for the new scope.
    /// This is used in scenarios such as modules inside modules.
    /// In such a scenario, the service asking for a module info must get a new instance of the module info service, not the one from the parent scope.
    /// </remarks>
    bool ShouldNotInheritState { get; set; }
}

/// <summary>
/// Typed initializer, specific to a specific scope accessor type.
/// </summary>
/// <typeparam name="TScopeDefinition"></typeparam>
internal interface IScopeAccessorInitializer<TScopeDefinition>
    : IScopeAccessorInitializer
    where TScopeDefinition : ScopeDefinition, new();
