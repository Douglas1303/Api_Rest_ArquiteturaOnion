using FluentValidation;

namespace Poc.Domain.Entities.Validations
{
    public class CategoryModelValidator : AbstractValidator<CategoryModel>
    {
        public CategoryModelValidator()
        {
            RuleFor(x => x.Descricao)
                .NotEmpty()
                .NotNull()
                .MinimumLength(2)
                .MaximumLength(300);
        }
    }
}