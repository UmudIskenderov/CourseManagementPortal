using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;
using CourseManagementPortalWebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementPortalWebUI.Controllers
{
    public class StudentsController : Controller
    {
        private readonly IStudentService _studentService;
        public StudentsController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var studentModels = _studentService.GetAll();

            var studentViewModel = new StudentViewModel();

            studentViewModel.Students = studentModels;

            return View(studentViewModel);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            if (id != 0)
            {
                var studentModel = _studentService.GetById(id);

                if (studentModel == null)
                    return NotFound("Student not found");

                return View(studentModel);
            }
            else
            {
                StudentModel studentModel = new StudentModel();
                return View(studentModel);
            }
        }

        [HttpPost]
        public IActionResult Save(StudentModel studentModel)
        {
            if (ModelState.IsValid == false)
            {
                return View(studentModel);
            }
            int id = _studentService.Save(studentModel);

            return RedirectToAction("Get");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var student = _studentService.GetById(id);
            var success = false;
            if(student != null)
                success = _studentService.Delete(student);

            if (success)
                return Ok();

            return BadRequest();
        }
    }
}
