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
        html += "<tr><th>Name</th><th>Age</th><th>GPA</th><th></th></tr>";
        for (i = 0; i < students.length; i++) {
            var student = students[i];
            html += "<tr><td>" + student.Name + "</td>";
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