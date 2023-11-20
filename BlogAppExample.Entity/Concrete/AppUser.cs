using Microsoft.AspNetCore.Identity;

namespace BlogAppExample.Entity.Concrete;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string SurName { get; set; }
    public string? ProfilePicture { get; set; }
    public string? Description { get; set; }
    public List<Category> Categories { get; set; }
    public List<BlogContent> BlogContents { get; set; }
}