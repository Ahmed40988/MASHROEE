document.addEventListener("DOMContentLoaded", function () {
    const searchForm = document.getElementById("searchForm");

    if (searchForm) {
        searchForm.addEventListener("submit", function (event) {
            event.preventDefault();

            const searchBy = document.getElementById("searchByDropdown").value;
            const searchValue = document.querySelector("input[name='searchValue']").value;
            if (!searchBy) {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Please select search by name or search by Price!.',
                    confirmButtonText: 'OK',
                    customClass: {
                        popup: 'swal-custom-popup',
                        title: 'swal-custom-title',
                        confirmButton: 'swal-custom-button'
                    }
                });
                return;
            }
            if (searchBy.toLowerCase() === "price" && isNaN(searchValue)) {
                Swal.fire({
                    icon: 'error',
                    title: 'Error',
                    text: 'Invalid price format. Please enter a valid number.',
                    confirmButtonText: 'OK',
                    customClass: {
                        popup: 'swal-custom-popup',
                        title: 'swal-custom-title',
                        confirmButton: 'swal-custom-button'
                    }
                });
                return;
            }

            searchForm.submit();
        });
    }
});

