using Recap.Core.Entities.Concrete;

namespace Recap.Core.Aspects.Secured.Jwt
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}
