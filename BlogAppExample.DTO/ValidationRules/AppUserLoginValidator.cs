using BlogAppExample.DTO.Dtos;
using FluentValidation;

namespace BlogAppExample.DTO.ValidationRules;

public class AppUserLoginValidator : AbstractValidator<AppUserLoginDto>
{
    public AppUserLoginValidator()
    {
        RuleFor(x => x.Email).NotEmpty();
        RuleFor(x => x.Password).NotEmpty();
    }
}