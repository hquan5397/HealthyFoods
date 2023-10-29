﻿namespace HealthyFoods.Core.Entities.Ingredients
{
    public class Item : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public double Price { get; set; }

        public int Quantity { get; set; }

        public string ImportedFrom { get; set; } = string.Empty;

        public static Item Create(string name, double price, int quantity, string importedFrom)
        {
            Item item = new Item()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Price = price,
                Quantity = quantity,
                ImportedFrom = importedFrom,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            return item;
        }

        public void Update(string name, double price, int quantity, string importedFrom)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
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
