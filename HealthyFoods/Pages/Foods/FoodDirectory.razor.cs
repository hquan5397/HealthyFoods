using CurrieTechnologies.Razor.SweetAlert2;
using HealthyFoods.Application.ApplicationLogic.Interfaces;
using HealthyFoods.Application.Models.Food;
using Microsoft.AspNetCore.Components;

namespace HealthyFoods.Pages.Foods
{
    public partial class FoodDirectory
    {
        [Inject]
        private IFoodService? _foodService { get; set; }

        [Inject]
        protected NavigationManager? _navigationManager { get; set; }

        [Inject]
        protected SweetAlertService? _sweetAlertService { get; set; }

        public List<FoodResponseModel> Foods { get; set; } = new();

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();
            try
            {
              //  var foodPaginatedResult = await _foodService!.GetMany(new GetFoodsRequestModel() { PageIndex = 0, PageSize = 1000 });

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
            _navigationManager!.NavigateTo($"/food/{Guid.Empty}");
        }
    }
}
