using ToSic.HierarchicalDI.DependencyInjection;
using ToSic.HierarchicalDI.DependencyInjection.PageScope;

namespace ToSic.HierarchicalDI.Page;
internal class PageInfo(IPageScopedService<PageInfoReal> realPageInfo): IPageInfo
{
    public int PageId => realPageInfo.Value.PageId; // Use the real PageInfo to get the PageId
}
