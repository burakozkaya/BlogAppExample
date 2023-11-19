using BlogAppExample.DAL.Context;
using BlogAppExample.DAL.Repository.Abstract;
using BlogAppExample.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogAppExample.DAL.Repository.Concrete;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepo
{
    public CategoryRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

    public override IEnumerable<Category> GetAll()
    {
        return _dbSet
            .Include(x => x.BlogContents)
            .ToList();
    }

    public override IEnumerable<Category> GetByPredicate(Expression<Func<Category, bool>> predicate)
    {
        return _dbSet
            .Where(predicate)
            .ToList();
    }

    public override Category? GetById(int id)
    {
        return _dbSet.Include(x => x.BlogContents).FirstOrDefault(x => x.Id == id);
    }
}