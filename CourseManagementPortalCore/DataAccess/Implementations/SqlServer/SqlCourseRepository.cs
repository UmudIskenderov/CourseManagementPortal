using CourseManagementPortalCore.DataAccess.Interfaces;
using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalCore.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.DataAccess.Implementations.SqlServer
{
    public class SqlCourseRepository : ICourseRepository
    {
        private string _connectionString;
        public SqlCourseRepository(string connectionString)
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
                    string cmdText = @"delete from LessonDays where StudentProgramId in (select Id from StudentPrograms where CourseId = @id)";                    
                    using (SqlCommand command = new SqlCommand(cmdText, connection, transaction))
                    {
                        bool isSuccess = false;

                        command.Parameters.AddWithValue("id", id);
                        isSuccess = command.ExecuteNonQuery() == 1;
                        command.Parameters.Clear();

                        command.CommandText = @"delete from Attendances where StudentProgramId in (select Id from StudentPrograms where CourseId = @id)";
                        command.Parameters.AddWithValue("id", id);
                        isSuccess = command.ExecuteNonQuery() == 1;
                        command.Parameters.Clear();

                        command.CommandText = @"delete from StudentPrograms where CourseId = @id";
                        command.Parameters.AddWithValue("id", id);
                        isSuccess = command.ExecuteNonQuery() == 1;
                        command.Parameters.Clear();

                        command.CommandText = @"delete from Programs where CourseId = @id";
                        command.Parameters.AddWithValue("id", id);
                        isSuccess = command.ExecuteNonQuery() == 1;
                        command.Parameters.Clear();

                        command.CommandText = @"delete from Courses where Id = @id";
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

        public List<Course> Get()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select * from Courses";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<Course> courses = new List<Course>();

                    while (reader.Read())
                    {
                        Course course = GetCourse(reader);
                        courses.Add(course);
                    }
                    return courses;
                }
            }
        }

        public byte GetDuration(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select Duration from Courses where Courses.Id = @id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() == false)
                    {
                        return 0;
                    }
                    byte duration = reader.GetByte("Duration");
                    return duration;
                }
            }
        }

        public Course? GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select * from Courses where Courses.Id = @id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() == false)
                        return null;
                    Course course = GetCourse(reader);
                    return course;
                }
            }
        }

        public int Insert(Course entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"insert into Courses output inserted.id 
                                   values(@name, @duration, @price, @creationTime, @modificationTime)";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    AddParameters(command, entity);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public bool Update(Course entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"update Courses set Name=@name, Duration=@duration, Price=@price, 
                                   CreationTime=@creationTime, ModificationTime=@modificationTime where Id=@id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", entity.Id);
                    AddParameters(command, entity);
                    return command.ExecuteNonQuery() == 1;
                }
            }
        }

        private Course GetCourse(SqlDataReader reader)
        {
            Course course = new Course();
            course.Id = reader.GetInt32("Id");
            course.Name = reader.GetString("Name");
            course.Duration = reader.GetByte("Duration");
            course.Price = reader.GetDecimal("Price");
            course.CreationTime = reader.GetDateTime("CreationTime");
            course.ModificationTime = reader.GetDateTime("ModificationTime");

            return course;
        }

        private void AddParameters(SqlCommand command, Course course)
        {
            command.Parameters.AddWithValue("name", course.Name);
            command.Parameters.AddWithValue("duration", course.Duration);
            command.Parameters.AddWithValue("price", course.Price);
            command.Parameters.AddWithValue("creationTime", course.CreationTime);
            command.Parameters.AddWithValue("modificationTime", course.ModificationTime);
        }
    }
}
