namespace Api.Features.Commons.Dtos;
/// <summary>
/// enum اطلاعات موجود در یک
/// </summary>
public readonly record struct GetAllDto
{
    /// <summary>
    /// آیدی
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// شرح
    /// </summary>
    public string Description { get; init; }

    /// <summary>
    /// نام
    /// </summary>
    public string? Name { get; init; }
}
