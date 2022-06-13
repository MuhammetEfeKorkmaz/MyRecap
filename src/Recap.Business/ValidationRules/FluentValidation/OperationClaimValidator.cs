using FluentValidation;
using Recap.Core.Entities.Concrete;

namespace Recap.Business.ValidationRules.FluentValidation
{
    public class OperationClaimValidator : AbstractValidator<OperationClaim>
    {
        public OperationClaimValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("boş geçilemez.").MaximumLength(4000).WithMessage("en fazla 4000 karakter olabilir.");
        }
    }
}
