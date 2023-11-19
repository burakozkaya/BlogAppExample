using AutoMapper;
using BlogAppExample.BLL.Abstract;
using BlogAppExample.DAL.UnitOfWork.Abstract;
using BlogAppExample.DTO.Dtos;
using BlogAppExample.Entity.Concrete;

namespace BlogAppExample.BLL.Concrete;

public class CategoryManager : GenericManager<Category, CategoryDTO>, ICategoryService
{
    public CategoryManager(IMapper mapper, IUOW uow) : base(mapper, uow)
    {
    }
}