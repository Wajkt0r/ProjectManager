$(document).ready(function () {

    LoadProjectTasks()

    $("#createProjectTaskServiceModal form").submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (data) {
                toastr["success"]("Created Task")
                LoadProjectTasks()
                $('#createProjectTaskServiceModal').modal('hide');
            },
            error: function () {
                toastr["error"]("Something went wrong")
            }
        })
    });
});

const projectTaskContainer = $("#tasks")
const projectEncodedName = projectTaskContainer.data('encodedName');

const TaskProgressStatus = {
    0: "InProgress",
    1: "Completed",
    2: "Cancelled"
};

const colors = {
    InProgress: 'bg-info',
    Cancelled: 'bg-danger',
    Completed: 'bg-success'
};


const RenderProjectTasks = (tasks, container) => {
    container.empty();

    for (const task of tasks) {
        const formattedDeadline = new Date(task.deadline).toLocaleString('pl-PL', { day: 'numeric', month: 'numeric', year: 'numeric', hour: '2-digit', minute: '2-digit' });
        let dateColor;
        if (new Date(task.deadline) < new Date()) {
            dateColor = 'bg-danger';
        } else {
            dateColor = 'bg-primary';
        }
        const statusString = TaskProgressStatus[task.taskProgressStatus];
        const statusColor = colors[statusString] || 'bg-secondary';
        container.append(
            `<li class="list-group-item d-flex justify-content-between align-items-center">
                <div class="task-details">
                    <h5 class="task-name" style="display: inline-block;">${task.name}</h5>
                    <span class="task-status badge ${statusColor}">${statusString}</span>
                    <p class="task-description">${task.description}</p>
                </div>
                <div class="task-actions">
                    <span class="badge ${dateColor} rounded-pill">${formattedDeadline}</span>
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
            toastr["error"]("Something went wrongf")
        }
    })
}

projectTaskContainer.on('click', '.edit-button', function () {
    const taskId = $(this).attr('data-taskId');
    window.location.href = `/ProjectTask/${taskId}/Edit`;
});

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
            toastr["error"]("Deleted task")
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