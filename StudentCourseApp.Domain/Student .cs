using System.ComponentModel.DataAnnotations;

namespace StudentCourseApp.Domain
{
    public class Student
    {
        public int Id { get; set; }
        
        [Required, StringLength(100)]
        public string Name { get; set; } = null!;

        [Required, StringLength(20)]
        public string Document { get; set; } = null!;

        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        private readonly List<StudentCourse> _enrollments = new();

        public IReadOnlyCollection<StudentCourse> Enrollments => _enrollments;

        public void Enroll(Course course)
        {
            if (course == null)
                throw new ArgumentNullException(nameof(course));

            if (_enrollments.Any(e => e.CourseId == course.Id))
                throw new InvalidOperationException("El estudiante ya tiene inscrita esta materia.");

            int totalCredits = _enrollments.Sum(e => e.Course.Credits);
            if (totalCredits + course.Credits > 12)
                throw new InvalidOperationException("No puedes inscribir más de 12 créditos en total.");

            int highCreditCourses = _enrollments.Count(e => e.Course.Credits > 4);
            if (course.Credits > 4 && highCreditCourses >= 3)
                throw new InvalidOperationException("No puedes inscribir más de 3 materias con más de 4 créditos.");

            

            _enrollments.Add(new StudentCourse(this, course));
        }

        // Constructor sin parámetros requerido por EF Core
        public Student() { }

    }
}
