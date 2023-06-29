using CourseManagementPortalWebUI.Models.Interface;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementPortalWebUI.Models.Implementations
{
    public class CourseModel : IModel
    {
        public int Id { get; set; }
        public int No { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 25 characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [Range(1, 255, ErrorMessage = "Duration must be between 1 and 255")]
        public byte Duration { get; set; }

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero")]
        public decimal Price { get; set; }
    }
}
