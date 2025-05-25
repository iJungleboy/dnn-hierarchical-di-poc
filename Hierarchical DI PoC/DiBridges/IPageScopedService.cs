namespace ToSic.HierarchicalDI.DiBridges;

public interface IPageScopedService<out T> where T : class
{
    T Value { get; }
}