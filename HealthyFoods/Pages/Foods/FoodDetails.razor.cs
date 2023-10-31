using CurrieTechnologies.Razor.SweetAlert2;
using HealthyFoods.Application.ApplicationLogic.Interfaces;
using HealthyFoods.Application.Models.Food;
using Microsoft.AspNetCore.Components;

namespace HealthyFoods.Pages.Foods;

public partial class FoodDetails
{
    [Inject]
    protected NavigationManager? _navigationManager { get; set; }

    [Inject]
    protected IFoodService? _foodService { get; set; }

    [Inject]
    protected SweetAlertService? _sweetAlert { get; set; }

    [Parameter]
    public string FoodId { get; set; } = Guid.Empty.ToString();

    public FoodModel Food { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        try
        {
            if (Guid.TryParse(FoodId, out var foodId) && FoodId != Guid.Empty.ToString())
            {
                Food.Id = foodId;

                var getFoodResult = await _foodService!.Get(foodId);

                if (getFoodResult != null)
                {
                    Food = new FoodModel()
                    {
                        FoodName = getFoodResult.FoodName,
                        Amount = getFoodResult.Amount,
                        Price = getFoodResult.Price,
                        ImportedFrom = getFoodResult.ImportedFrom,
                        ImportedDate = getFoodResult.CreatedDate
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
            if (Food.Id.HasValue)
            {
                var updateFoodModel = new UpdateFoodModel()
                {
                    Id = Food.Id.Value,
                    FoodName = Food.FoodName,
                    Amount = Food.Amount,
                    Price = Food.Price,
                    ImportedFrom = Food.ImportedFrom,
                };

                await _foodService!.Update(updateFoodModel);
            }
            else
            {
                var createFoodModel = new CreateFoodModel()
                {
                    FoodName = Food.FoodName,
                    Amount = Food.Amount,
                    Price = Food.Price,
                    ImportedFrom = Food.ImportedFrom,
                };

                await _foodService!.Import(createFoodModel);
            }

            await _sweetAlert!.FireAsync(new SweetAlertOptions
            {
                Html = "Successfully updated or imported",
                Icon = SweetAlertIcon.Success
            });
        }
        catch(Exception ex)
        {
            await _sweetAlert!.FireAsync(new SweetAlertOptions
            {
                Html = "Error when import or update data. Details: " + ex.Message,
                Icon = SweetAlertIcon.Error,
            });
        }
    }
}

public class FoodModel
{
    public Guid? Id { get; set; }

    public string FoodName { get; set; } = string.Empty;

    public double Amount { get; set; }

    public double Price { get; set; }

    public string ImportedFrom { get; set; } = string.Empty;

    public DateTime ImportedDate { get; set; }
}
