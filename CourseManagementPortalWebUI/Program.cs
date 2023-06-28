using CourseManagementPortalCore.DataAccess.Implementations.SqlServer;
using CourseManagementPortalCore.DataAccess.Interfaces;
using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalWebUI.Mappers.Implementations;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddTransient<IUnitOfWork>((serviceProvider) =>
{
    return new SqlUnitOfWork(connectionString);
});

builder.Services.AddScoped<IBaseMapper<Course, CourseModel>, CourseMapper>();
builder.Services.AddScoped<IBaseMapper<Teacher, TeacherModel>, TeacherMapper>();
builder.Services.AddScoped<IBaseMapper<CourseManagementPortalCore.Domain.Entities.Program, ProgramModel>, ProgramMapper>();
builder.Services.AddScoped<IBaseMapper<Student, StudentModel>, StudentMapper>();
builder.Services.AddScoped<IBaseMapper<StudentProgram, StudentProgramModel>, StudentProgramMapper>();
builder.Services.AddScoped<IBaseMapper<Attendance, AttendanceModel>, AttendanceMapper>();
builder.Services.AddScoped<IBaseMapper<LessonDay, LessonDayModel>, LessonDayMapper>();


builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<ITeacherService, TeacherService>();
builder.Services.AddScoped<IProgramService, ProgramService>();
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentProgramService, StudentProgramService>();
builder.Services.AddScoped<IAttendanceService, AttendanceService>();
builder.Services.AddScoped<ILessonDayService, LessonDayService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
