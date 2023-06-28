using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.Services.Interfaces
{
    public interface ILessonDayService
    {
        List<LessonDayModel> GetAll();
        LessonDayModel? GetById(int id);
        List<LessonDayModel> GetByDayOfWeek(DayOfWeek dayOfWeek);
        List<LessonDayModel> GetByStudentId(int studentId);
        int Save(LessonDayModel model);
        bool Delete(int id);
    }
}
