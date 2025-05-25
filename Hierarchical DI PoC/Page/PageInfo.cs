using ToSic.Sxc.Internal.Plumbing;

namespace ToSic.HierarchicalDI.TestObjects;
internal class PageInfo(PageScopedService<PageInfoReal> realPageInfo): IPageInfo
{
    public int PageId => realPageInfo.Value.PageId; // Use the real PageInfo to get the PageId
}
