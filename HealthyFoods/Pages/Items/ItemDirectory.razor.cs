using HealthyFoods.Application.ApplicationLogic.Interfaces;
using HealthyFoods.Application.Models.Item;
using Microsoft.AspNetCore.Components;

namespace HealthyFoods.Pages.Items
{
    public partial class ItemDirectory
    {
        public List<ItemReponseModel> Items { get; set; } = new();

        [Inject]
        private IItemService? ItemService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

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
    }
}
