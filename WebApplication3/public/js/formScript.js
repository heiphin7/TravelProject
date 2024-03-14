$(document).ready(function() {
    $('form').submit(function(e) {
        e.preventDefault(); // Предотвращаем отправку формы по умолчанию
        
        // Получаем данные формы
        var formData = {
            name: $('#name').val(),
            email: $('#email').val(),
            phone: $('#phone').val(),
            date1: $('#date1').val(),
            date2: $('#date2').val(),
            message: $('#message').val()
        };

        // Отправляем данные на сервер
        $.ajax({
            type: 'POST',
            url: '/submit-form', // Укажите URL вашего серверного обработчика формы
            data: formData,
            success: function(response) {
                // В случае успеха выполните необходимые действия
                alert('Данные успешно отправлены на сервер');
            },
            error: function(xhr, status, error) {
                // В случае ошибки вы можете выполнить другие действия
                alert('Произошла ошибка при отправке данных на сервер');
            }
        });
    });
});
