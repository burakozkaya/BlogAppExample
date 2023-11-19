using BlogAppExample.DTO.ValidationRules;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace BlogAppExample.DTO.Extension;

public static class DtoDependencies
{
    public static void AddDtoDependencies(this IServiceCollection service)
    {
        service.AddValidatorsFromAssemblyContaining<AppUserLoginValidator>();
        service.AddValidatorsFromAssemblyContaining<AppUserRegisterValidator>();
        service.AddValidatorsFromAssemblyContaining<BlogContentValidator>();
        service.AddValidatorsFromAssemblyContaining<CategoryValidator>();
    }
}