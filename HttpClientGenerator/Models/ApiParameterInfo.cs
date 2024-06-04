namespace Xdd.HttpHelpers.HttpClientGenerator.Models;

public class ApiParameterInfo
{
    public string Name { get; set; }
    public Type Type { get; set; }
    public ParameterSource Source { get; set; }
    public string? OptionalValue { get; set; }
}