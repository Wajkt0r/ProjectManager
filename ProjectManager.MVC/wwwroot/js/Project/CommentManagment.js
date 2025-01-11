const commentContainer = $("#comments")
const newCommentContainer = $("#addCommentForm")

$(document).ready(function () {
    const toastMessage = localStorage.getItem('toastMessage');
    const toastType = localStorage.getItem('toastType');

    if (toastMessage && toastType) {
        toastr[toastType](toastMessage);

        localStorage.removeItem('toastMessage');
        localStorage.removeItem('toastType');
    }
});

commentContainer.on('click', '.delete-comment-button', function () {
    const entityId = $(this).attr('data-commentId');
    const isComment = ($(this)).attr('type') === 'comment';

    $.ajax({
        url: `/ProjectTask/DeleteEntity?entityId=${entityId}&entityType=${isComment ? `comment` : `logtime`}`,
        type: 'DELETE',
        success: function (response) {
            ReloadTaskDetails("info", response.message);
        },
        error: function (response) {
            ReloadTaskDetails("error", response.message);
        }
    })
});

newCommentContainer.on('click', '.submit-new-comment', function (e) {
    e.preventDefault();
    const requestData = {
        Comment: $('textarea[name="Comment"]').val(),
        ProjectTaskId: parseInt($('input[name="TaskId"]').val()),
        CreatedById: $('input[name="UserId"]').val()
    }

    $.ajax({
        url: `/ProjectTask/AddComment`,
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
})

const ReloadTaskDetails = (actionResult, message) => {
    localStorage.setItem('toastMessage', message);
    localStorage.setItem('toastType', actionResult);

    window.location.href = `/Project/${projectEncodedName}/Tasks/${taskId}/Details?isEditable=${isEditable}`;
}
