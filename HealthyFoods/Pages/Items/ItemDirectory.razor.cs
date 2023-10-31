using CurrieTechnologies.Razor.SweetAlert2;
using HealthyFoods.Application.ApplicationLogic.Interfaces;
using HealthyFoods.Application.Models.Item;
using Microsoft.AspNetCore.Components;

namespace HealthyFoods.Pages.Items;

public partial class ItemDirectory
{

    [Inject]
    protected IItemService? _itemService { get; set; }

    [Inject]
    protected NavigationManager? _navigationManager { get; set; }

    [Inject]
    protected SweetAlertService? _sweetAlertService { get; set; }

    public List<ItemReponseModel> Items { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        try
        {
            //  var foodPaginatedResult = await FoodService.GetMany(new GetFoodsRequestModel() { PageIndex = 0, PageSize = 15 });

            var fakeItem = new ItemReponseModel()
            {
                Quantity = 1,
                Price = 186_000,
                ImportedFrom = "CoopMart XLHN",
                CreatedDate = DateTime.Now,
                ItemName = "Olive oil"
            };

            Items = new List<ItemReponseModel> { fakeItem };         
        }
        catch(Exception ex)
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
        _navigationManager!.NavigateTo($"/item/{Guid.Empty}");
    }
}
