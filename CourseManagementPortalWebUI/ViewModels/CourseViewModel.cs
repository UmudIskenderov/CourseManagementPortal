using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.ViewModels
{
    public class CourseViewModel
    {
        public CourseViewModel()
        {
            Courses = new List<CourseModel>();
        }
        public List<CourseModel> Courses { get; set; }
    }
}
