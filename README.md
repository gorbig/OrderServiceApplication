<!DOCTYPE html>
<html lang="ru">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    
</head>
<body>

<h1>Order Service API with API Gateway</h1>

<h2>Описание проекта</h2>

<p>Этот проект представляет собой микросервисную архитектуру для управления заказами (Order Service) на платформе ASP.NET Core. Сервис предоставляет REST API для выполнения CRUD-операций над заказами, используя Entity Framework Core для работы с базой данных PostgreSQL. В дополнение к этому, проект включает в себя API Gateway, реализованный с использованием Ocelot, для маршрутизации запросов к Order Service.</p>

<h2>Сильные стороны проекта</h2>

<ul>
    <li><strong>ASP.NET Core:</strong> Использование современного и производительного фреймворка для разработки веб-приложений.</li>
    <li><strong>Entity Framework Core:</strong> Интеграция с ORM для упрощения работы с базой данных.</li>
    <li><strong>REST API:</strong> Реализация стандартных HTTP методов (GET, POST, PUT, DELETE) для выполнения CRUD-операций.</li>
    <li><strong>Миграции:</strong> Использование миграций для управления схемой базы данных, что упрощает развертывание и поддержку проекта.</li>
    <li><strong>PostgreSQL:</strong> Поддержка реляционной базы данных PostgreSQL для хранения данных о заказах.</li>
    <li><strong>DTO (Data Transfer Object):</strong> Использование DTO для передачи данных между слоями приложения, что улучшает безопасность и упрощает управление данными.</li>
    <li><strong>Валидация:</strong> Реализация валидации данных с использованием атрибутов и Fluent Validation, что обеспечивает целостность данных и предотвращает ошибки.</li>
    <li><strong>Архитектура:</strong> Использование чистой архитектуры (Clean Architecture) с разделением на слои (Controllers, Services, Repositories), что улучшает тестируемость и поддерживаемость кода.</li>
    <li><strong>API Gateway с Ocelot:</strong> Внедрение API Gateway для маршрутизации запросов к Order Service, что улучшает масштабируемость и безопасность приложения.</li>
</ul>

<h2>Требования</h2>

<ul>
    <li>.NET 8 SDK</li>
    <li>PostgreSQL</li>
    <li>Postman (для тестирования API)</li>
    <li>Docker (для контейнеризации и запуска с помощью docker-compose)</li>
    <li>Ocelot (для API Gateway)</li>
</ul>

<h2>Установка и запуск</h2>

<ol>
    <li><strong>Клонирование репозитория:</strong>
        <pre><code>git clone https://github.com/gorbig/OrderServiceApplication.git
cd OrderServiceApplication</code></pre>
    </li>
    <li><strong>Настройка базы данных:</strong>
        <p>Перед запуском приложения убедитесь, что вы настроили подключение к базе данных PostgreSQL. Для этого:</p>
        <ol>
            <li>Перейдите в директорию <code>OrderServiceApplication/OrderService</code>.</li>
            <li>Откройте файл <code>appsettings.json</code>.</li>
            <li>Измените строку подключения к базе данных в разделе <code>ConnectionStrings</code>:</li>
            <pre><code>"ConnectionStrings": {
    "DefaultConnection": "Host=your_db_host;Database=your_db_name;Username=your_db_user;Password=your_db_password"
}</code></pre>
            <p>Замените <code>your_db_host</code>, <code>your_db_name</code>, <code>your_db_user</code> и <code>your_db_password</code> на соответствующие значения вашей базы данных PostgreSQL.</p>
        </ol>
    </li>
    <li><strong>Применение миграций:</strong>
        <p>После настройки подключения к базе данных, выполните миграции для создания необходимых таблиц в базе данных:</p>
        <pre><code>cd OrderService
dotnet ef database update</code></pre>
    </li>
    <li><strong>Запуск приложения с помощью docker-compose:</strong>
        <pre><code>docker-compose up --build</code></pre>
        <p>Приложение будет доступно по адресу <code>http://localhost:8080</code> через API Gateway.</p>
    </li>
</ol>

<h2>Тестирование API с помощью Postman</h2>

<ol>
    <li><strong>Создание заказа (POST)</strong>
        <ul>
            <li>URL: <code>http://localhost:8080/api/orders</code></li>
            <li>Метод: <code>POST</code></li>
            <li>Тело запроса:
                <pre><code>{
  "customerName": "Alice Johnson",
  "totalAmount": 150.75
}</code></pre>
            </li>
            <li>Ответ:
                <pre><code>{
  "id": 1,
  "customerName": "Alice Johnson",
  "totalAmount": 150.75
}</code></pre>
            </li>
        </ul>
    </li>
    <li><strong>Получение списка заказов (GET)</strong>
        <ul>
            <li>URL: <code>http://localhost:8080/api/orders</code></li>
            <li>Метод: <code>GET</code></li>
            <li>Ответ:
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
            </li>
        </ul>
    </li>
    <li><strong>Получение информации о конкретном заказе (GET)</strong>
        <ul>
            <li>URL: <code>http://localhost:8080/api/orders/{id}</code></li>
            <li>Метод: <code>GET</code></li>
            <li>Пример URL: <code>http://localhost:8080/api/orders/1</code></li>
            <li>Ответ:
                <pre><code>{
  "id": 1,
  "customerName": "Alice Johnson",
  "totalAmount": 150.75
}</code></pre>
            </li>
        </ul>
    </li>
    <li><strong>Обновление заказа (PUT)</strong>
        <ul>
            <li>URL: <code>http://localhost:8080/api/orders/{id}</code></li>
            <li>Метод: <code>PUT</code></li>
            <li>Пример URL: <code>http://localhost:8080/api/orders/1</code></li>
            <li>Тело запроса:
                <pre><code>{
  "id": 1,
  "customerName": "Alice Johnson Updated",
  "totalAmount": 175.50
}</code></pre>
            </li>
            <li>Ответ: <code>204 No Content</code></li>
        </ul>
    </li>
    <li><strong>Удаление заказа (DELETE)</strong>
        <ul>
            <li>URL: <code>http://localhost:8080/api/orders/{id}</code></li>
            <li>Метод: <code>DELETE</code></li>
            <li>Пример URL: <code>http://localhost:8080/api/orders/1</code></li>
            <li>Ответ: <code>204 No Content</code></li>
        </ul>
    </li>
</ol>

<h2>Заключение</h2>

<p>Этот проект демонстрирует базовые принципы разработки микросервисной архитектуры с использованием ASP.NET Core, Entity Framework Core и Ocelot API Gateway. Он предоставляет возможность создавать, читать, обновлять и удалять заказы, а также управлять схемой базы данных с помощью миграций. Проект может быть расширен и улучшен для более сложных сценариев использования.</p>

</body>
</html>
