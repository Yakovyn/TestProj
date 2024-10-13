$(document).ready(function () {
    $('.chosen-select').select2();
    $("#Employee_FirstName").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "/Employee/GetEmployeeNames",
                type: "GET",
                dataType: "json",
                data: {
                    term: request.term
                },
                success: function (data) {
                    response(data);
                },
                error: function () {
                    response([]);
                }
            });
        },
        minLength: 2
    });
});