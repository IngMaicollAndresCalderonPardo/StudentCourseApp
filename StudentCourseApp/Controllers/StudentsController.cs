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
    public class StudentsController : Controller
    {
        private readonly StudentService _studentService;
        private readonly CourseService _courseService;

        public StudentsController(StudentService studentService, CourseService courseService)
        {
            _studentService = studentService;
            _courseService = courseService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllAsync();
            return View(students);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid) return View(student);

            // Validación de duplicado (ver Parte 2 abajo)
            var existing = await _studentService.ExistsAsync(student.Name, student.Document);
            if (existing)
            {
                ModelState.AddModelError("", "Ya existe un estudiante con ese nombre y documento.");
                return View(student);
            }

            await _studentService.CreateAsync(student);
            TempData["Success"] = "Estudiante creado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student student)
        {
            if (!ModelState.IsValid) return View(student);
            await _studentService.UpdateAsync(student);
            TempData["Success"] = "Estudiante editado correctamente.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            await _studentService.DeleteAsync(id);
            TempData["Success"] = "Estudiante eliminado correctamente.";
            return RedirectToAction(nameof(Index));

        }

        // Enroll course
        public async Task<IActionResult> Enroll(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            var courses = await _courseService.GetAllAsync();

            ViewBag.Student = student;
            ViewBag.Courses = courses;

            return View();
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> Enroll(int studentId, int courseId)
        {
            var student = await _studentService.GetByIdAsync(studentId);
            var course = await _courseService.GetByIdAsync(courseId);

            if (student == null || course == null)
                return NotFound();

            try
            {
                student.Enroll(course);
                await _studentService.UpdateAsync(student);
                TempData["Success"] = "Materia asignada correctamente.";
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                // Recargar datos correctamente
                ViewBag.Student = student;
                ViewBag.Courses = await _courseService.GetAllAsync();

                return View(); // No pasar el modelo directamente
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            var student = await _studentService.GetByIdAsync(id);
            if (student == null)
                return NotFound();

            return View(student);
        }

    }
}
