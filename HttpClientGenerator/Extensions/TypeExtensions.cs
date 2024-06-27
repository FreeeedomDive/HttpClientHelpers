namespace Xdd.HttpHelpers.HttpClientGenerator.Extensions;

internal static class TypeExtensions
{
    public static string GetFriendlyTypeName(this Type type, bool isNullable)
    {
        if (type == typeof(void))
        {
            return string.Empty;
        }

        var nullablePart = isNullable ? "?" : string.Empty;
        if (Keywords.TryGetValue(type, out var keyword) && !string.IsNullOrEmpty(keyword))
        {
            return keyword + nullablePart;
        }

        if (!type.IsGenericType)
        {
            return $"{type.Namespace}.{type.Name}" + nullablePart;
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

    private static readonly Dictionary<Type, string> Keywords = new()
    {
        { typeof(bool), "bool" },
        { typeof(byte), "byte" },
        { typeof(short), "short" },
        { typeof(int), "int" },
        { typeof(long), "long" },
        { typeof(float), "float" },
        { typeof(double), "double" },
        { typeof(string), "string" },
    };
}