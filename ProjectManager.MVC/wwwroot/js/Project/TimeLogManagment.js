$("#showLogTimeForm").on('click', function () {
    const form = $("#logTimeForm");
    form.toggleClass('d-none');
});

$('#logTimeForm button[type="submit"]').on('click', function (e) {
    e.preventDefault();
    const requestData = {
        CommitMessage: $('input[name="CommitMessage"]').val(),
        LoggedInTaskId: $('input[name="TaskId"]').val(),
        LoggedById: $('input[name="UserId"]').val(),
        TimeSpent: $('input[name="TimeSpent"]').val()
    }
    console.log(requestData);


    $.ajax({
        url: `/ProjectTask/LogTime`,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(requestData),
        success: function (response) {
            ReloadTaskDetails("success", response.message);
        },
        error: function (xhr) {
            const response = xhr.responseJSON;
            if (response && response.message) {
                ReloadTaskDetails("error", response.message);
            } else {
                ReloadTaskDetails("error", "Unexpected error occurred");
            }
        }
    })
});