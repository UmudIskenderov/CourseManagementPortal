using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.ViewModels
{
    public class AddUpdateProgramViewModel
    {
        public AddUpdateProgramViewModel() 
        {
            Courses = new List<CourseModel>();
            Teachers = new List<TeacherModel>();
        }

        public List<CourseModel> Courses { get; set; }
        public List<TeacherModel> Teachers { get; set; }
        public int SelectedCourseId { get; set; }
        public int SelectedTeacherId { get; set; }
        public int ProgramId { get; set; }
    }
}
