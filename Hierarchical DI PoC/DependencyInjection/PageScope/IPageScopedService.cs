namespace ToSic.HierarchicalDI.DependencyInjection.PageScope;

public interface IPageScopedService<out T> where T : class
{
    T Value { get; }
}