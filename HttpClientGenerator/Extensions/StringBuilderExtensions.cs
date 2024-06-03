using System.Text;

namespace ClientsGenerator.Extensions;

public static class StringBuilderExtensions
{
    public static StringBuilder AppendIndent(this StringBuilder stringBuilder, int count = 1)
    {
        const int indentSize = 4;
        return stringBuilder.Append(string.Concat(Enumerable.Repeat(" ", indentSize * count)));
    }
}