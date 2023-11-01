using AutoMapper;
using HealthyFoods.Application.Models.Food;
using HealthyFoods.Core.Entities.Ingredients;

namespace HealthyFoods.Application.MapperProfiles;

public class ImportedFoodProfile : Profile
{
    public ImportedFoodProfile()
    {
        CreateMap<ImportedFood, ImportedFoodResponseModel>()
            .ForMember(model => model.FoodName, 
                configuration => configuration.MapFrom(entity => entity.RawFood.Name)
            );
    }
}
