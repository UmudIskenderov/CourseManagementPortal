using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.ViewModels
{
    public class AddUpdateStudentProgramViewModel
    {
        public AddUpdateStudentProgramViewModel() 
        {
            Students = new List<StudentModel>();
            Teachers = new List<TeacherModel>();
            Courses = new List<CourseModel>();
            SelectedStudent = new StudentModel();
            SelectedTeacher = new TeacherModel();
            SelectedCourse = new CourseModel();
        }

        public int StudentProgramId { get; set; }
        public List<StudentModel> Students { get; set; }
        public List<TeacherModel> Teachers { get; set; }
        public List<CourseModel> Courses { get; set; }
        public StudentModel SelectedStudent { get; set; }
        public TeacherModel SelectedTeacher { get; set; }
        public CourseModel SelectedCourse { get; set; }
        public DateTime StartDate { get; set; }
        public int FirstDayOfWeek { get; set; }
        public int LastDayOfWeek { get; set; }
    }
}
