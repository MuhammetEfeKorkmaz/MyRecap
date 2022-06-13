using BackEndShared.Core.Aspects.Secured;
using Recap.Business.Abstract;
using Recap.Business.ValidationRules.FluentValidation;
using Recap.Core.Aspects.Caching;
using Recap.Core.Aspects.Transaction;
using Recap.Core.Aspects.Validation;
using Recap.Dal.Abstract;
using Recap.Entities.Concrete;
using Recap.Entities.Results;

namespace Recap.Business.Concrete
{
    public class UnvanManager : IUnvanService
    {
        IUnvanDal  _unvanDal;
        public UnvanManager(IUnvanDal unvanDal)
        {
            _unvanDal = unvanDal;
        }


        [TransactionAspect(Priority = 3)]
        [ValidationAspect(typeof(UnvanValidator), Priority = 2)]
        [SecuredOperationAspect("UnvanManager.Add", Priority = 1)]
        public  IDataResult<Unvan> Add(Unvan param)
        {
             _unvanDal.Add(param);
            return new SuccessDataResult<Unvan>(param);
        }


        [TransactionAspect(Priority = 3)]
        [ValidationAspect(typeof(UnvanValidator), Priority = 2)]
        [SecuredOperationAspect("UnvanManager.Update", Priority = 1)]
        public  IResult Update(Unvan param)
        {
            _unvanDal.Update(param);
            return new SuccessResult("Ok");
        }


        [TransactionAspect(Priority = 2)]
        [SecuredOperationAspect("UnvanManager.Delete", Priority = 1)]
        public IResult Delete(int param)
        {
            _unvanDal.Delete(param);
            return new SuccessResult("Ok");
        }










        [CacheAspect(Priority = 2)]
        [SecuredOperationAspect("UnvanManager.GetAll", Priority = 1)]
        public  IDataResult<IList<Unvan>> GetAll(int skip = 0, int take = 0, bool IsActive = false, int Id=0,string Name="Q")
        {
            return
                new SuccessDataResult<IList<Unvan>>(_unvanDal.GetAllTakeSkip(
                   x => x.IsActive || !IsActive
                   && (x.Id == Id || Id == 0)
                   && (x.Name == Name || Name == "Q")
                   ,skip, take));

        }




        [CacheAspect(Priority = 2)]
        [SecuredOperationAspect("UnvanManager.GetById", Priority = 1)]
        public IDataResult<Unvan> GetById(int Id)
        {
            return new SuccessDataResult<Unvan>( _unvanDal.Get(x => x.Id == Id));
        }
        

      
    }
}
