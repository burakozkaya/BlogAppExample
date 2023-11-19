using BlogAppExample.Entity.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogAppExample.DAL.Context;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<BlogContent> BlogContents { get; set; }
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}