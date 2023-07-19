using CorrelationId;
using CorrelationId.DependencyInjection;
using CourseManagementPortalDataAccess.Implementations.EntityFramework;
using CourseManagementPortalDataAccess.Interfaces;
using CourseManagementPortalEntities.Entities;
using CourseManagementPortalWebUI.Mappers.Implementations;
using CourseManagementPortalWebUI.Mappers.Interfaces;
using CourseManagementPortalWebUI.Middlewares;
using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;
using Library.WebAPI.Middlewares;
using NLog;
using NLog.Web;

var logger = LogManager.Setup().LoadConfigurationFromFile("nLog.config").GetCurrentClassLogger();

try
{
    logger.Log(NLog.LogLevel.Info, "Application started");

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseNLog();

    // Add services to the container.
    builder.Services.AddControllersWithViews();

    //var connectionString = builder.Configuration.GetConnectionString("Default");

    builder.Services.AddDefaultCorrelationId();

    builder.Services.AddTransient<IUnitOfWork>((serviceProvider) =>
    {
        return new UnitOfWork();
    });

    builder.Services.AddScoped<IBaseMapper<Course, CourseModel>, CourseMapper>();
    builder.Services.AddScoped<IBaseMapper<Teacher, TeacherModel>, TeacherMapper>();
    builder.Services.AddScoped<IBaseMapper<CourseManagementPortalEntities.Entities.Program, ProgramModel>, ProgramMapper>();
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

    app.UseMiddleware<HttpLoggerMiddleware>();
    app.UseMiddleware<ErrorHandlerMiddleware>();

    app.UseCorrelationId();
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    logger.Fatal(ex, "Application start-up failed");
}
finally
{
    LogManager.Shutdown();
}