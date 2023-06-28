using CourseManagementPortalCore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.DataAccess.Interfaces
{
    public interface IEntityRepository<T> where T : IEntity
    {
        List<T> Get();
        T? GetById(int id);
        int Insert(T entity);
        bool Update(T entity);
        bool Delete(int id);
    }
}
