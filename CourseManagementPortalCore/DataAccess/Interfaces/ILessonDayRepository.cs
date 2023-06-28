using CourseManagementPortalCore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.DataAccess.Interfaces
{
    public interface ILessonDayRepository : IEntityRepository<LessonDay>
    {
        List<LessonDay> GetByDayOfWeek(byte dayOfWeek);
        List<LessonDay> GetByStudentId(int studentId);
    }
}
