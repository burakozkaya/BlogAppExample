using AutoMapper;
using BlogAppExample.DTO.Dtos;
using BlogAppExample.Entity.Concrete;

namespace BlogAppExample.BLL.MappingProfile;
public class BLLMappingProfile : Profile
{
    public BLLMappingProfile()
    {
        CreateMap<Category, CategoryDTO>()
            .ReverseMap();


        CreateMap<BlogContent, BlogContentDTO>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.AppUser.Name))
            .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => src.AppUser.SurName))
            .ReverseMap();

        CreateMap<AppUserRegisterDto, AppUser>().ReverseMap();
        CreateMap<BlogContentCreateDto, BlogContent>().ReverseMap();

        CreateMap<PasswordUpdateDto, AppUser>().ReverseMap();
    }

}