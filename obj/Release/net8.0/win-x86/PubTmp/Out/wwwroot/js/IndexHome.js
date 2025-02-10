
    document.addEventListener('DOMContentLoaded', function () {
        const productList = document.getElementById('product-list');
    let isScrolling;

    productList.addEventListener('scroll', function () {
        window.clearTimeout(isScrolling);
    isScrolling = setTimeout(function () {
        console.log('Scrolling stopped');
            }, 100);
        });
    });
