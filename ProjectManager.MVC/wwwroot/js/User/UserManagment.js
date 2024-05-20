document.addEventListener('DOMContentLoaded', function () {
    const deleteModal = document.getElementById('deleteUserModal');
    deleteModal.addEventListener('show.bs.modal', function (event) {
        const button = event.relatedTarget;
        const userEmail = button.getAttribute('data-username')

        const confirmButton = deleteModal.querySelector('#confirmDelete');

        const deleteUserFunction = () => {
            $.ajax({
                url: `/AdminPanel/User/${userEmail}/Delete`,
                type: 'POST',
                success: function (response) {
                    location.reload();
                },
                error: function () {
                    toastr["error"]("Something went wrong");
                    location.reload();
                }
            });
        };


        confirmButton.removeEventListener('click', deleteUserFunction);
        confirmButton.addEventListener('click', deleteUserFunction);

        const usernameSpan = deleteModal.querySelector('#delete-modal-username');
        usernameSpan.textContent = userEmail;
    });

    const editModal = document.getElementById('editRolesModal');
    editModal.addEventListener('show.bs.modal', function (event) {
        const button = event.relatedTarget;
        const userEmail = button.getAttribute('data-username');
        const roles = button.getAttribute('data-roles').split(',');
        const rolesSelect = editModal.querySelector('#roles');

        Array.from(rolesSelect.options).forEach(option => {
            option.selected = false;
        });

        roles.forEach(role => {
            const option = Array.from(rolesSelect.options).find(o => o.value === role);
            if (option) {
                option.selected = true;
            }
        });

        const editFunction = function () {
            const selectedRoles = Array.from(rolesSelect.selectedOptions).map(option => option.value);

            $.ajax({
                url: `/AdminPanel/User/${userEmail}/Roles`,
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(selectedRoles),
                dataType: 'json',
                success: function (response) {
                    console.log("User roles updated successfully");
                    location.reload();
                },
                error: function () {
                    console.error("Error updating user roles");
                    location.reload();
                }
            });
        };

        const saveRolesButton = editModal.querySelector('#saveRoles');
        saveRolesButton.removeEventListener('click', editFunction);
        saveRolesButton.addEventListener('click', editFunction, { once: true });

        const usernameSpan = editModal.querySelector('#edit-modal-username');
        usernameSpan.textContent = userEmail;
    });
});
