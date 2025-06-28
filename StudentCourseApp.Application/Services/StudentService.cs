using StudentCourseApp.Domain;
using StudentCourseApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourseApp.Application.Services
{
    public class StudentService
    {
        private readonly IStudentRepository _studentRepo;
        private readonly ICourseRepository _courseRepo;
        private readonly IUnitOfWork _unitOfWork;

        public StudentService(
            IStudentRepository studentRepo,
            ICourseRepository courseRepo,
            IUnitOfWork unitOfWork)
        {
            _studentRepo = studentRepo;
            _courseRepo = courseRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _studentRepo.GetAllAsync();
        }

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _studentRepo.GetByIdAsync(id);
        }

        public async Task CreateAsync(Student student)
        {
            await _studentRepo.AddAsync(student);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string name, string document)
        {
            return await _studentRepo.ExistsAsync(name, document);
        }

        public async Task UpdateAsync(Student student)
        {
            await _studentRepo.UpdateAsync(student);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _studentRepo.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task EnrollCourseAsync(int studentId, int courseId)
        {
            var student = await _studentRepo.GetByIdAsync(studentId);
            var course = await _courseRepo.GetByIdAsync(courseId);

            if (student == null || course == null)
                throw new InvalidOperationException("Estudiante o materia no encontrado");

            student.Enroll(course); // aplica validación de >4 créditos
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
