using CourseManagementPortalDataAccess.Interfaces;
using CourseManagementPortalEntities.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalDataAccess.Implementations.EntityFramework
{
    public class EFAttendanceRepository : EfEntityRepositoryBase<Attendance, CourseManagementPortalContext>, IAttendanceRepository
    {
    }
}
