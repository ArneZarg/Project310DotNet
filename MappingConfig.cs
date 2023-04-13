using _310NutritionAPI.Models;
using _310NutritionAPI.Models.DTO;
using AutoMapper;

namespace _310NutritionAPI
{
    public class MappingConfig:Profile
    {
        public MappingConfig() {
            //Products
            CreateMap<Product, ProductDTO>().ReverseMap().ForMember(dest => dest.Variants, opt => opt.MapFrom(src => src.Variants));
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();
            CreateMap<Product, ProductCreateDTO>().ReverseMap();
            
            //Variants
            CreateMap<Variant, VariantDTO>().ReverseMap();
            CreateMap<Variant, VariantCreateDTO>().ReverseMap();
            CreateMap<Variant, VariantUpdateDTO>().ReverseMap();

            //Collections
            CreateMap<Collection, CollectionDTO>().ReverseMap().ForMember(dest => dest.CollectionProducts, opt=>opt.MapFrom(src=>src.CollectionProducts));
            CreateMap<Collection, CollectionCreateDTO>().ReverseMap();
            CreateMap<Collection, CollectionUpdateDTO>().ReverseMap();
        }
    }
}
