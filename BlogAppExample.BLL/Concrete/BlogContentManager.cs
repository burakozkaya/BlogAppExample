using AutoMapper;
using BlogAppExample.BLL.Abstract;
using BlogAppExample.DAL.UnitOfWork.Abstract;
using BlogAppExample.DTO.Dtos;
using BlogAppExample.Entity.Concrete;

namespace BlogAppExample.BLL.Concrete;

public class BlogContentManager : GenericManager<BlogContent, BlogContentDTO>, IBlogContentService
{
    public BlogContentManager(IMapper mapper, IUOW uow) : base(mapper, uow)
    {
    }
}