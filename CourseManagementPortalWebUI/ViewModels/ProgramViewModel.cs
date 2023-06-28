using CourseManagementPortalWebUI.Models.Implementations;

namespace CourseManagementPortalWebUI.ViewModels
{
    public class ProgramViewModel
    {
        public ProgramViewModel() 
        {
            Programs = new List<ProgramModel>();
        }
        
        public List<ProgramModel> Programs { get; set; }
    }
}
