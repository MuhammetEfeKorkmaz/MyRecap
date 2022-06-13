using Recap.Business.Abstract;
using Recap.Business.ValidationRules.FluentValidation;
using Recap.Core.Aspects.Secured.Hashing;
using Recap.Core.Aspects.Secured.Jwt;
using Recap.Core.Aspects.Validation;
using Recap.Core.Entities.Concrete;
using Recap.Entities.Dtos;
using Recap.Entities.Results;

namespace Recap.Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private IUserService _userService;
        private ITokenHelper _tokenHelper;
        public AuthManager(IUserService userService, ITokenHelper tokenHelper)
        {
            _userService = userService;
            _tokenHelper = tokenHelper;
        }


 

        [ValidationAspect(typeof(UserForLoginDtoValidator), Priority = 2)]
        public IDataResult<User> LoginEmailPassword(UserForLoginDto param)
        {
            var userToCheck = _userService.GetByMail(param.Email);
            if (userToCheck == null || userToCheck.Data == null || !userToCheck.Success)
                return new ErrorDataResult<User>("Kullanıcı bulunamadı");


            if (!HashingHelper.VerifyPasswordHash(param.Password, userToCheck.Data.PasswordHash, userToCheck.Data.PasswordSalt))
                return new ErrorDataResult<User>("Şifre hatalı");


            return new SuccessDataResult<User>(userToCheck.Data, "Sisteme giriş başarılı");
        }





        public IResult UserExists(string email)
        {
            if (_userService.GetByMail(email) != null)
            {
                return new ErrorResult("Bu kullanıcı zaten mevcut");
            }
            return new SuccessResult();
        }




        [ValidationAspect(typeof(DepartmanValidator), Priority = 2)]
        public IDataResult<AccessToken> CreateAccessToken(User param)
        {
            var claims = _userService.GetClaims(param.Id);
            var accessToken = _tokenHelper.CreateToken(param, claims);
            return new SuccessDataResult<AccessToken>(accessToken, "Access token başarıyla oluşturuldu");
        }
    }
}
