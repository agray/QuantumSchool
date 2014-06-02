﻿using QuantumSchool.Models;
using QuantumSchool.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace QuantumSchool.Controllers {
    public class StudentsController : ControllerBase {
        // GET: Students/Add/1
        public ActionResult Add(int? id) {
            Student student = new Student();
            Course course = db.Courses.Find(id);
            student.Courses = new List<Course>();
            student.Courses.Add(course);
            PopulateAssignedCourseData(student);
            return View();
        }

        // POST: Students/Add
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add([Bind(Include = "StudentID,Name,Age,GPA")] Student student, string[] selectedCourses) {
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
                return RedirectToAction("Index", "Home");
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
            if(ModelState.IsValid) {
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
                return RedirectToAction("Index", "Home");
            }
            PopulateAssignedCourseData(student);
            return View(student);
        }

        private void UpdateStudentCourses(string[] selectedCourses, Student studentToUpdate) {
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
            DbSet<Course> allCourses = db.Courses;
            HashSet<int> studentCourses = new HashSet<int>(student.Courses.Select(c => c.CourseID));
            List<AssignedCourseData> viewModel = new List<AssignedCourseData>();
            foreach(Course course in allCourses) {
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
            return RedirectToAction("Index", "Home");
        }
    }
}