using FluentValidation;
using Recap.Entities.Concrete;

namespace Recap.Business.ValidationRules.FluentValidation
{
    public class UnvanValidator : AbstractValidator<Unvan>
    {
        public UnvanValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("boş geçilemez.").MaximumLength(4000).WithMessage("en fazla 4000 karakter olabilir.");
        }
    }

}
