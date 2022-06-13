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
    public class DepartmanManager : IDepartmanService
    {
        IDepartmanDal _departmanDal;
        public DepartmanManager(IDepartmanDal departmanDal)
        {
            _departmanDal = departmanDal;
        }



        [TransactionAspect(Priority =3)]
        [ValidationAspect(typeof(DepartmanValidator), Priority = 2)]
        [SecuredOperationAspect("DepartmanManager.Add", Priority = 1)]
        public IDataResult<Departman> Add(Departman param)
        {
            _departmanDal.Add(param);
            return new SuccessDataResult<Departman>(param);
        }






        [TransactionAspect(Priority = 3)]
        [ValidationAspect(typeof(DepartmanValidator), Priority = 2)]
        [SecuredOperationAspect("DepartmanManager.Update", Priority = 1)]
        public IResult Update(Departman param)
        {
            _departmanDal.Update(param);
            return new SuccessResult("Ok");
        }







        [TransactionAspect(Priority = 2)]
        [SecuredOperationAspect("DepartmanManager.Delete", Priority = 1)]
        public IResult Delete(int param)
        {
            _departmanDal.Delete(param);
            return new SuccessResult("Ok");
        }









        [CacheAspect(_duration: 120, Priority = 2)]
        [SecuredOperationAspect("DepartmanManager.GetAll", Priority = 1)]
        public  IDataResult<IList<Departman>> GetAll(int skip = 0, int take = 0, bool IsActive = false, int Id=0, string Name="Q", string RootPath="Q")
        {
            return new SuccessDataResult<IList<Departman>>(_departmanDal.GetAllTakeSkip(
                x => x.IsActive || !IsActive
                && (x.Id == Id || Id == 0)
                && (x.Name == Name || Name == "Q") 
                && (x.RootPath == RootPath || RootPath == "Q")
                , skip, take));

        }





        [CacheAspect(_duration:120,Priority = 2)]
        [SecuredOperationAspect("DepartmanManager.GetById", Priority = 1)]
        public  IDataResult<Departman> GetById(int Id)
        {
            return new SuccessDataResult<Departman>(_departmanDal.Get(x => x.Id == Id));
        }


      
    }
} 