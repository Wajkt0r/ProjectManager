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
                console.log(response);
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

        const contributorRow = `
                    <tr>
                        <td>${user.userName}</td>
                        <td>${user.email}</td>
                        <td>${rolesBadges}</td>
                        <td class="d-flex justify-content-center align-items-center">
                            <button class="btn btn-sm btn-warning mx-1" onclick="EditRoles('${user.email}')">Edit Roles</button>
                            <button class="btn btn-sm btn-danger mx-1" onclick="RemoveContributor('${user.email}')">Remove</button>
                        </td>
                    </tr>
                `;

        container.append(contributorRow); 
    });
}

const LoadProjectContributors = () => {
    $.ajax({
        url: `/Project/${projectEncodedName}/GetContributors`,
        type: 'GET',
        success: function (data) {
            if (!data.length) {
                $('#contributorsTableBody').html('<tr><td colspan="4" class="text-center">No contributors found.</td></tr>');
            } else {
                RenderProjectContributors(data, $('#contributorsTableBody'));
            }
        },
        error: function () {
            toastr["error"]("Something went wrong");
        }
    });
}

const EditRoles = (userEmail) => {
    console.log(`Edytowanie ról dla ${userEmail}`)
}

const RemoveContributor = (userEmail) => {
    $.ajax({
        url: `/Project/${projectEncodedName}/Contributors/${userEmail}/Remove`,
        type: 'POST',
        success: function (data) {
            toastr["info"](`Removed Contributor ${userEmail}`);
            LoadProjectContributors();
        },
        error: function () {
            toastr["error"]("Something went wrong");
        }

    });
}