using DotNetNuke.DependencyInjection;
using DotNetNuke.DependencyInjection.Scopes.Definitions;

namespace DotNetNuke.UnitTests.TestCurrentScopeDefinitions;
public class TestCurrentScopeDefinition(IServiceProvider globalServiceProvider)
{
    [Fact]
    public void RootScope_InspectInternalDefinition()
    {
        // Arrange
        var definition = globalServiceProvider.GetRequiredService<ICurrentScopeDefinition>().Definition;
        // Assert
        NotNull(definition);
        Equal(ServiceScopeConstants.ScopeRoot, definition.ScopeName);
    }

    [Fact]
    public void RootScope_ReportsRoot()
    {
        // Arrange
        var currentScopeDef = globalServiceProvider.GetRequiredService<ICurrentScopeDefinition>();
        // Act
        // Assert
        NotNull(currentScopeDef);
        Equal(ServiceScopeConstants.ScopeRoot, currentScopeDef.ScopeName);
        False(currentScopeDef.RestartsScopeWip);
    }

    [Fact]
    public void PageScope_ReportsPage()
    {
        // Arrange
        var pageSp = globalServiceProvider.SetupPage(2343);
        // Act
        var currentScopeDef = pageSp.GetRequiredService<ICurrentScopeDefinition>();
        // Assert
        NotNull(currentScopeDef);
        Equal(ServiceScopeConstants.ScopePage, currentScopeDef.ScopeName);
        False(currentScopeDef.RestartsScopeWip);
    }

    [Fact]
    public void ModuleScope_ReportsModule()
    {
        // Arrange
        var pageSp = globalServiceProvider.SetupPage(2343);
        var moduleSp = pageSp.SetupModule(1234);
        // Act
        var currentScopeDef = moduleSp.GetRequiredService<ICurrentScopeDefinition>();
        // Assert
        NotNull(currentScopeDef);
        Equal(ServiceScopeConstants.ScopeModule, currentScopeDef.ScopeName);
        False(currentScopeDef.RestartsScopeWip);
    }

    [Fact(Skip = "don't run, since we cannot use a Root Scope by design ATM")]
    public void PageScope_FromRoot_ReportsRoot()
    {
        // Arrange
        var pageSp = globalServiceProvider.SetupPage(2343);
        // Act
        var currentScopeDef = pageSp.GetRequiredService<IFromScope<ScopeRoot, ICurrentScopeDefinition>>().Value;
        // Assert
        NotNull(currentScopeDef);
        Equal(ServiceScopeConstants.ScopePage, currentScopeDef.ScopeName);
        False(currentScopeDef.RestartsScopeWip);
    }

    [Fact]
    public void ModuleScope_FromPage_ReportsPage()
    {
        // Arrange
        var pageSp = globalServiceProvider.SetupPage(2343);
        var moduleSp = pageSp.SetupModule(1234);
        // Act
        var currentScopeDef = moduleSp.GetRequiredService<IFromScope<ScopePage, ICurrentScopeDefinition>>().Value;
        // Assert
        NotNull(currentScopeDef);
        Equal(ServiceScopeConstants.ScopePage, currentScopeDef.ScopeName);
        False(currentScopeDef.RestartsScopeWip);
    }

    [Fact]
    public void ModuleScope_FromModule_ReportsModule()
    {
        // Arrange
        var pageSp = globalServiceProvider.SetupPage(2343);
        var moduleSp = pageSp.SetupModule(1234);
        // Act
        var currentScopeDef = moduleSp.GetRequiredService<IFromScope<ScopeModule, ICurrentScopeDefinition>>().Value;
        // Assert
        NotNull(currentScopeDef);
        Equal(ServiceScopeConstants.ScopeModule, currentScopeDef.ScopeName);
        False(currentScopeDef.RestartsScopeWip);
    }

}
