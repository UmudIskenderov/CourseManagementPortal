using CourseManagementPortalEntities.Entities;
using CourseManagementPortalDataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalDataAccess.Implementations.EntityFramework
{
    public class EFProgramRepository : EfEntityRepositoryBase<Program>, IProgramRepository
    {
        public EFProgramRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
