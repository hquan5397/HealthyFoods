namespace HealthyFoods.Core.Entities.Ingredients
{
    public class Food : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public double OriginalPrice { get; set; }

        public double PricePerKg { get; set; }

        public double Amount { get; set; }

        public string ImportedFrom { get; set; } = string.Empty;

        public static Food Create(string name, double orgiginalPrice, double price, double amount, string importedFrom)
        {
            var food = new Food
            {
                Id = Guid.NewGuid(),
                Name = name,
                OriginalPrice = orgiginalPrice,
                PricePerKg = price,
                Amount = amount,
                ImportedFrom = importedFrom,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            return food;
        }

        public void Update(string name,double originalPrice, double price, double amount, string importedFrom)
        {
            Name = name;
            OriginalPrice = originalPrice;
            PricePerKg = price;
            Amount = amount;
            ImportedFrom = importedFrom;
            UpdatedDate = DateTime.UtcNow;
        }

        public void Delete()
        {
            IsDeleted = true;
            UpdatedDate = DateTime.UtcNow;
        }
    }
}
