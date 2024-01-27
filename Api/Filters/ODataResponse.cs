namespace Api.Filters;

public class ODataResponse
{
    public long? Count { get; set; }

    public IQueryable? Value { get; set; }
}