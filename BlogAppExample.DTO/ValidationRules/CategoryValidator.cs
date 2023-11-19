using BlogAppExample.DTO.Dtos;
using FluentValidation;

namespace BlogAppExample.DTO.ValidationRules;

public class CategoryValidator : AbstractValidator<CategoryDTO>
{
    public CategoryValidator()
    {
        RuleFor(x => x.CategoryDescription).NotEmpty();
        RuleFor(x => x.CategoryName).NotEmpty();
    }
}