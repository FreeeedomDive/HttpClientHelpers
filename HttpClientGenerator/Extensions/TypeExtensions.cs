namespace Xdd.HttpHelpers.HttpClientGenerator.Extensions;

internal static class TypeExtensions
{
    public static string GetFriendlyTypeName(this Type type, bool isNullable)
    {
        if (type == typeof(void))
        {
            return string.Empty;
        }

        if (!type.IsGenericType)
        {
            return $"{type.Namespace}.{type.Name}";
        }

        var genericTypeDefinition = type.GetGenericTypeDefinition();
        var genericArguments = type.GetGenericArguments();
        var genericTypeName = genericTypeDefinition.Name;
        var backtickIndex = genericTypeName.IndexOf('`');
        if (backtickIndex > 0)
        {
            genericTypeName = genericTypeName[..backtickIndex];
        }

        return $"{genericTypeName}<{string.Join(", ", genericArguments.Select(x => GetFriendlyTypeName(x, isNullable)))}>";
    }
}