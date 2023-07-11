using AutoMapper;
using SampleWebApiAspNetCore.Dtos;
using SampleWebApiAspNetCore.Entities;

namespace SampleWebApiAspNetCore.MappingProfiles
{
    public class ImageMappings : Profile
    {
        public ImageMappings()
        {
            CreateMap<ImageEntity, ImageDto>().ReverseMap();
            CreateMap<ImageEntity, ImageUpdateDto>().ReverseMap();
            CreateMap<ImageEntity, ImageCreateDto>().ReverseMap();
        }
    }
}
