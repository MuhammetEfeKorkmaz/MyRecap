using FluentValidation;
using Recap.Core.Entities.Concrete;

namespace Recap.Business.ValidationRules.FluentValidation
{
    internal class UserOperationClaimValidator : AbstractValidator<UserOperationClaim>
    {
        public UserOperationClaimValidator()
        {
            RuleFor(c => c.OperationClaimId).GreaterThan(0).WithMessage("yetki kimli bilgisi 0 dan büyük olmalı.");
            RuleFor(c => c.UserId).GreaterThan(0).WithMessage("Kullanıcı kimli bilgisi 0 dan büyük olmalı.");
        }
    }
}
