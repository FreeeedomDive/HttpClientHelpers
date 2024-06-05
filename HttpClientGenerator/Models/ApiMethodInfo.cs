namespace Xdd.HttpHelpers.HttpClientGenerator.Models;

internal class ApiMethodInfo
{
    public string Name { get; set; } = null!;
    public string HttpMethod { get; set; } = null!;
    public string RouteTemplate { get; set; } = null!;
    public ApiParameterInfo[] Parameters { get; set; } = null!;
    public Type ReturnType { get; set; } = null!;
    public bool IsReturnTypeNullable { get; set; }
}