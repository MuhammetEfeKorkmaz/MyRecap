using Erp.Dal.Concrete.Factories.EntityFramework;
using Recap.Core.DataAccess.EntityFramework;
using Recap.Core.Entities.Concrete;
using Recap.Dal.Abstract;

namespace Recap.Dal.Concrete.Factories.EntityFramework
{
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, ReCapContext>, IOperationClaimDal
    {

    }
}
