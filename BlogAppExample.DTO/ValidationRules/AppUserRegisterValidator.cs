using BlogAppExample.DTO.Dtos;
using FluentValidation;

namespace BlogAppExample.DTO.ValidationRules;

public class AppUserRegisterValidator : AbstractValidator<AppUserRegisterDto>
{
    public AppUserRegisterValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.Name).NotEmpty();
        RuleFor(x => x.SurName).NotEmpty();
        RuleFor(x => x.UserName).NotEmpty();
    }
}