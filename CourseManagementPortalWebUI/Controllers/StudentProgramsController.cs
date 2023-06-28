using CourseManagementPortalCore.Domain.Entities;
using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;
using CourseManagementPortalWebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementPortalWebUI.Controllers
{
    public class StudentProgramsController : Controller
    {
        private readonly IStudentProgramService _studentProgramService;
        private readonly ITeacherService _teacherService;
        private readonly ICourseService _courseService;
        private readonly IStudentService _studentService;
        private readonly ILessonDayService _lessonDayService;
        public StudentProgramsController(IStudentProgramService studentProgramService, ITeacherService teacherService,
                                         ICourseService courseService, IStudentService studentService, ILessonDayService lessonDayService)
        {
            _studentProgramService = studentProgramService;
            _teacherService = teacherService;
            _courseService = courseService;
            _studentService = studentService;
            _lessonDayService = lessonDayService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var studentProgramModels = _studentProgramService.GetAll();

            var studentProgramViewModel = new StudentProgramViewModel();

            studentProgramViewModel.StudentPrograms = studentProgramModels;

            return View(studentProgramViewModel);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {            
            var courseModels = _courseService.GetAll();
            var teacherModels = _teacherService.GetAll();
            var studentModels = _studentService.GetAll();
            
            var addUpdateStudentProgramViewModel = new AddUpdateStudentProgramViewModel();
            addUpdateStudentProgramViewModel.Teachers = teacherModels;
            addUpdateStudentProgramViewModel.Courses = courseModels;
            addUpdateStudentProgramViewModel.Students = studentModels;

            if (id != 0)
            {
                var studentProgramModel = _studentProgramService.GetById(id);

                if (studentProgramModel == null)
                    return NotFound("Student's program not found");

                addUpdateStudentProgramViewModel.StudentProgramId = studentProgramModel.Id;
                addUpdateStudentProgramViewModel.SelectedCourseId = studentProgramModel.Course == null ? 0 : studentProgramModel.Course.Id;
                addUpdateStudentProgramViewModel.SelectedTeacherId = studentProgramModel.Teacher == null ? 0 : studentProgramModel.Teacher.Id;
                addUpdateStudentProgramViewModel.SelectedStudentId = studentProgramModel.Student == null ? 0 : studentProgramModel.Student.Id;
                addUpdateStudentProgramViewModel.StartDate = studentProgramModel.StartDate;

                return View(addUpdateStudentProgramViewModel);
            }
            else
            {
                return View(addUpdateStudentProgramViewModel);
            }
        }

        [HttpPost]
        public IActionResult Create(AddUpdateStudentProgramViewModel addUpdateStudentProgramViewModel)
        {
            if (addUpdateStudentProgramViewModel == null || addUpdateStudentProgramViewModel.SelectedCourseId == 0 || 
                addUpdateStudentProgramViewModel.SelectedTeacherId == 0 || addUpdateStudentProgramViewModel.SelectedStudentId == 0)
                return BadRequest();

            StudentProgramModel studentProgramModel = new StudentProgramModel();

            TeacherModel? teacherModel = _teacherService.GetById(addUpdateStudentProgramViewModel == null ? 0 : addUpdateStudentProgramViewModel.SelectedTeacherId);

            CourseModel? courseModel = _courseService.GetById(addUpdateStudentProgramViewModel == null ? 0 : addUpdateStudentProgramViewModel.SelectedCourseId);

            StudentModel? studentModel = _studentService.GetById(addUpdateStudentProgramViewModel == null ? 0 : addUpdateStudentProgramViewModel.SelectedStudentId);

            if(courseModel != null || teacherModel != null || studentModel != null)
            {
                studentProgramModel.Course = courseModel;
                studentProgramModel.Teacher = teacherModel;
                studentProgramModel.Student = studentModel;
                studentProgramModel.Id = addUpdateStudentProgramViewModel == null ? 0 : addUpdateStudentProgramViewModel.StudentProgramId;
                studentProgramModel.StartDate = addUpdateStudentProgramViewModel == null ? default(DateTime) : addUpdateStudentProgramViewModel.StartDate;
                studentProgramModel.EndDate = studentProgramModel.StartDate.AddMonths(courseModel == null ? 0 : courseModel.Duration);

                int id = _studentProgramService.Save(studentProgramModel);
                studentProgramModel.Id = id;
            }
            var lessonDayModels = new List<LessonDayModel>();
            if (addUpdateStudentProgramViewModel!= null && addUpdateStudentProgramViewModel.StudentProgramId != 0)
            {
                lessonDayModels = _lessonDayService.GetByStudentId(addUpdateStudentProgramViewModel.StudentProgramId);
            }

            LessonDayModel firstLessonDayModel = new LessonDayModel();
            if(lessonDayModels.Count != 0) firstLessonDayModel.Id = lessonDayModels.First().Id;
            firstLessonDayModel.StudentProgram = studentProgramModel;
            firstLessonDayModel.DayOfWeek = addUpdateStudentProgramViewModel == null ? default(DayOfWeek) : (DayOfWeek)addUpdateStudentProgramViewModel.FirstDayOfWeek;
            int firstId = _lessonDayService.Save(firstLessonDayModel);

            LessonDayModel lastLessonDayModel = new LessonDayModel();

            if(lessonDayModels.Count != 0) lastLessonDayModel.Id = lessonDayModels.Last().Id;
            lastLessonDayModel.StudentProgram = studentProgramModel;
            lastLessonDayModel.DayOfWeek = addUpdateStudentProgramViewModel == null ? default(DayOfWeek) : (DayOfWeek)addUpdateStudentProgramViewModel.LastDayOfWeek;
            int lastId = _lessonDayService.Save(lastLessonDayModel);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var success = _studentProgramService.Delete(id);

            if (success)
                return Ok();

            return BadRequest();
        }
    }
}
