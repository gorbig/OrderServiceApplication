<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">

</head>
<body>

<h1>Order Service API with API Gateway and RabbitMQ</h1>

<h2>Описание проекта</h2>

<p>Этот проект представляет собой микросервисную архитектуру для управления заказами (Order Service) на платформе ASP.NET Core. Сервис предоставляет REST API для выполнения CRUD-операций над заказами, используя Entity Framework Core для работы с базой данных PostgreSQL. В дополнение к этому, проект включает в себя API Gateway, реализованный с использованием Ocelot, для маршрутизации запросов к Order Service.</p>

<p><strong>Новые возможности:</strong></p>
<ul>
    <li><strong>RabbitMQ</strong>: Интеграция с RabbitMQ для асинхронной обработки событий, связанных с заказами. В проекте используются три вида очередей: <code>order-created-queue</code>, <code>order-updated-queue</code>, и <code>order-deleted-queue</code>.</li>
</ul>

<h2>Сильные стороны проекта</h2>

<ul>
    <li><strong>ASP.NET Core</strong>: Использование современного и производительного фреймворка для разработки веб-приложений.</li>
    <li><strong>Entity Framework Core</strong>: Интеграция с ORM для упрощения работы с базой данных.</li>
    <li><strong>REST API</strong>: Реализация стандартных HTTP методов (GET, POST, PUT, DELETE) для выполнения CRUD-операций.</li>
    <li><strong>Миграции</strong>: Использование миграций для управления схемой базы данных, что упрощает развертывание и поддержку проекта.</li>
    <li><strong>PostgreSQL</strong>: Поддержка реляционной базы данных PostgreSQL для хранения данных о заказах.</li>
    <li><strong>DTO (Data Transfer Object)</strong>: Использование DTO для передачи данных между слоями приложения, что улучшает безопасность и упрощает управление данными.</li>
    <li><strong>Валидация</strong>: Реализация валидации данных с использованием атрибутов и Fluent Validation, что обеспечивает целостность данных и предотвращает ошибки.</li>
    <li><strong>Архитектура</strong>: Использование чистой архитектуры (Clean Architecture) с разделением на слои (Controllers, Services, Repositories), что улучшает тестируемость и поддерживаемость кода.</li>
    <li><strong>API Gateway с Ocelot</strong>: Внедрение API Gateway для маршрутизации запросов к Order Service, что улучшает масштабируемость и безопасность приложения.</li>
    <li><strong>RabbitMQ</strong>: Интеграция с RabbitMQ для асинхронной обработки событий, связанных с заказами.</li>
</ul>

<h2>Требования</h2>

<ul>
    <li>.NET 8 SDK</li>
    <li>PostgreSQL</li>
    <li>Postman (для тестирования API)</li>
    <li>Docker (для контейнеризации и запуска с помощью docker-compose)</li>
    <li>Ocelot (для API Gateway)</li>
    <li>RabbitMQ (для асинхронной обработки событий)</li>
</ul>

<h2>Установка и запуск</h2>

<h3>Клонирование репозитория:</h3>

<pre><code>git clone https://github.com/gorbig/OrderServiceApplication.git
cd OrderServiceApplication</code></pre>

<h3>Настройка базы данных:</h3>

<p>Перед запуском приложения убедитесь, что вы настроили подключение к базе данных PostgreSQL. Для этого:</p>

<ol>
    <li>Перейдите в директорию <code>OrderServiceApplication/OrderService</code>.</li>
    <li>Откройте файл <code>appsettings.json</code>.</li>
    <li>Измените строку подключения к базе данных в разделе <code>ConnectionStrings</code>:</li>
</ol>

<pre><code>"ConnectionStrings": {
    "DefaultConnection": "Host=your_db_host;Database=your_db_name;Username=your_db_user;Password=your_db_password"
}</code></pre>

<p>Замените <code>your_db_host</code>, <code>your_db_name</code>, <code>your_db_user</code> и <code>your_db_password</code> на соответствующие значения вашей базы данных PostgreSQL.</p>

<h3>Применение миграций:</h3>

<pre><code>cd OrderService
dotnet ef database update</code></pre>

<h3>Запуск приложения с помощью docker-compose:</h3>

<p>RabbitMQ запускается автоматически вместе с другими сервисами с помощью <code>docker-compose</code>. Запуск остался таким же:</p>

<pre><code>docker-compose up --build</code></pre>

<p>Приложение будет доступно по адресу <code>http://localhost:8080</code> через API Gateway.</p>

<h2>Тестирование API с помощью Postman</h2>

<h3>Создание заказа (POST)</h3>

<ul>
    <li><strong>URL</strong>: <code>http://localhost:8080/api/orders</code></li>
    <li><strong>Метод</strong>: <code>POST</code></li>
    <li><strong>Тело запроса</strong>:</li>
</ul>

<pre><code>{
  "customerName": "Alice Johnson",
  "totalAmount": 150.75
}</code></pre>

<ul>
    <li><strong>Ответ</strong>:</li>
</ul>

<pre><code>{
  "id": 1,
  "customerName": "Alice Johnson",
  "totalAmount": 150.75
}</code></pre>

<h3>Получение списка заказов (GET)</h3>

<ul>
    <li><strong>URL</strong>: <code>http://localhost:8080/api/orders</code></li>
    <li><strong>Метод</strong>: <code>GET</code></li>
    <li><strong>Ответ</strong>:</li>
</ul>

<pre><code>[
  {
    "id": 1,
    "customerName": "Alice Johnson",
    "totalAmount": 150.75
  },
  {
    "id": 2,
    "customerName": "Bob Smith",
    "totalAmount": 200.00
  }
]</code></pre>

<h3>Получение информации о конкретном заказе (GET)</h3>

<ul>
    <li><strong>URL</strong>: <code>http://localhost:8080/api/orders/{id}</code></li>
    <li><strong>Метод</strong>: <code>GET</code></li>
    <li><strong>Пример URL</strong>: <code>http://localhost:8080/api/orders/1</code></li>
    <li><strong>Ответ</strong>:</li>
</ul>

<pre><code>{
  "id": 1,
  "customerName": "Alice Johnson",
  "totalAmount": 150.75
}</code></pre>

<h3>Обновление заказа (PUT)</h3>

<ul>
    <li><strong>URL</strong>: <code>http://localhost:8080/api/orders/{id}</code></li>
    <li><strong>Метод</strong>: <code>PUT</code></li>
    <li><strong>Пример URL</strong>: <code>http://localhost:8080/api/orders/1</code></li>
    <li><strong>Тело запроса</strong>:</li>
</ul>

<pre><code>{
  "id": 1,
  "customerName": "Alice Johnson Updated",
  "totalAmount": 175.50
}</code></pre>

<ul>
    <li><strong>Ответ</strong>: <code>204 No Content</code></li>
</ul>

<h3>Удаление заказа (DELETE)</h3>

<ul>
    <li><strong>URL</strong>: <code>http://localhost:8080/api/orders/{id}</code></li>
    <li><strong>Метод</strong>: <code>DELETE</code></li>
    <li><strong>Пример URL</strong>: <code>http://localhost:8080/api/orders/1</code></li>
    <li><strong>Ответ</strong>: <code>204 No Content</code></li>
</ul>

<h2>Заключение</h2>

<p>Этот проект демонстрирует базовые принципы разработки микросервисной архитектуры с использованием ASP.NET Core, Entity Framework Core, Ocelot API Gateway и RabbitMQ. Он предоставляет возможность создавать, читать, обновлять и удалять заказы, а также управлять схемой базы данных с помощью миграций. Проект может быть расширен и улучшен для более сложных сценариев использования.</p>

</body>
</html>
