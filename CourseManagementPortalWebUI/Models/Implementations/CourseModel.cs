using CourseManagementPortalWebUI.Models.Interface;

namespace CourseManagementPortalWebUI.Models.Implementations
{
    public class CourseModel : IModel
    {
        public int Id { get; set; }
        public int No { get; set; }
        public string? Name { get; set; }
        public byte Duration { get; set; }
        public decimal Price { get; set; }
    }
}
