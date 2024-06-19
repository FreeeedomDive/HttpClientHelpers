namespace Xdd.HttpHelpers.HttpClientGenerator.Options;

public class GeneratorOptions
{
    /// <summary>
    ///     Path to your project with client
    /// </summary>
    public string ProjectPath { get; set; } = null!;

    /// <summary>
    ///     Base namespace for client, it could be your client's project name (example Weather.Api.Client)
    /// </summary>
    public string ClientNamespace { get; set; } = null!;

    /// <summary>
    ///     Base client name (example WeatherApiClient)
    /// </summary>
    public string? ClientName { get; set; }

    /// <summary>
    ///     Base client's interface name (example IWeatherApiClient)
    /// </summary>
    public string? InterfaceName { get; set; }

    /// <summary>
    ///     What HTTP-client use to perform HTTP requests
    ///     Not yet implemented, using RestSharp by default
    /// </summary>
    public ClientType ClientType { get; set; }
}