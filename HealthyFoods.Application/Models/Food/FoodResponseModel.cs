namespace HealthyFoods.Application.Models.Food
{
    public class FoodResponseModel
    {
        public string FoodName { get; set; } = string.Empty;

        public double Amount { get; set; }

        public double Price { get; set; }

        public string ImportedFrom { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}
