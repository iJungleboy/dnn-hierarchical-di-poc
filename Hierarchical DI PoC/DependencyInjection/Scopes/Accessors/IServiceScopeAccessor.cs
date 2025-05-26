namespace DotNetNuke.DependencyInjection.Scopes.Accessors;

/// <summary>
/// Special helper to get a ServiceProvider of another scope. For scenarios where inner scopes are used, like for each module.
/// </summary>
// ReSharper disable once UnusedTypeParameter
public interface IServiceScopeAccessor<TScopeDefinition>
    where TScopeDefinition : ScopeDefinition, new()
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider">Page scoped service provider.</param>
    /// <param name="currentScopeName">Information about the scope this was created in, mainly for debugging issues.</param>
    internal void Setup(IServiceProvider serviceProvider, string currentScopeName);

    /// <summary>
    /// The ServiceProvider.
    /// </summary>
    /// <remarks>
    /// When in root scope, it will throw an exception since it should never be used in the root scope.
    /// When in the page scope, it's the default service provider of the page scope.
    /// When in a deeper scope (Module, Content-Block, etc.) it's the page scoped service provider.
    /// </remarks>
    IServiceProvider ServiceProvider { get; }

    /// <summary>
    /// Name of the scope that will be accessed; mainly for debugging purposes.
    /// </summary>
    internal string AccessedScopeName { get; }

    /// <summary>
    /// Name of the current scope where this scope accessor was created. Mainly for debugging purposes.
    /// </summary>
    internal string CurrentScopeName { get; }

    /// <summary>
    /// Determines if the scope accessor is initialized / valid.
    /// </summary>
    bool IsInitialized { get; }
}