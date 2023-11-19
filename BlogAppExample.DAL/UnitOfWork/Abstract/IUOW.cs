using BlogAppExample.DAL.Repository.Abstract;
using BlogAppExample.Entity.Abstract;

namespace BlogAppExample.DAL.UnitOfWork.Abstract;

public interface IUOW
{
    IBlogContentRepo BlogContentRepo { get; }
    ICategoryRepo CategoryRepo { get; }
    IGenericRepository<T> GetRepository<T>() where T : class, IBaseEntity;
    void SaveChanges();

}