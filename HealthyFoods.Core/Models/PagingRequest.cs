namespace HealthyFoods.Core.Models;

public class PagingRequest
{
    public int PageIndex { get; set; }

    public int PageSize { get; set; } = 10;
}
