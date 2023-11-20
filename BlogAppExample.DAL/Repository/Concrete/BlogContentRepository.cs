using BlogAppExample.DAL.Context;
using BlogAppExample.DAL.Repository.Abstract;
using BlogAppExample.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BlogAppExample.DAL.Repository.Concrete;

public class BlogContentRepository : GenericRepository<BlogContent>, IBlogContentRepo
{
    public BlogContentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }
    public override IEnumerable<BlogContent> GetAll()
    {
        return _dbSet
            .Include(x => x.Category)
            .ToList();
    }

    public override IEnumerable<BlogContent> GetByPredicate(Expression<Func<BlogContent, bool>> predicate)
    {
        return _dbSet.Include(x => x.Category)
            .Where(predicate)
            .ToList();
    }

    public override BlogContent? GetById(int id)
    {
        return _dbSet.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
    }

    public int Count(BlogContent content)
    {
        content.NumberOfReads++;
        _dbSet.Update(content);
        return content.NumberOfReads;
    }
}