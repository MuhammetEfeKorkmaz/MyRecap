using BackEndShared.Core.Aspects.Secured;
using Recap.Business.Abstract;
using Recap.Business.ValidationRules.FluentValidation;
using Recap.Core.Aspects.Caching;
using Recap.Core.Aspects.Transaction;
using Recap.Core.Aspects.Validation;
using Recap.Core.Entities.Concrete;
using Recap.Dal.Abstract;
using Recap.Entities.Results;

namespace Recap.Business.Concrete
{
    public class UserOperationClaimManager : IUserOperationClaimService
    {
        IUserOperationClaimDal  _userOperationClaimDal;
        public UserOperationClaimManager(IUserOperationClaimDal userOperationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
        }


        [TransactionAspect(Priority = 3)]
        [ValidationAspect(typeof(UserOperationClaimValidator), Priority = 2)]
        [SecuredOperationAspect("UserOperationClaimManager.Add", Priority = 1)]
        public IDataResult<UserOperationClaim> Add(UserOperationClaim param)
        {
            _userOperationClaimDal.Add(param);
            return new SuccessDataResult<UserOperationClaim>(param);
        }



        [TransactionAspect(Priority = 3)]
        [ValidationAspect(typeof(UserOperationClaimValidator), Priority = 2)]
        [SecuredOperationAspect("UserOperationClaimManager.Update", Priority = 1)]
        public IResult Update(UserOperationClaim param)
        {
            _userOperationClaimDal.Update(param);
            return new SuccessResult("Ok");
        }



        [TransactionAspect(Priority = 2)]
        [SecuredOperationAspect("UserOperationClaimManager.Delete", Priority = 1)]
        public IResult Delete(int param)
        {
            _userOperationClaimDal.Delete(param);
            return new SuccessResult("Ok");
        }




        [CacheAspect(Priority = 2)]
        [SecuredOperationAspect("UserOperationClaimManager.GetAll", Priority = 1)]
        public IList<UserOperationClaim> GetAll(bool IsActive = false, int Id = 0, int UserId = 0, int OperationClaimId = 0)
        {
            return
                  _userOperationClaimDal.GetAll(x => x.IsActive || !IsActive
                   && (x.UserId == UserId || UserId == 0)
                   && (x.OperationClaimId == OperationClaimId || OperationClaimId == 0));
        }




        [CacheAspect(Priority = 2)]
        [SecuredOperationAspect("UserOperationClaimManager.GetById", Priority = 1)]
        public UserOperationClaim GetById(int Id)
        {
            return _userOperationClaimDal.Get(x => x.Id == Id);
        }

       
    }
}
