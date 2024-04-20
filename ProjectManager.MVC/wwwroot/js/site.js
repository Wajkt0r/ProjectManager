const projectTaskContainer = $("#tasks")
const projectEncodedName = projectTaskContainer.data('encodedName');

const RenderProjectTasks = (tasks, container) => {
    container.empty();

    for (const task of tasks) {
        const formattedDeadline = new Date(task.deadline).toLocaleString('pl-PL', { day: 'numeric', month: 'numeric', year: 'numeric', hour: '2-digit', minute: '2-digit' });
        container.append(
            `<li class="list-group-item d-flex justify-content-between align-items-center">
                <div>
                    <h5>${task.name}</h5>
                    <p>${task.description}</p>
                </div>
                <div>
                    <span class="badge bg-primary rounded-pill">${formattedDeadline}</span>
                    <a class="badge bg-light text-dark m-1 edit-button" style="text-decoration: none;" data-taskId="${task.id}">Edit</a>
                    <a class="badge bg-danger rounded-pill m-1 delete-button" style="text-decoration: none;" data-taskId="${task.id}">Delete</a>
                </div>

            </li>`)
    }
}

const LoadProjectTasks = () => {
    $.ajax({
        url: `/Project/${projectEncodedName}/GetTasks`,
        type: 'get',
        success: function (data) {
            if (!data.length) {
                projectTaskContainer.html("There are no tasks for this project")
            } else {
                RenderProjectTasks(data, projectTaskContainer)
            }
        },
        error: function () {
            toastr["error"]("Something went wrong")
        }
    })
}

let isDeleting = false;
projectTaskContainer.on('click', '.delete-button', function () {
    if (isDeleting) {
        return;
    }
    isDeleting = true;
    const taskId = $(this).attr("data-taskId");

    $.ajax({
        url: `/ProjectTask/${taskId}/Delete`,
        type: 'POST',
        success: function (response) {
            toastr["success"]("Deleted task")
            LoadProjectTasks();
        },
        error: function () {
            toastr["error"]("Something went wrong")
        }
    });
    isDeleting = false;
});

$(document).ready(function () {
    LoadProjectTasks();
});
