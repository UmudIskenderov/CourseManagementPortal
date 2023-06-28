using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.ViewModels
{
    public class TeacherViewModel
    {
        public TeacherViewModel() 
        {
            Teachers = new List<TeacherModel>();
        }
        
        public List<TeacherModel> Teachers { get; set; }
    }
}
