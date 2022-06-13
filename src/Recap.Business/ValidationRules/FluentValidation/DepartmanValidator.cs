using FluentValidation;
using Recap.Entities.Concrete;

namespace Recap.Business.ValidationRules.FluentValidation
{

    public class DepartmanValidator : AbstractValidator<Departman>
    {
        public DepartmanValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("boş geçilemez.").MaximumLength(4000).WithMessage("en fazla 4000 karakter olabilir.");
            RuleFor(c => c.WinPassword).NotEmpty().WithMessage("boş geçilemez.").MaximumLength(4000).WithMessage("en fazla 4000 karakter olabilir.");
            RuleFor(c => c.WinUserName).NotEmpty().WithMessage("boş geçilemez.").MaximumLength(4000).WithMessage("en fazla 4000 karakter olabilir.");
        }
    }
}
