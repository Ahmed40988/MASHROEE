
    function decreaseQuantity(productId, price) {
        const quantityInput = document.getElementById(`quantity-${productId}`);
    let currentValue = parseInt(quantityInput.value);

        if (currentValue > 1) {
        currentValue--;
    quantityInput.value = currentValue;
    updateTotalPrice(productId, price);
        }
    }

    function increaseQuantity(productId, price) {
        const quantityInput = document.getElementById(`quantity-${productId}`);
    let currentValue = parseInt(quantityInput.value);

    currentValue++;
    quantityInput.value = currentValue;
    updateTotalPrice(productId, price);
    }

    function updateTotalPrice(productId, price) {
        const quantityInput = document.getElementById(`quantity-${productId}`);
    const totalPriceElement = document.getElementById(`total-price-${productId}`);

    const quantity = parseInt(quantityInput.value) || 1;
    const total = quantity * price;

    totalPriceElement.textContent = `${total.toFixed(2)} $`;
    }
document.addEventListener("DOMContentLoaded", function () {
    // اختيار النماذج فقط باستخدام ID أو Class
    const forms = document.querySelectorAll("#addto_cart");

    forms.forEach(form => {
        form.addEventListener("submit", function (event) {
            event.preventDefault();

            const formData = new FormData(form);

            fetch(form.action, {
                method: form.method,
                body: formData
            })
                .then(response => response.json())
                .then(data => {
                    Swal.fire({
                        icon: data.success ? 'success' : 'error',
                        title: data.success ? 'Added Successfully!' : 'Error',
                        text: data.message,
                        confirmButtonText: 'OK',
                        customClass: {
                            popup: 'swal-custom-popup',
                            title: 'swal-custom-title',
                            confirmButton: 'swal-custom-button'
                        }
                    });
                })
                .catch(error => {
                    Swal.fire({
                        icon: 'error',
                        title: 'Error',
                        text: 'An error occurred while processing the request.',
                        confirmButtonText: 'OK',
                        customClass: {
                            popup: 'swal-custom-popup',
                            title: 'swal-custom-title',
                            confirmButton: 'swal-custom-button'
                        }
                    });
                });
        });
    });
});

