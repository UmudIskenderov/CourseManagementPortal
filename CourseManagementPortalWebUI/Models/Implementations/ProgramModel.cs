using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalWebUI.Models.Interface;

namespace CourseManagementPortalWebUI.Models.Implementations
{
    public class ProgramModel : IModel
    {
        public int Id { get; set; }
        public int No { get; set; }
        public CourseModel? Course { get; set; }
        public TeacherModel? Teacher { get; set; }
    }
}
