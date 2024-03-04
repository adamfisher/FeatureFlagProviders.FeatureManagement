using Microsoft.Extensions.DependencyInjection;
using Microsoft.FeatureManagement;

namespace Growthbook.FeatureManagement.Tests
{
    public class GrowthBookFeatureDefinitionProviderTests : IClassFixture<GrowthbookWebApplicationFactory>
    {
        private readonly IServiceProvider _services;
        private readonly IFeatureManager _featureManager;

        public GrowthBookFeatureDefinitionProviderTests(GrowthbookWebApplicationFactory growthbookWebApplicationFactory)
        {
            _services = growthbookWebApplicationFactory.Services;
            _featureManager = _services.GetRequiredService<IFeatureManager>();
        }

        [Fact]
        [IntegrationTest]
        public async Task GetFeatureDefinitionAsync()
        {
            var enabled = await _featureManager.IsEnabledAsync("adminarea");
            enabled.Should().BeTrue();
        }

        [Fact]
        [IntegrationTest]
        public void GetAllFeatureDefinitionsAsync()
        {
            var featureNames = _featureManager.GetFeatureNamesAsync().ToBlockingEnumerable().ToList();
            featureNames.Should().NotBeEmpty();
        }
    }
}