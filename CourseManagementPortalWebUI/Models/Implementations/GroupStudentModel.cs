using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalWebUI.Models.Interface;

namespace CourseManagementPortalWebUI.Models.Implementations
{
    public class GroupStudentModel : IModel
    {
        public int Id { get; set; }
        public int No { get; set; }
        public StudentModel? Student { get; set; }
        public StudentProgramModel? Group { get; set; }
    }
}
