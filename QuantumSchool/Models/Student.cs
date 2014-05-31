using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantumSchool.Models {
    public class Student {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int StudentID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public int Age { get; set; }
        [Required]
        public decimal GPA { get; set; }
        [Required]
        public virtual ICollection<Course> Courses { get; set; }

        public Student() {
            Courses = new HashSet<Course>();
        }
    }
}