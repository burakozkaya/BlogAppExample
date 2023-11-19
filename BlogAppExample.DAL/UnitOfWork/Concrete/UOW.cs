using BlogAppExample.DAL.Context;
using BlogAppExample.DAL.Repository.Abstract;
using BlogAppExample.DAL.Repository.Concrete;
using BlogAppExample.DAL.UnitOfWork.Abstract;
using BlogAppExample.Entity.Abstract;

namespace BlogAppExample.DAL.UnitOfWork.Concrete;

public class UOW : IUOW, IDisposable
{
    public IBlogContentRepo BlogContentRepo { get; }
    public ICategoryRepo CategoryRepo { get; }

    private AppDbContext _context;

    private bool disposed = false;
    public UOW(IBlogContentRepo blogContentRepo, ICategoryRepo categoryRepo, AppDbContext context)
    {
        BlogContentRepo = blogContentRepo;
        CategoryRepo = categoryRepo;
        _context = context;
    }

    public IGenericRepository<T> GetRepository<T>() where T : class, IBaseEntity
    {
        return new GenericRepository<T>(_context);
    }

    public void SaveChanges()
    {
        _context.SaveChanges();
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }

            disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    ~UOW()
    {
        Dispose(false);
    }
}