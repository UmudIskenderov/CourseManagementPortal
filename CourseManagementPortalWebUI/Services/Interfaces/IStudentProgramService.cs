using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Services.Interfaces
{
    public interface IStudentProgramService
    {
        List<StudentProgramModel> GetAll();
        StudentProgramModel? GetById(int id);
        int Save(StudentProgramModel model);
        bool Delete(int id);
    }
}
