using QuantumSchool.DAL;
using QuantumSchool.Models;
using QuantumSchool.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace QuantumSchool.Controllers {
    public class StudentsController : Controller {
        private SchoolContext db = new SchoolContext();

        // GET: Students
        public ActionResult Index() {
            return View(db.Students.ToList());
        }

        // GET: Students/Details/5
        public ActionResult Details(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if(student == null) {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Students/Create
        public ActionResult Create() {
            Student student = new Student();
            student.Courses = new List<Course>();
            PopulateAssignedCourseData(student);
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,Name,Age,GPA")] Student student, string[] selectedCourses) {
            if(selectedCourses != null) {
                student.Courses = new List<Course>();
                foreach(var course in selectedCourses) {
                    var courseToAdd = db.Courses.Find(int.Parse(course));
                    student.Courses.Add(courseToAdd);
                }
            }
            if(ModelState.IsValid) {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedCourseData(student);
            return View(student);
        }

        // GET: Students/Edit/5
        public ActionResult Edit(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            PopulateAssignedCourseData(student);
            if(student == null) {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,Name,Age,GPA")] Student student, string[] selectedCourses) {
            //if(TryUpdateModel(student, "", new string[] { "Name", "Age", "GPA" })) {
            //    try {
            //        UpdateStudentCourses(selectedCourses, student);
            //        db.Students.Attach(student);
            //        db.Entry(student).State = EntityState.Modified;
            //        db.SaveChanges();
            //        return RedirectToAction("Index");
            //    } catch(RetryLimitExceededException /* dex */) {
            //        //Log the error (uncomment dex variable name and add a line here to write a log.
            //        ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            //    }
            //}
            //PopulateAssignedCourseData(student);
            //return View(student);

            //UpdateStudentCourses(selectedCourses, student);

            if(ModelState.IsValid) {
                //db.Students.Attach(student);
                db.Entry(student).State = EntityState.Deleted;
                db.SaveChanges();
                if(selectedCourses != null) {
                    student.Courses = new List<Course>();
                    foreach(var course in selectedCourses) {
                        var courseToAdd = db.Courses.Find(int.Parse(course));
                        student.Courses.Add(courseToAdd);
                    }
                }
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            PopulateAssignedCourseData(student);
            return View(student);
        }

        private void UpdateStudentCourses(string[] selectedCourses, Student studentToUpdate) {
            //studentToUpdate.Courses = new List<Course>();
            //if(selectedCourses != null) {
            //    foreach(var course in selectedCourses) {
            //        var courseToAdd = db.Courses.Find(int.Parse(course));
            //        studentToUpdate.Courses.Add(courseToAdd);
            //    }
            //}
            HashSet<string> selectedCoursesHS = new HashSet<string>(selectedCourses);
            HashSet<int> studentCourses = new HashSet<int>(studentToUpdate.Courses.Select(c => c.CourseID));
            foreach(var course in db.Courses) {
                if(selectedCoursesHS.Contains(course.CourseID.ToString())) {
                    if(!studentCourses.Contains(course.CourseID)) {
                        studentToUpdate.Courses.Add(course);
                    }
                } else {
                    if(studentCourses.Contains(course.CourseID)) {
                        studentToUpdate.Courses.Remove(course);
                    }
                }
            }
        }

        private void PopulateAssignedCourseData(Student student) {
            var allCourses = db.Courses;
            var studentCourses = new HashSet<int>(student.Courses.Select(c => c.CourseID));
            var viewModel = new List<AssignedCourseData>();
            foreach(var course in allCourses) {
                viewModel.Add(new AssignedCourseData {
                    CourseID = course.CourseID,
                    Title = course.Name,
                    Assigned = studentCourses.Contains(course.CourseID)
                });
            }
            ViewBag.Courses = viewModel;
        }

        // GET: Students/Delete/5
        public ActionResult Delete(int? id) {
            if(id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if(student == null) {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id) {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing) {
            if(disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}