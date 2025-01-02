$(document).ready(function () {

    LoadProjectContributors();

    $("#addProjectContributorModal form").submit(function (event) {
        event.preventDefault();

        $.ajax({
            url: $(this).attr('action'),
            type: $(this).attr('method'),
            data: $(this).serialize(),
            success: function (response) {
                toastr["success"](response.message);
                $("#userEmailInput").val('');
                LoadProjectContributors();
                $('#addProjectContributorModal').modal('hide');
            },
            error: function (xhr) {
                const response = xhr.responseJSON;
                if (response && response.message) {
                    toastr["error"](response.message);
                } else {
                    toastr["error"]("Unexpected error occurred");
                }
                console.log("Error: ", xhr);
            }
        })
    });

    return false;

});

const contributorsContainer = $("#contributors")
const projectEncodedName = contributorsContainer.data('encodedName')

const RenderProjectContributors = (contributors, container) => {
    container.empty(); 

    if (contributors.length === 0) {
        container.html('<tr><td colspan="4" class="text-center">No contributors found.</td></tr>');
        return;
    }

    contributors.forEach(user => {
        const rolesBadges = user.roles.map(role =>
            `<span class="badge badge-primary text-dark">${role}</span>`
        ).join(' ');

        let actionBadges = '';
        if (isEditable) {
            actionButtons = `
                <td class="d-flex justify-content-center align-items-center">
                    <button class="btn btn-sm btn-warning mx-1" onclick="EditRoles('${user.email}')">Edit Roles</button>
                    <button class="btn btn-sm btn-danger mx-1" onclick="RemoveContributor('${user.email}')">Remove</button>
                </td>`;
        }
        

        const contributorRow = `
                    <tr>
                        <td>${user.userName}</td>
                        <td>${user.email}</td>
                        <td>${rolesBadges}</td>
                ${isEditable ? actionButtons : ''}
                    </tr>
                `;

        container.append(contributorRow); 
    });
}

const LoadProjectContributors = () => {
    $.ajax({
        url: `/Project/${projectEncodedName}/ProjectContributors/Get`,
        type: 'GET',
        success: function (data) {
            if (!data.length) {
                $('#contributorsTableBody').html('<tr><td colspan="4" class="text-center">No contributors found.</td></tr>');
            } else {
                RenderProjectContributors(data, $('#contributorsTableBody'));
            }
        },
        error: function () {
            toastr["error"]("Unexpected error occurred");
        }
    });
}

const EditRoles = (userEmail) => {
    $.ajax({
        url: `/Project/${projectEncodedName}/ProjectContributors/${userEmail}/EditRolesForm`,
        type: 'GET',
        success: function (data) {
            $('#editRolesFormContainer').html(data);
            $('#editRolesModal').modal('show');
        },
        error: function () {
            toastr["error"]("Unexpected error occurred");
        }
    })
}

$('#saveRolesButton').click(function () {

    var userId = $('#editRolesForm input[name="UserId"]').val();
    var projectId = $('#editRolesForm input[name="ProjectId"]').val();

    var selectedRoles = [];
    $('#editRolesForm input[name="SelectedRoles"]:checked').each(function () {
        selectedRoles.push($(this).val());
    });

    var requestData = {
        UserId: userId,
        ProjectId: parseInt(projectId),
        SelectedRoles: selectedRoles
    }

    $.ajax({
        url: '/Project/ProjectContributors/UpdateRoles',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(requestData),
        success: function (response) {
            if (response.statusCode === 304) {
                toastr["info"](response.message);
            } else {
                toastr["success"](response.message); 
                $('#editRolesModal').modal('hide'); 
                LoadProjectContributors();
            }
        },
        error: function (xhr) {
            console.log(xhr.statusCode);
            const response = xhr.responseJSON;
            if (response && response.message) {
                toastr["error"](response.message);
            } else {
                toastr["error"]("Unexpected error occurred");
            }
            console.log("Error: ", xhr);
        }
    });
})

const RemoveContributor = (userEmail) => {
    $.ajax({
        url: `/Project/${projectEncodedName}/ProjectContributors/${userEmail}/Remove`,
        type: 'POST',
        success: function (data) {
            toastr["info"](`Removed Contributor ${userEmail}`);
            LoadProjectContributors();
        },
        error: function () {
            toastr["error"]("Unexpected error occurred");
        }

    });
}