using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.ViewModels
{
    public class AttendanceViewModel
    {
        public AttendanceViewModel() 
        {
            Attendances = new List<AttendanceModel>();
        }
        public List<AttendanceModel> Attendances { get; set; }
    }
}
