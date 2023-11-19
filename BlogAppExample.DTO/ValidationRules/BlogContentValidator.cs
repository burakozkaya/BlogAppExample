using BlogAppExample.DTO.Dtos;
using FluentValidation;

namespace BlogAppExample.DTO.ValidationRules;

public class BlogContentValidator : AbstractValidator<BlogContentDTO>
{
    public BlogContentValidator()
    {
        RuleFor(x => x.AppUserId).NotEmpty();
        RuleFor(x => x.CategoryId).NotEmpty();
        RuleFor(x => x.Content).NotEmpty();
        RuleFor(x => x.Title).NotEmpty();
    }
}