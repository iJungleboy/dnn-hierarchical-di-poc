namespace DotNetNuke.DependencyInjection.Scopes;

/// <summary>
/// Special helper to get a ServiceProvider of the page scope, in scenarios where inner scopes are used, like for each module.
/// </summary>
public interface IServiceScopeAccessor
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="serviceProvider">Page scoped service provider.</param>
    /// <param name="currentScopeName">Information about the scope this was created in, mainly for debugging issues.</param>
    internal void SetupServiceProvider(IServiceProvider serviceProvider, string currentScopeName);

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
    string AccessedScopeName { get; }

    /// <summary>
    /// Name of the current scope where this scope accessor was created. Mainly for debugging purposes.
    /// </summary>
    string CurrentScopeName { get; }

    bool IsValid { get; }
}