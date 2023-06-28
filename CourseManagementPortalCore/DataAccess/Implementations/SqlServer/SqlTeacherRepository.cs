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
    public class SqlTeacherRepository : ITeacherRepository
    {
        private string _connectionString;
        public SqlTeacherRepository(string connectionString)
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
                    string cmdText = @"delete from LessonDays where StudentProgramId in (select Id from StudentPrograms where TeacherId = @id)";
                    using (SqlCommand command = new SqlCommand(cmdText, connection, transaction))
                    {
                        bool isSuccess = false;

                        command.Parameters.AddWithValue("id", id);
                        isSuccess = command.ExecuteNonQuery() == 1;
                        command.Parameters.Clear();

                        command.CommandText = @"delete from Attendances where StudentProgramId in (select Id from StudentPrograms where TeacherId = @id)";
                        command.Parameters.AddWithValue("id", id);
                        isSuccess = command.ExecuteNonQuery() == 1;
                        command.Parameters.Clear();

                        command.CommandText = @"delete from StudentPrograms where TeacherId = @id";
                        command.Parameters.AddWithValue("id", id);
                        isSuccess = command.ExecuteNonQuery() == 1;
                        command.Parameters.Clear();

                        command.CommandText = @"delete from Programs where TeacherId = @id";
                        command.Parameters.AddWithValue("id", id);
                        isSuccess = command.ExecuteNonQuery() == 1;
                        command.Parameters.Clear();

                        command.CommandText = @"delete from Teachers where Id = @id";
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

        public List<Teacher> Get()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select * from Teachers";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<Teacher> teachers = new List<Teacher>();

                    while (reader.Read())
                    {
                        Teacher teacher = GetTeacher(reader);
                        teachers.Add(teacher);
                    }
                    return teachers;
                }
            }
        }

        public Teacher? GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select * from Teachers where Teachers.Id = @id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() == false)
                        return null;
                    Teacher teacher = GetTeacher(reader);
                    return teacher;
                }
            }
        }

        public int Insert(Teacher entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"insert into Teachers output inserted.id 
                                   values(@name, @surname, @birthDate, @profession)";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    AddParameters(command, entity);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public bool Update(Teacher entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"update Teachers set Name=@name, Surname=@surname, BirthDate=@birthDate, 
                                   Profession=@profession where Id=@id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", entity.Id);
                    AddParameters(command, entity);
                    return command.ExecuteNonQuery() == 1;
                }
            }
        }

        private Teacher GetTeacher(SqlDataReader reader)
        {
            Teacher teacher = new Teacher();
            teacher.Id = reader.GetInt32("Id");
            teacher.Name = reader.GetString("Name");
            teacher.Surname = reader.GetString("Surname");
            teacher.BirthDate = reader.GetDateTime("BirthDate");
            teacher.Profession = reader.GetString("Profession");

            return teacher;
        }

        private void AddParameters(SqlCommand command, Teacher teacher)
        {
            command.Parameters.AddWithValue("name", teacher.Name);
            command.Parameters.AddWithValue("surname", teacher.Surname);
            command.Parameters.AddWithValue("birthDate", teacher.BirthDate);
            command.Parameters.AddWithValue("profession", teacher.Profession);
        }
    }
}
