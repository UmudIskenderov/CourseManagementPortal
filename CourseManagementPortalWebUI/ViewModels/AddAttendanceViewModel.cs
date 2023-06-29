using CourseManagementPortalWebUI.Models.Implementations;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementPortalWebUI.ViewModels
{
    public class AddAttendanceViewModel
    {
        public int StudentProgramId { get; set; }

        [MaxLength(250, ErrorMessage = "Note must be less than 250 symbols")]
        public string? Note { get; set; }
        public bool IsParticipated { get; set; }
    }
}
