﻿using CourseManagementPortalWebUI.Models.Implementations;
using CourseManagementPortalWebUI.Services.Implementations;
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
        public IActionResult Get()
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
                CourseModel courseModel = new CourseModel();
                return View(courseModel);
            }
        }

        [HttpPost]
        public IActionResult Save(CourseModel courseModel)
        {
            if (ModelState.IsValid == false)
            {
                return View(courseModel);
            }

            int id = _courseService.Save(courseModel);
                        
            return RedirectToAction("Get");
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var course = _courseService.GetById(id);
            var success = false;
            if(course != null)
                success = _courseService.Delete(course);

            if (success)
                return Ok();

            return BadRequest();
        }
    }
}
