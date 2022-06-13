using Recap.Core.Entities.Concrete;
using Recap.Entities.Results;

namespace Recap.Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IDataResult<UserOperationClaim> Add(UserOperationClaim param);
        IResult Delete(int param);
        IResult Update(UserOperationClaim param);



        UserOperationClaim GetById(int Id);
        IList<UserOperationClaim> GetAll(bool IsActive = false, int Id = 0, int UserId = 0, int OperationClaimId = 0);

    }
}
