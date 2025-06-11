using DotNetNuke.DependencyInjection.Scopes.Definitions;

namespace DotNetNuke.Page;
internal class PageInfo(IFromScope<ScopePage, PageInfoState> realPageInfo): IPageInfo
{
    public int PageId => realPageInfo.Value.PageId; // Use the real PageInfo to get the PageId
}
