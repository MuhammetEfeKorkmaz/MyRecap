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
    public class OperationClaimManager : IOperationClaimService
    {
        IOperationClaimDal  _operationClaimDal;
        public OperationClaimManager(IOperationClaimDal operationClaimDal)
        {
            _operationClaimDal = operationClaimDal;
        }


        [TransactionAspect(Priority = 3)]
        [ValidationAspect(typeof(OperationClaimValidator), Priority = 2)]
        [SecuredOperationAspect("OperationClaimManager.Add", Priority = 1)]
        public IDataResult<OperationClaim> Add(OperationClaim param)
        {
            _operationClaimDal.Add(param);
            return new SuccessDataResult<OperationClaim>(param);
        }



        [TransactionAspect(Priority = 3)]
        [ValidationAspect(typeof(OperationClaimValidator), Priority = 2)]
        [SecuredOperationAspect("OperationClaimManager.Update", Priority = 1)]
        public IResult Update(OperationClaim param)
        {
            _operationClaimDal.Update(param);
            return new SuccessResult("Ok");
        }



        [TransactionAspect(Priority = 2)]
        [SecuredOperationAspect("OperationClaimManager.Delete", Priority = 1)]
        public IResult Delete(int param)
        {
            _operationClaimDal.Delete(param);
            return new SuccessResult("Ok");
        }




        [CacheAspect(Priority = 2)]
        [SecuredOperationAspect("OperationClaimManager.GetAll", Priority = 1)]
        public IList<OperationClaim> GetAll(int skip = 0, int take = 0, bool IsActive = false, int Id = 0, string Name = "Q")
        {
            return
                 _operationClaimDal.GetAllTakeSkip(x => x.IsActive || !IsActive
                   && (x.Id == Id || Id == 0)
                   && (x.Name == Name || Name == "Q"), skip, take);
        }



        [CacheAspect(Priority = 2)]
        [SecuredOperationAspect("OperationClaimManager.GetById", Priority = 1)]
        public OperationClaim GetById(int Id)
        {
            return  _operationClaimDal.Get(x => x.Id == Id);
        }



        [CacheAspect(Priority = 2)]
        [SecuredOperationAspect("OperationClaimManager.GetClaims", Priority = 1)]
        public IList<OperationClaim> GetClaims(User user)
        {
            return _operationClaimDal.GetAll(x => x.Id == user.Id);
        }

       
    }
}
