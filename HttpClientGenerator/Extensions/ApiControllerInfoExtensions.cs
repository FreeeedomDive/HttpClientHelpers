using Xdd.HttpHelpers.HttpClientGenerator.Models;

namespace Xdd.HttpHelpers.HttpClientGenerator.Extensions;

internal static class ApiControllerInfoExtensions
{
    public static string GetClientName(this ApiControllerInfo apiControllerInfo, bool isInterface = false)
    {
        return $"{(isInterface ? "I" : string.Empty)}{apiControllerInfo.Name}Client";
    }
}