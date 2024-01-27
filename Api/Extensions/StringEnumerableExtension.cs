namespace Api.Extensions;

public static class StringEnumerableExtension
{
    public static string HashtagifyList(this IEnumerable<string>? keywords)
    {
        if (!keywords!.Any())
            return string.Empty;

        keywords!.ToList().ForEach(kw => kw.Slugify());
        return string.Join(' ', keywords!);
    }
}