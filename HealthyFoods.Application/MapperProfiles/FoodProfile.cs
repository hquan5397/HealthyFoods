using AutoMapper;
using HealthyFoods.Application.Models.Food;
using HealthyFoods.Core.Entities.Ingredients;

namespace HealthyFoods.Application.MapperProfiles
{
    public class FoodProfile : Profile
    {
        public FoodProfile()
        {
            CreateMap<Food, FoodResponseModel>();

        }
    }
}
