#region Licence
/*
 * The MIT License
 *
 * Copyright (c) 2014, Andrew Gray
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
 * THE SOFTWARE.
 */
#endregion
using QuantumSchool.Core.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace QuantumSchool.Core.DAL {
    public class SchoolRepository {
        private SchoolContext db = new SchoolContext();

        public List<Course> GetCourses(){
            return db.Courses.ToList();
        }

        public Course GetCourseById(int courseId) {
            return db.Courses.Find(courseId);;
        }

        public void CreateCourse(Course course) {
            course.Students = new List<Student>();
            db.Courses.Add(course);
            db.SaveChanges();
        }

        public Course FindCourse(int? id) {
            return db.Courses.Find(id);
        }

        public void EditCourse(Course course) {
            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void DeleteCourse(int id) {
            Course course = FindCourse(id);
            db.Courses.Remove(course);
            db.SaveChanges();
        }

        public Student AddCourseToStudent(int? id) {
            Student student = new Student();
            Course course = FindCourse(id);
            student.Courses = new List<Course>();
            student.Courses.Add(course);
            return student;
        }

        public void AddStudent(Student student) {
            db.Students.Add(student);
            db.SaveChanges();
        }

        public void EditStudent(Student student) {
            db.Entry(student).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Student FindStudent(int? id) {
            return db.Students.Find(id);
        }

        public void DeleteStudent(Student student) {
            db.Entry(student).State = EntityState.Deleted;
            db.SaveChanges();
        }

        public void DeleteStudent(int id) {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
        }

        public void Dispose() {
            //Standard scaffolding method.
            db.Dispose();
        }
    }
}