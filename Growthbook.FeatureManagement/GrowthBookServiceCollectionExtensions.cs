using GrowthBook;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.FeatureManagement;

namespace Growthbook.FeatureManagement;

public static class GrowthBookServiceCollectionExtensions
{
    public static void AddGrowthbookFeatureManagement(this IServiceCollection services, GrowthBookOptions options)
    {
        services.AddSingleton(Options.Create(options));
        AddGrowthbookFeatureManagement(services);
    }

    public static void AddGrowthbookFeatureManagement(this IServiceCollection services, IConfiguration? configuration = null)
    {
        services.AddOptions<GrowthBookOptions>().BindConfiguration(GrowthBookOptions.Growthbook);
        
        services.AddHttpClient<GrowthBookHttpClient>();

        services.AddTransient<IGrowthBook, GrowthBook.GrowthBook>(sp =>
        {
            var options = sp.GetRequiredService<IOptions<GrowthBookOptions>>().Value;
            var logger = sp.GetRequiredService<ILogger<IGrowthBook>>();
            var client = sp.GetRequiredService<GrowthBookHttpClient>();
            var features = client.GetFeaturesAsync().Result;

            logger.LogInformation("Loaded {Feature Count} feature flags from '{ApiHost}'.", features?.Count, options.ApiHost);

            if (features?.Count >= 0)
            {
                foreach (var feature in features)
                { 
                    options.Features.TryAdd(feature.Key, feature.Value);
                }

                logger.LogInformation("Loaded {Feature Count} feature flags from '{ApiHost}'", features.Count, options.ApiHost);
            }
            else
            {
                logger.LogInformation("No features found for '{ApiHost}'", options.ApiHost);
            }

            foreach (var feature in options.Features)
            {
                logger.LogTrace("Feature: '{FeatureName}' => '{FeatureValue}'", feature.Key, feature.Value);
            }

            return new GrowthBook.GrowthBook(options);
        });

        // Implementation of IFeatureDefinitionProvider must be added into the service collection before adding feature management.
        services.AddSingleton<IFeatureDefinitionProvider, GrowthBookFeatureDefinitionProvider>();

        if (configuration != null)
        {
            services.AddFeatureManagement(configuration);
        }
        else
        {
            services.AddFeatureManagement();
        }
    }
}