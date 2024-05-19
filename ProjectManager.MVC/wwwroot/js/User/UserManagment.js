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
                    toastr["success"]("Deleted user");
                },
                error: function () {
                    toastr["error"]("Something went wrong");
                }
            });
        };

        confirmButton.removeEventListener('click', deleteUserFunction);
        confirmButton.addEventListener('click', deleteUserFunction);

        const usernameSpan = deleteModal.querySelector('#delete-modal-username');
        usernameSpan.textContent = userEmail;
    });

});
