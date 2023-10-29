namespace HealthyFoods.Application.Models.Item
{
    public class UpdateItemModel
    {
        public Guid Id { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public double Price { get; set; }

        public string ImportedFrom { get; set; } = string.Empty;
    }
}
