using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;
using CourseManagementPortalWebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementPortalWebUI.Controllers
{
    public class ProgramsController : Controller
    {
        private readonly IProgramService _programService;
        private readonly ITeacherService _teacherService;
        private readonly ICourseService _courseService;
        public ProgramsController(IProgramService programService, ITeacherService teacherService, 
                                  ICourseService courseService)
        {
            _programService = programService;
            _teacherService = teacherService;
            _courseService = courseService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var programModels = _programService.GetAll();

            var programViewModel = new ProgramViewModel();

            programViewModel.Programs = programModels;

            return View(programViewModel);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            var courseModels = _courseService.GetAll();
            var teacherModels = _teacherService.GetAll();

            var addUpdateProgramViewModel = new AddUpdateProgramViewModel();
            addUpdateProgramViewModel.Teachers = teacherModels;
            addUpdateProgramViewModel.Courses = courseModels;

            if (id != 0)
            {
                var programModel = _programService.GetById(id);

                if (programModel == null)
                    return NotFound("Program not found");

                addUpdateProgramViewModel.ProgramId = programModel.Id;
                addUpdateProgramViewModel.SelectedCourseId = programModel.Course.Id;
                addUpdateProgramViewModel.SelectedTeacherId = programModel.Teacher.Id;

                return View(addUpdateProgramViewModel);
            }
            else
            {
                return View(addUpdateProgramViewModel);
            }
        }

        [HttpPost]
        public IActionResult Create(AddUpdateProgramViewModel addUpdateProgramViewModel)
        {
            if (ModelState.IsValid == false)
            {
                return View(addUpdateProgramViewModel);
            }
            ProgramModel programModel = new ProgramModel();

            if(addUpdateProgramViewModel.ProgramId != 0) programModel.Id = addUpdateProgramViewModel.ProgramId;
            programModel.Course.Id = addUpdateProgramViewModel.SelectedCourseId;
            programModel.Teacher.Id = addUpdateProgramViewModel.SelectedTeacherId;

            int id = _programService.Save(programModel);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var program = _programService.GetById(id);
            var success = false;
            if(program != null)
                success = _programService.Delete(program);

            if (success)
                return Ok();

            return BadRequest();
        }
    }
}
