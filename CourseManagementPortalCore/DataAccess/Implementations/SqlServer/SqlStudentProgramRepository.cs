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
    public class SqlStudentProgramRepository : IStudentProgramRepository
    {
        private string _connectionString;
        public SqlStudentProgramRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                var transaction = connection.BeginTransaction();
                try
                {
                    string cmdText = @"delete from LessonDays where StudentProgramId = @id";
                    using (SqlCommand command = new SqlCommand(cmdText, connection, transaction))
                    {
                        bool isSuccess = false;

                        command.Parameters.AddWithValue("id", id);
                        isSuccess = command.ExecuteNonQuery() == 1;
                        command.Parameters.Clear();

                        command.CommandText = @"delete from Attendances where StudentProgramId = @id";
                        command.Parameters.AddWithValue("id", id);
                        isSuccess = command.ExecuteNonQuery() == 1;
                        command.Parameters.Clear();

                        command.CommandText = @"delete from StudentPrograms where Id = @id";
                        command.Parameters.AddWithValue("id", id);
                        isSuccess = command.ExecuteNonQuery() == 1;
                        command.Parameters.Clear();
                                               
                        transaction.Commit();
                        return isSuccess;
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public List<StudentProgram> Get()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select StudentPrograms.Id as SPId, Students.Id as SId, Teachers.Id as TId, Courses.Id as CId,
                                   Students.Name as SName, Students.Surname as SSurname, Students.BirthDate as SBirthDate,
                                   Students.CreationTime as SCreationTime, Students.ModificationTime as SModificationTime,
                                   Teachers.Name as TName, Teachers.Surname as TSurname, Teachers.BirthDate as TBirthDate,
                                   Courses.Name as CName, Courses.CreationTime as CCreationTime, Courses.ModificationTime as CModificationTime,
                                   * from StudentPrograms
                                   inner join Students on StudentPrograms.StudentId = Students.Id
                                   inner join Teachers on StudentPrograms.TeacherId = Teachers.Id
                                   inner join Courses on StudentPrograms.CourseId = Courses.Id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<StudentProgram> studentPrograms = new List<StudentProgram>();

                    while (reader.Read())
                    {
                        StudentProgram studentProgram = GetStudentProgram(reader);
                        studentPrograms.Add(studentProgram);
                    }
                    return studentPrograms;
                }
            }
        }

        public StudentProgram? GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select StudentPrograms.Id as SPId, Students.Id as SId, Teachers.Id as TId, Courses.Id as CId,
                                   Students.Name as SName, Students.Surname as SSurname, Students.BirthDate as SBirthDate,
                                   Students.CreationTime as SCreationTime, Students.ModificationTime as SModificationTime,
                                   Teachers.Name as TName, Teachers.Surname as TSurname, Teachers.BirthDate as TBirthDate,
                                   Courses.Name as CName, Courses.CreationTime as CCreationTime, Courses.ModificationTime as CModificationTime,
                                   * from StudentPrograms
                                   inner join Students on StudentPrograms.StudentId = Students.Id
                                   inner join Teachers on StudentPrograms.TeacherId = Teachers.Id
                                   inner join Courses on StudentPrograms.CourseId = Courses.Id where StudentPrograms.Id = @id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() == false)
                        return null;
                    StudentProgram studentProgram = GetStudentProgram(reader);
                    return studentProgram;
                }
            }
        }

        public int Insert(StudentProgram entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"insert into StudentPrograms output inserted.id 
                                   values(@startDate, @endDate, @studentId, @teacherId, @courseId)";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    AddParameters(command, entity);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public bool Update(StudentProgram entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"update StudentPrograms set StudentId=@studentId, TeacherId=@teacherId, CourseId=@courseId, 
                                   StartDate=@startDate, EndDate=@endDate where Id=@id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", entity.Id);
                    AddParameters(command, entity);
                    return command.ExecuteNonQuery() == 1;
                }
            }
        }

        private StudentProgram GetStudentProgram(SqlDataReader reader)
        {
            StudentProgram studentProgram = new StudentProgram();
            studentProgram.Id = reader.GetInt32("SPId");
            studentProgram.StartDate = reader.GetDateTime("StartDate");
            studentProgram.EndDate = reader.GetDateTime("EndDate");
            studentProgram.Student = new Student()
            {
                Id = reader.GetInt32("SId"),
                Name = reader.GetString("SName"),
                Surname = reader.GetString("SSurname"),
                BirthDate = reader.GetDateTime("SBirthDate"),
                CreationTime = reader.GetDateTime("SCreationTime"),
                ModificationTime = reader.GetDateTime("SModificationTime")
            };
            studentProgram.Teacher = new Teacher()
            {
                Id = reader.GetInt32("TId"),
                Name = reader.GetString("TName"),
                Surname = reader.GetString("TSurname"),
                BirthDate = reader.GetDateTime("TBirthDate"),
                Profession = reader.GetString("Profession")
            };
            studentProgram.Course = new Course()
            {
                Id = reader.GetInt32("CId"),
                Name = reader.GetString("CName"),
                Duration = reader.GetByte("Duration"),
                Price = reader.GetDecimal("Price"),
                CreationTime = reader.GetDateTime("CCreationTime"),
                ModificationTime = reader.GetDateTime("CModificationTime")
            };

            return studentProgram;
        }

        private void AddParameters(SqlCommand command, StudentProgram studentProgram)
        {
            command.Parameters.AddWithValue("studentId", studentProgram.Student?.Id);
            command.Parameters.AddWithValue("teacherId", studentProgram.Teacher?.Id);
            command.Parameters.AddWithValue("courseId", studentProgram.Course?.Id);
            command.Parameters.AddWithValue("startDate", studentProgram.StartDate);
            command.Parameters.AddWithValue("endDate", studentProgram.EndDate);
        }
    }
}
