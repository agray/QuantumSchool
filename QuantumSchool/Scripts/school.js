function CourseSelected(row) {
    jQuery(row).addClass("highlight").siblings().removeClass("highlight");

    var url = "/Home/GetCourseName";
    $.get(url, { courseId: row.id }, function (name) {
        $("#course_header").html(name);
    });

    $.getJSON('/Home/GetStudentsByCourseId', { courseId: row.id }, function (students) {
        process_students(students);
    });
}

function AddCourse() {
    window.location.href = "/Courses/Create";
}

function process_students(students) {
    debugger;
    if (students != undefined) {
        var tableHtml = "<table>"
        for (i = 0; i < students.length; i++) {
            var student = students[i];
            tableHtml += "<tr><td>" + student.Name + "</td>";
            tableHtml += "<td>" + student.Age + "</td>";
            tableHtml += "<td>" + student.GPA + "</td>";
            tableHtml += "<td><a href='/Students/Edit/" + student.StudentId + "'>Edit</a> | <a href='/Student/Delete/" + student.StudentId + "'>Delete</a></td>";
            tableHtml += "</tr>";
        }
        tableHtml += "</table>"
        $("#students_div").html(tableHtml);
    }
}