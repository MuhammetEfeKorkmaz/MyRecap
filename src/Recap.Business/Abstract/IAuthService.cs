using Recap.Core.Aspects.Secured.Jwt;
using Recap.Core.Entities.Concrete;
using Recap.Entities.Dtos;
using Recap.Entities.Results;

namespace Recap.Business.Abstract
{
    public interface IAuthService
    {  
        IDataResult<User> LoginEmailPassword(UserForLoginDto param);

        IResult UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User param);
    }
}
