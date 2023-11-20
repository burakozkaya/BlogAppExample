using BlogAppExample.BLL.ResponseConcrete;
using BlogAppExample.DTO.Dtos;
using BlogAppExample.Entity.Concrete;

namespace BlogAppExample.BLL.Abstract;

public interface IBlogContentService : IGenericService<BlogContent, BlogContentDTO>
{
    Response Count(BlogContentDTO contentDto);
    Response Insert(BlogContentCreateDto contentCreateDto);
    Response<IEnumerable<BlogContentDTO>> GetMostReaded();
    Response<IEnumerable<BlogContentDTO>> GetUserBlog(string userId);
}