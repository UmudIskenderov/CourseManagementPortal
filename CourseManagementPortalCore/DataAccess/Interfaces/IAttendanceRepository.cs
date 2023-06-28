using CourseManagementPortalCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.DataAccess.Interfaces
{
    public interface IAttendanceRepository : IEntityRepository<Attendance>
    {
        List<Attendance> GetByStudentId(int studentId);
    }
}
