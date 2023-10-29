namespace HealthyFoods.Core.Entities.Ingredients
{
    public class Food : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public double Price { get; set; }

        public double Amount { get; set; }

        public string ImportedFrom { get; set; } = string.Empty;

        public static Food Create(string name, double price, double amount, string importedFrom)
        {
            var food = new Food
            {
                Id = Guid.NewGuid(),
                Name = name,
                Price = price,
                Amount = amount,
                ImportedFrom = importedFrom,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            return food;
        }

        public void Update(string name, double price, double amount, string importedFrom)
        {
            Name = name;
            Price = price;
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
