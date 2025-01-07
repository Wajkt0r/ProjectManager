$(document).ready(function () {  

    LoadProjectContributorsToForm()

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
    0: "Not Assigned",
    1: "In Progress",
    2: "Completed",
};

const colors = {
    'Not Assigned': 'bg-light',
    'In Progress': 'bg-info',
    'Completed': 'bg-success'
};


const RenderProjectTasks = (tasks, container, myTasks) => {
    container.empty();
    container.append(`
                <div class="text-center align-items-center justify-content-center mb-3">
                    <button class="btn btn-sm ${myTasks ? "btn-outline-primary" : "btn-primary"} all-tasks">All Tasks</button>
                    <button class="btn btn-sm ${myTasks ? "btn-primary" : "btn-outline-primary"} my-tasks">My Tasks</button>
                </div>
    `);

    if (tasks.length === 0) {
        container.append(myTasks
            ? "There are no tasks in this project for you."
            : "There are no tasks for this project."
        );
        return;
    }

    for (const task of tasks) {
        const formattedDeadline = new Date(task.deadline).toLocaleString('pl-PL', { day: 'numeric', month: 'numeric', year: 'numeric', hour: '2-digit', minute: '2-digit' });
        let dateColor;
        if (new Date(task.deadline) < new Date()) {
            dateColor = 'text-danger';
        } else {
            dateColor = 'text-primary';
        }
        const statusString = TaskProgressStatus[task.taskProgressStatus];
        const statusColor = colors[statusString] || 'bg-secondary';
        container.append(`
            <div class="card mb-3 shadow-sm">
                <div class="card-header d-flex justify-content-between align-items-center">
                    <span class="fw-bold">${task.name}</span>
                    <span class="badge ${statusColor}">${statusString}</span>
                </div>
                <div class="card-body">
                    <p class="card-text"><strong>Assigned to:</strong> ${task.assignedUserEmail || 'Unassigned'}</p>
                    <p class="card-text ${dateColor}"><strong>Deadline:</strong> ${formattedDeadline}</p>
                </div>
                <div class="card-footer d-flex justify-content-end gap-2">
                    <button class="btn btn-sm btn-outline-primary details-button" data-taskId="${task.id}">Details</button>
                    ${isEditable ? `<button class="btn btn-sm btn-outline-danger delete-button" data-taskId="${task.id}">Delete</button>` : ''}
                </div>
            </div>
        `);
    }
}

const LoadProjectTasks = () => {
    $.ajax({
        url: `/Project/${projectEncodedName}/Tasks/GetTasks`,
        type: 'get',
        success: function (data) {
            RenderProjectTasks(data, projectTaskContainer, false)
        },
        error: function () {
            toastr["error"]("Something went wrong")
        }
    })
}

const LoadUserTasks = () => {
    $.ajax({
        url: `/Project/${projectEncodedName}/Tasks/GetTasks/${userEmail}`,
        type: 'get',
        success: function (data) {
            RenderProjectTasks(data, projectTaskContainer, true)
        },
        error: function () {
            toastr["error"]("Something went wrong")
        }
    })
}

const LoadProjectContributorsToForm = () => {
    $.ajax({
        url: `/Project/${projectEncodedName}/ProjectContributors/Get`,
        type: 'GET',
        success: function (data) {
            const select = $("#AssignedUserEmail");
            select.empty();
            select.append('<option value="">-- Select Contributor --</option>');
            data.forEach(user => {
                select.append(`<option value="${user.email}">${user.userName}</option>`);
            });
        },
        error: function () {
            toastr["error"]("Failed to load contributors");
        }
    })
}

projectTaskContainer.on('click', '.details-button', function () {
    const taskId = $(this).attr('data-taskId');
    window.location.href = `/Project/${projectEncodedName}/Tasks/${taskId}/Details?isEditable=${isEditable}`;
});

projectTaskContainer.on('click', '.my-tasks', function () {
    LoadUserTasks();
})

projectTaskContainer.on('click', '.all-tasks', function () {
    LoadProjectTasks();
})

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
