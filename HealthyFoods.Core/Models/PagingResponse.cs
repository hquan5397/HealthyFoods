namespace HealthyFoods.Core.Models;

public class PagingResponse<T> where T : class
{
    public IEnumerable<T> Result { get; set; } = Enumerable.Empty<T>();

    public int TotalRecords { get; set; }

    public int PageSize { get; set; }

    public int PageIndex { get; set; }
}
