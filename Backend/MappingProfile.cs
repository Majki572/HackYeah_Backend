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
    }

}
