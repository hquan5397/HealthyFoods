namespace HealthyFoods.Application.Models.Item
{
    public class ItemReponseModel
    {
        public string ItemName { get; set; } = string.Empty;

        public double Amount { get; set; }

        public double Price { get; set; }

        public string ImportedFrom { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}
