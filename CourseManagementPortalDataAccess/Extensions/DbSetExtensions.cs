using CourseManagementPortalEntities.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementPortalDataAccess.Extensions
{
    public static class DbSetExtensions
    {
        public static void Clear<T>(this DbSet<T> dbSet) where T : class, IEntity, new()
        {
            dbSet.RemoveRange(dbSet);
        }
    }
}
