using HealthyFoods.Application.ApplicationLogic.Interfaces;
using HealthyFoods.Application.Models.Food;
using Microsoft.AspNetCore.Components;

namespace HealthyFoods.Pages.Foods
{
    public partial class FoodDirectory
    {
        public List<FoodResponseModel> Foods { get; set; } = new();

        [Inject]
        private IFoodService? FoodService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

           //  var foodPaginatedResult = await FoodService.GetMany(new GetFoodsRequestModel() { PageIndex = 0, PageSize = 15 });

            var fakeFood = new FoodResponseModel()
            {
                Amount = 2,
                Price = 300_000,
                ImportedFrom = "CoopMart XLHN",
                CreatedDate = DateTime.Now,
                FoodName = "Beef"
            };

            Foods = new List<FoodResponseModel> { fakeFood };
        }
    }
}
