namespace Api.Extensions;

public static class StringEnumerableExtension
{
    public static string? Hashtagify(this IEnumerable<string>? keywords)
    {
        if (!keywords!.Any())
            return null;

        var list = new List<string>();
        keywords!.ToList().ForEach(kw => list.Add(kw.Slugify()));
        return string.Join(' ', list);
    }

    public static IEnumerable<string>? ListHashtags(this string? hashtagsString)
    {
        if (string.IsNullOrWhiteSpace(hashtagsString))
            return null;

        return hashtagsString.Split(' ').AsEnumerable();
    }
}