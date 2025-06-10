namespace DotNetNuke.DependencyInjection.Scopes;

/// <summary>
/// Provides a service from a specific scope.
/// </summary>
/// <remarks>
/// It looks like a Lazy{TService} which is why it has a Value property instead of a GetService method.
/// </remarks>
/// <typeparam name="TScopeDefinition">The scope where the real service is managed.</typeparam>
/// <typeparam name="TService">The service we're requesting.</typeparam>
// ReSharper disable once UnusedTypeParameter
internal interface IFromScope<TScopeDefinition, out TService>
    where TService : class
    where TScopeDefinition : ScopeDefinition, new()
{
    /// <summary>
    /// The resolved service from the specified scope.
    /// </summary>
    TService Value { get; }
}