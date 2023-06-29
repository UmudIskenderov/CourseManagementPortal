using CourseManagementPortalCore.DataAccess.Interfaces;
using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalCore.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseManagementPortalCore.DataAccess.Implementations.SqlServer
{
    public class SqlAttendanceRepository : IAttendanceRepository
    {
        private string _connectionString;
        public SqlAttendanceRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public bool Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"delete from Attendances where Id = @id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    return command.ExecuteNonQuery() == 1;
                }
            }
        }

        public List<Attendance> Get()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select Attendances.Id as AId, StudentPrograms.Id as SPId, Students.Id as SId, Teachers.Id as TId, Courses.Id as CId,
                                   Students.Name as SName, Students.Surname as SSurname, Students.BirthDate as SBirthDate,
                                   Students.CreationTime as SCreationTime, Students.ModificationTime as SModificationTime,
                                   Teachers.Name as TName, Teachers.Surname as TSurname, Teachers.BirthDate as TBirthDate,
                                   Courses.Name as CName, Courses.CreationTime as CCreationTime, Courses.ModificationTime as CModificationTime,
                                   * from Attendances
                                   inner join StudentPrograms on Attendances.StudentProgramId = StudentPrograms.Id
                                   inner join Students on StudentPrograms.StudentId = Students.Id
                                   inner join Teachers on StudentPrograms.TeacherId = Teachers.Id
                                   inner join Courses on StudentPrograms.CourseId = Courses.Id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    List<Attendance> attendances = new List<Attendance>();

                    while (reader.Read())
                    {
                        Attendance attendance = GetAttendance(reader);
                        attendances.Add(attendance);
                    }
                    return attendances;
                }
            }
        }

        public Attendance? GetById(int id)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select Attendances.Id as AId, StudentPrograms.Id as SPId, Students.Id as SId, Teachers.Id as TId, Courses.Id as CId,
                                   Students.Name as SName, Students.Surname as SSurname, Students.BirthDate as SBirthDate,
                                   Students.CreationTime as SCreationTime, Students.ModificationTime as SModificationTime,
                                   Teachers.Name as TName, Teachers.Surname as TSurname, Teachers.BirthDate as TBirthDate,
                                   Courses.Name as CName, Courses.CreationTime as CCreationTime, Courses.ModificationTime as CModificationTime,
                                   * from Attendances
                                   inner join StudentPrograms on Attendances.StudentProgramId = StudentPrograms.Id
                                   inner join Students on StudentPrograms.StudentId = Students.Id
                                   inner join Teachers on StudentPrograms.TeacherId = Teachers.Id
                                   inner join Courses on StudentPrograms.CourseId = Courses.Id where Attendances.Id = @id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", id);
                    SqlDataReader reader = command.ExecuteReader();
                    if (reader.Read() == false)
                        return null;
                    Attendance attendance = GetAttendance(reader);
                    return attendance;
                }
            }
        }

        public List<Attendance> GetByStudentId(int studentId)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"select Attendances.Id as AId, StudentPrograms.Id as SPId, Students.Id as SId, Teachers.Id as TId, Courses.Id as CId,
                                   Students.Name as SName, Students.Surname as SSurname, Students.BirthDate as SBirthDate,
                                   Students.CreationTime as SCreationTime, Students.ModificationTime as SModificationTime,
                                   Teachers.Name as TName, Teachers.Surname as TSurname, Teachers.BirthDate as TBirthDate,
                                   Courses.Name as CName, Courses.CreationTime as CCreationTime, Courses.ModificationTime as CModificationTime,
                                   * from Attendances
                                   inner join StudentPrograms on Attendances.StudentProgramId = StudentPrograms.Id
                                   inner join Students on StudentPrograms.StudentId = Students.Id
                                   inner join Teachers on StudentPrograms.TeacherId = Teachers.Id
                                   inner join Courses on StudentPrograms.CourseId = Courses.Id where StudentPrograms.Id = @studentId";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("studentId", studentId);
                    SqlDataReader reader = command.ExecuteReader();
                    List<Attendance> attendances = new List<Attendance>();

                    while (reader.Read())
                    {
                        Attendance attendance = GetAttendance(reader);
                        attendances.Add(attendance);
                    }
                    return attendances;
                }
            }
        }

        public int Insert(Attendance entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"insert into Attendances output inserted.id 
                                   values(@isParticipated, @note, @date, @studentProgramId)";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    AddParameters(command, entity);
                    return (int)command.ExecuteScalar();
                }
            }
        }

        public bool Update(Attendance entity)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                string cmdText = @"update Attendances set IsParticipated=@isParticipated, Note=@note, Date=@date, StudentProgramId=@studentProgramId where Id=@id";
                using (SqlCommand command = new SqlCommand(cmdText, connection))
                {
                    command.Parameters.AddWithValue("id", entity.Id);
                    AddParameters(command, entity);
                    return command.ExecuteNonQuery() == 1;
                }
            }
        }

        private Attendance GetAttendance(SqlDataReader reader)
        {
            Attendance attendance = new Attendance();
            attendance.Id = reader.GetInt32("AId");
            if (reader.IsDBNull("Note") == false)
                attendance.Note = reader.GetString("Note");
            attendance.Date = reader.GetDateTime("Date");
            attendance.IsParticipated = reader.GetBoolean("IsParticipated");
            attendance.StudentProgram = new StudentProgram()
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

            return attendance;
        }

        private void AddParameters(SqlCommand command, Attendance attendance)
        {
            command.Parameters.AddWithValue("studentProgramId", attendance.StudentProgram?.Id);
            command.Parameters.AddWithValue("date", attendance.Date);
            command.Parameters.AddWithValue("note", attendance.Note ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("isParticipated", attendance.IsParticipated);
        }       
    }
}
