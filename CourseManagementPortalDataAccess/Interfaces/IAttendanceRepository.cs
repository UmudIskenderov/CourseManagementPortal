using CourseManagementPortalEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalDataAccess.Interfaces
{
    public interface IAttendanceRepository : IEntityRepository<Attendance>
    {
    }
}
