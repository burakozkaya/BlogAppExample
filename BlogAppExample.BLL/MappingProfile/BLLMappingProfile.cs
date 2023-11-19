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
            .ReverseMap();

        CreateMap<AppUserRegisterDto, AppUser>().ReverseMap();

        CreateMap<PasswordUpdateDto,AppUser>().ReverseMap();
    }    

}