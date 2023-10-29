using HealthyFoods.Core.Models;

namespace HealthyFoods.Application.Models.Item
{
    public class GetItemsRequestModel : PagingRequest
    {
        public List<string> ItemNames { get; set; } = new List<string>();

        public List<Guid> ItemIds { get; set; } = new();

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }
    }
}
