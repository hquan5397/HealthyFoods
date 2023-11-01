namespace HealthyFoods.Core.Entities.Ingredients
{
    public class ImportedItem : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public double OriginalPrice { get; set; }

        public double PricePerEach { get; set; }

        public int Quantity { get; set; }

        public string ImportedFrom { get; set; } = string.Empty;

        public static ImportedItem Create(string name, double originalPrice, double price, int quantity, string importedFrom)
        {
            ImportedItem item = new ImportedItem()
            {
                Id = Guid.NewGuid(),
                Name = name,
                OriginalPrice = originalPrice,
                PricePerEach = price,
                Quantity = quantity,
                ImportedFrom = importedFrom,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            return item;
        }

        public void Update(string name, double originalPrice, double price, int quantity, string importedFrom)
        {
            Name = name;
            OriginalPrice = originalPrice;
            PricePerEach = price;
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
