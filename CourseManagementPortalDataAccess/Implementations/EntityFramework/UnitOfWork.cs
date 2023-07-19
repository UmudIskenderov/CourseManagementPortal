using CourseManagementPortalEntities.Entities;
using CourseManagementPortalDataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalDataAccess.Implementations.EntityFramework
{
    public class UnitOfWork : IUnitOfWork
    {
        public IAttendanceRepository AttendanceRepository => new EFAttendanceRepository();

        public ICourseRepository CourseRepository => new EFCourseRepository();

        public IStudentProgramRepository StudentProgramRepository => new EFStudentProgramRepository();

        public ILessonDayRepository LessonDayRepository => new EFLessonDayRepository();

        public IProgramRepository ProgramRepository => new EFProgramRepository();

        public IStudentRepository StudentRepository => new EFStudentRepository();

        public ITeacherRepository TeacherRepository => new EFTeacherRepository();
    }
}
