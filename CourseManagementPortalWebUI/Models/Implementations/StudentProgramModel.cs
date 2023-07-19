using CourseManagementPortalWebUI.Models.Interface;

namespace CourseManagementPortalWebUI.Models.Implementations
{
    public class StudentProgramModel : IModel
    {
        public StudentProgramModel() 
        {
            Student = new StudentModel();
            Teacher = new TeacherModel();
            Course = new CourseModel();
        }
        public int Id { get; set; }
        public int No { get; set; }
        public StudentModel Student { get; set; }
        public TeacherModel Teacher { get; set; }
        public CourseModel Course { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
