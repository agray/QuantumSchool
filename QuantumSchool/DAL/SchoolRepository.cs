using QuantumSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace QuantumSchool.DAL {
    public class SchoolRepository {
        private SchoolContext db = new SchoolContext();

        public List<Course> GetCourses(){
            return db.Courses.ToList();
        }

        public Course GetCourseById(int courseId) {
            return db.Courses.Find(courseId);;
        }

        //public JsonResult GetStudentsByCourseId(int courseId) {
        //    Course course = GetCourseById(courseId);
        //    var students = course.Students.Select(x => new { StudentID = x.StudentID,
        //                                                     Name = x.Name,
        //                                                     Age = x.Age,
        //                                                     GPA = x.GPA });
        //    return Json(students, JsonRequestBehavior.AllowGet);
        //}
    }
}