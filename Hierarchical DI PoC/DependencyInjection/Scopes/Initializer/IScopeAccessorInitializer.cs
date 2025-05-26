using DotNetNuke.DependencyInjection.Scopes.Accessors;

namespace DotNetNuke.DependencyInjection.Scopes.Initializer;

/// <summary>
/// Untyped initializer, to store in lists of initializers, to be run when a new scope is created.
/// </summary>
internal interface IScopeAccessorInitializer
{
    void Run(string currentName, IServiceProvider currentServiceProvider, IServiceProvider parentServiceProvider);
}

/// <summary>
/// Typed initializer, specific to a specific scope accessor type.
/// </summary>
/// <typeparam name="TScopeAccessor"></typeparam>
internal interface IScopeAccessorInitializer<TScopeAccessor>
    : IScopeAccessorInitializer
    where TScopeAccessor : IServiceScopeAccessor;