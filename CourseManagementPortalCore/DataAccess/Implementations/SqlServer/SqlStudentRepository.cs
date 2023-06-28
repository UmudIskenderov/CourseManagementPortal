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
    public class SqlStudentRepository : IStudentRepository
    {
        private string _connectionString;
        public SqlStudentRepository(string connectionString)
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
                    string cmdText = @"delete from LessonDays where StudentProgramId in (select Id from StudentPrograms where StudentId = @id)";
                    using (SqlCommand command = new SqlCommand(cmdText, connection, transaction))
                    {
                        bool isSuccess = false;

                        command.Parameters.AddWithValue("id", id);
                        isSuccess = command.ExecuteNonQuery() == 1;
                        command.Parameters.Clear();

                        command.CommandText = @"delete from Attendances where StudentProgramId in (select Id from StudentPrograms where StudentId = @id)";
                        command.Parameters.AddWithValue("id", id);
                        isSuccess = command.ExecuteNonQuery() == 1;
                        command.Parameters.Clear();

                        command.CommandText = @"delete from StudentPrograms where StudentId = @id";
                        command.Parameters.AddWithValue("id", id);
                        isSuccess = command.ExecuteNonQuery() == 1;
                        command.Parameters.Clear();

                        command.CommandText = @"delete from Students where Id = @id";
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

        public List<Student> Get()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select * from Students";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<Student> students = new List<Student>();

                    while (reader.Read())
                    {
                        Student student = GetStudent(reader);
                        students.Add(student);
                    }
                    return students;
                }
            }
        }

        public Student? GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select * from Students where Students.Id = @id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() == false)
                        return null;
                    Student student = GetStudent(reader);
                    return student;
                }
            }
        }

        public int Insert(Student entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"insert into Students output inserted.id 
                                   values(@name, @surname, @birthDate, @creationTime, @modificationTime)";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    AddParameters(command, entity);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public bool Update(Student entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"update Students set Name=@name, Surname=@surname, BirthDate=@birthDate, 
                                   CreationTime=@creationTime, ModificationTime=@modificationTime where Id=@id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", entity.Id);
                    AddParameters(command, entity);
                    return command.ExecuteNonQuery() == 1;
                }
            }
        }

        private Student GetStudent(SqlDataReader reader)
        {
            Student student = new Student();
            student.Id = reader.GetInt32("Id");
            student.Name = reader.GetString("Name");
            student.Surname = reader.GetString("Surname");
            student.BirthDate = reader.GetDateTime("BirthDate");
            student.CreationTime = reader.GetDateTime("CreationTime");
            student.ModificationTime = reader.GetDateTime("ModificationTime");

            return student;
        }

        private void AddParameters(SqlCommand command, Student student)
        {
            command.Parameters.AddWithValue("name", student.Name);
            command.Parameters.AddWithValue("surname", student.Surname);
            command.Parameters.AddWithValue("birthDate", student.BirthDate);
            command.Parameters.AddWithValue("creationTime", student.CreationTime);
            command.Parameters.AddWithValue("modificationTime", student.ModificationTime);
        }
    }
}
