using CourseManagementPortalCore.DataAccess.Interfaces;
using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalCore.Utils;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.DataAccess.Implementations.SqlServer
{
    public class SqlProgramRepository : IProgramRepository
    {
        private string _connectionString;
        public SqlProgramRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"delete from Programs where Id = @id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    return command.ExecuteNonQuery() == 1;
                }
            }
        }

        public List<Program> Get()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select Programs.Id as ProgramId, Courses.Id as CourseId, Teachers.Id as TeacherId,
                                   Courses.Name as CourseName, Teachers.Name as TeacherName, *
                                   from Programs
                                   inner join Courses on Programs.CourseId = Courses.Id
                                   inner join Teachers on Programs.TeacherId = Teachers.Id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<Program> programs = new List<Program>();

                    while (reader.Read())
                    {
                        Program program = GetProgram(reader);
                        programs.Add(program);
                    }
                    return programs;
                }
            }
        }

        public Program? GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select Programs.Id as ProgramId, Courses.Id as CourseId, Teachers.Id as TeacherId,
                                   Courses.Name as CourseName, Teachers.Name as TeacherName, *
                                   from Programs
                                   inner join Courses on Programs.CourseId = Courses.Id
                                   inner join Teachers on Programs.TeacherId = Teachers.Id where Programs.Id = @id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() == false)
                        return null;
                    Program program = GetProgram(reader);
                    return program;
                }
            }
        }

        public int Insert(Program entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"insert into Programs output inserted.id 
                                   values(@courseId, @teacherId)";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    AddParameters(command, entity);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public bool Update(Program entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"update Programs set CourseId=@courseId, TeacherId=@teacherId where Id=@id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", entity.Id);
                    AddParameters(command, entity);
                    return command.ExecuteNonQuery() == 1;
                }
            }
        }

        private Program GetProgram(SqlDataReader reader)
        {
            Program program = new Program();
            program.Id = reader.GetInt32("ProgramId");
            program.Teacher = new Teacher()
            {
                Id = reader.GetInt32("TeacherId"),
                BirthDate = reader.GetDateTime("BirthDate"),
                Name = reader.GetString("TeacherName"),
                Surname = reader.GetString("Surname"),
                Profession = reader.GetString("Profession")
            };
            program.Course = new Course()
            {
                Id = reader.GetInt32("CourseId"),
                Name = reader.GetString("CourseName"),
                Duration = reader.GetByte("Duration"),
                Price = reader.GetDecimal("Price"),
                CreationTime = reader.GetDateTime("CreationTime"),
                ModificationTime = reader.GetDateTime("ModificationTime")
            };

            return program;
        }

        private void AddParameters(SqlCommand command, Program program)
        {
            command.Parameters.AddWithValue("courseId", program.Course?.Id);
            command.Parameters.AddWithValue("teacherId", program.Teacher?.Id);
        }
    }
}
