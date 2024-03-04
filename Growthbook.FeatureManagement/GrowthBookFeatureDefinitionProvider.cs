using GrowthBook;
using Microsoft.Extensions.Configuration;
using Microsoft.FeatureManagement;

namespace Growthbook.FeatureManagement;

public class GrowthBookFeatureDefinitionProvider : IFeatureDefinitionProvider
{
    public IGrowthBook _growthBook;

    public GrowthBookFeatureDefinitionProvider(IGrowthBook growthBook)
    {
        _growthBook = growthBook;
    }

    public Task<FeatureDefinition> GetFeatureDefinitionAsync(string featureName)
    {
        var enabled = _growthBook.IsOn(featureName);

        var featureDefinition = new FeatureDefinition
        {
            Name = featureName
        };

        if (enabled)
        {
            List<FeatureFilterConfiguration> enabledFor = (List<FeatureFilterConfiguration>) featureDefinition.EnabledFor;
            enabledFor.Add(new FeatureFilterConfiguration { Name = "AlwaysOn" });
        }

        return Task.FromResult(featureDefinition);
    }

    public async IAsyncEnumerable<FeatureDefinition> GetAllFeatureDefinitionsAsync()
    {
        await _growthBook.LoadFeatures();
        var features = ((GrowthBook.GrowthBook) _growthBook).Features;

        foreach (var feature in features)
        {
            yield return await GetFeatureDefinitionAsync(feature.Key);
        }
    }
}