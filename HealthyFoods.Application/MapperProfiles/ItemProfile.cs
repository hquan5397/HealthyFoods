﻿using AutoMapper;
using HealthyFoods.Application.Models.Item;
using HealthyFoods.Core.Entities.Ingredients;

namespace HealthyFoods.Application.MapperProfiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile()
        {
            CreateMap<Item, ItemReponseModel>();
        }
    }
}