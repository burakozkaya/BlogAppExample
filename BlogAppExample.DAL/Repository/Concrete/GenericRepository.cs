using BlogAppExample.DAL.Context;
using BlogAppExample.DAL.Repository.Abstract;
using BlogAppExample.Entity.Abstract;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogAppExample.DAL.Repository.Concrete;

public class GenericRepository<T> : IGenericRepository<T> where T : class, IBaseEntity
{
    protected readonly AppDbContext _dbContext;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = _dbContext.Set<T>();
    }
    public void Insert(T entity)
    {
        _dbSet.Add(entity);
    }

    public void Delete(T entity)
    {
        _dbSet.Remove(entity);
    }

    public void Update(T entity)
    {
        _dbSet.Update(entity);
    }

    public virtual T? GetById(int id)
    {
        return _dbSet.Find(id);
    }

    public virtual IEnumerable<T> GetByPredicate(Expression<Func<T, bool>> predicate)
    {
        return _dbSet.Where(predicate).ToList();
    }

    public virtual IEnumerable<T> GetAll()
    {
        return _dbSet.ToList();
    }
}