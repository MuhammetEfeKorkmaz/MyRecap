using FluentValidation;
using Recap.Entities.Dtos;

namespace Recap.Business.ValidationRules.FluentValidation
{
    public class UserForLoginDtoValidator : AbstractValidator<UserForLoginDto>
    {
        public UserForLoginDtoValidator()
        {
            RuleFor(c => c.Email).NotEmpty().WithMessage("boş geçilemez.").MaximumLength(4000).WithMessage("en fazla 4000 karakter olabilir.");
            RuleFor(c => c.Password).NotEmpty().WithMessage("boş geçilemez.").MaximumLength(4000).WithMessage("en fazla 4000 karakter olabilir.");
        }
    }
}
