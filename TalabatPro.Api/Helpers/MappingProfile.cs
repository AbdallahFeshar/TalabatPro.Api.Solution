using AutoMapper;
using TalabatPro.Api.Core.Entities;
using TalabatPro.Api.DTOS;

namespace TalabatPro.Api.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                 .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                 .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                 .ForMember(d => d.PictureUrl, o => o.MapFrom<PictureUrlProduct>());
        }
    }
}
