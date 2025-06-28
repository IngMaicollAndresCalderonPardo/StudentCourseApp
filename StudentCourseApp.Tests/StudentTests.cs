/*
* StudentCourseApp.Tests
* Autor: Maicoll Calderón
* Descripción: Pruebas unitarias para la lógica de inscripción de estudiantes a materias.
* Fecha: 2025
* 
* Cobertura:
*  - Validaciones de inscripción (límites de créditos, duplicidad, nulos)
*  - Comprobación de datos básicos
*  - Restricciones según reglas del negocio
*/

using StudentCourseApp.Domain;
using System;
using Xunit;

namespace StudentCourseApp.Tests
{
    public class StudentTests
    {
        #region Reglas de Inscripción por Créditos

        [Fact]
        public void Enroll_Should_Allow_Courses_With_Exactly_4_Credits()
        {
            var student = CreateDefaultStudent("Laura");

            student.Enroll(new Course { Id = 1, Name = "Curso1", Code = "C1", Credits = 4 });
            student.Enroll(new Course { Id = 2, Name = "Curso2", Code = "C2", Credits = 4 });
            student.Enroll(new Course { Id = 3, Name = "Curso3", Code = "C3", Credits = 4 });

            Assert.Equal(3, student.Enrollments.Count); // Total 12 créditos, válidos
        }

        [Fact]
        public void Enroll_Should_Throw_When_More_Than_3_Heavy_Courses()
        {
            var student = CreateDefaultStudent("Maicoll");

            student.Enroll(new Course { Id = 1, Name = "Matemáticas", Code = "MAT101", Credits = 5 });
            student.Enroll(new Course { Id = 2, Name = "Física", Code = "FIS101", Credits = 5 });
            student.Enroll(new Course { Id = 3, Name = "Química", Code = "QUI101", Credits = 5 });

            var extraCourse = new Course { Id = 4, Name = "Biología", Code = "BIO101", Credits = 5 };

            var ex = Assert.Throws<InvalidOperationException>(() => student.Enroll(extraCourse));
            Assert.Equal("No puedes inscribir más de 3 materias con más de 4 créditos.", ex.Message);
        }

        [Fact]
        public void Enroll_Should_Throw_When_Total_Credits_Exceed_12()
        {
            var student = CreateDefaultStudent("Limitado");

            student.Enroll(new Course { Id = 1, Name = "Curso1", Code = "C1", Credits = 5 });
            student.Enroll(new Course { Id = 2, Name = "Curso2", Code = "C2", Credits = 5 });
            student.Enroll(new Course { Id = 3, Name = "Curso3", Code = "C3", Credits = 3 });

            var extra = new Course { Id = 4, Name = "CursoExtra", Code = "C4", Credits = 2 };

            var ex = Assert.Throws<InvalidOperationException>(() => student.Enroll(extra));
            Assert.Equal("No puedes inscribir más de 12 créditos en total.", ex.Message);
        }

        [Fact]
        public void Enroll_Should_Allow_Low_Credit_Courses_Without_Limit()
        {
            var student = CreateDefaultStudent("Nancy");

            for (int i = 1; i <= 10; i++)
            {
                student.Enroll(new Course { Id = i, Name = $"Curso{i}", Code = $"C{i}", Credits = 4 });
            }

            Assert.Equal(10, student.Enrollments.Count);
        }

        [Fact]
        public void Enroll_Should_Succeed_With_3_Or_Less_Heavy_Courses()
        {
            var student = CreateDefaultStudent("Andres");

            student.Enroll(new Course { Id = 1, Name = "Curso1", Code = "C1", Credits = 5 });
            student.Enroll(new Course { Id = 2, Name = "Curso2", Code = "C2", Credits = 4 });
            student.Enroll(new Course { Id = 3, Name = "Curso3", Code = "C3", Credits = 4 });

            Assert.Equal(3, student.Enrollments.Count);
        }

        #endregion

        #region Validaciones de Datos

        [Fact]
        public void Student_Should_Have_Valid_Data()
        {
            var student = new Student
            {
                Name = "Alfonso",
                Document = "91005685",
                Email = "alfonso@mail.com"
            };

            Assert.Equal("Alfonso", student.Name);
            Assert.Equal("91005685", student.Document);
            Assert.Equal("alfonso@mail.com", student.Email);
        }

        [Fact]
        public void Enroll_Should_Throw_If_Course_Is_Null()
        {
            var student = CreateDefaultStudent("Leszly");

            Assert.Throws<ArgumentNullException>(() => student.Enroll(null!));
        }

        #endregion

        #region Reglas de Duplicidad

        [Fact]
        public void Enroll_Should_Throw_When_Same_Course_Twice()
        {
            var student = CreateDefaultStudent("Tobby");

            var course = new Course { Id = 1, Name = "Inglés", Code = "ING101", Credits = 3 };
            student.Enroll(course);

            var ex = Assert.Throws<InvalidOperationException>(() => student.Enroll(course));
            Assert.Equal("El estudiante ya tiene inscrita esta materia.", ex.Message);
        }

        #endregion

        #region Helpers

        private Student CreateDefaultStudent(string name)
        {
            return new Student
            {
                Name = name,
                Document = $"{name.GetHashCode()}",
                Email = $"{name.ToLower()}@mail.com"
            };
        }

        #endregion
    }
}
