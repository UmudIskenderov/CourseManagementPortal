using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.ViewModels
{
    public class StudentViewModel
    {
        public StudentViewModel() 
        {
            Students = new List<StudentModel>();
        }

        public List<StudentModel> Students { get; set; }
    }
}
