using Erp.Dal.Concrete.Factories.EntityFramework;
using Recap.Core.DataAccess.EntityFramework;
using Recap.Dal.Abstract;
using Recap.Entities.Concrete;

namespace Recap.Dal.Concrete.Factories.EntityFramework
{

    public class EfDepartmanDal : EfEntityRepositoryBase<Departman, ReCapContext>, IDepartmanDal
    {

    }
    
}
