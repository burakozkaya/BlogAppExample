﻿using BlogAppExample.BLL.Abstract;
using BlogAppExample.BLL.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlogAppExample.BLL.Extension;

public static class BLLDependencies
{
    public static void AddBLLDependencies(this IServiceCollection service)
    {
        service.AddAutoMapper(Assembly.GetExecutingAssembly());
        service.AddScoped(typeof(IBlogContentService), typeof(BlogContentManager));
        service.AddScoped(typeof(ICategoryService), typeof(CategoryManager));
        service.AddScoped(typeof(IEmailService), typeof(EMailManager));
        service.AddScoped(typeof(IAccountService), typeof(AccountManager));


        service.AddSingleton(typeof(IUrlHelperFactory), typeof(UrlHelperFactory));

        service.AddSingleton(typeof(IActionContextAccessor), typeof(ActionContextAccessor));
    }
}