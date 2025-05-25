namespace ToSic.HierarchicalDI.Page;

internal class PageInfoReal : IPageInfo
{
    public int PageId { get; set; } = new Random().Next();
}
