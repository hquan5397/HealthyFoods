namespace HealthyFoods.Core.Entities.Ingredients;
public class RawFood : BaseEntity
{
    public string Name { get; set; } = string.Empty;

    public ICollection<ImportedFood> ImportedFoods { get; set; }

    public static RawFood Create(string name)
    {
        var rawFood = new RawFood();
        rawFood.Id= Guid.NewGuid();
        rawFood.Name = name;
        rawFood.CreatedDate= DateTime.Now;
        rawFood.UpdatedDate= DateTime.Now;

        return rawFood;
    }
}
