using Microsoft.AspNetCore.Identity;

namespace BlogAppExample.Entity.Concrete;

public class AppUser : IdentityUser
{
    public string Name { get; set; }
    public string SurName { get; set; }
    public List<BlogContent> BlogContents { get; set; }
}