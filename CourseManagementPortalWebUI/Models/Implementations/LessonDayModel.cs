using CourseManagementPortalWebUI.Models.Interface;

namespace CourseManagementPortalWebUI.Models.Implementations
{
    public class LessonDayModel : IModel
    {
        public int Id { get; set; }
        public int No { get; set; }
        public StudentProgramModel? StudentProgram { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
    }
}
