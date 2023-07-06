using CourseManagementPortalCore.DataAccess.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.DataAccess.Implementations.SqlServer
{
    public class SqlUnitOfWork : IUnitOfWork
    {
        private readonly string _connectionString;
        public SqlUnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IAttendanceRepository AttendanceRepository => new SqlAttendanceRepository(_connectionString);

        public ICourseRepository CourseRepository => new SqlCourseRepository(_connectionString);

        public IStudentProgramRepository StudentProgramRepository => new SqlStudentProgramRepository(_connectionString);

        public ILessonDayRepository LessonDayRepository => new SqlLessonDayRepository(_connectionString);

        public IProgramRepository ProgramRepository => new SqlProgramRepository(_connectionString);

        public IStudentRepository StudentRepository => new SqlStudentRepository(_connectionString);

        public ITeacherRepository TeacherRepository => new SqlTeacherRepository(_connectionString);
    }
}
