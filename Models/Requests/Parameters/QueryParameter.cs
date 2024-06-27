namespace Xdd.HttpHelpers.Models.Requests.Parameters;

public class QueryParameter : IRequestParameter
{
    public string Name { get; set; }
    public string? Value { get; set; }
}