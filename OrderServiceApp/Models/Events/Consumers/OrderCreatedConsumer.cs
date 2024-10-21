using MassTransit;
using OrderServiceApp.Models.Events;

namespace OrderServiceApp.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreated>
{
    public Task Consume(ConsumeContext<OrderCreated> context)
    {
        var orderCreated = context.Message;
        Console.WriteLine($"Order created: {orderCreated.OrderId}, Customer: {orderCreated.CustomerName}, Amount: {orderCreated.TotalAmount}, Created at: {orderCreated.CreatedAt}");
        return Task.CompletedTask;
    }
}