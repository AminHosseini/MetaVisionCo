using System.Globalization;

namespace Api.Extensions;

public static class ResponseHeaderExtension
{
    public static void SetTotalCount(this IHeaderDictionary headers, long? totalCount)
    {
        ArgumentNullException.ThrowIfNull(headers);
        if (totalCount == null)
            return;
        //headers.Add("X-Total-Count", string.Create(CultureInfo.InvariantCulture, $"{totalCount.Value}"));
        headers.Append("X-Total-Count", string.Create(CultureInfo.InvariantCulture, $"{totalCount.Value}"));
    }
}