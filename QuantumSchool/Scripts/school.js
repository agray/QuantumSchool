function CourseSelected(row) {
    debugger;
    jQuery(row).addClass("highlight").siblings().removeClass("highlight");

    var url = "/Home/GetCourseName";
    $.get(url, { courseId: row.id }, function (name) {
        debugger;
        $("#course_header").html(name);
    });
}