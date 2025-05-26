namespace DotNetNuke.Page;

/// <summary>
/// Simulate the setup of a page info
/// </summary>
internal class PageInfoInitializerService(PageInfoReal pageInfo)
{

    public void SetupCurrentPage(int pageId)
    {
        pageInfo.PageId = pageId;
    }
}
