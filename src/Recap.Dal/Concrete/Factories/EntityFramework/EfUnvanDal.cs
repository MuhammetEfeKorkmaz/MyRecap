using Recap.Core.DataAccess.EntityFramework;
using Recap.Dal.Abstract;
using Recap.Entities.Concrete;
using Erp.Dal.Concrete.Factories.EntityFramework; 
 
namespace Recap.Dal.Concrete.Factories.EntityFramework
{
    public class EfUnvanDal : EfEntityRepositoryBase<Unvan, ReCapContext>, IUnvanDal
    {

    }
}
