using DotNetNuke.DependencyInjection.Scopes;

namespace DotNetNuke.Page;
internal class PageInfo(IPageScopedService<PageInfoReal> realPageInfo): IPageInfo
{
    public int PageId => realPageInfo.Value.PageId; // Use the real PageInfo to get the PageId
}
