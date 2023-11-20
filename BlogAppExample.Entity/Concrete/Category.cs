using BlogAppExample.Entity.Abstract;

namespace BlogAppExample.Entity.Concrete;

public class Category : IBaseEntity
{
    public int Id { get; set; }
    public string CategoryName { get; set; }
    public string CategoryDescription { get; set; }
    public List<BlogContent> BlogContents { get; set; }
    public List<Category> Categories { get; set; }
}