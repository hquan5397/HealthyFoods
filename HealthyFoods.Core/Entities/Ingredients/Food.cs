namespace HealthyFoods.Core.Entities.Ingredients
{
    public class Food : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public double Price { get; set; }

        public double Amount { get; set; }

        public static Food Create(string name, double price, double amount)
        {
            var food = new Food
            {
                Id = Guid.NewGuid(),
                Name = name,
                Price = price,
                Amount = amount,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            return food;
        }

    }
}
