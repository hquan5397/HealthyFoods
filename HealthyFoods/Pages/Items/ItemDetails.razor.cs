using CurrieTechnologies.Razor.SweetAlert2;
using HealthyFoods.Application.ApplicationLogic.Interfaces;
using HealthyFoods.Application.Models.Item;
using Microsoft.AspNetCore.Components;

namespace HealthyFoods.Pages.Items;

public partial class ItemDetails
{
    [Inject]
    protected NavigationManager? _navigationManager { get; set; }

    [Inject]
    protected IItemService? _itemService { get; set; }

    [Inject]
    protected SweetAlertService? _sweetAlert { get; set; }

    [Parameter]
    public string ItemId { get; set; } = Guid.Empty.ToString();

    public ItemModel Item { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        await base.OnInitializedAsync();

        try
        {
            if (Guid.TryParse(ItemId, out var itemId) && ItemId != Guid.Empty.ToString())
            {
                Item.Id = itemId;

                var getItemResult = await _itemService!.Get(itemId);

                if (getItemResult != null)
                {
                    Item = new ItemModel()
                    {
                        ItemName = getItemResult.ItemName,
                        Quantity = getItemResult.Quantity,
                        PricePerEach = getItemResult.PricePerEach,
                        ImportedFrom = getItemResult.ImportedFrom,
                        ImportedDate = getItemResult.CreatedDate,
                        OriginalPrice = getItemResult.OriginalPrice,
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
            if (Item.Id.HasValue)
            {
                var updateItemModel = new UpdateItemModel()
                {
                    Id = Item.Id.Value,
                    ItemName = Item.ItemName,
                    Quantity = Item.Quantity,
                    PricePerEach = Item.PricePerEach,
                    ImportedFrom = Item.ImportedFrom,
                    OriginalPrice = Item.OriginalPrice,
                };

                await _itemService!.Update(updateItemModel);
            }
            else
            {
                var createItemModel = new CreateItemModel()
                {
                    ItemName = Item.ItemName,
                    Quantity = Item.Quantity,
                    PricePerEach = Item.PricePerEach,
                    ImportedFrom = Item.ImportedFrom,
                    OriginalPrice = Item.OriginalPrice,
                };

                await _itemService!.Import(createItemModel);
            }

            await _sweetAlert!.FireAsync(new SweetAlertOptions
            {
                Html = "Successfully updated or imported",
                Icon = SweetAlertIcon.Success
            });

            _navigationManager!.NavigateTo("/item");
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

public class ItemModel
{
    public Guid? Id { get; set; }

    public string ItemName { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public double OriginalPrice { get; set; }

    public double PricePerEach { get; set; }

    public string ImportedFrom { get; set; } = string.Empty;

    public DateTime ImportedDate { get; set; }
}
