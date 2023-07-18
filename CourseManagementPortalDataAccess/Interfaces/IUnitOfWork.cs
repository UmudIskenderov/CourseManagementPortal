using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalDataAccess.Interfaces
{
    public interface IUnitOfWork
    {
        IAttendanceRepository AttendanceRepository { get; }
        ICourseRepository CourseRepository { get; }
        IStudentProgramRepository StudentProgramRepository { get; }
        ILessonDayRepository LessonDayRepository { get; }
        IProgramRepository ProgramRepository { get; }
        IStudentRepository StudentRepository { get; }
        ITeacherRepository TeacherRepository { get; }
    }
}
