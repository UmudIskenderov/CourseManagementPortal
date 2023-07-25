using CourseManagementPortalEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementPortalDataAccess.Implementations.EntityFramework
{
    public class CourseManagementPortalContext : DbContext
    {
        private readonly string _connectionString;
        public CourseManagementPortalContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<StudentProgram> StudentPrograms { get; set; }
        public DbSet<LessonDay> LessonDays { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string connectionString = @"Server = localhost; Port=3303; Database = CourseManagementPortal; User ID=root; Password=Umud.2003";
            optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Course>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Duration).IsRequired();
                entity.Property(e => e.Price).IsRequired().HasColumnType("decimal(5, 2)");
                entity.Property(e => e.CreationTime).IsRequired();
                entity.Property(e => e.ModificationTime).IsRequired();
            });

            modelBuilder.Entity<Teacher>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(50);
                entity.Property(e => e.BirthDate).IsRequired();
                entity.Property(e => e.Profession).IsRequired().HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Surname).IsRequired().HasMaxLength(50);
                entity.Property(e => e.BirthDate).IsRequired();
                entity.Property(e => e.CreationTime).IsRequired();
                entity.Property(e => e.ModificationTime).IsRequired();
            });

            modelBuilder.Entity<Program>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Course).WithMany().HasForeignKey(e => e.CourseId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Teacher).WithMany().HasForeignKey(e => e.TeacherId).OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<StudentProgram>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Student).WithMany().HasForeignKey(e => e.StudentId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Teacher).WithMany().HasForeignKey(e => e.TeacherId).OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(e => e.Course).WithMany().HasForeignKey(e => e.CourseId).OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.EndDate).IsRequired();
            });

            modelBuilder.Entity<LessonDay>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.StudentProgram).WithMany().HasForeignKey(e => e.StudentProgramId).OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.DayOfWeek).IsRequired();
            });

            modelBuilder.Entity<Attendance>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.StudentProgram).WithMany().HasForeignKey(e => e.StudentProgramId).OnDelete(DeleteBehavior.Cascade);
                entity.Property(e => e.IsParticipated).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Note).HasMaxLength(500);
            });

            modelBuilder.Entity<Course>().HasData(
                new Course
                {
                    Id = 1,
                    Name = "Back-end development",
                    Duration = 10,
                    Price = 250,
                    CreationTime = new DateTime(2020, 1, 1),
                    ModificationTime = new DateTime(2020, 1, 1)
                },
                new Course
                {
                    Id = 2,
                    Name = "Front-end development",
                    Duration = 6,
                    Price = 200,
                    CreationTime = new DateTime(2020, 1, 1),
                    ModificationTime = new DateTime(2020, 1, 1)
                },
                new Course
                {
                    Id = 3,
                    Name = "UX/UI design",
                    Duration = 8,
                    Price = 300,
                    CreationTime = new DateTime(2021, 3, 30),
                    ModificationTime = new DateTime(2021, 3, 30)
                }
            );

            modelBuilder.Entity<Teacher>().HasData(
                new Teacher
                {
                    Id = 1,
                    Name = "Cavid",
                    Surname = "Əliyev",
                    BirthDate = new DateTime(1998, 12, 21),
                    Profession = "Software engineer"
                },
                new Teacher
                {
                    Id = 2,
                    Name = "Məhəmməd",
                    Surname = "Ağayev",
                    BirthDate = new DateTime(2000, 7, 15),
                    Profession = "UX/UI designer"
                }
            );

            modelBuilder.Entity<Student>().HasData(
                new Student
                {
                    Id = 1,
                    Name = "Səbuhi",
                    Surname = "Məmmədov",
                    BirthDate = new DateTime(2003, 3, 26),
                    CreationTime = new DateTime(2022, 12, 1),
                    ModificationTime = new DateTime(2022, 12, 1)
                },
                new Student
                {
                    Id = 2,
                    Name = "İlham",
                    Surname = "Həsənli",
                    BirthDate = new DateTime(2003, 8, 2),
                    CreationTime = new DateTime(2023, 2, 1),
                    ModificationTime = new DateTime(2023, 2, 1)
                },
                new Student
                {
                    Id = 3,
                    Name = "Ümüd",
                    Surname = "İskəndərov",
                    BirthDate = new DateTime(2003, 3, 18),
                    CreationTime = new DateTime(2023, 5, 1),
                    ModificationTime = new DateTime(2023, 5, 1)
                }
            );

            modelBuilder.Entity<Program>().HasData(
               new Program
               {
                   Id = 1,
                   CourseId = 1,
                   TeacherId = 1
               },
               new Program
               {
                   Id = 2,
                   CourseId = 2,
                   TeacherId = 1
               },
               new Program
               {
                   Id = 3,
                   CourseId = 3,
                   TeacherId = 2
               }
           );

            modelBuilder.Entity<StudentProgram>().HasData(
               new StudentProgram
               {
                   Id = 1,
                   StudentId = 3,
                   CourseId = 2,
                   TeacherId = 1,
                   StartDate = new DateTime(2023, 5, 1),
                   EndDate = new DateTime(2023, 11, 1)
               },
               new StudentProgram
               {
                   Id = 2,
                   StudentId = 1,
                   CourseId = 3,
                   TeacherId = 2,
                   StartDate = new DateTime(2022, 12, 1),
                   EndDate = new DateTime(2023, 8, 1)
               },
               new StudentProgram
               {
                   Id = 3,
                   StudentId = 2,
                   CourseId = 1,
                   TeacherId = 1,
                   StartDate = new DateTime(2023, 2, 1),
                   EndDate = new DateTime(2023, 12, 1)
               }
           );

            modelBuilder.Entity<LessonDay>().HasData(
              new LessonDay
              {
                  Id = 1,
                  StudentProgramId = 1,
                  DayOfWeek = 1
              },
              new LessonDay
              {
                  Id = 2,
                  StudentProgramId = 1,
                  DayOfWeek = 4
              },
              new LessonDay
              {
                  Id = 3,
                  StudentProgramId = 2,
                  DayOfWeek = 2
              },
              new LessonDay
              {
                  Id = 4,
                  StudentProgramId = 2,
                  DayOfWeek = 5
              },
              new LessonDay
              {
                  Id = 5,
                  StudentProgramId = 3,
                  DayOfWeek = 3
              },
              new LessonDay
              {
                  Id = 6,
                  StudentProgramId = 3,
                  DayOfWeek = 6
              }
          );

            modelBuilder.Entity<Attendance>().HasData(
              new Attendance
              {
                  Id = 1,
                  StudentProgramId = 3,
                  Date = new DateTime(2023, 7, 8),
                  IsParticipated = true,
                  Note = "Perfect"
              },
              new Attendance
              {
                  Id = 2,
                  StudentProgramId = 1,
                  Date = new DateTime(2023, 7, 10),
                  IsParticipated = true,
                  Note = "Perfect"
              },
              new Attendance
              {
                  Id = 3,
                  StudentProgramId = 3,
                  Date = new DateTime(2023, 7, 12),
                  IsParticipated = false,
                  Note = "Bad"
              },
              new Attendance
              {
                  Id = 4,
                  StudentProgramId = 1,
                  Date = new DateTime(2023, 7, 13),
                  IsParticipated = false,
                  Note = "Bad"
              },
              new Attendance
              {
                  Id = 5,
                  StudentProgramId = 2,
                  Date = new DateTime(2023, 7, 14),
                  IsParticipated = true,
                  Note = "Perfect"
              },
              new Attendance
              {
                  Id = 6,
                  StudentProgramId = 1,
                  Date = new DateTime(2023, 7, 17),
                  IsParticipated = true
              },
              new Attendance
              {
                  Id = 7,
                  StudentProgramId = 2,
                  Date = new DateTime(2023, 7, 18),
                  IsParticipated = true
              }
          );
        }
    }
}