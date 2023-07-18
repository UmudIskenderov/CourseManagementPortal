using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Services.Interfaces
{
    public interface ITeacherService
    {
        List<TeacherModel> GetAll();
        TeacherModel? GetById(int id);
        int Save(TeacherModel model);
        bool Delete(TeacherModel model);
    }
}
