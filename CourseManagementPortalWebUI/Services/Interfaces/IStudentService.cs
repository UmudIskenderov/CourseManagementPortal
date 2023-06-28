using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Services.Interfaces
{
    public interface IStudentService
    {
        List<StudentModel> GetAll();
        StudentModel? GetById(int id);
        int Save(StudentModel model);
        bool Delete(int id);
    }
}
