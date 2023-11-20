using BlogAppExample.DTO.Dtos;
using BlogAppExample.Entity.Concrete;

namespace BlogAppExample.BLL.Abstract;

public interface IBlogContentService : IGenericService<BlogContent, BlogContentDTO>
{
    int Count(BlogContentDTO contentDto);
}