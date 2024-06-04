namespace Xdd.HttpHelpers.HttpClientGenerator.Models;

internal class ApiControllerInfo
{
    public string Name { get; set; } = null!;
    public string Namespace { get; set; } = null!;
    public string RouteTemplate { get; set; } = null!;
    public ApiMethodInfo[] Methods { get; set; } = null!;
}