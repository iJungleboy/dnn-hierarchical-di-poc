using DotNetNuke.DependencyInjection;
using DotNetNuke.Page;

namespace DotNetNuke.UnitTests.TestPages;
public class TestPageInfoInScopePage(IServiceProvider globalServiceProvider)
{
    [Theory]
    [InlineData(21)]
    [InlineData(42)]
    public void PageScoped_PageInfo_RemembersId(int pageId)
    {
        // Arrange
        // Create a page scope and prepare shared page context
        var pageSp = globalServiceProvider.SetupPage(pageId);

        var pageOfPageScope = pageSp.GetRequiredService<IPageInfo>();
        Equal(pageId, pageOfPageScope.PageId);
    }

    [Theory]
    [InlineData(21)]
    [InlineData(42)]
    public void PageScoped_PageInfos_RememberIdButNotSame(int pageId)
    {
        var pageSp = globalServiceProvider.SetupPage(pageId);
        var pageInfo1 = pageSp.GetRequiredService<IPageInfo>();
        var pageInfo2 = pageSp.GetRequiredService<IPageInfo>();
        Equal(pageId, pageInfo1.PageId);
        Equal(pageId, pageInfo2.PageId);
        NotSame(pageInfo1, pageInfo2);
    }

    [Theory]
    [InlineData(21)]
    [InlineData(42)]
    public void PageScoped_PageInfoWhichCanOnlyLiveOnPageScope_RemembersId(int pageId)
    {
        // Arrange
        // Create a page scope and prepare shared page context
        var pageSp = globalServiceProvider.SetupPage(pageId);

        var pageOfPageScope = pageSp.GetRequiredService<IPageInfoWhichCanOnlyLiveOnPageScope>();
        Equal(pageId, pageOfPageScope.PageId);
    }

    [Theory]
    [InlineData(21)]
    [InlineData(42)]
    public void PageScoped_PageInfoWhichCanOnlyLiveOnPageScope_AreTheSame(int pageId)
    {
        var pageSp = globalServiceProvider.SetupPage(pageId);
        var pageInfo1 = pageSp.GetRequiredService<IPageInfoWhichCanOnlyLiveOnPageScope>();
        var pageInfo2 = pageSp.GetRequiredService<IPageInfoWhichCanOnlyLiveOnPageScope>();
        Equal(pageId, pageInfo1.PageId);
        Equal(pageId, pageInfo2.PageId);
        /*Not*/Same(pageInfo1, pageInfo2);
    }

}
