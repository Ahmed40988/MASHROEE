
    document.addEventListener("DOMContentLoaded", function () {
        document.querySelectorAll('.remove-form').forEach(form => {
            form.addEventListener('submit', function (e) {
                e.preventDefault();

                Swal.fire({
                    title: 'Are you sure?',
                    text: "You won't be able to revert this!",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Yes, remove it!',
                    customClass: {
                        popup: 'swal-custom-popup',
                        title: 'swal-custom-title',
                        confirmButton: 'swal-custom-button'
                    }
                }).then((result) => {
                    if (result.isConfirmed) {
                        e.target.submit();
                    }
                });
            });
        });

    document.querySelector('.clear-cart-form').addEventListener('submit', function (e) {
        e.preventDefault();

    Swal.fire({
        title: 'Clear Cart',
    text: 'Your cart will be cleared. Are you sure?',
    icon: 'warning',
    showCancelButton: true,
    confirmButtonColor: '#3085d6',
    cancelButtonColor: '#d33',
    confirmButtonText: 'Yes, clear it!',
    customClass: {
        popup: 'swal-custom-popup',
    title: 'swal-custom-title',
    confirmButton: 'swal-custom-button'
                }
            }).then((result) => {
                if (result.isConfirmed) {
        e.target.submit();
                }
            });
        });
    });
