const { Pool } = require('pg');

const pool = new Pool({
    user: 'postgres',
    host: 'localhost',
    database: 'tourismDatabase',
    password: 'admin',
    port: 5432,
});

const express = require('express');
const app = express();
const port = 3000;

// Middleware для обработки данных формы
app.use(express.urlencoded({ extended: true }));
app.use(express.json());

// Разрешаем доступ к статическим файлам (например, HTML, CSS, JavaScript)
app.use(express.static('public'));

// Обработчик POST запросов от формы
app.post('/submit-form', (req, res) => {
    const { name, subject, message } = req.body;

    // Проверяем, что все поля присутствуют и не пустые
    if (!name || !subject || !message) {
        return res.status(400).send('Все поля должны быть заполнены');
    }

    // Выполняем SQL запрос для вставки данных в базу данных
    pool.query('INSERT INTO tourismTable (name, subject, message) VALUES ($1, $2, $3)', [name, subject, message], (error, results) => {
        if (error) {
            console.error('Ошибка при выполнении SQL-запроса:', error);
            return res.status(500).send('Произошла ошибка при обработке запроса');
        }
        res.status(201).send('Данные успешно сохранены в базе данных');
    });
});
// server.js
// Обработчик POST запросов для контактных данных
app.post('/submit-contact', (req, res) => {
    const { firstName, lastName, email, phone, departureDate, arrivalDate, notes } = req.body;

    // Проверяем, что все поля присутствуют и не пустые
    if (!firstName || !lastName || !email || !phone || !departureDate || !arrivalDate || !notes) {
        return res.status(400).send('Все поля должны быть заполнены');
    }

    // Выполняем SQL запрос для вставки контактных данных в базу данных
    pool.query('INSERT INTO contactTable (first_name, last_name, email, phone, departure_date, arrival_date, notes) VALUES ($1, $2, $3, $4, $5, $6, $7)', [firstName, lastName, email, phone, departureDate, arrivalDate, notes], (error, results) => {
        if (error) {
            console.error('Ошибка при выполнении SQL-запроса:', error);
            return res.status(500).send('Произошла ошибка при обработке запроса');
        }
        res.status(201).send('Контактные данные успешно сохранены в базе данных');
    });
});

app.post('/package-details.html', (req, res) => {
    // Получение данных из формы package-details.html
    const { firstName, lastName, email, phone, departureDate, arrivalDate, notes } = req.body;
    // Здесь можно выполнить различные действия с полученными данными, например, сохранить их в базе данных
    // В данном примере просто отправляем ответ об успешном получении данных\
    res.redirect('/');
});

// Слушаем порт 3000
app.listen(port, () => {
  console.log(`Сервер запущен на http://localhost:${port}`);
});
