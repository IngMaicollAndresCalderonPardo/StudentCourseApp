using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentCourseApp.Domain
{
    public class Course
    {
        public int Id { get; set; }

        [Required, StringLength(100)]
        public string Name { get; set; } = null!;

        [Required, StringLength(10)]
        public string Code { get; set; } = null!;

        [Range(1, 10)]
        public int Credits { get; set; }
    }
}
