using BlogAppExample.Entity.Concrete;

namespace BlogAppExample.DAL.Repository.Abstract;

public interface IBlogContentRepo : IGenericRepository<BlogContent>
{
    IEnumerable<BlogContent> GetMostReaded();
    IEnumerable<BlogContent> GetUserBlog(string userId);
}