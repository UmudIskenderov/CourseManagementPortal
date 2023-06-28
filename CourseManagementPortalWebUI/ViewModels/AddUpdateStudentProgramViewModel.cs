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
        }

        public int StudentProgramId { get; set; }
        public int SelectedStudentId { get; set; }
        public int SelectedTeacherId { get; set; }
        public int SelectedCourseId { get; set; }
        public int FirstDayOfWeek { get; set; }
        public int LastDayOfWeek { get; set; }
        public DateTime StartDate { get; set; }
        public List<StudentModel> Students { get; set; }
        public List<TeacherModel> Teachers { get; set; }
        public List<CourseModel> Courses { get; set; }             
    }
}
