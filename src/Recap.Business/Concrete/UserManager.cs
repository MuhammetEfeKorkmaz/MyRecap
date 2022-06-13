using BackEndShared.Core.Aspects.Secured;
using Microsoft.Extensions.DependencyInjection;
using Recap.Business.Abstract;
using Recap.Business.ValidationRules.FluentValidation;
using Recap.Core;
using Recap.Core.Aspects.Caching;
using Recap.Core.Aspects.Secured.Hashing;
using Recap.Core.Aspects.Transaction;
using Recap.Core.Aspects.Validation;
using Recap.Core.Entities.Concrete;
using Recap.Core.Utilities.Email;
using Recap.Dal.Abstract;
using Recap.Entities.Results;
using System.Text;

namespace Recap.Business.Concrete
{
    public class UserManager : IUserService
    {
        IUserDal  _systemUserDal;
        IEmailManager _emailManager;
        public UserManager(IUserDal systemUserDal)
        {
            _emailManager = ServiceRegisterationMicrosoftDEPIServiceTool.ServiceProvider.GetService<IEmailManager>();
            
            _systemUserDal = systemUserDal; 
        }

        [TransactionAspect(Priority = 3)]
        [ValidationAspect(typeof(UserValidator),Priority =2)]
        [SecuredOperationAspect("UserManager.Add", Priority = 1)]
        public   IDataResult<User> Add(User param)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(param.Password, out passwordHash, out passwordSalt);
            param.PasswordSalt = passwordSalt;
            param.PasswordHash = passwordHash;
            param.Password = string.Empty;
            _systemUserDal.Add(param);
            return new SuccessDataResult<User>(param);
        }




        [SecuredOperationAspect("UserManager.PasswordChanged", Priority = 1)]
        public IResult PasswordChanged(int UserId,string param)
        {
            User user = _systemUserDal.Get(x => x.Id == UserId);
            if (user==null)
                return new ErrorResult("Kullanıcı Bulunamadı.");
            
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(param, out passwordHash, out passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            user.Password = string.Empty;
            _systemUserDal.Update(user);
            return new SuccessResult();
        }





        [SecuredOperationAspect("UserManager.PasswordSenderEmail", Priority = 1)]
        public IResult PasswordSenderEmail(string email)
        {
            User user = _systemUserDal.Get(x => x.Email == email);
            if (user == null)
                return new ErrorResult("Kullanıcıya Bağlı Email Hesabı Bulunamadı.");

            Random Rnd = new Random();
            StringBuilder StrBuild = new StringBuilder();
            for (int i = 0; i < 8; i++)
            {
                int ASCII = Rnd.Next(32, 127);
                char Karakter = Convert.ToChar(ASCII);
                StrBuild.Append(Karakter);
            }
             
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(StrBuild.ToString(), out passwordHash, out passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;
            user.Password = string.Empty;
            _systemUserDal.Update(user); 
            _emailManager.SenderUser(email, user.FirstName, user.LastName, StrBuild.ToString());
            return new SuccessResult();
        }




        [TransactionAspect(Priority = 3)]
        [ValidationAspect(typeof(UserValidator), Priority = 1)]
        [SecuredOperationAspect("UserManager.Update", Priority = 2)]
        public  IResult Update(User param)
        {
             _systemUserDal.Update(param);
            return new SuccessResult("Ok");
        }




        [TransactionAspect(Priority = 2)]
        [SecuredOperationAspect("UserManager.Delete", Priority = 1)]
        public IResult Delete(int param)
        {
            _systemUserDal.Delete(param);
            return new SuccessResult("Ok");
        }







        [CacheAspect(Priority = 2)]
        [SecuredOperationAspect("UserManager.GetAll", Priority = 1)]
        public  IDataResult<IList<User>> GetAll(int skip=0,int take=0,bool IsActive = false, int Id=0, string FirstName = "Q", string LastName = "Q", string Email = "Q", string SicilNo = "Q", string Pozisyon = "Q", string DahiliNo = "Q", int DepartmanId = 0, int UnvanId = 0)
        {
            return
                  new SuccessDataResult<IList<User>>(_systemUserDal.GetAllTakeSkip(x => x.IsActive || !IsActive
                   && (x.FirstName == FirstName || FirstName == "Q")
                   && (x.LastName == LastName || LastName == "Q")
                   && (x.Email == Email || Email == "Q")
                   && (x.DepartmanId == DepartmanId || DepartmanId == 0)
                   && (x.UnvanId == UnvanId || UnvanId == 0)
                    , skip, take));
                
        }






        [CacheAspect(Priority =2)]
        [SecuredOperationAspect("UserManager.GetById", Priority = 1)]
        public IDataResult<User> GetById(int Id)
        {
            return new SuccessDataResult<User>(_systemUserDal.Get(x => x.Id == Id));
        }





        [CacheAspect(Priority = 2)]
        [SecuredOperationAspect("UserManager.GetByMail", Priority = 1)]
        public IDataResult<User> GetByMail(string Email)
        {
            return new SuccessDataResult<User>(_systemUserDal.Get(x => x.Email == Email));
        }





        [CacheAspect(Priority = 2)]
        [SecuredOperationAspect("UserManager.GetClaims", Priority = 1)]
        public List<OperationClaim> GetClaims(int UserId)
        {
          return _systemUserDal.GetClaims(UserId);
        }
    }
}
