using MassTransit;
using OrderServiceApp.Models.Events;

namespace OrderServiceApp.Consumers;

public class OrderUpdatedConsumer : IConsumer<OrderUpdated>
{
    public Task Consume(ConsumeContext<OrderUpdated> context)
    {
        var orderUpdated = context.Message;
        Console.WriteLine($"Order updated: {orderUpdated.OrderId}, Customer: {orderUpdated.CustomerName}, Amount: {orderUpdated.TotalAmount}, Updated at: {orderUpdated.UpdatedAt}");
        return Task.CompletedTask;
    }
}