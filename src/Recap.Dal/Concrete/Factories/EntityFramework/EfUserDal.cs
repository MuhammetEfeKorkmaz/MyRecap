using Erp.Dal.Concrete.Factories.EntityFramework;
using Recap.Core.DataAccess.EntityFramework;
using Recap.Core.Entities.Concrete;
using Recap.Dal.Abstract;

namespace Recap.Dal.Concrete.Factories.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, ReCapContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(int UserId)
        {
            using (var context = new ReCapContext())
            {
                var result = from operationClaim in context.OperationClaim
                             join userOperationClaim in context.UserOperationClaim
                                 on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.UserId == UserId
                             select new OperationClaim { Id = operationClaim.Id, Name = operationClaim.Name };
                return result.ToList();
            }
        }
    }
}
