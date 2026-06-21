using System.Text.RegularExpressions;
using Humanizer;

namespace Acme.Center.Platform.Shared.Infrastructure.Persistence.EntityFrameworkCore.Configuration.Extensions;

public static partial class StringExtensions
{
    public static string ToSnakeCase(this string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return text;
        var value = SnakeCaseBoundaryRegex().Replace(text, "$1_$2");
        return value.Replace("-", "_").ToLowerInvariant();
    }

    public static string ToPlural(this string text)
    {
        return text.Pluralize(inputIsKnownToBeSingular: false);
    }

    [GeneratedRegex("([a-z0-9])([A-Z])")]
    private static partial Regex SnakeCaseBoundaryRegex();
}
