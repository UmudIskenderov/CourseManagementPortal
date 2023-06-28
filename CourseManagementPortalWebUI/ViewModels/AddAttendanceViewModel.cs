using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.ViewModels
{
    public class AddAttendanceViewModel
    {
        public int StudentProgramId { get; set; }
        public string? Note { get; set; }
        public bool IsParticipated { get; set; }
    }
}
