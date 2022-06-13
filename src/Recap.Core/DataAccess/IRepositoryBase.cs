using Recap.Core.Entities;
using System.Linq.Expressions;

namespace Recap.Core.DataAccess
{

    public interface IRepositoryBase<T> where T : class, IEntity, new()
    {
        IList<T> GetAll(Expression<Func<T, bool>> filter = null);
        IList<T> GetAllTakeSkip(Expression<Func<T, bool>> filter = null, int skip = 0, int take = 0);
        
        T Get(Expression<Func<T, bool>> filter);


        T Add(T entity);
        bool Update(T entity);
        bool Delete(int entity);
    }
}
