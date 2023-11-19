using BlogAppExample.DAL.Context;
using BlogAppExample.DAL.Repository.Abstract;
using BlogAppExample.DAL.Repository.Concrete;
using BlogAppExample.DAL.UnitOfWork.Abstract;
using BlogAppExample.DAL.UnitOfWork.Concrete;
using BlogAppExample.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
namespace BlogAppExample.DAL.Extension;

public static class DALDependencies
{
    public static void AddDALDependencies(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(x => x.UseSqlServer(connectionString));

        services.AddIdentity<AppUser, AppRole>(x =>
            {
                x.User.RequireUniqueEmail = true;
                x.SignIn.RequireConfirmedAccount = true;
            })
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        services.AddScoped(typeof(IBlogContentRepo), typeof(BlogContentRepository));
        services.AddScoped(typeof(ICategoryRepo), typeof(CategoryRepository));
        services.AddScoped(typeof(IUOW), typeof(UOW));
    }
}