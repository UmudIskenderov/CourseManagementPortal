using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.ViewModels
{
    public class LessonDayViewModel
    {
        public LessonDayViewModel() 
        {
            LessonDayModels = new List<LessonDayModel>();
        }
        public List<LessonDayModel> LessonDayModels { get; set; }
        public string? Message { get; set; }
    }
}
