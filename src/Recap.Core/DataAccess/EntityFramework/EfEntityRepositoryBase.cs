using Microsoft.EntityFrameworkCore;
using Recap.Core.Aspects.Performance;
using Recap.Core.DataAccess;
using Recap.Core.Entities;
using System.Linq.Expressions;

namespace Recap.Core.DataAccess.EntityFramework
{


    public class EfEntityRepositoryBase<TEntity, TContext> : IRepositoryBase<TEntity>
   where TEntity : class, IEntity, new()
   where TContext : DbContext, new()
    {

        [DeveloperDBPerformanceAspect]
        public TEntity Add(TEntity entity)
        {
            using TContext context = new TContext();
            var addedEntity = context.Entry(entity);
            addedEntity.State = EntityState.Added;
            return context.SaveChanges() > 0 ? entity : null;
        }


        [DeveloperDBPerformanceAspect]
        public bool Delete(int entity)
        {
            using TContext context = new TContext();
            var model = context.Set<TEntity>().FromSqlRaw($"Select top 1 * from {typeof(TEntity).Name} where Id={entity}").FirstOrDefault();
            if (model == null) return false;
            model.GetType().GetProperty("IsActive").SetValue(entity, false);
            var deletedEntity = context.Entry(model);
            deletedEntity.State = EntityState.Modified;
            return context.SaveChanges() > 0 ? true : false;
        }


        [DeveloperDBPerformanceAspect]
        public bool Update(TEntity entity)
        {
            using TContext context = new TContext();
            var IdProperty = (int)entity.GetType().GetProperty("Id").GetValue(entity, null);
            var dbKontrol = (context.Set<TEntity>().Select(x => x.GetType().GetProperty("Id").GetValue(x, null)).ToList()).Where(x => (int)x == IdProperty).Count();
            if (dbKontrol == 0)
                return false;

            var updatedEntity = context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            return context.SaveChanges() > 0 ? true : false;
        }



        [DeveloperDBPerformanceAspect]
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using TContext context = new TContext();
            return context.Set<TEntity>().SingleOrDefault(filter);
        }


        [DeveloperDBPerformanceAspect]
        public IList<TEntity> GetAllTakeSkip(Expression<Func<TEntity, bool>> filter = null, int skip = 0, int take = 0)
        {
            using TContext context = new TContext();
            if (take > 0)
                return filter == null
                 ? context.Set<TEntity>().ToList()
                 : context.Set<TEntity>().Where(filter).Skip(skip).Take(take).ToList(); 
            
            return filter == null
               ? context.Set<TEntity>().ToList()
               : context.Set<TEntity>().Where(filter).ToList();

        }

        [DeveloperDBPerformanceAspect]
        public IList<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using TContext context = new TContext();
            return filter == null
               ? context.Set<TEntity>().ToList()
               : context.Set<TEntity>().Where(filter).ToList();

        }








    }
}
