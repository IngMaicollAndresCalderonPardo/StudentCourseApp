using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudentCourseApp.Application.Services;
using StudentCourseApp.Domain;
using StudentCourseApp.Infrastructure.Persistence;

namespace StudentCourseApp.Controllers
{
    public class CoursesController : Controller
    {
        private readonly CourseService _courseService;

        public CoursesController(CourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllAsync();
            return View(courses);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Course course)
        {
            if (!ModelState.IsValid) return View(course);
            // Verificar si ya existe
            var exists = await _courseService.ExistsByNameOrCodeAsync(course.Name, course.Code);
            if (exists)
            {
                ModelState.AddModelError("", "Ya existe una materia con el mismo nombre o código.");
                return View(course);
            }

            await _courseService.CreateAsync(course);
            TempData["Success"] = "Materia creada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null) return NotFound();
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course course)
        {
            if (!ModelState.IsValid) return View(course);
            await _courseService.UpdateAsync(course);
            TempData["Success"] = "Materia Editada correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound();

            await _courseService.DeleteAsync(id);
            TempData["Success"] = "Materia eliminada correctamente.";
            return RedirectToAction(nameof(Index));
        }

    }
}
