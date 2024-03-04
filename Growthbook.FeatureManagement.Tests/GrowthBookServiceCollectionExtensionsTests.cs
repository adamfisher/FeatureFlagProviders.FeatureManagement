using GrowthBook;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace Growthbook.FeatureManagement.Tests
{
    public class GrowthBookServiceCollectionExtensionsTests : IClassFixture<GrowthbookWebApplicationFactory>
    {
        private readonly IServiceProvider _services;

        public GrowthBookServiceCollectionExtensionsTests(GrowthbookWebApplicationFactory growthbookWebApplicationFactory)
        {
            _services = growthbookWebApplicationFactory.Services;
        }

        [Fact]
        [IntegrationTest]
        public void AddGrowthbookFeatureManagement()
        {
            _services.Invoking(s => s.GetRequiredService<IGrowthBook>()).Should().NotThrow();
            _services.Invoking(s => s.GetRequiredService<IOptions<GrowthBookOptions>>()).Should().NotThrow();
            _services.Invoking(s => s.GetRequiredService<IFeatureManager>()).Should().NotThrow();
            _services.Invoking(s => s.GetRequiredService<IFeatureDefinitionProvider>()).Should().NotThrow();
            _services.GetRequiredService<IFeatureDefinitionProvider>().Should().BeOfType<GrowthBookFeatureDefinitionProvider>();
        }
    }
}