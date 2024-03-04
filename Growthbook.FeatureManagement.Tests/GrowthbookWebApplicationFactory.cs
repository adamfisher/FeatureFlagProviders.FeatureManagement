using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Growthbook.FeatureManagement.Tests;

public class GrowthbookWebApplicationFactory : WebApplicationFactory<HeadlessProgram>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);
        builder.ConfigureServices(services => services.AddGrowthbookFeatureManagement());
    }
}

public class HeadlessProgram
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddGrowthbookFeatureManagement(configuration);

        var app = builder.Build();
        app.Run();
    }
}