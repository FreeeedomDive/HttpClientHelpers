namespace Xdd.HttpHelpers.HttpClientGenerator.Models;

internal class ApiParameterInfo
{
    public string Name { get; set; }
    public Type Type { get; set; }
    public ParameterSource Source { get; set; }
    public bool IsNullable { get; set; }
    public string? OptionalValue { get; set; }
}