using QuantumSchool.DAL;
using QuantumSchool.Models;
using System.Linq;
using System.Web.Mvc;

namespace QuantumSchool.Controllers {
    public class HomeController : Controller {
        SchoolRepository repository = new SchoolRepository();
        public ActionResult Index() {
            return View(repository.GetCourses());
        }

        public ActionResult About() {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact() {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public string GetCourseName(string courseId) {
            Course course = repository.GetCourseById(int.Parse(courseId));
            return course.Name;
        }

        public JsonResult GetStudentsByCourseId(string courseId) {
            Course course = repository.GetCourseById(int.Parse(courseId));
            var students = course.Students.Select(x => new { StudentId = x.StudentID,
                                                             Name = x.Name,
                                                             Age = x.Age,
                                                             GPA = x.GPA
                                                            });
            return Json(students, JsonRequestBehavior.AllowGet);
        }
    }
}