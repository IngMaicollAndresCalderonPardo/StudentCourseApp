using StudentCourseApp.Domain;
using StudentCourseApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourseApp.Application.Services
{
    public class CourseService
    {
        private readonly ICourseRepository _courseRepo;
        private readonly IUnitOfWork _unitOfWork;

        public CourseService(ICourseRepository courseRepo, IUnitOfWork unitOfWork)
        {
            _courseRepo = courseRepo;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Course>> GetAllAsync()
        {
            return await _courseRepo.GetAllAsync();
        }

        public async Task<Course?> GetByIdAsync(int id)
        {
            return await _courseRepo.GetByIdAsync(id);
        }

        public async Task CreateAsync(Course course)
        {
            await _courseRepo.AddAsync(course);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<bool> ExistsByNameOrCodeAsync(string name,string code)
        {
            return await _courseRepo.ExistsByNameOrCodeAsync(name,code);
        }


        public async Task UpdateAsync(Course course)
        {
            await _courseRepo.UpdateAsync(course);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            await _courseRepo.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

    }
}
