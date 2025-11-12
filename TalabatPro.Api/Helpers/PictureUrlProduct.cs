using AutoMapper;
using TalabatPro.Api.Core.Entities;
using TalabatPro.Api.DTOS;

namespace TalabatPro.Api.Helpers
{
    public class PictureUrlProduct : IValueResolver<Product, ProductToReturnDto, string>
    {
        private readonly IConfiguration _config;

        public PictureUrlProduct(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(Product source, ProductToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
                return $"{_config["PictureUrlResolver"]}/{source.PictureUrl}";
            return string.Empty;
        }
    }
}
