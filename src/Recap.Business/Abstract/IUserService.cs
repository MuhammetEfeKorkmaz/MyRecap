using Recap.Core.Entities.Concrete;
using Recap.Entities.Results;

namespace Recap.Business.Abstract
{
    public interface IUserService
    {
       
        IDataResult<User> Add(User param);
        IResult PasswordChanged(int UserId, string param);
        IResult PasswordSenderEmail(string email);
        IResult Delete(int param);
        IResult Update(User param);


    
        IDataResult<User> GetById(int Id);
        IDataResult<IList<User>> GetAll(int skip=0,int take=0,bool IsActive = false, int Id = 0, string Isim = "Q", string Soyisim = "Q", string Email = "Q", string SicilNo = "Q", string Pozisyon = "Q", string DahiliNo = "Q", int DepartmanId = 0, int UnvanId = 0);

        List<OperationClaim> GetClaims(int UserId);
        IDataResult<User> GetByMail(string Email);
         
    }
}
