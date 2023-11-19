using BlogAppExample.DTO.Dtos;
using BlogAppExample.Entity.Concrete;

namespace BlogAppExample.BLL.Abstract;

public interface ICategoryService : IGenericService<Category, CategoryDTO>
{

}