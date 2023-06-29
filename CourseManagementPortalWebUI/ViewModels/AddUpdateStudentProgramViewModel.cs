using CourseManagementPortalWebUI.Models.Implementations;
using System.ComponentModel.DataAnnotations;

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
        public List<StudentModel> Students { get; set; }
        public List<TeacherModel> Teachers { get; set; }
        public List<CourseModel> Courses { get; set; }
        public int SelectedStudentId { get; set; }
        public int SelectedTeacherId { get; set; }
        public int SelectedCourseId { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime StartDate { get; set; }
        public int FirstDayOfWeek { get; set; }
        public int LastDayOfWeek { get; set; }
    }
}
