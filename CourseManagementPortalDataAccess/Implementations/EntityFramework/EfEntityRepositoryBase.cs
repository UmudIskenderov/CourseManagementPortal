using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CourseManagementPortalEntities.Entities;
using CourseManagementPortalDataAccess.Extensions;
using CourseManagementPortalDataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using CourseManagementPortalEntities.Interfaces;

namespace CourseManagementPortalDataAccess.Implementations.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public int Insert(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                return context.SaveChanges();
            }
        }

        public bool Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;
                return context.SaveChanges() != 0 ? true : false;
            }
        }

        public bool Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;
                return context.SaveChanges() != 0 ? true : false;
            }
        }

        public TEntity? Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().FirstOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>>? filter = null)
        {
            using (TContext context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public int GetNextId()
        {
            using (TContext context = new TContext())
            {
                var result = context.Set<TEntity>().ToList()
                    .Select(t => t.GetType().GetProperties()[0]
                    .GetValue(t)).LastOrDefault() as int?;

                //var obj = context.Set<TEntity>().LastOrDefault().GetType().GetProperties()[0];
                //var temp = obj.GetValue(obj) as int?;

                return result + 1 ?? 1;
            }
        }

        public void DeleteAll()
        {
            using (TContext context = new TContext())
            {
                context.Set<TEntity>().Clear();
                context.SaveChanges();
            }
        }
    }
}