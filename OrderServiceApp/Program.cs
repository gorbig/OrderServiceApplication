using Microsoft.EntityFrameworkCore;
using OrderServiceApp.Data;
using OrderServiceApp.IRepositories;
using OrderServiceApp.IServices;
using OrderServiceApp.Repositories;
using OrderServiceApp.Services;

var builder = WebApplication.CreateBuilder(args);

//-- Добавляем конфигурацию для чтения переменных окружения
builder.Configuration.AddEnvironmentVariables();

//--Получаем строку подключения из переменных окружения
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    .Replace("{Host}", Environment.GetEnvironmentVariable("DB_HOST"))
    .Replace("{Port}", Environment.GetEnvironmentVariable("DB_PORT"))
    .Replace("{Database}", Environment.GetEnvironmentVariable("DB_DATABASE"))
    .Replace("{Username}", Environment.GetEnvironmentVariable("DB_USERNAME"))
    .Replace("{Password}", Environment.GetEnvironmentVariable("DB_PASSWORD"));

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddDbContext<OrderContext>(options =>
    options.UseNpgsql(connectionString)
        .LogTo(Console.WriteLine, LogLevel.Information)); // Добавляем логирование для Entity Framework Core

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseRouting();

app.UseAuthorization();

app.MapControllers();

app.Run();