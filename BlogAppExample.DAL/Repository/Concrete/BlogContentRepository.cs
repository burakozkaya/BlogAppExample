using BlogAppExample.DAL.Context;
using BlogAppExample.DAL.Repository.Abstract;
using BlogAppExample.Entity.Concrete;

namespace BlogAppExample.DAL.Repository.Concrete;

public class BlogContentRepository : GenericRepository<BlogContent>, IBlogContentRepo
{
    public BlogContentRepository(AppDbContext dbContext) : base(dbContext)
    {
    }

}