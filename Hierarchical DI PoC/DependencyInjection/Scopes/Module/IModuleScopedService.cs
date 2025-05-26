namespace DotNetNuke.DependencyInjection.Scopes;

public interface IModuleScopedService<out T> where T : class
{
    T Value { get; }
}