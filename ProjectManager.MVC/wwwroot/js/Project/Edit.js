$(document).ready(function () {




    $("#createProjectTaskServiceModal form").submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {
                toastr["success"]("Created Task")
            },
            error: function () {
                toastr["error"]("Something went wrong")
            }
        })
    });
});