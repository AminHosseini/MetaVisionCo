namespace Api.Filters;

/// <summary>
/// Odata پاسخ
/// </summary>
public class ODataResponse
{
    /// <summary>
    /// تعداد
    /// </summary>
    public long? Count { get; set; }

    /// <summary>
    /// مقدار
    /// </summary>
    public IQueryable? Value { get; set; }
}