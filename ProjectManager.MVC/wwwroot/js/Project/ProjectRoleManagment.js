const projectRolesContainer = $("#project-roles");
let roleNameToDelete = "";

$(document).ready(function () {
    LoadProjectRoles();
});

const LoadProjectRoles = () => {
    $.ajax({
        url: `/Project/${projectEncodedName}/ProjectRoles`,
        type: 'GET',
        success: function (data) {
            if (!data.length) {
                projectRolesContainer.html(`<p>No project roles for this project</p>`);
            } else {
                RenderProjectRoles(data);
            }
        },
        error: function () {
            toastr["error"]("Unexpected error occurred");
        }
    });
};

const RenderProjectRoles = (projectRoles) => {
    projectRolesContainer.empty();
    projectRoles.forEach(projectRole => {
        const roleButton = $(`
            <button class="btn btn-outline-primary project-role-btn">
                ${projectRole.name} <i class="bi bi-x-circle"></i>
            </button>
        `);
        projectRolesContainer.append(roleButton);
    });
};

$(document).on("click", "#add-project-role", function() {
    const requestData = {
        ProjectEncodedName: projectEncodedName,
        ProjectRoleName: $("#new-role-name").val().trim()
    };

    $.ajax({
        url: `/Project/AddProjectRole`,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(requestData),
        success: function (response) {
            toastr["success"](response.message);
            $("#new-role-name").val('');
            LoadProjectRoles();
        },
        error: function (xhr) {
            const response = xhr.responseJSON;
            if (response && response.message) {
                toastr["error"](response.message);
            } else {
                toastr["error"]("Unexpected error occurred");
            }
        }
    })
})

$("#close-modal, #cancel-delete").on("click", function () {
    $("#confirm-delete-modal").fadeOut();
});

$(document).on("click", ".project-role-btn", function () {
    roleNameToDelete = $(this).text().trim();
    $("#confirm-delete-modal").fadeIn();
});

$("#confirm-delete").on("click", function () {
    const requestData = {
        ProjectEncodedName: projectEncodedName,
        ProjectRoleName: roleNameToDelete
    }
    $.ajax({
        url: `/Project/RemoveProjectRole/`,
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(requestData),
        success: function (response) {
            toastr["info"](response.message);
            LoadProjectRoles();
        },
        error: function (xhr) {
            const response = xhr.responseJSON;
            if (response && response.message) {
                toastr["error"](response.message);
            } else {
                toastr["error"]("Unexpected error occurred");
            }
        }
    });
    $("#confirm-delete-modal").fadeOut();
});
