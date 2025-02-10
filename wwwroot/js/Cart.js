
document.getElementById("confirm-order-btn").addEventListener("click", function () {
    Swal.fire({
        icon: 'success',
        title: 'Order Confirmed',
        text: 'Your order has been successfully confirmed!',
        confirmButtonText: 'OK'
    }).then((result) => {
        if (result.isConfirmed) {
            fetch('/Cart/ClearCart', {
                method: 'POST'
            })
            const confirmButton = document.getElementById("confirm-order-btn");

            confirmButton.classList.remove("btn-primary");
            confirmButton.classList.add("btn-success");
            confirmButton.innerHTML = "✅ Confirmed";
            confirmButton.style.pointerEvents = "none";
            document.getElementById("confirm-order-form").submit();
        }
    });
});

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
