using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalWebUI.Models.Interface;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementPortalWebUI.Models.Implementations
{
    public class AttendanceModel : IModel
    {
        public int Id { get; set; }
        public int No { get; set; }
        public StudentProgramModel? StudentProgram { get; set; }
        public bool IsParticipated { get; set; }
        public string? Note { get; set; }
        public DateTime Date { get; set; }
    }
}
