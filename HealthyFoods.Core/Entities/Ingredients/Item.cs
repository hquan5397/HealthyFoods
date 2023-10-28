namespace HealthyFoods.Core.Entities.Ingredients
{
    public class Item : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public double Price { get; set; }

        public int Quantity { get; set; }

        public static Item Create(string name, double price, int quantity)
        {
            Item item = new Item()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Price = price,
                Quantity = quantity,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            return item;
        }

    }
}
