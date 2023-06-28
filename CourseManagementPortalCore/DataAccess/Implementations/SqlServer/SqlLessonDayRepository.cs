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
    public class SqlLessonDayRepository : ILessonDayRepository
    {
        private string _connectionString;
        public SqlLessonDayRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"delete from LessonDays where Id = @id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    return command.ExecuteNonQuery() == 1;
                }
            }
        }

        public List<LessonDay> Get()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select LessonDays.Id as LId, StudentPrograms.Id as SPId, Students.Id as SId, Teachers.Id as TId, Courses.Id as CId,
                                   Students.Name as SName, Students.Surname as SSurname, Students.BirthDate as SBirthDate,
                                   Students.CreationTime as SCreationTime, Students.ModificationTime as SModificationTime,
                                   Teachers.Name as TName, Teachers.Surname as TSurname, Teachers.BirthDate as TBirthDate,
                                   Courses.Name as CName, Courses.CreationTime as CCreationTime, Courses.ModificationTime as CModificationTime,
                                   * from LessonDays
                                   inner join StudentPrograms on LessonDays.StudentProgramId = StudentPrograms.Id
                                   inner join Students on StudentPrograms.StudentId = Students.Id
                                   inner join Teachers on StudentPrograms.TeacherId = Teachers.Id
                                   inner join Courses on StudentPrograms.CourseId = Courses.Id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<LessonDay> lessonDays = new List<LessonDay>();

                    while (reader.Read())
                    {
                        LessonDay lessonDay = GetLessonDay(reader);
                        lessonDays.Add(lessonDay);
                    }
                    return lessonDays;
                }
            }
        }

        public LessonDay? GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select LessonDays.Id as LId, StudentPrograms.Id as SPId, Students.Id as SId, Teachers.Id as TId, Courses.Id as CId,
                                   Students.Name as SName, Students.Surname as SSurname, Students.BirthDate as SBirthDate,
                                   Students.CreationTime as SCreationTime, Students.ModificationTime as SModificationTime,
                                   Teachers.Name as TName, Teachers.Surname as TSurname, Teachers.BirthDate as TBirthDate,
                                   Courses.Name as CName, Courses.CreationTime as CCreationTime, Courses.ModificationTime as CModificationTime,
                                   * from LessonDays
                                   inner join StudentPrograms on LessonDays.StudentProgramId = StudentPrograms.Id
                                   inner join Students on StudentPrograms.StudentId = Students.Id
                                   inner join Teachers on StudentPrograms.TeacherId = Teachers.Id
                                   inner join Courses on StudentPrograms.CourseId = Courses.Id where LessonDays.Id = @id";                
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() == false)
                        return null;
                    LessonDay lessonDay = GetLessonDay(reader);
                    return lessonDay;
                }
            }
        }

        public List<LessonDay> GetByDayOfWeek(byte dayOfWeek)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select LessonDays.Id as LId, StudentPrograms.Id as SPId, Students.Id as SId, Teachers.Id as TId, Courses.Id as CId,
                                   Students.Name as SName, Students.Surname as SSurname, Students.BirthDate as SBirthDate,
                                   Students.CreationTime as SCreationTime, Students.ModificationTime as SModificationTime,
                                   Teachers.Name as TName, Teachers.Surname as TSurname, Teachers.BirthDate as TBirthDate,
                                   Courses.Name as CName, Courses.CreationTime as CCreationTime, Courses.ModificationTime as CModificationTime,
                                   * from LessonDays
                                   inner join StudentPrograms on LessonDays.StudentProgramId = StudentPrograms.Id
                                   inner join Students on StudentPrograms.StudentId = Students.Id
                                   inner join Teachers on StudentPrograms.TeacherId = Teachers.Id
                                   inner join Courses on StudentPrograms.CourseId = Courses.Id where LessonDays.DayOfWeek = @dayOfWeek";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("dayOfWeek", dayOfWeek);
                    SqlDataReader reader = command.ExecuteReader();
                    List<LessonDay> lessonDays = new List<LessonDay>();

                    while (reader.Read())
                    {
                        LessonDay lessonDay = GetLessonDay(reader);
                        lessonDays.Add(lessonDay);
                    }
                    return lessonDays;
                }
            }
        }

        public List<LessonDay> GetByStudentId(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select LessonDays.Id as LId, StudentPrograms.Id as SPId, Students.Id as SId, Teachers.Id as TId, Courses.Id as CId,
                                   Students.Name as SName, Students.Surname as SSurname, Students.BirthDate as SBirthDate,
                                   Students.CreationTime as SCreationTime, Students.ModificationTime as SModificationTime,
                                   Teachers.Name as TName, Teachers.Surname as TSurname, Teachers.BirthDate as TBirthDate,
                                   Courses.Name as CName, Courses.CreationTime as CCreationTime, Courses.ModificationTime as CModificationTime,
                                   * from LessonDays
                                   inner join StudentPrograms on LessonDays.StudentProgramId = StudentPrograms.Id
                                   inner join Students on StudentPrograms.StudentId = Students.Id
                                   inner join Teachers on StudentPrograms.TeacherId = Teachers.Id
                                   inner join Courses on StudentPrograms.CourseId = Courses.Id where StudentPrograms.Id = @studentId";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("studentId", studentId);
                    SqlDataReader reader = command.ExecuteReader();
                    List<LessonDay> lessonDays = new List<LessonDay>();

                    while (reader.Read())
                    {
                        LessonDay lessonDay = GetLessonDay(reader);
                        lessonDays.Add(lessonDay);
                    }
                    return lessonDays;
                }
            }
        }

        public int Insert(LessonDay entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"insert into LessonDays output inserted.id 
                                   values(@studentProgramId, @dayOfWeek)";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    AddParameters(command, entity);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public bool Update(LessonDay entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"update LessonDays set StudentProgramId=@studentProgramId, DayOfWeek=@dayOfWeek where Id=@id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", entity.Id);
                    AddParameters(command, entity);
                    return command.ExecuteNonQuery() == 1;
                }
            }
        }

        private LessonDay GetLessonDay(SqlDataReader reader)
        {
            LessonDay lessonDay = new LessonDay();
            lessonDay.Id = reader.GetInt32("LId");
            lessonDay.DayOfWeek = (DayOfWeek)reader.GetByte("DayOfWeek");
            lessonDay.StudentProgram = new StudentProgram()
            {
                Id = reader.GetInt32("SPId"),
                StartDate = reader.GetDateTime("StartDate"),
                EndDate = reader.GetDateTime("EndDate"),
                Student = new Student()
                {
                    Id = reader.GetInt32("SId"),
                    Name = reader.GetString("SName"),
                    Surname = reader.GetString("SSurname"),
                    BirthDate = reader.GetDateTime("SBirthDate"),
                    CreationTime = reader.GetDateTime("SCreationTime"),
                    ModificationTime = reader.GetDateTime("SModificationTime")
                },
                Teacher = new Teacher()
                {
                    Id = reader.GetInt32("TId"),
                    Name = reader.GetString("TName"),
                    Surname = reader.GetString("TSurname"),
                    BirthDate = reader.GetDateTime("TBirthDate"),
                    Profession = reader.GetString("Profession")
                },
                Course = new Course()
                {
                    Id = reader.GetInt32("CId"),
                    Name = reader.GetString("CName"),
                    Duration = reader.GetByte("Duration"),
                    Price = reader.GetDecimal("Price"),
                    CreationTime = reader.GetDateTime("CCreationTime"),
                    ModificationTime = reader.GetDateTime("CModificationTime")
                }
            };

            return lessonDay;
        }

        private void AddParameters(SqlCommand command, LessonDay lessonDay)
        {
            command.Parameters.AddWithValue("studentProgramId", lessonDay.StudentProgram?.Id);
            command.Parameters.AddWithValue("dayOfWeek", (byte)lessonDay.DayOfWeek);
        }        
    }
}
