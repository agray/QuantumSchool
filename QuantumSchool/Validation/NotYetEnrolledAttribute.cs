//#region Licence
///*
// * The MIT License
// *
// * Copyright (c) 2014, Andrew Gray
// *
// * Permission is hereby granted, free of charge, to any person obtaining a copy
// * of this software and associated documentation files (the "Software"), to deal
// * in the Software without restriction, including without limitation the rights
// * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// * copies of the Software, and to permit persons to whom the Software is
// * furnished to do so, subject to the following conditions:
// *
// * The above copyright notice and this permission notice shall be included in
// * all copies or substantial portions of the Software.
// *
// * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// * THE SOFTWARE.
// */
//#endregion
//using QuantumSchool.DAL;
//using QuantumSchool.Models;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace QuantumSchool.Validation {
//    public class NotYetEnrolledAttribute : ValidationAttribute {
//        private int ThisCourseId;
//        private SchoolRepository repository = new SchoolRepository();
//        public NotYetEnrolledAttribute(int thisCourseId)
//            : base("A student with the last name of {0} is already enrolled in another course.") {
//                ThisCourseId = thisCourseId;
//        }

//        public override string FormatErrorMessage(string name) {
//            return string.Format(ErrorMessageString, name);
//        }

//        protected override ValidationResult IsValid(string lastName, ValidationContext validationContext) {
//            IEnumerable<Course> OtherCourses = repository.GetOtherCourses(ThisCourseId);
//            foreach(Course course in OtherCourses) {
//                ICollection<Student> students = course.Students;
//                foreach(Student student in students) {
//                    if(student.LastName.Equals(lastName)) {
//                        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
//                    }
//                }
//            }
//            return ValidationResult.Success;
//        }
//    }
//}