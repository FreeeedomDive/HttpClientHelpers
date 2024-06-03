using ClientsGenerator.Models;

namespace ClientsGenerator.Extensions;

public static class ApiControllerInfoExtensions
{
    public static string GetClientName(this ApiControllerInfo apiControllerInfo, bool isInterface = false)
    {
        return $"{(isInterface ? "I" : string.Empty)}{apiControllerInfo.Name}Client";
    }
}