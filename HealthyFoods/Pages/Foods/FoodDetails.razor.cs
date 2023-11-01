using CurrieTechnologies.Razor.SweetAlert2;
using HealthyFoods.Application.ApplicationLogic.Interfaces;
using HealthyFoods.Application.Models.Food;
using Microsoft.AspNetCore.Components;

namespace HealthyFoods.Pages.Foods;

public partial class FoodDetails
{
    [Inject]
    protected NavigationManager? _navigatcionManager { get; set; }

    [Inject]
    protected IImportedFoodService? _importedFoodService { get; set; }

    [Inject]
    protected IRawFoodService? _rawFoodService { get; set; }

    [Inject]
    protected SweetAlertService? _sweetAlert { get; set; }

    [Parameter]
    public string ImportedFoodId { get; set; } = Guid.Empty.ToString();

    public ImportedFoodModel ImportedFood { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        try
        {
            //var rawFoods = await _rawFoodService!.GetAll();

            //ImportedFood.RawFoods = rawFoods.Select(x => new RawFoodModel()
            //    {
            //        Id = x.Key,
            //        Text = x.Value
            //    }
            //);

            ImportedFood.RawFoods = new[] { 
                new RawFoodModel() { Id = Guid.NewGuid(), Text = "Beef" },
                new RawFoodModel() {Id = Guid.NewGuid(), Text = "Pork"},
                new RawFoodModel() {Id = Guid.NewGuid(), Text = "Fish"}
            };

            if (Guid.TryParse(ImportedFoodId, out var foodId) && ImportedFoodId != Guid.Empty.ToString())
            {
                ImportedFood.Id = foodId;

                var getFoodResult = await _importedFoodService!.Get(foodId);

                if (getFoodResult != null)
                {
                    ImportedFood = new ImportedFoodModel()
                    {
                        FoodName = getFoodResult.FoodName,
                        Amount = getFoodResult.Amount,
                        PricePerKg = getFoodResult.PricePerKg,
                        ImportedFrom = getFoodResult.ImportedFrom,
                        ImportedDate = getFoodResult.CreatedDate,
                        OriginalPrice = getFoodResult.OriginalPrice,
                        TotalPrice = getFoodResult.TotalPrice,
                    };

                    StateHasChanged();
                }
            }
        }
        catch (Exception ex)
        {
            await _sweetAlert!.FireAsync(new SweetAlertOptions
            {
                Html = "Error when getting data. Details: " + ex.Message,
                Icon = SweetAlertIcon.Error,
            });
        }
    }

    private async void Summit()
    {
        try
        {
            if (ImportedFood.Id.HasValue)
            {
                var updateFoodModel = new UpdateImportedFoodModel()
                {
                    Id = ImportedFood.Id.Value,
                    RawFoodId = ImportedFood.RawFoodId,
                    Amount = ImportedFood.Amount,
                    PricePerKg = ImportedFood.PricePerKg,
                    ImportedFrom = ImportedFood.ImportedFrom,
                    OriginalPrice = ImportedFood.OriginalPrice,
                };

                await _importedFoodService!.Update(updateFoodModel);
            }
            else
            {
                var createFoodModel = new CreateImportedFoodModel()
                {
                    RawFoodId = ImportedFood.RawFoodId,
                    Amount = ImportedFood.Amount,
                    PricePerKg = ImportedFood.PricePerKg,
                    ImportedFrom = ImportedFood.ImportedFrom,
                    OriginalPrice = ImportedFood.OriginalPrice,
                };

                await _importedFoodService!.Import(createFoodModel);
            }

            await _sweetAlert!.FireAsync(new SweetAlertOptions
            {
                Html = "Successfully updated or imported",
                Icon = SweetAlertIcon.Success,
            });

            _navigatcionManager!.NavigateTo("/food");
        }
        catch (Exception ex)
        {
            await _sweetAlert!.FireAsync(new SweetAlertOptions
            {
                Html = "Error when import or update data. Details: " + ex.Message,
                Icon = SweetAlertIcon.Error,
            });
        }
    }
}

public class ImportedFoodModel
{
    public Guid? Id { get; set; }

    public string FoodName { get; set; } = string.Empty;

    public Guid RawFoodId { get; set; }

    public double Amount { get; set; }

    public double OriginalPrice { get; set; }

    public double PricePerKg { get; set; }

    public double TotalPrice { get; set; }

    public string ImportedFrom { get; set; } = string.Empty;

    public DateTime ImportedDate { get; set; }

    public IEnumerable<RawFoodModel> RawFoods { get; set; } = Enumerable.Empty<RawFoodModel>();
}

public class RawFoodModel
{
    public Guid? Id { get; set; }
    public string Text { get; set; } = string.Empty;
}
