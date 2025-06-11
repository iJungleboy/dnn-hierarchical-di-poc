using DotNetNuke.DependencyInjection.Scopes.Definitions;

namespace DotNetNuke.DependencyInjection.Scopes.Accessors;

/// <summary>
/// Typed initializer, specific to a specific scope accessor type.
/// </summary>
/// <typeparam name="TScopeDefinition"></typeparam>
internal interface IServiceScopeAccessorInitializer<TScopeDefinition>
    : IServiceScopeAccessorInitializer
    where TScopeDefinition : ScopeDefinition, new();