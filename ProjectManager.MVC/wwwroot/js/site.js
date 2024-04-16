const RenderProjectTasks = (tasks, container) => {
    container.empty();

    for (const task of tasks) {
        container.append(
            `<li class="list-group-item d-flex justify-content-between align-items-center">
                <div>
                    <h5>${task.name}</h5>
                    <p>${task.description}</p>
                </div>
                <span class="badge bg-primary rounded-pill">${task.deadline}</span>
            </li>`)
    }
}


const LoadProjectTasks = () => {
    const container = $("#tasks")
    const projectEncodedName = container.data("encodedName");

    $.ajax({
        url: `/Project/${projectEncodedName}/GetTasks`,
        type: 'get',
        success: function (data) {
            if (!data.length) {
                container.html("There are no tasks for this project")
            } else {
                RenderProjectTasks(data, container)
            }
        },
        error: function () {
            toastr["error"]("Something went wrong")
        }
    })
}