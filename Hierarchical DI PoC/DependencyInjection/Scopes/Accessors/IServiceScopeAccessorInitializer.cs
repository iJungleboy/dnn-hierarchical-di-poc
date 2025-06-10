namespace DotNetNuke.DependencyInjection.Scopes.Accessors;

/// <summary>
/// Helper to initialize a scope accessor for a specific scope.
/// </summary>
/// <remarks>
/// Services in the current scope will use the scope accessor to access services from another scope.
/// For this to work, a new scope must have the accessors configured, so they can find the scope they need to access.
/// 
/// So the ScopeAccessorInitializer will run when new scopes are created, to set up all the accessors for that scope.
///
/// Note that this is not a generic Interface, so that all the initializers can be placed in a list,
/// and the scope-creation can just call every initializer in the list.
/// 
/// But the real scope initializers will implement the generic interface, since that includes the information which scope they are meant for.
/// </remarks>
internal interface IServiceScopeAccessorInitializer
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