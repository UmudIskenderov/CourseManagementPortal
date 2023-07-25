using CourseManagementPortalWebUI.Constants;
using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;
using CourseManagementPortalWebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementPortalWebUI.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly IAttendanceService _attendanceService;
        private readonly IStudentProgramService _studentProgramService;
        private readonly ILessonDayService _lessonDayService;

        public AttendancesController(IAttendanceService attendanceService, IStudentProgramService studentProgramService,
                                     ILessonDayService lessonDayService)
        {
            _attendanceService = attendanceService;
            _studentProgramService = studentProgramService;
            _lessonDayService = lessonDayService;
        }

        [HttpGet]
        public IActionResult GetAttendances(int id)
        {            
            var attendanceModels = _attendanceService.GetByStudentId(id);

            var attendanceViewModel = new AttendanceViewModel();

            attendanceViewModel.Attendances = attendanceModels;

            return View(attendanceViewModel);
        }

        [HttpGet]
        public IActionResult Get()
        {            
            var currentDay = DateTime.Now.DayOfWeek;
            var lessonDayModels = _lessonDayService.GetByDayOfWeek(currentDay);
            var lessonDayViewModel = new LessonDayViewModel();
            lessonDayViewModel.LessonDayModels = lessonDayModels;
            if (TempData["Message"] != null) lessonDayViewModel.Message = TempData["Message"]?.ToString();
            return View(lessonDayViewModel);

        }
              

        [HttpGet]
        public IActionResult Create(int id)
        {
            var attendancesModels = _attendanceService.GetByStudentId(id);
            var isHaveAttendanceModels = attendancesModels.Any(x => x.Date.DayOfWeek == DateTime.Now.DayOfWeek && 
                                                                    x.Date.ToString(SystemConstants.DateFormat) == DateTime.Now.ToString(SystemConstants.DateFormat));
            if (isHaveAttendanceModels == true)
            {
                TempData["Message"] = "This already added!";
                return RedirectToAction("Get");
            }
            AddAttendanceViewModel addAttendanceViewModel = new AddAttendanceViewModel();
            var studentProgramModel = _studentProgramService.GetById(id);
            if (studentProgramModel == null)
                return BadRequest();
            addAttendanceViewModel.StudentProgramId = studentProgramModel.Id;

            return View(addAttendanceViewModel);
        }

        [HttpPost]
        public IActionResult Save(AddAttendanceViewModel addAttendanceViewModel)
        {
            if(ModelState.IsValid == false) 
            {
                return View(addAttendanceViewModel);
            }
            var attendanceModel = new AttendanceModel()
            {
                StudentProgram = _studentProgramService.GetById(addAttendanceViewModel.StudentProgramId),
                IsParticipated = addAttendanceViewModel.IsParticipated,
                Note = addAttendanceViewModel.Note
            };
            int id = _attendanceService.Save(attendanceModel);

            return RedirectToAction("Get");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var attendance = _attendanceService.GetById(id);
            var success = false;
            if (attendance != null)
                success = _attendanceService.Delete(attendance);

            if (success)
                return Ok();

            return BadRequest();
        }
    }
}
