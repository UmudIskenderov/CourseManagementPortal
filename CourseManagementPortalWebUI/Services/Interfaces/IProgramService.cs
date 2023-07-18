using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Services.Interfaces
{
    public interface IProgramService
    {
        List<ProgramModel> GetAll();
        ProgramModel? GetById(int id);
        int Save(ProgramModel model);
        bool Delete(ProgramModel model);
    }
}
