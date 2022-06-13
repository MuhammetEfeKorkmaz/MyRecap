using Microsoft.EntityFrameworkCore.Design;

namespace Erp.Dal.Concrete.Factories.EntityFramework
{
    public class ReCapContextDesingTimeFactory : IDesignTimeDbContextFactory<ReCapContext>
    {
        public ReCapContext CreateDbContext(string[] args)
        {
            return new ReCapContext();

        }
    }
     
}
