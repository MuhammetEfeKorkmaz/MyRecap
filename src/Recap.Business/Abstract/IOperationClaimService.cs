using Recap.Core.Entities.Concrete;
using Recap.Entities.Results;

namespace Recap.Business.Abstract
{
    public interface IOperationClaimService
    {
        IDataResult<OperationClaim> Add(OperationClaim param);
        IResult Delete(int param);
        IResult Update(OperationClaim param);



        OperationClaim GetById(int Id);
        IList<OperationClaim> GetAll(int skip = 0, int take = 0, bool IsActive = false, int Id = 0, string Name = "Q");


        

        IList<OperationClaim> GetClaims(User user);
    }
}
