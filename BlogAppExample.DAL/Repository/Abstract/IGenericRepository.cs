using BlogAppExample.Entity.Abstract;
using System.Linq.Expressions;

namespace BlogAppExample.DAL.Repository.Abstract;

public interface IGenericRepository<T> where T : class, IBaseEntity
{
    void Insert(T entity);
    void Delete(T entity);
    void Update(T entity);
    T? GetById(int id);
    IEnumerable<T> GetByPredicate(Expression<Func<T, bool>> predicate);
    IEnumerable<T> GetAll();
}