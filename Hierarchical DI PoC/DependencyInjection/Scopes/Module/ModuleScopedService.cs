using DotNetNuke.DependencyInjection.Scopes.Accessors;

namespace DotNetNuke.DependencyInjection.Scopes;

/// <summary>
/// Provide service which was generated within the page scope.
/// </summary>
/// <typeparam name="T"></typeparam>
internal class ModuleScopedService<T>(IServiceScopeAccessor<ScopeModule> moduleScopeAccessor) : IModuleScopedService<T> where T : class
{
    public T Value => moduleScopeAccessor.ServiceProvider.GetRequiredService<T>();
}