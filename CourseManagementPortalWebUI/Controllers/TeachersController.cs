﻿using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;
using CourseManagementPortalWebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementPortalWebUI.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherService _teacherService;
        public TeachersController(ITeacherService teacherService) 
        {
            _teacherService = teacherService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var teacherModels = _teacherService.GetAll();

            var teacherViewModel = new TeacherViewModel();

            teacherViewModel.Teachers = teacherModels;

            return View(teacherViewModel);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            if (id != 0)
            {
                var teacherModel = _teacherService.GetById(id);

                if (teacherModel == null)
                    return NotFound("Teacher not found");

                return View(teacherModel);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(TeacherModel teacherModel)
        {
            int id = _teacherService.Save(teacherModel);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var success = _teacherService.Delete(id);

            if (success)
                return Ok();

            return BadRequest();
        }
    }
}
