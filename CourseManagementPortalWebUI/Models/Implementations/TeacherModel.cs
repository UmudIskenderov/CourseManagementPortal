using CourseManagementPortalWebUI.Models.Interface;
using System.ComponentModel.DataAnnotations;

namespace CourseManagementPortalWebUI.Models.Implementations
{
    public class TeacherModel : IModel
    {
        public int Id { get; set; }
        public int No { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Name must be between 3 and 25 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Name must contain only alphabetic characters")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Surname is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Surname must be between 3 and 25 characters")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "Surname must contain only alphabetic characters")]
        public string? Surname { get; set; }

        [Required(ErrorMessage = "Birth date is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM-dd-yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "Profession is required")]
        [StringLength(25, MinimumLength = 3, ErrorMessage = "Profession must be between 3 and 25 characters")]
        public string? Profession { get; set; }
        public string? FullName => Name + " " + Surname;
    }
}
