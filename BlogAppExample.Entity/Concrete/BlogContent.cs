using BlogAppExample.Entity.Abstract;

namespace BlogAppExample.Entity.Concrete;

public class BlogContent : IBaseEntity
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }
    public string AppUserId { get; set; }
    public int CategoryId { get; set; }

    //Nav Property
    public Category Category { get; set; }
    public AppUser AppUser { get; set; }
}