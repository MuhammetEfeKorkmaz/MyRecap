using Recap.Core.DataAccess;
using Recap.Core.Entities.Concrete;

namespace Recap.Dal.Abstract
{
    public interface IUserDal : IRepositoryBase<User>
    {
        List<OperationClaim> GetClaims(int UserId);
    }
}
