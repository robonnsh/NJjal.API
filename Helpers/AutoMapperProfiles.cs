using AutoMapper;
using Njal_back.DTOS;
using Njal_back.Models;

namespace Njal_back.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            // Update whole product
            CreateMap<Product, ProductDto>().ReverseMap();

            // Update only designer name
            CreateMap<Product, DesignerNameDto>().ReverseMap();

            // Update only product name
            CreateMap<Product, ProductNameDto>().ReverseMap();
            
            // update price only
            CreateMap<Product, PriceDto>().ReverseMap();
        
        }
    }
}
