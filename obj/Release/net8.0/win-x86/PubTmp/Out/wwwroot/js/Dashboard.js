function loadPage(pageName) {
    var contentArea = document.getElementById('contentArea');
    var sidebar = document.getElementById('sidebar');

    // إخفاء الشريط الجانبي عند تحميل المحتوى
    sidebar.style.transform = 'translateX(-100%)';
    contentArea.style.display = 'block';

    fetch('/Dashboard/' + pageName)
        .then(response => {
            if (response.ok) {
                return response.text();
            } else {
                contentArea.innerHTML = 'Error!';
            }
        })
        .then(data => {
            contentArea.innerHTML = data;
        })
        .catch(error => {
            contentArea.innerHTML = 'Error!';
            console.error('Error:', error);
        });
}