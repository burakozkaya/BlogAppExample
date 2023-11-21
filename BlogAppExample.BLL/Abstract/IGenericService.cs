using BlogAppExample.BLL.ResponseConcrete;
using BlogAppExample.DTO.Base;
using BlogAppExample.Entity.Abstract;
using System.Linq.Expressions;

namespace BlogAppExample.BLL.Abstract;

public interface IGenericService<T, TDto>
    where T : class, IBaseEntity
    where TDto : class, IBaseDTO
{
    Response Insert(TDto dto);
    Response Update(TDto dto);
    Response Delete(int Id);
    Response<TDto> GetById(int id);
    Response<IEnumerable<TDto>> GetByPredicate(Expression<Func<T, bool>> predicate);
    Response<IEnumerable<TDto>> GetAll();
}