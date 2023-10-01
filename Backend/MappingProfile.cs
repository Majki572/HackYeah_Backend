using AutoMapper;
using Backend.DTO;
using Database.Models;

namespace Backend;

public class MappingProfile : Profile
{
    public MappingProfile() 
    {
        CreateMap<ProductFridge, ProductDTO>();
        CreateMap<ProductDTO, ProductFridge>();
        CreateMap<ProductDTOB, ProductFridge>();
        CreateMap<ProductFridge, ProductDTOB>();
        CreateMap<ProductDictionary, ProductDictionaryDTO>();
        CreateMap<ProductDictionaryDTO, ProductDictionary>();
        CreateMap<MealDTO, Meal>();
        CreateMap<Meal, MealDTO>();
    }

}
