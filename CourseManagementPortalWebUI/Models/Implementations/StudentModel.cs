using CourseManagementPortalWebUI.Models.Interface;

namespace CourseManagementPortalWebUI.Models.Implementations
{
    public class StudentModel : IModel
    {
        public int Id { get; set; }
        public int No { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string? FullName => Name + " " + Surname;
    }
}
