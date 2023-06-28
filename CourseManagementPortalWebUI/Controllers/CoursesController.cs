﻿using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Interfaces;
using CourseManagementPortalWebUI.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CourseManagementPortalWebUI.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseService _courseService;
        public CoursesController(ICourseService courseService) 
        {
            _courseService = courseService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var courseModels = _courseService.GetAll();

            var courseViewModel = new CourseViewModel();
            
            courseViewModel.Courses = courseModels;
            
            return View(courseViewModel);
        }

        [HttpGet]
        public IActionResult Create(int id)
        {
            if (id != 0)
            {
                var courseModel = _courseService.GetById(id);

                if (courseModel == null)
                    return NotFound("Course not found");

                return View(courseModel);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(CourseModel courseModel)
        {
            int id = _courseService.Save(courseModel);

            return RedirectToAction("Index");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var success = _courseService.Delete(id);

            if (success)
                return Ok();

            return BadRequest();
        }
    }
}
