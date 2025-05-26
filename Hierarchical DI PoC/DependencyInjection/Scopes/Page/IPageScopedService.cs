namespace DotNetNuke.DependencyInjection.Scopes;

public interface IPageScopedService<out T> where T : class
{
    T Value { get; }
}