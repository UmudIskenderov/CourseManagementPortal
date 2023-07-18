using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Services.Interfaces
{
    public interface IAttendanceService
    {
        List<AttendanceModel> GetAll();
        AttendanceModel? GetById(int id);
        List<AttendanceModel> GetByStudentId(int studentId);
        int Save(AttendanceModel model);
        bool Delete(AttendanceModel model);
    }
}
