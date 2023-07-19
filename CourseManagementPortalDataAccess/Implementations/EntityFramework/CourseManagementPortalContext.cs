using CourseManagementPortalDataAccess.Factories;
using CourseManagementPortalEntities.Entities;
using Microsoft.EntityFrameworkCore;

namespace CourseManagementPortalDataAccess.Implementations.EntityFramework
{
    public class CourseManagementPortalContext : DbContext
    {
        public DbSet<Course> Courses { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<StudentProgram> StudentPrograms { get; set; }
        public DbSet<LessonDay> LessonDays { get; set; }
        public DbSet<Attendance> Attendances { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            DbFactory.Create(optionsBuilder);
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
        }
    }
}