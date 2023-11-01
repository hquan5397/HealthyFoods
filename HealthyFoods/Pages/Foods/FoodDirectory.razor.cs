using CurrieTechnologies.Razor.SweetAlert2;
using HealthyFoods.Application.ApplicationLogic.Interfaces;
using HealthyFoods.Application.Models.Food;
using Microsoft.AspNetCore.Components;

namespace HealthyFoods.Pages.Foods;

public partial class FoodDirectory
{
    [Inject]
    private IImportedFoodService? _importedFoodService { get; set; }

    [Inject]
    protected NavigationManager? _navigationManager { get; set; }

    [Inject]
    protected SweetAlertService? _sweetAlertService { get; set; }

    public List<ImportedFoodResponseModel> Foods { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();
        try
        {
            var foodPaginatedResult = await _importedFoodService!.GetMany(new GetImportedFoodsRequestModel() { PageIndex = 0, PageSize = 1000 });

            Foods = foodPaginatedResult.Result.ToList();
        }
        catch(Exception)
        {
            await _sweetAlertService!.FireAsync(new SweetAlertOptions()
            {
                Html = "Error when getting data",
                Icon = SweetAlertIcon.Error
            });
        }
    }

    public void OnAddNewClicked()
    {
        _navigationManager!.NavigateTo($"/imported-food/{Guid.Empty}");
    }
}
