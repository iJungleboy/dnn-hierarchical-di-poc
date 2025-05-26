namespace DotNetNuke.DependencyInjection.Scopes;

/// <summary>
/// Untyped initializer, to store in lists of initializers, to be run when a new scope is created.
/// </summary>
internal interface IScopeInitializer
{
    void Run(string currentName, IServiceProvider currentServiceProvider, IServiceProvider parentServiceProvider);
}

/// <summary>
/// Typed initializer, specific to a specific scope accessor type.
/// </summary>
/// <typeparam name="TScopeAccessor"></typeparam>
internal interface IScopeInitializer<TScopeAccessor>
    : IScopeInitializer
    where TScopeAccessor : IServiceScopeAccessor;