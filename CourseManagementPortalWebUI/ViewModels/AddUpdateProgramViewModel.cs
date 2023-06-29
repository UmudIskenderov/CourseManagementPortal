using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.ViewModels
{
    public class AddUpdateProgramViewModel
    {
        public AddUpdateProgramViewModel() 
        {
            Courses = new List<CourseModel>();
            Teachers = new List<TeacherModel>();
            SelectedCourse = new CourseModel();
            SelectedTeacher = new TeacherModel();
        }

        public List<CourseModel> Courses { get; set; }
        public List<TeacherModel> Teachers { get; set; }
        public CourseModel SelectedCourse { get; set; }
        public TeacherModel SelectedTeacher { get; set; }
        public int ProgramId { get; set; }
    }
}
