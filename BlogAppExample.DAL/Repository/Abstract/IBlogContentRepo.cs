using BlogAppExample.Entity.Concrete;

namespace BlogAppExample.DAL.Repository.Abstract;

public interface IBlogContentRepo : IGenericRepository<BlogContent>
{
    int Count(BlogContent content);
}