using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.ViewModels
{
    public class StudentProgramViewModel
    {
        public StudentProgramViewModel() 
        {
            StudentPrograms = new List<StudentProgramModel>();
        }
        
        public List<StudentProgramModel> StudentPrograms { get; set; }
    }
}
