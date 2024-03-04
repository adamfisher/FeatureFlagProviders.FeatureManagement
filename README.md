# FeatureFlagProviders.FeatureManagement

Configures `Microsoft.FeatureManagement` built into .NET Core to work with popular feature flag services like Launch Darkly, GrowthBook, and Flagsmith.

Due to how differences in behavior and data available, some functionality may be simplified to accommodate the simplest use cases (i.e. feature flag is on or off) without regard to things like experiments, attributes, or metrics for example. Pull requests are welcome for adding functionality or implementing a new provider.

## Install

| **Package Name**                 	| **NuGet**                                                                                                                                                         	|
|----------------------------------	|-------------------------------------------------------------------------------------------------------------------------------------------------------------------	|
| `Growthbook.FeatureManagement`   	| [![](https://raw.githubusercontent.com/pixel-cookers/built-with-badges/master/nuget/nuget-long.png)](https://www.nuget.org/packages/Growthbook.FeatureManagement) 	|
| `Flagsmith.FeatureManagement`    	| Not implemented                                                                                                                                                   	|
| `LaunchDarkly.FeatureManagement` 	| Not implemented                                                                                                                                                   	|

## Getting Started

In your `Program.cs` file, add the registration below to add the relevant feature flag provider. Although not necessary, if you explicitly call `services.AddFeatureManagement()`, it must come ***after*** the call to add one of the feature flag providers below.

```csharp
services.AddGrowthbookFeatureManagement();
```

Get your [GrowthBook Client Key](https://app.growthbook.io/sdks) to configure your `AppSettings.json` to have the relevant settings needed to configure the feature flag provider as shown below:

```json
{
  "Growthbook": {
    "ApiHost": "https://cdn.growthbook.io",
    "ClientKey": "sdk-CLIENTKEYHERE"
  }
}
```
The [GrowthBook C# SDK](https://github.com/growthbook/growthbook-c-sharp) is used internally. The available options for the configuration are the same as the properties availble on the [`Context`](https://github.com/growthbook/growthbook-c-sharp/blob/main/GrowthBook/Context.cs) class.

Then you inject `IFeatureManager` to be used for flag lookups.

## How to Use Feature Management in .NET Core

See [Tutorial: Use feature flags in an ASP.NET Core app](https://learn.microsoft.com/en-us/azure/azure-app-configuration/use-feature-flags-dotnet-core) for more information on how to use .NET Core's built-in feature management system.