namespace Api.Extensions;

/// <summary>
/// کلاس اضافه کننده قابلیت هایی به لیست هایی از نوع استرینگ
/// </summary>
public static class StringEnumerableExtension
{
    /// <summary>
    /// تبدیل لیستی از استرینگ ها به یک استرینگ هشتگی شکل شده
    /// </summary>
    /// <param name="keywords">کلمات کلیدی</param>
    /// <returns>یک استرینگ هشتگی شکل شده</returns>
    public static string? Hashtagify(this IEnumerable<string>? keywords)
    {
        if (!keywords!.Any())
            return null;

        var list = new List<string>();
        keywords!.ToList().ForEach(kw => list.Add(kw.Slugify()));
        return string.Join(' ', list);
    }

    /// <summary>
    /// تبدیل یک استرینگ به لیستی استرینگ های هشتگی شکل شده
    /// </summary>
    /// <param name="hashtagsString">یک استرینگ هستگی شکل نشده</param>
    /// <returns>لیستی از استرینگ های هشتگی شکل شده</returns>
    public static IEnumerable<string>? ListHashtags(this string? hashtagsString)
    {
        if (string.IsNullOrWhiteSpace(hashtagsString))
            return null;

        return hashtagsString.Split(' ').AsEnumerable();
    }
}