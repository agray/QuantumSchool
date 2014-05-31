using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuantumSchool.Models {
    public class Course {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int CourseID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        public string Teacher { get; set; }
        [Required]
        public virtual ICollection<Student> Students { get; set; }

        public Course() {
            Students = new HashSet<Student>();
        }
    }
}