using System.Net;
using System.Net.Http.Json;
using GrowthBook;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Growthbook.FeatureManagement;

public class GrowthBookHttpClient
{
    private HttpClient _client { get; }
    private readonly GrowthBookOptions _options;

    public GrowthBookHttpClient(HttpClient client, IOptions<GrowthBookOptions> options)
    {
        _client = client;
        _options = options.Value;
        _client.BaseAddress = new Uri(_options.ApiHost);
    }

    public async Task<IDictionary<string, Feature>?> GetFeaturesAsync(CancellationToken? cancellationToken = null)
    {
        var response = await _client.GetAsync($"/api/features/{_options.ClientKey}", cancellationToken ?? CancellationToken.None);

        if (response.IsSuccessStatusCode)
        {
            var content = await response.Content.ReadAsStringAsync();
            var featuresResult = JsonConvert.DeserializeObject<FeaturesResult>(content);
            return featuresResult?.Features;
        }

        return default;
    }
}

public class FeaturesResult
{
    public HttpStatusCode Status { get; set; }
    public IDictionary<string, Feature>? Features { get; set; }
    public DateTimeOffset? DateUpdated { get; set; }
}