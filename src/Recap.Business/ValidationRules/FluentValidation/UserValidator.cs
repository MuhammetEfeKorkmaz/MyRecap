using FluentValidation;
using Recap.Core.Entities.Concrete;

namespace Recap.Business.ValidationRules.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        { 
            RuleFor(c => c.Email).NotEmpty().WithMessage("boş geçilemez.").MaximumLength(4000).WithMessage("en fazla 4000 karakter olabilir.");
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("boş geçilemez.").MaximumLength(4000).WithMessage("en fazla 4000 karakter olabilir.");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("boş geçilemez.").MaximumLength(4000).WithMessage("en fazla 4000 karakter olabilir.");
             
        }
    }

}
