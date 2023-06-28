using CourseManagementPortalWebUI.Models.Interface;

namespace CourseManagementPortalWebUI.Models.Implementations
{
    public class TeacherModel : IModel
    {
        public int Id { get; set; }
        public int No { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string? Profession { get; set; }
        public string? FullName => Name + " " + Surname;
    }
}
