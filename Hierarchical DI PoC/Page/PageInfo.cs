namespace DotNetNuke.Page;
internal class PageInfo(IScopedService<ScopePage, PageInfoState> realPageInfo): IPageInfo
{
    public int PageId => realPageInfo.Value.PageId; // Use the real PageInfo to get the PageId
}
