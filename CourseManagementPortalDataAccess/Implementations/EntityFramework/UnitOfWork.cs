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
        private readonly string _connectionString;
        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }
        public IAttendanceRepository AttendanceRepository => new EFAttendanceRepository(_connectionString);

        public ICourseRepository CourseRepository => new EFCourseRepository(_connectionString);

        public IStudentProgramRepository StudentProgramRepository => new EFStudentProgramRepository(_connectionString);

        public ILessonDayRepository LessonDayRepository => new EFLessonDayRepository(_connectionString);

        public IProgramRepository ProgramRepository => new EFProgramRepository(_connectionString);

        public IStudentRepository StudentRepository => new EFStudentRepository(_connectionString);

        public ITeacherRepository TeacherRepository => new EFTeacherRepository(_connectionString);
    }
}
