using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrderServiceApp.Data;
using OrderServiceApp.IRepositories;
using OrderServiceApp.IServices;
using OrderServiceApp.Repositories;
using OrderServiceApp.Services;
using OrderServiceApp.Consumers;
using OrderServiceApp.Models.Events;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderCreatedConsumer>(); 
    x.AddConsumer<OrderUpdatedConsumer>(); 
    x.AddConsumer<OrderDeletedConsumer>(); 

    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("rabbitmq", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        // Настройка публикации сообщений в нужные обменники
        cfg.Message<OrderCreated>(e => e.SetEntityName("OrderServiceApp.Models.Events:OrderCreated"));
        cfg.Message<OrderUpdated>(e => e.SetEntityName("OrderServiceApp.Models.Events:OrderUpdated"));
        cfg.Message<OrderDeleted>(e => e.SetEntityName("OrderServiceApp.Models.Events:OrderDeleted"));

        // Определяем тип обменника для каждого типа сообщения
        cfg.Publish<OrderCreated>(e => e.ExchangeType = ExchangeType.Fanout);
        cfg.Publish<OrderUpdated>(e => e.ExchangeType = ExchangeType.Fanout);
        cfg.Publish<OrderDeleted>(e => e.ExchangeType = ExchangeType.Fanout);

        // Настраиваем потребители для очередей
        cfg.ReceiveEndpoint("order-created-queue", e =>
        {
            e.ConfigureConsumer<OrderCreatedConsumer>(context);
        });

        cfg.ReceiveEndpoint("order-updated-queue", e =>
        {
            e.ConfigureConsumer<OrderUpdatedConsumer>(context);
        });

        cfg.ReceiveEndpoint("order-deleted-queue", e =>
        {
            e.ConfigureConsumer<OrderDeletedConsumer>(context);
        });
    });
});

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddDebug();

builder.Services.AddDbContext<OrderContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
        .LogTo(Console.WriteLine, LogLevel.Information)); 

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