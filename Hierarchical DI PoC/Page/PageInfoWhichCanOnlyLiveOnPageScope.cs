namespace DotNetNuke.Page;

/// <summary>
/// EXPERIMENTAL: This service will only live on the page scope, and should never be accessed from any other scope.
/// But anybody asking for IPageInfoStateWhichCanOnlyLiveOnPageScope will get this identical instance.
/// </summary>
internal class PageInfoWhichCanOnlyLiveOnPageScope : IPageInfo, IPageInfoWhichCanOnlyLiveOnPageScope
{
    public int PageId { get; set; } = -1;
}
