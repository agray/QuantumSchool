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
function CourseSelected(row) {
    jQuery(row).addClass("highlight").siblings().removeClass("highlight");

    var url = "/Home/GetCourseName";
    $.get(url, { courseId: row.id }, function (name) {
        $("#course_header").html(name);
    });

    $.getJSON('/Home/GetStudentsByCourseId',
              { courseId: row.id },
              (function(courseId) {
                  return function (students) {
                            debugger;
                            process_students(students, courseId);
                         };
              }(row.id)
              ));
}

function AddCourse() {
    window.location.href = "/Courses/Create";
}

function AddStudent(courseId) {
    debugger;
    window.location.href = "/Students/Add/" + courseId;
}

function process_students(students, courseId) {
    if (students != undefined && students.length != 0) {
        var html = "<table class=\"table\">"
        html += "<tr><th>&nbsp;</th><th>First Name</th><th>Last Name</th><th>Age</th><th>GPA</th><th></th></tr>";
        for (i = 0; i < students.length; i++) {
            var student = students[i];
            html += student.GPA >= 3.2 ? "<tr><td width=\"30px\"><img src=\"Images/dux.jpg\" width=\"30px\"></td>" : "<tr><td>&nbsp;</td>";
            debugger;
            html += "<td>" + student.FirstName + "</td>";
            debugger;
            html += "<td>" + student.LastName + "</td>";
            html += "<td>" + student.Age + "</td>";
            html += "<td>" + student.GPA + "</td>";
            html += "<td><a href='/Students/Edit/" + student.StudentId + "'>Edit</a> | <a href='/Students/Delete/" + student.StudentId + "'>Delete</a></td>";
            html += "</tr>";
        }
        html += "</table>";
    } else {
        html = ("There are no students enrolled in this course.<br/>");
    }
    html += "<button id=\"add_student\" onclick=\"AddStudent(" + courseId + ")\">Add</button>";
    $("#students_div").html(html);
    debugger;
}