using CourseManagementPortalEntities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalDataAccess.Interfaces
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        int Insert(T entity);
        bool Update(T entity);
        bool Delete(T entity);
        T? Get(Expression<Func<T, bool>> filter);
        List<T> GetAll(Expression<Func<T, bool>>? filter = null);
        int GetNextId();
        void DeleteAll();
    }
}
