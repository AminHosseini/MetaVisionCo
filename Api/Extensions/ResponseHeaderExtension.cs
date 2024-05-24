using System.Globalization;

namespace Api.Extensions;

/// <summary>
/// در پاسخ ها header تغییر 
/// </summary>
public static class ResponseHeaderExtension
{
    /// <summary>
    /// header اضافه کردن تعداد به
    /// </summary>
    /// <param name="headers">هدر</param>
    /// <param name="totalCount">تعداد کل</param>
    public static void SetTotalCount(this IHeaderDictionary headers, long? totalCount)
    {
        ArgumentNullException.ThrowIfNull(headers);
        if (totalCount == null)
            return;
        //headers.Add("X-Total-Count", string.Create(CultureInfo.InvariantCulture, $"{totalCount.Value}"));
        headers.Append("X-Total-Count", string.Create(CultureInfo.InvariantCulture, $"{totalCount.Value}"));
    }
}