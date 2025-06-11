namespace DotNetNuke.Page;

/// <summary>
/// Simulate the setup of a page info
/// </summary>
internal class PageInfoInitializerService(PageInfoState pageInfo, IPageInfoWhichCanOnlyLiveOnPageScope pageInfoScoped)
{
    public void SetupCurrentPage(int pageId)
    {
        pageInfo.PageId = pageId;
        pageInfoScoped.PageId = pageId;
    }
}
