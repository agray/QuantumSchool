using QuantumSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace QuantumSchool.DAL {
    public class SchoolRepository {
        private SchoolContext db = new SchoolContext();

        public List<Course> GetCourses(){
            return db.Courses.ToList();
        }

        public Course GetCourseById(int courseId) {
            return db.Courses.Find(courseId);;
        }

        public List<Student> GetStudentsByCourseId(int courseId) {
            Course course = GetCourseById(courseId);
            return course.Students.ToList();
        }
    }
}